using System;
using System.Collections.Generic;
using System.Diagnostics;
using ET.Ability;
using ET.AbilityConfig;

namespace ET
{
    public static class SyncDataManager_DamageShowSystem
    {
        public class AwakeSystem: AwakeSystem<SyncDataManager_DamageShow>
        {
            protected override void Awake(SyncDataManager_DamageShow self)
            {
                self.player2SyncUnit = new();
                self.NeedSyncDamageShowList = new();
            }
        }

        [ObjectSystem]
        public class SyncDataManager_DamageShowDestroySystem: DestroySystem<SyncDataManager_DamageShow>
        {
            protected override void Destroy(SyncDataManager_DamageShow self)
            {
                self.player2SyncUnit.Clear();
                self.NeedSyncDamageShowList.Clear();
            }
        }

        public static void FixedUpdate(this SyncDataManager_DamageShow self, float fixedDeltaTime)
        {
	        self.SyncData2Client();
        }

        public static void AddSyncDamageShow(this SyncDataManager_DamageShow self, Unit unit, int damageValue, bool isCrt)
        {
            if (unit.GetComponent<AOIEntity>() == null)
            {
                return;
            }
            self.NeedSyncDamageShowList.Add((unit, damageValue, isCrt));
        }

        public static void SyncData2Client(this SyncDataManager_DamageShow self)
        {
	        self.DealSyncData2PlayerId();
	        self.SyncData2Client_Wait();
        }

		public static void DealSyncData2PlayerId(this SyncDataManager_DamageShow self)
		{
			if (self.NeedSyncDamageShowList.Count == 0)
				return;

			SyncDataManager syncDataManager = UnitHelper.GetSyncDataManagerComponent(self.DomainScene());
			foreach ((Unit unit, int damageValue, bool isCrt) in self.NeedSyncDamageShowList)
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
						self.player2SyncUnit.Add(playerId, (unit, damageValue, isCrt));
					}
				}
			}
			self.NeedSyncDamageShowList.Clear();
		}

		public static void SyncData2Client_Wait(this SyncDataManager_DamageShow self)
		{
            if (self.player2SyncUnit.Count == 0)
                return;

            SyncDataManager syncDataManager = UnitHelper.GetSyncDataManagerComponent(self.DomainScene());
            self.removePlayerIds.Clear();
            foreach (var item in self.player2SyncUnit)
            {
	            long playerId = item.Key;
	            List<(Unit unit, int damageValue, bool isCrt)> list = item.Value;

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

	            SyncData_DamageShow _SyncData_DamageShow = self.AddChild<SyncData_DamageShow>(true);
	            _SyncData_DamageShow.Init(list);
	            if (_SyncData_DamageShow.unitId.Count == 0)
	            {
		            _SyncData_DamageShow.Dispose();
		            self.removePlayerIds.Add(playerId);
		            continue;
	            }
	            byte[] syncData = _SyncData_DamageShow.ToBson();
	            _SyncData_DamageShow.Dispose();

	            //Log.Debug($"zpb ET.SyncDataManager_DamageShowSystem.SyncData2Client_Wait {playerId} {list.Count}");

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