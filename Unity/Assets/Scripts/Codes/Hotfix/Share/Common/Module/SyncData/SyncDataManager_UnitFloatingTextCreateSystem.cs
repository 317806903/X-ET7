using System;
using System.Collections.Generic;
using System.Diagnostics;
using ET.Ability;

namespace ET
{
    public static class SyncDataManager_UnitFloatingTextSystem
    {
        public class AwakeSystem: AwakeSystem<SyncDataManager_UnitFloatingText>
        {
            protected override void Awake(SyncDataManager_UnitFloatingText self)
            {
                self.player2SyncUnit = new();
                self.NeedSyncList = new();
            }
        }

        [ObjectSystem]
        public class SyncDataManager_UnitFloatingTextDestroySystem: DestroySystem<SyncDataManager_UnitFloatingText>
        {
            protected override void Destroy(SyncDataManager_UnitFloatingText self)
            {
                self.player2SyncUnit.Clear();
                self.NeedSyncList.Clear();
            }
        }

        public static void FixedUpdate(this SyncDataManager_UnitFloatingText self, float fixedDeltaTime)
        {
	        self.SyncData2Client();
        }

        public static void AddSyncFloatingText(this SyncDataManager_UnitFloatingText self, Unit unit, string floatingTextId, int showNum, bool isOnlySelfShow)
        {
            if (unit.GetComponent<AOIEntity>() == null)
            {
                return;
            }
            self.NeedSyncList.Add((unit, floatingTextId, showNum, isOnlySelfShow));
        }

        public static void SyncData2Client(this SyncDataManager_UnitFloatingText self)
        {
	        self.DealSyncData2PlayerId();
	        self.SyncData2Client_Wait();
        }

		public static void DealSyncData2PlayerId(this SyncDataManager_UnitFloatingText self)
		{
			if (self.NeedSyncList.Count == 0)
				return;

			SyncDataManager syncDataManager = UnitHelper.GetSyncDataManagerComponent(self.DomainScene());
			foreach ((Unit unit, string floatingTextActionId, int showNum, bool isOnlySelfShow) in self.NeedSyncList)
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
						self.player2SyncUnit.Add(playerId, (unit, floatingTextActionId, showNum, isOnlySelfShow));
					}
				}
			}
			self.NeedSyncList.Clear();
		}

		public static void SyncData2Client_Wait(this SyncDataManager_UnitFloatingText self)
		{
            if (self.player2SyncUnit.Count == 0)
                return;

            SyncDataManager syncDataManager = UnitHelper.GetSyncDataManagerComponent(self.DomainScene());
            self.removePlayerIds.Clear();
            foreach (var item in self.player2SyncUnit)
            {
	            long playerId = item.Key;
	            HashSet<(Unit unit, string floatingTextActionId, int showNum, bool isOnlySelfShow)> list = item.Value;

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
		            self.removePlayerIds.Add(playerId);
		            continue;
	            }

	            SyncData_UnitFloatingText _SyncData_UnitFloatingText = self.AddChild<SyncData_UnitFloatingText>(true);
	            _SyncData_UnitFloatingText.Init(list);
	            if (_SyncData_UnitFloatingText.unitId.Count == 0)
	            {
		            _SyncData_UnitFloatingText.Dispose();
		            self.removePlayerIds.Add(playerId);
		            continue;
	            }
	            byte[] syncData = _SyncData_UnitFloatingText.ToBson();
	            _SyncData_UnitFloatingText.Dispose();

	            //Log.Debug($"zpb ET.SyncDataManager_UnitFloatingTextSystem.SyncData2Client_Wait {playerId} {list.Count}");

	            syncDataManager.SyncData2OnlyPlayer(playerId, syncData);
	            self.removePlayerIds.Add(playerId);
            }

            foreach (long playerId in self.removePlayerIds)
            {
	            self.player2SyncUnit.Remove(playerId);
            }
            self.removePlayerIds.Clear();
		}
    }
}