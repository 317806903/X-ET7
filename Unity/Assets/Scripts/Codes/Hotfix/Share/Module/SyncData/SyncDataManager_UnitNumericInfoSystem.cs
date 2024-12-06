using System;
using System.Collections.Generic;
using System.Diagnostics;
using ET.Ability;

namespace ET
{
    public static class SyncDataManager_UnitNumericInfoSystem
    {
        public class AwakeSystem: AwakeSystem<SyncDataManager_UnitNumericInfo>
        {
            protected override void Awake(SyncDataManager_UnitNumericInfo self)
            {
	            self.player2SyncUnit = new();
	            self.player2SyncUnit_AllKey = new();
	            self.NeedSyncNumericUnit = new();
	            self.NeedSyncNumericUnit_AllKey = new();
            }
        }

        [ObjectSystem]
        public class SyncDataManager_UnitNumericInfoDestroySystem: DestroySystem<SyncDataManager_UnitNumericInfo>
        {
            protected override void Destroy(SyncDataManager_UnitNumericInfo self)
            {
	            self.player2SyncUnit.Clear();
	            self.player2SyncUnit_AllKey.Clear();
	            self.NeedSyncNumericUnit.Clear();
	            self.NeedSyncNumericUnit_AllKey.Clear();
            }
        }

        public static void FixedUpdate(this SyncDataManager_UnitNumericInfo self, float fixedDeltaTime)
        {
	        self.SyncData2Client_AllKey();
	        self.SyncData2Client();
        }

        public static void AddSyncNumericUnit(this SyncDataManager_UnitNumericInfo self, Unit unit)
        {
            if (self.NeedSyncNumericUnit_AllKey.Contains(unit))
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
            self.NeedSyncNumericUnit_AllKey.Add(unit);
        }

        public static void AddSyncNumericUnitByKey(this SyncDataManager_UnitNumericInfo self, Unit unit, int numericKey)
        {
            if (self.NeedSyncNumericUnit.Contains(unit.Id, numericKey))
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
            self.NeedSyncNumericUnit.Add(unit.Id, numericKey);
        }

        public static void SyncData2Client(this SyncDataManager_UnitNumericInfo self)
        {
	        self.DealSyncData2PlayerId();
	        self.SyncData2Client_Wait();
        }

        public static void DealSyncData2PlayerId(this SyncDataManager_UnitNumericInfo self)
        {
	        if (self.NeedSyncNumericUnit.Count == 0)
		        return;

	        SyncDataManager syncDataManager = UnitHelper.GetSyncDataManagerComponent(self.DomainScene());
	        foreach (var item in self.NeedSyncNumericUnit)
	        {
		        long unitId = item.Key;
		        HashSet<int> numericTypes = item.Value;
		        Unit unit = UnitHelper.GetUnit(self.DomainScene(), unitId);
		        if (unit == null || unit.IsDisposed)
		        {
			        continue;
		        }

		        List<long> syncData2Players = syncDataManager.GetSyncData2Players(unit);
		        if (syncData2Players != null)
		        {
			        foreach (long playerId in syncData2Players)
			        {
				        if (self.player2SyncUnit.TryGetValue(playerId, unit, out var curNumericTypes))
				        {
					        foreach (int numericType in numericTypes)
					        {
						        curNumericTypes.Add(numericType);
					        }
				        }
				        else
				        {
					        self.player2SyncUnit.Add(playerId, unit, numericTypes);
				        }
			        }
		        }
	        }
	        self.NeedSyncNumericUnit.Clear();
        }

