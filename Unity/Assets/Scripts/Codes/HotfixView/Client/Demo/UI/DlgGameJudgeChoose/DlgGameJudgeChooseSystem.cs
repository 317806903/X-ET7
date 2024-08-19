using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgGameJudgeChoose))]
	public static class DlgGameJudgeChooseSystem
	{
		public static void RegisterUIEvent(this DlgGameJudgeChoose self)
		{
			self.View.E_LoveItMenuButton.AddListenerAsync(self.OnShowLoveIt);
			self.View.E_ComplainMenuButton.AddListenerAsync(self.OnShowComplain);
			self.View.E_CloseMenuButton.AddListenerAsync(self.OnCloseMenu);

			self.View.E_LoveItButton.AddListenerAsync(self.OnSendLoveIt);
			self.View.E_CloseLoveItButton.AddListenerAsync(self.OnCloseLoveIt);

			self.View.E_SendComplainButton.AddListenerAsync(self.OnSendComplain);
			self.View.E_CloseComplainButton.AddListenerAsync(self.OnCloseComplain);

			self.View.E_InputFieldComplainTMP_InputField.onEndEdit.AddListener(self.OnEndEdit);
			self.View.E_InputFieldComplainTMP_InputField.onValueChanged.AddListener(self.OnValueChanged);
		}

		public static async ETTask ShowWindow(this DlgGameJudgeChoose self, ShowWindowData contextData = null)
		{
			self.limitNum = 450;

			self.ShowStep(true, false, false);

			EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
			{
				eventName = "RatingStarted",
			});
		}

		public static void HideWindow(this DlgGameJudgeChoose self)
		{
		}

		public static void ShowStep(this DlgGameJudgeChoose self, bool isMenu, bool isLoveIt, bool isComplain)
		{
			self.View.EG_OperatorMenuRectTransform.SetVisible(isMenu);
			self.View.EG_LoveItRectTransform.SetVisible(isLoveIt);
			self.View.EG_ComplainRectTransform.SetVisible(isComplain);
		}

        public static async ETTask OnCloseMenu(this DlgGameJudgeChoose self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

            EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
            {
	            eventName = "RatingEnded",
	            properties = new()
	            {
		            {"function_name", GameJudgeChooseType.ClickClose.ToString()},
	            }
            });

			await GameJudgeChooseHelper.SendRecordGameJudgeChooseAsync(self.DomainScene(), GameJudgeChooseType.ClickClose, "");

			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameJudgeChoose>();
        }

        public static async ETTask OnCloseLoveIt(this DlgGameJudgeChoose self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);
			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameJudgeChoose>();
        }

        public static async ETTask OnCloseComplain(this DlgGameJudgeChoose self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);
			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameJudgeChoose>();
        }

        public static async ETTask OnShowLoveIt(this DlgGameJudgeChoose self)
        {
	        self.ShowStep(false, true, false);
        }

        public static async ETTask OnShowComplain(this DlgGameJudgeChoose self)
        {
	        self.ShowStep(false, false, true);
        }

        public static async ETTask OnSendLoveIt(this DlgGameJudgeChoose self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

            EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
            {
	            eventName = "RatingEnded",
	            properties = new()
	            {
		            {"function_name", GameJudgeChooseType.ClickLoveIt.ToString()},
	            }
            });

			await GameJudgeChooseHelper.SendRecordGameJudgeChooseAsync(self.DomainScene(), GameJudgeChooseType.ClickLoveIt, "");

			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameJudgeChoose>();

			string url = ChannelSettingComponent.Instance.GetGameJudgeURL();
			bool isWebView = ChannelSettingComponent.Instance.ChkIsGameJudgeUseWebView();

			ET.Client.UIManagerHelper.ShowUrl(self.DomainScene(),url, isWebView);
        }

        public static async ETTask OnSendComplain(this DlgGameJudgeChoose self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

            var msg = self.View.E_InputFieldComplainTMP_InputField.text;
            EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
            {
	            eventName = "RatingEnded",
	            properties = new()
	            {
		            {"function_name", GameJudgeChooseType.ClickComplain.ToString()},
		            {"complain", msg},
	            }
            });

            await GameJudgeChooseHelper.SendRecordGameJudgeChooseAsync(self.DomainScene(), GameJudgeChooseType.ClickComplain, msg);

            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameJudgeChoose>();
        }

        public static void OnEndEdit(this DlgGameJudgeChoose self, string value)
        {
	        if (value.Length < 1)
	        {
		        string tipMsg = "cannot be empty";
		        UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
		        return;
	        }

	        if (value.Length > self.limitNum)
	        {
		        string tipMsg = "length exceeds limit";
		        UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
		        return;
	        }
        }

        public static void OnValueChanged(this DlgGameJudgeChoose self, string value)
        {
	        if (value.Length > self.limitNum)
	        {
		        string valueLimit = value.Substring(0, self.limitNum);
		        self.View.E_InputFieldComplainTMP_InputField.SetTextWithoutNotify(valueLimit);

		        string tipMsg = "length exceeds limit";
		        UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
		        return;
	        }

	        string txt = $"{value.Length}/{self.limitNum}";
	        self.View.E_numInputTextMeshProUGUI.text = txt;
        }

	}
}
