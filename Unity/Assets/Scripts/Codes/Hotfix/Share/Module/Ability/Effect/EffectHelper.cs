using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (Unit))]
    public static class EffectHelper
    {
        public static void AddEffect(Unit unit, ActionCfg_EffectCreate actionCfgCreateEffect, SelectHandle selectHandle)
        {
            if (actionCfgCreateEffect.Duration <= 0 && actionCfgCreateEffect.Duration != -1)
            {
                Log.Error(
                    $"ET.Ability.EffectHelper.AddEffect [{actionCfgCreateEffect.Id}] Duration[{actionCfgCreateEffect.Duration}] <= 0 && Duration != -1");
                return;
            }

            if (selectHandle.selectHandleType == SelectHandleType.SelectUnits)
            {
                AddEffectWhenSelectUnits(unit, actionCfgCreateEffect, selectHandle);
            }
            else if (selectHandle.selectHandleType == SelectHandleType.SelectPosition)
            {
                AddEffectWhenSelectPosition(unit, actionCfgCreateEffect, selectHandle);
            }
        }

        public static void AddEffectWhenSelectUnits(Unit unit, ActionCfg_EffectCreate actionCfgCreateEffect, SelectHandle selectHandle)
        {
            EffectComponent effectComponent;
            bool isSceneEffect = actionCfgCreateEffect.IsSceneEffect;
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

                    EffectObj effectObj = effectComponent.AddEffect(unitEffect.Id, actionCfgCreateEffect.Key, actionCfgCreateEffect.MaxKeyNum,
                        actionCfgCreateEffect.ResId, actionCfgCreateEffect.Duration,
                        actionCfgCreateEffect.OffSetInfo);
                    if (effectObj != null)
                    {
                        EventType.SyncUnitEffects _SyncUnitEffects = new() { unit = unitEffect, isAddEffect = true, effectObj = effectObj, };
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
                    Unit unitSceneEffect = ET.GamePlayHelper.CreateSceneEffect(unit.DomainScene(), position, forward);
                    effectComponent = unitSceneEffect.GetComponent<EffectComponent>();

                    EffectObj effectObj = effectComponent.AddEffect(unitSceneEffect.Id, actionCfgCreateEffect.Key, actionCfgCreateEffect.MaxKeyNum,
                        actionCfgCreateEffect.ResId, actionCfgCreateEffect.Duration, actionCfgCreateEffect.OffSetInfo);
                    if (effectObj != null)
                    {
                        EventType.SyncUnitEffects _SyncUnitEffects = new() { unit = unitSceneEffect, isAddEffect = true, effectObj = effectObj, };
                        EventSystem.Instance.Publish(unit.DomainScene(), _SyncUnitEffects);
                    }
                }
            }
        }

        public static void AddEffectWhenSelectPosition(Unit unit, ActionCfg_EffectCreate actionCfgCreateEffect, SelectHandle selectHandle)
        {
            EffectComponent effectComponent;
            float3 position = selectHandle.position;
            float3 forward = new float3(0, 0, 1);
            Unit unitSceneEffect = ET.GamePlayHelper.CreateSceneEffect(unit.DomainScene(), position, forward);
            effectComponent = unitSceneEffect.GetComponent<EffectComponent>();

            EffectObj effectObj = effectComponent.AddEffect(unitSceneEffect.Id, actionCfgCreateEffect.Key, actionCfgCreateEffect.MaxKeyNum,
                actionCfgCreateEffect.ResId, actionCfgCreateEffect.Duration, actionCfgCreateEffect.OffSetInfo);
            if (effectObj != null)
            {
                EventType.SyncUnitEffects _SyncUnitEffects = new() { unit = unitSceneEffect, isAddEffect = true, effectObj = effectObj, };
                EventSystem.Instance.Publish(unit.DomainScene(), _SyncUnitEffects);
            }
        }
        
        public static void RemoveEffect(Unit unit, ActionCfg_EffectRemove actionCfg_RemoveEffect)
        {
            EffectComponent effectComponent = unit.GetComponent<EffectComponent>();
            effectComponent.RemoveEffectByKey(actionCfg_RemoveEffect.Key);
        }
    }
}