using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (GlobalBuffGameObj))]
    public static class GlobalBuffGameObjSystem
    {
        [ObjectSystem]
        public class GlobalBuffObjAwakeSystem: AwakeSystem<GlobalBuffGameObj>
        {
            protected override void Awake(GlobalBuffGameObj self)
            {
            }
        }

        [ObjectSystem]
        public class GlobalBuffObjDestroySystem: DestroySystem<GlobalBuffGameObj>
        {
            protected override void Destroy(GlobalBuffGameObj self)
            {
                self.monitorTriggerList?.Clear();
            }
        }

        public static void Init(this GlobalBuffGameObj self, GameGlobalBuffCfg gameGlobalBuffCfg)
        {
            self.monitorTriggerList = new();
            self.CfgId = gameGlobalBuffCfg.Id;
            for (int i = 0; i < self.model.MonitorTriggers.Count; i++)
            {
                AbilityGameMonitorTriggerEvent abilityGameMonitorTriggerEvent = EnumHelper.FromString<AbilityGameMonitorTriggerEvent>(self.model.MonitorTriggers[i].GlobalBuffTrig.ToString());
                self.monitorTriggerList.Add(abilityGameMonitorTriggerEvent, self.model.MonitorTriggers[i]);
            }

            self.permanent = true;
            self.duration = 100;
            self.orgDuration = self.duration;
            self.buffActions = gameGlobalBuffCfg.MonitorTriggers;

            self.timeElapsed = 0;
            self.ticked = 0;
        }

        public static void InitActionContext(this GlobalBuffGameObj self, ref ActionGameContext actionGameContext)
        {
            actionGameContext.buffCfgId = self.CfgId;
            actionGameContext.buffId = self.Id;
            self.actionGameContext = actionGameContext;
        }

        public static void ChgDuration(this GlobalBuffGameObj self, float duration)
        {
            self.timeElapsed = 0;
            self.duration = duration;
        }

        public static List<GlobalBuffActionCall> GetActionIds(this GlobalBuffGameObj self, AbilityGameMonitorTriggerEvent abilityGameMonitorTriggerEvent)
        {
            return self.monitorTriggerList[abilityGameMonitorTriggerEvent];
        }

        public static void FixedUpdate(this GlobalBuffGameObj self, float fixedDeltaTime)
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

        public static void TrigEvent(this GlobalBuffGameObj self, AbilityGameMonitorTriggerEvent abilityGameMonitorTriggerEvent, Unit onAttackUnit = null, Unit beHurtUnit = null)
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

        public static void EventHandler(this GlobalBuffGameObj self, GlobalBuffActionCall buffActionCall, Unit onAttackUnit, Unit beHurtUnit)
        {
            string actionId = buffActionCall.ActionId;
            ActionGameHandlerHelper.CreateAction(self.DomainScene(), actionId, ref self.actionGameContext);
        }

        public static bool ChkNeedRemove(this GlobalBuffGameObj self)
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
