using System;
using System.Collections.Generic;
using System.Numerics;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (SceneEffectComponent))]
    [FriendOf(typeof (EffectObj))]
    public static class SceneEffectComponentSystem
    {
        [ObjectSystem]
        public class SceneEffectComponentAwakeSystem: AwakeSystem<SceneEffectComponent>
        {
            protected override void Awake(SceneEffectComponent self)
            {
                self.removeList = new();
                self.recordEffectList = new();
            }
        }

        [ObjectSystem]
        public class SceneEffectComponentDestroySystem: DestroySystem<SceneEffectComponent>
        {
            protected override void Destroy(SceneEffectComponent self)
            {
                self.removeList.Clear();
                self.recordEffectList.Clear();
            }
        }

        [ObjectSystem]
        public class SceneEffectComponentFixedUpdateSystem: FixedUpdateSystem<SceneEffectComponent>
        {
            protected override void FixedUpdate(SceneEffectComponent self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static void RecordEffect(this SceneEffectComponent self, Unit unitEffect, string key, EffectObj effectObj)
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }

            if (self.recordEffectList.TryGetValue(unitEffect.Id, out var recordKey2Obj) == false)
            {
                recordKey2Obj = new();
                self.recordEffectList.Add(unitEffect.Id, recordKey2Obj);
            }
            recordKey2Obj.Add(key, effectObj);

        }

        public static bool ChkCanAddEffect(this SceneEffectComponent self, Unit unitEffect, string key, int maxKeyNum)
        {
            if (self.recordEffectList.TryGetValue(unitEffect.Id, out var recordKey2Obj) == false)
            {
                return true;
            }

            if (recordKey2Obj.ContainsKey(key) && recordKey2Obj[key].Count >= maxKeyNum)
            {
                return false;
            }

            return true;
        }

        public static void RemoveEffectByKey(this SceneEffectComponent self, Unit unitEffect, string key)
        {
            if (self.recordEffectList.TryGetValue(unitEffect.Id, out var recordKey2Obj) == false)
            {
                return;
            }

            if (recordKey2Obj.ContainsKey(key) == false)
            {
                return;
            }
            foreach (EffectObj effectObj in recordKey2Obj[key])
            {
                effectObj?.WillDestroy();
            }
            recordKey2Obj.Remove(key);
        }

        public static void FixedUpdate(this SceneEffectComponent self, float fixedDeltaTime)
        {
            foreach (var tmp1 in self.recordEffectList)
            {
                long unitId = tmp1.Key;
                Unit unitEffect = UnitHelper.GetUnit(self.DomainScene(), unitId);
                if (UnitHelper.ChkUnitAlive(unitEffect) == false)
                {
                    self.removeList.Add(unitId);
                }

                var tmp2 = tmp1.Value;
                foreach (var tmp21 in tmp2.Keys)
                {
                    List<EntityRef<EffectObj>> effectObjRefList = tmp2[tmp21];
                    for (int i = effectObjRefList.Count-1; i >= 0; i--)
                    {
                        EffectObj effectObj = effectObjRefList[i];
                        if (effectObj == null)
                        {
                            effectObjRefList.RemoveAt(i);
                        }
                    }
                }
            }
            int count = self.removeList.Count;
            for (int i = 0; i < count; i++)
            {
                long unitId = self.removeList[i];
                self.recordEffectList.Remove(unitId);
            }

            self.removeList.Clear();
        }
    }
}