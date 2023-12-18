using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (GlobalBuffUnitObj))]
    public static class GlobalBuffUnitObjSystem
    {
        [ObjectSystem]
        public class GlobalBuffObjAwakeSystem: AwakeSystem<GlobalBuffUnitObj>
        {
            protected override void Awake(GlobalBuffUnitObj self)
            {
            }
        }

        [ObjectSystem]
        public class GlobalBuffObjDestroySystem: DestroySystem<GlobalBuffUnitObj>
        {
            protected override void Destroy(GlobalBuffUnitObj self)
            {
                self.monitorTriggerList?.Clear();
            }
        }

        public static void Init(this GlobalBuffUnitObj self, Unit unit, UnitGlobalBuffCfg unitGlobalBuffCfg)
        {
            self.monitorTriggerList = new();
            self.CfgId = unitGlobalBuffCfg.Id;
            for (int i = 0; i < self.model.MonitorTriggers.Count; i++)
            {
                AbilityGameMonitorTriggerEvent abilityGameMonitorTriggerEvent = EnumHelper.FromString<AbilityGameMonitorTriggerEvent>(self.model.MonitorTriggers[i].GlobalBuffTrig.ToString());
                self.monitorTriggerList.Add(abilityGameMonitorTriggerEvent, self.model.MonitorTriggers[i]);
            }

            self.unitId = unit.Id;

            self.permanent = true;
            self.duration = 100;
            self.orgDuration = self.duration;
            self.buffActions = unitGlobalBuffCfg.MonitorTriggers;

            self.timeElapsed = 0;
            self.ticked = 0;
        }

        public static void InitActionContext(this GlobalBuffUnitObj self, ref ActionContext actionContext)
        {
            actionContext.buffUnitId = self.GetUnit().Id;
            actionContext.buffCfgId = self.CfgId;
            actionContext.buffId = self.Id;
            self.actionContext = actionContext;
        }

        public static void ChgDuration(this GlobalBuffUnitObj self, float duration)
        {
            self.timeElapsed = 0;
            self.duration = duration;
        }

        /// <summary>
        /// 拥有者
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Unit GetUnit(this GlobalBuffUnitObj self)
        {
            return UnitHelper.GetUnit(self.DomainScene(), self.unitId);
        }

        public static List<GlobalBuffActionCall> GetActionIds(this GlobalBuffUnitObj self, AbilityGameMonitorTriggerEvent abilityGameMonitorTriggerEvent)
        {
            return self.monitorTriggerList[abilityGameMonitorTriggerEvent];
        }

        public static void FixedUpdate(this GlobalBuffUnitObj self, float fixedDeltaTime)
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

        public static void TrigEvent(this GlobalBuffUnitObj self, AbilityGameMonitorTriggerEvent abilityGameMonitorTriggerEvent, Unit onAttackUnit = null, Unit beHurtUnit = null)
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

        public static void EventHandler(this GlobalBuffUnitObj self, GlobalBuffActionCall buffActionCall, Unit onAttackUnit, Unit beHurtUnit)
        {
            string actionId = buffActionCall.ActionId;
            SelectHandle curSelectHandle = SelectHandleHelper.CreateUnitSelfSelectHandle(self.GetUnit());
            ActionHandlerHelper.CreateAction(self.GetUnit(), null, actionId, buffActionCall.DelayTime, curSelectHandle, ref self.actionContext);
        }

        public static bool ChkNeedRemove(this GlobalBuffUnitObj self)
        {
            //只要duration <= 0，不管是否是permanent都移除掉
            if (self.duration <= 0)
            {
                return true;
            }

            Unit unit = self.GetUnit();
            if (UnitHelper.ChkUnitAlive(unit) == false)
            {
                return true;
            }

            return false;
        }

    }
}