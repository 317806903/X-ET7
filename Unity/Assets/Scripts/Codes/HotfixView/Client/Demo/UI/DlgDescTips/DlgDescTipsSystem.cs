using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgDescTips))]
	public static class DlgDescTipsSystem
	{
		public static void RegisterUIEvent(this DlgDescTips self)
		{
			self.View.EButton_CloseButton.AddListenerAsync(self.OnCloseButton);
			self.View.E_BGButton.AddListenerAsync(self.OnCloseButton);
		}

		public static void ShowWindow(this DlgDescTips self, ShowWindowData contextData = null)
		{
			if (contextData == null)
			{
				Log.Error("contextData == null");
				return;
			}
			else
			{
				DlgDescTips_ShowWindowData _DlgDescTips_ShowWindowData = contextData as DlgDescTips_ShowWindowData;
				self.View.ELabel_Label1TextMeshProUGUI.text = _DlgDescTips_ShowWindowData.Desc;
				self.View.EG_TipsRectTransform.position = _DlgDescTips_ShowWindowData.Pos;
			}
		}

		public static void HideWindow(this DlgDescTips self)
		{
		}

		public static async ETTask OnCloseButton(this DlgDescTips self)
		{
			await TimerComponent.Instance.WaitFrameAsync();
			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgDescTips>();
		}
	}
}
