using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgGameReport))]
	public static class DlgGameReportSystem
	{
		public static void RegisterUIEvent(this DlgGameReport self)
		{
			self.View.E_SendComplainButton.AddListenerAsync(self.OnSendComplain);
			self.View.E_CloseComplainButton.AddListenerAsync(self.OnCloseComplain);

			self.View.E_InputFieldComplainTMP_InputField.onEndEdit.AddListener(self.OnEndEdit);
			self.View.E_InputFieldComplainTMP_InputField.onValueChanged.AddListener(self.OnValueChanged);
		}

		public static void ShowWindow(this DlgGameReport self, ShowWindowData contextData = null)
		{
			self.limitNum = 450;

			self.View.E_Toggle_3Toggle.isOn = true;
			self.View.E_InputFieldComplainTMP_InputField.text = "";

			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgFixedMenuHighest>();
		}

		public static void HideWindow(this DlgGameReport self)
		{
			UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindow<DlgFixedMenuHighest>();
		}

		public static async ETTask OnCloseComplain(this DlgGameReport self)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);
			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameReport>();
		}

        public static async ETTask OnSendComplain(this DlgGameReport self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

            var msg = self.View.E_InputFieldComplainTMP_InputField.text;

            if (msg.Length < 1)
            {
	            string tipMsg = "cannot be empty";
	            UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
	            return;
            }

            Toggle toggle = self.View.E_ToggleGroupToggleGroup.GetFirstActiveToggle();
            string toggleType = toggle.transform.Find("info").gameObject.GetComponent<TMPro.TextMeshProUGUI>().text;

            EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
            {
	            eventName = "ReportEnded",
	            properties = new()
	            {
		            {"toggleType", toggleType},
		            {"complain", msg},
	            }
            });

            UIManagerHelper.ShowTip(self.DomainScene(), "Success");

            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameReport>();
        }

        public static void OnEndEdit(this DlgGameReport self, string value)
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

        public static void OnValueChanged(this DlgGameReport self, string value)
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
