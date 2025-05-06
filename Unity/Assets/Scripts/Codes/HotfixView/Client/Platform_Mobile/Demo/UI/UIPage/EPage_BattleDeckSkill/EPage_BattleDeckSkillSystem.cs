using System.Collections;
using System.Collections.Generic;
using System;
using ET.AbilityConfig;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[Invoke(TimerInvokeType.EPage_BattleDeckSkillFrameTimer)]
	public class EPage_BattleDeckSkillTimer: ATimer<EPage_BattleDeckSkill>
	{
		protected override void Run(EPage_BattleDeckSkill self)
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

	[FriendOf(typeof(EPage_BattleDeckSkill))]
	public static class EPage_BattleDeckSkillSystem
	{
		public static void RegisterUIEvent(this EPage_BattleDeckSkill self)
		{
			self.View.ELoopScrollList_BattleDeckItemLoopHorizontalScrollRect.prefabSource.prefabName = "Item_BattleDeckSkill";
			self.View.ELoopScrollList_BattleDeckItemLoopHorizontalScrollRect.prefabSource.poolSize = GlobalSettingCfgCategory.Instance.MaxBattleSkillNum;
			self.View.ELoopScrollList_BattleDeckItemLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
			{
				self.AddBattleDeckItemRefreshListener(transform, i).Coroutine();
			});

			self.View.ELoopScrollList_BagCardItemLoopHorizontalScrollRect.prefabSource.prefabName = "Item_BattleDeckSkill";
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
				self.View.E_DebugButton.AddListenerAsync(self.AddSkillsWhenDebug);
			}
			else
			{
				self.View.E_DebugButton.SetVisible(false);
			}
		}

		public static async ETTask PreLoadWindow(this EPage_BattleDeckSkill self)
		{
			await self.SetDefaultBattleDeckItemList();

			await ET.Client.PlayerCacheHelper.GetMyBattleSkillItemCfgIdList(self.DomainScene());

			await self.CreateCardScrollItem();
		}

		public static async ETTask ShowPage(this EPage_BattleDeckSkill self, ShowWindowData contextData = null)
		{
			self.View.uiTransform.SetVisible(true);

			self.dlgShowTime = TimeHelper.ClientNow();

			self.moveItemCfgId = "";
			self.replaceIndex = -1;
			self.HideMoveBagItem();

			self.CreateCardScrollItem().Coroutine();
			self.Timer = TimerComponent.Instance.NewFrameTimer(TimerInvokeType.EPage_BattleDeckSkillFrameTimer, self);

		}

		public static void HidePage(this EPage_BattleDeckSkill self)
		{
			self.View.uiTransform.SetVisible(false);
			TimerComponent.Instance?.Remove(ref self.Timer);
		}

		public static async ETTask Refresh(this EPage_BattleDeckSkill self)
		{
			await ET.Client.PlayerCacheHelper.GetMyBattleSkillItemCfgIdList(self.DomainScene());
			await self.CreateCardScrollItem();
		}

		public static async ETTask SetDefaultBattleDeckItemList(this EPage_BattleDeckSkill self)
		{
			PlayerBackPackComponent playerBackPackComponent = await PlayerCacheHelper.GetMyPlayerBackPack(self.DomainScene());

			PlayerBattleSkillComponent playerBattleSkillComponent = await PlayerCacheHelper.GetMyPlayerBattleSkill(self.DomainScene());

			bool isNeedChg = playerBattleSkillComponent.SetBattleSkillItemCfgIdList(playerBackPackComponent.GetItemListByItemType(ItemType.Skill, ItemSubType.None));
			if (isNeedChg)
			{
				bool bRet = await ET.Client.ItemHelper.ReplaceBattleDeck(self.DomainScene(), -1, "");
			}
		}

		public static async ETTask CreateCardScrollItem(this EPage_BattleDeckSkill self)
		{
			int count = GlobalSettingCfgCategory.Instance.MaxBattleSkillNum;
			if (self.View.ELoopScrollList_BattleDeckItemLoopHorizontalScrollRect.totalCount != count)
			{
				self.AddUIScrollItems(ref self.ScrollBattleDeckItem, count);
				self.View.ELoopScrollList_BattleDeckItemLoopHorizontalScrollRect.SetVisible(true, count);
			}
			else
			{
				self.View.ELoopScrollList_BattleDeckItemLoopHorizontalScrollRect.RefreshCells();
			}

			List<(string itemCfgId, bool isAlreadyHave)> itemList = await self.GetSkillItemListWhenNotBattleDeck();
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

		public static void BindMoveBagItem(this EPage_BattleDeckSkill self)
		{
			self.moveBagItem = self.AddChild<Scroll_Item_BattleDeckSkill>();
			Transform child = self.View.EG_MoveItemRectTransform.GetChild(0);
			self.moveBagItem.BindTrans(child);
			child.gameObject.SetActive(false);
		}

		public static async ETTask<List<(string itemCfgId, bool isAlreadyHave)>> GetSkillItemListWhenNotBattleDeck(this EPage_BattleDeckSkill self)
		{
			return await ET.Client.PlayerCacheHelper.GetSkillItemListWhenNotBattleDeck(self.DomainScene(), false);
		}

		public static async ETTask ShowBagItem(this EPage_BattleDeckSkill self, Scroll_Item_BattleDeckSkill item_BattleDeckSkill, string itemCfgId, bool isShowRedDot, bool isAlreadyHave)
		{
			await item_BattleDeckSkill.Init(itemCfgId, true, !isAlreadyHave);
			item_BattleDeckSkill.ShowRedDot(isShowRedDot);
		}

		public static void MoveMoveBagItem(this EPage_BattleDeckSkill self, Vector2 screenPos)
		{
			if (self.lastScreenPos.Equals(screenPos))
			{
				return;
			}

			self.lastScreenPos = screenPos;

			Scroll_Item_BattleDeckSkill bagItem = self.moveBagItem;

			var canvasRT = bagItem.uiTransform.parent.GetComponent<RectTransform>();
			// 将屏幕坐标转换为UI坐标
			Vector2 canvasPosition;
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRT, screenPos, UIRootManagerComponent.Instance.UICamera, out canvasPosition))
			{
				bagItem.uiTransform.GetComponent<RectTransform>().anchoredPosition = canvasPosition;
			}

		}

		public static async ETTask ShowMoveBagItem(this EPage_BattleDeckSkill self, string itemCfgId)
		{
			Scroll_Item_BattleDeckSkill bagItem = self.moveBagItem;
			self.ShowBagItem(bagItem, itemCfgId, false, true).Coroutine();
			bagItem.uiTransform.gameObject.SetActive(true);
		}

		public static void HideMoveBagItem(this EPage_BattleDeckSkill self)
		{
			Scroll_Item_BattleDeckSkill bagItem = self.moveBagItem;
			bagItem.uiTransform.gameObject.SetActive(false);
			bagItem.uiTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-10000, -10000);
		}

		public static async ETTask AddBagItemRefreshListener(this EPage_BattleDeckSkill self, Transform transform, int index)
		{
			Scroll_Item_BattleDeckSkill bagItem = self.ScrollBagItem[index].BindTrans(transform);
			if (self.IsDisposed || bagItem.uiTransform == null)
			{
				return;
			}
			List<(string itemCfgId, bool isAlreadyHave)> itemList = await self.GetSkillItemListWhenNotBattleDeck();
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

		public static async ETTask AddBattleDeckItemRefreshListener(this EPage_BattleDeckSkill self, Transform transform, int index)
		{
			Scroll_Item_BattleDeckSkill battleDeckItem = self.ScrollBattleDeckItem[index].BindTrans(transform);
			if (battleDeckItem.uiTransform == null)
			{
				return;
			}

			List<string> itemList = await ET.Client.PlayerCacheHelper.GetMyBattleSkillItemCfgIdList(self.DomainScene());
			if (self.IsDisposed)
			{
				return;
			}
			if (battleDeckItem.uiTransform == null)
			{
				return;
			}
			string itemCfgId = "";
			if (index < itemList.Count)
			{
				itemCfgId = itemList[index];
			}

			PlayerBackPackComponent playerBackPackComponent = await PlayerCacheHelper.GetMyPlayerBackPack(self.DomainScene());
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

		public static void Update(this EPage_BattleDeckSkill self)
		{
            if (string.IsNullOrEmpty(self.moveItemCfgId))
            {
                return;
            }

            bool bRet = false;
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

		public static async ETTask ChkPointUp(this EPage_BattleDeckSkill self)
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

		public static async ETTask AddSkillsWhenDebug(this EPage_BattleDeckSkill self)
		{
			if (GlobalConfig.Instance.dbType == DBType.NoDB)
			{
				PlayerBackPackComponent playerBackPackComponent = await PlayerCacheHelper.GetMyPlayerBackPack(self.DomainScene());
				bool isChg = false;

				var SkillDefenseSkillList = ET.ItemHelper.GetSkillListInBattleDeck();
				foreach (string SkillCfgId in SkillDefenseSkillList)
				{
					bool isExist = playerBackPackComponent.ChkItemExist(SkillCfgId);
					if (isExist == false)
					{
						isChg = true;
						playerBackPackComponent.AddItem(SkillCfgId, 1);
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
