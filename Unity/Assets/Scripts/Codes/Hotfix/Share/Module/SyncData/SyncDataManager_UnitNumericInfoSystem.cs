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
    public static class SyncDataManager_UnitNumericInfoSystem
    {
        public class AwakeSystem: AwakeSystem<SyncDataManager_UnitNumericInfo>
        {
            protected override void Awake(SyncDataManager_UnitNumericInfo self)
            {
	            self.NeedSyncNumericUnitsKey = new();
	            self.NeedSyncNumericUnits = new();
            }
        }

        [ObjectSystem]
        public class SyncDataManager_UnitNumericInfoDestroySystem: DestroySystem<SyncDataManager_UnitNumericInfo>
        {
            protected override void Destroy(SyncDataManager_UnitNumericInfo self)
            {
	            self.NeedSyncNumericUnitsKey.Clear();
	            self.NeedSyncNumericUnits.Clear();
            }
        }

        public static void FixedUpdate(this SyncDataManager_UnitNumericInfo self, float fixedDeltaTime)
        {
	        if (++self.curFrameSyncNumeric >= self.waitFrameSyncNumeric)
	        {
		        self.curFrameSyncNumeric = 0;

		        self.SyncNumericUnit().Coroutine();
	        }
	        if (++self.curFrameSyncNumericKey >= self.waitFrameSyncNumericKey)
	        {
		        self.curFrameSyncNumericKey = 0;

		        self.SyncNumericUnitKey().Coroutine();
	        }
        }

        public static void AddSyncNumericUnit(this SyncDataManager_UnitNumericInfo self, Unit unit)
        {
            if (self.NeedSyncNumericUnits.Contains(unit))
            {
                return;
            }
            if (unit.GetComponent<AOIEntity>() == null)
            {
                return;
            }
            if (unit.GetComponent<NumericComponent>() == null)
            {
	            return;
            }
            self.NeedSyncNumericUnits.Add(unit);
        }

        public static void AddSyncNumericUnitByKey(this SyncDataManager_UnitNumericInfo self, Unit unit, int numericKey)
        {
            if (self.NeedSyncNumericUnitsKey.Contains(unit.Id, numericKey))
            {
                return;
            }
            if (unit.GetComponent<AOIEntity>() == null)
            {
                return;
            }
            if (unit.GetComponent<NumericComponent>() == null)
            {
                return;
            }
            self.NeedSyncNumericUnitsKey.Add(unit.Id, numericKey);
        }

		public static async ETTask SyncNumericUnit(this SyncDataManager_UnitNumericInfo self)
		{
            if (self.NeedSyncNumericUnits.Count == 0)
                return;

            foreach (Unit unit in self.NeedSyncNumericUnits)
            {
	            if (unit.IsDisposed)
	            {
		            continue;
	            }
	            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
	            if (numericComponent == null)
	            {
		            continue;
	            }

	            SyncData_UnitNumericInfo _SyncData_UnitNumericInfo = self.AddChild<SyncData_UnitNumericInfo>(true);
	            _SyncData_UnitNumericInfo.Init(unit);
	            byte[] syncData = _SyncData_UnitNumericInfo.ToBson();

	            SyncDataManager syncDataManager = UnitHelper.GetSyncDataManagerComponent(unit.DomainScene());
	            syncDataManager.SyncData2Players(unit, syncData);
	            _SyncData_UnitNumericInfo.Dispose();
            }

            self.NeedSyncNumericUnits.Clear();
            await ETTask.CompletedTask;
		}

		public static async ETTask SyncNumericUnitKey(this SyncDataManager_UnitNumericInfo self)
		{
            if (self.NeedSyncNumericUnitsKey.Count == 0)
                return;

            foreach (var item in self.NeedSyncNumericUnitsKey)
            {
	            long unitId = item.Key;
	            Unit unit = UnitHelper.GetUnit(self.DomainScene(), unitId);
	            if (unit == null || unit.IsDisposed)
	            {
		            continue;
	            }
	            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
	            if (numericComponent == null)
	            {
		            continue;
	            }

	            SyncData_UnitNumericInfo _SyncData_UnitNumericInfo = self.AddChild<SyncData_UnitNumericInfo>(true);
	            _SyncData_UnitNumericInfo.Init(unit, item.Value);
	            byte[] syncData = _SyncData_UnitNumericInfo.ToBson();

	            SyncDataManager syncDataManager = UnitHelper.GetSyncDataManagerComponent(unit.DomainScene());
	            syncDataManager.SyncData2Players(unit, syncData);
	            _SyncData_UnitNumericInfo.Dispose();
            }

            self.NeedSyncNumericUnitsKey.Clear();
            await ETTask.CompletedTask;
		}

    }
}