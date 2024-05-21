using System;
using System.Collections.Generic;
using System.Diagnostics;
using ET.Ability;
using ET.AbilityConfig;

namespace ET
{
    public static class SyncDataManager_UnitGetCoinShowSystem
    {
        public class AwakeSystem: AwakeSystem<SyncDataManager_UnitGetCoinShow>
        {
            protected override void Awake(SyncDataManager_UnitGetCoinShow self)
            {
                self.player2SyncUnit = new();
                self.NeedSyncGetCoinShowList = new();
            }
        }

        [ObjectSystem]
        public class SyncDataManager_UnitGetCoinShowDestroySystem: DestroySystem<SyncDataManager_UnitGetCoinShow>
        {
            protected override void Destroy(SyncDataManager_UnitGetCoinShow self)
            {
                self.player2SyncUnit.Clear();
                self.NeedSyncGetCoinShowList.Clear();
            }
        }

        public static void FixedUpdate(this SyncDataManager_UnitGetCoinShow self, float fixedDeltaTime)
        {
	        self.SyncData2Client().Coroutine();
        }

        public static void AddSyncGetCoinShow(this SyncDataManager_UnitGetCoinShow self, long playerId, Unit unit, CoinType coinType, int chgValue)
        {
            if (unit.GetComponent<AOIEntity>() == null)
            {
                return;
            }
            self.NeedSyncGetCoinShowList.Add((playerId, unit, coinType, chgValue));
        }

        public static async ETTask SyncData2Client(this SyncDataManager_UnitGetCoinShow self)
        {
	        self.DealSyncData2PlayerId();
	        await self.SyncData2Client_Wait();
        }

		public static void DealSyncData2PlayerId(this SyncDataManager_UnitGetCoinShow self)
		{
			if (self.NeedSyncGetCoinShowList.Count == 0)
				return;

			foreach ((long playerId, Unit unit, CoinType coinType, int chgValue) in self.NeedSyncGetCoinShowList)
			{
				if (unit == null)
				{
					continue;
				}

				self.player2SyncUnit.Add(playerId, (unit, coinType, chgValue));
			}
			self.NeedSyncGetCoinShowList.Clear();
		}

		public static async ETTask SyncData2Client_Wait(this SyncDataManager_UnitGetCoinShow self)
		{
            if (self.player2SyncUnit.Count == 0)
                return;

            SyncDataManager syncDataManager = UnitHelper.GetSyncDataManagerComponent(self.DomainScene());
            using ListComponent<long> removePlayerIds = ListComponent<long>.Create();
            foreach (var item in self.player2SyncUnit)
            {
	            long playerId = item.Key;
	            List<(Unit unit, CoinType coinType, int chgValue)> list = item.Value;

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

	            SyncData_UnitGetCoinShow _SyncData_UnitGetCoinShow = self.AddChild<SyncData_UnitGetCoinShow>(true);
	            _SyncData_UnitGetCoinShow.Init(list);
	            if (_SyncData_UnitGetCoinShow.unitId.Count == 0)
	            {
		            _SyncData_UnitGetCoinShow.Dispose();
		            removePlayerIds.Add(playerId);
		            continue;
	            }
	            byte[] syncData = _SyncData_UnitGetCoinShow.ToBson();
	            _SyncData_UnitGetCoinShow.Dispose();

	            //Log.Debug($"zpb ET.SyncDataManager_UnitGetCoinShowSystem.SyncData2Client_Wait {playerId} {list.Count}");

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