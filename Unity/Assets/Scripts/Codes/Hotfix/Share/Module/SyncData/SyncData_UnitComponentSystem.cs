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
                self.unitComponents = new();
            }
        }

        [ObjectSystem]
        public class SyncData_UnitComponentDestroySystem: DestroySystem<SyncData_UnitComponent>
        {
            protected override void Destroy(SyncData_UnitComponent self)
            {
                self.unitComponents.Clear();
            }
        }

        public static void Init(this SyncData_UnitComponent self, Unit unit, HashSet<Type> types)
        {
            self.unitId = unit.Id;
            self.unitComponents.Clear();

            foreach (Type type in types)
            {
                Entity component = unit.GetComponent(type);
                if (component != null)
                {
                    self.unitComponents.Add(component.ToBson());
                }
            }
        }

        public static void DealByBytes(this SyncData_UnitComponent self, Unit unit)
        {
            foreach (var item in self.unitComponents)
            {
                Entity entity = MongoHelper.Deserialize<Entity>(item);
                System.Type type = entity.GetType();
                unit.RemoveComponent(type);
                unit.AddComponent(entity);
            }
        }
    }
}