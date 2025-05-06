using System.Collections;
using System.Collections.Generic;
using System;
using ET.AbilityConfig;
using SuperScrollView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace ET.Client
{
	[Invoke(TimerInvokeType.DlgSkillDetailsFrameTimer)]
	public class DlgSkillDetailsTimer : ATimer<DlgSkillDetails>
	{
		protected override void Run(DlgSkillDetails self)
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

	[FriendOf(typeof(DlgSkillDetails))]
	public static class DlgSkillDetailssSystem
	{
		public static void RegisterUIEvent(this DlgSkillDetails self)
		{
			self.View.EButton_BgButton.AddListener(self.OnClickBg);
			self.View.EButton_LeftButton.AddListener(self.SwitchToPreSkill);
			self.View.EButton_RightButton.AddListener(self.SwitchToNextSkill);


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

		public static async ETTask ShowWindow(this DlgSkillDetails self, ShowWindowData contextData = null)
		{
			self.dlgShowTime = TimeHelper.ClientNow();

			ShowData_DlgSkillDetails _ShowData_DlgSkillDetails = contextData as ShowData_DlgSkillDetails;
			self.Init(_ShowData_DlgSkillDetails.skillCfgId, _ShowData_DlgSkillDetails.isShowStatus, _ShowData_DlgSkillDetails.isLock);
		}

		public static void HideWindow(this DlgSkillDetails self)
		{
			UIAudioManagerHelper.ResetMusicStatus(self.DomainScene());
			TimerComponent.Instance?.Remove(ref self.Timer);

			if (self.videoPlayer != null)
			{
				self.videoPlayer.Stop();
			}
			if(string.IsNullOrEmpty(self.videoPath) == false)
			{
				ResComponent.Instance.UnloadAsset(self.videoPath);
			}
			GL.Clear(false, true, Color.black);
		}

		public static bool ChkCanClickBg(this DlgSkillDetails self)
		{
			if (self.dlgShowTime < TimeHelper.ClientNow() - (long)(1000 * 0.5f))
			{
				return true;
			}
			return false;
		}

		public static void Init(this DlgSkillDetails self, string skillCfgId, bool isShowStatus, bool isLock)
		{
			Log.Debug($"---Init towerCfgId[{skillCfgId}]");
			if (ET.ItemHelper.ChkIsSkill(skillCfgId) == false)
			{
				return;
			}
			self.isShowStatus = isShowStatus;
			self.isLock = isLock;
			self.baseSkillCfgId = skillCfgId;
			self.curSkillCfgId = skillCfgId;

			self.curSkillIndex = ET.ItemHelper.GetSkillItemLevel(self.baseSkillCfgId);

			self.ShowDetails().Coroutine();
		}

		public static async ETTask Refresh(this DlgSkillDetails self)
		{
			self.ShowDetails().Coroutine();
		}

		public static async ETTask ShowDetails(this DlgSkillDetails self)
		{
			self.ShowPreNextButton();
			self.ShowLockInfo();
			self.ShowDebugButton();
			self.ShowUpgradeInfo();

			string skillCfgId = self.curSkillCfgId;
			PlayerSkillCfg playerSkillCfg = PlayerSkillCfgCategory.Instance.Get(skillCfgId);

			self.View.ELabel_NameTextMeshProUGUI.text = ET.ItemHelper.GetItemName(skillCfgId);
			self.View.ELabel_DescriptionTextMeshProUGUI.text = ET.ItemHelper.GetItemDesc(skillCfgId);

			int qualityType = (int)ET.ItemHelper.GetItemQualityType(skillCfgId);
			self.View.EImage_BoxLowImage.SetVisible(qualityType == 0);
			self.View.EImage_BoxMiddleImage.SetVisible(qualityType == 1);
			self.View.EImage_BoxHighImage.SetVisible(qualityType == 2);

			self.ShowAttribute(playerSkillCfg.PropertyType, playerSkillCfg.Level);

			self.ShowVideo(playerSkillCfg.TutorialCfgId).Coroutine();
		}

		public static void ShowAttribute(this DlgSkillDetails self, string propertyType, int level)
		{
			var attributeList = ET.ItemHelper.GetAttributeProperty(propertyType, level);
			self.View.ENode_Attribute1Image.SetVisible(attributeList.Count >= 1);
			self.View.ENode_Attribute2Image.SetVisible(attributeList.Count >= 2);
			self.View.ENode_Attribute3Image.SetVisible(attributeList.Count >= 3);
			self.View.ENode_AttributeLine2Image.SetVisible(attributeList.Count >= 2);
			self.View.ENode_AttributeLine3Image.SetVisible(attributeList.Count >= 3);
			if (attributeList.Count >= 1)
			{
				self.View.Elabel_Attribute1TextMeshProUGUI.text = attributeList[0].title;
				self.View.Elabel_AttributeValue1TextMeshProUGUI.text = attributeList[0].content;
			}
			if (attributeList.Count >= 2)
			{
				self.View.Elabel_Attribute2TextMeshProUGUI.text = attributeList[1].title;
				self.View.Elabel_AttributeValue2TextMeshProUGUI.text = attributeList[1].content;
			}
			if (attributeList.Count >= 3)
			{
				self.View.Elabel_Attribute3TextMeshProUGUI.text = attributeList[2].title;
				self.View.Elabel_AttributeValue3TextMeshProUGUI.text = attributeList[2].content;
			}
		}

		public static void SwitchToPreSkill(this DlgSkillDetails self)
		{
			string preSkillCfgId = PlayerSkillCfgCategory.Instance.GetPreSkillCfg(self.curSkillCfgId).Id;
			self.curSkillCfgId = preSkillCfgId;
			self.curSkillIndex --;

			self.ShowDetails().Coroutine();
		}

		public static void SwitchToNextSkill(this DlgSkillDetails self)
		{
			string nextSkillCfgId = PlayerSkillCfgCategory.Instance.GetNextSkillCfg(self.curSkillCfgId).Id;
			self.curSkillCfgId = nextSkillCfgId;
			self.curSkillIndex ++;

			self.ShowDetails().Coroutine();
		}

		public static void ShowPreNextButton(this DlgSkillDetails self)
		{
			self.View.EButton_LeftButton.SetVisible(false);
			self.View.EButton_RightButton.SetVisible(false);
			PlayerSkillCfg prePlayerSkillCfg = PlayerSkillCfgCategory.Instance.GetPreSkillCfg(self.curSkillCfgId);
			if (prePlayerSkillCfg != null)
			{
				self.View.EButton_LeftButton.SetVisible(true);
			}

			PlayerSkillCfg nextPlayerSkillCfg = PlayerSkillCfgCategory.Instance.GetNextSkillCfg(self.curSkillCfgId);
			if (nextPlayerSkillCfg != null)
			{
				self.View.EButton_RightButton.SetVisible(true);
			}
		}

		public static void ShowLockInfo(this DlgSkillDetails self)
		{
			if (self.isShowStatus == false)
			{
				self.View.EButton_UnlockButton.SetVisible(false);
				return;
			}
			if (self.isLock)
			{
				self.View.EButton_UnlockButton.SetVisible(true);

				string showTip = ET.ItemHelper.GetItemUnLockTip(self.baseSkillCfgId, true);
				self.View.ELable_UnLockShowTipTextMeshProUGUI.text = showTip;
				self.View.EButton_UnlockButton.AddListener(async () =>
				{
					bool bRet = await ET.Client.UIManagerHelper.ClickItemWhenLock(self.DomainScene(), self.baseSkillCfgId);
					if (bRet)
					{
						self.isLock = false;
						await self.Refresh();
					}
				});
			}
			else
			{
				self.View.EButton_UnlockButton.SetVisible(false);
			}
		}

		public static void ShowUpgradeInfo(this DlgSkillDetails self)
		{
			if (ET.Client.UIManagerHelper.ChkIsDebug())
			{
				self.View.EButton_UpgradeButton.SetVisible(true);
				self.View.EButton_UpgradeButton.AddListener(async () =>
				{
					ET.Client.ItemHelper.UpgradeItem(self.DomainScene(), self.curSkillCfgId).Coroutine();
				});
			}
			else
			{
				self.View.EButton_UpgradeButton.SetVisible(false);
			}
		}

		public static void ShowDebugButton(this DlgSkillDetails self)
		{
			if (self.isLock)
			{
				if (ET.Client.UIManagerHelper.ChkIsDebug())
				{
					self.View.E_DebugButton.SetVisible(true);
					self.View.E_DebugButton.AddListenerAsync(self.AddSkillWhenDebug);
				}
				else
				{
					self.View.E_DebugButton.SetVisible(false);
				}
			}
			else
			{
				self.View.E_DebugButton.SetVisible(false);
			}
		}

		public static void OnClickBg(this DlgSkillDetails self)
		{
			if (self.ChkCanClickBg() == false)
			{
				return;
			}
			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgSkillDetails>();
		}

		public static async ETTask ShowVideo(this DlgSkillDetails self, string tutorialCfgId)
		{
			self.View.E_VideoShowRawImage.enabled = false;
			self.tutorialCfgId = "";

			if (tutorialCfgId.IsNullOrEmpty())
			{
				return;
			}
			await self.PlayVideo(tutorialCfgId);

			TimerComponent.Instance?.Remove(ref self.Timer);
			self.Timer = TimerComponent.Instance.NewFrameTimer(TimerInvokeType.DlgSkillDetailsFrameTimer, self);
		}

		//播放视频
		public static async ETTask PlayVideo(this DlgSkillDetails self, string tutorialCfgId)
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

            string videoPath = tutorialCfg.ResVideo_Ref.ResName;
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


		public static async ETTask OnClickPause(this DlgSkillDetails self)
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

		public static void OnClickSlider(this DlgSkillDetails self, float scale)
		{
			self.dragTime = TimeHelper.ClientNow() + 500;
			double process = Math.Round(Convert.ToDouble(scale) * Convert.ToDouble(self.videoPlayer.frameCount), 0);
			self.videoPlayer.frame = (long)process;
        }

        public static void Update(this DlgSkillDetails self)
        {
	        if (self == null || self.IsDisposed || self.View == null || self.videoPlayer == null)
	        {
		        return;
	        }

	        if (self.tutorialCfgId.IsNullOrEmpty())
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
        public static void VideoAdaptive(this DlgSkillDetails self, VideoPlayer videoPlayer)
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

        public static void SetUIPlayStatus(this DlgSkillDetails self, bool isPlaying)
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

        public static async ETTask OnClickVideo(this DlgSkillDetails self)
        {
            await self.OnClickPause();
        }

        public static async ETTask AddSkillWhenDebug(this DlgSkillDetails self)
        {
	        if (GlobalConfig.Instance.dbType == DBType.NoDB)
	        {
		        PlayerBackPackComponent playerBackPackComponent = await PlayerCacheHelper.GetMyPlayerBackPack(self.DomainScene());
		        bool isHave = playerBackPackComponent.ChkItemExist(self.baseSkillCfgId);
		        if (isHave)
		        {
			        return;
		        }
		        playerBackPackComponent.AddItem(self.baseSkillCfgId, 1);

		        await ET.Client.PlayerCacheHelper.SaveMyPlayerModel(self.DomainScene(), PlayerModelType.BackPack, null);
		        await ET.Client.PlayerCacheHelper.GetUIRedDotType(self.DomainScene());

		        UIManagerHelper.ShowTip(self.DomainScene(), "success");

		        self.isLock = false;
		        await self.ShowDetails();
	        }
        }

	}
}
