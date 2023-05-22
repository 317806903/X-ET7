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
        public static void CreateBullet(Unit unit, ActionCfg_FireBullet actionCfgFireBullet, SelectHandle selectHandle)
        {
            //UnitHelper_Create.CreateWhenServer()
            UnitComponent unitComponent = UnitHelper.GetUnitComponent(unit);
            Unit bulletUnit = unitComponent.AddChild<Unit>();
            bulletUnit.Type = UnitType.Bullet;
            bulletUnit.AddComponent<TeamFlagObj, TeamFlagType>(unit.GetComponent<TeamFlagObj>().GetTeamFlagType());
            BulletObj bulletObj = bulletUnit.AddComponent<BulletObj>();
            bulletObj.Init(unit.Id, actionCfgFireBullet.BulletId, actionCfgFireBullet.Duration);

            NumericComponent numericComponent = bulletUnit.AddComponent<NumericComponent>();
            numericComponent.Set(NumericType.Speed, 6f); // 速度是6米每秒
            numericComponent.Set(NumericType.AOI, 15000); // 视野15米

            UnitHelper.ResetNodePosition(unit, bulletUnit, actionCfgFireBullet.NodeName, actionCfgFireBullet.OffSetPosition, actionCfgFireBullet.RelateForward);

            MoveTweenHelper.CreateMoveTween(bulletUnit, actionCfgFireBullet.MoveType, selectHandle);
            bulletUnit.AddComponent<AOIEntity, int, float3>(9 * 1000, bulletUnit.Position);
            
            unitComponent.Add(bulletUnit);

            EventSystem.Instance.Publish(unit.DomainScene(), new AbilityTriggerEventType.UnitOnCreate()
            {
                unit = unit,
                createUnit = bulletUnit,
            });
            
            //EventSystem.Instance.Invoke<SyncUnits>(new SyncUnits(){
            //    units = new List<Unit>(){bulletUnit},
            //});
        }
        
        public static void EventHandler(Unit unit, AbilityBulletMonitorTriggerEvent abilityBulletMonitorTriggerEvent, Unit onHitUnit, Unit beHurtUnit)
        {
            unit.GetComponent<BulletObj>()?.EventHandler(abilityBulletMonitorTriggerEvent, onHitUnit, beHurtUnit);
        }
        
        public static bool ChkBulletHit(Unit unitBullet, Unit unit)
        {
            BulletObj bulletObj = unitBullet.GetComponent<BulletObj>();
            if (bulletObj.CanHit(unit) == false)
            {
                return false;
            }
            float bRadius = 0.1f;
            float cRadius = 0.1f;
            float3 dis = unitBullet.Position - unit.Position;
            
            if (math.pow(dis.x, 2) + math.pow(dis.z, 2) <= math.pow(bRadius + cRadius, 2))
            {
                return true;
            }

            return false;
        }
        
        public static void DoBulletHit(Unit unitBullet, Unit unit)
        {
            BulletObj bulletObj = unitBullet.GetComponent<BulletObj>();
            bulletObj.canHitTimes -= 1;
            
            EventSystem.Instance.Publish(unitBullet.DomainScene(), new AbilityTriggerEventType.UnitOnHit()
            {
                attackerUnit = unitBullet,
                defenderUnit = unit,
            });

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