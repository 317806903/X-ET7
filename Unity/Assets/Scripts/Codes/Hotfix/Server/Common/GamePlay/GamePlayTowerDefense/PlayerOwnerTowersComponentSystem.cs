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
			self.initPlayerOwnerTowerCard = false;
			await self.InitTowerPool();
			await self.InitSkillPool();
			await self.InitMonsterCallPool();
			self.initPlayerOwnerTowerCard = true;
		}

		public static async ETTask InitTowerPool(this PlayerOwnerTowersComponent self)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
			TowerDefense_BuyTowerRefreshRuleCfg towerDefense_BuyTowerRefreshRuleCfg = gamePlayTowerDefenseComponent.model.BuyTowerRefreshRuleCfgId_Ref;
			if (towerDefense_BuyTowerRefreshRuleCfg.IsUseMyTowers == false)
			{
				return;
			}

			List<long> playerList = gamePlayTowerDefenseComponent.GetPlayerList();
			for (int i = 0; i < playerList.Count; i++)
			{
				long playerId = playerList[i];
				await self.InitOwnerTowerPool(playerId);
			}
		}

		public static async ETTask InitOwnerTowerPool(this PlayerOwnerTowersComponent self, long playerId)
		{
			List<ItemComponent> itemList = await ET.Server.PlayerCacheHelper.GetBattleTowerItemListByPlayerId(self.DomainScene(), playerId, true);
			foreach (var itemComponent in itemList)
			{
				ItemTowerComponent itemTowerComponent = itemComponent.GetComponent<ItemTowerComponent>();
				self.playerOwnerTowerCardIds.Add(playerId, itemComponent.CfgId, itemTowerComponent.level);
			}
		}

		public static async ETTask InitSkillPool(this PlayerOwnerTowersComponent self)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
			TowerDefense_BuyTowerRefreshRuleCfg towerDefense_BuyTowerRefreshRuleCfg = gamePlayTowerDefenseComponent.model.BuyTowerRefreshRuleCfgId_Ref;
			if (towerDefense_BuyTowerRefreshRuleCfg.IsUseMySkills == false)
			{
				return;
			}

			List<long> playerList = gamePlayTowerDefenseComponent.GetPlayerList();
			for (int i = 0; i < playerList.Count; i++)
			{
				long playerId = playerList[i];
				await self.InitOwnerSkillPool(playerId);
			}

		}

		public static async ETTask InitOwnerSkillPool(this PlayerOwnerTowersComponent self, long playerId)
		{
			List<ItemComponent> itemList = await ET.Server.PlayerCacheHelper.GetBattleSkillItemListByPlayerId(self.DomainScene(), playerId, true);
			foreach (var itemComponent in itemList)
			{
				ItemSkillComponent itemSkillComponent = itemComponent.GetComponent<ItemSkillComponent>();
				self.playerOwnerTowerCardIds.Add(playerId, itemComponent.CfgId, itemSkillComponent.level);
			}
		}


		public static async ETTask InitMonsterCallPool(this PlayerOwnerTowersComponent self)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
			TowerDefense_BuyTowerRefreshRuleCfg towerDefense_BuyTowerRefreshRuleCfg = gamePlayTowerDefenseComponent.model.BuyTowerRefreshRuleCfgId_Ref;
			if (towerDefense_BuyTowerRefreshRuleCfg.IsUseMyMonsterCalls == false)
			{
				return;
			}

			List<long> playerList = gamePlayTowerDefenseComponent.GetPlayerList();
			for (int i = 0; i < playerList.Count; i++)
			{
				long playerId = playerList[i];
				await self.InitOwnerMonsterCallPool(playerId);
			}
		}

		public static async ETTask InitOwnerMonsterCallPool(this PlayerOwnerTowersComponent self, long playerId)
		{
			List<ItemComponent> itemList = await ET.Server.PlayerCacheHelper.GetBattleMonsterCallItemListByPlayerId(self.DomainScene(), playerId, true);
			foreach (var itemComponent in itemList)
			{
				ItemTowerComponent itemTowerComponent = itemComponent.GetComponent<ItemTowerComponent>();
				self.playerOwnerTowerCardIds.Add(playerId, itemComponent.CfgId, itemTowerComponent.level);
			}
		}

	}
}