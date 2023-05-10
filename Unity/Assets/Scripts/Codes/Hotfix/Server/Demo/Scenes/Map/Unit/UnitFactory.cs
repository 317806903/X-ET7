using System;
using Unity.Mathematics;

namespace ET.Server
{
    public static class UnitFactory
    {
        public static Unit Create(Scene scene, long id, UnitType unitType)
        {
            UnitComponent unitComponent = scene.GetComponent<UnitComponent>();
            switch (unitType)
            {
                case UnitType.Player:
                {
                    Unit unit = unitComponent.AddChildWithId<Unit, int>(id, 1001);
                    unit.AddComponent<MoveComponent>();
                    unit.Position = new float3(-10, 0, -10);
			
                    NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
                    numericComponent.Set(NumericType.Speed, 6f); // 速度是6米每秒
                    numericComponent.Set(NumericType.AOI, 15000); // 视野15米
                    
                    unitComponent.Add(unit);
                    // 加入aoi
                    unit.AddComponent<AOIEntity, int, float3>(9 * 1000, unit.Position);
                    
                    unit.AddComponent<ET.Ability.TimelineComponent>();
                    unit.AddComponent<ET.Ability.BuffComponent>();
                    unit.AddComponent<ET.Ability.SkillComponent>();

                    unit.AddComponent<ET.Ability.EffectComponent>();
                    unit.AddComponent<ET.Ability.MoveComponent>();
                    unit.AddComponent<ET.Ability.RotateComponent>();
                    return unit;
                    break;
                }
                case UnitType.Monster:
                {
                    Unit unit = unitComponent.AddChildWithId<Unit, int>(id, 1001);
                    unit.AddComponent<MoveComponent>();
                    unit.Position = new float3(-10, 0, -10);
			
                    NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
                    numericComponent.Set(NumericType.Speed, 6f); // 速度是6米每秒
                    numericComponent.Set(NumericType.AOI, 15000); // 视野15米
                    
                    unitComponent.Add(unit);
                    // 加入aoi
                    unit.AddComponent<AOIEntity, int, float3>(9 * 1000, unit.Position);
                    
                    unit.AddComponent<ET.Ability.TimelineComponent>();
                    unit.AddComponent<ET.Ability.BuffComponent>();
                    unit.AddComponent<ET.Ability.SkillComponent>();

                    unit.AddComponent<ET.Ability.EffectComponent>();
                    unit.AddComponent<ET.Ability.MoveComponent>();
                    unit.AddComponent<ET.Ability.RotateComponent>();
                    return unit;
                    break;
                }
                case UnitType.NPC:
                {
                    Unit unit = unitComponent.AddChildWithId<Unit, int>(id, 1001);
                    unit.AddComponent<MoveComponent>();
                    unit.Position = new float3(-10, 0, -10);
			
                    NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
                    numericComponent.Set(NumericType.Speed, 6f); // 速度是6米每秒
                    numericComponent.Set(NumericType.AOI, 15000); // 视野15米
                    
                    unitComponent.Add(unit);
                    // 加入aoi
                    unit.AddComponent<AOIEntity, int, float3>(9 * 1000, unit.Position);
                    
                    unit.AddComponent<ET.Ability.TimelineComponent>();
                    unit.AddComponent<ET.Ability.BuffComponent>();
                    unit.AddComponent<ET.Ability.SkillComponent>();
                    
                    unit.AddComponent<ET.Ability.EffectComponent>();
                    unit.AddComponent<ET.Ability.MoveComponent>();
                    unit.AddComponent<ET.Ability.RotateComponent>();
                    return unit;
                    break;
                }
                case UnitType.SceneObj:
                {
                    Unit unit = unitComponent.AddChildWithId<Unit, int>(id, 1001);
                    unit.AddComponent<MoveComponent>();
                    unit.Position = new float3(-10, 0, -10);
			
                    NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
                    numericComponent.Set(NumericType.Speed, 6f); // 速度是6米每秒
                    numericComponent.Set(NumericType.AOI, 15000); // 视野15米
                    
                    unitComponent.Add(unit);
                    // 加入aoi
                    unit.AddComponent<AOIEntity, int, float3>(9 * 1000, unit.Position);
                    
                    unit.AddComponent<ET.Ability.TimelineComponent>();
                    unit.AddComponent<ET.Ability.BuffComponent>();
                    unit.AddComponent<ET.Ability.SkillComponent>();
                    
                    unit.AddComponent<ET.Ability.EffectComponent>();
                    unit.AddComponent<ET.Ability.MoveComponent>();
                    unit.AddComponent<ET.Ability.RotateComponent>();
                    return unit;
                    break;
                }
                case UnitType.Bullet:
                {
                    Unit unit = unitComponent.AddChildWithId<Unit, int>(id, 1001);
                    unit.AddComponent<MoveComponent>();
                    unit.Position = new float3(-10, 0, -10);
			
                    NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
                    numericComponent.Set(NumericType.Speed, 6f); // 速度是6米每秒
                    numericComponent.Set(NumericType.AOI, 15000); // 视野15米
                    
                    unitComponent.Add(unit);
                    // 加入aoi
                    unit.AddComponent<AOIEntity, int, float3>(9 * 1000, unit.Position);
                    
                    unit.AddComponent<ET.Ability.TimelineComponent>();
                    unit.AddComponent<ET.Ability.BuffComponent>();
                    unit.AddComponent<ET.Ability.SkillComponent>();
                    unit.AddComponent<ET.Ability.BulletObj>();
                    unit.AddComponent<ET.Ability.EffectComponent>();
                    unit.AddComponent<ET.Ability.MoveComponent>();
                    unit.AddComponent<ET.Ability.RotateComponent>();
                    return unit;
                    break;
                }
                case UnitType.Aoe:
                {
                    Unit unit = unitComponent.AddChildWithId<Unit, int>(id, 1001);
                    unit.AddComponent<MoveComponent>();
                    unit.Position = new float3(-10, 0, -10);
			
                    NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
                    numericComponent.Set(NumericType.Speed, 6f); // 速度是6米每秒
                    numericComponent.Set(NumericType.AOI, 15000); // 视野15米
                    
                    unitComponent.Add(unit);
                    // 加入aoi
                    unit.AddComponent<AOIEntity, int, float3>(9 * 1000, unit.Position);
                    
                    unit.AddComponent<ET.Ability.TimelineComponent>();
                    unit.AddComponent<ET.Ability.BuffComponent>();
                    unit.AddComponent<ET.Ability.SkillComponent>();
                    unit.AddComponent<ET.Ability.AoeObj>();
                    unit.AddComponent<ET.Ability.EffectComponent>();
                    unit.AddComponent<ET.Ability.MoveComponent>();
                    unit.AddComponent<ET.Ability.RotateComponent>();
                    return unit;
                    break;
                }
                default:
                    throw new Exception($"not such unit type: {unitType}");
            }
        }
    }
}