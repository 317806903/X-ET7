using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace ET.Client
{
	[FriendOf(typeof(DlgVideoShow))]
	public static class DlgVideoShowSystem
	{
		public static void RegisterUIEvent(this DlgVideoShow self)
		{
			self.View.E_NextButton.AddListenerAsync(self.DoNext);
		}

		public static void ShowWindow(this DlgVideoShow self, ShowWindowData contextData = null)
		{
			self._ShowWindow().Coroutine();
		}

		public static async ETTask _ShowWindow(this DlgVideoShow self)
		{
			self.videoPath = "Assets/ResAB/Video/Beginners_Movie.mp4";

			self.View.E_TextContextTextMeshProUGUI.SetVisible(false);
			self.View.E_NextButton.SetVisible(false);

			VideoPlayer videoPlayer = self.View.uiTransform.transform.Find("VideoPlay").gameObject.GetComponent<VideoPlayer>();
			RawImage image = self.View.E_VideoShowRawImage;
			VideoClip videoToPlay = await ResComponent.Instance.LoadAssetAsync<VideoClip>(self.videoPath);
			AudioSource audioSource = null;

			videoPlayer.source = VideoSource.VideoClip;
			if (audioSource == null)
			{
				videoPlayer.audioOutputMode = VideoAudioOutputMode.None;
			}
			else
			{
				videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
				//videoPlayer.EnableAudioTrack(0, true);
				videoPlayer.SetTargetAudioSource(0, audioSource);
			}
			videoPlayer.clip = videoToPlay;
			videoPlayer.Prepare();
			while (videoPlayer.isPrepared == false)
			{
				await TimerComponent.Instance.WaitFrameAsync();
			}

			image.texture = videoPlayer.texture;
			videoPlayer.Play();
			while (videoPlayer.isPlaying)
			{
				await TimerComponent.Instance.WaitFrameAsync();
			}
		}

		public static async ETTask DoNext(this DlgVideoShow self)
		{
			UIAudioManagerHelper.PlayUIAudioConfirm(self.DomainScene());

			ResComponent.Instance.UnloadAsset(self.videoPath);

			UIManagerHelper.GetUIComponent(self.DomainScene()).CloseWindow<DlgVideoShow>();
		}

	}
}
