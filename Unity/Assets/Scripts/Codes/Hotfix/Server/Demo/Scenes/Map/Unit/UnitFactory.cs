using System;
using Unity.Mathematics;

namespace ET.Server
{
    public static class UnitFactory
    {
        public static Unit Create(Scene scene, long id, UnitType unitType, float3 position)
        {
            Unit unit = ET.Ability.UnitHelper_Create.CreateWhenServer(scene, id, unitType, position);
            
            UnitComponent unitComponent = scene.GetComponent<UnitComponent>();
            switch (unitType)
            {
                case UnitType.Player:
                {
                    // 加入aoi
                    unit.AddComponent<AOIEntity, int, float3>(9 * 1000, unit.Position);
                    
                    return unit;
                    break;
                }
                case UnitType.Monster:
                {
                    // 加入aoi
                    unit.AddComponent<AOIEntity, int, float3>(9 * 1000, unit.Position);
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
                    // 加入aoi
                    unit.AddComponent<AOIEntity, int, float3>(9 * 1000, unit.Position);
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
    }
}