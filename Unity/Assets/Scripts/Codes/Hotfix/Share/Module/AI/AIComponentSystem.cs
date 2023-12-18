using System;
using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof(AIComponent))]
    [FriendOf(typeof(AIDispatcherComponent))]
    public static class AIComponentSystem
    {
        [Invoke(TimerInvokeType.AITimer)]
        public class AITimer: ATimer<AIComponent>
        {
            protected override void Run(AIComponent self)
            {
                try
                {
                    if (self.isEnable == false)
                    {
                        return;
                    }
                    self.Check(false);
                }
                catch (Exception e)
                {
                    Log.Error($"move timer error: {self.Id}\n{e}");
                }
            }
        }

        [ObjectSystem]
        public class AIComponentAwakeSystem: AwakeSystem<AIComponent, string>
        {
            protected override void Awake(AIComponent self, string aiConfigId)
            {
                self.isEnable = true;
                self.AICfgId = aiConfigId;
                self.curFrameIndex = new();

                var oneAI = AICfgCategory.Instance.GetAI(self.AICfgId);
                foreach (AICfg aiConfig in oneAI.Values)
                {
                    self.curFrameIndex.Add(aiConfig.Order, 0);
                }


                self.isNear = true;
                self._ResetRepeatedTimer();

                self.FirstCheck().Coroutine();
            }
        }

        [ObjectSystem]
        public class AIComponentDestroySystem: DestroySystem<AIComponent>
        {
            protected override void Destroy(AIComponent self)
            {
                TimerComponent.Instance?.Remove(ref self.Timer);
                self.CancellationToken?.Cancel();
                self.CancellationToken = null;
                self.Current = "";
            }
        }

        public static void ResetRepeatedTimerByDis(this AIComponent self, Unit chkDisUnit)
        {
            if (chkDisUnit == null)
            {
                return;
            }
            if (self.lastChkDisTime == 0 || self.lastChkDisTime < TimeHelper.ServerNow() - self.chkDisTimeInterval * 1000)
            {
                self.lastChkDisTime = TimeHelper.ServerNow();
            }
            else
            {
                return;
            }
            float curDisSq = math.lengthsq(self.GetUnit().Position - chkDisUnit.Position);
            if (curDisSq > self.nearDis * self.nearDis)
            {
                self.isNear = false;
            }
            else
            {
                self.isNear = true;
            }

            self._ResetRepeatedTimer();
        }

        public static void ResetRepeatedTimerByAttack(this AIComponent self)
        {
            self.isNear = true;
            self._ResetRepeatedTimer();
        }

        public static void _ResetRepeatedTimer(this AIComponent self)
        {
            int newRepeatedTimer;
            if (self.isNear)
            {
                newRepeatedTimer = self.NewRepeatedTimerNear;
            }
            else
            {
                newRepeatedTimer = self.NewRepeatedTimerFast;
            }
            if (self.curNewRepeatedTimer != newRepeatedTimer)
            {
                TimerComponent.Instance?.Remove(ref self.Timer);
                self.Timer = TimerComponent.Instance.NewRepeatedTimer(newRepeatedTimer, TimerInvokeType.AITimer, self);
                self.curNewRepeatedTimer = newRepeatedTimer;
            }
        }

        public static async ETTask FirstCheck(this AIComponent self)
        {
            int retryCount = 10;
            while (true)
            {
                if (self == null || self.IsDisposed)
                {
                    return;
                }

                if (retryCount <= 0)
                {
                    return;
                }
                var unit = self.GetUnit();
                if (unit == null || unit.IsDisposed)
                {
                    return;
                }
                var AOIEntity = unit.GetComponent<AOIEntity>();
                if (AOIEntity == null || AOIEntity.IsDisposed)
                {
                    await TimerComponent.Instance.WaitFrameAsync();
                    retryCount--;
                    continue;
                }
                var seeUnits = AOIEntity.GetSeeUnits();
                if (seeUnits.Count == 0)
                {
                    await TimerComponent.Instance.WaitFrameAsync();
                    retryCount--;
                    continue;
                }
                else
                {
                    break;
                }
            }

            int random = RandomGenerator.RandomNumber(0, 5);
            for (int i = 0; i < random; i++)
            {
                await TimerComponent.Instance.WaitFrameAsync();
            }
            self.Check(true);
        }

        public static Unit GetUnit(this AIComponent self)
        {
            return self.GetParent<Unit>();
        }

        public static string GetAICfgId(this AIComponent self)
        {
            return self.AICfgId;
        }

        public static void PauseAI(this AIComponent self)
        {
            self.Cancel();
            self.isEnable = false;
        }

        public static void RecoveryAI(this AIComponent self)
        {
            self.isEnable = true;
        }

        public static void Check(this AIComponent self, bool isFirst)
        {
            if (self.Parent == null)
            {
                self.Cancel();
                TimerComponent.Instance.Remove(ref self.Timer);
                return;
            }

            if (UnitHelper.ChkUnitAlive(self.GetUnit()) == false)
            {
                self.Cancel();
                TimerComponent.Instance.Remove(ref self.Timer);
                return;
            }

            var oneAI = AICfgCategory.Instance.GetAI(self.AICfgId);

            foreach (AICfg aiConfig in oneAI.Values)
            {
                if (isFirst == false && aiConfig.WaitFrameNum > 0)
                {
                    int waitFrameNum = aiConfig.WaitFrameNum;
                    if (self.curFrameIndex[aiConfig.Order] < waitFrameNum)
                    {
                        self.curFrameIndex[aiConfig.Order]++;
                        continue;
                    }
                    else
                    {
                        self.curFrameIndex[aiConfig.Order] = 0;
                    }
                }

                AIDispatcherComponent.Instance.AIHandlers.TryGetValue(aiConfig.Name, out AAIHandler aaiHandler);

                if (aaiHandler == null)
                {
                    Log.Error($"not found aihandler: {aiConfig.Name}");
                    continue;
                }

                int ret = aaiHandler.Check(self, aiConfig, isFirst);
                if (ret != 0)
                {
                    continue;
                }

                if (self.Current == aiConfig.Order.ToString())
                {
                    break;
                }

                //Log.Debug($"==新 aiConfig.Id {aiConfig.Id} self.Current={self.Current}");
                self.Cancel(); // 取消之前的行为
                ETCancellationToken cancellationToken = new ETCancellationToken();
                self.CancellationToken = cancellationToken;
                self.Current = aiConfig.Order.ToString();

                aaiHandler.Execute(self, aiConfig, cancellationToken).Coroutine();
                return;
            }

        }

        public static void Cancel(this AIComponent self)
        {
            self.CancellationToken?.Cancel();
            self.Current = "";
            self.CancellationToken = null;
        }
    }
}