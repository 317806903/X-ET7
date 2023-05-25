using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    [FriendOf(typeof(UnitComponent))]
    public static class UnitHelper_Create
    {
        public static UnitComponent GetUnitComponent(Unit unit)
        {
            return unit.DomainScene().GetComponent<UnitComponent>();
        }
        
        public static UnitComponent GetUnitComponent(Scene scene)
        {
            return scene.GetComponent<UnitComponent>();
        }
        
        public static Unit CreateWhenClient(Scene scene, UnitInfo unitInfo)
        {
            UnitComponent unitComponent = scene.GetComponent<UnitComponent>();
            long id = unitInfo.UnitId;
            string unitCfgId = unitInfo.ConfigId;
            UnitType unitType = (UnitType) unitInfo.Type;
            Unit unit = unitComponent.AddChildWithId<Unit, string>(id, unitCfgId);
            unit.Type = unitType;
            unitComponent.Add(unit);
            return unit;
        }

        public static Unit CreateWhenServer_Common_Before(UnitComponent unitComponent, string unitCfgId, UnitType unitType, TeamFlagType teamFlagType, float3 position, 
        float3 forward)
        {
            Unit unit = unitComponent.AddChild<Unit, string>(unitCfgId);
            unit.AddComponent<MoveByPathComponent>();
            unit.Position = position;
            unit.Forward = forward;
            unit.Type = unitType;
			
            NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
            numericComponent.Set(NumericType.Speed, 6f); // 速度是6米每秒
            numericComponent.Set(NumericType.AOI, 15000); // 视野15米

            unit.AddComponent<ET.Ability.TimelineComponent>();
            unit.AddComponent<ET.Ability.BuffComponent>();
            unit.AddComponent<ET.Ability.SkillComponent>();

            unit.AddComponent<ET.Ability.EffectComponent>();
            unit.AddComponent<ET.Ability.MoveComponent>();
            unit.AddComponent<ET.Ability.RotateComponent>();

            unit.AddComponent<TeamFlagObj, TeamFlagType>(teamFlagType);
            
            return unit;
        }
        
        public static void CreateWhenServer_Common_After(UnitComponent unitComponent, Unit unit)
        {
            unitComponent.Add(unit);
            // 加入aoi
            unit.AddComponent<AOIEntity, int, float3>(9 * 1000, unit.Position);
        }
        
        public static Unit CreateWhenServer_CommonPlayer(Scene scene, long playerId, string unitCfgId, UnitType unitType, TeamFlagType teamFlagType, float3 
        position, 
        float3 forward)
        {
            UnitComponent unitComponent = GetUnitComponent(scene);
            
            Unit unit = unitComponent.AddChildWithId<Unit, string>(playerId, unitCfgId);
            unit.AddComponent<MoveByPathComponent>();
            unit.Position = position;
            unit.Forward = forward;
            unit.Type = unitType;
			
            NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
            numericComponent.Set(NumericType.Speed, 6f); // 速度是6米每秒
            numericComponent.Set(NumericType.AOI, 15000); // 视野15米

            unitComponent.Add(unit);

            unit.AddComponent<ET.Ability.TimelineComponent>();
            unit.AddComponent<ET.Ability.BuffComponent>();
            unit.AddComponent<ET.Ability.SkillComponent>();

            unit.AddComponent<ET.Ability.EffectComponent>();
            unit.AddComponent<ET.Ability.MoveComponent>();
            unit.AddComponent<ET.Ability.RotateComponent>();

            unit.AddComponent<TeamFlagObj, TeamFlagType>(teamFlagType);
            // 加入aoi
            unit.AddComponent<AOIEntity, int, float3>(9 * 1000, unit.Position);
            return unit;
        }
        
        public static Unit CreateWhenServer_Player(Scene scene, long playerId, float3 position, float3 forward)
        {
            UnitComponent unitComponent = GetUnitComponent(scene);

            //Unit unit = CreateWhenServer_Common_Before(unitComponent, "Unit_Player1", UnitType.Player, TeamFlagType.Team1, position, forward);
            //CreateWhenServer_Common_After(unitComponent, unit);
            
            Unit unit = CreateWhenServer_CommonPlayer(scene, playerId, "Unit_Player1", UnitType.Player, TeamFlagType.Team1, position, forward);
            
            return unit;
        }

        public static Unit CreateWhenServer_Monster(Scene scene, float3 position, float3 forward)
        {
            UnitComponent unitComponent = GetUnitComponent(scene);
            Unit unit = CreateWhenServer_Common_Before(unitComponent, "Unit_Monster1", UnitType.Monster, TeamFlagType.Monster, position, forward);
            unit.AddComponent<AIComponent, int>(11);
            unit.AddComponent<PathfindingComponent, string>(scene.Name);
            CreateWhenServer_Common_After(unitComponent, unit);

            SkillHelper.LearnSkill(unit, "Skill_1", 1, SkillSlotType.NormalAttack);

            return unit;
        }

        public static Unit CreateWhenServer_Bullet(Scene scene, Unit unitCaster, ActionCfg_FireBullet actionCfgFireBullet, SelectHandle selectHandle)
        {
            UnitComponent unitComponent = GetUnitComponent(scene);
            TeamFlagType teamFlagType = unitCaster.GetComponent<TeamFlagObj>().teamFlagType;
            
            float3 position = unitCaster.Position;
            float3 forward = unitCaster.Forward;
            Unit bulletUnit = CreateWhenServer_Common_Before(unitComponent, "Unit_Bullet1", UnitType.Bullet, teamFlagType, position, forward);

            BulletObj bulletObj = bulletUnit.AddComponent<BulletObj>();
            bulletObj.Init(unitCaster.Id, actionCfgFireBullet.BulletId, actionCfgFireBullet.Duration);

            UnitHelper.ResetNodePosition(unitCaster, bulletUnit, actionCfgFireBullet.NodeName, actionCfgFireBullet.OffSetPosition, actionCfgFireBullet.RelateForward);

            MoveTweenHelper.CreateMoveTween(bulletUnit, actionCfgFireBullet.MoveType, selectHandle);
            
            CreateWhenServer_Common_After(unitComponent, bulletUnit);

            return bulletUnit;
        }

    }
}