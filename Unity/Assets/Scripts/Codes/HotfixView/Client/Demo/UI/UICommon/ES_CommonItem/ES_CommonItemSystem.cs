using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(ES_CommonItem))]
	public static class ES_CommonItemSystem
	{
		public static void RegisterUIEvent(this ES_CommonItem self)
		{
		}

		public static void ShowCommonUI(this ES_CommonItem self, ShowWindowData contextData = null)
		{
			self.View.uiTransform.SetVisible(true);

		}

		public static void HideCommonUI(this ES_CommonItem self)
		{
			self.View.uiTransform.SetVisible(false);

		}

		public static async ETTask Init(this ES_CommonItem self, string itemCfgId, bool needClickShowDetail, bool isShowStatus, bool isLock)
		{
			self.View.uiTransform.SetVisible(true);
			self.itemCfgId = itemCfgId;
			self.extendClickAction = null;
			ET.EventTriggerListener.Get(self.View.EButton_SelectWhenCommonButton.gameObject).RemoveAllListeners();

			if (needClickShowDetail)
			{
				self.View.EButton_SelectWhenCommonButton.SetVisible(true);
				ET.EventTriggerListener.Get(self.View.EButton_SelectWhenCommonButton.gameObject).onClick.AddListener((go, xx) =>
				{
					UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);
					if (self.extendClickAction != null)
					{
						self.extendClickAction();
					}
					self.ShowDetails(itemCfgId, isShowStatus, isLock);
				});
			}

			self.View.ELabel_ItemNameTextMeshProUGUI.text = ET.ItemHelper.GetItemName(itemCfgId);
			self.View.E_ItemIconImage.SetImageByItemCfgId(self, itemCfgId).Coroutine();

			self.SetItemQualityRank(itemCfgId);
			self.SetItemLabels(itemCfgId);
			self.SetItemQualityType(itemCfgId);
			self.SetItemNum(0);
		}

		public static void ShowDetails(this ES_CommonItem self, string itemCfgId, bool isShowStatus, bool isLock)
		{
			if (string.IsNullOrEmpty(itemCfgId))
			{
				return;
			}

			Vector3 pos = ET.Client.EUIHelper.GetRectTransformMidTop(self.View.uiTransform.GetComponent<RectTransform>());
			ET.Client.UIManagerHelper.ShowItemInfoWnd(self.DomainScene(), itemCfgId, pos, isShowStatus, isLock);
		}

		public static GameObject GetActionButton(this ES_CommonItem self)
		{
			return self.View.EButton_SelectWhenCommonButton.gameObject;
		}

		public static void AddExtendClickAction(this ES_CommonItem self, Action extendClickAction)
		{
			self.extendClickAction = extendClickAction;
		}

		public static void SetItemQualityType(this ES_CommonItem self, string itemCfgId)
		{
			int towerQuality = (int)ET.ItemHelper.GetItemQualityType(itemCfgId);
			self.View.EImage_LowImage.SetVisible(towerQuality == 0);
			self.View.EImage_MiddleImage.SetVisible(towerQuality == 1);
			self.View.EImage_HighImage.SetVisible(towerQuality == 2);
		}

		public static void SetItemLabels(this ES_CommonItem self, string itemCfgId)
		{
			if (ET.ItemHelper.ChkIsTower(itemCfgId))
			{
				self.View.EG_ItemLabelRectTransform.SetVisible(true);
				List<string> labels = ET.ItemHelper.GetTowerItemLabels(itemCfgId);
				int labelCount = labels.Count;
				self.View.EImage_Label1Image.gameObject.SetActive((labelCount>=1));
				self.View.EImage_Label2Image.gameObject.SetActive((labelCount>=2));
				if (labelCount >= 1)
				{
					self.View.ELabel_Label1TextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValueByExcel(labels[0]);
				}
				if (labelCount >= 2)
				{
					self.View.ELabel_Label2TextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValueByExcel(labels[1]);
				}
			}
			else
			{
				self.View.EG_ItemLabelRectTransform.SetVisible(false);
			}
		}

		public static void SetItemQualityRank(this ES_CommonItem self, string itemCfgId)
		{
			if (ET.ItemHelper.ChkIsTower(itemCfgId))
			{
				self.View.EG_ItemQualityRankRectTransform.SetVisible(true);
				int starCount = (int)ET.ItemHelper.GetTowerItemQualityRank(itemCfgId);
				self.View.E_IconStar1Image.gameObject.SetActive(starCount>=1);
				self.View.E_IconStar2Image.gameObject.SetActive(starCount>=2);
				self.View.E_IconStar3Image.gameObject.SetActive(starCount>=3);
			}
			else
			{
				self.View.EG_ItemQualityRankRectTransform.SetVisible(false);
			}
		}

		public static void SetItemNum(this ES_CommonItem self, int num)
		{
			if (num <= 1)
			{
				self.View.ELabel_ItemNumTextMeshProUGUI.SetVisible(false);
			}
			else
			{
				self.View.ELabel_ItemNumTextMeshProUGUI.SetVisible(true);
				self.View.ELabel_ItemNumTextMeshProUGUI.text = $"{num}";
			}
		}
	}
}
