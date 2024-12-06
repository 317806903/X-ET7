using System;
using System.Collections.Generic;
using System.Diagnostics;
using ET.Ability;

namespace ET
{
    public static class SyncDataManager_UnitPosInfoSystem
    {
        public class AwakeSystem: AwakeSystem<SyncDataManager_UnitPosInfo>
        {
            protected override void Awake(SyncDataManager_UnitPosInfo self)
            {
                self.player2SyncUnit = new();
                self.NeedSyncPosUnits = new();
            }
        }

        [ObjectSystem]
        public class SyncDataManager_UnitPosInfoDestroySystem: DestroySystem<SyncDataManager_UnitPosInfo>
        {
            protected override void Destroy(SyncDataManager_UnitPosInfo self)
            {
                self.player2SyncUnit.Clear();
                self.NeedSyncPosUnits.Clear();
            }
        }

        public static void FixedUpdate(this SyncDataManager_UnitPosInfo self, float fixedDeltaTime)
        {
	        self.SyncData2Client();
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

		public static void SyncData2Client(this SyncDataManager_UnitPosInfo self)
		{
			self.DealSyncData2PlayerId();
			self.SyncData2Client_Wait();
		}

		public static void DealSyncData2PlayerId(this SyncDataManager_UnitPosInfo self)
		{
			if (self.NeedSyncPosUnits.Count == 0)
				return;

			SyncDataManager syncDataManager = UnitHelper.GetSyncDataManagerComponent(self.DomainScene());
			//同步单位状态（位置、方向、）
			foreach (Unit unit in self.NeedSyncPosUnits)
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
						self.player2SyncUnit.Add(playerId, unit);
					}
				}
			}
			self.NeedSyncPosUnits.Clear();
		}

		public static void SyncData2Client_Wait(this SyncDataManager_UnitPosInfo self)
		{
            if (self.player2SyncUnit.Count == 0)
                return;

            SyncDataManager syncDataManager = UnitHelper.GetSyncDataManagerComponent(self.DomainScene());
            self.removePlayerIds.Clear();
            foreach (var item in self.player2SyncUnit)
            {
	            long playerId = item.Key;
	            HashSet<Unit> list = item.Value;

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

	            SyncData_UnitPosInfo _SyncData_UnitPosInfo = self.AddChild<SyncData_UnitPosInfo>(true);
	            _SyncData_UnitPosInfo.Init(list);
	            if (_SyncData_UnitPosInfo.unitId.Count == 0)
	            {
		            _SyncData_UnitPosInfo.Dispose();
		            self.removePlayerIds.Add(playerId);
		            continue;
	            }
	            byte[] syncData = _SyncData_UnitPosInfo.ToBson();
	            syncDataManager.SyncData2OnlyPlayer(playerId, syncData);
	            _SyncData_UnitPosInfo.Dispose();
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