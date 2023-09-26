using ET.Ability;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DotRecast.Detour;
using DotRecast.Recast.Toolset;
using DotRecast.Recast.Toolset.Builder;
using DotRecast.Recast.Toolset.Geom;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof (GamePlayTowerDefenseComponent))]
    [FriendOf(typeof (Unit))]
    public static class GamePlayTowerDefenseComponentSystem
    {
        [ObjectSystem]
        public class GamePlayTowerDefenseComponentAwakeSystem: AwakeSystem<GamePlayTowerDefenseComponent>
        {
            protected override void Awake(GamePlayTowerDefenseComponent self)
            {
            }
        }

        [ObjectSystem]
        public class GamePlayTowerDefenseComponentDestroySystem: DestroySystem<GamePlayTowerDefenseComponent>
        {
            protected override void Destroy(GamePlayTowerDefenseComponent self)
            {
            }
        }

        [ObjectSystem]
        public class GamePlayTowerDefenseComponentFixedUpdateSystem: FixedUpdateSystem<GamePlayTowerDefenseComponent>
        {
            protected override void FixedUpdate(GamePlayTowerDefenseComponent self)
            {
                if (self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static void FixedUpdate(this GamePlayTowerDefenseComponent self, float fixedDeltaTime)
        {
        }

        public static Unit GetHomeUnit(this GamePlayTowerDefenseComponent self)
        {
            return self.GetComponent<PutHomeComponent>().GetHomeUnit();
        }

        public static float3 GetHomePosition(this GamePlayTowerDefenseComponent self)
        {
            return self.GetComponent<PutHomeComponent>().GetPosition();
        }

        public static float3 GetCallMonsterPosition(this GamePlayTowerDefenseComponent self, long playerId)
        {
            return self.GetComponent<PutMonsterCallComponent>().GetPosition(playerId);
        }

        public static GamePlayComponent GetGamePlay(this GamePlayTowerDefenseComponent self)
        {
            GamePlayComponent gamePlayComponent = self.GetParent<GamePlayComponent>();
            return gamePlayComponent;
        }

        public static List<long> GetPlayerList(this GamePlayTowerDefenseComponent self)
        {
            GamePlayComponent gamePlayComponent = self.GetGamePlay();
            return gamePlayComponent.GetPlayerList();
        }

        public static void NoticeToClientAll(this GamePlayTowerDefenseComponent self)
        {
            List<long> playerList = self.GetPlayerList();
            for (int i = 0; i < playerList.Count; i++)
            {
                self.NoticeToClient(playerList[i]);
            }
        }

        public static void NoticeToClient(this GamePlayTowerDefenseComponent self, long playerId)
		{
			EventType.WaitNoticeGamePlayModeToClient _WaitNoticeGamePlayModeChgToClient = new ()
			{
				playerId = playerId,
				gamePlayComponent = self.GetGamePlay(),
			};
			EventSystem.Instance.Publish(self.DomainScene(), _WaitNoticeGamePlayModeChgToClient);
        }

        public static void Init(this GamePlayTowerDefenseComponent self, long ownerPlayerId, string gamePlayModeCfgId)
        {
            self.gamePlayModeCfgId = gamePlayModeCfgId;
            self.ownerPlayerId = ownerPlayerId;
            self.AddComponent<PlayerOwnerTowersComponent>();
            MonsterWaveCallComponent monsterWaveCallComponent = self.AddComponent<MonsterWaveCallComponent>();
            monsterWaveCallComponent.Init(self.model.MonsterWaveCallRuleCfgId);

            self.InitPlayerCoin();

            self.DoReadyForBattle().Coroutine();
        }

        public static async ETTask DoReadyForBattle(this GamePlayTowerDefenseComponent self)
        {
            await self.TransToPutHome();
            await ETTask.CompletedTask;
        }

        public static void InitPlayerCoin(this GamePlayTowerDefenseComponent self)
        {
            int initGold = self.model.PlayerInitGold;
            List<long> playerList = self.GetPlayerList();
            for (int i = 0; i < playerList.Count; i++)
            {
                ET.GamePlayHelper.SetPlayerCoin(self.DomainScene(), playerList[i], CoinType.Gold, initGold);
            }
        }

        public static void Start(this GamePlayTowerDefenseComponent self)
        {
            GamePlayComponent gamePlayComponent = self.GetGamePlay();
            gamePlayComponent.Start();

            self.TransToShowStartEffect().Coroutine();
        }

        public static async ETTask TransToShowStartEffect(this GamePlayTowerDefenseComponent self)
        {
            self.gamePlayTowerDefenseStatus = GamePlayTowerDefenseStatus.ShowStartEffect;
            self.NoticeToClientAll();

            await TimerComponent.Instance.WaitAsync(2000);

            if (self.IsDisposed)
            {
                return;
            }
            await self.TransToRestTime();
        }

        public static async ETTask TransToPutHome(this GamePlayTowerDefenseComponent self)
        {
            self.gamePlayTowerDefenseStatus = GamePlayTowerDefenseStatus.PutHome;
            self.NoticeToClientAll();
            await ETTask.CompletedTask;
        }

        public static async ETTask TransToPutMonsterPoint(this GamePlayTowerDefenseComponent self)
        {
            self.gamePlayTowerDefenseStatus = GamePlayTowerDefenseStatus.PutMonsterPoint;
            self.NoticeToClientAll();
            await ETTask.CompletedTask;
        }

        public static async ETTask TransToBattle(this GamePlayTowerDefenseComponent self)
        {
            self.RemoveComponent<RestTimeComponent>();
            self.GetComponent<MonsterWaveCallComponent>().DoNextMonsterWaveCall();
            self.gamePlayTowerDefenseStatus = GamePlayTowerDefenseStatus.InTheBattle;
            self.NoticeToClientAll();
            await ETTask.CompletedTask;
        }

        public static async ETTask TransToRestTime(this GamePlayTowerDefenseComponent self)
        {
            RestTimeComponent restTimeComponent = self.GetComponent<RestTimeComponent>();
            if (restTimeComponent == null)
            {
                restTimeComponent = self.AddComponent<RestTimeComponent>();
            }

            restTimeComponent.Init(self.model.ResTime);


            if (self.gamePlayTowerDefenseStatus != GamePlayTowerDefenseStatus.ShowStartEffect)
            {
                self.GetComponent<PlayerOwnerTowersComponent>().RefreshAllPlayerTowerPool();
                self.DoPlayerCoinInterestOnDeposit();
                await TimerComponent.Instance.WaitAsync(1000);
                if (self.IsDisposed)
                {
                    return;
                }
                self.DoPlayerCoinWaveRewardGold();

                await TimerComponent.Instance.WaitAsync(1000);
                if (self.IsDisposed)
                {
                    return;
                }
            }

            self.gamePlayTowerDefenseStatus = GamePlayTowerDefenseStatus.RestTime;
            self.NoticeToClientAll();
        }

        public static void DoPlayerCoinInterestOnDeposit(this GamePlayTowerDefenseComponent self)
        {
            int interestOnDeposit = self.model.InterestOnDeposit;
            List<long> playerList = self.GetPlayerList();
            for (int i = 0; i < playerList.Count; i++)
            {
                GamePlayPlayerListComponent gamePlayPlayerListComponent = self.GetGamePlay().GetComponent<GamePlayPlayerListComponent>();
                int curGold = gamePlayPlayerListComponent.GetPlayerCoin(playerList[i], CoinType.Gold);
                int interestGold = (int)(curGold * interestOnDeposit * 0.01f);
                gamePlayPlayerListComponent.ChgPlayerCoin(playerList[i], CoinType.Gold, interestGold, GetCoinType.InterestOnDeposit);
            }
        }

        public static void DoPlayerCoinWaveRewardGold(this GamePlayTowerDefenseComponent self)
        {
            MonsterWaveCallComponent monsterWaveCallComponent = self.GetComponent<MonsterWaveCallComponent>();
            int waveRewardGold = monsterWaveCallComponent.GetWaveRewardGold();
            List<long> playerList = self.GetPlayerList();
            for (int i = 0; i < playerList.Count; i++)
            {
                GamePlayPlayerListComponent gamePlayPlayerListComponent = self.GetGamePlay().GetComponent<GamePlayPlayerListComponent>();

                gamePlayPlayerListComponent.ChgPlayerCoin(playerList[i], CoinType.Gold, waveRewardGold, GetCoinType.WaveRewardGold);
            }
        }

        public static async ETTask TransToGameSuccess(this GamePlayTowerDefenseComponent self)
        {
            self.gamePlayTowerDefenseStatus = GamePlayTowerDefenseStatus.GameSuccess;

            GamePlayComponent gamePlayComponent = self.GetGamePlay();
            gamePlayComponent.GameEnd();

            self.NoticeToClientAll();
            await ETTask.CompletedTask;
        }

        public static async ETTask TransToGameFailed(this GamePlayTowerDefenseComponent self)
        {
            self.gamePlayTowerDefenseStatus = GamePlayTowerDefenseStatus.GameFailed;

            GamePlayComponent gamePlayComponent = self.GetGamePlay();
            gamePlayComponent.GameEnd();

            self.NoticeToClientAll();
            await ETTask.CompletedTask;
        }

        public static bool ChkIsGameEnd(this GamePlayTowerDefenseComponent self)
        {
            if (self.gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.GameSuccess
                || self.gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.GameFailed)
            {
                return true;
            }

            return false;
        }

        public static GamePlayTowerDefenseStatus GetGamePlayTowerDefenseStatus(this GamePlayTowerDefenseComponent self)
        {
            return self.gamePlayTowerDefenseStatus;
        }

        public static (bool, string) ChkBuyPlayerTower(this GamePlayTowerDefenseComponent self, long playerId, int index)
        {
            return self.GetComponent<PlayerOwnerTowersComponent>().ChkBuyPlayerTower(playerId, index);
        }

        public static bool BuyPlayerTower(this GamePlayTowerDefenseComponent self, long playerId, int index)
        {
            return self.GetComponent<PlayerOwnerTowersComponent>().BuyPlayerTower(playerId, index);
        }

        public static (bool, string) ChkRefreshBuyPlayerTower(this GamePlayTowerDefenseComponent self, long playerId)
        {
            return self.GetComponent<PlayerOwnerTowersComponent>().ChkRefreshTowerPool(playerId);
        }

        public static bool RefreshBuyPlayerTower(this GamePlayTowerDefenseComponent self, long playerId)
        {
            return self.GetComponent<PlayerOwnerTowersComponent>().RefreshTowerPool(playerId);
        }

        public static int GetLeftCallPlayerTowerCount(this GamePlayTowerDefenseComponent self, long playerId)
        {
            return self.GetComponent<PlayerOwnerTowersComponent>().GetLeftCallPlayerTowerCount(playerId);
        }

        public static (bool, string) ChkCallPlayerTower(this GamePlayTowerDefenseComponent self, long playerId, string towerCfgId)
        {
            return self.GetComponent<PlayerOwnerTowersComponent>().ChkCallPlayerTower(playerId, towerCfgId);
        }

        public static bool CallPlayerTower(this GamePlayTowerDefenseComponent self, long playerId, string towerCfgId, float3 position)
        {
            return self.GetComponent<PlayerOwnerTowersComponent>().CallPlayerTower(playerId, towerCfgId, position);
        }

        public static (bool, string) ChkIsUpgradeMaxPlayerTower(this GamePlayTowerDefenseComponent self, string towerCfgId)
        {
            return self.GetComponent<PlayerOwnerTowersComponent>().ChkIsUpgradeMaxPlayerTower(towerCfgId);
        }

        public static (bool, string, Dictionary<string, int>) ChkUpgradePlayerTower(this GamePlayTowerDefenseComponent self, long playerId, long towerUnitId)
        {
            return self.GetComponent<PlayerOwnerTowersComponent>().ChkUpgradePlayerTower(playerId, towerUnitId);
        }

        public static bool UpgradePlayerTower(this GamePlayTowerDefenseComponent self, long playerId, long towerUnitId)
        {
            return self.GetComponent<PlayerOwnerTowersComponent>().UpgradePlayerTower(playerId, towerUnitId);
        }

        public static bool ScalePlayerTower(this GamePlayTowerDefenseComponent self, long playerId, long towerUnitId)
        {
            return self.GetComponent<PlayerOwnerTowersComponent>().ScalePlayerTower(playerId, towerUnitId);
        }

        public static (bool, string) ChkReclaimPlayerTower(this GamePlayTowerDefenseComponent self, long playerId, long towerUnitId)
        {
            return self.GetComponent<PlayerOwnerTowersComponent>().ChkReclaimPlayerTower(playerId, towerUnitId);
        }

        public static bool ReclaimPlayerTower(this GamePlayTowerDefenseComponent self, long playerId, long towerUnitId)
        {
            return self.GetComponent<PlayerOwnerTowersComponent>().ReclaimPlayerTower(playerId, towerUnitId);
        }

        public static void DealFriendTeamFlagType(this GamePlayTowerDefenseComponent self)
        {
            ListComponent<TeamFlagType> teamFlagTypes = ListComponent<TeamFlagType>.Create();
            teamFlagTypes.Add(TeamFlagType.TeamGlobal1);
            GamePlayComponent gamePlayComponent = self.GetGamePlay();
            gamePlayComponent.DealFriendTeamFlag(teamFlagTypes, true, true);
        }

        public static void DealEscape(this GamePlayTowerDefenseComponent self, Unit unit)
        {
            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();

            long attackValue = numericComponent.GetAsInt(NumericType.PhysicalAttack);
            Damage damage = new(NumericType.PhysicalAttack, attackValue);
            ET.Ability.DamageHelper.CreateDamageInfo(unit, self.GetHomeUnit(), damage, 0, 0, null);
            unit.DestroyWithDeathShow();
        }

        public static void DealUnitBeKill(this GamePlayTowerDefenseComponent self, Unit attackerUnit, Unit beKillUnit)
        {
            long attackerPlayerId = GamePlayHelper.GetPlayerIdByUnitId(attackerUnit);
            if (attackerPlayerId == -1)
            {
                return;
            }

            long beKillUnitPlayerId = GamePlayHelper.GetPlayerIdByUnitId(beKillUnit);
            if (beKillUnitPlayerId != -1)
            {
                return;
            }

            MonsterWaveCallComponent monsterWaveCallComponent = self.GetComponent<MonsterWaveCallComponent>();
            int rewardGold = monsterWaveCallComponent.GetMonsterRewardGoldByUnitId(beKillUnit.Id);
            if (rewardGold > 0)
            {
                ET.GamePlayHelper.ChgPlayerCoin(self.DomainScene(), attackerPlayerId, CoinType.Gold, rewardGold);
            }
        }

        public static bool ChkIsNearTower(this GamePlayTowerDefenseComponent self, float3 targetPos, float targetUnitRadius)
        {
            PlayerOwnerTowersComponent playerOwnerTowersComponent = self.GetComponent<PlayerOwnerTowersComponent>();
            return playerOwnerTowersComponent.ChkIsNearTower(targetPos, targetUnitRadius);
        }

    }
}