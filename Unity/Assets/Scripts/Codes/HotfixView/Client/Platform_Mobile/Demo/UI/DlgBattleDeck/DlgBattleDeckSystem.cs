using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgBattleDeck))]
	public static class DlgBattleDeckSystem
	{
		public static void RegisterUIEvent(this DlgBattleDeck self)
		{
			self.View.E_QuitBattleButton.AddListenerAsync(self.Back);

			self.View.ETabSelectBtnTowerButton.AddListener(() => {self.SwitchPage(0); });
			self.View.ETabSelectBtnSkillButton.AddListener(() => { self.SwitchPage(1); });
			self.View.ETabUnSelectBtnTowerButton.AddListener(() => { self.SetToggleButtonsActive(true); self.SwitchPage( 0); });
			self.View.ETabUnSelectBtnSkillButton.AddListener(() => { self.SetToggleButtonsActive(false);self.SwitchPage(1); });
		}

		//切换界面
		private static void SwitchPage(this DlgBattleDeck self, int pageIndex)
		{
			self.pageIndex = pageIndex;
			if (pageIndex == 0)
			{
				self.View.EPage_BattleDeckTower.ShowPage().Coroutine();
				self.View.EPage_BattleDeckSkill.HidePage();
			}
			else
			{

				UIManagerHelper.HideUIRedDot(self.DomainScene(), UIRedDotType.PVESeason).Coroutine();

				self.View.EPage_BattleDeckSkill.ShowPage().Coroutine();
				self.View.EPage_BattleDeckTower.HidePage();
			}
		}

		//控制4个按钮的显影
		private static void SetToggleButtonsActive(this DlgBattleDeck self, bool isRegular)
		{
			self.View.ETabSelectBtnTowerButton.SetVisible(isRegular);
			self.View.ETabUnSelectBtnTowerButton.SetVisible(!isRegular);
			self.View.ETabSelectBtnSkillButton.SetVisible(!isRegular);
			self.View.ETabUnSelectBtnSkillButton.SetVisible(isRegular);
		}

		public static async ETTask PreLoadWindow(this DlgBattleDeck self, ShowWindowData contextData, Action finished)
		{
			switch (self.pageIndex)
			{
				case 0:
					await self.View.EPage_BattleDeckTower.PreLoadWindow();
					break;
				case 1:
					await self.View.EPage_BattleDeckSkill.PreLoadWindow();
					break;
			}

			if (self.IsDisposed)
			{
				return;
			}
			finished?.Invoke();
		}

		public static async ETTask ShowWindow(this DlgBattleDeck self, ShowWindowData contextData = null)
		{
			self.dlgShowTime = TimeHelper.ClientNow();

			self.ShowBg().Coroutine();

			switch (self.pageIndex)
			{
				case 0:
					self.SetToggleButtonsActive(true);
					self.View.EPage_BattleDeckTower.ShowPage().Coroutine();
					self.View.EPage_BattleDeckSkill.HidePage();
					break;
				case 1:
					self.SetToggleButtonsActive(false);
					self.View.EPage_BattleDeckSkill.ShowPage().Coroutine();
					self.View.EPage_BattleDeckTower.HidePage();
					break;
			}
		}

		public static bool ChkCanClickBg(this DlgBattleDeck self)
		{
			if (self.dlgShowTime < TimeHelper.ClientNow() - (long)(1000 * 1f))
			{
				return true;
			}
			return false;
		}

		public static void HideWindow(this DlgBattleDeck self)
		{
		}

		//背景
		public static async ETTask ShowBg(this DlgBattleDeck self)
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

		//外部调用（设置select/unlock两个按钮状态）
		public static async ETTask RefreshWhenPlayerModelChg(this DlgBattleDeck self, PlayerModelType playerModelType)
		{
			switch (self.pageIndex)
			{
				case 0:
					if (playerModelType == PlayerModelType.BackPack || playerModelType == PlayerModelType.BattleCard)
					{
						self.View.EPage_BattleDeckTower.Refresh().Coroutine();
					}
					break;
				case 1:
					if (playerModelType == PlayerModelType.BackPack || playerModelType == PlayerModelType.BattleSkill)
					{
						self.View.EPage_BattleDeckSkill.Refresh().Coroutine();
					}
					break;
			}
		}

		//退出按钮
		public static async ETTask Back(this DlgBattleDeck self)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Back);
			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgBattleDeck>();
			await UIManagerHelper.EnterGameModeUI(self.DomainScene());
		}

	}
}
