using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

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

        public static void Init(this BuffObj self, Unit casterUnit, Unit unit, AddBuffInfo addBuffInfo)
        {
            self.monitorTriggerList = new();
            self.CfgId = addBuffInfo.BuffId;
            for (int i = 0; i < self.model.MonitorTriggers.Count; i++)
            {
                AbilityBuffMonitorTriggerEvent abilityBuffMonitorTriggerEvent = (AbilityBuffMonitorTriggerEvent)Enum.Parse(typeof(AbilityBuffMonitorTriggerEvent), self.model.MonitorTriggers[i].BuffTrig.ToString());
                self.monitorTriggerList.Add(abilityBuffMonitorTriggerEvent, self.model.MonitorTriggers[i]);
            }

            self.permanent = addBuffInfo.Duration == -1? true : false;
            self.duration = addBuffInfo.Duration == -1? 1 : addBuffInfo.Duration;
            self.orgDuration = self.duration;
            self.stack = 0;
            self.AddStackCount(addBuffInfo.AddStack, false);
            self.casterUnitId = casterUnit.Id;
            self.carrierUnitId = unit.Id;
            self.timeElapsed = 0;
            self.ticked = 0;
            self.buffAction = addBuffInfo.BuffAction;
        }
        
        public static void InitActionContext(this BuffObj self, ActionContext actionContext)
        {
            actionContext.buffUnitId = self.carrierUnitId;
            actionContext.buffCfgId = self.CfgId;
            actionContext.buffId = self.Id;
            self.actionContext = actionContext;
        }

        public static void AddStackCount(this BuffObj self, int addStack, bool needPublic = true)
        {
            int oldStackCount = self.stack;
            self.stack = math.clamp(self.stack + addStack, 0, self.model.MaxStack);
            self.ChgBuffStackCountWhenModifyAttribute(oldStackCount, self.stack);
            
            //Log.Debug($"---AddStackCount {self.CfgId} addStack={addStack} self.stack={self.stack} self.duration={self.duration}");
            if (needPublic && addStack > 0)
            {
                self.TrigEvent(AbilityBuffMonitorTriggerEvent.BuffOnRefresh);
            }
            //Log.Debug($"---AddStackCount 22 {self.CfgId} self.stack={self.stack} self.duration={self.duration}");
        }

        public static void ChgDuration(this BuffObj self, float duration)
        {
            self.duration = duration;
            Log.Debug($"---ChgDuration {self.CfgId} self.stack={self.stack} self.duration={self.duration}");
        }

        public static void SetEnabled(this BuffObj self, bool isEnabled)
        {
            bool oldIsEnabled = self.isEnabled;
            if (oldIsEnabled == isEnabled)
            {
                return;
            }
            if (oldIsEnabled && isEnabled == false)
            {
                self.RemoveBuffWhenModifyAttribute();
                self.isEnabled = false;
            }
            else
            {
                self.isEnabled = true;
                self.AddBuffWhenModifyAttribute();
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

            int tickCount = math.min(self.model.TickTime.Count, 3);
            for (int i = 0; i < tickCount; i++)
            {
                if (self.model.TickTime[i] > 0)
                {
                    //float取模不精准，所以用x1000后的整数来
                    if (Math.Round(self.timeElapsed * 1000) % Math.Round(self.model.TickTime[i] * 1000) == 0)
                    {
                        if (i == 0)
                        {
                            self.TrigEvent(AbilityBuffMonitorTriggerEvent.BuffOnTick1);
                        }
                        else if (i == 1)
                        {
                            self.TrigEvent(AbilityBuffMonitorTriggerEvent.BuffOnTick2);
                        }
                        else if (i == 2)
                        {
                            self.TrigEvent(AbilityBuffMonitorTriggerEvent.BuffOnTick3);
                        }

                        self.ticked += 1;
                    }
                }
            }
        }

        public static void TrigEvent(this BuffObj self, AbilityBuffMonitorTriggerEvent abilityBuffMonitorTriggerEvent, Unit onAttackUnit = null, Unit 
        beHurtUnit = null)
        {
            if (self.isEnabled == false)
            {
                return;
            }
            List<BuffActionCall> buffActionCalls = self.GetActionIds(abilityBuffMonitorTriggerEvent);
            if (buffActionCalls.Count > 0)
            {
                for (int i = 0; i < buffActionCalls.Count; i++)
                {
                    self.EventHandler(buffActionCalls[i], onAttackUnit, beHurtUnit);
                }
            }
        }
        
        public static void EventHandler(this BuffObj self, BuffActionCall buffActionCall, Unit onAttackUnit, Unit beHurtUnit)
        {
            if (onAttackUnit != null)
            {
                self.actionContext.attackerUnitId = onAttackUnit.Id;
            }
            string actionId = buffActionCall.ActionId;
            SelectHandle selectHandle;
            if (buffActionCall.ActionCallParam is ActionCallSelectLast)
            {
                selectHandle = UnitHelper.GetSaveSelectHandle(self.GetUnit());
            }
            else if (buffActionCall.ActionCallParam is ActionCallAutoUnit actionCallAutoUnit)
            {
                selectHandle = SelectHandleHelper.CreateSelectHandle(self.GetUnit(), actionCallAutoUnit);
            }
            else if (buffActionCall.ActionCallParam is ActionCallAutoSelf actionCallAutoSelf)
            {
                selectHandle = SelectHandleHelper.CreateSelectHandle(self.GetUnit(), actionCallAutoSelf);
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
                selectHandle = SelectHandleHelper.CreateUnitSelectHandle(self.GetUnit(), targetUnit, buffActionCall.ActionCallParam);
            }

            SelectHandle curSelectHandle = selectHandle;
            (bool bRet1, bool isChgSelect1, SelectHandle newSelectHandle1) = ConditionHandleHelper.ChkCondition(self.GetUnit(), curSelectHandle, buffActionCall.ActionCondition1, self.actionContext);
            if (isChgSelect1)
            {
                curSelectHandle = newSelectHandle1;
            }
            (bool bRet2, bool isChgSelect2, SelectHandle newSelectHandle2) = ConditionHandleHelper.ChkCondition(self.GetUnit(), curSelectHandle, buffActionCall.ActionCondition2, self.actionContext);
            if (isChgSelect2)
            {
                curSelectHandle = newSelectHandle2;
            }
            if (bRet1 && bRet2)
            {
                ActionHandlerHelper.CreateAction(self.GetUnit(), actionId, buffActionCall.DelayTime, curSelectHandle, self.actionContext);
            }
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