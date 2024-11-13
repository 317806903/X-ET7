using ET.Ability;
using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Server
{
    [FriendOf(typeof(PlayerOwnerTowersComponent))]
    public static class PlayerOwnerTowersComponentSystem
	{
		[ObjectSystem]
		public class PlayerOwnerTowersComponentAwakeSystem : AwakeSystem<PlayerOwnerTowersComponent>
		{
			protected override void Awake(PlayerOwnerTowersComponent self)
			{
				self.playerOwnerTowerCardIds = new();
				self.Init().Coroutine();
			}
		}

		[ObjectSystem]
		public class PlayerOwnerTowersComponentDestroySystem : DestroySystem<PlayerOwnerTowersComponent>
		{
			protected override void Destroy(PlayerOwnerTowersComponent self)
			{
				self.playerOwnerTowerCardIds.Clear();
			}
		}

		public static async ETTask Init(this PlayerOwnerTowersComponent self)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
			TowerDefense_BuyTowerRefreshRuleCfg towerDefense_BuyTowerRefreshRuleCfg = gamePlayTowerDefenseComponent.model.BuyTowerRefreshRuleCfgId_Ref;
			if (towerDefense_BuyTowerRefreshRuleCfg.IsUseMyTowers == false)
			{
				self.initPlayerOwnerTowerCard = true;
				return;
			}

			List<long> playerList = gamePlayTowerDefenseComponent.GetPlayerList();
			for (int i = 0; i < playerList.Count; i++)
			{
				long playerId = playerList[i];
				await self.InitOwnerTowersPool(playerId);
			}

			self.initPlayerOwnerTowerCard = true;
		}

		public static async ETTask InitOwnerTowersPool(this PlayerOwnerTowersComponent self, long playerId)
		{
			List<ItemComponent> itemList = await ET.Server.PlayerCacheHelper.GetBattleTowerItemListByPlayerId(self.DomainScene(), playerId, true);
			foreach (var itemComponent in itemList)
			{
				ItemTowerComponent itemTowerComponent = itemComponent.GetComponent<ItemTowerComponent>();
				self.playerOwnerTowerCardIds.Add(playerId, itemComponent.CfgId, itemTowerComponent.level);
			}
		}
	}
}