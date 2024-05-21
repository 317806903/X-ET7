using System;
using System.Collections.Generic;
using System.Diagnostics;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
    public static class SyncData_UnitComponentSystem
    {

        public class AwakeSystem: AwakeSystem<SyncData_UnitComponent>
        {
            protected override void Awake(SyncData_UnitComponent self)
            {
                self.unitId = new();
                self.unitComponentCount = new();
                self.unitComponents = new();
            }
        }

        [ObjectSystem]
        public class SyncData_UnitComponentDestroySystem: DestroySystem<SyncData_UnitComponent>
        {
            protected override void Destroy(SyncData_UnitComponent self)
            {
                self.unitId.Clear();
                self.unitComponentCount.Clear();
                self.unitComponents.Clear();
            }
        }

        public static void Init(this SyncData_UnitComponent self, List<(Unit unit, HashSet<Type> types)> list)
        {
            self.unitId.Clear();
            self.unitComponentCount.Clear();
            self.unitComponents.Clear();
            if (list == null)
            {
                return;
            }
            foreach ((Unit unit, HashSet<Type> types) in list)
            {
                long unitId = unit.Id;
                int count = 0;
                foreach (Type type in types)
                {
                    Entity component = unit.GetComponent(type);
                    if (component != null)
                    {
                        count++;
                        self.unitComponents.Add(component.ToBson());
                    }
                }
                self.unitId.Add(unitId);
                self.unitComponentCount.Add(count);
            }
        }

        public static async ETTask DealByBytes(this SyncData_UnitComponent self, UnitComponent unitComponent)
        {
            int index = 0;
            int count = self.unitId.Count;
            for (int i = 0; i < count; i++)
            {
                long unitId = self.unitId[i];
                int unitComponentCount = self.unitComponentCount[i];
                int indexBegin = index;
                index += unitComponentCount;
                Unit unit = unitComponent.Get(unitId);
                if (unit == null)
                {
                    continue;
                }

                for (int j = indexBegin; j < index; j++)
                {
                    byte[] component = self.unitComponents[j];
                    Entity entity = MongoHelper.Deserialize<Entity>(component);
                    System.Type type = entity.GetType();
                    unit.RemoveComponent(type);
                    unit.AddComponent(entity);
                }
            }
            await ETTask.CompletedTask;
        }
    }
}