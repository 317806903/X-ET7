using System;
using System.Collections.Generic;
using System.Diagnostics;
using ET.Ability;

namespace ET
{
    public static class SyncDataManager_UnitEffectsSystem
    {
        public class AwakeSystem: AwakeSystem<SyncDataManager_UnitEffects>
        {
            protected override void Awake(SyncDataManager_UnitEffects self)
            {
	            self.player2SyncUnit = new();
	            self.NeedSyncList = new();
            }
        }

        [ObjectSystem]
        public class SyncDataManager_UnitEffectsDestroySystem: DestroySystem<SyncDataManager_UnitEffects>
        {
            protected override void Destroy(SyncDataManager_UnitEffects self)
            {
	            self.player2SyncUnit.Clear();
	            self.NeedSyncList.Clear();
            }
        }

        public static void FixedUpdate(this SyncDataManager_UnitEffects self, float fixedDeltaTime)
        {
	        self.SyncData2Client().Coroutine();
        }

        public static void AddSyncUnit(this SyncDataManager_UnitEffects self, Unit unit, long effectObjId, bool isOnlySelfShow)
        {
            if (self.NeedSyncList.Contains(unit, effectObjId))
            {
                return;
            }
            if (unit.GetComponent<AOIEntity>() == null)
            {
                return;
            }
            self.NeedSyncList.Add(unit, effectObjId);
        }

        public static async ETTask SyncData2Client(this SyncDataManager_UnitEffects self)
        {
	        self.DealSyncData2PlayerId();
	        await self.SyncData2Client_Wait();
        }

        public static void DealSyncData2PlayerId(this SyncDataManager_UnitEffects self)
        {
	        if (self.NeedSyncList.Count == 0)
		        return;

	        SyncDataManager syncDataManager = UnitHelper.GetSyncDataManagerComponent(self.DomainScene());
	        foreach (var item in self.NeedSyncList)
	        {
		        Unit unit = item.Key;
		        if (unit == null)
		        {
			        continue;
		        }

		        foreach (long effectObjId in item.Value)
		        {
			        bool isAddOrRemove = false;
			        byte[] unitEffectComponent = null;
			        EffectObj effectObj = EffectHelper.GetEffectObj(unit, effectObjId);
			        if (effectObj == null)
			        {
				        isAddOrRemove = false;
				        unitEffectComponent = null;
			        }
			        else
			        {

				        if (effectObj.ChkNeedRemove())
				        {
					        isAddOrRemove = false;
					        unitEffectComponent = null;
				        }
				        else
				        {
					        isAddOrRemove = true;
					        unitEffectComponent = effectObj.ToBson();
				        }
			        }

			        if (unit.IsDisposed && isAddOrRemove)
			        {
				        continue;
			        }

			        List<long> syncData2Players = syncDataManager.GetSyncData2Players(unit);
			        if (syncData2Players != null)
			        {
				        foreach (long playerId in syncData2Players)
				        {
					        self.player2SyncUnit.Add(playerId, (unit, effectObjId, isAddOrRemove, unitEffectComponent));
				        }
			        }
		        }

	        }
	        self.NeedSyncList.Clear();
        }

        public static async ETTask SyncData2Client_Wait(this SyncDataManager_UnitEffects self)
        {
	        if (self.player2SyncUnit.Count == 0)
		        return;

	        SyncDataManager syncDataManager = UnitHelper.GetSyncDataManagerComponent(self.DomainScene());
	        using ListComponent<long> removePlayerIds = ListComponent<long>.Create();
	        foreach (var item in self.player2SyncUnit)
	        {
		        long playerId = item.Key;
		        List<(Unit unit, long effectObjId, bool isAddOrRemove, byte[] unitEffectComponent)> list = item.Value;

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

		        SyncData_UnitEffects _SyncData_UnitEffects = self.AddChild<SyncData_UnitEffects>(true);
		        _SyncData_UnitEffects.Init(list);
		        if (_SyncData_UnitEffects.unitId.Count == 0)
		        {
			        _SyncData_UnitEffects.Dispose();
			        removePlayerIds.Add(playerId);
			        continue;
		        }

		        byte[] syncData = _SyncData_UnitEffects.ToBson();
		        _SyncData_UnitEffects.Dispose();

		        //Log.Debug($"zpb ET.SyncDataManager_UnitEffectsSystem.SyncData2Client_Wait {playerId} {list.Count}");

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