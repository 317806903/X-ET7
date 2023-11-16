using System.Collections;
using System.Collections.Generic;
using System;
using ET.EventType;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgUpdate))]
	public static class DlgUpdateSystem
	{

		public static void RegisterUIEvent(this DlgUpdate self)
		{
			self.transBackground = self.View.uiTransform.Find("Sprite_BackGround/ProgressPrarent/Downloading/Background");
			self.transPercentage = self.View.uiTransform.Find("Sprite_BackGround/ProgressPrarent/Downloading/Percentage");
		}

		public static void ShowWindow(this DlgUpdate self, ShowWindowData contextData = null)
		{
			self.ShowProcess(0);
		}

		public static void ShowProcess(this DlgUpdate self, float per)
		{
			self.transBackground.localScale = new Vector3(per, 1, 1);
			self.transPercentage.gameObject.GetComponent<TextMeshProUGUI>().text = $"{(int)(per * 100)}%";
		}

		public static void UpdateUI(this DlgUpdate self, OnPatchDownloadProgress a)
		{
			self.View.ELabel_TotalDownloadCountText.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_UpdateRes_TotalNum", a.TotalDownloadCount);
			self.View.ELabel_CurrentDownloadCountText.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_UpdateRes_CurDownNum", a.CurrentDownloadCount);
			self.View.ELabel_TotalDownloadSizeBytesText.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_UpdateRes_TotalDownSize", a.TotalDownloadSizeBytes);
			self.View.ELabel_CurrentDownloadSizeBytesText.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_UpdateRes_TotalNum", a.CurrentDownloadSizeBytes);
			float per = (float)a.CurrentDownloadCount/a.TotalDownloadCount;

			self.ShowProcess(per);
		}
	}
}
