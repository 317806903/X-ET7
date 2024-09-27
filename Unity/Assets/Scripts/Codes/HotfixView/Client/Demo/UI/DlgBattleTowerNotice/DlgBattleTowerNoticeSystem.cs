using System.Collections;
using System.Collections.Generic;
using System;
using ET.AbilityConfig;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgBattleTowerNotice))]
	public static class DlgBattleTowerNoticeSystem
	{
		public static void RegisterUIEvent(this DlgBattleTowerNotice self)
		{
			self.View.ELoopScrollList_NoticeLoopVerticalScrollRect.prefabSource.prefabName = "Item_BattleNotice";
			self.View.ELoopScrollList_NoticeLoopVerticalScrollRect.prefabSource.poolSize = 5;
			self.View.ELoopScrollList_NoticeLoopVerticalScrollRect.AddItemRefreshListener((transform, i) =>
				self.AddItemRefreshListener(transform, i));
		}

		public static async ETTask ShowWindow(this DlgBattleTowerNotice self, ShowWindowData contextData = null)
		{
			self.dlgShowTime = TimeHelper.ClientNow();

		}

		public static bool ChkCanClickBg(this DlgBattleTowerNotice self)
		{
			if (self.dlgShowTime < TimeHelper.ClientNow() - (long)(1000 * 1f))
			{
				return true;
			}
			return false;
		}

		public static void HideWindow(this DlgBattleTowerNotice self)
		{
		}

		public static async ETTask RefreshWhenNoticeShowBattleNotice(this DlgBattleTowerNotice self, string tutorialCfgId)
		{
			if (self.tutorialCfgIdList.Contains(tutorialCfgId))
			{
				return;
			}

			PlayerOtherInfoComponent playerOtherInfoComponent = await PlayerCacheHelper.GetMyPlayerOtherInfo(self.DomainScene());
			if (self.tutorialCfgIdList.Contains(tutorialCfgId))
			{
				return;
			}

			if (playerOtherInfoComponent.ChkNeedBattleNotice(tutorialCfgId) == false)
			{
				return;
			}

			self.tutorialCfgIdList.Add(tutorialCfgId);

			self._RefreshList();
		}

		public static void _RefreshList(this DlgBattleTowerNotice self)
		{
			int count = self.tutorialCfgIdList.Count;
			if (self.View.ELoopScrollList_NoticeLoopVerticalScrollRect.totalCount != count)
			{
				self.AddUIScrollItems(ref self.ScrollItem, count);
				self.View.ELoopScrollList_NoticeLoopVerticalScrollRect.SetVisible(true, count);
			}
		}

		public static void AddItemRefreshListener(this DlgBattleTowerNotice self, Transform transform, int index)
		{
			transform.name = $"Item_BattleNotice_{index}";
			Scroll_Item_BattleNotice item = self.ScrollItem[index].BindTrans(transform);

			List<string> list = self.tutorialCfgIdList;
			string tutorialCfgId = list[index];
			TutorialCfg tutorialCfg = TutorialCfgCategory.Instance.Get(tutorialCfgId);
			item.EButton_IconImage.SetImageByResIconCfgId(self, tutorialCfg.ResIcon).Coroutine();
			item.EClickButton.AddListenerAsync( async () =>
			{
				DlgTutorialOne_ShowWindowData _DlgTutorialOne_ShowWindowData = new();
				_DlgTutorialOne_ShowWindowData.tutorialCfgId = tutorialCfgId;
				await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgTutorialOne>(_DlgTutorialOne_ShowWindowData);

				self.tutorialCfgIdList.Remove(tutorialCfgId);
				self._RefreshList();

				ET.Client.PlayerCacheHelper.SendSetBattleNoticeFinished(self.DomainScene(), tutorialCfgId);
			});
		}

	}
}
