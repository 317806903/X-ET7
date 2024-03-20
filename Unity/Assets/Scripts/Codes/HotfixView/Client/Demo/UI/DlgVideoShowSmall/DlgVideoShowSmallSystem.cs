using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace ET.Client
{
	[FriendOf(typeof(DlgVideoShowSmall))]
	public static class DlgVideoShowSmallSystem
	{
		public static void RegisterUIEvent(this DlgVideoShowSmall self)
		{
		}

		public static void ShowWindow(this DlgVideoShowSmall self, ShowWindowData contextData = null)
		{
			self._ShowWindow().Coroutine();
		}

		public static void HideWindow(this DlgVideoShowSmall self)
		{
		}

		public static async ETTask _ShowWindow(this DlgVideoShowSmall self)
		{
			// ET.Client.UIAudioManagerHelper.StopMusic(self.DomainScene());
			VideoPlayer videoPlayer = self.View.uiTransform.transform.Find("VideoPlay").gameObject.GetComponent<VideoPlayer>();
			RawImage image = self.View.E_VideoShowRawImage.gameObject.GetComponent<RawImage>();
			videoPlayer.Prepare();
			while (videoPlayer.isPrepared == false)
			{
				await TimerComponent.Instance.WaitFrameAsync();
			}

			image.texture = videoPlayer.texture;
			videoPlayer.Play();
		}
	}
}
