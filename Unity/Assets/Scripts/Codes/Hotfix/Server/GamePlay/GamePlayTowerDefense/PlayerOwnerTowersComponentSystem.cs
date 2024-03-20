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
			List<long> playerList = gamePlayTowerDefenseComponent.GetPlayerList();
			for (int i = 0; i < playerList.Count; i++)
			{
				long playerId = playerList[i];
				await self.InitOwnerTowersPool(playerId);
			}
		}

		public static async ETTask InitOwnerTowersPool(this PlayerOwnerTowersComponent self, long playerId)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
			TowerDefense_BuyTowerRefreshRuleCfg towerDefense_BuyTowerRefreshRuleCfg = TowerDefense_BuyTowerRefreshRuleCfgCategory.Instance.Get(gamePlayTowerDefenseComponent.model.BuyTowerRefreshRuleCfgId);
			if (towerDefense_BuyTowerRefreshRuleCfg.IsUseMyTowers == false)
			{
				return;
			}

			PlayerBattleCardComponent playerBattleCardComponent = await ET.Server.PlayerCacheHelper.GetPlayerBattleCardByPlayerId(self.DomainScene(), playerId, true);
			PlayerBackPackComponent playerBackPackComponent = await ET.Server.PlayerCacheHelper.GetPlayerBackPackByPlayerId(self.DomainScene(), playerId, true);
			foreach (var itemId in playerBattleCardComponent.itemList)
			{
				ItemComponent itemComponent = playerBackPackComponent.GetItem(itemId);
				ItemTowerComponent itemTowerComponent = itemComponent.GetComponent<ItemTowerComponent>();
				self.playerOwnerTowerCardIds.Add(playerId, itemComponent.CfgId, itemTowerComponent.level);
			}
		}

	}
}