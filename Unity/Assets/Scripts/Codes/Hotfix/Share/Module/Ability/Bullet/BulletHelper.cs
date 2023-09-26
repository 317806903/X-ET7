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
        public static void CreateBullet(Unit unit, ActionCfg_FireBullet actionCfgFireBullet, SelectHandle selectHandle, ActionContext actionContext)
        {
            Unit bulletUnit = ET.GamePlayHelper.CreateBulletByUnit(unit.DomainScene(), unit, actionCfgFireBullet, selectHandle, actionContext);

            EventSystem.Instance.Publish(unit.DomainScene(), new AbilityTriggerEventType.UnitOnCreate()
            {
                unit = unit,
                createUnit = bulletUnit,
            });

        }

        public static void EventHandler(Unit unit, AbilityBulletMonitorTriggerEvent abilityBulletMonitorTriggerEvent, Unit onAttackUnit, Unit beHurtUnit)
        {
            unit.GetComponent<BulletObj>()?.TrigEvent(abilityBulletMonitorTriggerEvent, onAttackUnit, beHurtUnit);
        }

        public static (bool, bool, float3) ChkBulletHit(Unit unitBullet, Unit unit)
        {
            BulletObj bulletObj = unitBullet.GetComponent<BulletObj>();

            MoveTweenObj moveTweenObj = unitBullet.GetComponent<MoveTweenObj>();
            float3 posBeforeBullet = moveTweenObj.lastPosition;
            float3 posAfterBullet = unitBullet.Position;

            bool bHitUnit = false;
            bool bHitMesh = false;
            float3 hitPos = float3.zero;

            if (bulletObj.CanHitUnit(unit))
            {
                (bool isHitUnit, float3 hitUnitPos) = _ChkBulletHitUnit(unitBullet, unit, posBeforeBullet, posAfterBullet);
                if (isHitUnit)
                {
                    posAfterBullet = hitUnitPos;
                    bHitUnit = true;
                    hitPos = hitUnitPos;
                }
            }

            if (bulletObj.CanHitMesh())
            {
                (bool isHitMesh, float3 hitMeshPos) = RecastHelper.ChkHitMesh(unitBullet.DomainScene(), posBeforeBullet, posAfterBullet);
                if (isHitMesh)
                {
                    bHitUnit = false;
                    bHitMesh = true;
                    hitPos = hitMeshPos;
                }
            }

            return (bHitUnit, bHitMesh, hitPos);
        }

        public static (bool, bool, float3) ChkBulletHit_Old(Unit unitBullet, Unit unit)
        {
            BulletObj bulletObj = unitBullet.GetComponent<BulletObj>();

            MoveTweenObj moveTweenObj = unitBullet.GetComponent<MoveTweenObj>();
            float3 posBeforeBullet = moveTweenObj.lastPosition;
            float3 posAfterBullet = unitBullet.Position;

            (bool isHitUnit_PreChk, float3 hitPos) = _ChkBulletHitUnit(unitBullet, unit, posBeforeBullet, posAfterBullet);

            if (isHitUnit_PreChk)
            {
                posAfterBullet = hitPos;
            }
            List<float3> segmentPoints = ET.RecastHelper.GetSegmentPoints(unitBullet.DomainScene(), posBeforeBullet, posAfterBullet);

            bool bHitUnit = false;
            bool bHitMesh = false;
            float3 lastPos = posBeforeBullet;

            float bulletRadius = ET.Ability.UnitHelper.GetBodyRadius(unitBullet);
            for (int i = 0; i < segmentPoints.Count; i++)
            {
                float3 pos = segmentPoints[i];
                if (bulletObj.CanHitUnit(unit))
                {
                    bool bHitUnitTmp = UnitHelper.ChkIsNear(unit, pos, bulletRadius, 0, true);
                    if (bHitUnitTmp)
                    {
                        bHitUnit = true;
                        hitPos = pos;
                        break;
                    }
                    else
                    {
                        (bHitUnitTmp, hitPos) = _ChkBulletHitUnit(unitBullet, unit, lastPos, pos);
                        if (bHitUnitTmp)
                        {
                            bHitUnit = true;
                            hitPos = pos;
                            break;
                        }
                    }
                }

                if (bulletObj.CanHitMesh())
                {
                    bool bHitMeshTmp = RecastHelper.ChkHitMeshOnPoint(unitBullet.DomainScene(), pos);
                    if (bHitMeshTmp)
                    {
                        bHitMesh = true;
                        hitPos = pos;
                        break;
                    }
                }

                lastPos = pos;
            }
            return (bHitUnit, bHitMesh, hitPos);

            // float targetRadius = ET.Ability.UnitHelper.GetBodyRadius(unit);
            // float3 dis = unitBullet.Position - unit.Position;
            // if (math.pow(dis.x, 2) + math.pow(dis.z, 2) <= math.pow(bulletRadius + targetRadius, 2))
            // {
            //     return true;
            // }
            //
            // MoveTweenObj moveTweenObj = unitBullet.GetComponent<MoveTweenObj>();
            // if (moveTweenObj.speed > 10 && math.lengthsq(dis) <= 16)
            // {
            //     float3 targetPos = unit.Position;
            //     float3 posBeforeBullet = moveTweenObj.lastPosition;
            //     float3 posAfterBullet = unitBullet.Position;
            //     float3 p1toT = targetPos - posBeforeBullet;
            //     float3 p1toP2 = posAfterBullet - posBeforeBullet;
            //     float3 p2toT = targetPos - posAfterBullet;
            //     float3 p2toP1 = posBeforeBullet - posAfterBullet;
            //     float cosTP1P2 = math.dot(math.normalize(p1toT), math.normalize(p1toP2));
            //     float cosTP2P1 = math.dot(math.normalize(p2toT), math.normalize(p2toP1));
            //     if (cosTP1P2 > 0 && cosTP2P1 <= 0)  //同侧靠近,但还未到
            //     {
            //         return false;
            //     }
            //     else if (cosTP1P2 <= 0 && cosTP2P1 > 0)  //同侧远离
            //     {
            //         //判断下移动前的那个点
            //         if (math.pow(p1toT.x, 2) + math.pow(p1toT.z, 2) <= math.pow(bulletRadius + targetRadius, 2))
            //         {
            //             return true;
            //         }
            //         return false;
            //     }
            //     else if (cosTP1P2 > 0 && cosTP2P1 > 0) //target在中间
            //     {
            //         float disTmp = math.length(p1toT) * math.sin(math.acos(cosTP1P2));
            //         //判断垂直距离
            //         if (disTmp <= bulletRadius + targetRadius)
            //         {
            //             return true;
            //         }
            //         return false;
            //     }
            //     return false;
            // }
            // else
            // {
            //     return false;
            // }
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
            if (moveTweenObj.speed > 10 && math.lengthsq(dis) <= 16)
            {
                float targetRadius = ET.Ability.UnitHelper.GetBodyRadius(unit);
                float3 targetPos = unit.Position;

                float3 p1toT = targetPos - posBeforeBullet;
                float3 p1toP2 = posAfterBullet - posBeforeBullet;
                float3 p2toT = targetPos - posAfterBullet;
                float3 p2toP1 = posBeforeBullet - posAfterBullet;
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
                    float disTmp = math.length(p1toT) * math.sin(math.acos(cosTP1P2));
                    //判断垂直距离
                    if (disTmp <= bulletRadius + targetRadius)
                    {
                        float3 hitPos = posBeforeBullet + normalize_p1toP2 * math.length(p1toT) * cosTP1P2;
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

        public static void DoBulletHitUnit(Unit unitBullet, Unit unit)
        {
            BulletObj bulletObj = unitBullet.GetComponent<BulletObj>();
            bulletObj.canHitTimes -= 1;

            ProfilerSample.BeginSample("DoBulletHit");
            EventSystem.Instance.Publish(unitBullet.DomainScene(), new AbilityTriggerEventType.BulletOnHit()
            {
                attackerUnit = unitBullet,
                defenderUnit = unit,
            });
            ProfilerSample.EndSample();

            if (bulletObj.canHitTimes > 0)
            {
                BulletHitRecord bulletHitRecord = BulletHitRecord.Create();
                bulletHitRecord.targetUnitId = unit.Id;
                bulletHitRecord.timeToCanHit = bulletObj.model.SameTargetDelay;
                bulletObj.hitRecords.Add(bulletHitRecord);
            }
        }

        public static void DoBulletHitMesh(Unit unitBullet, float3 hitPos)
        {
            BulletObj bulletObj = unitBullet.GetComponent<BulletObj>();
            //bulletObj.canHitTimes -= 1;
            bulletObj.canHitTimes = 0;

            EventSystem.Instance.Publish(unitBullet.DomainScene(), new AbilityTriggerEventType.BulletOnHitMesh()
            {
                attackerUnit = unitBullet,
                hitPos = hitPos,
            });

            if (bulletObj.canHitTimes > 0)
            {
                BulletHitRecord bulletHitRecord = BulletHitRecord.Create();
                bulletHitRecord.targetUnitId = -1;
                bulletHitRecord.timeToCanHit = bulletObj.model.SameTargetDelay;
                bulletObj.hitRecords.Add(bulletHitRecord);
            }
        }

    }
}