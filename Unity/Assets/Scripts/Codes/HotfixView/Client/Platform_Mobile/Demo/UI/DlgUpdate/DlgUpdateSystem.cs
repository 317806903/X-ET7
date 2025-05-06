using System.Collections;
using System.Collections.Generic;
using System;
using ET.EventType;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YooAsset;

namespace ET.Client
{
	[FriendOf(typeof(DlgUpdate))]
	public static class DlgUpdateSystem
	{

		public static void RegisterUIEvent(this DlgUpdate self)
		{
			self.rectTransBackground = (RectTransform)self.View.uiTransform.Find("Sprite_BackGround/HotUpdates/ProgressPrarent/Processing/Background");
			self.rectTransValueImage = (RectTransform)self.View.uiTransform.Find("Sprite_BackGround/HotUpdates/ProgressPrarent/Processing/Background/Value");
			self.transPercentage = self.View.uiTransform.Find("Sprite_BackGround/HotUpdates/ProgressPrarent/Processing/Percentage");

			self.transProgress = self.View.uiTransform.Find("Sprite_BackGround/HotUpdates/ProgressPrarent");
			self.transCheckUpdate = self.View.uiTransform.Find("Sprite_BackGround/HotUpdates/CheckUpdate");
		}

		public static async ETTask ShowWindow(this DlgUpdate self, ShowWindowData contextData = null)
		{
			self.LoadBG().Coroutine();
			self.transProgress.gameObject.SetActive(false);
			self.transCheckUpdate.gameObject.SetActive(true);
			self.transCheckUpdate.GetComponent<TextMeshProUGUI>().text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Res_CheckUpdate");
			self.ShowProcess(0);
			ResourcePackage package = YooAssets.GetPackage("DefaultPackage");
			self.View.ELabel_VersionTextMeshProUGUI.text = $"{ResConfig.Instance.Channel} {Application.version}-{package.GetPackageVersion()}";
		}

		public static void HideWindow(this DlgUpdate self)
		{

		}

		public static async ETTask LoadBG(this DlgUpdate self)
		{
			self.View.E_BGImage.LoadBG(self).Coroutine();
		}

		public static void ShowProcess(this DlgUpdate self, float per)
		{
			float backgroudWidth = self.rectTransBackground.sizeDelta.x;
			float newValueImageWidth = Mathf.Clamp(per * backgroudWidth, self.rectTransValueImage.sizeDelta.y, backgroudWidth);
			self.rectTransValueImage.sizeDelta = new Vector2(newValueImageWidth, self.rectTransValueImage.sizeDelta.y);
		}

		public static void UpdateUI(this DlgUpdate self, ClientEventType.OnPatchDownloadProgress a)
		{
			self.HideCheckUpdateText();

			self.transProgress.gameObject.SetActive(true);
			self.View.ELabel_TotalDownloadCountText.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_UpdateRes_TotalNum", a.TotalDownloadCount);
			self.View.ELabel_CurrentDownloadCountText.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_UpdateRes_CurDownNum", a.CurrentDownloadCount);
			self.View.ELabel_TotalDownloadSizeBytesText.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_UpdateRes_TotalDownSize", a.TotalDownloadSizeBytes);
			self.View.ELabel_CurrentDownloadSizeBytesText.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_UpdateRes_TotalNum", a.CurrentDownloadSizeBytes);
			long totalDownloadSizeMB = a.TotalDownloadSizeBytes / (1024 * 1024);
			if (totalDownloadSizeMB == 0)
			{
				totalDownloadSizeMB = 1;
			}
			long currentDownloadSizeMB = a.CurrentDownloadSizeBytes / (1024 * 1024);
			if (currentDownloadSizeMB == 0)
			{
				currentDownloadSizeMB = 1;
			}
			self.transPercentage.gameObject.GetComponent<TextMeshProUGUI>().text =
					LocalizeComponent.Instance.GetTextValue("TextCode_Key_Res_DownloadProgress", currentDownloadSizeMB, totalDownloadSizeMB);

			float per = (float)a.CurrentDownloadSizeBytes/a.TotalDownloadSizeBytes;
			self.ShowProcess(per);
		}

		public static void HideCheckUpdateText(this DlgUpdate self)
		{
			self.transCheckUpdate.gameObject.SetActive(false);
		}
	}
}
