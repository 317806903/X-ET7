using System;
using System.Collections.Generic;
using System.Diagnostics;
using ET.Ability;

namespace ET
{
    public static class SyncDataManager_UnitComponentSystem
    {
        public class AwakeSystem: AwakeSystem<SyncDataManager_UnitComponent>
        {
            protected override void Awake(SyncDataManager_UnitComponent self)
            {
	            self.player2SyncUnit = new();
	            self.NeedSyncList = new();
            }
        }

        [ObjectSystem]
        public class SyncDataManager_UnitComponentDestroySystem: DestroySystem<SyncDataManager_UnitComponent>
        {
            protected override void Destroy(SyncDataManager_UnitComponent self)
            {
	            self.player2SyncUnit.Clear();
	            self.NeedSyncList.Clear();
            }
        }

        public static void FixedUpdate(this SyncDataManager_UnitComponent self, float fixedDeltaTime)
        {
	        self.SyncData2Client().Coroutine();
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

        public static async ETTask SyncData2Client(this SyncDataManager_UnitComponent self)
        {
	        self.DealSyncData2PlayerId();
	        await self.SyncData2Client_Wait();
        }

        public static void DealSyncData2PlayerId(this SyncDataManager_UnitComponent self)
        {
	        if (self.NeedSyncList.Count == 0)
		        return;

	        SyncDataManager syncDataManager = UnitHelper.GetSyncDataManagerComponent(self.DomainScene());
	        foreach (var item in self.NeedSyncList)
	        {
		        long unitId = item.Key;
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
				        self.player2SyncUnit.Add(playerId, (unit, item.Value));
			        }
		        }
	        }
	        self.NeedSyncList.Clear();
        }

        public static async ETTask SyncData2Client_Wait(this SyncDataManager_UnitComponent self)
        {
	        if (self.player2SyncUnit.Count == 0)
		        return;

	        SyncDataManager syncDataManager = UnitHelper.GetSyncDataManagerComponent(self.DomainScene());
	        using ListComponent<long> removePlayerIds = ListComponent<long>.Create();
	        foreach (var item in self.player2SyncUnit)
	        {
		        long playerId = item.Key;
		        List<(Unit unit, HashSet<Type> types)> list = item.Value;

		        if (syncDataManager.playerSessionInfoList.TryGetValue(playerId, out int synFrame) == false)
		        {
			        synFrame = 3;
		        }
		        self.waitFrameSync[playerId] = synFrame;
		        if (self.curFrameSync.ContainsKey(playerId) == false)
		        {
			        self.curFrameSync[playerId] = 0;
		        }
		        if (++self.curFrameSync[playerId] >= self.waitFrameSync[playerId])
		        {
			        self.curFrameSync[playerId] = 0;
		        }
		        else
		        {
			        continue;
		        }

		        if (list.Count == 0)
		        {
			        removePlayerIds.Add(playerId);
			        continue;
		        }

		        SyncData_UnitComponent _SyncData_UnitComponent = self.AddChild<SyncData_UnitComponent>(true);
		        _SyncData_UnitComponent.Init(list);
		        if (_SyncData_UnitComponent.unitId.Count == 0)
		        {
			        _SyncData_UnitComponent.Dispose();
			        removePlayerIds.Add(playerId);
			        continue;
		        }

		        byte[] syncData = _SyncData_UnitComponent.ToBson();
		        _SyncData_UnitComponent.Dispose();

		        //Log.Debug($"zpb ET.SyncDataManager_UnitComponentSystem.SyncData2Client_Wait {playerId} {list.Count}");

		        syncDataManager.SyncData2OnlyPlayer(playerId, syncData);
		        removePlayerIds.Add(playerId);
	        }

	        foreach (long playerId in removePlayerIds)
	        {
		        self.player2SyncUnit.Remove(playerId);
	        }

	        removePlayerIds.Clear();

	        await ETTask.CompletedTask;
        }
    }
}