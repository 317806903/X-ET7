using System;
using System.Collections.Generic;
using System.Xml.Schema;
using ET.AbilityConfig;

namespace ET.Ability
{
    [FriendOf(typeof (BulletObj))]
    public static class BulletObjSystem
    {
        [ObjectSystem]
        public class BulletObjAwakeSystem: AwakeSystem<BulletObj>
        {
            protected override void Awake(BulletObj self)
            {
            }
        }

        [ObjectSystem]
        public class BulletObjDestroySystem: DestroySystem<BulletObj>
        {
            protected override void Destroy(BulletObj self)
            {
                self.hitRecords.Dispose();
            }
        }
        
        [ObjectSystem]
        public class BulletObjFixedUpdateSystem: FixedUpdateSystem<BulletObj>
        {
            protected override void FixedUpdate(BulletObj self)
            {
                if (self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static void Init(this BulletObj self, long casterUnitId, string bulletCfgId, float duration)
        {
            self.casterUnitId = casterUnitId;
            self.CfgId = bulletCfgId;
            self.duration = duration;
            self.timeElapsed = 0;
            self.canHitAfterCreated = self.model.CanHitAfterCreated;
            self.canHitTimes = self.model.HitTimes;
            self.hitRecords = ListComponent<BulletHitRecord>.Create();
        }

        public static void InitActionContext(this BulletObj self, ActionContext actionContext)
        {
            actionContext.isBreakSoftBati = false;
            actionContext.isBreakStrongBati = false;
            self.actionContext = actionContext;
        }

        public static List<BulletActionCall> GetActionIds(this BulletObj self, AbilityBulletMonitorTriggerEvent abilityBulletMonitorTriggerEvent)
        {
            ListComponent<BulletActionCall> actionList = ListComponent<BulletActionCall>.Create();
            for (int i = 0; i < self.model.MonitorTriggers.Count; i++)
            {
                if (self.model.MonitorTriggers[i].BulletTrig.ToString() == abilityBulletMonitorTriggerEvent.ToString())
                {
                    actionList.Add(self.model.MonitorTriggers[i]);
                }
            }
            return actionList;
        }

        /// <summary>
        /// 获取子弹发射者(上级)
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Unit GetCasterUnit(this BulletObj self)
        {
            return UnitHelper.GetUnit(self.DomainScene(), self.casterUnitId);
        }
        
        /// <summary>
        /// 获取子弹发射者(player或monster)
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Unit GetCasterPlayerUnit(this BulletObj self)
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

        /// <summary>
        /// 获取子弹unit
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Unit GetUnit(this BulletObj self)
        {
            return self.GetParent<Unit>();
        }

        public static void EventHandler(this BulletObj self, AbilityBulletMonitorTriggerEvent abilityBulletMonitorTriggerEvent, Unit onAttackUnit, Unit beHurtUnit)
        {
            if (onAttackUnit != null)
            {
                self.actionContext.attackerUnitId = onAttackUnit.Id;
            }
            List<BulletActionCall> actionIds = self.GetActionIds(abilityBulletMonitorTriggerEvent);
            for (int i = 0; i < actionIds.Count; i++)
            {
                BulletActionCall bulletActionCall = actionIds[i];
                string actionId = bulletActionCall.ActionId;
                SelectHandle selectHandle;
                if (bulletActionCall.ActionCallParam is ActionCallSelectLast)
                {
                    selectHandle = UnitHelper.GetSaveSelectHandle(self.GetUnit());
                }
                else if (bulletActionCall.ActionCallParam is ActionCallAutoUnit actionCallAutoUnit)
                {
                    selectHandle = SelectHandleHelper.CreateSelectHandle(self.GetUnit(), actionCallAutoUnit);
                }
                else if (bulletActionCall.ActionCallParam is ActionCallAutoSelf actionCallAutoSelf)
                {
                    selectHandle = SelectHandleHelper.CreateSelectHandle(self.GetUnit(), actionCallAutoSelf);
                }
                else
                {
                    Unit targetUnit;
                    if (bulletActionCall.ActionCallParam is ActionCallCasterUnit actionCallCasterUnit)
                    {
                        targetUnit = self.GetCasterUnit();
                    }
                    else if (bulletActionCall.ActionCallParam is ActionCallCasterPlayerUnit actionCallCasterPlayerUnit)
                    {
                        targetUnit = self.GetCasterPlayerUnit();
                    }
                    else if (bulletActionCall.ActionCallParam is ActionCallOnAttackUnit actionCallOnAttackUnit)
                    {
                        targetUnit = onAttackUnit;
                    }
                    else if (bulletActionCall.ActionCallParam is ActionCallBeHurtUnit actionCallBeHurtUnit)
                    {
                        targetUnit = beHurtUnit;
                    }
                    else
                    {
                        targetUnit = self.GetUnit();
                    }
                    selectHandle = SelectHandleHelper.CreateUnitSelectHandle(self.GetUnit(), targetUnit, bulletActionCall.ActionCallParam);
                }

                SelectHandle curSelectHandle = selectHandle;
                (bool bRet1, bool isChgSelect1, SelectHandle newSelectHandle1) = ConditionHandleHelper.ChkCondition(self.GetUnit(), curSelectHandle, bulletActionCall.ActionCondition1, self.actionContext);
                if (isChgSelect1)
                {
                    curSelectHandle = newSelectHandle1;
                }
                (bool bRet2, bool isChgSelect2, SelectHandle newSelectHandle2) = ConditionHandleHelper.ChkCondition(self.GetUnit(), curSelectHandle, bulletActionCall.ActionCondition2, self.actionContext);
                if (isChgSelect2)
                {
                    curSelectHandle = newSelectHandle2;
                }
                if (bRet1 && bRet2)
                {
                    ActionHandlerHelper.CreateAction(self.GetUnit(), actionId, bulletActionCall.DelayTime, curSelectHandle, self.actionContext);
                }
            }
        }

        public static void FixedUpdate(this BulletObj self, float fixedDeltaTime)
        {
            float timePassed = fixedDeltaTime;
            self.duration -= timePassed;
            //Log.Error(" self.duration:" + self.duration + " " + self.GetUnit().Id);
            self.timeElapsed += timePassed;
            if (self.hitRecords != null)
            {
                for (int i = self.hitRecords.Count - 1; i >= 0; i--)
                {
                    self.hitRecords[i].timeToCanHit -= timePassed;
                    if (self.hitRecords[i].timeToCanHit <= 0)
                    {
                        self.hitRecords.RemoveAt(i);
                    }
                }
            }

            if (self.canHitAfterCreated > 0)
            {
                self.canHitAfterCreated -= timePassed;
            }

            if (self.duration <= 0 || self.canHitTimes <= 0)
            {
                self.GetUnit().Destroy();
            }
        }

        public static bool CanHit(this BulletObj self, Unit unit)
        {
            if (self.canHitTimes <= 0)
                return false;
            if (self.canHitAfterCreated > 0)
                return false;
            if (self.hitRecords != null)
            {
                for (int i = 0; i < self.hitRecords.Count; i++)
                {
                    if (self.hitRecords[i].targetUnitId == unit.Id)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}