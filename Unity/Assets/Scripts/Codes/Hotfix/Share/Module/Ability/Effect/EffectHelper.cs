using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (Unit))]
    public static class EffectHelper
    {
        public static async ETTask AddEffect(Unit unit, ActionCfg_EffectCreate actionCfgCreateEffect, SelectHandle selectHandle,
        ActionContext actionContext)
        {
            if (actionCfgCreateEffect.Duration <= 0 && actionCfgCreateEffect.Duration != -1)
            {
                Log.Error(
                    $"ET.Ability.EffectHelper.AddEffect [{actionCfgCreateEffect.Id}] Duration[{actionCfgCreateEffect.Duration}] <= 0 && Duration != -1");
                return;
            }

            if (selectHandle.selectHandleType == SelectHandleType.SelectUnits)
            {
                await AddEffectWhenSelectUnits(unit, actionCfgCreateEffect, selectHandle, actionContext);
            }
            else
            {
                await AddEffectWhenSelectPosition(unit, actionCfgCreateEffect, selectHandle, actionContext);
            }
        }

        public static async ETTask AddEffectWhenSelectUnits(Unit unit, ActionCfg_EffectCreate actionCfgCreateEffect, SelectHandle selectHandle, ActionContext actionContext)
        {
            EffectComponent effectComponent;
            bool isSceneEffect = actionCfgCreateEffect.IsSceneEffect;
            bool IsScaleByUnit = actionCfgCreateEffect.IsScaleByUnit;
            if (isSceneEffect == false)
            {
                long effectCasterUnitId = unit.Id;
                if (UnitHelper.ChkIsBullet(unit))
                {
                    effectCasterUnitId = unit.GetCaster().Id;
                }
                foreach (var unitId in selectHandle.unitIds)
                {
                    Unit unitEffect = UnitHelper.GetUnit(unit.DomainScene(), unitId);
                    if (UnitHelper.ChkUnitAlive(unitEffect, true) == false)
                    {
                        continue;
                    }

                    bool canAddEffect = ChkCanAddEffect(false, unitEffect, actionCfgCreateEffect);
                    if (canAddEffect == false)
                    {
                        continue;
                    }

                    effectComponent = unitEffect.GetComponent<EffectComponent>();
                    if (effectComponent == null)
                    {
                        effectComponent = unitEffect.AddComponent<EffectComponent>();
                    }

                    EffectObj effectObj = effectComponent.AddEffect(effectCasterUnitId, actionCfgCreateEffect, actionCfgCreateEffect.IsScaleByUnit);
                    if (effectObj != null)
                    {
                        if (actionCfgCreateEffect.EffectShowType == EffectShowType.Send2Receive2Receive)
                        {
                            effectCasterUnitId = unitEffect.Id;
                        }

                        ET.Ability.UnitHelper.AddSyncData_UnitEffects(unitEffect, effectObj.Id, actionCfgCreateEffect.IsOnlySelfShow);
                    }

                    while (IdGenerater.Instance.ChkGenerateIdFull())
                    {
                        await TimerComponent.Instance.WaitFrameAsync();
                    }
                }
            }
            else
            {
                long effectCasterUnitId = unit.Id;
                if (UnitHelper.ChkIsBullet(unit))
                {
                    effectCasterUnitId = unit.GetCaster().Id;
                }
                foreach (var unitId in selectHandle.unitIds)
                {
                    Unit unitEffect = UnitHelper.GetUnit(unit.DomainScene(), unitId);
                    if (UnitHelper.ChkUnitAlive(unitEffect, true) == false)
                    {
                        continue;
                    }

                    bool canAddEffect = ChkCanAddEffect(true, unitEffect, actionCfgCreateEffect);
                    if (canAddEffect == false)
                    {
                        continue;
                    }

                    float3 position = unitEffect.Position;
                    float3 forward = unitEffect.Forward;
                    Unit unitSceneEffect = ET.GamePlayHelper.CreateSceneEffect(unit.DomainScene(), position, forward);
                    effectComponent = unitSceneEffect.GetComponent<EffectComponent>();
                    if (effectComponent == null)
                    {
                        effectComponent = unitSceneEffect.AddComponent<EffectComponent>();
                    }

                    EffectObj effectObj = effectComponent.AddEffect(effectCasterUnitId, actionCfgCreateEffect, false);
                    if (effectObj != null)
                    {
                        if (actionCfgCreateEffect.EffectShowType == EffectShowType.Send2Receive2Receive)
                        {
                            effectCasterUnitId = unitEffect.Id;
                        }
                        SceneEffectComponent sceneEffectComponent = unitEffect.DomainScene().GetComponent<SceneEffectComponent>();
                        sceneEffectComponent.RecordEffect(unitEffect, actionCfgCreateEffect.Key, effectObj);

                        ET.Ability.UnitHelper.AddSyncData_UnitEffects(unitSceneEffect, effectObj.Id, actionCfgCreateEffect.IsOnlySelfShow);
                    }

                    while (IdGenerater.Instance.ChkGenerateIdFull())
                    {
                        await TimerComponent.Instance.WaitFrameAsync();
                    }
                }
            }
        }

        public static async ETTask AddEffectWhenSelectPosition(Unit unit, ActionCfg_EffectCreate actionCfgCreateEffect, SelectHandle selectHandle, ActionContext actionContext)
        {
            bool canAddEffect = ChkCanAddEffect(true, unit, actionCfgCreateEffect);
            if (canAddEffect == false)
            {
                return;
            }

            long effectCasterUnitId = unit.Id;
            if (UnitHelper.ChkIsBullet(unit))
            {
                effectCasterUnitId = unit.GetCaster().Id;
            }
            float3 position = selectHandle.position;
            float3 forward = new float3(0, 0, 1);
            if (selectHandle.selectHandleType == SelectHandleType.SelectDirection)
            {
                forward = selectHandle.direction;
            }
            Unit unitSceneEffect = ET.GamePlayHelper.CreateSceneEffect(unit.DomainScene(), position, forward);
            EffectComponent effectComponent = unitSceneEffect.GetComponent<EffectComponent>();
            if (effectComponent == null)
            {
                effectComponent = unitSceneEffect.AddComponent<EffectComponent>();
            }

            EffectObj effectObj = effectComponent.AddEffect(effectCasterUnitId, actionCfgCreateEffect, false);
            if (effectObj != null)
            {
                SceneEffectComponent sceneEffectComponent = unit.DomainScene().GetComponent<SceneEffectComponent>();
                sceneEffectComponent.RecordEffect(unit, actionCfgCreateEffect.Key, effectObj);

                ET.Ability.UnitHelper.AddSyncData_UnitEffects(unitSceneEffect, effectObj.Id, actionCfgCreateEffect.IsOnlySelfShow);
            }

            await ETTask.CompletedTask;
        }

        public static void RemoveEffect(Unit unit, ActionCfg_EffectRemove actionCfg_RemoveEffect)
        {
            EffectComponent effectComponent = unit.GetComponent<EffectComponent>();
            effectComponent.RemoveEffectByKey(actionCfg_RemoveEffect.Key);

            SceneEffectComponent sceneEffectComponent = unit.DomainScene().GetComponent<SceneEffectComponent>();
            sceneEffectComponent.RemoveEffectByKey(unit, actionCfg_RemoveEffect.Key);
        }

        public static bool ChkCanAddEffect(bool isSceneEffect, Unit unitEffect, ActionCfg_EffectCreate actionCfgCreateEffect)
        {
            string key = actionCfgCreateEffect.Key;
            int maxKeyNum = actionCfgCreateEffect.MaxKeyNum;
            if (string.IsNullOrEmpty(key))
            {
                return true;
            }

            if (maxKeyNum == -1)
            {
                return true;
            }

            if (maxKeyNum <= 0)
            {
                return false;
            }

            if (isSceneEffect)
            {
                SceneEffectComponent sceneEffectComponent = unitEffect.DomainScene().GetComponent<SceneEffectComponent>();
                return sceneEffectComponent.ChkCanAddEffect(unitEffect, key, maxKeyNum);
            }
            else
            {
                EffectComponent effectComponent = unitEffect.GetComponent<EffectComponent>();
                if (effectComponent == null)
                {
                    return true;
                }

                return effectComponent.ChkCanAddEffect(unitEffect, key, maxKeyNum);
            }
        }

        public static EffectObj AddSelfEffect(Unit unit, ActionCfg_EffectCreate actionCfgCreateEffect)
        {
            if (actionCfgCreateEffect.Duration <= 0 && actionCfgCreateEffect.Duration != -1)
            {
                Log.Error($"ET.Ability.EffectHelper.AddEffect [{actionCfgCreateEffect.Id}] Duration[{actionCfgCreateEffect.Duration}] <= 0 && Duration != -1");
                return null;
            }

            Unit unitEffect = unit;
            bool canAddEffect = ChkCanAddEffect(false, unitEffect, actionCfgCreateEffect);
            if (canAddEffect == false)
            {
                return null;
            }

            EffectComponent effectComponent = unitEffect.GetComponent<EffectComponent>();
            if (effectComponent == null)
            {
                effectComponent = unitEffect.AddComponent<EffectComponent>();
            }

            EffectObj effectObj = effectComponent.AddEffect(unit.Id, actionCfgCreateEffect, actionCfgCreateEffect.IsScaleByUnit);
            if (effectObj != null)
            {
                ET.Ability.UnitHelper.AddSyncData_UnitEffects(unitEffect, effectObj.Id, actionCfgCreateEffect.IsOnlySelfShow);
            }

            return effectObj;
        }

        public static List<EffectObj> GetEffectsByEffectShowType(Unit unit, EffectShowType effectShowType)
        {
            EffectComponent effectComponent = unit.GetComponent<EffectComponent>();
            if (effectComponent == null)
            {
                return null;
            }

            List<long> effectObjList = effectComponent.GetEffectObjIdsByEffectShowType(effectShowType);
            if (effectObjList == null || effectObjList.Count == 0)
            {
                return null;
            }

            List<EffectObj> effectList = ListComponent<EffectObj>.Create();
            foreach (long effectObjId in effectObjList)
            {
                EffectObj effectObj = effectComponent.GetChild<EffectObj>(effectObjId);
                effectList.Add(effectObj);
            }
            return effectList;
        }

        public static EffectObj GetEffectObj(Unit unit, long effectObjId)
        {
            if (unit == null)
            {
                return null;
            }
            EffectComponent effectComponent = unit.GetComponent<EffectComponent>();
            if (effectComponent == null)
            {
                return null;
            }

            return effectComponent.GetEffectObj(effectObjId);
        }
    }
}