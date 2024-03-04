using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(Scroll_Item_TowerIcon))]
	public static class Scroll_Item_TowerIconSystem
	{
		public static void Init(this Scroll_Item_TowerIcon self)
		{
		}

		public static async ETTask SetIconAndLevel(this Scroll_Item_TowerIcon self, string itemCfgId, int index)
		{
			string curItemCfgId = itemCfgId.Substring(0, itemCfgId.Length - 1) + index.ToString();
			self.EG_IconStarRectTransform.SetVisible(true);
			int count = (int)ItemHelper.GetTowerItemQualityRank(curItemCfgId);
			self.E_IconStar1Image.SetVisible(count >= 1);
			self.E_IconStar2Image.SetVisible(count >= 2);
			self.E_IconStar3Image.SetVisible(count >= 3);
			await self.EImage_IconImage.SetImageByPath(ItemHelper.GetItemIcon(curItemCfgId));
		}

	}
}
