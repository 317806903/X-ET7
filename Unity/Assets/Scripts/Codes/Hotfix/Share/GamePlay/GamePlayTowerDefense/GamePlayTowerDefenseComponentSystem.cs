using ET.Ability;
using System;
using System.Collections.Generic;
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
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
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

        public static float3 GetHomePosition(this GamePlayTowerDefenseComponent self, long playerId)
        {
            return self.GetComponent<PutHomeComponent>().GetPosition(playerId);
        }

        public static float3 GetCallMonsterPosition(this GamePlayTowerDefenseComponent self, long playerId)
        {
            return self.GetComponent<PutMonsterCallComponent>().GetPosition(playerId);
        }

        public static long GetCallMonsterUnitId(this GamePlayTowerDefenseComponent self, long playerId)
        {
            return self.GetComponent<PutMonsterCallComponent>().GetCallMonsterUnitId(playerId);
        }

        public static async ETTask Init(this GamePlayTowerDefenseComponent self, long ownerPlayerId, string gamePlayModeCfgId, RoomTypeInfo roomTypeInfo)
        {
            self.gamePlayModeCfgId = gamePlayModeCfgId;
            self.roomTypeInfo = roomTypeInfo;
            self.ownerPlayerId = ownerPlayerId;
            PlayerOwnerTowersComponent playerOwnerTowersComponent = self.AddComponent<PlayerOwnerTowersComponent>();
            await playerOwnerTowersComponent.Init();

            MonsterWaveCallComponent monsterWaveCallComponent = self.AddComponent<MonsterWaveCallComponent>();
            monsterWaveCallComponent.Init(self.model.MonsterWaveCallRuleCfgId, self.model.MonsterWaveCallStartWaveIndex);

            await self.InitPlayerGameRecover();
            self.InitPlayerCoin();

        }

        public static async ETTask DoReadyForBattle(this GamePlayTowerDefenseComponent self)
        {
            await self.Start();
            await ETTask.CompletedTask;
        }

        public static bool IsEndlessChallengeMonster(this GamePlayTowerDefenseComponent self)
        {
            GamePlayBattleLevelCfg gamePlayBattleLevelCfg = self.GetGamePlay().GetGamePlayBattleConfig();
            return gamePlayBattleLevelCfg.GamePlayMode is GamePlayTowerDefenseEndlessChallengeMonster;
        }

        public static bool IsEndlessChallengeMode(this GamePlayTowerDefenseComponent self)
        {
            if (self.roomTypeInfo.roomType == RoomType.Normal)
            {
                if (self.roomTypeInfo.subRoomType == SubRoomType.NormalEndlessChallenge)
                {
                    return true;
                }
            }
            else if (self.roomTypeInfo.roomType == RoomType.AR)
            {
                if (self.roomTypeInfo.subRoomType == SubRoomType.AREndlessChallenge)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsPVEMode(this GamePlayTowerDefenseComponent self)
        {
            if (self.roomTypeInfo.roomType == RoomType.Normal)
            {
                if (self.roomTypeInfo.subRoomType == SubRoomType.NormalPVE)
                {
                    return true;
                }
            }
            else if (self.roomTypeInfo.roomType == RoomType.AR)
            {
                if (self.roomTypeInfo.subRoomType == SubRoomType.ARPVE)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsPVPMode(this GamePlayTowerDefenseComponent self)
        {
            if (self.roomTypeInfo.roomType == RoomType.Normal)
            {
                if (self.roomTypeInfo.subRoomType == SubRoomType.NormalPVP)
                {
                    return true;
                }
            }
            else if (self.roomTypeInfo.roomType == RoomType.AR)
            {
                if (self.roomTypeInfo.subRoomType == SubRoomType.ARPVP)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsTutorialFirstModel(this GamePlayTowerDefenseComponent self)
        {
            if (self.roomTypeInfo.roomType == RoomType.Normal)
            {
                return false;
            }
            else if (self.roomTypeInfo.roomType == RoomType.AR)
            {
                if (self.roomTypeInfo.subRoomType == SubRoomType.ARTutorialFirst)
                {
                    return true;
                }
            }
            return false;
        }

        public static async ETTask InitPlayerGameRecover(this GamePlayTowerDefenseComponent self)
        {
            int recoverTimeoutTime = 0;
            int recoverFreeTimes = 0;
            int recoverByWatchAdTimes = 0;
            int recoverCostArcadeCoinTimes = 0;
            int recoverCostArcadeCoinNum = 0;
            int recoverAddHp = 0;
            int recoverAddGold = 0;
            if (ET.SceneHelper.ChkIsGameModeArcade())
            {
                recoverTimeoutTime = GlobalSettingCfgCategory.Instance.GameModeArcadeRecoverTimeoutTime;
                recoverByWatchAdTimes = 0;
                if (self.IsEndlessChallengeMode())
                {
                    recoverFreeTimes = GlobalSettingCfgCategory.Instance.GameModeArcadeEndlessChallengeReviveTimeWhenFree;
                    recoverCostArcadeCoinTimes = GlobalSettingCfgCategory.Instance.GameModeArcadeEndlessChallengeReviveTimeWhenPay;
                    recoverCostArcadeCoinNum = GlobalSettingCfgCategory.Instance.GameModeArcadeEndlessChallengeCostWhenRevive;
                }
                else if (self.IsPVPMode())
                {
                    recoverFreeTimes = GlobalSettingCfgCategory.Instance.GameModeArcadePVPReviveTimeWhenFree;
                    recoverCostArcadeCoinTimes = GlobalSettingCfgCategory.Instance.GameModeArcadePVPReviveTimeWhenPay;
                    recoverCostArcadeCoinNum = GlobalSettingCfgCategory.Instance.GameModeArcadePVPCostWhenRevive;
                }
                recoverAddHp = GlobalSettingCfgCategory.Instance.GameModeArcadeRecoverAddHp;
                recoverAddGold = GlobalSettingCfgCategory.Instance.GameModeArcadeRecoverAddGold;
            }
            else if (GlobalSettingCfgCategory.Instance.AdmobAvailable)
            {
                recoverTimeoutTime = GlobalSettingCfgCategory.Instance.RecoverTimeoutTime;
                if (self.IsEndlessChallengeMode())
                {
                    recoverFreeTimes = 0;
                    recoverByWatchAdTimes = 1;
                    recoverCostArcadeCoinTimes = 0;
                }
                recoverAddHp = GlobalSettingCfgCategory.Instance.RecoverAddHp;
                recoverAddGold = GlobalSettingCfgCategory.Instance.RecoverAddGold;
            }

            int recoverByWatchAdTimesNew = recoverByWatchAdTimes;
            Dictionary<long, TeamFlagType> allPlayerTeamFlag = self.GetGamePlay().GetAllPlayerTeamFlag();
            foreach (var playerTeamFlag in allPlayerTeamFlag)
            {
                long playerId = playerTeamFlag.Key;
                TeamFlagType homeTeamFlagType = self.GetHomeTeamFlagTypeByPlayer(playerId);

                GamePlayHelper.ChgGamePlayNumericValueByHomeTeamFlagType(self.DomainScene(), homeTeamFlagType, GameNumericType.TowerDefense_PlayerResurrectionTimesBase, recoverByWatchAdTimes, true);

                float tmp = GamePlayHelper.GetGamePlayNumericValueByHomeTeamFlagType(self.DomainScene(), homeTeamFlagType, GameNumericType.TowerDefense_PlayerResurrectionTimes);
                if (recoverByWatchAdTimesNew < tmp)
                {
                    recoverByWatchAdTimesNew = (int)tmp;
                }
            }
            recoverByWatchAdTimes = recoverByWatchAdTimesNew;


            GameRecoverComponent gameRecoverComponent = self.AddComponent<GameRecoverComponent>();
            gameRecoverComponent.Init(recoverTimeoutTime, recoverFreeTimes, recoverByWatchAdTimes, recoverCostArcadeCoinTimes, recoverCostArcadeCoinNum, recoverAddHp, recoverAddGold);

            await ETTask.CompletedTask;
        }

        public static void InitPlayerCoin(this GamePlayTowerDefenseComponent self)
        {
            int initGold = self.model.PlayerInitGold;
            List<long> playerList = self.GetPlayerList();
            for (int i = 0; i < playerList.Count; i++)
            {
                long playerId = playerList[i];
                GamePlayHelper.ChgGamePlayNumericValueByPlayerId(self.DomainScene(), playerId, GameNumericType.TowerDefense_PlayerInitGoldBase, initGold, true);
                float newInitGold = GamePlayHelper.GetGamePlayNumericValueByPlayerId(self.DomainScene(), playerId, GameNumericType.TowerDefense_PlayerInitGold);

                ET.GamePlayHelper.SetPlayerCoin(self.DomainScene(), playerId, CoinTypeInGame.Gold, (int)newInitGold);
            }
        }

        public static async ETTask Start(this GamePlayTowerDefenseComponent self)
        {
            GamePlayComponent gamePlayComponent = self.GetGamePlay();
            gamePlayComponent.Start();

            self.gamePlayTowerDefenseStatus = GamePlayTowerDefenseStatus.GameBegin;
            await self.DoNextStep();

            await ETTask.CompletedTask;
        }

        public static async ETTask DoNextStep(this GamePlayTowerDefenseComponent self)
        {
            if (self.gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.GameEnd
                || self.gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.Recover
                || self.gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.Recovering
                || self.gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.WaitRescan)
            {
                return;
            }
            Dictionary<GamePlayTowerDefenseStatus, GamePlayTowerDefenseStatus> nextStep = new()
            {
                { GamePlayTowerDefenseStatus.GameBegin, GamePlayTowerDefenseStatus.WaitMeshFinished },
                { GamePlayTowerDefenseStatus.WaitMeshFinished, GamePlayTowerDefenseStatus.PutHome },
                { GamePlayTowerDefenseStatus.PutHome, GamePlayTowerDefenseStatus.PutMonsterPoint },
                { GamePlayTowerDefenseStatus.PutMonsterPoint, GamePlayTowerDefenseStatus.ShowStartEffect },
                { GamePlayTowerDefenseStatus.ShowStartEffect, GamePlayTowerDefenseStatus.RestTime },
                { GamePlayTowerDefenseStatus.RestTime, GamePlayTowerDefenseStatus.InTheBattle },
                { GamePlayTowerDefenseStatus.InTheBattleEnd, GamePlayTowerDefenseStatus.RestTime },
            };
            if (nextStep.ContainsKey(self.gamePlayTowerDefenseStatus) == false)
            {
                Log.Error($"ET.GamePlayTowerDefenseComponentSystem.DoNextStep nextStep.ContainsKey({self.gamePlayTowerDefenseStatus}) == false");
                return;
            }
            var nextStatus = nextStep[self.gamePlayTowerDefenseStatus];
            switch (nextStatus)
            {
                case GamePlayTowerDefenseStatus.WaitMeshFinished:
                    await self.TransToWaitMeshFinished();
                    break;
                case GamePlayTowerDefenseStatus.PutHome:
                    await self.TransToPutHome();
                    break;
                case GamePlayTowerDefenseStatus.PutMonsterPoint:
                    await self.TransToPutMonsterPoint();
                    break;
                case GamePlayTowerDefenseStatus.ShowStartEffect:
                    await self.TransToShowStartEffect();
                    break;
                case GamePlayTowerDefenseStatus.RestTime:
                    await self.TransToRestTime();
                    break;
                case GamePlayTowerDefenseStatus.InTheBattle:
                    await self.TransToInTheBattleBegin();
                    break;
            }
        }

        public static async ETTask TransToShowStartEffect(this GamePlayTowerDefenseComponent self)
        {
            self.gamePlayTowerDefenseStatus = GamePlayTowerDefenseStatus.ShowStartEffect;

            self.NoticeToClientAll();

            GamePlayComponent gamePlayComponent = self.GetGamePlay();
            gamePlayComponent.StartShow();
            gamePlayComponent.NoticeGameBegin2Server();

            EventSystem.Instance.Publish(self.DomainScene(), new ET.Ability.AbilityTriggerEventType.GamePlayTowerDefense_Status_ShowStartEffectBegin());

            await TimerComponent.Instance.WaitAsync(2000);

            if (self.IsDisposed)
            {
                return;
            }

            EventSystem.Instance.Publish(self.DomainScene(), new ET.Ability.AbilityTriggerEventType.GamePlayTowerDefense_Status_ShowStartEffectEnd());

            await self.DoNextStep();
        }

        public static async ETTask TransToWaitMeshFinished(this GamePlayTowerDefenseComponent self)
        {
            self.gamePlayTowerDefenseStatus = GamePlayTowerDefenseStatus.WaitMeshFinished;
            self.NoticeToClientAll(true);

            GamePlayComponent gamePlayComponent = self.GetGamePlay();
            while (true)
            {
                (bool isLoadMeshFinished, bool isLoadMeshError) = gamePlayComponent.ChkNavMeshReady();
                if (isLoadMeshError)
                {
                    self.gamePlayTowerDefenseStatus = GamePlayTowerDefenseStatus.LoadMeshErr;
                    self.NoticeToClientAll(true);
                    return;
                }
                else if (isLoadMeshFinished == false)
                {
                    await TimerComponent.Instance.WaitAsync(1000);
                    if (self.IsDisposed)
                    {
                        return;
                    }
                }
                else
                {
                    break;
                }
            }

            await self.DoNextStep();

            await ETTask.CompletedTask;
        }

        public static async ETTask TransToPutHome(this GamePlayTowerDefenseComponent self)
        {
            self.GetComponent<PlayerOwnerTowersComponent>().RefreshAllPlayerTowerPool();

            PutHomeComponent putHomeComponent = self.GetComponent<PutHomeComponent>();
            if (putHomeComponent == null)
            {
                putHomeComponent = self.AddComponent<PutHomeComponent>();
            }

            self.gamePlayTowerDefenseStatus = GamePlayTowerDefenseStatus.PutHome;
            self.NoticeToClientAll(true);

            EventSystem.Instance.Publish(self.DomainScene(), new ET.Ability.AbilityTriggerEventType.GamePlayTowerDefense_Status_PutHomeBegin());

            await ETTask.CompletedTask;
        }

        public static async ETTask TransToPutMonsterPoint(this GamePlayTowerDefenseComponent self)
        {
            self.gamePlayTowerDefenseStatus = GamePlayTowerDefenseStatus.PutMonsterPoint;

            EventSystem.Instance.Publish(self.DomainScene(), new ET.Ability.AbilityTriggerEventType.GamePlayTowerDefense_Status_PutMonsterPointBegin());

            GamePlayBattleLevelCfg gamePlayBattleLevelCfg = self.GetGamePlay().GetGamePlayBattleConfig();
            if (gamePlayBattleLevelCfg.TeamMode is PlayerTeam)
            {
                PutHomeComponent putHomeComponent = self.GetComponent<PutHomeComponent>();
                (float3 midPos, float3 forward) = putHomeComponent.GetMidPos();

                PutMonsterCallComponent putMonsterCallComponent = self.AddComponent<PutMonsterCallComponent>();
                putMonsterCallComponent.InitWhenPVP(midPos, forward);
            }
            else
            {
                self.NoticeToClientAll();
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask FinishedPutMonsterPoint(this GamePlayTowerDefenseComponent self)
        {
            EventSystem.Instance.Publish(self.DomainScene(), new ET.Ability.AbilityTriggerEventType.GamePlayTowerDefense_Status_PutMonsterPointEnd());

            self.DoNextStep().Coroutine();
            await ETTask.CompletedTask;
        }

        public static async ETTask TransToInTheBattleBegin(this GamePlayTowerDefenseComponent self)
        {
            self.RecordPlayerInfo();
            self.RemoveComponent<RestTimeComponent>();
            self.GetComponent<MonsterWaveCallComponent>().DoNextMonsterWaveCall();
            self.gamePlayTowerDefenseStatus = GamePlayTowerDefenseStatus.InTheBattle;
            self.NoticeToClientAll();

            EventSystem.Instance.Publish(self.DomainScene(), new ET.Ability.AbilityTriggerEventType.GamePlayTowerDefense_Status_InTheBattleBegin());

            await ETTask.CompletedTask;
        }

        public static async ETTask TransToInTheBattleEnd(this GamePlayTowerDefenseComponent self)
        {
            if(self.ChkIsGameEnd() || self.ChkIsGameRecover() || self.ChkIsGameRecovering())
            {
                return;
            }
            EventSystem.Instance.Publish(self.DomainScene(), new ET.Ability.AbilityTriggerEventType.GamePlayTowerDefense_Status_InTheBattleEnd());

            self.GetComponent<PutHomeComponent>().RecoveryHomeHP();

            if (self.model.IsAutoRefreshBuyTower)
            {
                self.GetComponent<PlayerOwnerTowersComponent>().RefreshAllPlayerTowerPool();
            }

            self.gamePlayTowerDefenseStatus = GamePlayTowerDefenseStatus.InTheBattleEnd;
            self.NoticeToClientAll();
            await TimerComponent.Instance.WaitAsync(100);

            bool bRet = self.DoPlayerCoinDivideEquallyTeamCoin();
            if (bRet)
            {
                await TimerComponent.Instance.WaitAsync(1000);
                if (self.IsDisposed)
                {
                    return;
                }
            }

            bRet = self.DoPlayerCoinInterestOnDeposit();
            if (bRet)
            {
                await TimerComponent.Instance.WaitAsync(1000);
                if (self.IsDisposed)
                {
                    return;
                }
            }

            bRet = self.DoPlayerCoinWaveRewardGold();
            if (bRet)
            {
                await TimerComponent.Instance.WaitAsync(1000);
                if (self.IsDisposed)
                {
                    return;
                }
            }

            await self.DoNextStep();
        }

        public static async ETTask TransToRestTime(this GamePlayTowerDefenseComponent self)
        {
            RestTimeComponent restTimeComponent = self.GetComponent<RestTimeComponent>();
            if (restTimeComponent == null)
            {
                restTimeComponent = self.AddComponent<RestTimeComponent>();
            }

            restTimeComponent.Init(self.model.ResTime);

            self.gamePlayTowerDefenseStatus = GamePlayTowerDefenseStatus.RestTime;
            self.NoticeToClientAll();

            EventSystem.Instance.Publish(self.DomainScene(), new ET.Ability.AbilityTriggerEventType.GamePlayTowerDefense_Status_RestTimeBegin());

            await ETTask.CompletedTask;
        }

        public static bool DoPlayerCoinDivideEquallyTeamCoin(this GamePlayTowerDefenseComponent self)
        {
            GamePlayPlayerListComponent gamePlayPlayerListComponent = self.GetGamePlay().GetComponent<GamePlayPlayerListComponent>();
            (bool bRet, MultiDictionary<long, string, int> playerId2Coins) = gamePlayPlayerListComponent.GetTeamCoin2Players();
            if (bRet == false)
            {
                return false;
            }

            foreach (var playerId2Coin in playerId2Coins)
            {
                foreach (var coin in playerId2Coin.Value)
                {
                    CoinTypeInGame coinType = (CoinTypeInGame)Enum.Parse(typeof (CoinTypeInGame), coin.Key);
                    gamePlayPlayerListComponent.ChgPlayerCoin(playerId2Coin.Key, coinType, coin.Value, GetCoinType.DivideEquallyTeamCoin);
                }
            }
            return true;
        }

        public static bool DoPlayerCoinInterestOnDeposit(this GamePlayTowerDefenseComponent self)
        {
            int interestOnDeposit = self.model.InterestOnDeposit;
            if (interestOnDeposit == 0)
            {
                return false;
            }
            List<long> playerList = self.GetPlayerList();
            for (int i = 0; i < playerList.Count; i++)
            {
                GamePlayPlayerListComponent gamePlayPlayerListComponent = self.GetGamePlay().GetComponent<GamePlayPlayerListComponent>();
                float curGold = gamePlayPlayerListComponent.GetPlayerCoin(playerList[i], CoinTypeInGame.Gold);
                float interestGold = curGold * interestOnDeposit * 0.01f;
                gamePlayPlayerListComponent.ChgPlayerCoin(playerList[i], CoinTypeInGame.Gold, interestGold, GetCoinType.InterestOnDeposit);
            }

            return true;
        }

        public static bool DoPlayerCoinWaveRewardGold(this GamePlayTowerDefenseComponent self)
        {
            MonsterWaveCallComponent monsterWaveCallComponent = self.GetComponent<MonsterWaveCallComponent>();
            int waveRewardGold = monsterWaveCallComponent.GetWaveRewardGold();
            if (waveRewardGold == 0)
            {
                return false;
            }
            List<long> playerList = self.GetPlayerList();
            for (int i = 0; i < playerList.Count; i++)
            {
                GamePlayPlayerListComponent gamePlayPlayerListComponent = self.GetGamePlay().GetComponent<GamePlayPlayerListComponent>();

                gamePlayPlayerListComponent.ChgPlayerCoin(playerList[i], CoinTypeInGame.Gold, waveRewardGold, GetCoinType.WaveRewardGold);
            }

            return true;
        }

        public static async ETTask TransToRecover(this GamePlayTowerDefenseComponent self)
        {
            self.gamePlayTowerDefenseStatus = GamePlayTowerDefenseStatus.Recover;

            self.GetGamePlay().PauseAllAI();

            self.NoticeToClientAll();
            await ETTask.CompletedTask;
        }

        public static async ETTask TransToWaitRescan(this GamePlayTowerDefenseComponent self)
        {
            GamePlayComponent gamePlayComponent = self.GetGamePlay();
            gamePlayComponent.StopAllAI();
            gamePlayComponent.NoticeGameEndToRoom(false);

            self.gamePlayTowerDefenseStatus = GamePlayTowerDefenseStatus.WaitRescan;

            self.NoticeToClientAll();

            gamePlayComponent.AllPlayerQuit().Coroutine();

            await ETTask.CompletedTask;
        }

        public static async ETTask TransToRecovering(this GamePlayTowerDefenseComponent self)
        {
            self.gamePlayTowerDefenseStatus = GamePlayTowerDefenseStatus.Recovering;

            self.NoticeToClientAll();
            await ETTask.CompletedTask;
        }

        public static bool ChkNeedToRecover(this GamePlayTowerDefenseComponent self)
        {
            GameRecoverComponent gameRecoverComponent = self.GetComponent<GameRecoverComponent>();
            return gameRecoverComponent.ChkNeedToRecover();
        }

        public static async ETTask TransToGameResult(this GamePlayTowerDefenseComponent self, bool bSuccess, bool isTimeOut = false)
        {
            if (bSuccess)
            {
                await self.TransToGameEnd(isTimeOut);
            }
            else
            {
                if (self.ChkNeedToRecover())
                {
                    await self.TransToRecover();
                }
                else
                {
                    await self.TransToGameEnd(isTimeOut);
                }
            }
        }

        public static async ETTask TransToGameEnd(this GamePlayTowerDefenseComponent self, bool isTimeOut = false)
        {

            self.gamePlayTowerDefenseStatus = GamePlayTowerDefenseStatus.GameEnd;

            await self.GameEnd(isTimeOut);

            GamePlayComponent gamePlayComponent = self.GetGamePlay();
            await gamePlayComponent.GameEnd();

            self.NoticeToClientAll();

            await ETTask.CompletedTask;
        }

        public static async ETTask GameEnd(this GamePlayTowerDefenseComponent self, bool isTimeOut = false)
        {
            PutHomeComponent putHomeComponent = self.GetComponent<PutHomeComponent>();
            putHomeComponent.BattleResult(isTimeOut);

            await ETTask.CompletedTask;
        }

        public static bool ChkIsGameEnd(this GamePlayTowerDefenseComponent self)
        {
            if (self.gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.GameEnd)
            {
                return true;
            }

            return false;
        }

        public static bool ChkIsGameWaitRescan(this GamePlayTowerDefenseComponent self)
        {
            if (self.gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.WaitRescan)
            {
                return true;
            }

            return false;
        }

        public static bool ChkIsGameInTheBattle(this GamePlayTowerDefenseComponent self)
        {
            if (self.gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.InTheBattle)
            {
                return true;
            }

            return false;
        }

        public static bool ChkIsGameRecover(this GamePlayTowerDefenseComponent self)
        {
            if (self.gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.Recover)
            {
                return true;
            }

            return false;
        }

        public static bool ChkIsGameRecovering(this GamePlayTowerDefenseComponent self)
        {
            if (self.gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.Recovering)
            {
                return true;
            }

            return false;
        }

        public static bool ChkHomeWin(this GamePlayTowerDefenseComponent self, long playerId)
        {
            PutHomeComponent putHomeComponent = self.GetComponent<PutHomeComponent>();
            return putHomeComponent.ChkHomeWin(playerId);
        }

        public static Dictionary<long, bool> GetPlayerWinResult(this GamePlayTowerDefenseComponent self)
        {
            DictionaryComponent<long, bool> dic = DictionaryComponent<long, bool>.Create();
            List<long> playerList = self.GetPlayerList();
            foreach (long playerId in playerList)
            {
                dic[playerId] = self.ChkHomeWin(playerId);
            }
            return dic;
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

        public static (bool, string, Dictionary<string, int>, List<long>) ChkUpgradePlayerTower(this GamePlayTowerDefenseComponent self, long playerId, long towerUnitId, bool onlyChkPool)
        {
            return self.GetComponent<PlayerOwnerTowersComponent>().ChkUpgradePlayerTower(playerId, towerUnitId, onlyChkPool);
        }

        public static (bool, string, Dictionary<string, int>, List<long>) ChkUpgradePlayerTower(this GamePlayTowerDefenseComponent self, long playerId, string towerCfgId, bool onlyChkPool)
        {
            return self.GetComponent<PlayerOwnerTowersComponent>().ChkUpgradePlayerTower(playerId, 0, towerCfgId, onlyChkPool);
        }

        public static bool UpgradePlayerTower(this GamePlayTowerDefenseComponent self, long playerId, long towerUnitId, bool onlyChkPool)
        {
            return self.GetComponent<PlayerOwnerTowersComponent>().UpgradePlayerTower(playerId, towerUnitId, onlyChkPool);
        }

        public static bool UpgradePlayerTower(this GamePlayTowerDefenseComponent self, long playerId, string towerCfgId, bool onlyChkPool)
        {
            return self.GetComponent<PlayerOwnerTowersComponent>().UpgradePlayerTower(playerId, towerCfgId, onlyChkPool);
        }

        public static bool ScalePlayerTower(this GamePlayTowerDefenseComponent self, long playerId, long towerUnitId)
        {
            return self.GetComponent<PlayerOwnerTowersComponent>().ScalePlayerTower(playerId, towerUnitId);
        }

        public static bool ScalePlayerTowerCard(this GamePlayTowerDefenseComponent self, long playerId, string towerCfgId)
        {
            return self.GetComponent<PlayerOwnerTowersComponent>().ScalePlayerTowerCard(playerId, towerCfgId);
        }

        public static (bool, string) ChkReclaimPlayerTower(this GamePlayTowerDefenseComponent self, long playerId, long towerUnitId)
        {
            return self.GetComponent<PlayerOwnerTowersComponent>().ChkReclaimPlayerTower(playerId, towerUnitId);
        }

        public static bool ReclaimPlayerTower(this GamePlayTowerDefenseComponent self, long playerId, long towerUnitId)
        {
            return self.GetComponent<PlayerOwnerTowersComponent>().ReclaimPlayerTower(playerId, towerUnitId);
        }

        public static (bool, string) ChkMovePlayerTower(this GamePlayTowerDefenseComponent self, long playerId, long towerUnitId, float3 position)
        {
            return self.GetComponent<PlayerOwnerTowersComponent>().ChkMovePlayerTower(playerId, towerUnitId, position);
        }

        public static bool MovePlayerTower(this GamePlayTowerDefenseComponent self, long playerId, long towerUnitId, float3 position)
        {
            return self.GetComponent<PlayerOwnerTowersComponent>().MovePlayerTower(playerId, towerUnitId, position);
        }

        public static void SetReadyWhenRestTime(this GamePlayTowerDefenseComponent self, long playerId)
        {
            RestTimeComponent restTimeComponent = self.GetComponent<RestTimeComponent>();
            restTimeComponent?.SetReadyWhenRestTime(playerId);
            self.NoticeToClientAll();
        }

        /// <summary>
        /// 处理阵营关系
        /// </summary>
        /// <param name="self"></param>
        public static void DealFriendTeamFlagType(this GamePlayTowerDefenseComponent self)
        {
            // ListComponent<TeamFlagType> teamFlagTypes = ListComponent<TeamFlagType>.Create();
            // teamFlagTypes.Add(TeamFlagType.TeamGlobal1);
            // GamePlayComponent gamePlayComponent = self.GetGamePlay();
            // gamePlayComponent.DealFriendTeamFlag(teamFlagTypes, true, true);

            GamePlayComponent gamePlayComponent = self.GetGamePlay();
            //TeamPlayer1, TeamPlayer2 之间设成 友好
            gamePlayComponent.DealFriendTeamFlag(null, true, true);

            var allPlayerTeamFlag = gamePlayComponent.GetAllPlayerTeamFlag();
            //TeamPlayer1, TeamGlobal1, Monster2 之间设成 友好
            foreach (var allPlayerTeamFlagOne in allPlayerTeamFlag)
            {
                long playerId = allPlayerTeamFlagOne.Key;
                TeamFlagType teamFlagType = allPlayerTeamFlagOne.Value;
                List<TeamFlagType> teamFlagTypes = new();
                teamFlagTypes.Add(teamFlagType);
                teamFlagTypes.Add(self.GetHomeTeamFlagTypeByPlayer(playerId));
                foreach (var allPlayerTeamFlagOneOther in allPlayerTeamFlag)
                {
                    long otherPlayerId = allPlayerTeamFlagOneOther.Key;
                    TeamFlagType otherTeamFlagType = allPlayerTeamFlagOneOther.Value;
                    if (teamFlagType == otherTeamFlagType)
                    {
                        continue;
                    }
                    teamFlagTypes.Add(self.GetMonsterTeamFlagTypeByPlayer(otherPlayerId));
                }
                gamePlayComponent.DealFriendTeamFlag(teamFlagTypes, false, false);
            }

            //TeamPlayer1, TeamGlobal1, TeamGlobal2 之间设成 友好
            foreach (var allPlayerTeamFlagOne in allPlayerTeamFlag)
            {
                long playerId = allPlayerTeamFlagOne.Key;
                TeamFlagType teamFlagType = allPlayerTeamFlagOne.Value;
                List<TeamFlagType> teamFlagTypes = new();
                teamFlagTypes.Add(teamFlagType);
                teamFlagTypes.Add(self.GetHomeTeamFlagTypeByPlayer(playerId));
                foreach (var allPlayerTeamFlagOneOther in allPlayerTeamFlag)
                {
                    long otherPlayerId = allPlayerTeamFlagOneOther.Key;
                    TeamFlagType otherTeamFlagType = allPlayerTeamFlagOneOther.Value;
                    if (teamFlagType == otherTeamFlagType)
                    {
                        continue;
                    }
                    teamFlagTypes.Add(self.GetHomeTeamFlagTypeByPlayer(otherPlayerId));
                }
                gamePlayComponent.DealFriendTeamFlag(teamFlagTypes, false, false);
            }

            //Monster1, Monster2 之间设成 友好
            foreach (var allPlayerTeamFlagOne in allPlayerTeamFlag)
            {
                long playerId = allPlayerTeamFlagOne.Key;
                TeamFlagType teamFlagType = allPlayerTeamFlagOne.Value;
                List<TeamFlagType> teamFlagTypes = new();
                teamFlagTypes.Add(self.GetMonsterTeamFlagTypeByPlayer(playerId));
                foreach (var allPlayerTeamFlagOneOther in allPlayerTeamFlag)
                {
                    long otherPlayerId = allPlayerTeamFlagOneOther.Key;
                    TeamFlagType otherTeamFlagType = allPlayerTeamFlagOneOther.Value;
                    if (teamFlagType == otherTeamFlagType)
                    {
                        continue;
                    }
                    teamFlagTypes.Add(self.GetMonsterTeamFlagTypeByPlayer(otherPlayerId));
                }
                gamePlayComponent.DealFriendTeamFlag(teamFlagTypes, false, false);
            }
        }

        public static void RecordPlayerInfo(this GamePlayTowerDefenseComponent self)
        {
            GamePlayPlayerListComponent gamePlayPlayerListComponent = self.GetGamePlay().GetComponent<GamePlayPlayerListComponent>();
            gamePlayPlayerListComponent.RecordPlayerGold();

            PutHomeComponent putHomeComponent = self.GetComponent<PutHomeComponent>();
            putHomeComponent.RecordHomeHp();
        }

        public static void DealRecover(this GamePlayTowerDefenseComponent self)
        {
            MonsterWaveCallComponent monsterWaveCallComponent = self.GetComponent<MonsterWaveCallComponent>();
            monsterWaveCallComponent.RecoverWaveIndex();

            GameRecoverComponent gameRecoverComponent = self.GetComponent<GameRecoverComponent>();

            GamePlayPlayerListComponent gamePlayPlayerListComponent = self.GetGamePlay().GetComponent<GamePlayPlayerListComponent>();
            int recoverAddGold = gameRecoverComponent.recoverAddGold;
            gamePlayPlayerListComponent.RecoverPlayerGold(recoverAddGold);

            PutHomeComponent putHomeComponent = self.GetComponent<PutHomeComponent>();
            int recoverAddHp = gameRecoverComponent.recoverAddHp;
            putHomeComponent.RecoverHomeHp(recoverAddHp);

            self.GetGamePlay().RecoveryAllAI();

            self.TransToRestTime().Coroutine();
        }

        public static void DealEscape(this GamePlayTowerDefenseComponent self, Unit unit, Unit homeUnit)
        {
            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();

            long attackValue = numericComponent.GetAsInt(NumericType.PhysicalAttack);
            Damage damage = new(NumericType.PhysicalAttack, attackValue);
            ET.Ability.DamageHelper.CreateDamageInfo(unit, homeUnit, damage, false);
            unit.DestroyWithDeathShow();
        }

        public static void DealUnitBeKill(this GamePlayTowerDefenseComponent self, Unit attackerUnit, Unit beKillUnit)
        {
            long beKillUnitPlayerId = GamePlayHelper.GetPlayerIdByUnitId(beKillUnit);
            if (beKillUnitPlayerId != -1)
            {
                TowerComponent towerComponent = beKillUnit.GetComponent<TowerComponent>();
                if (towerComponent != null)
                {
                    self.GetComponent<PlayerOwnerTowersComponent>().DestroyPlayerTower(beKillUnitPlayerId, beKillUnit.Id);
                }

                return;
            }

            long attackerPlayerId = GamePlayHelper.GetPlayerIdByUnitId(attackerUnit);
            if (attackerPlayerId != -1)
            {
                MonsterComponent monsterComponent = beKillUnit.GetComponent<MonsterComponent>();
                if (monsterComponent != null)
                {
                    int rewardGold = monsterComponent.rewardGold;
                    if (rewardGold > 0)
                    {
                        // GamePlayBattleLevelCfg gamePlayBattleLevelCfg = self.GetGamePlay().GetGamePlayBattleConfig();
                        // if (gamePlayBattleLevelCfg.TeamMode is PlayerTeam)
                        // {
                        //     ET.GamePlayHelper.ChgTeamCoin(self.DomainScene(), attackerPlayerId, CoinTypeInGame.Gold, rewardGold);
                        // }
                        // else
                        // {
                        //     ET.GamePlayHelper.ChgPlayerCoin(self.DomainScene(), attackerPlayerId, CoinTypeInGame.Gold, rewardGold);
                        // }

                        long playerId = attackerPlayerId;
                        GamePlayHelper.ChgGamePlayNumericValueByPlayerId(self.DomainScene(), playerId, GameNumericType.TowerDefense_PlayerRewardWhenKillMonsterBase, rewardGold, true);
                        float newRewardGold = GamePlayHelper.GetGamePlayNumericValueByPlayerId(self.DomainScene(), playerId, GameNumericType.TowerDefense_PlayerRewardWhenKillMonster);

                        ET.GamePlayHelper.ChgPlayerCoinShare(self.DomainScene(), attackerPlayerId, CoinTypeInGame.Gold, (int)newRewardGold, beKillUnit);

                    }
                }
            }

        }

        public static List<long> GetPutTowers(this GamePlayTowerDefenseComponent self, long playerId)
        {
            PlayerOwnerTowersComponent playerOwnerTowersComponent = self.GetComponent<PlayerOwnerTowersComponent>();
            return playerOwnerTowersComponent.GetPutTowers(playerId);
        }

        public static int GetPutAttackTowerLimitCount(this GamePlayTowerDefenseComponent self, long playerId)
        {
            PlayerOwnerTowersComponent playerOwnerTowersComponent = self.GetComponent<PlayerOwnerTowersComponent>();
            return playerOwnerTowersComponent.GetPutAttackTowerLimitCount(playerId);
        }

        public static int GetPutAttackTowerCount(this GamePlayTowerDefenseComponent self, long playerId)
        {
            PlayerOwnerTowersComponent playerOwnerTowersComponent = self.GetComponent<PlayerOwnerTowersComponent>();
            return playerOwnerTowersComponent.GetPutAttackTowerCount(playerId);
        }

        public static Dictionary<string, int> GetPlayerOwnerTowers(this GamePlayTowerDefenseComponent self, long playerId)
        {
            PlayerOwnerTowersComponent playerOwnerTowersComponent = self.GetComponent<PlayerOwnerTowersComponent>();
            return playerOwnerTowersComponent.GetPlayerOwnerTowers(playerId);
        }

        public static bool ChkIsNearTower(this GamePlayTowerDefenseComponent self, float3 targetPos, float targetUnitRadius, long playerId = -1, long ignoreTowerUnitId = -1)
        {
            PlayerOwnerTowersComponent playerOwnerTowersComponent = self.GetComponent<PlayerOwnerTowersComponent>();
            return playerOwnerTowersComponent.ChkIsNearTower(targetPos, targetUnitRadius, playerId, ignoreTowerUnitId);
        }

        public static TeamFlagType GetHomeTeamFlagTypeByPlayer(this GamePlayTowerDefenseComponent self, long playerId)
        {
            return GamePlayHelper.GetHomeTeamFlagTypeByPlayer(self.DomainScene(), playerId);
        }

        public static TeamFlagType GetMonsterTeamFlagTypeByPlayer(this GamePlayTowerDefenseComponent self, long playerId)
        {
            return GamePlayHelper.GetMonsterTeamFlagTypeByPlayer(self.DomainScene(), playerId);
        }

        public static (TeamFlagType, Unit) GetNearHostileHomeByPlayerId(this GamePlayTowerDefenseComponent self, long playerId, float3 pos)
        {
            TeamFlagType playerTeamFlagType = self.GetGamePlay().GetTeamFlagByPlayerId(playerId);
            PutHomeComponent putHomeComponent = self.GetComponent<PutHomeComponent>();
            return putHomeComponent.GetNearHostileHomeByPlayerId(playerTeamFlagType, pos);
        }

        public static TeamFlagType GetPlayerCallMonsterTeamFlagTypeByPlayer(this GamePlayTowerDefenseComponent self, long playerId, float3 pos)
        {
            (TeamFlagType nearHostileTeamFlagType, Unit homeUnit)  = self.GetNearHostileHomeByPlayerId(playerId, pos);
            return ET.GamePlayTowerDefenseHelper.GetMonsterTeamFlagType(nearHostileTeamFlagType);
        }

    }
}