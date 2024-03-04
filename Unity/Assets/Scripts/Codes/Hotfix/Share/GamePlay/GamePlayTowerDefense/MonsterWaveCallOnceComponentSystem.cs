using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof (MonsterWaveCallOnceComponent))]
    public static class MonsterWaveCallOnceComponentSystem
    {
        [ObjectSystem]
        public class MonsterWaveCallOnceComponentAwakeSystem: AwakeSystem<MonsterWaveCallOnceComponent>
        {
            protected override void Awake(MonsterWaveCallOnceComponent self)
            {
                self.monsterWaveUnitList = new();
                self.monsterWaveCallIsFinished = new();
            }
        }

        [ObjectSystem]
        public class MonsterWaveCallOnceComponentDestroySystem: DestroySystem<MonsterWaveCallOnceComponent>
        {
            protected override void Destroy(MonsterWaveCallOnceComponent self)
            {
                self.ClearAllMonster();
                self.monsterWaveUnitList.Clear();
                self.monsterWaveCallIsFinished.Clear();
            }
        }

        [ObjectSystem]
        public class MonsterWaveCallOnceComponentFixedUpdateSystem: FixedUpdateSystem<MonsterWaveCallOnceComponent>
        {
            protected override void FixedUpdate(MonsterWaveCallOnceComponent self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static void Init(this MonsterWaveCallOnceComponent self, long playerId, string monsterWaveRule, int index,
        float monsterWaveNumScalePercent, float monsterWaveLevelScalePercent, float waveRewardGoldScalePercent)
        {
            self.playerId = playerId;
            self.monsterWaveRule = monsterWaveRule;
            self.waveIndex = index;
            self.monsterWaveNumScalePercent = monsterWaveNumScalePercent;
            self.monsterWaveLevelScalePercent = monsterWaveLevelScalePercent;
            self.waveRewardGoldScalePercent = waveRewardGoldScalePercent;
            TowerDefense_MonsterWaveCallRuleCfg monsterWaveCallCfg =
                TowerDefense_MonsterWaveCallRuleCfgCategory.Instance.Get(self.monsterWaveRule, self.waveIndex);
            self.duration = monsterWaveCallCfg.Duration;
            self.timeElapsed = 0;
        }

        public static void FixedUpdate(this MonsterWaveCallOnceComponent self, float fixedDeltaTime)
        {
            if (self.duration > 0)
            {
                self.duration -= fixedDeltaTime;
            }
            else
            {

            }

            float wasTimeElapsed = self.timeElapsed;
            self.timeElapsed += fixedDeltaTime;

            TowerDefense_MonsterWaveCallRuleCfg monsterWaveCallCfg =
                TowerDefense_MonsterWaveCallRuleCfgCategory.Instance.Get(self.monsterWaveRule, self.waveIndex);
            int count = monsterWaveCallCfg.Nodes.Count;
            //执行时间点内的事情
            for (int i = 0; i < count; i++)
            {
                MonsterWaveCallNode monsterWaveCallNode = monsterWaveCallCfg.Nodes[i];
                if (monsterWaveCallNode.TimeElapsed >= wasTimeElapsed)
                {
                    self.monsterWaveCallIsFinished[monsterWaveCallNode] = false;
                }

                if (
                    monsterWaveCallNode.TimeElapsed < self.timeElapsed &&
                    monsterWaveCallNode.TimeElapsed >= wasTimeElapsed
                )
                {
                    self.CallMonster(monsterWaveCallNode).Coroutine();
                }
            }
        }

        public static bool ChkIsGameEnd(this MonsterWaveCallOnceComponent self)
        {
            MonsterWaveCallComponent monsterWaveCallComponent = self.GetParent<MonsterWaveCallComponent>();
            return monsterWaveCallComponent.ChkIsGameEnd();
        }

        public static async ETTask CallMonster(this MonsterWaveCallOnceComponent self, MonsterWaveCallNode monsterWaveCallNode)
        {
            MonsterWaveCallComponent monsterWaveCallComponent = self.GetParent<MonsterWaveCallComponent>();
            int leftNum = (int)math.floor(monsterWaveCallNode.TotalNum * (100 + self.monsterWaveNumScalePercent) * 0.01f);
            int onceNum = (int)math.floor(monsterWaveCallNode.OnceCallNum * (100 + self.monsterWaveNumScalePercent) * 0.01f);
            int monsterLevel = (int)math.floor(monsterWaveCallNode.Level * (100 + self.monsterWaveLevelScalePercent) * 0.01f);
            int monsterRewardGold = (int)math.floor(monsterWaveCallNode.RewardGold * (100 + self.waveRewardGoldScalePercent) * 0.01f);
            List<string> createActionIds = monsterWaveCallNode.CreateActionIds;
            while (leftNum > 0)
            {
                if (self.IsDisposed)
                {
                    return;
                }

                for (int i = 0; i < onceNum; i++)
                {
                    Unit monsterUnit =
                        monsterWaveCallComponent.CallMonsterOnce(self.playerId, monsterWaveCallNode.MonsterCfgId, monsterLevel, monsterRewardGold);
                    self.monsterWaveUnitList.Add(monsterUnit.Id);

                    ET.GamePlayHelper.DoCreateActions(monsterUnit, createActionIds);

                    leftNum -= 1;
                    if (leftNum == 0)
                    {
                        break;
                    }
                }

                if (self.ChkIsGameEnd())
                {
                    return;
                }

                await TimerComponent.Instance.WaitAsync((long)(monsterWaveCallNode.OnceIntervalTime * 1000));
            }

            self.monsterWaveCallIsFinished[monsterWaveCallNode] = true;
        }

        public static void ClearAllMonster(this MonsterWaveCallOnceComponent self)
        {
            foreach (long monsterUnitId in self.monsterWaveUnitList)
            {
                Unit monsterUnit = UnitHelper.GetUnit(self.DomainScene(), monsterUnitId);
                if (UnitHelper.ChkUnitAlive(monsterUnit, true))
                {
                    monsterUnit.DestroyNotDeathShow();
                }
            }

            self.monsterWaveUnitList.Clear();
        }

        public static bool ChkMonsterCallFinish(this MonsterWaveCallOnceComponent self)
        {
            if (self.timeElapsed == 0)
            {
                return false;
            }

            foreach (var monsterWaveCall in self.monsterWaveCallIsFinished)
            {
                if (monsterWaveCall.Value == false)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool ChkMonsterCallAllClear(this MonsterWaveCallOnceComponent self)
        {
            if (self.ChkMonsterCallFinish() == false)
            {
                return false;
            }

            foreach (long unitId in self.monsterWaveUnitList)
            {
                if (UnitHelper.ChkUnitAlive(self.DomainScene(), unitId))
                {
                    return false;
                }
            }

            return true;
        }
    }
}