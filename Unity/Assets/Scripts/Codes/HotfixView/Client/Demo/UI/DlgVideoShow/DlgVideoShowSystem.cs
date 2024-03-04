using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace ET.Client
{
	[Invoke(TimerInvokeType.DlgVideoShowFrameTimer)]
	public class DlgVideoShowTimer: ATimer<DlgVideoShow>
	{
		protected override void Run(DlgVideoShow self)
		{
			try
			{
				self.Update();
			}
			catch (Exception e)
			{
				Log.Error($"DlgVideoShow timer error: {self.Id}\n{e}");
			}
		}
	}

	[FriendOf(typeof(DlgVideoShow))]
	public static class DlgVideoShowSystem
	{
		public static void RegisterUIEvent(this DlgVideoShow self)
		{
			self.View.E_ReturnLoginButton.AddListenerAsync(self.DoNext);
			self.View.E_NextButton.AddListenerAsync(self.DoNext);
			self.View.E_VideoShowButton.AddListenerAsync(self.ClickVideo);
			self.View.E_SliderSlider.AddListener(self.OnSider);
		}

		public static void ShowWindow(this DlgVideoShow self, ShowWindowData contextData = null)
		{
			self._ShowWindow().Coroutine();
			self.Timer = TimerComponent.Instance.NewFrameTimer(TimerInvokeType.DlgVideoShowFrameTimer, self);
		}

		public static void HideWindow(this DlgVideoShow self)
		{
			ET.Client.UIAudioManagerHelper.ResumeMusic(self.DomainScene());
			TimerComponent.Instance?.Remove(ref self.Timer);
		}

		public static async ETTask _ShowWindow(this DlgVideoShow self)
		{
			self.View.E_ReturnLoginButton.SetVisible(false);
			self.View.E_SliderSlider.SetVisible(false);

			ET.Client.UIAudioManagerHelper.StopMusic(self.DomainScene());
			self.videoPath = "Assets/ResAB/Video/Beginners_Movie.mp4";

			self.View.E_TextContextTextMeshProUGUI.SetVisible(false);
			self.View.E_NextButton.SetVisible(false);

			self.videoPlayer = self.View.uiTransform.transform.Find("VideoPlay").gameObject.GetComponent<VideoPlayer>();
			VideoPlayer videoPlayer = self.videoPlayer;
			RawImage image = self.View.E_VideoShowRawImage;
			VideoClip videoToPlay = await ResComponent.Instance.LoadAssetAsync<VideoClip>(self.videoPath);

			videoPlayer.source = VideoSource.VideoClip;

			// AudioSource audioSource = null;
			// if (audioSource == null)
			// {
			// 	videoPlayer.audioOutputMode = VideoAudioOutputMode.None;
			// }
			// else
			// {
			// 	videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
			// 	//videoPlayer.EnableAudioTrack(0, true);
			// 	videoPlayer.SetTargetAudioSource(0, audioSource);
			// }
			videoPlayer.audioOutputMode = VideoAudioOutputMode.Direct;

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
			await TimerComponent.Instance.WaitAsync(1000);
			//self.DoNext().Coroutine();
		}

		public static async ETTask DoNext(this DlgVideoShow self)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

			ResComponent.Instance.UnloadAsset(self.videoPath);

			UIManagerHelper.GetUIComponent(self.DomainScene()).CloseWindow<DlgVideoShow>();
		}

		public static async ETTask ClickVideo(this DlgVideoShow self)
		{
			bool curStatus = self.View.E_ReturnLoginButton.gameObject.activeInHierarchy;
			self.View.E_ReturnLoginButton.SetVisible(!curStatus);
			self.View.E_SliderSlider.SetVisible(!curStatus);
		}

		public static void Update(this DlgVideoShow self)
		{
			self.View.E_SliderSlider.SetValueWithoutNotify((float)self.videoPlayer.time / (float)self.videoPlayer.clip.length);
		}

		public static void OnSider(this DlgVideoShow self, float scale)
		{
			self.videoPlayer.time = (long)(scale * self.videoPlayer.clip.length);
		}

		public static void OnPlay(this DlgVideoShow self)
		{
			self.videoPlayer.Play();
		}

		public static void OnPause(this DlgVideoShow self)
		{
			self.videoPlayer.Pause();
		}
	}
}
