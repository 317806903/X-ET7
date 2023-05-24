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

        public static void EventHandler(this BulletObj self, AbilityBulletMonitorTriggerEvent abilityBulletMonitorTriggerEvent, Unit onHitUnit, Unit beHurtUnit)
        {
            List<BulletActionCall> actionIds = self.GetActionIds(abilityBulletMonitorTriggerEvent);
            for (int i = 0; i < actionIds.Count; i++)
            {
                BulletActionCall bulletActionCall = actionIds[i];
                string actionId = bulletActionCall.ActionId;
                SelectHandle selectHandle;
                if (bulletActionCall.ActionCallParam is ActionCallAutoUnit actionCallAutoUnit)
                {
                    selectHandle = SelectHandleHelper.GetSelectHandle(self.GetUnit(), actionCallAutoUnit);
                }
                else if (bulletActionCall.ActionCallParam is ActionCallAutoSelf actionCallAutoSelf)
                {
                    selectHandle = SelectHandleHelper.GetSelectHandle(self.GetUnit(), actionCallAutoSelf);
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
                    else if (bulletActionCall.ActionCallParam is ActionCallOnHitUnit actionCallOnHitUnit)
                    {
                        targetUnit = onHitUnit;
                    }
                    else if (bulletActionCall.ActionCallParam is ActionCallBeHurtUnit actionCallBeHurtUnit)
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