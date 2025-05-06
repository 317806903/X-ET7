using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using ET.AbilityConfig;
using UnityEngine.EventSystems;

namespace ET.Client
{
    [Invoke(TimerInvokeType.DlgTutorialOneFrameTimer)]
    public class DlgTutorialOneTimer : ATimer<DlgTutorialOne>
    {
        protected override void Run(DlgTutorialOne self)
        {
            try
            {
                if (self.IsDisposed||self.isDrag)
                {
                    return;
                }
                self.Update();

            }
            catch (Exception e)
            {
                Log.Error($"DlgVideoShow timer error: {self.Id}\n{e}");
            }
        }
    }
    [FriendOf(typeof(DlgTutorialOne))]
	public static class DlgTutorialOneSystem
	{
		public static void RegisterUIEvent(this DlgTutorialOne self)
		{
			self.View.E_QuitBattleButton.AddListenerAsync(self.OnClickBack);
			self.View.EButton_PauseButton.AddListenerAsync(self.OnClickPause);
			self.View.E_SliderSlider.AddListener(self.OnClickSlider);
            self.View.EButton_VideoButton.AddListenerAsync(self.OnClickVideo);

			self.videoPlayer = self.View.E_videoVideoPlayer;
			self.videoPlayer.loopPointReached += (VideoPlayer source) =>
			{
				if (self.IsDisposed)
				{
					return;
				}

				EventSystem.Instance.Publish(self.DomainScene(), new ClientEventType.NoticeEventLogging()
				{
					eventName = "VideoFinished",
					properties = new()
					{
						{"video_id", self.tutorialCfgId},
					}
				});
			};
			self.rawImageWidth = self.View.E_VideoShowRawImage.rectTransform.rect.width;
			self.rawImageHeight = self.View.E_VideoShowRawImage.rectTransform.rect.height;
		}

		public static async ETTask ShowWindow(this DlgTutorialOne self, ShowWindowData contextData = null)
		{
			self.dlgShowTime = TimeHelper.ClientNow();
			self.Timer = TimerComponent.Instance.NewFrameTimer(TimerInvokeType.DlgTutorialOneFrameTimer, self);
			DlgTutorialOne_ShowWindowData _DlgTutorialOne_ShowWindowData = contextData as DlgTutorialOne_ShowWindowData;
			string tutorialCfgId = _DlgTutorialOne_ShowWindowData.tutorialCfgId;
			await self.PlayVideo(tutorialCfgId);
		}

		public static bool ChkCanClickBg(this DlgTutorialOne self)
		{
			if (self.dlgShowTime < TimeHelper.ClientNow() - (long)(1000 * 1f))
			{
				return true;
			}
			return false;
		}

		public static void HideWindow(this DlgTutorialOne self)
		{
            UIAudioManagerHelper.ResetMusicStatus(self.DomainScene());
            TimerComponent.Instance?.Remove(ref self.Timer);
            if (self.videoPlayer != null)
            {
	            self.videoPlayer.Stop();
            }
        }

