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
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
			List<long> playerList = gamePlayTowerDefenseComponent.GetPlayerList();
			for (int i = 0; i < playerList.Count; i++)
			{
				long playerId = playerList[i];
				self.RefreshPlayerTowerPool(playerId);
				self.playerRefreshTowerCost[playerId] = gamePlayTowerDefenseComponent.model.RefreshBuyTowerCost;
				self.playerOwnerTowerId[playerId] = new();
			}
		}

		public static bool RefreshTowerPool(this PlayerOwnerTowersComponent self, long playerId)
		{
			bool success = self.CostWhenRefresh(playerId);
			if (success == false)
			{
				return false;
			}
			self.RefreshPlayerTowerPool(playerId);
			
			self.NoticeToClient(playerId);
			return true;
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

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
			int count = gamePlayTowerDefenseComponent.model.BuyTowerPoolCount;
			TowerDefense_BuyTowerRefreshRuleCfg towerDefense_BuyTowerRefreshRuleCfg = TowerDefense_BuyTowerRefreshRuleCfgCategory.Instance.Get(gamePlayTowerDefenseComponent.model.BuyTowerRefreshRuleCfgId);
			for (int i = 0; i < count; i++)
			{
				string towerId = ET.RandomGenerator.GetRandomIndexLinear(towerDefense_BuyTowerRefreshRuleCfg.TowerWeight);
				self.playerTowerBuyPools.Add(playerId, towerId);
				self.playerTowerBuyPoolBoughts.Add(playerId, false);
			}
		}

		public static bool CostWhenRefresh(this PlayerOwnerTowersComponent self, long playerId)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
			int chgValue = gamePlayTowerDefenseComponent.model.RefreshBuyTowerCost;
			int curGold = GamePlayHelper.GetPlayerCoin(self.DomainScene(), playerId, CoinType.Gold);
			if (curGold < chgValue)
			{
				return false;
			}
			GamePlayHelper.ChgPlayerCoin(self.DomainScene(), playerId, CoinType.Gold, -chgValue);
			return true;
		}

		public static bool CostWhenBuyTower(this PlayerOwnerTowersComponent self, long playerId, string towerId)
		{
			TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(towerId);
			int chgValue = towerCfg.BuyTowerCostGold;
			int curGold = GamePlayHelper.GetPlayerCoin(self.DomainScene(), playerId, CoinType.Gold);
			if (curGold < chgValue)
			{
				return false;
			}
			GamePlayHelper.ChgPlayerCoin(self.DomainScene(), playerId, CoinType.Gold, -chgValue);
			return true;
		}

		public static bool BuyPlayerTower(this PlayerOwnerTowersComponent self, long playerId, int index)
		{
			if (self.playerTowerBuyPools.ContainsKey(playerId) == false)
			{
				Log.Error($" BuyPlayerTower self.playerTowerPools.ContainsKey(playerId) == false");
				return false;
			}
			if (self.playerTowerBuyPools[playerId].Count <= index)
			{
				Log.Error($" BuyPlayerTower self.playerTowerPools[playerId].Count <= index");
				return false;
			}
			
			bool isBought = self.playerTowerBuyPoolBoughts[playerId][index];
			if (isBought)
			{
				Log.Error($" BuyPlayerTower isBought==true");
				return false;
			}

			string towerId = self.playerTowerBuyPools[playerId][index];
			
			bool success = self.CostWhenBuyTower(playerId, towerId);
			if (success == false)
			{
				return false;
			}
			
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
			return true;
		}

		public static void NoticeToClient(this PlayerOwnerTowersComponent self, long playerId)
		{
			self.GetParent<GamePlayTowerDefenseComponent>().NoticeToClient(playerId);
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

		public static Unit CreateTower(this PlayerOwnerTowersComponent self, long playerId, string towerId, float3 position)
		{
			return GamePlayTowerDefenseHelper.CreateTower(self.DomainScene(), playerId, towerId, position);
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

			Unit towerUnit = self.CreateTower(playerId, towerId, position);

			self.unitId2TowerCfgId[towerUnit.Id] = towerId;
			
			self.NoticeToClient(playerId);
		}

	}
}