        public static void SyncData2Client_Wait(this SyncDataManager_UnitNumericInfo self)
        {
	        if (self.player2SyncUnit.Count == 0)
		        return;

	        SyncDataManager syncDataManager = UnitHelper.GetSyncDataManagerComponent(self.DomainScene());
	        self.removePlayerIds.Clear();
	        foreach (var item in self.player2SyncUnit)
	        {
		        long playerId = item.Key;
		        Dictionary<Unit, HashSet<int>> list = item.Value;

		        if (syncDataManager.playerSessionInfoList.TryGetValue(playerId, out int synFrame) == false)
		        {
			        synFrame = 3;
		        }
		        self.waitFrameSyncNumericKey[playerId] = synFrame;
		        if (self.curFrameSyncNumericKey.ContainsKey(playerId) == false)
		        {
			        self.curFrameSyncNumericKey[playerId] = 0;
		        }
		        if (++self.curFrameSyncNumericKey[playerId] >= self.waitFrameSyncNumericKey[playerId])
		        {
			        self.curFrameSyncNumericKey[playerId] = 0;
		        }
		        else
		        {
			        continue;
		        }

		        if (list.Count == 0)
		        {
			        self.removePlayerIds.Add(playerId);
			        continue;
		        }

		        SyncData_UnitNumericInfo _SyncData_UnitNumericInfo = self.AddChild<SyncData_UnitNumericInfo>(true);
		        _SyncData_UnitNumericInfo.Init(list);

		        if (_SyncData_UnitNumericInfo.unitId.Count == 0)
		        {
			        _SyncData_UnitNumericInfo.Dispose();
			        self.removePlayerIds.Add(playerId);
			        continue;
		        }

		        byte[] syncData = _SyncData_UnitNumericInfo.ToBson();
		        _SyncData_UnitNumericInfo.Dispose();

		        //Log.Debug($"zpb ET.SyncDataManager_UnitNumericInfoSystem.SyncData2Client_Wait {playerId} {list.Count}");

		        syncDataManager.SyncData2OnlyPlayer(playerId, syncData);
		        self.removePlayerIds.Add(playerId);
	        }

	        foreach (long playerId in self.removePlayerIds)
	        {
		        self.player2SyncUnit.Remove(playerId);
	        }
	        self.removePlayerIds.Clear();
        }

        public static void SyncData2Client_AllKey(this SyncDataManager_UnitNumericInfo self)
        {
	        self.DealSyncData2PlayerId_AllKey();
	        self.SyncData2Client_AllKey_Wait();
        }

        public static void DealSyncData2PlayerId_AllKey(this SyncDataManager_UnitNumericInfo self)
        {
	        if (self.NeedSyncNumericUnit_AllKey.Count == 0)
		        return;

	        SyncDataManager syncDataManager = UnitHelper.GetSyncDataManagerComponent(self.DomainScene());
	        foreach (Unit unit in self.NeedSyncNumericUnit_AllKey)
	        {
		        if (unit == null || unit.IsDisposed)
		        {
			        continue;
		        }

		        List<long> syncData2Players = syncDataManager.GetSyncData2Players(unit);
		        if (syncData2Players != null)
		        {
			        foreach (long playerId in syncData2Players)
			        {
				        self.player2SyncUnit_AllKey.Add(playerId, unit);
			        }
		        }
	        }
	        self.NeedSyncNumericUnit_AllKey.Clear();
        }

        public static void SyncData2Client_AllKey_Wait(this SyncDataManager_UnitNumericInfo self)
        {
	        if (self.player2SyncUnit_AllKey.Count == 0)
		        return;

	        SyncDataManager syncDataManager = UnitHelper.GetSyncDataManagerComponent(self.DomainScene());
	        self.removePlayerIds_AllKey.Clear();
	        foreach (var item in self.player2SyncUnit_AllKey)
	        {
		        long playerId = item.Key;
		        HashSet<Unit> list = item.Value;

		        if (syncDataManager.playerSessionInfoList.TryGetValue(playerId, out int synFrame) == false)
		        {
			        synFrame = 3;
		        }
		        self.waitFrameSyncNumeric[playerId] = synFrame;
		        if (self.curFrameSyncNumeric.ContainsKey(playerId) == false)
		        {
			        self.curFrameSyncNumeric[playerId] = 0;
		        }
		        if (++self.curFrameSyncNumeric[playerId] >= self.waitFrameSyncNumeric[playerId])
		        {
			        self.curFrameSyncNumeric[playerId] = 0;
		        }
		        else
		        {
			        continue;
		        }

		        if (list.Count == 0)
		        {
			        self.removePlayerIds_AllKey.Add(playerId);
			        continue;
		        }

		        SyncData_UnitNumericInfo _SyncData_UnitNumericInfo = self.AddChild<SyncData_UnitNumericInfo>(true);
		        _SyncData_UnitNumericInfo.Init(list);

		        if (_SyncData_UnitNumericInfo.unitId.Count == 0)
		        {
			        _SyncData_UnitNumericInfo.Dispose();
			        self.removePlayerIds_AllKey.Add(playerId);
			        continue;
		        }

		        byte[] syncData = _SyncData_UnitNumericInfo.ToBson();
		        _SyncData_UnitNumericInfo.Dispose();

		        //Log.Debug($"zpb ET.SyncDataManager_UnitNumericInfoSystem.SyncData2Client_AllKey_Wait {playerId} {list.Count}");

		        syncDataManager.SyncData2OnlyPlayer(playerId, syncData);
		        self.removePlayerIds_AllKey.Add(playerId);
	        }

	        foreach (long playerId in self.removePlayerIds_AllKey)
	        {
		        self.player2SyncUnit_AllKey.Remove(playerId);
	        }
	        self.removePlayerIds_AllKey.Clear();
        }
    }
}