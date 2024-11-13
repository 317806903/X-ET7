using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(Scroll_Item_BattleDeckTower))]
	public static class Scroll_Item_BattleDeckTowerSystem
	{
		public static void RegisterUIEvent(this Scroll_Item_BattleDeckTower self)
		{
		}

		public static void HideItem(this Scroll_Item_BattleDeckTower self)
		{
		}

        public static async ETTask Init(this Scroll_Item_BattleDeckTower self, string itemCfgId, bool needClickShowDetail, bool isLock)
        {
	        self.itemCfgId = itemCfgId;
	        if (string.IsNullOrEmpty(itemCfgId))
	        {
		        self.E_NoneImage.SetVisible(true);
		        self.ES_CommonItem.View.uiTransform.SetVisible(false);
		        return;
	        }
	        self.E_NoneImage.SetVisible(false);

	        await self.ES_CommonItem.Init(itemCfgId, needClickShowDetail, true, isLock);
	        self.ShowLock(isLock);
		}

        public static GameObject GetActionButton(this Scroll_Item_BattleDeckTower self)
        {
	        return self.ES_CommonItem.GetActionButton();
        }

		public static void ShowLock(this Scroll_Item_BattleDeckTower self, bool isLock)
		{
			if (string.IsNullOrEmpty(self.itemCfgId))
			{
				self.EG_LockRectTransform.SetVisible(false);
				return;
			}
			self.EG_LockRectTransform.SetVisible(isLock);
		}

		public static void ShowRedDot(this Scroll_Item_BattleDeckTower self, bool isShowRedDot)
		{
			if (string.IsNullOrEmpty(self.itemCfgId))
			{
				self.E_RedDotImage.SetVisible(false);
				return;
			}
			self.ES_CommonItem.AddExtendClickAction(() =>
			{
				if (isShowRedDot)
				{
					UIManagerHelper.HideUIRedDot(self.DomainScene(), UIRedDotType.None, self.itemCfgId).Coroutine();
					self.E_RedDotImage.SetVisible(false);
					isShowRedDot = false;
				}
			});

			self.E_RedDotImage.SetVisible(isShowRedDot);
		}
	}
}
