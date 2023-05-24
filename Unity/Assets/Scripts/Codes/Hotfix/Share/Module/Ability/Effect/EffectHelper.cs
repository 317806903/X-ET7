using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class EffectHelper
    {
        public static void AddEffect(Unit unit, ActionCfg_CreateEffect actionCfgCreateEffect, SelectHandle selectHandle)
        {
            EffectComponent effectComponent;
            bool isSceneEffect = actionCfgCreateEffect.IsSceneEffect;
            
            if (selectHandle.selectHandleType == SelectHandleType.SelectUnits)
            {
                isSceneEffect = false;
            }
            else if (selectHandle.selectHandleType == SelectHandleType.SelectPosition)
            {
                isSceneEffect = true;
            }
            
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
                    actionCfgCreateEffect.NodeName, actionCfgCreateEffect.OffSetPosition, 
                        actionCfgCreateEffect.RelateForward);
                    EventSystem.Instance.Invoke<SyncUnitEffects>(new SyncUnitEffects(){
                        unit = unitEffect,
                        isSceneEffect = false,
                        isAddEffect = true,
                        effectObj = effectObj,
                    });

                }
            }
            else
            {
                effectComponent = unit.DomainScene().GetComponent<EffectComponent>();
                
                EffectObj effectObj = effectComponent.AddEffect(0, actionCfgCreateEffect.Key, actionCfgCreateEffect.ResId, actionCfgCreateEffect.Duration, actionCfgCreateEffect.NodeName, actionCfgCreateEffect.OffSetPosition, 
                    actionCfgCreateEffect.RelateForward);
                EventSystem.Instance.Invoke<SyncUnitEffects>(new SyncUnitEffects(){
                    unit = unit,
                    isSceneEffect = true,
                    isAddEffect = true,
                    effectObj = effectObj,
                });

            }

        }
    }
}