using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    [FriendOf(typeof(UnitComponent))]
    [FriendOf(typeof(TeamFlagObj))]
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
            numericComponent.Set(NumericType.AOI, 15000); // 视野15米
            if (unitType != UnitType.Bullet)
            {
                numericComponent.Set(NumericType.Speed, unit.model.MoveSpeed); // 速度是6米每秒
                numericComponent.Set(NumericType.RotationSpeed, unit.model.RotationSpeed);
            }

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
            unit.AddComponent<AOIEntity, int, float3>(30 * 1000, unit.Position);
        }
        
        public static Unit CreateWhenServer_CommonPlayerUnit(Scene scene, long playerId, string unitCfgId, int level, UnitType unitType, TeamFlagType teamFlagType, float3 
        position, 
        float3 forward)
        {
            UnitComponent unitComponent = GetUnitComponent(scene);
            
            Unit unit;
            if (playerId == -1)
            {
                unit = unitComponent.AddChild<Unit, string>(unitCfgId);
                playerId = unit.Id;
            }
            else
            {
                unit = unitComponent.AddChildWithId<Unit, string>(playerId, unitCfgId);
            }
            unit.AddComponent<MoveByPathComponent>();
            unit.Position = position;
            unit.Forward = forward;
            unit.Type = unitType;
			
            NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
            numericComponent.Set(NumericType.AOI, 15000); // 视野15米
            numericComponent.Set(NumericType.Speed, unit.model.MoveSpeed); // 速度是6米每秒
            numericComponent.Set(NumericType.RotationSpeed, unit.model.RotationSpeed);

            unitComponent.Add(unit);

            unit.AddComponent<ET.Ability.TimelineComponent>();
            unit.AddComponent<ET.Ability.BuffComponent>();
            unit.AddComponent<ET.Ability.SkillComponent>();

            unit.AddComponent<ET.Ability.EffectComponent>();
            unit.AddComponent<ET.Ability.MoveComponent>();
            unit.AddComponent<ET.Ability.RotateComponent>();

            unit.AddComponent<TeamFlagObj, TeamFlagType>(teamFlagType);

            UnitPropertyCfg unitPropertyCfg = UnitPropertyCfgCategory.Instance.Get(unit.model.PropertyType, level);
            numericComponent.Set(NumericType.MaxHp, unitPropertyCfg.Hp);
            numericComponent.Set(NumericType.Hp, unitPropertyCfg.Hp);
            numericComponent.Set(NumericType.PhysicalAttack, unitPropertyCfg.PhysicalAttack);
            
            // 加入aoi
            unit.AddComponent<AOIEntity, int, float3>(30 * 1000, unit.Position);
            return unit;
        }
        
        public static Unit CreateWhenServer_PlayerUnit(Scene scene, long playerId, int level, TeamFlagType teamFlagType, float3 position, float3 forward)
        {
            UnitComponent unitComponent = GetUnitComponent(scene);
            Unit unit;
            if (false)
            {
                //{
                //    unit = CreateWhenServer_Common_Before(unitComponent, "Unit_MachineGunTower_0", UnitType.Player, TeamFlagType.Team1, position, forward);
                // NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
                //
                // UnitPropertyCfg unitPropertyCfg = UnitPropertyCfgCategory.Instance.Get(unit.model.PropertyType, level);
                // numericComponent.Set(NumericType.MaxHp, unitPropertyCfg.Hp);
                // numericComponent.Set(NumericType.Hp, unitPropertyCfg.Hp);
                // numericComponent.Set(NumericType.PhysicalAttack, unitPropertyCfg.PhysicalAttack);

                    
                //    CreateWhenServer_Common_After(unitComponent, unit);
                //}
            }
            else
            {
                {
                    unit = CreateWhenServer_CommonPlayerUnit(scene, playerId, "Unit_HeadQuarter", level, UnitType.PlayerUnit, teamFlagType, position, forward);
                }
            }
            
            return unit;
        }

        public static Unit CreateWhenServer_HeadQuarterUnit(Scene scene, string unitCfgId, TeamFlagType teamFlagType, float3 position, float3 forward)
        {
            int level = 1;
            Unit unit = CreateWhenServer_CommonPlayerUnit(scene, -1, unitCfgId, level, UnitType.PlayerUnit, teamFlagType, position, forward);

            return unit;
        }

        public static Unit CreateWhenServer_MonsterCallUnit(Scene scene, string unitCfgId, float3 position, float3 forward)
        {
            TeamFlagType teamFlagType = TeamFlagType.Monster; 
            Unit unit = CreateWhenServer_NPC(scene, unitCfgId, teamFlagType, position, forward);

            return unit;
        }

        public static Unit CreateWhenServer_ObserverUnit(Scene scene, long playerId, TeamFlagType teamFlagType, float3 position, float3 forward)
        {
            string unitCfgId = "Unit_Observer";
            int level = 1;
            Unit unit = CreateWhenServer_CommonPlayerUnit(scene, playerId, unitCfgId, level, UnitType.ObserverUnit, teamFlagType, position, forward);

            return unit;
        }

        public static Unit CreateWhenServer_ActorUnit(Scene scene, string unitCfgId, int level, TeamFlagType teamFlagType, float3 position, float3 
        forward, string aiCfg)
        {
            UnitComponent unitComponent = GetUnitComponent(scene);
            Unit unit = CreateWhenServer_Common_Before(unitComponent, unitCfgId, UnitType.ActorUnit, teamFlagType, position, forward);
            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();

            UnitPropertyCfg unitPropertyCfg = UnitPropertyCfgCategory.Instance.Get(unit.model.PropertyType, level);
            numericComponent.Set(NumericType.MaxHp, unitPropertyCfg.Hp);
            numericComponent.Set(NumericType.Hp, unitPropertyCfg.Hp);
            numericComponent.Set(NumericType.PhysicalAttack, unitPropertyCfg.PhysicalAttack);

            unit.AddComponent<AIComponent, string>(aiCfg);
            unit.AddComponent<PathfindingComponent, string>(scene.Name);
            CreateWhenServer_Common_After(unitComponent, unit);

            UnitCfg unitCfg = UnitCfgCategory.Instance.Get(unitCfgId);
            int count = unitCfg.SkillList.Count;
            for (int i = 0; i < count; i++)
            {
                SkillHelper.LearnSkill(unit, unitCfg.SkillList[i], 1, SkillSlotType.NormalAttack);
            }

            return unit;
        }

        public static Unit CreateWhenServer_TowerUnit(Scene scene, string towerCfgId, TeamFlagType teamFlagType, float3 position, float3 forward)
        {
            TowerCfg towerCfg = TowerCfgCategory.Instance.Get(towerCfgId);
            Unit unit = CreateWhenServer_ActorUnit(scene, towerCfg.UnitId, towerCfg.Level, teamFlagType, position, forward, towerCfg.AiCfgId);
            return unit;
        }

        public static Unit CreateWhenServer_MonsterUnit(Scene scene, string monsterCfgId, int level, float3 position, float3 forward)
        {
            MonsterCfg monsterCfg = MonsterCfgCategory.Instance.Get(monsterCfgId);
            Unit unit = CreateWhenServer_ActorUnit(scene, monsterCfg.UnitId, level, TeamFlagType.Monster, position, forward, monsterCfg.AiCfgId);
            return unit;
        }

        public static Unit CreateWhenServer_Bullet(Scene scene, Unit unitCaster, ActionCfg_FireBullet actionCfgFireBullet, SelectHandle selectHandle, ActionContext actionContext)
        {
            UnitComponent unitComponent = GetUnitComponent(scene);
            TeamFlagType teamFlagType = unitCaster.GetComponent<TeamFlagObj>().GetTeamFlagType();
            
            float3 position = unitCaster.Position;
            float3 forward = unitCaster.Forward;
            Unit bulletUnit = CreateWhenServer_Common_Before(unitComponent, "Unit_Bullet1", UnitType.Bullet, teamFlagType, position, forward);

            NumericComponent numericComponentCaster = unitCaster.GetComponent<NumericComponent>();
            NumericComponent numericComponentBullet = bulletUnit.GetComponent<NumericComponent>();
            foreach (var numeric in numericComponentCaster.NumericDic)
            {
                numericComponentBullet.Set(numeric.Key, numeric.Value);
            }
            numericComponentBullet.isFloatKey = numericComponentCaster.isFloatKey;
            numericComponentBullet.Set(NumericType.RotationSpeed, 2000f);//子弹写死一个大的值

            BulletObj bulletObj = bulletUnit.AddComponent<BulletObj>();
            bulletObj.Init(unitCaster.Id, actionCfgFireBullet.BulletId, actionCfgFireBullet.Duration);
            bulletObj.InitActionContext(actionContext);

            (float3 newPosition, float3 newForward) = UnitHelper.GetNewNodePosition(unitCaster, actionCfgFireBullet.OffSetInfo);
            bulletUnit.Position = newPosition;
            bulletUnit.Forward = newForward;

            MoveTweenHelper.CreateMoveTween(bulletUnit, actionCfgFireBullet.MoveType, selectHandle);
            
            CreateWhenServer_Common_After(unitComponent, bulletUnit);

            return bulletUnit;
        }

        public static Unit CreateWhenServer_SceneEffect(Scene scene, float3 position, float3 forward)
        {
            UnitComponent unitComponent = GetUnitComponent(scene);
            Unit unit = CreateWhenServer_Common_Before(unitComponent, "Unit_SceneEffectNone", UnitType.SceneEffect, TeamFlagType.TeamGlobal1, position, forward);
            CreateWhenServer_Common_After(unitComponent, unit);

            return unit;
        }
        
        public static Unit CreateWhenServer_NPC(Scene scene, string unitCfgId, TeamFlagType teamFlagType, float3 
                position, 
        float3 forward)
        {
            UnitComponent unitComponent = GetUnitComponent(scene);
            Unit unit = CreateWhenServer_Common_Before(unitComponent, unitCfgId, UnitType.NPC, teamFlagType, position, forward);
            CreateWhenServer_Common_After(unitComponent, unit);

            return unit;
        }

    }
}