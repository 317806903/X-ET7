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

		public static async ETTask ShowWindow(this DlgFixedMenu self, ShowWindowData contextData = null)
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
			await self.UpdateTokenArcadeCoin();
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
				self.View.EButton_TokenArcadeCoinButton.SetVisible(true);
				self.View.EButton_TokenArcadeCoinButton.AddListenerAsync(self.ClickArcadeCoin);
				self.UpdateTokenArcadeCoin().Coroutine();
			}
			else
			{
				self.View.EButton_TokenArcadeCoinButton.SetVisible(false);
			}

			if (ET.SceneHelper.ChkIsGameModeArcade() == false && status)
			{
				self.View.EButton_TokenDiamondButton.SetVisible(true);

				self.View.EButton_TokenDiamondButton.AddListenerAsync(self.ClickDiamond);
				self.UpdateTokenDiamond().Coroutine();
			}
			else
			{
				self.View.EButton_TokenDiamondButton.SetVisible(false);
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

		public static async ETTask UpdateTokenArcadeCoin(this DlgFixedMenu self)
		{
			int curArcadeCoin = await PlayerCacheHelper.GetTokenArcadeCoin(self.DomainScene());
			string msg = $"{curArcadeCoin}";
			self.View.ELabel_TokenArcadeCoinNumTextMeshProUGUI.text = msg;
		}

		public static async ETTask UpdateTokenDiamond(this DlgFixedMenu self)
		{
			int curDiamond = await PlayerCacheHelper.GetTokenDiamond(self.DomainScene());
			string msg = $"{curDiamond}";
			self.View.ELabel_TokenDiamondNumTextMeshProUGUI.text = msg;
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

		public static async ETTask ClickDiamond(this DlgFixedMenu self)
		{
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

            Log.Error($"Click ClickDiamond");
            bool isGetDiamondWhenClick = ChannelSettingComponent.Instance.ChkIsGetDiamondWhenClick();
            if (isGetDiamondWhenClick)
            {
	            PlayerBackPackComponent playerBackPackComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerBackPack(self.DomainScene(), false);
	            string itemCfgId = ItemHelper.GetTokenDiamondCfgId();
	            playerBackPackComponent.AddItem(itemCfgId, 50);
	            await ET.Client.PlayerCacheHelper.SaveMyPlayerModel(self.DomainScene(), PlayerModelType.BackPack, null);
            }
		}

	}
}
