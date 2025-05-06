using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof (GamePlayStatisticalDataManagerComponent))]
    public static class GamePlayStatisticalDataManagerComponentSystem
    {
        [ObjectSystem]
        public class GamePlayStatisticalDataManagerComponentAwakeSystem: AwakeSystem<GamePlayStatisticalDataManagerComponent>
        {
            protected override void Awake(GamePlayStatisticalDataManagerComponent self)
            {
            }
        }

        [ObjectSystem]
        public class GamePlayStatisticalDataManagerComponentDestroySystem: DestroySystem<GamePlayStatisticalDataManagerComponent>
        {
            protected override void Destroy(GamePlayStatisticalDataManagerComponent self)
            {
            }
        }

        public static void Init(this GamePlayStatisticalDataManagerComponent self)
        {
        }

        public static void AddKillInfo(this GamePlayStatisticalDataManagerComponent self, Unit attackerUnit, Unit beKillUnit)
        {
            long attackerPlayerId = GamePlayHelper.GetPlayerIdByUnitId(attackerUnit);
            if (attackerPlayerId == (long)ET.PlayerId.PlayerNone)
            {
                return;
            }

            long beKillUnitPlayerId = GamePlayHelper.GetPlayerIdByUnitId(beKillUnit);
            if (beKillUnitPlayerId != (long)ET.PlayerId.PlayerNone)
            {
                return;
            }

            GamePlayStatisticalDataComponent gamePlayStatisticalDataComponent = self.GetGamePlayStatisticalData(attackerPlayerId);
            gamePlayStatisticalDataComponent.AddKillInfo(attackerUnit, beKillUnit);
        }

        public static void AddMonsterEscapeInfo(this GamePlayStatisticalDataManagerComponent self, Unit monsterUnit, int attackValue)
        {
            MonsterComponent monsterComponent = monsterUnit.GetComponent<MonsterComponent>();
            if (monsterComponent == null)
            {
                return;
            }

            if (monsterComponent.playerId == (long)ET.PlayerId.PlayerNone)
            {
                return;
            }

            GamePlayStatisticalDataComponent gamePlayStatisticalDataComponent = self.GetGamePlayStatisticalData(monsterComponent.playerId);
            gamePlayStatisticalDataComponent.AddMonsterEscapeInfo(attackValue);
        }

        public static int GetPlayerKillNum(this GamePlayStatisticalDataManagerComponent self, long playerId)
        {
            if (self == null)
            {
                return 0;
            }
            GamePlayStatisticalDataComponent gamePlayStatisticalDataComponent = self.GetGamePlayStatisticalData(playerId);
            if (gamePlayStatisticalDataComponent == null)
            {
                return 0;
            }
            return gamePlayStatisticalDataComponent.GetPlayerKillNum();
        }

        public static int GetPlayerMonsterEscapeAttackValue(this GamePlayStatisticalDataManagerComponent self, long playerId)
        {
            if (self == null)
            {
                return 0;
            }
            GamePlayStatisticalDataComponent gamePlayStatisticalDataComponent = self.GetGamePlayStatisticalData(playerId);
            if (gamePlayStatisticalDataComponent == null)
            {
                return 0;
            }
            return gamePlayStatisticalDataComponent.GetPlayerMonsterEscapeAttackValue();
        }

        public static void NoticeToClient(this GamePlayStatisticalDataManagerComponent self, long playerId)
        {
            GamePlayStatisticalDataComponent gamePlayStatisticalDataComponent = self.GetGamePlayStatisticalData(playerId);
            gamePlayStatisticalDataComponent.NoticeToClient(false);
        }

        public static void NoticeToClientAll(this GamePlayStatisticalDataManagerComponent self)
        {
            List<long> playerList = self.GetGamePlay().GetPlayerList();
            foreach (long playerId in playerList)
            {
                self.NoticeToClient(playerId);
            }
        }

        public static GamePlayStatisticalDataComponent GetGamePlayStatisticalData(this GamePlayStatisticalDataManagerComponent self, long playerId)
        {
            GamePlayStatisticalDataComponent gamePlayStatisticalDataComponent = self.GetChild<GamePlayStatisticalDataComponent>(playerId);
            if (gamePlayStatisticalDataComponent == null)
            {
                gamePlayStatisticalDataComponent = self.AddChildWithId<GamePlayStatisticalDataComponent>(playerId);
                gamePlayStatisticalDataComponent.Init(playerId);
            }
            return gamePlayStatisticalDataComponent;
        }

        public static GamePlayComponent GetGamePlay(this GamePlayStatisticalDataManagerComponent self)
        {
            GamePlayComponent gamePlayComponent = self.GetParent<GamePlayComponent>();
            return gamePlayComponent;
        }

        public static GamePlayBattleLevelCfg GetGamePlayBattleConfig(this GamePlayStatisticalDataManagerComponent self)
        {
            GamePlayComponent gamePlayComponent = self.GetGamePlay();
            return gamePlayComponent.GetGamePlayBattleConfig();
        }
    }
}