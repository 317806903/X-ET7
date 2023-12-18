using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (GlobalBuffPlayerObj))]
    public static class GlobalBuffPlayerObjSystem
    {
        [ObjectSystem]
        public class GlobalBuffObjAwakeSystem: AwakeSystem<GlobalBuffPlayerObj>
        {
            protected override void Awake(GlobalBuffPlayerObj self)
            {
            }
        }

        [ObjectSystem]
        public class GlobalBuffObjDestroySystem: DestroySystem<GlobalBuffPlayerObj>
        {
            protected override void Destroy(GlobalBuffPlayerObj self)
            {
                self.monitorTriggerList?.Clear();
            }
        }

        public static void Init(this GlobalBuffPlayerObj self, long playerId, PlayerGlobalBuffCfg playerGlobalBuffCfg)
        {
            self.monitorTriggerList = new();
            self.CfgId = playerGlobalBuffCfg.Id;
            for (int i = 0; i < self.model.MonitorTriggers.Count; i++)
            {
                AbilityGameMonitorTriggerEvent abilityGameMonitorTriggerEvent = EnumHelper.FromString<AbilityGameMonitorTriggerEvent>(self.model.MonitorTriggers[i].GlobalBuffTrig.ToString());
                self.monitorTriggerList.Add(abilityGameMonitorTriggerEvent, self.model.MonitorTriggers[i]);
            }

            self.permanent = true;
            self.duration = 100;
            self.orgDuration = self.duration;
            self.buffActions = playerGlobalBuffCfg.MonitorTriggers;

            self.timeElapsed = 0;
            self.ticked = 0;
        }

        public static void InitActionContext(this GlobalBuffPlayerObj self, ref ActionPlayerContext actionPlayerContext)
        {
            actionPlayerContext.buffCfgId = self.CfgId;
            actionPlayerContext.buffId = self.Id;
            self.actionPlayerContext = actionPlayerContext;
        }

        public static void ChgDuration(this GlobalBuffPlayerObj self, float duration)
        {
            self.timeElapsed = 0;
            self.duration = duration;
        }

        public static List<GlobalBuffActionCall> GetActionIds(this GlobalBuffPlayerObj self, AbilityGameMonitorTriggerEvent abilityGameMonitorTriggerEvent)
        {
            return self.monitorTriggerList[abilityGameMonitorTriggerEvent];
        }

        public static void FixedUpdate(this GlobalBuffPlayerObj self, float fixedDeltaTime)
        {
            float timePassed = fixedDeltaTime;
            if (self.permanent == false) self.duration -= timePassed;
            float lastTimeElapsed = self.timeElapsed;
            self.timeElapsed += timePassed;

            int tickCount = math.min(self.model.TickTime.Count, 3);
            for (int i = 0; i < tickCount; i++)
            {
                if (self.model.TickTime[i] > 0)
                {
                    int lastCount = (int)(lastTimeElapsed / self.model.TickTime[i]);
                    int newCount = (int)(self.timeElapsed / self.model.TickTime[i]);
                    while (newCount > lastCount)
                    {
                        lastCount++;
                        if (i == 0)
                        {
                            self.TrigEvent(AbilityGameMonitorTriggerEvent.GlobalBuffOnTick1);
                        }
                        else if (i == 1)
                        {
                            self.TrigEvent(AbilityGameMonitorTriggerEvent.GlobalBuffOnTick2);
                        }
                        else if (i == 2)
                        {
                            self.TrigEvent(AbilityGameMonitorTriggerEvent.GlobalBuffOnTick3);
                        }

                        self.ticked += 1;
                    }
                }
            }
        }

        public static void TrigEvent(this GlobalBuffPlayerObj self, AbilityGameMonitorTriggerEvent abilityGameMonitorTriggerEvent, Unit onAttackUnit = null, Unit beHurtUnit = null)
        {
            List<GlobalBuffActionCall> buffActionCalls = self.GetActionIds(abilityGameMonitorTriggerEvent);
            if (buffActionCalls.Count > 0)
            {
                for (int i = 0; i < buffActionCalls.Count; i++)
                {
                    self.EventHandler(buffActionCalls[i], onAttackUnit, beHurtUnit);
                }
            }
        }

        public static void EventHandler(this GlobalBuffPlayerObj self, GlobalBuffActionCall buffActionCall, Unit onAttackUnit, Unit beHurtUnit)
        {
            string actionId = buffActionCall.ActionId;
            ActionPlayerHandlerHelper.CreateAction(self.DomainScene(), self.playerId, actionId, ref self.actionPlayerContext);
        }

        public static bool ChkNeedRemove(this GlobalBuffPlayerObj self)
        {
            //只要duration <= 0，不管是否是permanent都移除掉
            if (self.duration <= 0)
            {
                return true;
            }

            return false;
        }

    }
}
