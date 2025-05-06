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
			self.View.E_BGButton.AddListener(() =>
			{
				if (self.ChkCanClickBg() == false)
				{
					return;
				}
				self.OnCloseBtnClick();
			});

			self.View.EButton_WatchADButton.AddListenerAsync(self.GetPhysicalStrenthByADAsync);
			self.View.EButton_CoinButton.AddListener(self.GetPhysicalStrengthByCoin);

		}

		public static async ETTask ShowWindow(this DlgPhysicalStrength self, ShowWindowData contextData = null)
		{
			ET.Client.UIManagerHelper.ShowUIAnimation(self.View.EG_OpenAnimationRectTransform, null);
			self.dlgShowTime = TimeHelper.ClientNow();
			self.ShowBg();
			self.Update().Coroutine();

			self.Timer = TimerComponent.Instance.NewRepeatedTimer(1000, TimerInvokeType.PhysicalStrengthTimer, self);
		}

		public static bool ChkCanClickBg(this DlgPhysicalStrength self)
		{
			if (self.dlgShowTime < TimeHelper.ClientNow() - (long)(1000 * 1f))
			{
				return true;
			}
			return false;
		}

		public static void ShowBg(this DlgPhysicalStrength self)
		{
		}

		public static void HideWindow(this DlgPhysicalStrength self)
		{
			TimerComponent.Instance?.Remove(ref self.Timer);
		}

		public static async ETTask RefreshWhenBaseInfoChg(this DlgPhysicalStrength self)
		{
			await self.Update();
		}

		public static async ETTask Update(this DlgPhysicalStrength self)
		{
			PlayerBaseInfoComponent playerBaseInfoComponent =
					await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());
			if (self.IsDisposed || playerBaseInfoComponent == null || playerBaseInfoComponent.IsDisposed)
			{
				return;
			}
			int maxPhysicalStrength = GlobalSettingCfgCategory.Instance.UpperLimitOfPhysicalStrength;
			int curPhysicalStrength = playerBaseInfoComponent.GetPhysicalStrength();
			string msg = curPhysicalStrength + "/" + maxPhysicalStrength;
			self.View.ELabel_PercentageTextMeshProUGUI.text = msg;
			self.View.E_PhysicalStrengthSlider.value = (float)curPhysicalStrength / maxPhysicalStrength;

			TimeSpan timeSpan = TimeSpan.FromSeconds(playerBaseInfoComponent.GetRecoverLeftTime());

			self.View.ELabel_RcoverNumTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_PhysicalStrength_Regain", GlobalSettingCfgCategory.Instance.RecoverIncreaseOfPhysicalStrength, timeSpan.ToString());

			self.View.ELabel_GetPhysicalStrengthNumTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_PhysicalStrength_WatchAd", GlobalSettingCfgCategory.Instance.RecoverIncreaseOfPhysicalStrengthByAd);

		}

		public static void OnCloseBtnClick(this DlgPhysicalStrength self)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgPhysicalStrength>();
		}

		public static async ETTask GetPhysicalStrenthByADAsync(this DlgPhysicalStrength self)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

			PlayerBaseInfoComponent playerBaseInfoComponent =
					await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());
			int maxPhysicalStrength = GlobalSettingCfgCategory.Instance.UpperLimitOfPhysicalStrength;
			if (playerBaseInfoComponent.GetPhysicalStrength() == maxPhysicalStrength)
			{
				string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_PhysicalStrength_IsFull");
				UIManagerHelper.ShowOnlyConfirm(self.DomainScene(), msg, null, null, null);
			}
			else
			{
				EventSystem.Instance.Publish(self.DomainScene(), new ClientEventType.NoticeEventLogging()
                {
                    eventName = "AdClicked",
                    properties = new()
                        {
                            {"resource", "体力"},
                        }
                });

				EventSystem.Instance.Publish(self.DomainScene(), new ClientEventType.ShowAdvert()
				{
					rewardCallback = () =>
					{
						ET.Client.PlayerCacheHelper.AddPlayerPhysicalStrenthByAdAsync(self.DomainScene()).Coroutine();
					},
					failCallback = () =>
					{
						string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Reward_Without_RewardAd");
						ET.Client.UIManagerHelper.ShowOnlyConfirm(self.DomainScene(), msg,
							() => { ET.Client.PlayerCacheHelper.AddPlayerPhysicalStrenthByAdAsync(self.DomainScene()).Coroutine(); });
					}
				});
			}
		}

		public static void GetPhysicalStrengthByCoin(this DlgPhysicalStrength self)
		{
		}

	}
}
