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
            if (attackerPlayerId == -1)
            {
                return;
            }

            long beKillUnitPlayerId = GamePlayHelper.GetPlayerIdByUnitId(beKillUnit);
            if (beKillUnitPlayerId != -1)
            {
                return;
            }

            GamePlayStatisticalDataComponent gamePlayStatisticalDataComponent = self.GetGamePlayStatisticalData(attackerPlayerId);
            gamePlayStatisticalDataComponent.AddKillInfo(attackerUnit, beKillUnit);
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