using System;
using System.Collections.Generic;
using System.Linq;
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
        
        public static Unit CreateWhenServer(Scene scene, long id, UnitType unitType)
        {
            UnitComponent unitComponent = GetUnitComponent(scene);
            
            switch (unitType)
            {
                case UnitType.Player:
                {
                    Unit unit = unitComponent.AddChildWithId<Unit, string>(id, "Unit_1");
                    unit.AddComponent<MoveByPathComponent>();
                    unit.Position = new float3(-10, 0, -10);
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
                    return unit;
                    break;
                }
                case UnitType.Monster:
                {
                    Unit unit = unitComponent.AddChildWithId<Unit, string>(id, "Unit_1");
                    unit.AddComponent<MoveByPathComponent>();
                    unit.Position = new float3(-10, 0, -10);
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
                    return unit;
                    break;
                }
                case UnitType.NPC:
                {
                    break;
                }
                case UnitType.SceneObj:
                {
                    break;
                }
                case UnitType.Bullet:
                {
                    Unit unit = unitComponent.AddChildWithId<Unit, string>(id, "Unit_1");
                    unit.AddComponent<MoveByPathComponent>();
                    unit.Position = new float3(-10, 0, -10);
                    unit.Type = unitType;
			
                    NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
                    numericComponent.Set(NumericType.Speed, 6f); // 速度是6米每秒
                    numericComponent.Set(NumericType.AOI, 15000); // 视野15米
                    
                    unitComponent.Add(unit);
                    
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
                    break;
                }
                default:
                    throw new Exception($"not such unit type: {unitType}");
            }

            return null;
        }

        public static Unit CreateWhenClient(Scene scene, long id, UnitType unitType)
        {
            UnitComponent unitComponent = scene.GetComponent<UnitComponent>();
            Unit unit = unitComponent.AddChildWithId<Unit>(id);
            unit.Type = unitType;
            unitComponent.Add(unit);
            return unit;
        }
    }
}