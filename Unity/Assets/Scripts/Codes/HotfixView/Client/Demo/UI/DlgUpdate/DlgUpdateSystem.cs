using System.Collections;
using System.Collections.Generic;
using System;
using ET.EventType;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgUpdate))]
	public static class DlgUpdateSystem
	{

		public static void RegisterUIEvent(this DlgUpdate self)
		{

		}

		public static void ShowWindow(this DlgUpdate self, ShowWindowData contextData = null)
		{
		}

		public static void UpdateUI(this DlgUpdate self, OnPatchDownloadProgress a)
		{
			self.View.ELabel_TotalDownloadCountText.text = $"总下载数量:{a.TotalDownloadCount}";
			self.View.ELabel_CurrentDownloadCountText.text = $"当前已下载数量:{a.CurrentDownloadCount}";
			self.View.ELabel_TotalDownloadSizeBytesText.text = $"总下载大小:{a.TotalDownloadSizeBytes}";
			self.View.ELabel_CurrentDownloadSizeBytesText.text = $"当前已下载大小:{a.CurrentDownloadSizeBytes}";
			self.View.E_SliderSlider.value = (float)a.CurrentDownloadCount/a.TotalDownloadCount;
		}



	}
}
