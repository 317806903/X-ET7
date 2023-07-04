using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class EffectHelper
    {
        public static void AddEffect(Unit unit, ActionCfg_CreateEffect actionCfgCreateEffect, SelectHandle selectHandle)
        {
            EffectComponent effectComponent;
            bool isSceneEffect = actionCfgCreateEffect.IsSceneEffect;
            
            // if (selectHandle.selectHandleType == SelectHandleType.SelectUnits)
            // {
            //     isSceneEffect = false;
            // }
            // else if (selectHandle.selectHandleType == SelectHandleType.SelectPosition)
            // {
            //     isSceneEffect = true;
            // }
            
            if (isSceneEffect == false)
            {
                foreach (var unitId in selectHandle.unitIds)
                {
                    Unit unitEffect = UnitHelper.GetUnit(unit.DomainScene(), unitId);
                    effectComponent = unitEffect.GetComponent<EffectComponent>();
                    if (effectComponent == null)
                    {
                        effectComponent = unitEffect.AddComponent<EffectComponent>();
                    }
                    EffectObj effectObj = effectComponent.AddEffect(unitEffect.Id, actionCfgCreateEffect.Key, actionCfgCreateEffect.ResId, actionCfgCreateEffect.Duration, 
                    actionCfgCreateEffect.OffSetInfo);
                    if (effectObj != null)
                    {
                        EventType.SyncUnitEffects _SyncUnitEffects = new ()
                        {
                            unit = unitEffect,
                            isAddEffect = true,
                            effectObj = effectObj,
                        };
                        EventSystem.Instance.Publish(unit.DomainScene(), _SyncUnitEffects);
                    }
                }
            }
            else
            {
                foreach (var unitId in selectHandle.unitIds)
                {
                    Unit unitEffect = UnitHelper.GetUnit(unit.DomainScene(), unitId);
                    if (unitEffect == null)
                    {
                        continue;
                    }

                    float3 position = unitEffect.Position;
                    float3 forward = unitEffect.Forward;
                    Unit unitSceneEffect = UnitHelper_Create.CreateWhenServer_SceneEffect(unit.DomainScene(), position, forward);
                    effectComponent = unitSceneEffect.GetComponent<EffectComponent>();
                
                    EffectObj effectObj = effectComponent.AddEffect(unitSceneEffect.Id, actionCfgCreateEffect.Key, actionCfgCreateEffect.ResId, actionCfgCreateEffect.Duration, actionCfgCreateEffect.OffSetInfo);
                    if (effectObj != null)
                    {
                        EventType.SyncUnitEffects _SyncUnitEffects = new ()
                        {
                            unit = unitSceneEffect,
                            isAddEffect = true,
                            effectObj = effectObj,
                        };
                        EventSystem.Instance.Publish(unit.DomainScene(), _SyncUnitEffects);
                    }
                }

            }

        }
        
        public static void RemoveEffect(Unit unit, ActionCfg_RemoveEffect actionCfg_RemoveEffect)
        {
            EffectComponent effectComponent = unit.GetComponent<EffectComponent>();
            effectComponent.RemoveEffectByKey(actionCfg_RemoveEffect.Key);
        }

    }
}