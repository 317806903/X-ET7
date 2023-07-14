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
            unit.GetComponent<BulletObj>()?.EventHandler(abilityBulletMonitorTriggerEvent, onAttackUnit, beHurtUnit);
        }
        
        public static bool ChkBulletHit(Unit unitBullet, Unit unit)
        {
            BulletObj bulletObj = unitBullet.GetComponent<BulletObj>();
            if (bulletObj.CanHit(unit) == false)
            {
                return false;
            }
            
            float bulletRadius = unitBullet.GetComponent<BulletObj>().model.BodyRadius;
            float targetRadius = unit.model.BodyRadius;
            float3 dis = unitBullet.Position - unit.Position;
            if (math.pow(dis.x, 2) + math.pow(dis.z, 2) <= math.pow(bulletRadius + targetRadius, 2))
            {
                return true;
            }

            MoveTweenObj moveTweenObj = unitBullet.GetComponent<MoveTweenObj>();
            if (moveTweenObj.speed > 10 && math.lengthsq(dis) <= 16)
            {
                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                float3 targetPos = unit.Position;
                float3 posBeforeBullet = unitBullet.Position - moveTweenObj.speed * moveTweenObj.forward * fixedDeltaTime;
                float3 posAfterBullet = unitBullet.Position;
                float3 p1toT = targetPos - posBeforeBullet;
                float3 p1toP2 = posAfterBullet - posBeforeBullet;
                float3 p2toT = targetPos - posAfterBullet;
                float3 p2toP1 = posBeforeBullet - posAfterBullet;
                float cosTP1P2 = math.dot(math.normalize(p1toT), math.normalize(p1toP2));
                float cosTP2P1 = math.dot(math.normalize(p2toT), math.normalize(p2toP1));
                if (cosTP1P2 > 0 && cosTP2P1 <= 0)  //同侧靠近,但还未到
                {
                    return false;
                }
                else if (cosTP1P2 <= 0 && cosTP2P1 > 0)  //同侧远离
                {
                    //判断下移动前的那个点
                    if (math.pow(p1toT.x, 2) + math.pow(p1toT.z, 2) <= math.pow(bulletRadius + targetRadius, 2))
                    {
                        return true;
                    }
                    return false;
                }
                else if (cosTP1P2 > 0 && cosTP2P1 > 0) //target在中间
                {
                    float disTmp = math.length(p1toT) * math.sin(math.acos(cosTP1P2));
                    //判断垂直距离
                    if (disTmp <= bulletRadius + targetRadius)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            else
            {
                return false;
            }
        }
        
        public static void DoBulletHit(Unit unitBullet, Unit unit)
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

            if (bulletObj.canHitTimes > 0){
                bulletObj.hitRecords.Add(new BulletHitRecord()
                {
                    targetUnitId = unit.Id,
                    timeToCanHit = bulletObj.model.SameTargetDelay,
                });
            }
        }
        
    }
}