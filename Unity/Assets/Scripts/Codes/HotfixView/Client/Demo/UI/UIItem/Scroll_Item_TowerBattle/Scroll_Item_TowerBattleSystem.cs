using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(Scroll_Item_TowerBattle))]
	public static class Scroll_Item_TowerBattleSystem
	{
		public static void RegisterUIEvent(this Scroll_Item_TowerBattle self)
		{
		}

		public static void HideItem(this Scroll_Item_TowerBattle self)
		{
		}

		public static async ETTask Init(this Scroll_Item_TowerBattle self, string itemCfgId, bool needClickShowDetail)
		{
			await self.ES_CommonItem.Init(itemCfgId, needClickShowDetail, false, false);
		}

		public static void SetItemNum(this Scroll_Item_TowerBattle self, int num)
		{
			self.ES_CommonItem.SetItemNum(num);
		}

		public static GameObject GetActionButton(this Scroll_Item_TowerBattle self)
		{
			return self.ES_CommonItem.GetActionButton();
		}

	}
}
