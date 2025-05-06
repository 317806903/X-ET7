using System;
using System.Collections.Generic;
using System.Diagnostics;
using ET.Ability;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
    public static class SyncData_UnitEffectsSystem
    {

        public class AwakeSystem: AwakeSystem<SyncData_UnitEffects>
        {
            protected override void Awake(SyncData_UnitEffects self)
            {
            }
        }

        [ObjectSystem]
        public class SyncData_UnitEffectsDestroySystem: DestroySystem<SyncData_UnitEffects>
        {
            protected override void Destroy(SyncData_UnitEffects self)
            {
                self.unitId.Clear();
                self.isAddOrRemove.Clear();
                self.unitEffectObjIds.Clear();
                self.unitEffectComponents.Clear();
            }
        }

        public static void Init(this SyncData_UnitEffects self, List<(Unit unit, long effectObjId, bool isAddOrRemove, byte[] unitEffectComponent)> list)
        {
            self.unitId.Clear();
            self.isAddOrRemove.Clear();
            self.unitEffectObjIds.Clear();
            self.unitEffectComponents.Clear();
            if (list == null)
            {
                return;
            }
            foreach ((Unit unit, long effectObjId, bool isAddOrRemove, byte[] unitEffectComponent) in list)
            {
                if (unit == null)
                {
                    continue;
                }
                long unitId = unit.Id;
                self.unitId.Add(unitId);
                self.isAddOrRemove.Add(isAddOrRemove);
                self.unitEffectObjIds.Add(effectObjId);
                self.unitEffectComponents.Add(unitEffectComponent);
            }
        }

        public static async ETTask DealByBytes(this SyncData_UnitEffects self, UnitComponent unitComponent)
        {
            int count = self.unitId.Count;
            for (int i = 0; i < count; i++)
            {
                long unitId = self.unitId[i];
                long effectObjId = self.unitEffectObjIds[i];
                bool isAddOrRemove = self.isAddOrRemove[i];
                byte[] unitEffectComponent = self.unitEffectComponents[i];
                self.DealOneUnit(unitComponent, unitId, effectObjId, isAddOrRemove, unitEffectComponent).Coroutine();
            }
            await ETTask.CompletedTask;
        }

        public static async ETTask DealOneUnit(this SyncData_UnitEffects self, UnitComponent unitComponent, long unitId, long effectObjId, bool isAddOrRemove, byte[] unitEffectComponent)
        {
            Unit unit = unitComponent.Get(unitId);
            int retryNum = 30;
            while (unit == null)
            {
                await TimerComponent.Instance.WaitFrameAsync();
                if (unitComponent.IsDisposed)
                {
                    return;
                }
                unit = unitComponent.Get(unitId);
                if (retryNum-- < 0)
                {
                    //Log.Debug($"--zpb 8800 Client unitId[{unitId}] effectObjId[{effectObjId}] isAddOrRemove[{isAddOrRemove}] retryNum-- < 0");
                    return;
                }
            }

            EffectComponent effectComponent = unit.GetComponent<EffectComponent>();
            if (effectComponent == null)
            {
                effectComponent = unit.AddComponent<EffectComponent>();
            }
            if (isAddOrRemove)
            {
                byte[] component = unitEffectComponent;
                EffectObj entity = MongoHelper.Deserialize<EffectObj>(component);
                if (entity == null)
                {
                    return;
                }

                if (effectComponent.GetChild<EffectObj>(entity.Id) != null)
                {
                    //entity.Dispose();
                    effectComponent.RemoveChild(entity.Id);
                }
                effectComponent.AddChild(entity);
            }
            else
            {
                effectComponent.RemoveChild(effectObjId);
            }

            EffectShowChgComponent effectShowChgComponent = unit.GetComponent<EffectShowChgComponent>();
            if (effectShowChgComponent == null)
            {
                effectShowChgComponent = unit.AddComponent<EffectShowChgComponent>();
            }
            effectShowChgComponent.chgEffectList.Add(effectObjId);

            await ETTask.CompletedTask;
        }
    }
}