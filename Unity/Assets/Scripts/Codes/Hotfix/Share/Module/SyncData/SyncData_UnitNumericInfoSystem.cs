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
            }
        }

        public static void Init(this SyncData_UnitNumericInfo self, Unit unit)
        {
            self.unitId = unit.Id;
            self.KV.Clear();
        }

    }
}