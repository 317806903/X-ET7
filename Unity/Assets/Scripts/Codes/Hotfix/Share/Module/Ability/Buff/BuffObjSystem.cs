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
                self.monitorTriggerList = new();
            }
        }

        [ObjectSystem]
        public class BuffObjDestroySystem: DestroySystem<BuffObj>
        {
            protected override void Destroy(BuffObj self)
            {
                self.monitorTriggerList?.Clear();
                self.monitorTriggerList = null;
                self.buffActions = null;
                if (self.selfEffectList != null)
                {
                    self.selfEffectList.Clear();
                    self.selfEffectList = null;
                }
            }
        }

        public static void Init(this BuffObj self, Unit casterUnit, Unit unit, AddBuffInfo addBuffInfo)
        {
            self.CfgId = addBuffInfo.BuffId;
            for (int i = 0; i < self.model.MonitorTriggers.Count; i++)
            {
                AbilityConfig.BuffTriggerEvent abilityBuffMonitorTriggerEvent = self.model.MonitorTriggers[i].BuffTrig;
                self.monitorTriggerList.Add(abilityBuffMonitorTriggerEvent, self.model.MonitorTriggers[i]);
            }

            self.permanent = addBuffInfo.Duration == -1? true : false;
            self.duration = addBuffInfo.Duration == -1? 100 : addBuffInfo.Duration;
            self.orgDuration = self.duration;
            self.stack = 0;
            self.buffActions = addBuffInfo.BuffActions;

            self.casterUnitId = casterUnit.Id;
            Unit casterPlayerUnit = self.GetCasterActorUnit();
            self.casterUnitId = casterPlayerUnit.Id;

            self.timeElapsed = 0;
            self.ticked = 0;
            self.AddStackCount(addBuffInfo.AddStack, false);
        }

        public static void InitActionContext(this BuffObj self, ref ActionContext actionContext)
        {
            actionContext.buffUnitId = self.GetUnit().Id;
            actionContext.buffCfgId = self.CfgId;
            actionContext.buffId = self.Id;
            self.actionContext = actionContext;
        }

        public static void AddStackCount(this BuffObj self, ValueOperation op, int chgStack, bool needPublic = true)
        {
            int oldStackCount = self.stack;
            if (op == ValueOperation.Add)
            {
                self.stack = math.clamp(self.stack + chgStack, 0, self.model.MaxStack);
            }
            else if (op == ValueOperation.Reduce)
            {
                self.stack = math.clamp(self.stack - chgStack, 0, self.model.MaxStack);
            }
            else if (op == ValueOperation.Set)
            {
                self.stack = math.clamp(chgStack, 0, self.model.MaxStack);
            }
            self.DealSpecWhenChgBuffStackCount(oldStackCount, self.stack);

            //Log.Debug($"---AddStackCount {self.CfgId} addStack={addStack} self.stack={self.stack} self.duration={self.duration}");
            if (needPublic && op == ValueOperation.Add)
            {
                self.TrigEvent(AbilityConfig.BuffTriggerEvent.BuffOnRefresh);
            }
            //Log.Debug($"---AddStackCount 22 {self.CfgId} self.stack={self.stack} self.duration={self.duration}");
        }

        public static void AddStackCount(this BuffObj self, int addStackCount, bool needPublic = true)
        {
            int oldStackCount = self.stack;
            self.stack = math.clamp(self.stack + addStackCount, 0, self.model.MaxStack);
            if (needPublic)
            {
                self.DealSpecWhenChgBuffStackCount(oldStackCount, self.stack);
            }

            //Log.Debug($"---AddStackCount {self.CfgId} addStack={addStack} self.stack={self.stack} self.duration={self.duration}");
            if (needPublic && addStackCount > 0)
            {
                self.TrigEvent(AbilityConfig.BuffTriggerEvent.BuffOnRefresh);
            }
            //Log.Debug($"---AddStackCount 22 {self.CfgId} self.stack={self.stack} self.duration={self.duration}");
        }

        public static void ChgDuration(this BuffObj self, float duration)
        {
            self.timeElapsed = 0;
            self.duration = duration;
            //Log.Debug($"---ChgDuration {self.CfgId} self.stack={self.stack} self.duration={self.duration}");
        }

        public static void ChgTotalDuration(this BuffObj self, float newTotalDuration)
        {
            self.duration = newTotalDuration - self.timeElapsed;
        }

        public static void SetEnabled(this BuffObj self, bool isEnabled)
        {
            bool oldIsEnabled = self.isEnabled;
            if (oldIsEnabled == isEnabled)
            {
                return;
            }
            if (isEnabled == false)
            {
                self.isEnabled = false;
                self.DealSelfEffectWhenSetEnable(false);
                self.DealSpecWhenSetEnable(false);
            }
            else
            {
                self.isEnabled = true;
                self.DealSelfEffectWhenSetEnable(true);
                self.DealSpecWhenSetEnable(true);
            }
        }

        /// <summary>
        /// 拥有者
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Unit GetUnit(this BuffObj self)
        {
            return self.GetParent<BuffComponent>().GetParent<Unit>();
        }

        /// <summary>
        /// 获取buff发射者(player或monster)
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Unit GetCasterActorUnit(this BuffObj self)
        {
            Unit unit = UnitHelper.GetUnit(self.DomainScene(), self.casterUnitId);
            return unit.GetCasterActor();
        }

        public static List<BuffActionCall> GetActionIds(this BuffObj self, AbilityConfig.BuffTriggerEvent abilityBuffMonitorTriggerEvent)
        {
            return self.monitorTriggerList[abilityBuffMonitorTriggerEvent];
        }

        public static void FixedUpdate(this BuffObj self, float fixedDeltaTime)
        {
            float timePassed = fixedDeltaTime;
            if (self.permanent == false) self.duration -= timePassed;
            float lastTimeElapsed = self.timeElapsedReal;
            self.timeElapsedReal += timePassed;
            self.timeElapsed += timePassed;

            self.ChkBuffTick(lastTimeElapsed);
        }

        public static void ChkBuffTick(this BuffObj self, float lastTimeElapsed)
        {
            int tickCount = math.min(self.model.TickTime.Count, 3);
            if (tickCount > 0)
            {
                bool bContinue = true;
                if (self.model.BuffType == BuffType.Debuff)
                {
                    bContinue = true;
                }
                else
                {
                    bool bRet = BuffHelper.ChkCanBuffTick(self.GetUnit());
                    if (bRet)
                    {
                        bContinue = true;
                    }
                    else
                    {
                        bContinue = false;
                    }
                }
                if (bContinue)
                {
                    for (int i = 0; i < tickCount; i++)
                    {
                        if (self.model.TickTime[i] > 0)
                        {
                            int lastCount = (int)(lastTimeElapsed / self.model.TickTime[i]);
                            int newCount = (int)(self.timeElapsedReal / self.model.TickTime[i]);
                            while (newCount > lastCount)
                            {
                                lastCount++;
                                if (i == 0)
                                {
                                    self.TrigEvent(AbilityConfig.BuffTriggerEvent.BuffOnTick1);
                                }
                                else if (i == 1)
                                {
                                    self.TrigEvent(AbilityConfig.BuffTriggerEvent.BuffOnTick2);
                                }
                                else if (i == 2)
                                {
                                    self.TrigEvent(AbilityConfig.BuffTriggerEvent.BuffOnTick3);
                                }

                                self.ticked += 1;
                            }
                        }
                    }
                }
            }
        }

        public static void TrigEvent(this BuffObj self, AbilityConfig.BuffTriggerEvent abilityBuffMonitorTriggerEvent, Unit onAttackUnit = null, Unit beHurtUnit = null)
        {
            if (self.isEnabled == false)
            {
                return;
            }
            List<BuffActionCall> buffActionCalls = self.GetActionIds(abilityBuffMonitorTriggerEvent);
            if (buffActionCalls.Count > 0)
            {
                bool bContinue = true;
                if (self.model.BuffType == BuffType.Debuff)
                {
                    bContinue = true;
                }
                else
                {
                    bool bRet = BuffHelper.ChkCanBuffTrig(self.GetUnit());
                    if (bRet)
                    {
                        bContinue = true;
                    }
                    else
                    {
                        bContinue = false;
                    }
                }
                if (bContinue)
                {
                    for (int i = 0; i < buffActionCalls.Count; i++)
                    {
                        self.EventHandler(buffActionCalls[i], onAttackUnit, beHurtUnit);
                    }
                }
            }
        }

        public static void EventHandler(this BuffObj self, BuffActionCall buffActionCall, Unit onAttackUnit, Unit beHurtUnit)
        {
            Unit casterActorUnit = self.GetCasterActorUnit();
            if (casterActorUnit == null)
            {
                self.ChgDuration(0);
                return;
            }


            (SelectHandle selectHandle, Unit resetPosByUnit) = ET.Ability.SelectHandleHelper.DealSelectHandler(self.GetUnit(), buffActionCall.ActionCallParam_Ref, onAttackUnit, beHurtUnit, ref self.actionContext);
            if (resetPosByUnit == null && self.GetUnit() != casterActorUnit)
            {
                resetPosByUnit = self.GetUnit();
            }
            ET.Ability.ActionHandlerHelper.DoActionTriggerHandler(self.GetUnit(), casterActorUnit, buffActionCall.DelayTime, buffActionCall.ActionId, buffActionCall.ActionCondition1, buffActionCall.ActionCondition2, selectHandle, resetPosByUnit, ref self.actionContext);
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