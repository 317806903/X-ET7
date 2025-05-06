using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(Scroll_Item_TowerBattleBuy))]
	public static class Scroll_Item_TowerBattleBuySystem
	{
		public static void RegisterUIEvent(this Scroll_Item_TowerBattleBuy self)
		{
		}

		public static void HideItem(this Scroll_Item_TowerBattleBuy self)
		{
		}


        public static async ETTask Init(this Scroll_Item_TowerBattleBuy self, string itemCfgId, bool needClickShowDetail, int itemNum = 0)
        {
	        await self.ES_CommonItem.Init(itemCfgId, needClickShowDetail, false, false);
	        self.ES_CommonItem.SetItemNum(itemNum);
		}

        public static GameObject GetActionButton(this Scroll_Item_TowerBattleBuy self)
        {
	        return self.ES_CommonItem.GetActionButton();
        }

		public static void SetBuyStatus(this Scroll_Item_TowerBattleBuy self, bool isBought, int buyTowerCostGold)
		{
			if (isBought)
			{
				self.EImage_PurchasedImage.SetVisible(true);
				self.EG_BuyBGRectTransform.SetVisible(false);
			}
			else
			{
				self.EImage_PurchasedImage.SetVisible(false);

				self.EG_BuyBGRectTransform.SetVisible(true);

				self.ELabel_BuyCostTextMeshProUGUI.ShowCoinCostTextInBattleTower(self.DomainScene(), buyTowerCostGold).Coroutine();
			}
		}
	}
}
