using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(Scroll_Item_TowerBuy))]
	public static class Scroll_Item_TowerBuySystem
	{
		public static void Init(this Scroll_Item_TowerBuy self)
		{
		}

		public static void SetQuality(this Scroll_Item_TowerBuy self, string itemCfgId)
		{
			int towerQuality = (int)ItemHelper.GetItemQualityType(itemCfgId);
			self.EImage_LowImage.SetVisible(towerQuality == 0);
			self.EImage_MiddleImage.SetVisible(towerQuality == 1);
			self.EImage_HighImage.SetVisible(towerQuality == 2);
		}
		
		public static void SetLabels(this Scroll_Item_TowerBuy self, string itemCfgId)
		{
			List<string> labels = ItemHelper.GetTowerItemLabels(itemCfgId);
			int labelCount = labels.Count;
			self.EImage_Label1Image.gameObject.SetActive((labelCount>=1));
			self.EImage_Label2Image.gameObject.SetActive((labelCount>=2));
			if (labelCount >= 1)
			{
				self.ELabel_Label1TextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue(labels[0]);
			}
			if (labelCount >= 2)
			{
				self.ELabel_Label2TextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue(labels[1]);
			}
		}

		public static void SetLevel(this Scroll_Item_TowerBuy self, string itemCfgId)
		{
			self.EG_IconStarRectTransform.SetVisible(true);
			int starCount = (int)ItemHelper.GetTowerItemQualityRank(itemCfgId);
			self.E_IconStar1Image.gameObject.SetActive(starCount>=1);
			self.E_IconStar2Image.gameObject.SetActive(starCount>=2);
			self.E_IconStar3Image.gameObject.SetActive(starCount>=3);
		}
	}
}
