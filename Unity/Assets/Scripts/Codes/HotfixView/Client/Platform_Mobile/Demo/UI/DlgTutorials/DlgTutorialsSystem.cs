using ET.AbilityConfig;
using System;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;

namespace ET.Client
{
    [Invoke(TimerInvokeType.DlgTutorialsFrameTimer)]
    public class DlgTutorialsTimer : ATimer<DlgTutorials>
    {
        protected override void Run(DlgTutorials self)
        {
            try
            {
                if (self.IsDisposed)
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

    [FriendOf(typeof(DlgTutorials))]
	public static class DlgTutorialsSystem
	{
		public static void RegisterUIEvent(this DlgTutorials self)
		{
            self.View.E_QuitBattleButton.AddListenerAsync(self.DoNext);
            self.View.EButton_VideoButton.AddListenerAsync(self.ClickVideo);
            self.View.E_SliderSlider.AddListener(self.OnSider);
            self.View.EButton_PauseButton.AddListenerAsync(self.ClickPauseButton);
            self.View.ELoopScrollList_VideoSelectLoopVerticalScrollRect.prefabSource.prefabName = "Item_Tutorials";
            self.View.ELoopScrollList_VideoSelectLoopVerticalScrollRect.prefabSource.poolSize = 7;
            self.View.ELoopScrollList_VideoSelectLoopVerticalScrollRect.AddItemRefreshListener((transform, index) =>
            {
                self.AddItemRefreshCallBack(transform, index).Coroutine();
            });
            self.Init();
        }

		public static async ETTask ShowWindow(this DlgTutorials self, ShowWindowData contextData = null)
		{
            self.dlgShowTime = TimeHelper.ClientNow();
            //根据不同语言需要多次后去获取视频列表
            self.GetVideoList();
            await UIManagerHelper.HideUIRedDot(self.DomainScene(), UIRedDotType.Tutorial);

            self.AddUIScrollItems(ref self.videoSelectDic, self.tutorialCfgList.Count);
            self.View.ELoopScrollList_VideoSelectLoopVerticalScrollRect.SetVisible(true, self.tutorialCfgList.Count);
            await self.PlayDefault();

            self.Timer = TimerComponent.Instance.NewFrameTimer(TimerInvokeType.DlgTutorialsFrameTimer, self);
        }

		public static bool ChkCanClickBg(this DlgTutorials self)
		{
			if (self.dlgShowTime < TimeHelper.ClientNow() - (long)(1000 * 1f))
			{
				return true;
			}
			return false;
		}

		public static void HideWindow(this DlgTutorials self)
		{
            ET.Client.UIAudioManagerHelper.ResetMusicStatus(self.DomainScene());
            TimerComponent.Instance?.Remove(ref self.Timer);
            if (self.videoPlayer != null)
            {
                self.videoPlayer.Stop();
            }
            self.videoPath = null;
        }

        public static void Update(this DlgTutorials self)
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

        public static async ETTask DoNext(this DlgTutorials self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

            foreach (string videoPath in self.cacheVideoPath)
            {
                ResComponent.Instance.UnloadAsset(videoPath);
            }
            self.cacheVideoPath.Clear();

            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgTutorials>();
            GL.Clear(false, true, Color.black);
            await ETTask.CompletedTask;
        }

        public static async ETTask ClickVideo(this DlgTutorials self)
        {
            await self.ClickPauseButton();
            await ETTask.CompletedTask;
        }

        public static void OnSider(this DlgTutorials self, float scale)
        {
            self.dragTime = TimeHelper.ClientNow() + 500;
            double process = Math.Round(Convert.ToDouble(scale) * Convert.ToDouble(self.videoPlayer.frameCount), 0);
            self.videoPlayer.frame = (long)process;
        }

        //刷新回调
        public static async ETTask AddItemRefreshCallBack(this DlgTutorials self, Transform transform, int index)
        {
            Scroll_Item_Tutorials videoSelect = self.videoSelectDic[index].BindTrans(transform);
            videoSelect.Init(index, self.SetTutorialInfo );
            string title = self.tutorialCfgList[index].Name;
            self.videoSelectDic[index].ELabel_VideoSelectTextMeshProUGUI.text = title;
            await ETTask.CompletedTask;
        }

        //初始化 播放组件和视频个数
        public static void Init(this DlgTutorials self)
        {
            self.videoPlayer = self.View.E_videoVideoPlayer;
            self.videoPlayer.source = VideoSource.VideoClip;
            self.videoPlayer.audioOutputMode = VideoAudioOutputMode.Direct;
            self.videoPlayer.loopPointReached += (VideoPlayer source) =>
            {
                if (self.IsDisposed)
                {
                    return;
                }
                string tutorialCfgId = self.tutorialCfgList[self.videoIndex].Id;
                EventSystem.Instance.Publish(self.DomainScene(), new ClientEventType.NoticeEventLogging()
                {
                    eventName = "VideoFinished",
                    properties = new()
                    {
                        {"video_id", tutorialCfgId},
                    }
                });
            };

            //获取RawImage的宽高
            self.rawImageHeight = self.View.E_VideoShowRawImage.rectTransform.rect.height;
            self.rawImageWidth = self.View.E_VideoShowRawImage.rectTransform.rect.width;
        }

        //设置视频数量
        public static void GetVideoList(this DlgTutorials self)
        {
           List<TutorialCfg> tutorialList = TutorialCfgCategory.Instance.DataList;
            List<TutorialCfg> tutorialCfgs = new List<TutorialCfg>();
            foreach (TutorialCfg t in tutorialList)
            {
                if(t.IsShow)
                {
                    tutorialCfgs.Add(t);
                }
            }
            self.tutorialCfgList = tutorialCfgs;
        }

        public static void SetTutorialInfo(this DlgTutorials self, int index)
        {
            //是否播放背景音乐
            if (self.tutorialCfgList[index].IsSound)
            {
                UIAudioManagerHelper.StopMusic(self.DomainScene());
            }
            else
            {
                UIAudioManagerHelper.ResetMusicStatus(self.DomainScene());
            }
            string videoPath = self.tutorialCfgList[index].ResVideo_Ref.ResName;
            string videoDescibe = self.tutorialCfgList[index].Desc;

            self.View.E_infoTextMeshProUGUI.text = videoDescibe;
            if(self.videoIndex==index)
            {
                self.videoSelectDic[index].SetItemStatus(true);
            }
            else
            {
                self.videoSelectDic[index].SetItemStatus(true);
                self.videoSelectDic[self.videoIndex].SetItemStatus(false);
                self.videoIndex = index;
            }
            self.PlayVideo(videoPath).Coroutine();
        }

        //播放视频
        public static async ETTask PlayVideo(this DlgTutorials self, string videoPath)
        {
            string tutorialCfgId = self.tutorialCfgList[self.videoIndex].Id;
            EventSystem.Instance.Publish(self.DomainScene(), new ClientEventType.NoticeEventLogging()
            {
                eventName = "VideoPlayed",
                properties = new()
                {
                    {"video_id", tutorialCfgId},
                }
            });

            self.View.E_VideoShowRawImage.enabled = false;
            if (self.videoPlayer.isPlaying)
            {
                self.videoPlayer.Stop();
            }

            if(string.IsNullOrEmpty(videoPath))
            {
                return;
            }
            VideoClip videoClip = await ResComponent.Instance.LoadAssetAsync<VideoClip>(videoPath);
            self.cacheVideoPath.Add(videoPath);

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
            self.videoPlayer.isLooping = true;
            self.videoPath = videoPath;
            self.SetUIPlayStatus(true);
        }

        //打开面板后默认播放
        public static async ETTask PlayDefault(this DlgTutorials self)
        {
            //是否播放背景音乐
            if (self.tutorialCfgList[self.videoDefalutIndex].IsSound == true)
            {
                UIAudioManagerHelper.StopMusic(self.DomainScene());
            }
            else
            {
                UIAudioManagerHelper.ResetMusicStatus(self.DomainScene());
            }
            self.View.E_infoTextMeshProUGUI.text = self.tutorialCfgList[self.videoDefalutIndex].Desc;
            //设置按钮选中状态
            self.videoIndex = self.videoDefalutIndex;
            self.videoSelectDic[self.videoDefalutIndex].SetItemStatus(true);
            await self.PlayVideo(self.tutorialCfgList[self.videoDefalutIndex].ResVideo_Ref.ResName);

        }

        //暂停视频
        public static async ETTask ClickPauseButton(this DlgTutorials self)
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

        public static void SetUIPlayStatus(this DlgTutorials self, bool isPlaying)
        {
            if(isPlaying)
            {
                self.View.E_Image_PlayImage.SetVisible(false);
            }
            else
            {
                self.View.E_Image_PlayImage.SetVisible(true);
            }
        }

        //视频自适应
        public static void VideoAdaptive(this DlgTutorials self, VideoPlayer videoPlayer)
        {
            float videoWidth = videoPlayer.texture.width;
            float videoHeight = videoPlayer.texture.height;
            float ratio = videoWidth / videoHeight;
            float rawImageWidth = self.rawImageWidth;
            float rawImageHeight = self.rawImageHeight;
            float rawImageRatio = rawImageWidth / rawImageHeight;
            if(ratio>rawImageRatio)
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

    }
}
