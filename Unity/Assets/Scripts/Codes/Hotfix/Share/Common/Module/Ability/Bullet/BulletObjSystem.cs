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
                self.monitorTriggerList = new();
            }
        }

        [ObjectSystem]
        public class BulletObjDestroySystem: DestroySystem<BulletObj>
        {
            protected override void Destroy(BulletObj self)
            {
                self.monitorTriggerList?.Clear();
                self.monitorTriggerList = null;

                if (self.hitRecords != null)
                {
                    foreach (BulletHitRecord bulletHitRecord in self.hitRecords.Values)
                    {
                        bulletHitRecord.Dispose();
                    }
                    self.hitRecords.Dispose();
                    self.hitRecords = null;
                }

                if (self.removeList != null)
                {
                    self.removeList.Dispose();
                    self.removeList = null;
                }

                self.preHitUnitIds = null;
            }
        }

        [ObjectSystem]
        public class BulletObjFixedUpdateSystem: FixedUpdateSystem<BulletObj>
        {
            protected override void FixedUpdate(BulletObj self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static void Init(this BulletObj self, long casterUnitId, string bulletCfgId, float duration)
        {
            self.CfgId = bulletCfgId;
            for (int i = 0; i < self.model.MonitorTriggers.Count; i++)
            {
                AbilityConfig.BulletTriggerEvent abilityBulletMonitorTriggerEvent = self.model.MonitorTriggers[i].BulletTrig;
                self.monitorTriggerList.Add(abilityBulletMonitorTriggerEvent, self.model.MonitorTriggers[i]);
            }

            self.duration = duration;
            self.timeElapsed = 0;
            self.canHitAfterCreated = self.model.CanHitAfterCreated;
            self.canHitTimes = self.model.HitTimes;
            self.hitRecords = DictionaryComponent<long, BulletHitRecord>.Create();
            self.removeList = ListComponent<long>.Create();
        }

        public static void InitActionContext(this BulletObj self, ref ActionContext actionContext)
        {
            actionContext.isBreakSoftBati = false;
            actionContext.isBreakStrongBati = false;
            self.actionContext = actionContext;
        }

        public static List<BulletActionCall> GetActionIds(this BulletObj self, AbilityConfig.BulletTriggerEvent abilityBulletMonitorTriggerEvent)
        {
            return self.monitorTriggerList[abilityBulletMonitorTriggerEvent];
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

        public static void TrigEvent(this BulletObj self, AbilityConfig.BulletTriggerEvent abilityBulletMonitorTriggerEvent, Unit onAttackUnit, Unit
            beHurtUnit, ref ActionContext actionContext)
        {
            List<BulletActionCall> bulletActionCalls = self.GetActionIds(abilityBulletMonitorTriggerEvent);
            if (bulletActionCalls.Count > 0)
            {
                for (int i = 0; i < bulletActionCalls.Count; i++)
                {
                    self.EventHandler(bulletActionCalls[i], onAttackUnit, beHurtUnit, ref actionContext);
                }
            }
        }

        public static void EventHandler(this BulletObj self, BulletActionCall bulletActionCall, Unit onAttackUnit, Unit beHurtUnit, ref ActionContext actionContext)
        {
            if (beHurtUnit != null)
            {
                actionContext.defenderUnitId = beHurtUnit.Id;
            }

            bool bRetChk = ET.Ability.ActionHandlerHelper.ChkActionCondition(self.GetUnit(), bulletActionCall.ChkCondition1, bulletActionCall.ChkCondition2, bulletActionCall.ChkCondition1SelectObj_Ref, bulletActionCall.ChkCondition2SelectObj_Ref, ref actionContext);
            if (bRetChk == false)
            {
                return;
            }

            (SelectHandle selectHandle, Unit resetPosByUnit) = ET.Ability.SelectHandleHelper.DealSelectHandler(self.GetUnit(), bulletActionCall.ActionCallParam_Ref, onAttackUnit, beHurtUnit, ref actionContext);
            if (selectHandle == null)
            {
                return;
            }
            ET.Ability.ActionHandlerHelper.DoActionTriggerHandler(self.GetUnit(), self.GetUnit(), bulletActionCall.DelayTime, bulletActionCall.ActionIds, bulletActionCall.FilterCondition1, bulletActionCall.FilterCondition2, selectHandle, resetPosByUnit, ref actionContext);
        }

        public static void FixedUpdate(this BulletObj self, float fixedDeltaTime)
        {
            float timePassed = fixedDeltaTime;
            self.duration -= timePassed;
            self.timeElapsed += timePassed;

            if (self.canHitAfterCreated > 0)
            {
                self.canHitAfterCreated -= timePassed;
            }

            if (self.duration <= 0 || self.canHitTimes <= 0)
            {
                self.GetUnit().DestroyWithDeathShow(false);
                return;
            }

            if (self.hitRecords != null && self.hitRecords.Count > 0)
            {
                self.removeList.Clear();
                foreach (var item in self.hitRecords)
                {
                    BulletHitRecord bulletHitRecord = item.Value;

                    bulletHitRecord.timeToCanHit -= timePassed;
                    if (bulletHitRecord.timeToCanHit <= 0)
                    {
                        self.removeList.Add(item.Key);
                    }
                }

                foreach (long unitId in self.removeList)
                {
                    self.hitRecords.Remove(unitId);
                }
                self.removeList.Clear();
            }

        }

        public static bool ChkCanTrigHit(this BulletObj self)
        {
            if (self.canHitTimes <= 0)
                return false;
            if (self.canHitAfterCreated > 0)
                return false;
            if (self.GetUnit().ChkIsInDeath())
            {
                return false;
            }

            MoveTweenObj moveTweenObj = self.GetUnit().GetComponent<MoveTweenObj>();
            if (moveTweenObj != null && moveTweenObj.isNeedChkHoldTime)
            {
                return false;
            }

            return true;
        }

        public static bool CanHitUnit(this BulletObj self, Unit unit)
        {
            if (self.model.CanHitUnit == false)
            {
                return false;
            }
            if (self.ChkCanTrigHit() == false)
                return false;

            if (self.hitRecords != null)
            {
                if (self.hitRecords.ContainsKey(unit.Id))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool CanHitMesh(this BulletObj self, bool isOnlyChkMesh)
        {
            if (self.ChkCanTrigHit() == false)
                return false;

            if (isOnlyChkMesh)
            {
                if (self.model.CanHitMesh == false)
                {
                    return false;
                }
            }
            else
            {
                if (UnitHelper.IsNeedChkMesh(self.GetUnit()) == false)
                {
                    return false;
                }
            }

            if (self.hitRecords != null)
            {
                if (self.hitRecords.ContainsKey(-1))
                {
                    return false;
                }
            }

            return true;
        }

        public static void AddPreHitUnit(this BulletObj self, long unitId)
        {
            if (self.preHitUnitIds == null)
            {
                self.preHitUnitIds = new();
            }

            self.preHitUnitIds.Add(unitId);
        }

        public static void ResetPreHitUnit(this BulletObj self)
        {
            self.preHitUnitIds?.Clear();
        }

        public static HashSet<long> GetPreHitUnit(this BulletObj self)
        {
            return self.preHitUnitIds;
        }

        public static void SetPreHitPos(this BulletObj self)
        {
            self.preHitPos = true;
        }

        public static bool ChkPreHitPos(this BulletObj self)
        {
            return self.preHitPos;
        }
    }
}