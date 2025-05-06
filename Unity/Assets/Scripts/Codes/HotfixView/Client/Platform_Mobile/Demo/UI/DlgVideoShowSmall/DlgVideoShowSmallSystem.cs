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

		public static async ETTask ShowWindow(this DlgVideoShowSmall self, ShowWindowData contextData = null)
		{
			self._ShowWindow().Coroutine();
		}

		public static void HideWindow(this DlgVideoShowSmall self)
		{
			if (self.videoPlayer != null)
			{
				self.videoPlayer.Stop();
			}
		}

		public static async ETTask _ShowWindow(this DlgVideoShowSmall self)
		{
			await TimerComponent.Instance.WaitFrameAsync();
			GL.Clear(false, true, Color.black);
			// ET.Client.UIAudioManagerHelper.StopMusic(self.DomainScene());
			self.videoPlayer = self.View.uiTransform.transform.Find("VideoPlay").gameObject.GetComponent<VideoPlayer>();


			self.videoPath = "Assets/ResAB/Video/RealityGuard_Scan_Tutorial.mp4";

			VideoClip videoToPlay = await ResComponent.Instance.LoadAssetAsync<VideoClip>(self.videoPath);

			self.videoPlayer.Stop();
			self.videoPlayer.clip = videoToPlay;
			self.videoPlayer.isLooping = false;
			RawImage image = self.View.E_VideoShowRawImage.gameObject.GetComponent<RawImage>();
			self.videoPlayer.Prepare();
			while (self.videoPlayer.isPrepared == false)
			{
				await TimerComponent.Instance.WaitFrameAsync();
				if (self.IsDisposed)
				{
					return;
				}
			}

			image.texture = self.videoPlayer.texture;
			self.videoPlayer.Play();

			while (true)
			{
				if (self.videoPlayer == null)
				{
					return;
				}
				if (self.videoPlayer.isPlaying == false)
				{
					self.videoPlayer.time = 0;
					self.videoPlayer.Play();
				}
				await TimerComponent.Instance.WaitFrameAsync();
				if (self.IsDisposed)
				{
					return;
				}
			}

		}

		public static async ETTask Stop(this DlgVideoShowSmall self)
		{
			self.videoPlayer.Stop();
			self.videoPlayer = null;
			if(string.IsNullOrEmpty(self.videoPath) == false)
			{
				ResComponent.Instance.UnloadAsset(self.videoPath);
			}

			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgVideoShowSmall>();
			GL.Clear(false, true, Color.black);
		}
	}
}