		public static async ETTask OnClickBack(this DlgTutorialOne self)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Back);
			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgTutorialOne>();
			if(string.IsNullOrEmpty(self.videoPath) == false)
			{
				ResComponent.Instance.UnloadAsset(self.videoPath);
			}
			GL.Clear(false, true, Color.black);
			await ETTask.CompletedTask;
		}

		//播放视频
		public static async ETTask PlayVideo(this DlgTutorialOne self, string tutorialCfgId)
		{
			TutorialCfg tutorialCfg = TutorialCfgCategory.Instance.Get(tutorialCfgId);
			if(tutorialCfg.IsSound)
                UIAudioManagerHelper.StopMusic(self.DomainScene());
			else
                UIAudioManagerHelper.ResetMusicStatus(self.DomainScene());
            self.tutorialCfgId = tutorialCfg.Id;
			EventSystem.Instance.Publish(self.DomainScene(), new ClientEventType.NoticeEventLogging()
			{
				eventName = "VideoPlayed",
				properties = new()
				{
					{"video_id", self.tutorialCfgId},
				}
			});

			self.View.E_VideoShowRawImage.enabled = false;

            string videoPath = tutorialCfg.ResVideo_Ref.ResName;
            string info = $"<b><color=#ffffff>{tutorialCfg.Name}:</color></b> {tutorialCfg.Desc}";
			self.View.ELabel_InfoTextMeshProUGUI.text = info;
			self.videoPlayer.source = VideoSource.VideoClip;
			self.videoPlayer.audioOutputMode = VideoAudioOutputMode.Direct;
			if (self.videoPlayer.isPlaying)
			{
				self.videoPlayer.Stop();
			}
			VideoClip videoClip = await ResComponent.Instance.LoadAssetAsync<VideoClip>(videoPath);
			self.videoPlayer.Prepare();
			self.videoPlayer.clip = videoClip;
			while (!self.videoPlayer.isPrepared)
			{
				await TimerComponent.Instance.WaitAsync(100);
				if (self.IsDisposed)
				{
					return;
				}
			}

			self.VideoAdaptive(self.videoPlayer);
			self.View.E_VideoShowRawImage.enabled = true;
			self.View.E_VideoShowRawImage.texture = self.videoPlayer.texture;
			self.videoPlayer.Play();
            self.SetUIPlayStatus(true);
            self.videoPlayer.isLooping = true;
			self.videoPath = videoPath;
		}


		public static async ETTask OnClickPause(this DlgTutorialOne self)
		{
            if (self.videoPlayer.isPlaying)
            {
                self.videoPlayer.Pause();
                self.SetUIPlayStatus(false);
            }
            else if (self.videoPlayer.isPaused)
            {
                self.videoPlayer.Play();
                self.SetUIPlayStatus(true);
            }
            await ETTask.CompletedTask;
        }

		public static void OnClickSlider(this DlgTutorialOne self, float scale)
		{
			self.dragTime = TimeHelper.ClientNow() + 500;
			double process = Math.Round(Convert.ToDouble(scale) * Convert.ToDouble(self.videoPlayer.frameCount), 0);
			self.videoPlayer.frame = (long)process;
        }

        public static void Update(this DlgTutorialOne self)
        {
	        if (self == null || self.IsDisposed || self.View == null || self.videoPlayer == null)
	        {
		        return;
	        }
	        if (self.videoPlayer.clip == null)
	        {
		        return;
	        }
	        if (self.dragTime > TimeHelper.ClientNow())
	        {
		        return;
	        }
	        if(self.videoPlayer.isPlaying ==false)
	        {
		        return;
	        }
	        double process = Convert.ToDouble(self.videoPlayer.frame) / Convert.ToDouble(self.videoPlayer.frameCount);
	        self.View.E_SliderSlider.SetValueWithoutNotify((float)process);
        }

        //视频自适应
        public static void VideoAdaptive(this DlgTutorialOne self, VideoPlayer videoPlayer)
        {
            float videoWidth = videoPlayer.texture.width;
            float videoHeight = videoPlayer.texture.height;
            float ratio = videoWidth / videoHeight;
            float rawImageWidth = self.rawImageWidth;
            float rawImageHeight = self.rawImageHeight;
            float rawImageRatio = rawImageWidth / rawImageHeight;
            if (ratio > rawImageRatio)
            {
                float newRawImageHeight = rawImageWidth / ratio;
                self.View.E_VideoShowRawImage.rectTransform.sizeDelta = new Vector2(rawImageWidth, newRawImageHeight);
            }
            else
            {
                float newRawImageWidth = rawImageHeight * ratio;
                self.View.E_VideoShowRawImage.rectTransform.sizeDelta = new Vector2(newRawImageWidth, rawImageHeight);
            }
        }

        public static void SetUIPlayStatus(this DlgTutorialOne self, bool isPlaying)
        {
            if (isPlaying)
            {
                self.View.E_Image_PlayImage.SetVisible(false);
            }
            else
            {
                self.View.E_Image_PlayImage.SetVisible(true);
            }
        }

        public static async ETTask OnClickVideo(this DlgTutorialOne self)
        {
            await self.OnClickPause();
        }
    }
}
