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

			self.View.ELoopScrollList_BagItemLoopVerticalScrollRect.prefabSource.prefabName = "Item_ItemShow";
			self.View.ELoopScrollList_BagItemLoopVerticalScrollRect.prefabSource.poolSize = 24;
			self.View.ELoopScrollList_BagItemLoopVerticalScrollRect.AddItemRefreshListener((transform, i) =>
					self.AddBagItemRefreshListener(transform, i).Coroutine());
		}

		public static async ETTask ShowWindow(this DlgBag self, ShowWindowData contextData = null)
		{
			self.ShowBg();
			self.CreateCardScrollItem();
		}

		public static void HideWindow(this DlgBag self)
		{
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

		public static async ETTask Refresh(this DlgBag self)
		{
			self.CreateCardScrollItem();

			self.View.ELoopScrollList_BagItemLoopVerticalScrollRect.RefreshCells();
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
			await UIManagerHelper.EnterGameModeUI(self.DomainScene());
		}

		public static async ETTask AddBagItemRefreshListener(this DlgBag self, Transform transform, int index)
		{
			Scroll_Item_ItemShow BagItem = self.ScrollBagItem[index].BindTrans(transform);
			PlayerBackPackComponent playerBackPackComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerBackPack(self.DomainScene());
			List<ItemComponent> itemList = playerBackPackComponent.GetItemList();
			itemList.Sort((x, y) => y.model.ShowPriority.CompareTo(x.model.ShowPriority));
			string itemCfgId = "";
			int itemNum = 0;
			if (index < itemList.Count)
			{
				itemCfgId = itemList[index].CfgId;
				itemNum = itemList[index].count;
			}
			await BagItem.Init(itemCfgId, true, itemNum);
		}

		public static async ETTask OnBgClick(this DlgBag self)
		{
			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgBag>();
			await UIManagerHelper.EnterGameModeUI(self.DomainScene());
		}
	}
}
