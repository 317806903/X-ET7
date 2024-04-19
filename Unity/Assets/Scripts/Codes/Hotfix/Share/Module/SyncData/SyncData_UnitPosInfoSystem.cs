using System.Collections.Generic;
using System.Diagnostics;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
    public static class SyncData_UnitPosInfoSystem
    {
        public class AwakeSystem: AwakeSystem<SyncData_UnitPosInfo>
        {
            protected override void Awake(SyncData_UnitPosInfo self)
            {
            }
        }

        [ObjectSystem]
        public class SyncData_UnitPosInfoDestroySystem: DestroySystem<SyncData_UnitPosInfo>
        {
            protected override void Destroy(SyncData_UnitPosInfo self)
            {
            }
        }

        public static void Init(this SyncData_UnitPosInfo self, Unit unit)
        {
            self.unitId = unit.Id;
            self.posX = (int)(unit.Position.x * 1000);
            self.posY = (int)(unit.Position.y * 1000);
            self.posZ = (int)(unit.Position.z * 1000);
            self.forwardX = (int)(unit.Forward.x * 1000);
            self.forwardY = (int)(unit.Forward.y * 1000);
            self.forwardZ = (int)(unit.Forward.z * 1000);
        }

        public static void DealByBytes(this SyncData_UnitPosInfo self, Unit unit)
        {
            unit.Position = self.GetPos();
            unit.Forward = self.GetForward();
        }

        public static float3 GetPos(this SyncData_UnitPosInfo self)
        {
            return new float3(self.posX * 0.001f, self.posY * 0.001f, self.posZ * 0.001f);
        }

        public static float3 GetForward(this SyncData_UnitPosInfo self)
        {
            return new float3(self.forwardX * 0.001f, self.forwardY * 0.001f, self.forwardZ * 0.001f);
        }
    }
}