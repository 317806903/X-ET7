using System.Collections;
using System.Collections.Generic;
using System;
using ET.AbilityConfig;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[Invoke(TimerInvokeType.DlgFixedMenuFrameTimer)]
	public class DlgFixedMenuTimer: ATimer<DlgFixedMenu>
	{
		protected override void Run(DlgFixedMenu self)
		{
			try
			{
				if (self.IsDisposed)
				{
					TimerComponent.Instance?.Remove(ref self.Timer);
					return;
				}
				self.Update();
			}
			catch (Exception e)
			{
				Log.Error($"DlgFixedMenu timer error: {self.Id}\n{e}");
			}
		}
	}

	[FriendOf(typeof(DlgFixedMenu))]
	public static class DlgFixedMenuSystem
	{
		public static void RegisterUIEvent(this DlgFixedMenu self)
		{
		}

		public static void ShowWindow(this DlgFixedMenu self, ShowWindowData contextData = null)
		{
			self._ShowWindow().Coroutine();
		}

		public static async ETTask _ShowWindow(this DlgFixedMenu self)
		{
			self.ShowCoinList(true);
			self.Timer = TimerComponent.Instance.NewFrameTimer(TimerInvokeType.DlgFixedMenuFrameTimer, self);
		}

		public static async ETTask RefreshWhenBaseInfoChg(this DlgFixedMenu self)
		{
			await self.UpdatePhysicalStrength();
			await self.UpdateArcadeCoin();
		}

		public static void ShowCoinList(this DlgFixedMenu self, bool status)
		{
			self.View.EG_CoinListRectTransform.SetVisible(status);

			self.ShowCoinBar(status);
			self.ShowPhysicalBar(status);
		}

		public static void ShowCoinBar(this DlgFixedMenu self, bool status)
		{
			if (ET.SceneHelper.ChkIsGameModeArcade() && status)
			{
				self.View.EButton_ArcadeCoinButton.SetVisible(true);
				self.View.EButton_ArcadeCoinButton.AddListenerAsync(self.ClickArcadeCoin);
				self.UpdateArcadeCoin().Coroutine();
			}
			else
			{
				self.View.EButton_ArcadeCoinButton.SetVisible(false);
			}
		}

		public static void ShowPhysicalBar(this DlgFixedMenu self, bool status)
		{
			if (ET.SceneHelper.ChkIsGameModeArcade())
			{
				self.View.EButton_PhysicalStrengthButton.SetVisible(false);
				return;
			}
			if (GlobalSettingCfgCategory.Instance.PhysicalStrengthShow && status)
			{
				self.View.EButton_PhysicalStrengthButton.SetVisible(true);
				self.View.EButton_PhysicalStrengthButton.AddListenerAsync(self.ClickPhysicalStrength);
				self.UpdatePhysicalStrength().Coroutine();
			}
			else
			{
				self.View.EButton_PhysicalStrengthButton.SetVisible(false);
			}
		}

		public static async ETTask UpdatePhysicalStrength(this DlgFixedMenu self)
		{
			PlayerBaseInfoComponent playerBaseInfoComponent =
				await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());
			if (playerBaseInfoComponent == null)
			{
				return;
			}
			int maxPhysicalStrength = GlobalSettingCfgCategory.Instance.UpperLimitOfPhysicalStrength;
			int curPhysicalStrength = playerBaseInfoComponent.GetPhysicalStrength();
			string msg = curPhysicalStrength + "/" + maxPhysicalStrength;
			self.View.ELabel_PhysicalStrengthNumTextMeshProUGUI.text = msg;
		}

		public static async ETTask UpdateArcadeCoin(this DlgFixedMenu self)
		{
			PlayerBaseInfoComponent playerBaseInfoComponent =
				await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());
			if (playerBaseInfoComponent == null)
			{
				return;
			}
			int arcadeCoinNum = playerBaseInfoComponent.arcadeCoinNum;
			string msg = $"{arcadeCoinNum}";
			self.View.ELabel_ArcadeCoinNumTextMeshProUGUI.text = msg;
		}

		public static void HideWindow(this DlgFixedMenu self)
		{
			TimerComponent.Instance?.Remove(ref self.Timer);
		}

		public static void Update(this DlgFixedMenu self)
		{
			if (++self.curFrame >= self.waitFrame)
			{
				self.curFrame = 0;

				bool status = self.ChkIsShowCoinList();
				self.ShowCoinList(status);
			}
		}

		public static bool ChkIsShowCoinList(this DlgFixedMenu self)
		{
			DlgARHall _DlgARHall = UIManagerHelper.GetUIComponent(self.DomainScene()).GetDlgLogic<DlgARHall>(true);
			if (_DlgARHall != null)
			{
				return false;
			}

			if (ET.Client.ARSessionHelper.ChkARMirrorSceneUIShow(self.DomainScene()))
			{
				return false;
			}

			PlayerStatusComponent playerStatusComponent = ET.Client.PlayerStatusHelper.GetMyPlayerStatusComponent(self.DomainScene());
			PlayerStatus playerStatus = playerStatusComponent.PlayerStatus;
			if (playerStatus == PlayerStatus.Hall)
			{
				return true;
			}
			else if (playerStatus == PlayerStatus.Room)
			{
				return true;
			}
			else if (playerStatus == PlayerStatus.Battle)
			{
				return false;
			}
			return false;
		}

		public static async ETTask ClickPhysicalStrength(this DlgFixedMenu self)
		{
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

			await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgPhysicalStrength>();
		}

		public static async ETTask ClickArcadeCoin(this DlgFixedMenu self)
		{
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

			UIManagerHelper.ShowDlgArcade(self.DomainScene(), 8, null);
		}

	}
}
