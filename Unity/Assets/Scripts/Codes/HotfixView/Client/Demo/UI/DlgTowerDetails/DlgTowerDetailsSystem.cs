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
	[Invoke(TimerInvokeType.DlgTowerDetailsFrameTimer)]
	public class DlgTowerDetailsTimer : ATimer<DlgTowerDetails>
	{
		protected override void Run(DlgTowerDetails self)
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

	[FriendOf(typeof(DlgTowerDetails))]
	public static class DlgTowerDetailsSystem
	{
		public static void RegisterUIEvent(this DlgTowerDetails self)
		{
			self.View.EButton_BgButton.AddListener(self.OnClickBg);
			self.View.EButton_LeftButton.AddListener(self.SwitchToPreItem);
			self.View.EButton_RightButton.AddListener(self.SwitchToNextItem);


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

				EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
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

		public static async ETTask ShowWindow(this DlgTowerDetails self, ShowWindowData contextData = null)
		{
			self.dlgShowTime = TimeHelper.ClientNow();

			ShowData_DlgTowerDetails _ShowData_DlgTowerDetails = contextData as ShowData_DlgTowerDetails;
			self.Init(_ShowData_DlgTowerDetails.towerCfgId, _ShowData_DlgTowerDetails.isShowStatus, _ShowData_DlgTowerDetails.isLock);
		}

		public static void HideWindow(this DlgTowerDetails self)
		{
			UIAudioManagerHelper.ResetMusicStatus(self.DomainScene());
			TimerComponent.Instance?.Remove(ref self.Timer);

			if(string.IsNullOrEmpty(self.videoPath) == false)
			{
				ResComponent.Instance.UnloadAsset(self.videoPath);
			}
			GL.Clear(false, true, Color.black);
		}

		public static bool ChkCanClickBg(this DlgTowerDetails self)
		{
			if (self.dlgShowTime < TimeHelper.ClientNow() - (long)(1000 * 0.5f))
			{
				return true;
			}
			return false;
		}

		public static void Init(this DlgTowerDetails self, string towerCfgId, bool isShowStatus, bool isLock)
		{
			Log.Debug($"---Init towerCfgId[{towerCfgId}]");
			if (ET.ItemHelper.ChkIsTower(towerCfgId) == false)
			{
				return;
			}
			self.isShowStatus = isShowStatus;
			self.isLock = isLock;
			self.baseTowerCfgId = towerCfgId;
			self.curTowerCfgId = towerCfgId;

			self.curTowerIndex = ET.ItemHelper.GetTowerItemLevel(self.baseTowerCfgId);

			self.ShowDetails().Coroutine();
		}

		public static async ETTask Refresh(this DlgTowerDetails self)
		{
			self.ShowDetails().Coroutine();
		}

		public static async ETTask ShowDetails(this DlgTowerDetails self)
		{
			self.ShowPreNextButton();
			self.ShowToggleChoose();
			self.ShowLockInfo();
			self.ShowDebugButton();

			string itemCfgId = self.curTowerCfgId;

			self.View.ELabel_NameTextMeshProUGUI.text = ET.ItemHelper.GetItemName(itemCfgId);
			self.View.ELabel_DescriptionTextMeshProUGUI.text = ET.ItemHelper.GetItemDesc(itemCfgId);


			List<string> labels = ET.ItemHelper.GetTowerItemLabels(itemCfgId);
			int labelCount = labels.Count;
			self.View.EImage_Label1Image.gameObject.SetActive(labelCount >= 1);
			self.View.EImage_Label2Image.gameObject.SetActive(labelCount >= 2);
			if (labelCount >= 1)
			{
				self.View.ELabel_Label1TextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValueByExcel(labels[0]);
			}

			if (labelCount >= 2)
			{
				self.View.ELabel_Label2TextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValueByExcel(labels[1]);
			}

			LayoutRebuilder.ForceRebuildLayoutImmediate(self.View.EImage_Label1Image.transform.parent.GetComponent<RectTransform>());

			int qualityType = (int)ET.ItemHelper.GetItemQualityType(itemCfgId);
			self.View.EImage_BoxLowImage.SetVisible(qualityType == 0);
			self.View.EImage_BoxMiddleImage.SetVisible(qualityType == 1);
			self.View.EImage_BoxHighImage.SetVisible(qualityType == 2);

			int count = (int)ET.ItemHelper.GetTowerItemQualityRank(itemCfgId);
			self.View.E_IconStar1Image.SetVisible(count >= 1);
			self.View.E_IconStar2Image.SetVisible(count >= 2);
			self.View.E_IconStar3Image.SetVisible(count >= 3);

			TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(self.curTowerCfgId);
			string propertyType = UnitCfgCategory.Instance.Get(towerCfg.UnitId[0]).PropertyType;
			self.ShowAttribute(propertyType, towerCfg.Level[0]);

			string tutorialCfgId = towerCfg.TutorialCfgId;
			self.ShowVideo(tutorialCfgId).Coroutine();
		}

		public static void ShowAttribute(this DlgTowerDetails self, string propertyType, int level)
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

		public static void SwitchToPreItem(this DlgTowerDetails self)
		{

			string preItemCfgId = ET.ItemHelper.GetTowerItemPreTowerConfigId(self.curTowerCfgId);
			self.curTowerCfgId = preItemCfgId;
			self.curTowerIndex --;

			self.ShowDetails().Coroutine();
		}

		public static void SwitchToNextItem(this DlgTowerDetails self)
		{
			string nextItemCfgId = ET.ItemHelper.GetTowerItemNextTowerConfigId(self.curTowerCfgId);
			self.curTowerCfgId = nextItemCfgId;
			self.curTowerIndex ++;

			self.ShowDetails().Coroutine();
		}

		public static void ShowPreNextButton(this DlgTowerDetails self)
		{
			self.View.EButton_LeftButton.SetVisible(false);
			self.View.EButton_RightButton.SetVisible(false);
			string preItemCfgId = ET.ItemHelper.GetTowerItemPreTowerConfigId(self.curTowerCfgId);
			if (string.IsNullOrEmpty(preItemCfgId) == false)
			{
				self.View.EButton_LeftButton.SetVisible(true);
			}

			string nextItemCfgId = ET.ItemHelper.GetTowerItemNextTowerConfigId(self.curTowerCfgId);
			if (string.IsNullOrEmpty(nextItemCfgId) == false)
			{
				self.View.EButton_RightButton.SetVisible(true);
			}
		}

		public static void ShowToggleChoose(this DlgTowerDetails self)
		{
			self.View.EToggle_Lv1Toggle.SetVisible(true);
			self.View.EToggle_Lv2Toggle.SetVisible(true);
			self.View.EToggle_Lv3Toggle.SetVisible(true);

			self.View.EToggle_Lv1Toggle.SetIsOnWithoutNotify(self.curTowerIndex == 1);
			self.View.EToggle_Lv2Toggle.SetIsOnWithoutNotify(self.curTowerIndex == 2);
			self.View.EToggle_Lv3Toggle.SetIsOnWithoutNotify(self.curTowerIndex == 3);

			self.View.EToggle_Lv1Toggle.AddListener((isOn) =>
			{
				if (isOn == false)
				{
					self.View.EToggle_Lv1Toggle.SetIsOnWithoutNotify(true);
					return;
				}
				self.ClickToggleChoose(1);
			});
			self.View.EToggle_Lv2Toggle.AddListener((isOn) =>
			{
				if (isOn == false)
				{
					self.View.EToggle_Lv2Toggle.SetIsOnWithoutNotify(true);
					return;
				}
				self.ClickToggleChoose(2);
			});
			self.View.EToggle_Lv3Toggle.AddListener((isOn) =>
			{
				if (isOn == false)
				{
					self.View.EToggle_Lv3Toggle.SetIsOnWithoutNotify(true);
					return;
				}
				self.ClickToggleChoose(3);
			});
		}

		public static void ClickToggleChoose(this DlgTowerDetails self, int curTowerIndex)
		{
			if (self.curTowerIndex == curTowerIndex)
			{
				return;
			}
			if (self.curTowerIndex > curTowerIndex)
			{
				string preItemCfgId = ET.ItemHelper.GetTowerItemPreTowerConfigId(self.curTowerCfgId, self.curTowerIndex - curTowerIndex);
				self.curTowerCfgId = preItemCfgId;
				self.curTowerIndex = curTowerIndex;
			}
			else
			{
				string nextItemCfgId = ET.ItemHelper.GetTowerItemNextTowerConfigId(self.curTowerCfgId, curTowerIndex - self.curTowerIndex);
				self.curTowerCfgId = nextItemCfgId;
				self.curTowerIndex = curTowerIndex;
			}

			self.ShowDetails().Coroutine();
		}

		public static void ShowLockInfo(this DlgTowerDetails self)
		{
			if (self.isShowStatus == false)
			{
				self.View.EButton_UnlockButton.SetVisible(false);
				return;
			}
			if (self.isLock)
			{
				self.View.EButton_UnlockButton.SetVisible(true);

				string showTip = ET.ItemHelper.GetItemUnLockTip(self.baseTowerCfgId, true);
				self.View.ELable_UnLockShowTipTextMeshProUGUI.text = showTip;
				self.View.EButton_UnlockButton.AddListener(async () =>
				{
					bool bRet = await ET.Client.UIManagerHelper.ClickItemWhenLock(self.DomainScene(), self.baseTowerCfgId);
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

		public static void ShowDebugButton(this DlgTowerDetails self)
		{
			if (self.isLock)
			{
				if (Application.isMobilePlatform == false)
				{
					self.View.E_DebugButton.SetVisible(true);
					self.View.E_DebugButton.AddListenerAsync(self.AddCardWhenDebug);
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

		public static void OnClickBg(this DlgTowerDetails self)
		{
			if (self.ChkCanClickBg() == false)
			{
				return;
			}
			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgTowerDetails>();
		}

		public static async ETTask ShowVideo(this DlgTowerDetails self, string tutorialCfgId)
		{
			self.View.E_VideoShowRawImage.enabled = false;
			self.tutorialCfgId = "";

			if (tutorialCfgId.IsNullOrEmpty())
			{
				return;
			}
			await self.PlayVideo(tutorialCfgId);

			TimerComponent.Instance?.Remove(ref self.Timer);
			self.Timer = TimerComponent.Instance.NewFrameTimer(TimerInvokeType.DlgTowerDetailsFrameTimer, self);
		}

		//播放视频
		public static async ETTask PlayVideo(this DlgTowerDetails self, string tutorialCfgId)
		{
			TutorialCfg tutorialCfg = TutorialCfgCategory.Instance.Get(tutorialCfgId);
			if(tutorialCfg.IsSound)
                UIAudioManagerHelper.StopMusic(self.DomainScene());
			else
                UIAudioManagerHelper.ResetMusicStatus(self.DomainScene());
            self.tutorialCfgId = tutorialCfg.Id;
			EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
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


		public static async ETTask OnClickPause(this DlgTowerDetails self)
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

		public static void OnClickSlider(this DlgTowerDetails self, float scale)
		{
			self.dragTime = TimeHelper.ClientNow() + 500;
			double process = Math.Round(Convert.ToDouble(scale) * Convert.ToDouble(self.videoPlayer.frameCount), 0);
			self.videoPlayer.frame = (long)process;
        }

        public static void Update(this DlgTowerDetails self)
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
        public static void VideoAdaptive(this DlgTowerDetails self, VideoPlayer videoPlayer)
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

        public static void SetUIPlayStatus(this DlgTowerDetails self, bool isPlaying)
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

        public static async ETTask OnClickVideo(this DlgTowerDetails self)
        {
            await self.OnClickPause();
        }

        public static async ETTask AddCardWhenDebug(this DlgTowerDetails self)
        {
	        if (GlobalConfig.Instance.dbType == DBType.NoDB)
	        {
		        PlayerBackPackComponent playerBackPackComponent = await PlayerCacheHelper.GetMyPlayerBackPack(self.DomainScene());
		        bool isHave = playerBackPackComponent._ChkItemExist(self.baseTowerCfgId);
		        if (isHave)
		        {
			        return;
		        }
		        playerBackPackComponent.AddItem(self.baseTowerCfgId, 1);

		        await ET.Client.PlayerCacheHelper.SaveMyPlayerModel(self.DomainScene(), PlayerModelType.BackPack, null);
		        await ET.Client.PlayerCacheHelper.GetUIRedDotType(self.DomainScene());

		        UIManagerHelper.ShowTip(self.DomainScene(), "success");

		        self.isLock = false;
		        await self.ShowDetails();
	        }
        }

	}
}
