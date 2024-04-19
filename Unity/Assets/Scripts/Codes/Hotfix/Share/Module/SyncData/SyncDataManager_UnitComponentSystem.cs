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
    public static class SyncDataManager_UnitComponentSystem
    {
        public class AwakeSystem: AwakeSystem<SyncDataManager_UnitComponent>
        {
            protected override void Awake(SyncDataManager_UnitComponent self)
            {
	            self.NeedSyncList = new();
            }
        }

        [ObjectSystem]
        public class SyncDataManager_UnitComponentDestroySystem: DestroySystem<SyncDataManager_UnitComponent>
        {
            protected override void Destroy(SyncDataManager_UnitComponent self)
            {
	            self.NeedSyncList.Clear();
            }
        }

        public static void FixedUpdate(this SyncDataManager_UnitComponent self, float fixedDeltaTime)
        {
	        if (++self.curFrameSync >= self.waitFrameSync)
	        {
		        self.curFrameSync = 0;

		        self.SyncUnits().Coroutine();
	        }
        }

        public static void AddSyncUnit(this SyncDataManager_UnitComponent self, Unit unit, Type type)
        {
            if (self.NeedSyncList.Contains(unit.Id, type))
            {
                return;
            }
            if (unit.GetComponent<AOIEntity>() == null)
            {
                return;
            }
            self.NeedSyncList.Add(unit.Id, type);
        }

		public static async ETTask SyncUnits(this SyncDataManager_UnitComponent self)
		{
            if (self.NeedSyncList.Count == 0)
                return;

            foreach (var item in self.NeedSyncList)
            {
	            long unitId = item.Key;
	            Unit unit = UnitHelper.GetUnit(self.DomainScene(), unitId);
	            if (unit == null || unit.IsDisposed)
	            {
		            continue;
	            }

	            SyncData_UnitComponent _SyncData_UnitComponent = self.AddChild<SyncData_UnitComponent>(true);
	            _SyncData_UnitComponent.Init(unit, item.Value);
	            if (_SyncData_UnitComponent.unitComponents.Count == 0)
	            {
		            _SyncData_UnitComponent.Dispose();
		            continue;
	            }
	            byte[] syncData = _SyncData_UnitComponent.ToBson();
	            SyncDataManager syncDataManager = UnitHelper.GetSyncDataManagerComponent(unit.DomainScene());
	            syncDataManager.SyncData2Players(unit, syncData);
	            _SyncData_UnitComponent.Dispose();
            }

            self.NeedSyncList.Clear();
            await ETTask.CompletedTask;
		}

    }
}