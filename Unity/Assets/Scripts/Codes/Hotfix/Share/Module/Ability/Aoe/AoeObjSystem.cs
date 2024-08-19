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
                self.aoeInUnitList = new ();
                self.aoeChgUnitList = new ();
                self.monitorTriggerList = new();
            }
        }

        [ObjectSystem]
        public class AoeObjDestroySystem: DestroySystem<AoeObj>
        {
            protected override void Destroy(AoeObj self)
            {
                self.aoeInUnitList.Clear();
                self.aoeChgUnitList.Clear();
                self.monitorTriggerList?.Clear();
                self.monitorTriggerList = null;
                self.aoeTargetCondition = null;
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
            self.CfgId = aoeCfgId;
            for (int i = 0; i < self.model.MonitorTriggers.Count; i++)
            {
                AbilityConfig.AoeTriggerEvent abilityAoeMonitorTriggerEvent = self.model.MonitorTriggers[i].AoeTrig;
                self.monitorTriggerList.Add(abilityAoeMonitorTriggerEvent, self.model.MonitorTriggers[i]);
            }
            self.permanent = duration == -1? true : false;
            self.duration = duration == -1? 100 : duration;
            self.timeElapsed = 0;
            self.aoeTargetCondition = aoeTargetCondition;
        }

        public static void InitActionContext(this AoeObj self, ref ActionContext actionContext)
        {
            actionContext.aoeId = self.Id;
            self.actionContext = actionContext;
        }

        public static List<AoeActionCall> GetActionIds(this AoeObj self, AbilityConfig.AoeTriggerEvent abilityAoeMonitorTriggerEvent)
        {
            return self.monitorTriggerList[abilityAoeMonitorTriggerEvent];
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

        public static void TrigEvent(this AoeObj self, AbilityConfig.AoeTriggerEvent abilityAoeMonitorTriggerEvent)
        {
            List<AoeActionCall> aoeActionCalls = self.GetActionIds(abilityAoeMonitorTriggerEvent);
            if (aoeActionCalls.Count > 0)
            {
                for (int i = 0; i < aoeActionCalls.Count; i++)
                {
                    self.EventHandler(aoeActionCalls[i]);
                }
            }
        }

        public static void EventHandler(this AoeObj self, AoeActionCall aoeActionCall)
        {
            SelectHandle selectHandle = ET.Ability.SelectHandleHelper.CreateSelectHandleWhenAoe(self.GetUnit(), aoeActionCall.AoeSelectObjectType);
            if (selectHandle == null)
            {
                return;
            }
            ET.Ability.ActionHandlerHelper.DoActionTriggerHandler(self.GetUnit(), self.GetUnit(), aoeActionCall.DelayTime, aoeActionCall.ActionId, aoeActionCall.ActionCondition1, aoeActionCall.ActionCondition2, selectHandle, null, ref self.actionContext);
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
                            self.TrigEvent(AbilityConfig.AoeTriggerEvent.AoeOnTick1);
                        }
                        else if (i == 1)
                        {
                            self.TrigEvent(AbilityConfig.AoeTriggerEvent.AoeOnTick2);
                        }
                        else if (i == 2)
                        {
                            self.TrigEvent(AbilityConfig.AoeTriggerEvent.AoeOnTick3);
                        }

                        self.ticked += 1;
                    }
                }
            }

            if (++self.curFrameChk >= self.waitFrameChk)
            {
                self.curFrameChk = 0;
                self.ChkAoeEnterOrExist();
            }

        }

        public static void ChkAoeEnterOrExist(this AoeObj self)
        {
            Unit curUnit = self.GetUnit();
            var seeUnits = curUnit.GetComponent<AOIEntity>().GetSeeUnits();
            using HashSetComponent<long> list = HashSetComponent<long>.Create();
            foreach (var seeUnit in seeUnits)
            {
                AOIEntity aoiEntityTmp = seeUnit.Value;
                if (aoiEntityTmp == null)
                {
                    continue;
                }
                Unit unit = aoiEntityTmp.Unit;
                if (unit == null)
                {
                    continue;
                }

                if (self.aoeTargetCondition.Radius > 0)
                {
                    float curDisSqr = UnitHelper.GetTargetUnitDisSqr(curUnit, unit);
                    if (curDisSqr > self.aoeTargetCondition.Radius * self.aoeTargetCondition.Radius)
                    {
                        continue;
                    }
                }
                list.Add(unit.Id);
            }

            SelectHandle curSelectHandle = ET.Ability.SelectHandleHelper.GetSelectHandleWithSelectObjectType(curUnit, self.aoeTargetCondition.SelectObjectUnitTypeBase, list);
            (bool bRet1, bool isChgSelect1, SelectHandle newSelectHandle1) = UnitConditionHandleHelper.ChkCondition(self.GetUnit(), curSelectHandle, self.aoeTargetCondition.ActionCondition1, ref self.actionContext);
            if (isChgSelect1)
            {
                curSelectHandle = newSelectHandle1;
            }

            if (bRet1)
            {
                (bool bRet2, bool isChgSelect2, SelectHandle newSelectHandle2) = UnitConditionHandleHelper.ChkCondition(self.GetUnit(), curSelectHandle, self.aoeTargetCondition.ActionCondition2, ref self.actionContext);
                if (isChgSelect2)
                {
                    curSelectHandle = newSelectHandle2;
                }
                if (bRet1 && bRet2)
                {
                    self.ChkChgList(curSelectHandle.unitIds);
                }
            }
        }

        public static void ChkChgList(this AoeObj self, List<long> newInList)
        {
            HashSetComponent<long> newEnterList = HashSetComponent<long>.Create();
            HashSetComponent<long> newExistList = HashSetComponent<long>.Create();
            for (int i = 0; i < newInList.Count; i++)
            {
                long unitId = newInList[i];
                if (self.aoeInUnitList.Contains(unitId))
                {
                    self.aoeInUnitList.Remove(unitId);
                }
                else
                {
                    newEnterList.Add(unitId);
                }
            }

            foreach (long unitId in self.aoeInUnitList)
            {
                newExistList.Add(unitId);
            }

            self.aoeInUnitList.Clear();
            for (int i = 0; i < newInList.Count; i++)
            {
                long unitId = newInList[i];
                self.aoeInUnitList.Add(unitId);
            }

            if (newEnterList.Count > 0)
            {
                self.aoeChgUnitList = newEnterList;
                self.TrigEvent(AbilityConfig.AoeTriggerEvent.AoeOnEnter);
            }

            if (newExistList.Count > 0)
            {
                self.aoeChgUnitList = newExistList;
                self.TrigEvent(AbilityConfig.AoeTriggerEvent.AoeOnExist);
            }
        }
    }
}