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
            }
        }

        [ObjectSystem]
        public class SyncData_UnitNumericInfoDestroySystem: DestroySystem<SyncData_UnitNumericInfo>
        {
            protected override void Destroy(SyncData_UnitNumericInfo self)
            {
                self.unitId.Clear();
                self.KVCount.Clear();
                self.KVKey.Clear();
                self.KVValue.Clear();
                self.list.Clear();
            }
        }

        public static void Init(this SyncData_UnitNumericInfo self, HashSet<Unit> list)
        {
            self.unitId.Clear();
            self.KVCount.Clear();
            self.KVKey.Clear();
            self.KVValue.Clear();
            if (list == null)
            {
                return;
            }

            foreach (Unit unit in list)
            {
                if (unit == null)
                {
                    continue;
                }
                long unitId = unit.Id;
                NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
                if (numericComponent == null)
                {
                    continue;
                }
                self.unitId.Add(unitId);
                foreach ((int key, long value) in numericComponent.NumericDic)
                {
                    self.KVKey.Add(key);
                    self.KVValue.Add(value);
                }
                self.KVCount.Add(self.KVKey.Count);
            }
        }

        public static void Init(this SyncData_UnitNumericInfo self, Dictionary<Unit, HashSet<int>> list)
        {
            self.unitId.Clear();
            self.KVCount.Clear();
            self.KVKey.Clear();
            self.KVValue.Clear();
            if (list == null)
            {
                return;
            }
            foreach ((Unit unit, HashSet<int> keys) in list)
            {
                if (unit == null)
                {
                    continue;
                }
                long unitId = unit.Id;
                NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
                if (numericComponent == null)
                {
                    continue;
                }
                self.unitId.Add(unitId);
                int count = 0;
                foreach (int numericKey in keys)
                {
                    if (numericComponent.NumericDic.TryGetValue(numericKey, out long numericValue))
                    {
                        count++;
                        self.KVKey.Add(numericKey);
                        self.KVValue.Add(numericValue);
                    }
                }
                self.KVCount.Add(count);
            }
        }

        public static void DealByBytes(this SyncData_UnitNumericInfo self, UnitComponent unitComponent)
        {
            self.list.Clear();
            int index = 0;
            int count = self.unitId.Count;
            for (int i = 0; i < count; i++)
            {
                long unitId = self.unitId[i];
                Unit unit = unitComponent.Get(unitId);
                if (unit == null)
                {
                    continue;
                }

                int KVCount = self.KVCount[i];
                int indexBegin = index;
                index += KVCount;
                NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
                if (numericComponent == null)
                {
                    numericComponent = unit.AddComponent<NumericComponent>();
                }
                for (int j = indexBegin; j < index; j++)
                {
                    int key = self.KVKey[j];
                    long value = self.KVValue[j];
                    numericComponent.SetNoEvent(key, value);
                    if (key == ET.NumericType.Hp)
                    {
                        self.list.Add(unit);
                    }
                }
            }

            EventType.SyncHealthBar _SyncHealthBar = new ()
            {
                list = self.list,
            };
            EventSystem.Instance.Publish(unitComponent.DomainScene(), _SyncHealthBar);

        }

    }
}