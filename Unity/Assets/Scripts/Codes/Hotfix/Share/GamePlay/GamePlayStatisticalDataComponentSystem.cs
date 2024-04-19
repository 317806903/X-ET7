using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof (GamePlayStatisticalDataComponent))]
    public static class GamePlayStatisticalDataComponentSystem
    {
        [ObjectSystem]
        public class GamePlayStatisticalDataComponentAwakeSystem: AwakeSystem<GamePlayStatisticalDataComponent>
        {
            protected override void Awake(GamePlayStatisticalDataComponent self)
            {
                self.towerUnitId2MonsterType2Num = new();
                self.towerCfgId2MonsterType2Num = new();
                self.towerType2MonsterType2Num = new();
                self.playerAllMonsterType2Num = new();
            }
        }

        [ObjectSystem]
        public class GamePlayStatisticalDataComponentDestroySystem: DestroySystem<GamePlayStatisticalDataComponent>
        {
            protected override void Destroy(GamePlayStatisticalDataComponent self)
            {
                self.towerUnitId2MonsterType2Num?.Clear();
                self.towerCfgId2MonsterType2Num?.Clear();
                self.towerType2MonsterType2Num?.Clear();
                self.playerAllMonsterType2Num?.Clear();
            }
        }

        public static void Init(this GamePlayStatisticalDataComponent self, long playerId)
        {
            self.playerId = playerId;
        }

        public static void AddKillInfo(this GamePlayStatisticalDataComponent self, Unit attackerUnit, Unit beKillUnit)
        {
            Unit attackerActorUnit = UnitHelper.GetCasterActorUnit(attackerUnit);
            if (attackerActorUnit == null)
            {
                return;
            }

            self.AddTowerKillMonsterInfo(attackerActorUnit, beKillUnit);
        }

        public static void AddTowerKillMonsterInfo(this GamePlayStatisticalDataComponent self, Unit attackerUnit, Unit beKillUnit)
        {
            TowerComponent towerComponent = attackerUnit.GetComponent<TowerComponent>();
            if (towerComponent == null)
            {
                return;
            }

            string towerCfgId = towerComponent.towerCfgId;
            PlayerTowerType towerType = towerComponent.model.Type;

            MonsterComponent monsterComponent = beKillUnit.GetComponent<MonsterComponent>();
            if (monsterComponent == null)
            {
                return;
            }

            string monsterCfgId = monsterComponent.monsterCfgId;
            MonsterType monsterTypeMenu = monsterComponent.model.Type;
            string monsterType = monsterTypeMenu.ToString();
            {
                if(self.towerUnitId2MonsterType2Num.TryGetValue(attackerUnit.Id, monsterType, out int num))
                {
                }
                num++;
                self.towerUnitId2MonsterType2Num.Add(attackerUnit.Id, monsterType, num);

            }

            {
                if(self.towerCfgId2MonsterType2Num.TryGetValue(towerCfgId, monsterType, out int num))
                {
                }
                num++;
                self.towerCfgId2MonsterType2Num.Add(towerCfgId, monsterType, num);

            }

            {
                if(self.towerType2MonsterType2Num.TryGetValue(towerType, monsterType, out int num))
                {
                }
                num++;
                self.towerType2MonsterType2Num.Add(towerType, monsterType, num);

            }

            {
                if(self.playerAllMonsterType2Num.TryGetValue(monsterType, out int num))
                {
                }
                num++;
                self.playerAllMonsterType2Num[monsterType] = num;

            }

            self.NoticeToClient(false);
        }

        public static int GetPlayerKillNum(this GamePlayStatisticalDataComponent self)
        {
            int totalKillNum = 0;
            foreach (var item in self.playerAllMonsterType2Num)
            {
                totalKillNum += item.Value;
            }
            return totalKillNum;
        }

        public static GamePlayComponent GetGamePlay(this GamePlayStatisticalDataComponent self)
        {
            GamePlayStatisticalDataManagerComponent gamePlayStatisticalDataManagerComponent = self.GetParent<GamePlayStatisticalDataManagerComponent>();
            return gamePlayStatisticalDataManagerComponent.GetGamePlay();
        }

        public static GamePlayBattleLevelCfg GetGamePlayBattleConfig(this GamePlayStatisticalDataComponent self)
        {
            GamePlayStatisticalDataManagerComponent gamePlayStatisticalDataManagerComponent = self.GetParent<GamePlayStatisticalDataManagerComponent>();
            return gamePlayStatisticalDataManagerComponent.GetGamePlayBattleConfig();
        }

        public static void NoticeToClient(this GamePlayStatisticalDataComponent self, bool isQuick)
        {
            if (isQuick == false)
            {
                EventType.WaitNoticeGamePlayStatisticalToClient _WaitNoticeGamePlayStatisticalToClient = new()
                {
                    playerId = self.playerId, gamePlayStatisticalDataComponent = self,
                };
                EventSystem.Instance.Publish(self.DomainScene(), _WaitNoticeGamePlayStatisticalToClient);
            }
            else
            {
                EventType.NoticeGamePlayStatisticalToClient _NoticeGamePlayStatisticalToClient = new()
                {
                    playerId = self.playerId, gamePlayStatisticalDataComponent = self,
                };
                EventSystem.Instance.Publish(self.DomainScene(), _NoticeGamePlayStatisticalToClient);
            }
        }
    }
}