using System.Collections;
using System.Collections.Generic;
using System;
using ET.AbilityConfig;
using SuperScrollView;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgItemDetails))]
	public static class DlgItemDetailsSystem
	{
		public static void RegisterUIEvent(this DlgItemDetails self)
		{
			self.View.EButton_DetailBgButton.AddListener(self.OnDetailBgButton);
			self.View.EButton_LeftButton.AddListener(self.SwitchToPreItem);
			self.View.EButton_RightButton.AddListener(self.SwitchToNextItem);

			self.View.E_ScrollView_IconLoopListView2.InitListView(0, self.OnGetItemByIndex);
			self.View.E_ScrollView_IconLoopListView2.mOnSnapNearestChanged = self.SwitchSpriteCallback;
		}

		public static async ETTask ShowWindow(this DlgItemDetails self, ShowWindowData contextData = null)
		{
			self.dlgShowTime = TimeHelper.ClientNow();
			//self.AddUIScrollItems(ref self.ScrollTowerIcon, 3);
			//self.ShowDetails(self.curItemCfgId).Coroutine();
		}

		public static bool ChkCanClickBg(this DlgItemDetails self)
		{
			if (self.dlgShowTime < TimeHelper.ClientNow() - (long)(1000 * 0.5f))
			{
				return true;
			}
			return false;
		}

		public static void SetCurItemCfgId(this DlgItemDetails self, string itemCfgId)
		{
			self.baseItemCfgId = itemCfgId;
			self.curItemCfgId = itemCfgId;
			self.curItemIndex = 0;

			int count = TowerDefense_TowerCfgCategory.Instance.GetTowerMaxLevel(self.baseItemCfgId);
			self.AddUIScrollItems(ref self.ScrollTowerIcon, count);
			if (self.View.E_ScrollView_IconLoopListView2.ItemTotalCount == count)
			{
				self.View.E_ScrollView_IconLoopListView2.MovePanelToItemIndex(0, 0);
			}
			else
			{
				self.View.E_ScrollView_IconLoopListView2.SetListItemCount(count, true);
			}
		}

		public static async ETTask ShowDetails(this DlgItemDetails self)
		{
			string itemCfgId = self.curItemCfgId;
			int itemIndex = self.curItemIndex;

			self.View.EImage_IconImage.SetVisible(false);
			self.View.E_ScrollView_IconLoopListView2.gameObject.SetActive(false);
			self.View.ELabel_NameTextMeshProUGUI.text = ItemHelper.GetItemName(itemCfgId);
			self.View.ELabel_DescriptionTextMeshProUGUI.text = ItemHelper.GetItemDesc(itemCfgId);

			if (ItemHelper.ChkIsTower(itemCfgId))
			{
				self.View.E_ScrollView_IconLoopListView2.gameObject.SetActive(true);

				List<string> labels = ItemHelper.GetTowerItemLabels(itemCfgId);
				int labelCount = labels.Count;
				self.View.EImage_Label1Image.gameObject.SetActive(labelCount >= 1);
				self.View.EImage_Label2Image.gameObject.SetActive(labelCount >= 2);
				if (labelCount >= 1)
				{
					self.View.ELabel_Label1TextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValueByExcel(labels[0]);
				}

				if (labelCount >= 2)
				{
					self.View.ELabel_Label2TextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValueByExcel(labels[1]);
				}

				LayoutRebuilder.ForceRebuildLayoutImmediate(self.View.EImage_Label1Image.transform.parent.GetComponent<RectTransform>());

				int towerQuality = (int)ItemHelper.GetItemQualityType(itemCfgId);
				self.View.EImage_BoxLowImage.SetVisible(towerQuality == 0);
				self.View.EImage_BoxMiddleImage.SetVisible(towerQuality == 1);
				self.View.EImage_BoxHighImage.SetVisible(towerQuality == 2);
			}
			else
			{
				self.View.EImage_IconImage.SetVisible(true);
				await self.View.EButton_TowerIcoImage.SetImageByItemCfgId(self, itemCfgId);
			}
		}

		public static LoopListViewItem2 OnGetItemByIndex(this DlgItemDetails self, LoopListView2 listView, int index)
		{
			//Log.Error($"---zpb OnGetItemByIndex {index}");

			LoopListViewItem2 item = listView.NewListViewItem("Item_TowerIcon");
			Scroll_Item_TowerIcon itemm = self.ScrollTowerIcon[index].BindTrans(item.transform);

			string curItemCfgId = ItemHelper.GetTowerItemNextTowerConfigId(self.baseItemCfgId, index);
			itemm.SetIconAndLevel(curItemCfgId).Coroutine();
			return item;
		}

		public static void SwitchSpriteCallback(this DlgItemDetails self, LoopListView2 loopListView2 = null, LoopListViewItem2 item = null)
		{
			//Log.Error($"---zpb SwitchSpriteCallback {item.ItemIndex}");
			self.curItemCfgId = ItemHelper.GetTowerItemNextTowerConfigId(self.baseItemCfgId, item.ItemIndex);
			self.curItemIndex = item.ItemIndex;

			self.View.EButton_LeftButton.SetVisible(false);
			self.View.EButton_RightButton.SetVisible(false);
			string preItemCfgId = ItemHelper.GetTowerItemPreTowerConfigId(self.curItemCfgId);
			if (string.IsNullOrEmpty(preItemCfgId) == false)
			{
				self.View.EButton_LeftButton.SetVisible(true);
			}
			string nextItemCfgId = ItemHelper.GetTowerItemNextTowerConfigId(self.curItemCfgId);
			if (string.IsNullOrEmpty(nextItemCfgId) == false)
			{
				self.View.EButton_RightButton.SetVisible(true);
			}

			TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(self.curItemCfgId);

			var attributeList = ItemHelper.GetTowerAttribute(self.curItemCfgId, towerCfg.Level[0]);
			self.View.ENode_Attribute1Image.SetVisible(attributeList.Count >= 1);
			self.View.ENode_Attribute2Image.SetVisible(attributeList.Count >= 2);
			self.View.ENode_Attribute3Image.SetVisible(attributeList.Count >= 3);
			self.View.ENode_AttributeLine2Image.SetVisible(attributeList.Count >= 2);
			self.View.ENode_AttributeLine3Image.SetVisible(attributeList.Count >= 3);
			if (attributeList.Count >= 1)
			{
				self.View.Elabel_Attribute1TextMeshProUGUI.text = attributeList[0].title;
				self.View.Elabel_AttributeValue1TextMeshProUGUI.text = attributeList[0].content;
			}
			if (attributeList.Count >= 2)
			{
				self.View.Elabel_Attribute2TextMeshProUGUI.text = attributeList[1].title;
				self.View.Elabel_AttributeValue2TextMeshProUGUI.text = attributeList[1].content;
			}
			if (attributeList.Count >= 3)
			{
				self.View.Elabel_Attribute3TextMeshProUGUI.text = attributeList[2].title;
				self.View.Elabel_AttributeValue3TextMeshProUGUI.text = attributeList[2].content;
			}

			self.ShowDetails().Coroutine();

		}

		public static void SwitchToPreItem(this DlgItemDetails self)
		{
			self.View.E_ScrollView_IconLoopListView2.MovePanelToItemIndex(self.curItemIndex - 1, 0);
		}

		public static void SwitchToNextItem(this DlgItemDetails self)
		{
			self.View.E_ScrollView_IconLoopListView2.MovePanelToItemIndex(self.curItemIndex + 1, 0);
		}

		public static void OnDetailBgButton(this DlgItemDetails self)
		{
			if (self.ChkCanClickBg() == false)
			{
				return;
			}
			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgItemDetails>();
		}

	}
}
