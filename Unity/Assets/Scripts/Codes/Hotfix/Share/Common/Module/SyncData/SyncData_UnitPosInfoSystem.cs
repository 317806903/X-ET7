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
                self.posX.Add((int)(unit.Position.x * 10000));
                self.posY.Add((int)(unit.Position.y * 10000));
                self.posZ.Add((int)(unit.Position.z * 10000));
                self.rotationX.Add((int)(unit.Rotation.value.x * 10000));
                self.rotationY.Add((int)(unit.Rotation.value.y * 10000));
                self.rotationZ.Add((int)(unit.Rotation.value.z * 10000));
                self.rotationW.Add((int)(unit.Rotation.value.w * 10000));
            }
        }

        public static void DealByBytes(this SyncData_UnitPosInfo self, UnitComponent unitComponent)
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
                    unitClientPosComponent.targetPosClientNeedTime = 0;
                    unitClientPosComponent.targetPosClientTime = TimeHelper.ClientNow() + (long)(unitClientPosComponent.targetPosClientNeedTime * 1000);
                    unitClientPosComponent.serverTime = self.serverTime;
                }
                else
                {
                    // Log.Error($"--zpb Sync[{(self.serverTime - unitClientPosComponent.serverTime) * 0.001f}] unitClientPosComponent.targetPosClientNeedTime[{unitClientPosComponent.targetPosClientNeedTime}] ");
                    if (unitClientPosComponent.serverTime == 0)
                    {
                        unitClientPosComponent.targetPosClientNeedTime = 0;
                    }
                    else
                    {
                        unitClientPosComponent.targetPosClientNeedTime = (self.serverTime - unitClientPosComponent.serverTime) * 0.001f;
                    }
                    unitClientPosComponent.targetPosClientTime = TimeHelper.ClientNow() + (long)(unitClientPosComponent.targetPosClientNeedTime * 1000);
                    unitClientPosComponent.serverTime = self.serverTime;
                }


                float3 serverPos = self.GetPos(i);
                quaternion serverRotation = self.GetRotation(i);
                serverPos = ET.Ability.UnitHelper.TranServerPos2ClientPos(unitComponent.DomainScene(), serverPos);
                serverRotation = ET.Ability.UnitHelper.TranServerQuaternion2ClientQuaternion(serverRotation);

                unit.SetPositionWhenClient(serverPos);
                unit.SetRotationWhenClient(serverRotation);
            }
        }

        public static float3 GetPos(this SyncData_UnitPosInfo self, int i)
        {
            return new float3(self.posX[i] * 0.0001f, self.posY[i] * 0.0001f, self.posZ[i] * 0.0001f);
        }

        public static quaternion GetRotation(this SyncData_UnitPosInfo self, int i)
        {
            return new quaternion(self.rotationX[i] * 0.0001f, self.rotationY[i] * 0.0001f, self.rotationZ[i] * 0.0001f, self.rotationW[i] * 0.0001f);
        }
    }
}