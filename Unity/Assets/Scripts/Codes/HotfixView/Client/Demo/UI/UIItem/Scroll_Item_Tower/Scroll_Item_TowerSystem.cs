using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(Scroll_Item_Tower))]
	public static class Scroll_Item_TowerSystem
	{
		public static void Init(this Scroll_Item_Tower self)
		{
		}

		public static async ETTask ShowBagItem(this Scroll_Item_Tower self, string itemCfgId, bool needClickShowDetail)
		{
			ET.EventTriggerListener.Get(self.EButton_SelectButton.gameObject).RemoveAllListeners();
			if (needClickShowDetail)
			{
				ET.EventTriggerListener.Get(self.EButton_SelectButton.gameObject).onClick.AddListener((go, xx) =>
				{
					UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);
					self.ShowDetails(itemCfgId);
				});
			}

			self.ELabel_NameTextMeshProUGUI.text = ItemHelper.GetItemName(itemCfgId);
			await self.EButton_TowerIcoImage.SetImageByItemCfgId(self, itemCfgId);

			self.EG_IconStarRectTransform.SetVisible(false);
			self.EImage_Label1Image.gameObject.SetActive(false);
			self.EImage_Label2Image.gameObject.SetActive(false);
			if (ItemHelper.ChkIsTower(itemCfgId))
			{
				self.SetLevel(itemCfgId);
				self.SetLabels(itemCfgId);
			}
			self.SetQuality(itemCfgId);
		}

		public static void ShowDetails(this Scroll_Item_Tower self, string itemCfgId)
		{
			if (string.IsNullOrEmpty(itemCfgId))
			{
				return;
			}

			Vector3 pos = ET.Client.EUIHelper.GetRectTransformMidTop(self.uiTransform.GetComponent<RectTransform>());
			ET.Client.UIManagerHelper.ShowItemInfoWnd(self.DomainScene(), itemCfgId, pos);
		}

		public static void SetQuality(this Scroll_Item_Tower self, string itemCfgId)
		{
			int towerQuality = (int)ItemHelper.GetItemQualityType(itemCfgId);
			self.EImage_LowImage.SetVisible(towerQuality == 0);
			self.EImage_MiddleImage.SetVisible(towerQuality == 1);
			self.EImage_HighImage.SetVisible(towerQuality == 2);
		}

		public static void SetLabels(this Scroll_Item_Tower self, string itemCfgId)
		{
			List<string> labels = ItemHelper.GetTowerItemLabels(itemCfgId);
			int labelCount = labels.Count;
			self.EImage_Label1Image.gameObject.SetActive((labelCount>=1));
			self.EImage_Label2Image.gameObject.SetActive((labelCount>=2));
			if (labelCount >= 1)
			{
				self.ELabel_Label1TextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValueByExcel(labels[0]);
			}
			if (labelCount >= 2)
			{
				self.ELabel_Label2TextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValueByExcel(labels[1]);
			}
		}

		public static void SetLevel(this Scroll_Item_Tower self, string itemCfgId)
		{
			self.EG_IconStarRectTransform.SetVisible(true);
			int starCount = (int)ItemHelper.GetTowerItemQualityRank(itemCfgId);
			self.E_IconStar1Image.gameObject.SetActive(starCount>=1);
			self.E_IconStar2Image.gameObject.SetActive(starCount>=2);
			self.E_IconStar3Image.gameObject.SetActive(starCount>=3);
		}
	}
}
