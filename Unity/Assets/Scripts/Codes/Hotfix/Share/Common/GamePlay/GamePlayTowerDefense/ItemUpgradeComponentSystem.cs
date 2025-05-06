using ET.Ability;
using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof(ItemUpgradeComponent))]
    public static class ItemUpgradeComponentSystem
	{
		[ObjectSystem]
		public class ItemUpgradeComponentAwakeSystem : AwakeSystem<ItemUpgradeComponent>
		{
			protected override void Awake(ItemUpgradeComponent self)
			{
				self.chooseItemGiftDic = new();
			}
		}

		[ObjectSystem]
		public class ItemUpgradeComponentDestroySystem : DestroySystem<ItemUpgradeComponent>
		{
			protected override void Destroy(ItemUpgradeComponent self)
			{
			}
		}

		public static void Init(this ItemUpgradeComponent self, long playerId, string itemCfgId, int curLevel)
		{
			self.playerId = playerId;
			self.itemCfgId = itemCfgId;
			self.curLevel = curLevel;
		}

		public static void ResetUpgradeCostInfo(this ItemUpgradeComponent self)
		{
			ItemUpgradeCfg itemUpgradeCfg = ItemHelper.GetItemUpgradeInfo(self.itemCfgId, self.curLevel);
			if (itemUpgradeCfg == null)
			{
				self.upgradeCostCardWhenInGame = 0;
				self.upgradeCostGoldWhenInGame = 0;
				return;
			}

			self.upgradeCostCardWhenInGame = itemUpgradeCfg.UpgradeCostCardWhenInGame;
			long playerId = TeamFlagHelper.GetPlayerId(self.GetUnit());
			TeamFlagType teamFlagType = TeamFlagHelper.GetTeamFlag(self.GetUnit());
			if (playerId != (long)ET.PlayerId.PlayerNone)
			{
				float costGold = itemUpgradeCfg.UpgradeCostGoldWhenInGame;
				GamePlayHelper.ChgGamePlayNumericValueByPlayerId(self.DomainScene(), playerId, GameNumericType.TowerDefense_UpgradeItemUnitPriceBase, costGold, true);
				float newCostGold = GamePlayHelper.GetGamePlayNumericValueByPlayerId(self.DomainScene(), playerId, GameNumericType.TowerDefense_UpgradeItemUnitPrice);

				self.upgradeCostGoldWhenInGame = (int)newCostGold;
			}
			else
			{
				TeamFlagType homeTeamFlagType = GamePlayHelper.GetHomeTeamFlagType(teamFlagType);
				float costGold = itemUpgradeCfg.UpgradeCostGoldWhenInGame;
				GamePlayHelper.ChgGamePlayNumericValueByHomeTeamFlagType(self.DomainScene(), homeTeamFlagType, GameNumericType.TowerDefense_UpgradeItemUnitPriceBase, costGold, true);
				float newCostGold = GamePlayHelper.GetGamePlayNumericValueByHomeTeamFlagType(self.DomainScene(), homeTeamFlagType, GameNumericType.TowerDefense_UpgradeItemUnitPrice);

				self.upgradeCostGoldWhenInGame = (int)newCostGold;
			}
		}

		public static Unit GetUnit(this ItemUpgradeComponent self)
		{
			return self.GetParent<Unit>();
		}

		public static bool IsAllowFirstLevelNull(this ItemUpgradeComponent self)
		{
			return true;
		}

		public static (bool bRet, string msg) ChkCanUpgrade(this ItemUpgradeComponent self, int nextLevel, string itemGiftCfgId)
		{
			if (nextLevel > self.maxUpgradeLevel)
			{
				return (false, $"nextLevel[{nextLevel}] > self.maxUpgradeLevel[{self.maxUpgradeLevel}]");
			}

			if (nextLevel < self.curLevel)
			{
				return (false, $"nextLevel[{nextLevel}] < self.curLevel[{self.curLevel}]");
			}
			if (nextLevel > self.curLevel + 1)
			{
				return (false, $"nextLevel[{nextLevel}] > self.curLevel[{self.curLevel}] + 1");
			}

			if (self.IsAllowFirstLevelNull())
			{
				if (nextLevel == self.curLevel)
				{
					return (false, $"nextLevel[{nextLevel}] == self.curLevel[{self.curLevel}]");
				}
			}
			else
			{
				if (nextLevel == self.curLevel)
				{
					if (self.chooseItemGiftDic.ContainsKey(self.curLevel))
					{
						return (false, $"nextLevel[{nextLevel}] already choosed");
					}
				}
			}

			if (ItemGiftCfgCategory.Instance.Contain(itemGiftCfgId) == false)
			{
				return (false, $"itemGiftCfgId[{itemGiftCfgId}] not exist");
			}

			ItemUpgradeCfg itemUpgradeCfg = ItemHelper.GetItemUpgradeInfo(self.itemCfgId, nextLevel);
			if (itemUpgradeCfg.UpgradeItemGifts.Contains(itemGiftCfgId) == false)
			{
				return (false, $"itemCfgId[{self.itemCfgId}] nextLevel[{nextLevel}] itemGiftCfgId[{itemGiftCfgId}] not exist");
			}

			return (true, "");
		}

		public static (int costGold, int costCard) GetUpgradeCost(this ItemUpgradeComponent self)
		{
			if (self.IsAllowFirstLevelNull())
			{
				ItemUpgradeCfg itemUpgradeCfg = ItemHelper.GetItemUpgradeInfo(self.itemCfgId, self.curLevel + 1);
				if (itemUpgradeCfg == null)
				{
					return (0, 0);
				}
				return (self.upgradeCostGoldWhenInGame, self.upgradeCostCardWhenInGame);
			}
			else
			{
				if (self.chooseItemGiftDic.ContainsKey(self.curLevel))
				{
					ItemUpgradeCfg itemUpgradeCfg = ItemHelper.GetItemUpgradeInfo(self.itemCfgId, self.curLevel + 1);
					if (itemUpgradeCfg == null)
					{
						return (0, 0);
					}
					return (self.upgradeCostGoldWhenInGame, self.upgradeCostCardWhenInGame);
				}
				else
				{
					return (0, 0);
				}
			}
		}

		public static void Upgrade(this ItemUpgradeComponent self, int nextLevel, string itemGiftCfgId)
		{
			self.curLevel = nextLevel;
			self.chooseItemGiftDic[nextLevel] = itemGiftCfgId;

			self.ResetUpgradeCostInfo();
		}

		public static bool IsMaxUpgrade(this ItemUpgradeComponent self)
		{
			if (self.maxUpgradeLevel == self.curLevel)
			{
				return true;
			}

			return false;
		}
	}
}