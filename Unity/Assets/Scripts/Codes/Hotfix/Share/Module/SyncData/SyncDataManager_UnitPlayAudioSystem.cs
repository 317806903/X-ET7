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
    public static class SyncDataManager_UnitPlayAudioSystem
    {
        public class AwakeSystem: AwakeSystem<SyncDataManager_UnitPlayAudio>
        {
            protected override void Awake(SyncDataManager_UnitPlayAudio self)
            {
                self.NeedSyncPlayAudioList = new();
            }
        }

        [ObjectSystem]
        public class SyncDataManager_UnitPlayAudioDestroySystem: DestroySystem<SyncDataManager_UnitPlayAudio>
        {
            protected override void Destroy(SyncDataManager_UnitPlayAudio self)
            {
                self.NeedSyncPlayAudioList.Clear();
            }
        }

        public static void FixedUpdate(this SyncDataManager_UnitPlayAudio self, float fixedDeltaTime)
        {
	        if (++self.curFrameSyncPlayAudio >= self.waitFrameSyncPlayAudio)
	        {
		        self.curFrameSyncPlayAudio = 0;

		        self.SyncPlayAudio().Coroutine();
	        }
        }

        public static void AddSyncPlayAudio(this SyncDataManager_UnitPlayAudio self, Unit unit, string playAudioActionId, bool isOnlySelfShow)
        {
            if (unit.GetComponent<AOIEntity>() == null)
            {
                return;
            }
            self.NeedSyncPlayAudioList.Add((unit, playAudioActionId, isOnlySelfShow));
        }

		public static async ETTask SyncPlayAudio(this SyncDataManager_UnitPlayAudio self)
		{
            if (self.NeedSyncPlayAudioList.Count == 0)
                return;

            foreach ((Unit unit, string playAudioActionId, bool isOnlySelfShow) in self.NeedSyncPlayAudioList)
            {
	            if (unit.IsDisposed)
	            {
		            continue;
	            }

	            SyncData_UnitPlayAudio _SyncData_UnitPlayAudio = self.AddChild<SyncData_UnitPlayAudio>(true);
	            _SyncData_UnitPlayAudio.Init(unit, playAudioActionId, isOnlySelfShow);
	            byte[] syncData = _SyncData_UnitPlayAudio.ToBson();

	            SyncDataManager syncDataManager = UnitHelper.GetSyncDataManagerComponent(unit.DomainScene());
	            if (isOnlySelfShow)
	            {
		            long playerId = ET.GamePlayHelper.GetPlayerIdByUnitId(unit);
		            if (playerId != -1)
		            {
			            syncDataManager.SyncData2OnlyPlayer(playerId, syncData);
		            }
		            continue;
	            }

	            syncDataManager.SyncData2Players(unit, syncData);

	            _SyncData_UnitPlayAudio.Dispose();
            }

            self.NeedSyncPlayAudioList.Clear();

            await ETTask.CompletedTask;
		}

    }
}