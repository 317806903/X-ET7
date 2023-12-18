using System.Collections;
using System.Collections.Generic;
using System;
using ET.AbilityConfig;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace ET.Client
{
	[FriendOf(typeof(DlgBeginnersGuideStory))]
	public static class DlgBeginnersGuideStorySystem
	{
		public static void RegisterUIEvent(this DlgBeginnersGuideStory self)
		{
			self.View.E_BG1Button.AddListenerAsync(self.DoNext);
			self.View.E_BG2Button.AddListenerAsync(self.DoNext);
			self.View.E_BG3Button.AddListenerAsync(self.DoNext);
			self.View.E_BG4Button.AddListenerAsync(self.DoNext);
			self.View.E_BG5Button.AddListenerAsync(self.DoNext);
		}

		public static void ShowWindow(this DlgBeginnersGuideStory self, ShowWindowData contextData = null)
		{
			self.index = 1;
			DlgBeginnersGuideStory_ShowWindowData dlgBeginnersGuideStoryShowWindowData = (DlgBeginnersGuideStory_ShowWindowData)contextData;
			self.finishCallBack = dlgBeginnersGuideStoryShowWindowData.finishCallBack;

			for (int i = 0; i < self.totalNum; i++)
			{
				Transform trans = self.View.uiTransform.transform.Find($"story{i + 1}");
				trans.gameObject.SetActive(false);
			}
			self.View.E_ImgBGImage.SetVisible(true);
			self.DoNext().Coroutine();
		}

		public static async ETTask DoNext(this DlgBeginnersGuideStory self)
		{
			UIAudioManagerHelper.PlayUIAudioConfirm(self.DomainScene());

			await self.ShowStory(self.index++);
		}

		public static async ETTask ShowStory(this DlgBeginnersGuideStory self, int index)
		{
			if (index < self.totalNum)
			{
				await self.DoShowStory(index);
			}
			else if (index == self.totalNum)
			{
				await self.ShowVideo(index);
			}
			else
			{
				UIManagerHelper.GetUIComponent(self.DomainScene()).CloseWindow<DlgBeginnersGuideStory>();
				self.finishCallBack?.Invoke();
			}
		}

		public static async ETTask DoShowStory(this DlgBeginnersGuideStory self, int index)
		{
			Transform oldTrans = null;
			if (index > 1)
			{
				oldTrans = self.View.uiTransform.transform.Find($"story{index-1}");
				if (index == 2)
				{
					self.View.E_ImgBGImage.SetVisible(true);
				}
			}
			if (oldTrans != null)
			{
				Animator oldAnimator = oldTrans.gameObject.GetComponent<Animator>();
				oldAnimator.Play("BeginnersGuideStory_end", 0, 0);
			}
			Transform newTrans = self.View.uiTransform.transform.Find($"story{index}");
			newTrans.gameObject.SetActive(true);
			Animator animator = newTrans.gameObject.GetComponent<Animator>();
			animator.Play("BeginnersGuideStory_start", 0, 0);

			await ETTask.CompletedTask;
		}

		public static async ETTask ShowVideo(this DlgBeginnersGuideStory self, int index)
		{
			await self.DoShowStory(index);
			Transform newTrans = self.View.uiTransform.transform.Find($"story{index}");
			newTrans.gameObject.SetActive(true);
			VideoPlayer videoPlayer = self.View.uiTransform.transform.Find("VideoPlay").gameObject.GetComponent<VideoPlayer>();
			RawImage image = newTrans.Find("VideoImg").gameObject.GetComponent<RawImage>();
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

	}
}
