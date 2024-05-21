using System;
using System.Collections.Generic;
using System.Diagnostics;
using ET.Ability;

namespace ET
{
    public static class SyncDataManager_UnitPlayAudioSystem
    {
        public class AwakeSystem: AwakeSystem<SyncDataManager_UnitPlayAudio>
        {
            protected override void Awake(SyncDataManager_UnitPlayAudio self)
            {
                self.player2SyncUnit = new();
                self.NeedSyncPlayAudioList = new();
            }
        }

        [ObjectSystem]
        public class SyncDataManager_UnitPlayAudioDestroySystem: DestroySystem<SyncDataManager_UnitPlayAudio>
        {
            protected override void Destroy(SyncDataManager_UnitPlayAudio self)
            {
                self.player2SyncUnit.Clear();
                self.NeedSyncPlayAudioList.Clear();
            }
        }

        public static void FixedUpdate(this SyncDataManager_UnitPlayAudio self, float fixedDeltaTime)
        {
	        self.SyncData2Client().Coroutine();
        }

        public static void AddSyncPlayAudio(this SyncDataManager_UnitPlayAudio self, Unit unit, string playAudioActionId, bool isOnlySelfShow)
        {
            if (unit.GetComponent<AOIEntity>() == null)
            {
                return;
            }
            self.NeedSyncPlayAudioList.Add((unit, playAudioActionId, isOnlySelfShow));
        }

        public static async ETTask SyncData2Client(this SyncDataManager_UnitPlayAudio self)
        {
	        self.DealSyncData2PlayerId();
	        await self.SyncData2Client_Wait();
        }

		public static void DealSyncData2PlayerId(this SyncDataManager_UnitPlayAudio self)
		{
			if (self.NeedSyncPlayAudioList.Count == 0)
				return;

			SyncDataManager syncDataManager = UnitHelper.GetSyncDataManagerComponent(self.DomainScene());
			foreach ((Unit unit, string playAudioActionId, bool isOnlySelfShow) in self.NeedSyncPlayAudioList)
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
						self.player2SyncUnit.Add(playerId, (unit, playAudioActionId, isOnlySelfShow));
					}
				}
			}
			self.NeedSyncPlayAudioList.Clear();
		}

		public static async ETTask SyncData2Client_Wait(this SyncDataManager_UnitPlayAudio self)
		{
            if (self.player2SyncUnit.Count == 0)
                return;

            SyncDataManager syncDataManager = UnitHelper.GetSyncDataManagerComponent(self.DomainScene());
            using ListComponent<long> removePlayerIds = ListComponent<long>.Create();
            foreach (var item in self.player2SyncUnit)
            {
	            long playerId = item.Key;
	            HashSet<(Unit unit, string playAudioActionId, bool isOnlySelfShow)> list = item.Value;

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

	            SyncData_UnitPlayAudio _SyncData_UnitPlayAudio = self.AddChild<SyncData_UnitPlayAudio>(true);
	            _SyncData_UnitPlayAudio.Init(list);
	            if (_SyncData_UnitPlayAudio.unitId.Count == 0)
	            {
		            _SyncData_UnitPlayAudio.Dispose();
		            removePlayerIds.Add(playerId);
		            continue;
	            }
	            byte[] syncData = _SyncData_UnitPlayAudio.ToBson();
	            _SyncData_UnitPlayAudio.Dispose();

	            //Log.Debug($"zpb ET.SyncDataManager_UnitPlayAudioSystem.SyncData2Client_Wait {playerId} {list.Count}");

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