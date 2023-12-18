using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (AoeObj))]
    public static class AoeObjSystem
    {
        [ObjectSystem]
        public class AoeObjAwakeSystem: AwakeSystem<AoeObj>
        {
            protected override void Awake(AoeObj self)
            {
            }
        }

        [ObjectSystem]
        public class AoeObjDestroySystem: DestroySystem<AoeObj>
        {
            protected override void Destroy(AoeObj self)
            {
            }
        }

        [ObjectSystem]
        public class AoeObjFixedUpdateSystem: FixedUpdateSystem<AoeObj>
        {
            protected override void FixedUpdate(AoeObj self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static void Init(this AoeObj self, long casterUnitId, string aoeCfgId, float duration, AoeTargetCondition aoeTargetCondition)
        {
            self.permanent = duration == -1? true : false;
            self.duration = duration == -1? 100 : duration;
            self.casterUnitId = casterUnitId;
            self.CfgId = aoeCfgId;
            self.duration = duration;
            self.timeElapsed = 0;
            self.aoeTargetCondition = aoeTargetCondition;
        }

        public static void InitActionContext(this AoeObj self, ref ActionContext actionContext)
        {
            actionContext.aoeId = self.Id;
            self.actionContext = actionContext;
        }

        public static List<AoeActionCall> GetActionIds(this AoeObj self, AbilityAoeMonitorTriggerEvent abilityAoeMonitorTriggerEvent)
        {
            ListComponent<AoeActionCall> actionList = ListComponent<AoeActionCall>.Create();
            for (int i = 0; i < self.model.MonitorTriggers.Count; i++)
            {
                if (self.model.MonitorTriggers[i].AoeTrig.ToString() == abilityAoeMonitorTriggerEvent.ToString())
                {
                    actionList.Add(self.model.MonitorTriggers[i]);
                }
            }
            return actionList;
        }


        /// <summary>
        /// 获取aoe发射者(上级)
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Unit GetCasterUnit(this AoeObj self)
        {
            return UnitHelper.GetUnit(self.DomainScene(), self.casterUnitId);
        }

        /// <summary>
        /// 获取aoe发射者(player或monster)
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Unit GetCasterActorUnit(this AoeObj self)
        {
            return UnitHelper.GetCasterActorUnit(self.DomainScene(), self.casterUnitId);
        }

        /// <summary>
        /// 获取子弹unit
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Unit GetUnit(this AoeObj self)
        {
            return self.GetParent<Unit>();
        }

        public static void TrigEvent(this AoeObj self, AbilityAoeMonitorTriggerEvent abilityAoeMonitorTriggerEvent, Unit onAttackUnit = null, Unit
            beHurtUnit = null)
        {
            List<AoeActionCall> aoeActionCalls = self.GetActionIds(abilityAoeMonitorTriggerEvent);
            if (aoeActionCalls.Count > 0)
            {
                for (int i = 0; i < aoeActionCalls.Count; i++)
                {
                    self.EventHandler(aoeActionCalls[i], onAttackUnit, beHurtUnit);
                }
            }
        }

        public static void EventHandler(this AoeObj self, AoeActionCall aoeActionCall, Unit onAttackUnit, Unit beHurtUnit)
        {
            if (onAttackUnit != null)
            {
                self.actionContext.attackerUnitId = onAttackUnit.Id;
            }

            string actionId = aoeActionCall.ActionId;
            SelectHandle selectHandle;
            Unit resetPosByUnit = null;
            if (aoeActionCall.ActionCallParam is ActionCallSelectLast)
            {
                selectHandle = UnitHelper.GetSaveSelectHandle(self.GetUnit());
            }
            else if (aoeActionCall.ActionCallParam is ActionCallAutoUnit actionCallAutoUnit)
            {
                selectHandle = SelectHandleHelper.CreateSelectHandle(self.GetUnit(), beHurtUnit, actionCallAutoUnit, ref self.actionContext);
            }
            else if (aoeActionCall.ActionCallParam is ActionCallAutoSelf actionCallAutoSelf)
            {
                selectHandle = SelectHandleHelper.CreateSelectHandle(self.GetUnit(), null, actionCallAutoSelf, ref self.actionContext);
            }
            else if (aoeActionCall.ActionCallParam is ActionCallOnAoeChgUnit actionCallOnAoeChgUnit)
            {
                selectHandle = SelectHandleHelper.CreateUnitNoneSelectHandle();
                foreach (long unitId in self.chgUnitList)
                {
                    selectHandle.unitIds.Add(unitId);
                }
            }
            else if (aoeActionCall.ActionCallParam is ActionCallOnAoeInUnit actionCallOnAoeInUnit)
            {
                selectHandle = SelectHandleHelper.CreateUnitNoneSelectHandle();
                foreach (long unitId in self.unitIds)
                {
                    selectHandle.unitIds.Add(unitId);
                }
            }
            else
            {
                Unit targetUnit;
                if (aoeActionCall.ActionCallParam is ActionCallCasterUnit actionCallCasterUnit)
                {
                    targetUnit = self.GetCasterUnit();
                }
                else if (aoeActionCall.ActionCallParam is ActionCallCasterPlayerUnit actionCallCasterPlayerUnit)
                {
                    targetUnit = self.GetCasterActorUnit();
                }
                else if (aoeActionCall.ActionCallParam is ActionCallOnAttackUnit actionCallOnAttackUnit)
                {
                    targetUnit = onAttackUnit;
                }
                else if (aoeActionCall.ActionCallParam is ActionCallBeHurtUnit actionCallBeHurtUnit)
                {
                    targetUnit = beHurtUnit;
                }
                else
                {
                    targetUnit = self.GetUnit();
                }
                resetPosByUnit = targetUnit;
                selectHandle = SelectHandleHelper.CreateUnitSelectHandle(self.GetUnit(), targetUnit, aoeActionCall.ActionCallParam);
            }

            if (selectHandle == null)
            {
                return;
            }

            SelectHandle curSelectHandle = selectHandle;
            (bool bRet1, bool isChgSelect1, SelectHandle newSelectHandle1) = ConditionHandleHelper.ChkCondition(self.GetUnit(), curSelectHandle, aoeActionCall.ActionCondition1, ref self.actionContext);
            if (isChgSelect1)
            {
                curSelectHandle = newSelectHandle1;
            }
            (bool bRet2, bool isChgSelect2, SelectHandle newSelectHandle2) = ConditionHandleHelper.ChkCondition(self.GetUnit(), curSelectHandle, aoeActionCall.ActionCondition2, ref self.actionContext);
            if (isChgSelect2)
            {
                curSelectHandle = newSelectHandle2;
            }
            if (bRet1 && bRet2)
            {
                ActionHandlerHelper.CreateAction(self.GetUnit(), resetPosByUnit,  actionId, aoeActionCall.DelayTime, curSelectHandle, ref self.actionContext);
            }
        }

        public static void FixedUpdate(this AoeObj self, float fixedDeltaTime)
        {
            float timePassed = fixedDeltaTime;
            if (self.permanent == false) self.duration -= timePassed;
            float lastTimeElapsed = self.timeElapsed;
            self.timeElapsed += timePassed;
            if (self.duration <= 0)
            {
                self.GetUnit().DestroyWithDeathShow();
                return;
            }

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
                            self.TrigEvent(AbilityAoeMonitorTriggerEvent.AoeOnTick1);
                        }
                        else if (i == 1)
                        {
                            self.TrigEvent(AbilityAoeMonitorTriggerEvent.AoeOnTick2);
                        }
                        else if (i == 2)
                        {
                            self.TrigEvent(AbilityAoeMonitorTriggerEvent.AoeOnTick3);
                        }

                        self.ticked += 1;
                    }
                }
            }

            self.ChkAoeEnterOrExist();
        }

        public static void ChkAoeEnterOrExist(this AoeObj self)
        {
            Unit curUnit = self.GetUnit();
            SelectHandle curSelectHandle = SelectHandleHelper.CreateUnitNoneSelectHandle();
            var seeUnits = curUnit.GetComponent<AOIEntity>().GetSeeUnits();
            foreach (var seeUnit in seeUnits)
            {
                AOIEntity aoiEntityTmp = seeUnit.Value;
                Unit unit = aoiEntityTmp.Unit;
                if (unit == null)
                {
                    continue;
                }
                curSelectHandle.unitIds.Add(unit.Id);
            }
            (bool bRet1, bool isChgSelect1, SelectHandle newSelectHandle1) = ConditionHandleHelper.ChkCondition(self.GetUnit(), curSelectHandle, self.aoeTargetCondition.ActionCondition1, ref self.actionContext);
            if (isChgSelect1)
            {
                curSelectHandle = newSelectHandle1;
            }
            (bool bRet2, bool isChgSelect2, SelectHandle newSelectHandle2) = ConditionHandleHelper.ChkCondition(self.GetUnit(), curSelectHandle, self.aoeTargetCondition.ActionCondition2, ref self.actionContext);
            if (isChgSelect2)
            {
                curSelectHandle = newSelectHandle2;
            }
            if (bRet1 && bRet2)
            {
                self.ChkChgList(curSelectHandle.unitIds);
            }
        }

        public static void ChkChgList(this AoeObj self, List<long> newInList)
        {
            ListComponent<long> newEnterList = ListComponent<long>.Create();
            ListComponent<long> newExistList = ListComponent<long>.Create();
            for (int i = 0; i < newInList.Count; i++)
            {
                long unitId = newInList[i];
                if (self.unitIds.Contains(unitId))
                {
                    self.unitIds.Remove(unitId);
                }
                else
                {
                    newEnterList.Add(unitId);
                }
            }

            foreach (long unitId in self.unitIds)
            {
                newExistList.Add(unitId);
            }

            self.unitIds.Clear();
            for (int i = 0; i < newInList.Count; i++)
            {
                long unitId = newInList[i];
                self.unitIds.Add(unitId);
            }

            self.chgUnitList = newEnterList;
            self.TrigEvent(AbilityAoeMonitorTriggerEvent.AoeOnEnter);
            self.chgUnitList = newExistList;
            self.TrigEvent(AbilityAoeMonitorTriggerEvent.AoeOnExist);
        }
    }
}