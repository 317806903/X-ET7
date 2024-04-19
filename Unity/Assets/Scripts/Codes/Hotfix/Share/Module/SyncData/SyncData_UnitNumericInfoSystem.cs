using System.Collections.Generic;
using System.Diagnostics;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
    public static class SyncData_UnitNumericInfoSystem
    {

        public class AwakeSystem: AwakeSystem<SyncData_UnitNumericInfo>
        {
            protected override void Awake(SyncData_UnitNumericInfo self)
            {
                self.KV = new();
            }
        }

        [ObjectSystem]
        public class SyncData_UnitNumericInfoDestroySystem: DestroySystem<SyncData_UnitNumericInfo>
        {
            protected override void Destroy(SyncData_UnitNumericInfo self)
            {
                self.KV.Clear();
            }
        }

        public static void Init(this SyncData_UnitNumericInfo self, Unit unit)
        {
            self.unitId = unit.Id;
            self.KV.Clear();

            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
            foreach ((int key, long value) in numericComponent.NumericDic)
            {
                self.KV.Add(key, value);
            }
        }

        public static void Init(this SyncData_UnitNumericInfo self, Unit unit, HashSet<int> keys)
        {
            self.unitId = unit.Id;
            self.KV.Clear();

            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
            foreach (int numericKey in keys)
            {
                if (numericComponent.NumericDic.TryGetValue(numericKey, out long numericValue))
                {
                    self.KV.Add(numericKey, numericValue);
                }
            }
        }

        public static void DealByBytes(this SyncData_UnitNumericInfo self, Unit unit)
        {
            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
            if (numericComponent == null)
            {
                numericComponent = unit.AddComponent<NumericComponent>();
            }
            foreach (var kv in self.KV)
            {
                numericComponent.SetAsLong(kv.Key, kv.Value);
            }
        }

    }
}