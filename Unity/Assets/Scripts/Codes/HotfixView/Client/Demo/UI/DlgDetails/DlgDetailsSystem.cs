using System.Collections;
using System.Collections.Generic;
using System;
using SuperScrollView;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgDetails))]
	public static class DlgDetailsSystem
	{
		public static void RegisterUIEvent(this DlgDetails self)
		{
			self.View.EButton_DetailBgButton.AddListener(self.OnDetailBgButton);
			self.View.EButton_LeftButton.AddListener(self.SwitchToPreItem);
			self.View.EButton_RightButton.AddListener(self.SwitchToNextItem);
			
			self.View.E_ScrollView_IconLoopListView2.InitListView(3, self.OnGetItemByIndex);
			self.View.E_ScrollView_IconLoopListView2.mOnSnapNearestChanged = self.SwitchSpriteCallback;
		}

		public static void ShowWindow(this DlgDetails self, ShowWindowData contextData = null)
		{
			self.AddUIScrollItems(ref self.ScrollTowerIcon, 3);
			//self.ShowDetails(self.curItemCfgId).Coroutine();
		}

		public static void SetCurItemCfgId(this DlgDetails self, string itemCfgId)
		{
			self.curItemCfgId = itemCfgId;
			self.ShowDetails(self.curItemCfgId).Coroutine();
		}

		public static async ETTask ShowDetails(this DlgDetails self, string itemCfgId)
		{
			self.View.EImage_IconImage.SetVisible(false);
			self.View.E_ScrollView_IconLoopListView2.gameObject.SetActive(false);
			self.View.ELabel_NameTextMeshProUGUI.text = ItemHelper.GetItemName(itemCfgId);
			self.View.ELabel_DescriptionTextMeshProUGUI.text = ItemHelper.GetItemDesc(itemCfgId);

			if (ItemHelper.ChkIsTower(itemCfgId))
			{
				self.View.E_ScrollView_IconLoopListView2.gameObject.SetActive(true);
				//self.View.E_ScrollView_IconLoopListView2.RefreshAllShownItem();
				int index = int.Parse(self.curItemCfgId.Substring(self.curItemCfgId.Length - 1, 1));
				self.View.E_ScrollView_IconLoopListView2.MovePanelToItemIndex(index - 1, 0);
				List<string> labels = ItemHelper.GetTowerItemLabels(itemCfgId);
				int labelCount = labels.Count;
				self.View.EImage_Label1Image.gameObject.SetActive(labelCount >= 1);
				self.View.EImage_Label2Image.gameObject.SetActive(labelCount >= 2);
				if (labelCount >= 1)
				{
					self.View.ELabel_Label1TextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue(labels[0]);
				}

				if (labelCount >= 2)
				{
					self.View.ELabel_Label2TextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue(labels[1]);
				}

				LayoutRebuilder.ForceRebuildLayoutImmediate(self.View.EImage_Label1Image.transform.parent.GetComponent<RectTransform>());

				// self.View.EG_IconStarRectTransform.SetVisible(true);
				// int starCount = (int)ItemHelper.GetTowerItemQualityRank(itemCfgId);
				// self.View.E_IconStar1Image.gameObject.SetActive(starCount >= 1);
				// self.View.E_IconStar2Image.gameObject.SetActive(starCount >= 2);
				// self.View.E_IconStar3Image.gameObject.SetActive(starCount >= 3);

				int towerQuality = (int)ItemHelper.GetItemQualityType(itemCfgId);
				self.View.EImage_BoxLowImage.SetVisible(towerQuality == 0);
				self.View.EImage_BoxMiddleImage.SetVisible(towerQuality == 1);
				self.View.EImage_BoxHighImage.SetVisible(towerQuality == 2);
			}
			else
			{
				self.View.EImage_IconImage.SetVisible(true);
				await self.View.EButton_TowerIcoImage.SetImageByPath(ItemHelper.GetItemIcon(itemCfgId));
			}
		}
		
		public static LoopListViewItem2 OnGetItemByIndex(this DlgDetails self, LoopListView2 listView, int index)
		{
			if (index < 0 || index > 3)
			{
				return null;
			}

			LoopListViewItem2 item = listView.NewListViewItem("Item_TowerIcon");
			Scroll_Item_TowerIcon itemm = self.ScrollTowerIcon[index].BindTrans(item.transform);
			itemm.SetIconAndLevel(self.curItemCfgId ,index + 1).Coroutine();
			return item;
		}

		public static void SwitchSpriteCallback(this DlgDetails self, LoopListView2 loopListView2 = null, LoopListViewItem2 item = null)
		{
			int level = self.View.E_ScrollView_IconLoopListView2.CurSnapNearestItemIndex + 1;
			self.curItemCfgId = self.curItemCfgId.Substring(0, self.curItemCfgId.Length - 1) + level;
			
			self.View.EButton_LeftButton.SetVisible(false);
			self.View.EButton_RightButton.SetVisible(false);
			string preItemCfgId = ItemHelper.GetTowerItemPreTowerConfigId(self.curItemCfgId);
			if (preItemCfgId != null)
			{
				self.View.EButton_LeftButton.SetVisible(true);
			}
			string nextItemCfgId = ItemHelper.GetTowerItemNextTowerConfigId(self.curItemCfgId);
			if (nextItemCfgId != null)
			{
				self.View.EButton_RightButton.SetVisible(true);
			}

			var attributeList = ItemHelper.GetTowerAttribute(self.curItemCfgId, level);
			self.View.ENode_Attribute1Image.SetVisible(attributeList.Count >= 1);
			self.View.ENode_Attribute2Image.SetVisible(attributeList.Count >= 2);
			self.View.ENode_Attribute3Image.SetVisible(attributeList.Count >= 3);
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
			Canvas.ForceUpdateCanvases();
			LayoutRebuilder.ForceRebuildLayoutImmediate(self.View.ENode_Attribute2Image.transform.parent.GetComponent<RectTransform>());
		}
		
		public static void SwitchToPreItem(this DlgDetails self)
		{
			if (!ItemHelper.ChkIsTower(self.curItemCfgId))
			{
				return;
			}
			string preItemCfgId = ItemHelper.GetTowerItemPreTowerConfigId(self.curItemCfgId);
			if (preItemCfgId != null)
			{
				int index = int.Parse(preItemCfgId.Substring(preItemCfgId.Length - 1, 1));
				self.View.E_ScrollView_IconLoopListView2.MovePanelToItemIndex(index - 1, 0);
			}
		}
		
		public static void SwitchToNextItem(this DlgDetails self)
		{
			if (!ItemHelper.ChkIsTower(self.curItemCfgId))
			{
				return;
			}
			string nextItemCfgId = ItemHelper.GetTowerItemNextTowerConfigId(self.curItemCfgId);
			if (nextItemCfgId != null)
			{
				int index = int.Parse(nextItemCfgId.Substring(nextItemCfgId.Length - 1, 1));
				self.View.E_ScrollView_IconLoopListView2.MovePanelToItemIndex(index - 1, 0);
			}
		}

		public static void OnDetailBgButton(this DlgDetails self)
		{
			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgDetails>();
		}

	}
}
