using System;
using System.Collections.Generic;
using System.Diagnostics;
using ET.Ability;

namespace ET
{
    public static class SyncDataManager_UnitBaseInfoSystem
    {
        public class AwakeSystem: AwakeSystem<SyncDataManager_UnitBaseInfo>
        {
            protected override void Awake(SyncDataManager_UnitBaseInfo self)
            {
                self.player2SyncUnit = new();
                self.NeedSyncUnits = new();
            }
        }

        [ObjectSystem]
        public class SyncDataManager_UnitBaseInfoDestroySystem: DestroySystem<SyncDataManager_UnitBaseInfo>
        {
            protected override void Destroy(SyncDataManager_UnitBaseInfo self)
            {
                self.player2SyncUnit.Clear();
                self.NeedSyncUnits.Clear();
            }
        }

        public static void FixedUpdate(this SyncDataManager_UnitBaseInfo self, float fixedDeltaTime)
        {
	        self.SyncData2Client();
        }

        public static void AddSyncPosUnit(this SyncDataManager_UnitBaseInfo self, Unit unit)
        {
            if (self.NeedSyncUnits.Contains(unit))
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

            self.NeedSyncUnits.Add(unit);
        }

		public static void SyncData2Client(this SyncDataManager_UnitBaseInfo self)
		{
			self.DealSyncData2PlayerId();
			self.SyncData2Client_Wait();
		}

		public static void DealSyncData2PlayerId(this SyncDataManager_UnitBaseInfo self)
		{
			if (self.NeedSyncUnits.Count == 0)
				return;

			SyncDataManager syncDataManager = UnitHelper.GetSyncDataManagerComponent(self.DomainScene());
			//同步单位状态（位置、方向、）
			foreach (Unit unit in self.NeedSyncUnits)
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
			self.NeedSyncUnits.Clear();
		}

		public static void SyncData2Client_Wait(this SyncDataManager_UnitBaseInfo self)
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

	            SyncData_UnitBaseInfo _SyncData_UnitBaseInfo = self.AddChild<SyncData_UnitBaseInfo>(true);
	            _SyncData_UnitBaseInfo.Init(list);
	            if (_SyncData_UnitBaseInfo.unitId.Count == 0)
	            {
		            _SyncData_UnitBaseInfo.Dispose();
		            self.removePlayerIds.Add(playerId);
		            continue;
	            }
	            byte[] syncData = _SyncData_UnitBaseInfo.ToBson();
	            syncDataManager.SyncData2OnlyPlayer(playerId, syncData);
	            _SyncData_UnitBaseInfo.Dispose();
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