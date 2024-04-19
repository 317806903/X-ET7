using System.Collections;
using System.Collections.Generic;
using System;
using ET.AbilityConfig;
using SuperScrollView;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[Invoke(TimerInvokeType.DlgBattleDeckFrameTimer)]
	public class DlgBattleDeckTimer: ATimer<DlgBattleDeck>
	{
		protected override void Run(DlgBattleDeck self)
		{
			try
			{
				self.Update();
			}
			catch (Exception e)
			{
				Log.Error($"move timer error: {self.Id}\n{e}");
			}
		}
	}

	[FriendOf(typeof(DlgBattleDeck))]
	public static class DlgBattleDeckSystem
	{
		public static void RegisterUIEvent(this DlgBattleDeck self)
		{
			self.View.E_QuitBattleButton.AddListenerAsync(self.OnQuitButton);
			self.View.E_BG_ClickButton.AddListenerAsync(self.OnBgClick);

			self.View.ELoopScrollList_BattleDeckItemLoopHorizontalScrollRect.prefabSource.prefabName = "Item_TowerBuy";
			self.View.ELoopScrollList_BattleDeckItemLoopHorizontalScrollRect.prefabSource.poolSize = GlobalSettingCfgCategory.Instance.MaxBattleCardNum;
			self.View.ELoopScrollList_BattleDeckItemLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
					self.AddBattleDeckItemRefreshListener(transform, i).Coroutine());

			self.View.ELoopScrollList_TowerCardItemLoopHorizontalScrollRect.prefabSource.prefabName = "Item_TowerBuy";
			self.View.ELoopScrollList_TowerCardItemLoopHorizontalScrollRect.prefabSource.poolSize = 10;
			self.View.ELoopScrollList_TowerCardItemLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
					self.AddBagItemRefreshListener(transform, i).Coroutine());

			self.BindMoveBagItem();
		}

		public static void ShowWindow(this DlgBattleDeck self, ShowWindowData contextData = null)
		{
			self.moveItemCfgId = "";
			self.replaceIndex = -1;
			self.HideMoveBagItem();

			self.ShowBg();
			self.CreateCardScrollItem().Coroutine();
			self.Timer = TimerComponent.Instance.NewFrameTimer(TimerInvokeType.DlgBattleDeckFrameTimer, self);
		}

		public static void HideWindow(this DlgBattleDeck self)
		{
			TimerComponent.Instance?.Remove(ref self.Timer);
		}

		public static async ETTask Refresh(this DlgBattleDeck self)
		{
			await self.CreateCardScrollItem();

			self.View.ELoopScrollList_BattleDeckItemLoopHorizontalScrollRect.RefreshCells();
			self.View.ELoopScrollList_TowerCardItemLoopHorizontalScrollRect.RefreshCells();
		}

		public static void ShowBg(this DlgBattleDeck self)
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

		public static async ETTask CreateCardScrollItem(this DlgBattleDeck self)
		{
			int count = GlobalSettingCfgCategory.Instance.MaxBattleCardNum;
			if (self.View.ELoopScrollList_BattleDeckItemLoopHorizontalScrollRect.totalCount != count)
			{
				self.AddUIScrollItems(ref self.ScrollBattleDeckItem, count);
				self.View.ELoopScrollList_BattleDeckItemLoopHorizontalScrollRect.SetVisible(true, count);
			}

			List<ItemComponent> itemList = await self.GetTowerItemListWhenNotBattleDeck();
			int itemCount = itemList.Count;
			if (self.View.ELoopScrollList_TowerCardItemLoopHorizontalScrollRect.totalCount != itemCount)
			{
				self.AddUIScrollItems(ref self.ScrollBagItem, itemCount);
				self.View.ELoopScrollList_TowerCardItemLoopHorizontalScrollRect.SetVisible(true, itemCount);
			}
		}

		public static async ETTask OnQuitButton(this DlgBattleDeck self)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(),SoundEffectType.Back);
			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgBattleDeck>();
			await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgGameModeAR>();
		}

		public static void BindMoveBagItem(this DlgBattleDeck self)
		{
			self.moveBagItem = self.AddChild<Scroll_Item_TowerBuy>();
			Transform child = self.View.EG_MoveItemRectTransform.GetChild(0);
			self.moveBagItem.BindTrans(child);
			child.gameObject.SetActive(false);
		}

		public static async ETTask<List<ItemComponent>> GetTowerItemListWhenNotBattleDeck(this DlgBattleDeck self)
		{
			List<ItemComponent> itemListWhenNotBattleDeck = ListComponent<ItemComponent>.Create();
			PlayerBackPackComponent playerBackPackComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerBackPack(self.DomainScene());
			List<ItemComponent> towerList = playerBackPackComponent.GetItemListByItemType(ItemType.Tower, ItemSubType.None);
			towerList.Sort((x, y) => x.model.ShowPriority.CompareTo(y.model.ShowPriority));

			List<ItemComponent> itemIdBattleCardList = await ET.Client.PlayerCacheHelper.GetMyBattleCardItemList(self.DomainScene());

			foreach (ItemComponent itemComponent in towerList)
			{
				if (itemIdBattleCardList.Contains(itemComponent))
				{
					continue;
				}
				itemListWhenNotBattleDeck.Add(itemComponent);
			}
			return itemListWhenNotBattleDeck;
		}

		public static async ETTask ShowBagItem(this DlgBattleDeck self, Scroll_Item_TowerBuy BagItem, string itemCfgId)
		{
			BagItem.ShowBagItem(itemCfgId, true);
		}

		public static void MoveMoveBagItem(this DlgBattleDeck self, Vector2 screenPos)
		{
			if (self.lastScreenPos.Equals(screenPos))
			{
				return;
			}

			self.lastScreenPos = screenPos;

			Scroll_Item_TowerBuy BagItem = self.moveBagItem;

			var canvasRT = BagItem.uiTransform.parent.GetComponent<RectTransform>();
			// 将屏幕坐标转换为UI坐标
			Vector2 canvasPosition;
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRT, screenPos, UIManagerComponent.Instance.UICamera, out canvasPosition))
			{
				BagItem.uiTransform.GetComponent<RectTransform>().anchoredPosition = canvasPosition;
			}

		}

		public static async ETTask ShowMoveBagItem(this DlgBattleDeck self, string itemCfgId)
		{
			Scroll_Item_TowerBuy BagItem = self.moveBagItem;
			self.ShowBagItem(BagItem, itemCfgId).Coroutine();
			BagItem.uiTransform.gameObject.SetActive(true);
		}

		public static void HideMoveBagItem(this DlgBattleDeck self)
		{
			Scroll_Item_TowerBuy BagItem = self.moveBagItem;
			BagItem.uiTransform.gameObject.SetActive(false);
			BagItem.uiTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-10000, -10000);
		}

		public static async ETTask AddBagItemRefreshListener(this DlgBattleDeck self, Transform transform, int index)
		{
			Scroll_Item_TowerBuy BagItem = self.ScrollBagItem[index].BindTrans(transform);
			if (BagItem.uiTransform == null)
			{
				return;
			}
			List<ItemComponent> itemList = await self.GetTowerItemListWhenNotBattleDeck();
			if (BagItem.uiTransform == null)
			{
				return;
			}

			long itemId = itemList[index].Id;
			string itemCfgId = itemList[index].CfgId;
			self.ShowBagItem(BagItem, itemCfgId).Coroutine();

			ET.EventTriggerListener.Get(BagItem.EButton_SelectButton.gameObject).onPress.AddListener((go, xx) =>
			{
				//Log.Debug($"zpb BagItem Press");

				self.moveItemCfgId = itemCfgId;
				self.replaceIndex = -1;
				self.ShowMoveBagItem(itemCfgId).Coroutine();
			});

		}

		public static async ETTask AddBattleDeckItemRefreshListener(this DlgBattleDeck self, Transform transform, int index)
		{
			Scroll_Item_TowerBuy BattleDeckItem = self.ScrollBattleDeckItem[index].BindTrans(transform);
			if (BattleDeckItem.uiTransform == null)
			{
				return;
			}

			List<ItemComponent> itemList = await ET.Client.PlayerCacheHelper.GetMyBattleCardItemList(self.DomainScene());
			if (BattleDeckItem.uiTransform == null)
			{
				return;
			}
			string itemCfgId = "";
			if (index < itemList.Count)
			{
				itemCfgId = itemList[index].CfgId;
			}

			self.ShowBagItem(BattleDeckItem, itemCfgId).Coroutine();

			ET.EventTriggerListener.Get(BattleDeckItem.EButton_SelectButton.gameObject).onDrop.AddListener((go, xx) =>
			{
				//Log.Debug($"zpb BattleDeckItem onDrop");
				self.replaceIndex = index;
			});

			ET.EventTriggerListener.Get(BattleDeckItem.EButton_SelectButton.gameObject).onEnter.AddListener((go, xx) =>
			{
				//Log.Debug($"zpb BattleDeckItem onEnter");
				if (string.IsNullOrEmpty(self.moveItemCfgId))
				{
					return;
				}

				BattleDeckItem.uiTransform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
			});

			ET.EventTriggerListener.Get(BattleDeckItem.EButton_SelectButton.gameObject).onExit.AddListener((go, xx) =>
			{
				//Log.Debug($"zpb BattleDeckItem onExit");
				BattleDeckItem.uiTransform.localScale = Vector3.one;
			});

		}

		public static async ETTask OnBgClick(this DlgBattleDeck self)
		{
			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgBattleDeck>();
			await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgGameModeAR>();
		}

		public static void Update(this DlgBattleDeck self)
		{
            if (string.IsNullOrEmpty(self.moveItemCfgId))
            {
                return;
            }

            bool bRet = false;
            Vector2 screenPos = Vector2.zero;
            (bRet, screenPos) = ET.UGUIHelper.GetUserInputDownOrPress();
            if (bRet)
            {
	            self.MoveMoveBagItem(screenPos);
            }
            else
            {
	            (bRet, screenPos) = ET.UGUIHelper.GetUserInputUp();
                if (bRet)
                {
	                self.ChkPointUp().Coroutine();
                }
            }
		}

		public static async ETTask ChkPointUp(this DlgBattleDeck self)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.BattleForbidden);
			string moveItemCfgId = self.moveItemCfgId;
			self.moveItemCfgId = "";
			self.HideMoveBagItem();

			await TimerComponent.Instance.WaitFrameAsync();
			if (self.replaceIndex == -1)
			{
				return;
			}

			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.TowerPush);
			PlayerBattleCardComponent playerBattleCardComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerBattleCard(self.DomainScene(), false);
			playerBattleCardComponent.ReplaceBattleCardItemCfgId(self.replaceIndex, moveItemCfgId);
			await ET.Client.PlayerCacheHelper.SaveMyPlayerModel(self.DomainScene(), PlayerModelType.BattleCard, null);
		}

	}
}
