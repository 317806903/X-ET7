using System.Collections;
using System.Collections.Generic;
using System;
using ET.AbilityConfig;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

namespace ET.Client
{
	[Invoke(TimerInvokeType.PhysicalStrengthTimer)]
	public class DlgPhysicalStrengthTimer: ATimer<DlgPhysicalStrength>
	{
		protected override void Run(DlgPhysicalStrength self)
		{
			try
			{
				self.Update().Coroutine();
			}
			catch (Exception e)
			{
				Log.Error($"move timer error: {self.Id}\n{e}");
			}
		}
	}
	
	[FriendOf(typeof(DlgPhysicalStrength))]
	public static class DlgPhysicalStrengthSystem
	{
		public static void RegisterUIEvent(this DlgPhysicalStrength self)
		{
			self.View.EButton_CloseButton.AddListener(self.OnCloseBtnClick);
			self.View.E_BG_ClickButton.AddListener(self.OnBGClick);
			
			self.View.EButton_WatchADButton.AddListenerAsync(self.GetPhysicalStrenthByADAsync);
			self.View.EButton_CoinButton.AddListener(self.GetPhysicalStrengthByCoin);

		}

		public static void ShowWindow(this DlgPhysicalStrength self, ShowWindowData contextData = null)
		{
			self.ShowBg().Coroutine();
			self.Update().Coroutine();
			
			self.Timer = TimerComponent.Instance.NewRepeatedTimer(1000, TimerInvokeType.PhysicalStrengthTimer, self);
		}
		
		public static async ETTask ShowBg(this DlgPhysicalStrength self)
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

		public static void HideWindow(this DlgPhysicalStrength self)
		{
			TimerComponent.Instance?.Remove(ref self.Timer);
		}

		public static async ETTask Update(this DlgPhysicalStrength self)
		{
			PlayerBaseInfoComponent playerBaseInfoComponent =
					await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());
			int maxPhysicalStrength = GlobalSettingCfgCategory.Instance.UpperLimitOfPhysicalStrength;
			int curPhysicalStrength = playerBaseInfoComponent.GetPhysicalStrength();
			string msg = curPhysicalStrength + "/" + maxPhysicalStrength;
			self.View.ELabel_PercentageTextMeshProUGUI.text = msg;
			self.View.E_PhysicalStrengthSlider.value = (float)curPhysicalStrength / maxPhysicalStrength;
			
			TimeSpan timeSpan = TimeSpan.FromSeconds(playerBaseInfoComponent.GetRevoerLeftTime());
			self.View.ELabel_RecoverLeftTImeTextMeshProUGUI.text = timeSpan.ToString();
			self.View.ELabel_RcoverNumTextMeshProUGUI.text = GlobalSettingCfgCategory.Instance.RecoverIncreaseOfPhysicalStrength.ToString();
		}

		public static void OnCloseBtnClick(this DlgPhysicalStrength self)
		{
			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgPhysicalStrength>();
		}
		
		public static void OnBGClick(this DlgPhysicalStrength self)
		{
			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgPhysicalStrength>();
		}

		public static async ETTask GetPhysicalStrenthByADAsync(this DlgPhysicalStrength self)
		{
			await ET.Client.AdmobSDKComponent.Instance.ShowRewardedAd(async () =>
            {
				await ET.Client.PlayerCacheHelper.AddPlayerPhysicalStrenthByAdAsync(self.DomainScene());
            });
		}

		public static void GetPhysicalStrengthByCoin(this DlgPhysicalStrength self)
		{
		}

	}
}
