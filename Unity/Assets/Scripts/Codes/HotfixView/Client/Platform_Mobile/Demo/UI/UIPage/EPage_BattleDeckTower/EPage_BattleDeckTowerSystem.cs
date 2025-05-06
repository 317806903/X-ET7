using System.Collections;
using System.Collections.Generic;
using System;
using ET.AbilityConfig;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[Invoke(TimerInvokeType.EPage_BattleDeckTowerFrameTimer)]
	public class EPage_BattleDeckTowerTimer: ATimer<EPage_BattleDeckTower>
	{
		protected override void Run(EPage_BattleDeckTower self)
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

	[FriendOf(typeof(EPage_BattleDeckTower))]
	public static class EPage_BattleDeckTowerSystem
	{
		public static void RegisterUIEvent(this EPage_BattleDeckTower self)
		{
			self.View.ELoopScrollList_BattleDeckItemLoopHorizontalScrollRect.prefabSource.prefabName = "Item_BattleDeckTower";
			self.View.ELoopScrollList_BattleDeckItemLoopHorizontalScrollRect.prefabSource.poolSize = GlobalSettingCfgCategory.Instance.MaxBattleCardNum;
			self.View.ELoopScrollList_BattleDeckItemLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
			{
				self.AddBattleDeckItemRefreshListener(transform, i).Coroutine();
			});

			self.View.ELoopScrollList_BagCardItemLoopHorizontalScrollRect.prefabSource.prefabName = "Item_BattleDeckTower";
			self.View.ELoopScrollList_BagCardItemLoopHorizontalScrollRect.prefabSource.poolSize = 10;
			self.View.ELoopScrollList_BagCardItemLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
			{
				self.AddBagItemRefreshListener(transform, i).Coroutine();
				self.View.ELoopScrollList_BagCardItemLoopHorizontalScrollRect.SetSrcollMiddle();
			});

			self.BindMoveBagItem();

			if (ET.Client.UIManagerHelper.ChkIsDebug())
			{
				self.View.E_DebugButton.SetVisible(true);
				self.View.E_DebugButton.AddListenerAsync(self.AddCardsWhenDebug);
			}
			else
			{
				self.View.E_DebugButton.SetVisible(false);
			}
		}

		public static async ETTask PreLoadWindow(this EPage_BattleDeckTower self)
		{
			await self.SetDefaultBattleDeckItemList();

			await ET.Client.PlayerCacheHelper.GetMyBattleTowerItemCfgIdList(self.DomainScene());

			await self.CreateCardScrollItem();
		}

		public static async ETTask ShowPage(this EPage_BattleDeckTower self, ShowWindowData contextData = null)
		{
			self.dlgShowTime = TimeHelper.ClientNow();

			self.moveItemCfgId = "";
			self.replaceIndex = -1;
			self.HideMoveBagItem();

			self.CreateCardScrollItem().Coroutine();
			self.View.uiTransform.SetVisible(true);

			self.Timer = TimerComponent.Instance.NewFrameTimer(TimerInvokeType.EPage_BattleDeckTowerFrameTimer, self);

		}

		public static void HidePage(this EPage_BattleDeckTower self)
		{
			self.View.uiTransform.SetVisible(false);
			TimerComponent.Instance?.Remove(ref self.Timer);
		}

		public static async ETTask Refresh(this EPage_BattleDeckTower self)
		{
			await ET.Client.PlayerCacheHelper.GetMyBattleTowerItemCfgIdList(self.DomainScene());
			await self.CreateCardScrollItem();
		}

		public static async ETTask SetDefaultBattleDeckItemList(this EPage_BattleDeckTower self)
		{
			PlayerBackPackComponent playerBackPackComponent = await PlayerCacheHelper.GetMyPlayerBackPack(self.DomainScene());
			PlayerBattleCardComponent playerBattleCardComponent = await PlayerCacheHelper.GetMyPlayerBattleCard(self.DomainScene());

			bool isNeedChg =
				playerBattleCardComponent.SetBattleCardItemCfgIdList(playerBackPackComponent.GetItemListByItemType(ItemType.Tower, ItemSubType.BattleDeckTower));
			if (isNeedChg)
			{
				bool bRet = await ET.Client.ItemHelper.ReplaceBattleDeck(self.DomainScene(), -1, "");
			}
		}

		public static async ETTask CreateCardScrollItem(this EPage_BattleDeckTower self)
		{
			int count = GlobalSettingCfgCategory.Instance.MaxBattleCardNum;
			if (self.View.ELoopScrollList_BattleDeckItemLoopHorizontalScrollRect.totalCount != count)
			{
				self.AddUIScrollItems(ref self.ScrollBattleDeckItem, count);
				self.View.ELoopScrollList_BattleDeckItemLoopHorizontalScrollRect.SetVisible(true, count);
			}
			else
			{
				self.View.ELoopScrollList_BattleDeckItemLoopHorizontalScrollRect.RefreshCells();
			}

			List<(string itemCfgId, bool isAlreadyHave)> itemList = await self.GetTowerItemListWhenNotBattleDeck();
			int itemCount = itemList.Count;
			if (self.View.ELoopScrollList_BagCardItemLoopHorizontalScrollRect.totalCount != itemCount)
			{
				self.AddUIScrollItems(ref self.ScrollBagItem, itemCount);
				self.View.ELoopScrollList_BagCardItemLoopHorizontalScrollRect.SetVisible(true, itemCount);
			}
			else
			{
				self.View.ELoopScrollList_BagCardItemLoopHorizontalScrollRect.RefreshCells();
			}
		}

		public static void BindMoveBagItem(this EPage_BattleDeckTower self)
		{
			self.moveBagItem = self.AddChild<Scroll_Item_BattleDeckTower>();
			Transform child = self.View.EG_MoveItemRectTransform.GetChild(0);
			self.moveBagItem.BindTrans(child);
			child.gameObject.SetActive(false);
		}

		public static async ETTask<List<(string itemCfgId, bool isAlreadyHave)>> GetTowerItemListWhenNotBattleDeck(this EPage_BattleDeckTower self)
		{
			return await ET.Client.PlayerCacheHelper.GetTowerItemListWhenNotBattleDeck(self.DomainScene(), false);
		}

		public static async ETTask ShowBagItem(this EPage_BattleDeckTower self, Scroll_Item_BattleDeckTower item_BattleDeckTower, string itemCfgId, bool isShowRedDot, bool isAlreadyHave)
		{
			await item_BattleDeckTower.Init(itemCfgId, true, !isAlreadyHave);
			item_BattleDeckTower.ShowRedDot(isShowRedDot);
		}

		public static void MoveMoveBagItem(this EPage_BattleDeckTower self, Vector2 screenPos)
		{
			if (self.lastScreenPos.Equals(screenPos))
			{
				return;
			}

			self.lastScreenPos = screenPos;

			Scroll_Item_BattleDeckTower bagItem = self.moveBagItem;

			var canvasRT = bagItem.uiTransform.parent.GetComponent<RectTransform>();
			// 将屏幕坐标转换为UI坐标
			Vector2 canvasPosition;
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRT, screenPos, UIRootManagerComponent.Instance.UICamera, out canvasPosition))
			{
				bagItem.uiTransform.GetComponent<RectTransform>().anchoredPosition = canvasPosition;
			}

		}

		public static async ETTask ShowMoveBagItem(this EPage_BattleDeckTower self, string itemCfgId)
		{
			Scroll_Item_BattleDeckTower bagItem = self.moveBagItem;
			await self.ShowBagItem(bagItem, itemCfgId, false, true);
			bagItem.uiTransform.gameObject.SetActive(true);
		}

		public static void HideMoveBagItem(this EPage_BattleDeckTower self)
		{
			Scroll_Item_BattleDeckTower bagItem = self.moveBagItem;
			bagItem.uiTransform.gameObject.SetActive(false);
			bagItem.uiTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-10000, -10000);
		}

		public static async ETTask AddBagItemRefreshListener(this EPage_BattleDeckTower self, Transform transform, int index)
		{
			Scroll_Item_BattleDeckTower bagItem = self.ScrollBagItem[index].BindTrans(transform);
			if (self.IsDisposed || bagItem.uiTransform == null)
			{
				return;
			}
			List<(string itemCfgId, bool isAlreadyHave)> itemList = await self.GetTowerItemListWhenNotBattleDeck();
			if (self.IsDisposed || bagItem.uiTransform == null)
			{
				return;
			}

			string itemCfgId = itemList[index].itemCfgId;
			bool isAlreadyHave = itemList[index].isAlreadyHave;
			PlayerBackPackComponent playerBackPackComponent = await PlayerCacheHelper.GetMyPlayerBackPack(self.DomainScene());
			if (self.IsDisposed || bagItem.uiTransform == null)
			{
				return;
			}
			bool isShowRedDot = playerBackPackComponent.ChkIsNewItem(itemCfgId);

			self.ShowBagItem(bagItem, itemCfgId, isShowRedDot, isAlreadyHave).Coroutine();

			ET.EventTriggerListener.Get(bagItem.GetActionButton()).onPress.AddListener((go, xx) =>
			{
				//Log.Debug($"zpb bagItem Press");
				if (isAlreadyHave == false)
				{
					return;
				}
				self.moveItemCfgId = itemCfgId;
				self.replaceIndex = -1;
				self.ShowMoveBagItem(itemCfgId).Coroutine();
			});

		}

		public static async ETTask AddBattleDeckItemRefreshListener(this EPage_BattleDeckTower self, Transform transform, int index)
		{
			Scroll_Item_BattleDeckTower battleDeckItem = self.ScrollBattleDeckItem[index].BindTrans(transform);
			if (battleDeckItem.uiTransform == null)
			{
				return;
			}

			List<string> itemList = await ET.Client.PlayerCacheHelper.GetMyBattleTowerItemCfgIdList(self.DomainScene());
			if (self.IsDisposed)
			{
				return;
			}
			if (battleDeckItem.uiTransform == null)
			{
				return;
			}
			string itemCfgId = "";
			//Log.Error($"--zpb-- index[{index}] itemList.Count[{itemList.Count}] {itemList}");
			if (index < itemList.Count)
			{
				itemCfgId = itemList[index];
			}

			PlayerBackPackComponent playerBackPackComponent = await PlayerCacheHelper.GetMyPlayerBackPack(self.DomainScene());
			if (self.IsDisposed)
			{
				return;
			}
			bool isShowRedDot = playerBackPackComponent.ChkIsNewItem(itemCfgId);

			self.ShowBagItem(battleDeckItem, itemCfgId, isShowRedDot, true).Coroutine();

			ET.EventTriggerListener.Get(battleDeckItem.GetActionButton()).onDrop.AddListener((go, xx) =>
			{
				//Log.Error($"zpb battleDeckItem onDrop self.moveItemCfgId[{self.moveItemCfgId}] index[{index}] self.replaceIndex[{self.replaceIndex}]");
				self.replaceIndex = index;
			});

			ET.EventTriggerListener.Get(battleDeckItem.GetActionButton()).onEnter.AddListener((go, xx) =>
			{
				//Log.Error($"zpb battleDeckItem onEnter self.moveItemCfgId[{self.moveItemCfgId}] index[{index}]");
				if (string.IsNullOrEmpty(self.moveItemCfgId))
				{
					return;
				}

				battleDeckItem.uiTransform.localScale = new Vector3(0.9f, 0.9f, 0.9f);

				self.replaceIndex = index;
			});

			ET.EventTriggerListener.Get(battleDeckItem.GetActionButton()).onExit.AddListener(async (go, xx) =>
			{
				await TimerComponent.Instance.WaitFrameAsync();
				//Log.Error($"zpb battleDeckItem onExit self.moveItemCfgId[{self.moveItemCfgId}] index[{index}] self.replaceIndex[{self.replaceIndex}]");
				battleDeckItem.uiTransform.localScale = Vector3.one;
				if (string.IsNullOrEmpty(self.moveItemCfgId) == false)
				{
					self.replaceIndex = -1;
				}
			});
		}

		public static void Update(this EPage_BattleDeckTower self)
		{
            if (string.IsNullOrEmpty(self.moveItemCfgId))
            {
                return;
            }

            bool bRet = false;
            Vector2 screenPos = Vector2.zero;
            Vector3 touchPosition = Vector3.zero;
			Vector3 touchForward = Vector3.zero;
            (bRet, touchPosition, touchForward) = ET.UGUIHelper.GetUserInputDownOrPress();
            if (bRet)
            {
	            self.MoveMoveBagItem(touchPosition);
            }
            else
            {
	            (bRet, touchPosition, touchForward) = ET.UGUIHelper.GetUserInputUp();
                if (bRet)
                {
	                self.ChkPointUp().Coroutine();
                }
            }
		}

		public static async ETTask ChkPointUp(this EPage_BattleDeckTower self)
		{
			//Log.Error($"zpb battleDeckItem ChkPointUp self.moveItemCfgId[{self.moveItemCfgId}] self.replaceIndex[{self.replaceIndex}]");

			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.BattleForbidden);
			string moveItemCfgId = self.moveItemCfgId;
			self.moveItemCfgId = "";
			self.HideMoveBagItem();

			if (self.replaceIndex == -1)
			{
				return;
			}

			int replaceIndex = self.replaceIndex;
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.TowerPush);

			bool bRet = await ET.Client.ItemHelper.ReplaceBattleDeck(self.DomainScene(), replaceIndex, moveItemCfgId);
			if (bRet)
			{
				string txt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_BattleDeckOperateSuccess");
				UIManagerHelper.ShowTip(self.DomainScene(), txt);
			}

		}

		public static async ETTask AddCardsWhenDebug(this EPage_BattleDeckTower self)
		{
			if (GlobalConfig.Instance.dbType == DBType.NoDB)
			{
				PlayerBackPackComponent playerBackPackComponent = await PlayerCacheHelper.GetMyPlayerBackPack(self.DomainScene());
				bool isChg = false;

				var towerDefenseTowerList = ET.ItemHelper.GetTowerListInBattleDeck();
				foreach (string towerCfgId in towerDefenseTowerList)
				{
					bool isExist = playerBackPackComponent.ChkItemExist(towerCfgId);
					if (isExist == false)
					{
						isChg = true;
						playerBackPackComponent.AddItem(towerCfgId, 1);
					}
				}

				if (isChg)
				{
					await ET.Client.PlayerCacheHelper.SaveMyPlayerModel(self.DomainScene(), PlayerModelType.BackPack, null);
					await ET.Client.PlayerCacheHelper.GetUIRedDotType(self.DomainScene());

					UIManagerHelper.ShowTip(self.DomainScene(), "success");

					await self.Refresh();
				}
			}
		}

	}
}
