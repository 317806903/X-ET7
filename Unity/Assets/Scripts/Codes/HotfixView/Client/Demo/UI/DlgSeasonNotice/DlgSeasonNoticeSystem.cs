using System.Collections;
using System.Collections.Generic;
using System;
using ET.AbilityConfig;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgSeasonNotice))]
	public static class DlgSeasonNoticeSystem
	{
		public static void RegisterUIEvent(this DlgSeasonNotice self)
		{
			self.View.E_BG_ClickButton.AddListenerAsync(self.Back);

			//头像框
			self.View.ELoopListView_FrameLoopHorizontalScrollRect.prefabSource.prefabName = "Item_Frame";
			self.View.ELoopListView_FrameLoopHorizontalScrollRect.prefabSource.poolSize = 5;
			self.View.ELoopListView_FrameLoopHorizontalScrollRect.AddItemRefreshListener(async (transform, i) =>
			{
				await self.AddFrameItemRefreshListener(transform, i);
				self.View.ELoopListView_FrameLoopHorizontalScrollRect.SetSrcollMiddle();
			});

			//赛季奖励塔
			self.View.ELoopListView_CardsLoopHorizontalScrollRect.prefabSource.prefabName = "Item_ItemShow";
			self.View.ELoopListView_CardsLoopHorizontalScrollRect.prefabSource.poolSize = 4;
			self.View.ELoopListView_CardsLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
			{
				self.AddTowerBuyListener(transform, i).Coroutine();
				self.View.ELoopListView_CardsLoopHorizontalScrollRect.SetSrcollMiddle();
			});

			//赛季怪物
			self.View.ELoopListView_MonsersLoopHorizontalScrollRect.prefabSource.prefabName = "Item_Monsters";
			self.View.ELoopListView_MonsersLoopHorizontalScrollRect.prefabSource.poolSize = 4;
			self.View.ELoopListView_MonsersLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
			{
				self.AddMonsterListener(transform, i);
				self.View.ELoopListView_MonsersLoopHorizontalScrollRect.SetSrcollMiddle();
			});
		}

		public static async ETTask ShowWindow(this DlgSeasonNotice self, ShowWindowData contextData = null)
		{
			self.ShowBg().Coroutine();
			self.SetTitleTxt().Coroutine();

			SeasonComponent seasonComponent = ET.Client.SeasonHelper.GetSeasonComponent(self.DomainScene());
			self.avatarFrameList = seasonComponent.cfg.RewardItemListShow;

			self.seasonCfgId = ET.Client.SeasonHelper.GetSeasonCfgId(self.DomainScene());
			self.SetEloopNumber(true).Coroutine();

			self.View.ELoopListView_CardsLoopHorizontalScrollRect.RefreshCells();
			self.View.ELoopListView_FrameLoopHorizontalScrollRect.RefreshCells();
			self.View.ELoopListView_MonsersLoopHorizontalScrollRect.RefreshCells();
		}

		public static void HideWindow(this DlgSeasonNotice self)
		{
		}

		public static async ETTask ShowBg(this DlgSeasonNotice self)
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
			await ETTask.CompletedTask;
		}

		public static async ETTask Back(this DlgSeasonNotice self)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Back);
			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgSeasonNotice>();
			await UIManagerHelper.EnterGameModeUI(self.DomainScene());
		}

		public static async ETTask SetEloopNumber(this DlgSeasonNotice self, bool bClear)
		{
			List<string> list;
			SeasonComponent seasonComponent = ET.Client.SeasonHelper.GetSeasonComponent(self.DomainScene());
			list = seasonComponent.cfg.TowerListShow;
			self.AddUIScrollItems(ref self.ScrollItemReward, list.Count);
			self.View.ELoopListView_CardsLoopHorizontalScrollRect.SetVisible(true, list.Count);

			list = seasonComponent.cfg.MonsterListShow;
			self.AddUIScrollItems(ref self.ScrollItemMonster, list.Count);
			self.View.ELoopListView_MonsersLoopHorizontalScrollRect.SetVisible(true, list.Count);

			list = self.avatarFrameList;
			self.AddUIScrollItems(ref self.ScrollItemFrameIcons, list.Count);
			self.View.ELoopListView_FrameLoopHorizontalScrollRect.SetVisible(true, list.Count);
		}

		public static async ETTask SetTitleTxt(this DlgSeasonNotice self)
		{
			SeasonComponent seasonComponent = ET.Client.SeasonHelper.GetSeasonComponent(self.DomainScene());
			string textName = seasonComponent.cfg.Name;
			self.View.ETxtTitleTextMeshProUGUI.SetText(textName);

			await ETTask.CompletedTask;
		}

		public static async ETTask AddFrameItemRefreshListener(this DlgSeasonNotice self, Transform transform, int index)
		{
			transform.gameObject.name = "Item_Frame" + index;
			Scroll_Item_Frame itemFrame = self.ScrollItemFrameIcons[index].BindTrans(transform);
			string itemCfgId = null;
			itemCfgId = self.avatarFrameList[index];
			itemFrame.ShowFrameItem(itemCfgId, true);
			await itemFrame.EImage_FrameImage.SetImageByItemCfgId(self, itemCfgId);
			itemFrame.EIcon_SelectedImage.gameObject.SetActive(false);
		}

		public static async ETTask AddTowerBuyListener(this DlgSeasonNotice self, Transform transform, int index)
		{
			List<string> list;
			SeasonComponent seasonComponent = ET.Client.SeasonHelper.GetSeasonComponent(self.DomainScene());

			transform.name = $"Item_TowerBattleBuy_{index}";
			Scroll_Item_ItemShow itemTowerBuy = self.ScrollItemReward[index].BindTrans(transform);

			//int clearLevel = await self.GetCurPveIndex();
			//ChallengeLevelCfg challengeLevelCfg =
			//    SeasonChallengeLevelCfgCategory.Instance.GetChallengeByIndex(self.seasonCfgId, self.selectIndex + 1);

			list = seasonComponent.cfg.TowerListShow;

			string itemCfgId = list[index];
			await itemTowerBuy.Init(itemCfgId, true);

			//WJTODO 获取玩家背包是否有该塔
			//itemTowerBuy.SetCheckMark(clearLevel >= challengeLevelCfg.Index);
		}

		public static void AddMonsterListener(this DlgSeasonNotice self, Transform transform, int index)
		{
			SeasonComponent seasonComponent = ET.Client.SeasonHelper.GetSeasonComponent(self.DomainScene());

			transform.name = $"Item_Monster_{index}";
			Scroll_Item_Monsters itemMonster = self.ScrollItemMonster[index].BindTrans(transform);
			List<string> monsterList;
			monsterList = seasonComponent.cfg.MonsterListShow;

			string itemCfgId = monsterList[index];
			itemMonster.ShowMonsterItem(itemCfgId, true).Coroutine();
		}

	}
}
