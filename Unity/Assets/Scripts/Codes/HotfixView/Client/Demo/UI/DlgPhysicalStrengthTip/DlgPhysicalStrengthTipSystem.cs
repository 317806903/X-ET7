using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgPhysicalStrengthTip))]
	public static class DlgPhysicalStrengthTipSystem
	{
		public static void RegisterUIEvent(this DlgPhysicalStrengthTip self)
		{
			self.View.EButton_CloseButton.AddListener(self.OnCloseBtnClick);
			self.View.E_BG_ClickButton.AddListener(self.OnBGClick);
			self.View.EButton_CancelButton.AddListener(self.OnCancelClick);
		}

		public static void ShowWindow(this DlgPhysicalStrengthTip self, ShowWindowData contextData = null)
		{
		}

		public static void SetText(this DlgPhysicalStrengthTip self, string takePhysicalStrength)
		{
			self.View.ELabel_TakephysicalStrengthTextMeshProUGUI.text = takePhysicalStrength;
		}
		
		public static void OnCloseBtnClick(this DlgPhysicalStrengthTip self)
		{
			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgPhysicalStrengthTip>();
		}
		
		public static void OnBGClick(this DlgPhysicalStrengthTip self)
		{
			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgPhysicalStrengthTip>();
		}
		
		public static void OnCancelClick(this DlgPhysicalStrengthTip self)
		{
			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgPhysicalStrengthTip>();
		}


	}
}
