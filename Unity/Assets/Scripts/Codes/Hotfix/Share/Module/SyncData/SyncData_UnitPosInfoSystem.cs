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
                self.unitId = new();
                self.posX = new();
                self.posY = new();
                self.posZ = new();
                self.rotationX = new();
                self.rotationY = new();
                self.rotationZ = new();
                self.rotationW = new();
            }
        }

        [ObjectSystem]
        public class SyncData_UnitPosInfoDestroySystem: DestroySystem<SyncData_UnitPosInfo>
        {
            protected override void Destroy(SyncData_UnitPosInfo self)
            {
                self.unitId.Clear();
                self.posX.Clear();
                self.posY.Clear();
                self.posZ.Clear();
                self.rotationX.Clear();
                self.rotationY.Clear();
                self.rotationZ.Clear();
                self.rotationW.Clear();
            }
        }

        public static void Init(this SyncData_UnitPosInfo self, HashSet<Unit> list)
        {
            self.unitId.Clear();
            self.posX.Clear();
            self.posY.Clear();
            self.posZ.Clear();
            self.rotationX.Clear();
            self.rotationY.Clear();
            self.rotationZ.Clear();
            self.rotationW.Clear();
            if (list == null)
            {
                return;
            }
            self.serverTime = TimeHelper.ServerNow();
            foreach (var unit in list)
            {
                self.unitId.Add(unit.Id);
                self.posX.Add((int)(unit.Position.x * 1000));
                self.posY.Add((int)(unit.Position.y * 1000));
                self.posZ.Add((int)(unit.Position.z * 1000));
                self.rotationX.Add((int)(unit.Rotation.value.x * 1000));
                self.rotationY.Add((int)(unit.Rotation.value.y * 1000));
                self.rotationZ.Add((int)(unit.Rotation.value.z * 1000));
                self.rotationW.Add((int)(unit.Rotation.value.w * 1000));
            }
        }

        public static async ETTask DealByBytes(this SyncData_UnitPosInfo self, UnitComponent unitComponent)
        {
            int count = self.unitId.Count;
            for (int i = 0; i < count; i++)
            {
                Unit unit = unitComponent.Get(self.unitId[i]);
                if (unit == null)
                {
                    continue;
                }
                UnitClientPosComponent unitClientPosComponent = unit.GetComponent<UnitClientPosComponent>();
                if (unitClientPosComponent == null)
                {
                    unitClientPosComponent = unit.AddComponent<UnitClientPosComponent>();
                    unitClientPosComponent.lastClientPosition = float3.zero;
                    unitClientPosComponent.targetPosClientNeedTime = 0.2f;
                    unitClientPosComponent.targetPosClientTime = TimeHelper.ClientNow() + (long)(unitClientPosComponent.targetPosClientNeedTime * 1000);
                    unitClientPosComponent.serverTime = self.serverTime;
                }
                else
                {
                    unitClientPosComponent.lastClientPosition = unitClientPosComponent.clientPosition;
                    unitClientPosComponent.targetPosClientNeedTime = (self.serverTime - unitClientPosComponent.serverTime) * 0.001f;
                    unitClientPosComponent.targetPosClientTime = TimeHelper.ClientNow() + (long)(unitClientPosComponent.targetPosClientNeedTime * 1000);
                    unitClientPosComponent.serverTime = self.serverTime;
                }


                unit.SetPositionWhenClient(self.GetPos(i));
                unit.SetRotationWhenClient(self.GetRotation(i));
            }
            await ETTask.CompletedTask;
        }

        public static float3 GetPos(this SyncData_UnitPosInfo self, int i)
        {
            return new float3(self.posX[i] * 0.001f, self.posY[i] * 0.001f, self.posZ[i] * 0.001f);
        }

        public static quaternion GetRotation(this SyncData_UnitPosInfo self, int i)
        {
            return new quaternion(self.rotationX[i] * 0.001f, self.rotationY[i] * 0.001f, self.rotationZ[i] * 0.001f, self.rotationW[i] * 0.001f);
        }
    }
}