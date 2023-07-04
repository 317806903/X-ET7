using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof(PlayerOwnerTowersComponent))]
    public static class PlayerOwnerTowersComponentSystem
	{
		[ObjectSystem]
		public class PlayerOwnerTowersComponentAwakeSystem : AwakeSystem<PlayerOwnerTowersComponent>
		{
			protected override void Awake(PlayerOwnerTowersComponent self)
			{
				self.playerOwnerTowerId = new();
				self.playerTowerBuyPools = new();
				self.playerTowerBuyPoolBoughts = new();
				self.playerRefreshTowerCost = new();
				self.unitId2TowerCfgId = new();

				self.Init();
			}
		}
	
		[ObjectSystem]
		public class PlayerOwnerTowersComponentDestroySystem : DestroySystem<PlayerOwnerTowersComponent>
		{
			protected override void Destroy(PlayerOwnerTowersComponent self)
			{
				self.playerOwnerTowerId.Clear();
				self.playerTowerBuyPools.Clear();
				self.playerTowerBuyPoolBoughts.Clear();
				self.playerRefreshTowerCost.Clear();
				self.unitId2TowerCfgId.Clear();
			}
		}

		public static void Init(this PlayerOwnerTowersComponent self)
		{
			List<RoomMember> roomMembers = self.GetParent<GamePlayComponent>().GetPlayerList();
			for (int i = 0; i < roomMembers.Count; i++)
			{
				long playerId = roomMembers[i].Id;
				self.RefreshPlayerTowerPool(playerId);
				self.playerRefreshTowerCost[playerId] = 1;
				self.playerOwnerTowerId[playerId] = new();
			}
		}

		public static void RefreshTowerPool(this PlayerOwnerTowersComponent self, long playerId)
		{
			self.RefreshPlayerTowerPool(playerId);
			self.CostWhenRefresh(playerId);
			
			self.NoticeToClient(playerId);
		}

		public static void RefreshPlayerTowerPool(this PlayerOwnerTowersComponent self, long playerId)
		{
			if (self.playerTowerBuyPools.ContainsKey(playerId))
			{
				self.playerTowerBuyPools[playerId].Clear();
			}
			if (self.playerTowerBuyPoolBoughts.ContainsKey(playerId))
			{
				self.playerTowerBuyPoolBoughts[playerId].Clear();
			}

			var towerPools = TowerCfgCategory.Instance.DataList;
			int count = 4;
			for (int i = 0; i < count; i++)
			{
				int randomIndex = RandomGenerator.RandomNumber(0, towerPools.Count);
				self.playerTowerBuyPools.Add(playerId, towerPools[randomIndex].Id);
				self.playerTowerBuyPoolBoughts.Add(playerId, false);
			}
		}

		public static void CostWhenRefresh(this PlayerOwnerTowersComponent self, long playerId)
		{
			
		}

		public static void BuyPlayerTower(this PlayerOwnerTowersComponent self, long playerId, int index)
		{
			if (self.playerTowerBuyPools.ContainsKey(playerId) == false)
			{
				Log.Error($" BuyPlayerTower self.playerTowerPools.ContainsKey(playerId) == false");
				return;
			}
			if (self.playerTowerBuyPools[playerId].Count <= index)
			{
				Log.Error($" BuyPlayerTower self.playerTowerPools[playerId].Count <= index");
				return;
			}
			
			bool isBought = self.playerTowerBuyPoolBoughts[playerId][index];
			if (isBought)
			{
				Log.Error($" BuyPlayerTower isBought==true");
				return;
			}

			string towerId = self.playerTowerBuyPools[playerId][index];
			if (self.playerOwnerTowerId.TryGetValue(playerId, towerId, out int ownCount) == false)
			{
				self.playerOwnerTowerId.Add(playerId, towerId, 1);
			}
			else
			{
				self.playerOwnerTowerId[playerId][towerId] += 1;
			}
			self.playerTowerBuyPoolBoughts[playerId][index] = true;

			if (self.ChkRefreshPlayerTowerPool(playerId))
			{
				self.RefreshPlayerTowerPool(playerId);
			}

			self.NoticeToClient(playerId);
		}

		public static void NoticeToClient(this PlayerOwnerTowersComponent self, long playerId)
		{
			self.GetParent<GamePlayComponent>().NoticeToClient(playerId);
		}
		
		public static bool ChkRefreshPlayerTowerPool(this PlayerOwnerTowersComponent self, long playerId)
		{
			List<bool> list = self.playerTowerBuyPoolBoughts[playerId];
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i] == false)
				{
					return false;
				}
			}
			return true;
		}

		public static void CallPlayerTower(this PlayerOwnerTowersComponent self, long playerId, string towerId, float3 position)
		{
			if (self.playerOwnerTowerId.TryGetValue(playerId, towerId, out int count) == false)
			{
				Log.Error($" CallPlayerTower self.playerOwnerTowerId.TryGetValue(playerId, towerId, out int count) == false");
				return;
			}

			if (count <= 0)
			{
				Log.Error($" CallPlayerTower count <= 0");
				return;
			}

			self.playerOwnerTowerId[playerId][towerId] = count - 1;
			
			Unit unit = UnitHelper.GetUnit(self.DomainScene(), playerId);
			TeamFlagType teamFlagType = unit.GetComponent<TeamFlagObj>().GetTeamFlagType();
			float3 forward = new float3(0, 0, 1);
			Unit towerUnit = UnitHelper_Create.CreateWhenServer_TowerUnit(self.DomainScene(), towerId, teamFlagType, position, forward);

			self.unitId2TowerCfgId[towerUnit.Id] = towerId;
			
			self.NoticeToClient(playerId);
		}

	}
}