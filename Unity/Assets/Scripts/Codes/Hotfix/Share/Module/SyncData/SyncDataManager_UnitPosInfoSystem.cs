using System;
using System.Collections.Generic;
using System.Diagnostics;
using ET.Ability;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
    public static class SyncDataManager_UnitPosInfoSystem
    {
        public class AwakeSystem: AwakeSystem<SyncDataManager_UnitPosInfo>
        {
            protected override void Awake(SyncDataManager_UnitPosInfo self)
            {
                self.NeedSyncPosUnits = new();
            }
        }

        [ObjectSystem]
        public class SyncDataManager_UnitPosInfoDestroySystem: DestroySystem<SyncDataManager_UnitPosInfo>
        {
            protected override void Destroy(SyncDataManager_UnitPosInfo self)
            {
                self.NeedSyncPosUnits.Clear();
            }
        }

        public static void FixedUpdate(this SyncDataManager_UnitPosInfo self, float fixedDeltaTime)
        {
	        if (++self.curFrameSyncPos >= self.waitFrameSyncPos)
	        {
		        self.curFrameSyncPos = 0;

		        self.SyncPosUnit().Coroutine();
	        }
        }

        public static void AddSyncPosUnit(this SyncDataManager_UnitPosInfo self, Unit unit)
        {
            if (self.NeedSyncPosUnits.Contains(unit))
            {
                return;
            }

            if (unit.GetComponent<MoveTweenObj>() != null)
            {
	            return;
            }
            if (unit.GetComponent<AOIEntity>() == null)
            {
                return;
            }
            self.NeedSyncPosUnits.Add(unit);
        }

		public static async ETTask SyncPosUnit(this SyncDataManager_UnitPosInfo self)
		{
            if (self.NeedSyncPosUnits.Count == 0)
                return;

            //同步单位状态（位置、方向、）
            foreach (Unit unit in self.NeedSyncPosUnits)
            {
	            if (unit.IsDisposed)
	            {
		            continue;
	            }
	            MoveByPathComponent moveByPathComponent = unit.GetComponent<MoveByPathComponent>();
	            if (moveByPathComponent != null)
	            {
		            if (moveByPathComponent.IsArrived() == false)
		            {
			            continue;
		            }
	            }

	            SyncData_UnitPosInfo _SyncData_UnitPosInfo = self.AddChild<SyncData_UnitPosInfo>(true);
	            _SyncData_UnitPosInfo.Init(unit);
	            byte[] syncData = _SyncData_UnitPosInfo.ToBson();

	            SyncDataManager syncDataManager = UnitHelper.GetSyncDataManagerComponent(unit.DomainScene());
	            syncDataManager.SyncData2Players(unit, syncData);
	            _SyncData_UnitPosInfo.Dispose();
            }

            self.NeedSyncPosUnits.Clear();

            await ETTask.CompletedTask;
		}

    }
}