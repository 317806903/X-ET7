using System.Collections.Generic;
using System.Numerics;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    [FriendOf(typeof(BulletObj))]
    [FriendOf(typeof(NumericComponent))]
    public static class BulletHelper
    {
        public static void CreateBulletAll(Unit unit, ActionCfg_FireBullet actionCfgFireBullet, SelectHandle selectHandle, ref ActionContext actionContext)
        {
            if (ET.Ability.SelectHandleHelper.ChkIsNullSelectHandle(selectHandle))
            {
                return;
            }
            if (selectHandle.selectHandleType == SelectHandleType.SelectUnits)
            {
                int count = selectHandle.unitIds.Count;
                if (count > 1)
                {
                    for (int i = 0; i < count; i++)
                    {
                        Unit targetUnit = UnitHelper.GetUnit(unit.DomainScene(), selectHandle.unitIds[i]);
                        SelectHandle selectHandleOne = SelectHandleHelper.CreateUnitSelectHandle(unit, targetUnit, null);

                        CreateBulletOne(unit, actionCfgFireBullet, selectHandleOne, ref actionContext);
                    }
                }
                else
                {
                    CreateBulletOne(unit, actionCfgFireBullet, selectHandle, ref actionContext);
                }
            }
            else
            {
                CreateBulletOne(unit, actionCfgFireBullet, selectHandle, ref actionContext);
            }
        }

        public static void CreateBulletOne(Unit unit, ActionCfg_FireBullet actionCfgFireBullet, SelectHandle selectHandle, ref ActionContext actionContext)
        {
            Unit bulletUnit = ET.GamePlayHelper.CreateBulletByUnit(unit.DomainScene(), unit, actionCfgFireBullet, selectHandle, actionContext);

            unit.AddOwnCaller(bulletUnit);
            bulletUnit.AddCaster(unit);

            foreach (var actionId in actionCfgFireBullet.MoveTweenTargetActionId)
            {
                ActionHandlerHelper.CreateAction(bulletUnit, null, actionId, 0.1f, selectHandle, ref actionContext);
            }

            EventSystem.Instance.Publish(unit.DomainScene(), new AbilityTriggerEventType.UnitOnCreate()
            {
                actionContext = actionContext,
                unit = unit,
                createUnit = bulletUnit,
            });

            EventSystem.Instance.Publish(unit.DomainScene(), new ET.Ability.AbilityTriggerEventType.CallBullet()
            {
                actionContext = actionContext,
                unit = unit,
                beCallUnit = bulletUnit,
            });
        }

        public static void EventHandler(Unit unit, AbilityConfig.BulletTriggerEvent abilityBulletMonitorTriggerEvent, Unit onAttackUnit, Unit beHurtUnit, ref ActionContext actionContext)
        {
            unit.GetComponent<BulletObj>()?.TrigEvent(abilityBulletMonitorTriggerEvent, onAttackUnit, beHurtUnit, ref actionContext);
        }

        public static (bool, bool, float3, float3) ChkBulletHit(Unit unitBullet, Unit unit)
        {
            BulletObj bulletObj = unitBullet.GetComponent<BulletObj>();

            MoveTweenObj moveTweenObj = unitBullet.GetComponent<MoveTweenObj>();
            float3 posBeforeBullet = moveTweenObj.lastPosition;
            float3 posAfterBullet = unitBullet.Position;

            bool isHitUnit = false;
            bool isHitMesh = false;
            float3 hitUnitPos = float3.zero;
            float3 hitMeshPos = float3.zero;

            if (bulletObj.CanHitUnit(unit) && moveTweenObj.ChkCanTouchUnit(unit))
            {
                (isHitUnit, hitUnitPos) = _ChkBulletHitUnit(unitBullet, unit, posBeforeBullet, posAfterBullet);
                if (isHitUnit)
                {
                    posAfterBullet = hitUnitPos;
                }
            }

            if (bulletObj.CanHitMesh(false))
            {
                (isHitMesh, hitMeshPos) = RecastHelper.ChkHitMesh(unitBullet.DomainScene(), posBeforeBullet, posAfterBullet);
                if (isHitMesh)
                {
                    isHitUnit = false;
                }
            }

            return (isHitUnit, isHitMesh, hitUnitPos, hitMeshPos);
        }

        public static (bool, float3) ChkBulletHitMesh(Unit unitBullet)
        {
            BulletObj bulletObj = unitBullet.GetComponent<BulletObj>();

            MoveTweenObj moveTweenObj = unitBullet.GetComponent<MoveTweenObj>();
            float3 posBeforeBullet = moveTweenObj.lastPosition;
            float3 posAfterBullet = unitBullet.Position;

            bool isHitMesh = false;
            float3 hitMeshPos = float3.zero;

            if (bulletObj.CanHitMesh(true))
            {
                (isHitMesh, hitMeshPos) = RecastHelper.ChkHitMesh(unitBullet.DomainScene(), posBeforeBullet, posAfterBullet);
            }

            return (isHitMesh, hitMeshPos);
        }

        public static (bool, float3) _ChkBulletHitUnit(Unit unitBullet, Unit unit, float3 posBeforeBullet, float3 posAfterBullet)
        {
            float bulletRadius = ET.Ability.UnitHelper.GetBodyRadius(unitBullet);
            bool bHitUnitTmp = UnitHelper.ChkIsNear(unit, posBeforeBullet, bulletRadius, 0, true);
            if (bHitUnitTmp)
            {
                return (true, posBeforeBullet);
            }
            bHitUnitTmp = UnitHelper.ChkIsNear(unit, posAfterBullet, bulletRadius, 0, true);
            if (bHitUnitTmp)
            {
                return (true, posAfterBullet);
            }

            if (posBeforeBullet.Equals(posAfterBullet))
            {
                return (false, float3.zero);
            }

            float3 dis = unitBullet.Position - unit.Position;
            MoveTweenObj moveTweenObj = unitBullet.GetComponent<MoveTweenObj>();
            if (moveTweenObj.speed > 5 && math.lengthsq(dis) <= 25)
            {
                float targetRadius = ET.Ability.UnitHelper.GetBodyRadius(unit);
                float3 targetPos = unit.Position;

                float3 p1toT = targetPos - posBeforeBullet;
                p1toT.y = 0;
                float3 p1toP2 = posAfterBullet - posBeforeBullet;
                p1toP2.y = 0;
                float3 p2toT = targetPos - posAfterBullet;
                p2toT.y = 0;
                float3 p2toP1 = posBeforeBullet - posAfterBullet;
                p2toP1.y = 0;
                float3 normalize_p1toT = math.normalize(p1toT);
                float3 normalize_p2toT = math.normalize(p2toT);
                float3 normalize_p1toP2 = math.normalize(p1toP2);
                float3 normalize_p2toP1 = math.normalize(p2toP1);
                float cosTP1P2 = math.dot(normalize_p1toT, normalize_p1toP2);
                float cosTP2P1 = math.dot(normalize_p2toT, normalize_p2toP1);
                if (cosTP1P2 > 0 && cosTP2P1 <= 0)  //同侧靠近,但还未到
                {
                    return (false, float3.zero);
                }
                else if (cosTP1P2 <= 0 && cosTP2P1 > 0)  //同侧远离
                {
                    // //判断下移动前的那个点
                    // if (math.pow(p1toT.x, 2) + math.pow(p1toT.z, 2) <= math.pow(bulletRadius + targetRadius, 2))
                    // {
                    //     return true;
                    // }
                    return (false, float3.zero);
                }
                else if (cosTP1P2 > 0 && cosTP2P1 > 0) //target在中间
                {
                    float lengthTmp = math.length(p1toT);
                    float disTmp = lengthTmp * math.sin(math.acos(cosTP1P2));
                    //判断垂直距离
                    if (disTmp <= bulletRadius + targetRadius)
                    {
                        float3 hitPos = posBeforeBullet + math.normalize(posAfterBullet - posBeforeBullet) * lengthTmp * cosTP1P2;
                        return (true, hitPos);
                    }
                    return (false, float3.zero);
                }
                return (false, float3.zero);
            }
            else
            {
                return (false, float3.zero);
            }
        }

        public static void DoBulletHitUnit(Unit unitBullet, Unit unit, float3 hitUnitPos)
        {
            BulletObj bulletObj = unitBullet.GetComponent<BulletObj>();

            if (bulletObj.canHitTimes > 0)
            {
                if (bulletObj.hitRecords.ContainsKey(unit.Id))
                {
                    return;
                }
                BulletHitRecord bulletHitRecord = BulletHitRecord.Create();
                bulletHitRecord.targetUnitId = unit.Id;
                bulletHitRecord.timeToCanHit = bulletObj.model.SameTargetDelay;
                bulletObj.hitRecords.Add(bulletHitRecord.targetUnitId, bulletHitRecord);
                bulletObj.canHitTimes -= 1;
            }
            else
            {
                return;
            }

            if (bulletObj.canHitTimes == 0)
            {
                unitBullet.Position = hitUnitPos;
            }

            ActionContext actionContext = bulletObj.actionContext;
            actionContext.hitPosition = hitUnitPos;
            EventSystem.Instance.Publish(unitBullet.DomainScene(), new AbilityTriggerEventType.BulletOnHit()
            {
                actionContext = actionContext,
                attackerUnit = unitBullet,
                defenderUnit = unit,
            });

        }

        public static void DoBulletHitMesh(Unit unitBullet, float3 hitMeshPos)
        {
            BulletObj bulletObj = unitBullet.GetComponent<BulletObj>();

            if (bulletObj.canHitTimes > 0)
            {
                if (bulletObj.hitRecords.ContainsKey(-1))
                {
                    return;
                }
                BulletHitRecord bulletHitRecord = BulletHitRecord.Create();
                bulletHitRecord.targetUnitId = -1;
                bulletHitRecord.timeToCanHit = bulletObj.model.SameTargetDelay;
                bulletObj.hitRecords.Add(bulletHitRecord.targetUnitId, bulletHitRecord);
                //bulletObj.canHitTimes -= 1;
                bulletObj.canHitTimes = 0;
            }
            else
            {
                return;
            }

            if (bulletObj.canHitTimes == 0)
            {
                unitBullet.Position = hitMeshPos;
            }

            ActionContext actionContext = bulletObj.actionContext;
            actionContext.hitPosition = hitMeshPos;
            EventSystem.Instance.Publish(unitBullet.DomainScene(), new AbilityTriggerEventType.BulletOnHitMesh()
            {
                actionContext = actionContext,
                attackerUnit = unitBullet,
            });

        }

        public static void DoBulletHitPos(Unit unitBullet)
        {
            BulletObj bulletObj = unitBullet.GetComponent<BulletObj>();

            if (bulletObj.canHitTimes > 0)
            {
                bulletObj.canHitTimes = 0;
            }
            else
            {
                return;
            }

            float3 hitPos = unitBullet.Position;
            ActionContext actionContext = bulletObj.actionContext;
            actionContext.hitPosition = hitPos;
            EventSystem.Instance.Publish(unitBullet.DomainScene(), new AbilityTriggerEventType.BulletOnHitPos()
            {
                actionContext = actionContext,
                attackerUnit = unitBullet,
            });

        }

    }
}