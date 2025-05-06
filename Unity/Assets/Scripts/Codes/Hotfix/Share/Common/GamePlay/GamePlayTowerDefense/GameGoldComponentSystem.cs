using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof(GameGoldComponent))]
    public static class GameGoldComponentSystem
    {
        [ObjectSystem]
        public class GameGoldComponentAwakeSystem : AwakeSystem<GameGoldComponent>
        {
            protected override void Awake(GameGoldComponent self)
            {
                self.playerId2TeamScale = new();
                self.Init();
            }
        }

        [ObjectSystem]
        public class GameGoldComponentDestroySystem : DestroySystem<GameGoldComponent>
        {
            protected override void Destroy(GameGoldComponent self)
            {
            }
        }

        [ObjectSystem]
        public class GameGoldComponentFixedUpdateSystem: FixedUpdateSystem<GameGoldComponent>
        {
            protected override void FixedUpdate(GameGoldComponent self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static void Init(this GameGoldComponent self)
        {
            self.InitPlayerId2TeamScale();

            self.InitPlayerCoin();
        }

        public static void FixedUpdate(this GameGoldComponent self, float fixedDeltaTime)
        {
            if (self.ChkIsNeedIncreaseCoinWhenInterval() == false)
            {
                return;
            }

            if (self.nextIncreaseTime == 0 || self.nextIncreaseTime > TimeHelper.ServerNow())
            {
                return;
            }

            self.increaseNum++;
            self.DoPlayerIncreaseCoinWhenInterval(self.increaseNum);
            self.StartIncreaseCoinWhenInterval();
        }

        public static GamePlayComponent GetGamePlay(this GameGoldComponent self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
            GamePlayComponent gamePlayComponent = gamePlayTowerDefenseComponent.GetParent<GamePlayComponent>();
            return gamePlayComponent;
        }

        public static GamePlayTowerDefenseComponent GetGamePlayTowerDefense(this GameGoldComponent self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
            return gamePlayTowerDefenseComponent;
        }

        public static void InitPlayerId2TeamScale(this GameGoldComponent self)
        {
            var homeTeamFlagPlayerCountDic = self.GetHomeTeamFlagPlayerCount();
            int maxTeamCount = 0;
            foreach (var item in homeTeamFlagPlayerCountDic)
            {
                if (maxTeamCount < item.Value)
                {
                    maxTeamCount = item.Value;
                }
            }
            List<long> playerList = self.GetPlayerList();
            foreach (long playerId in playerList)
            {
                TeamFlagType homeTeamFlagType = self.GetGamePlayTowerDefense().GetHomeTeamFlagTypeByPlayer(playerId);
                int homeTeamFlagPlayerCount = homeTeamFlagPlayerCountDic[homeTeamFlagType];

                self.playerId2TeamScale[playerId] =  (float)maxTeamCount / homeTeamFlagPlayerCount;
            }
        }

        public static void InitPlayerCoin(this GameGoldComponent self)
        {
            int initGold = self.GetGamePlayTowerDefense().model.IncreaseGold.PlayerInitGold;

            List<long> playerList = self.GetPlayerList();
            foreach (long playerId in playerList)
            {
                float playerInitGold = initGold * self.playerId2TeamScale[playerId];

                GamePlayHelper.ChgGamePlayNumericValueByPlayerId(self.DomainScene(), playerId, GameNumericType.TowerDefense_PlayerInitGoldBase, playerInitGold, true);
                float newInitGold = GamePlayHelper.GetGamePlayNumericValueByPlayerId(self.DomainScene(), playerId, GameNumericType.TowerDefense_PlayerInitGold);
                ET.GamePlayHelper.SetPlayerCoin(self.DomainScene(), playerId, CoinTypeInGame.Gold, (int)newInitGold);
            }
        }

        public static bool ChkIsNeedIncreaseCoinWhenWave(this GameGoldComponent self)
        {
            int InitGoldWhenWave = self.GetGamePlayTowerDefense().model.IncreaseGold.InitGoldWhenWave;
            float IncreaseGoldWhenWave = self.GetGamePlayTowerDefense().model.IncreaseGold.IncreaseGoldWhenWave;
            float IncreasePercentGoldWhenWave = self.GetGamePlayTowerDefense().model.IncreaseGold.IncreasePercentGoldWhenWave;
            if (InitGoldWhenWave == 0 && IncreaseGoldWhenWave == 0 && IncreasePercentGoldWhenWave == 0)
            {
                return false;
            }
            return true;
        }

        public static bool ChkIsNeedIncreaseCoinWhenInterval(this GameGoldComponent self)
        {
            int Interval = self.GetGamePlayTowerDefense().model.IncreaseGold.Interval;
            int InitGoldWhenInterval = self.GetGamePlayTowerDefense().model.IncreaseGold.InitGoldWhenInterval;
            float IncreaseGoldWhenInterval = self.GetGamePlayTowerDefense().model.IncreaseGold.IncreaseGoldWhenInterval;
            float IncreasePercentGoldWhenInterval = self.GetGamePlayTowerDefense().model.IncreaseGold.IncreasePercentGoldWhenInterval;
            if (Interval <= 0)
            {
                return false;
            }
            if (InitGoldWhenInterval == 0 && IncreaseGoldWhenInterval == 0 && IncreasePercentGoldWhenInterval == 0)
            {
                return false;
            }
            return true;
        }

        public static void StartIncreaseCoinWhenInterval(this GameGoldComponent self)
        {
            int Interval = self.GetGamePlayTowerDefense().model.IncreaseGold.Interval;
            self.nextIncreaseTime = TimeHelper.ServerNow() + Interval * 1000;
        }

        public static void DoPlayerIncreaseCoinWhenInterval(this GameGoldComponent self, int intervalTimes)
        {
            int initGoldWhenInterval = self.GetGamePlayTowerDefense().model.IncreaseGold.InitGoldWhenInterval;
            float increaseGoldWhenInterval = self.GetGamePlayTowerDefense().model.IncreaseGold.IncreaseGoldWhenInterval;
            float increasePercentGoldWhenInterval = self.GetGamePlayTowerDefense().model.IncreaseGold.IncreasePercentGoldWhenInterval;
            float playerIncreaseCoinWhenInterval = initGoldWhenInterval * (1 + increasePercentGoldWhenInterval * intervalTimes) + increaseGoldWhenInterval * intervalTimes;

            List<long> playerList = self.GetPlayerList();
            foreach (long playerId in playerList)
            {
                float increaseCoinWhenInterval = playerIncreaseCoinWhenInterval * self.playerId2TeamScale[playerId];

                GamePlayHelper.ChgGamePlayNumericValueByPlayerId(self.DomainScene(), playerId, GameNumericType.TowerDefense_PlayerInitGoldBase, increaseCoinWhenInterval, true);
                float newIncreaseCoinWhenInterval = GamePlayHelper.GetGamePlayNumericValueByPlayerId(self.DomainScene(), playerId, GameNumericType.TowerDefense_PlayerInitGold);

                GamePlayPlayerListComponent gamePlayPlayerListComponent = self.GetGamePlayerListComponent();
                gamePlayPlayerListComponent.ChgPlayerCoin(playerId, CoinTypeInGame.Gold, newIncreaseCoinWhenInterval, GetCoinType.IntervalRewardGold);
            }
        }

        public static void DoPlayerIncreaseCoinWhenWave(this GameGoldComponent self)
        {
            MonsterWaveCallComponent monsterWaveCallComponent = self.GetGamePlayTowerDefense().GetComponent<MonsterWaveCallComponent>();
            int waveIndex = monsterWaveCallComponent.GetWaveIndex();
            if (waveIndex == 0)
            {
                return;
            }

            int initGoldWhenWave = self.GetGamePlayTowerDefense().model.IncreaseGold.InitGoldWhenWave;
            float increaseGoldWhenWave = self.GetGamePlayTowerDefense().model.IncreaseGold.IncreaseGoldWhenWave;
            float increasePercentGoldWhenWave = self.GetGamePlayTowerDefense().model.IncreaseGold.IncreasePercentGoldWhenWave;
            float playerIncreaseCoinWhenWave = initGoldWhenWave * (1 + increasePercentGoldWhenWave * waveIndex) + increaseGoldWhenWave * waveIndex;

            monsterWaveCallComponent.ResetWaveRewardGold(ref playerIncreaseCoinWhenWave);

            List<long> playerList = self.GetPlayerList();
            foreach (long playerId in playerList)
            {
                float increaseCoinWhenWave = playerIncreaseCoinWhenWave * self.playerId2TeamScale[playerId];

                GamePlayHelper.ChgGamePlayNumericValueByPlayerId(self.DomainScene(), playerId, GameNumericType.TowerDefense_PlayerInitGoldBase, increaseCoinWhenWave, true);
                float newIncreaseCoinWhenWave = GamePlayHelper.GetGamePlayNumericValueByPlayerId(self.DomainScene(), playerId, GameNumericType.TowerDefense_PlayerInitGold);

                GamePlayPlayerListComponent gamePlayPlayerListComponent = self.GetGamePlayerListComponent();
                gamePlayPlayerListComponent.ChgPlayerCoin(playerId, CoinTypeInGame.Gold, newIncreaseCoinWhenWave, GetCoinType.WaveRewardGold);
            }
        }

        public static Dictionary<TeamFlagType, int> GetHomeTeamFlagPlayerCount(this GameGoldComponent self)
        {
            Dictionary<TeamFlagType, int> dic = DictionaryComponent<TeamFlagType, int>.Create();
            List<long> playerList = self.GetPlayerList();
            foreach (long playerId in playerList)
            {
                TeamFlagType homeTeamFlagType = self.GetGamePlayTowerDefense().GetHomeTeamFlagTypeByPlayer(playerId);
                if (dic.ContainsKey(homeTeamFlagType) == false)
                {
                    dic[homeTeamFlagType] = 0;
                }
                dic[homeTeamFlagType]++;
            }
            return dic;
        }

        public static List<long> GetPlayerList(this GameGoldComponent self)
        {
            return self.GetGamePlayTowerDefense().GetPlayerList();
        }

        public static GamePlayPlayerListComponent GetGamePlayerListComponent(this GameGoldComponent self)
        {
            return self.GetGamePlay().GetComponent<GamePlayPlayerListComponent>();
        }
    }
}
