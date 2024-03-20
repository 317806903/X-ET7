using System.Collections;
using System.Collections.Generic;
using System;
using SuperScrollView;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgBag))]
	public static class DlgBagSystem
	{
		public static void RegisterUIEvent(this DlgBag self)
		{
			self.View.E_QuitBattleButton.AddListenerAsync(self.OnQuitButton);
			self.View.E_BG_ClickButton.AddListenerAsync(self.OnBgClick);
			
			self.View.ELoopScrollList_BagItemLoopVerticalScrollRect.prefabSource.prefabName = "Item_TowerBuy";
			self.View.ELoopScrollList_BagItemLoopVerticalScrollRect.prefabSource.poolSize = 24;
			self.View.ELoopScrollList_BagItemLoopVerticalScrollRect.AddItemRefreshListener((transform, i) =>
					self.AddBagItemRefreshListener(transform, i));
		}

		public static void ShowWindow(this DlgBag self, ShowWindowData contextData = null)
		{
			self.ShowBg();
			self.CreateCardScrollItem();
		}
		
		public static void ShowBg(this DlgBag self)
		{
			bool isARCameraEnable = ET.Client.ARSessionHelper.ChkARCameraEnable(self.DomainScene());
			isARCameraEnable = false;
			if (isARCameraEnable)
			{
				self.View.EG_bgARRectTransform.SetVisible(true);
				self.View.EG_bgRectTransform.SetVisible(false);
			}
			else
			{
				self.View.EG_bgARRectTransform.SetVisible(false);
				self.View.EG_bgRectTransform.SetVisible(true);
			}
		}
		
		public static void CreateCardScrollItem(this DlgBag self)
		{
			int count = 24;
			self.AddUIScrollItems(ref self.ScrollBagItem, count);
			self.View.ELoopScrollList_BagItemLoopVerticalScrollRect.SetVisible(true, count);
		}

		public static async ETTask OnQuitButton(this DlgBag self)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(),SoundEffectType.Back);
			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgBag>();
			await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgGameModeAR>();
		}

		public static async ETTask AddBagItemRefreshListener(this DlgBag self, Transform transform, int index)
		{
			Scroll_Item_TowerBuy BagItem = self.ScrollBagItem[index].BindTrans(transform);
			PlayerBackPackComponent playerBackPackComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerBackPack(self.DomainScene());
			List<ItemComponent> itemList = playerBackPackComponent.GetItemList();

			if (index < itemList.Count)
			{
				string itemCfgId = itemList[index].CfgId;
				BagItem.EButton_nameTextMeshProUGUI.text = ItemHelper.GetItemName(itemCfgId);
				await BagItem.EButton_IconImage.SetImageByPath(ItemHelper.GetItemIcon(itemCfgId));
				BagItem.EImage_TowerBuyShowImage.SetVisible(true);
				BagItem.EButton_BuyButton.SetVisible(false);
				
				if (ItemHelper.ChkIsTower(itemCfgId)) 
				{
					BagItem.SetLabels(itemCfgId);
					BagItem.SetQuality(itemCfgId);
					BagItem.SetCheckMark(false);
				}

				BagItem.EButton_SelectButton.AddListener(()=>
				{
					self.ShowDetails(itemCfgId);
				});
			}
			else
			{
				BagItem.EImage_TowerBuyShowImage.SetVisible(false);
			}
		}

		public static void ShowDetails(this DlgBag self, string itemCfgId)
		{
			UIComponent _UIComponent = UIManagerHelper.GetUIComponent(self.DomainScene());
			_UIComponent.ShowWindow<DlgDetails>();
			DlgDetails _DlgDetails = _UIComponent.GetDlgLogic<DlgDetails>(true);
			if (_DlgDetails != null)
			{
				_DlgDetails.SetCurItemCfgId(itemCfgId);
			}
		}
		
		public static async ETTask OnBgClick(this DlgBag self)
		{
			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgBag>();
			await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgGameModeAR>();
		}
	}
}
