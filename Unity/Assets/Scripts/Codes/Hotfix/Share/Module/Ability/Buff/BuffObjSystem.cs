using System;
using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
    [FriendOf(typeof (BuffObj))]
    public static class BuffObjSystem
    {
        [ObjectSystem]
        public class BuffObjAwakeSystem: AwakeSystem<BuffObj>
        {
            protected override void Awake(BuffObj self)
            {
            }
        }

        [ObjectSystem]
        public class BuffObjDestroySystem: DestroySystem<BuffObj>
        {
            protected override void Destroy(BuffObj self)
            {
            }
        }

        public static void Init(this BuffObj self, string buffCfgId)
        {
            self.monitorTriggerList = new();
            self.CfgId = buffCfgId;
            for (int i = 0; i < self.model.MonitorTriggers.Count; i++)
            {
                AbilityBuffMonitorTriggerEvent abilityBuffMonitorTriggerEvent = (AbilityBuffMonitorTriggerEvent)Enum.Parse(typeof(AbilityBuffMonitorTriggerEvent), self.model.MonitorTriggers[i].BuffTrig.ToString());
                self.monitorTriggerList.Add(abilityBuffMonitorTriggerEvent, self.model.MonitorTriggers[i]);
            }
        }

        /// <summary>
        /// 拥有者
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Unit GetUnit(this BuffObj self)
        {
            return UnitHelper.GetUnit(self.DomainScene(), self.carrierUnitId);
        }

        /// <summary>
        /// 施法者
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Unit GetCasterUnit(this BuffObj self)
        {
            return UnitHelper.GetUnit(self.DomainScene(), self.casterUnitId);
        }

        /// <summary>
        /// 获取buff发射者(player或monster)
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Unit GetCasterPlayerUnit(this BuffObj self)
        {
            Unit unit = UnitHelper.GetUnit(self.DomainScene(), self.casterUnitId);
            while(true)
            {
                if (UnitHelper.ChkIsBullet(unit))
                {
                    unit = unit.GetComponent<BulletObj>().GetCasterPlayerUnit();
                }
                else if (UnitHelper.ChkIsAoe(unit))
                {
                    unit = unit.GetComponent<AoeObj>().GetCasterPlayerUnit();
                }
                else
                {
                    break;
                }
            }
            return unit;
        }

        public static List<BuffActionCall> GetActionIds(this BuffObj self, AbilityBuffMonitorTriggerEvent abilityBuffMonitorTriggerEvent)
        {
            return self.monitorTriggerList[abilityBuffMonitorTriggerEvent];
        }

        public static void FixedUpdate(this BuffObj self, float fixedDeltaTime)
        {
            float timePassed = fixedDeltaTime;
            if (self.permanent == false) self.duration -= timePassed;
            self.timeElapsed += timePassed;

            if (self.model.TickTime > 0)
            {
                //float取模不精准，所以用x1000后的整数来
                if (Math.Round(self.timeElapsed * 1000) % Math.Round(self.model.TickTime * 1000) == 0)
                {
                    List<BuffActionCall> buffActionCalls = self.GetActionIds(AbilityBuffMonitorTriggerEvent.BuffOnTick);
                    if (buffActionCalls.Count > 0)
                    {
                        for (int i = 0; i < buffActionCalls.Count; i++)
                        {
                            self.EventHandler(buffActionCalls[i], null, null);
                        }
                    }
                    self.ticked += 1;
                }
            }
        }

        public static void EventHandler(this BuffObj self, BuffActionCall buffActionCall, Unit onAttackUnit, Unit beHurtUnit)
        {
            string actionId = buffActionCall.ActionId;
            SelectHandle selectHandle;
            if (buffActionCall.ActionCallParam is ActionCallAutoUnit actionCallAutoUnit)
            {
                selectHandle = SelectHandleHelper.GetSelectHandle(self.GetUnit(), actionCallAutoUnit);
            }
            else if (buffActionCall.ActionCallParam is ActionCallAutoSelf actionCallAutoSelf)
            {
                selectHandle = SelectHandleHelper.GetSelectHandle(self.GetUnit(), actionCallAutoSelf);
            }
            else
            {
                Unit targetUnit;
                if (buffActionCall.ActionCallParam is ActionCallCasterUnit actionCallCasterUnit)
                {
                    targetUnit = self.GetCasterUnit();
                }
                else if (buffActionCall.ActionCallParam is ActionCallCasterPlayerUnit actionCallCasterPlayerUnit)
                {
                    targetUnit = self.GetCasterPlayerUnit();
                }
                else if (buffActionCall.ActionCallParam is ActionCallOnAttackUnit actionCallOnAttackUnit)
                {
                    targetUnit = onAttackUnit;
                }
                else if (buffActionCall.ActionCallParam is ActionCallBeHurtUnit actionCallBeHurtUnit)
                {
                    targetUnit = beHurtUnit;
                }
                else
                {
                    targetUnit = self.GetUnit();
                }
                selectHandle = SelectHandleHelper.GetSelectHandle(self.GetUnit(), targetUnit);
            }
            ActionHandlerHelper.CreateAction(self.GetUnit(), actionId, selectHandle);
        }

        public static bool ChkNeedRemove(this BuffObj self)
        {
            //只要duration <= 0，不管是否是permanent都移除掉
            if (self.duration <= 0 || self.stack <= 0)
            {
                return true;
            }

            return false;
        }
    }
}