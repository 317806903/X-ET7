using ET.Ability;
using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Server
{
    [FriendOf(typeof(ItemUpgradeComponent))]
    public static class ItemUpgradeComponentSystem
	{
		[ObjectSystem]
		public class ItemUpgradeComponentAwakeSystem : AwakeSystem<ItemUpgradeComponent>
		{
			protected override void Awake(ItemUpgradeComponent self)
			{
				self.SetMaxUpgradeLevel().Coroutine();
			}
		}

		public static async ETTask SetMaxUpgradeLevel(this ItemUpgradeComponent self)
		{
			await TimerComponent.Instance.WaitFrameAsync();
			if (self.IsDisposed)
			{
				return;
			}

			if (ET.ItemHelper.ChkIsHeadQuarter(self.itemCfgId))
			{
				int curLevel = GlobalSettingCfgCategory.Instance.HeadQuarterLevelWhenInBattle;
				self.maxUpgradeLevel = curLevel;
			}
			else
			{
				PlayerBackPackComponent playerBackPackComponent = await PlayerCacheHelper.GetPlayerBackPackByPlayerId(self.DomainScene(), self.playerId);
				if (self.IsDisposed)
				{
					return;
				}
				int curLevel = playerBackPackComponent.GetItemLevelWhenStack(self.itemCfgId);
				self.maxUpgradeLevel = curLevel;
			}

			self.ResetUpgradeCostInfo();

			Ability.UnitHelper.AddSyncData_UnitComponent(self.GetUnit(), self.GetType());
		}

	}
}