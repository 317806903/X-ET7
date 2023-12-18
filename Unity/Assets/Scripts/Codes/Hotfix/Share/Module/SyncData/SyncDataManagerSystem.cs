using System.Collections.Generic;
using System.Diagnostics;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
    [ComponentOf(typeof (SyncDataManager))]
    public static class SyncDataManagerSystem
    {

        public class AwakeSystem: AwakeSystem<SyncDataManager>
        {
            protected override void Awake(SyncDataManager self)
            {
            }
        }

        [ObjectSystem]
        public class SyncDataManagerDestroySystem: DestroySystem<SyncDataManager>
        {
            protected override void Destroy(SyncDataManager self)
            {
            }
        }

        [ObjectSystem]
        public class SyncDataManagerFixedUpdateSystem: FixedUpdateSystem<SyncDataManager>
        {
            protected override void FixedUpdate(SyncDataManager self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static void FixedUpdate(this SyncDataManager self, float fixedDeltaTime)
        {
        }

        public static void AddSyncData_UnitPosInfo(this SyncDataManager self, Unit unit)
        {
            self.AddChild<SyncData_UnitPosInfo>(true).Init(unit);
        }

        public static void AddSyncData_UnitNumericInfo(this SyncDataManager self, Unit unit)
        {
            self.AddChild<SyncData_UnitNumericInfo>(true).Init(unit);
        }

    }
}