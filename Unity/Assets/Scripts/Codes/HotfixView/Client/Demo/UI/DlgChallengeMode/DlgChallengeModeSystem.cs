using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using ET.AbilityConfig;
using TMPro;

namespace ET.Client
{
	[FriendOf(typeof(DlgChallengeMode))]
	public static class DlgChallengeModeSystem
	{
		public static void RegisterUIEvent(this DlgChallengeMode self)
		{
			self.View.E_SelectButton.AddListenerAsync(self.Select);
			self.View.E_UnlockedButton.AddListenerAsync(self.Unlocked);
			self.View.E_QuitBattleButton.AddListenerAsync(self.Back);

			self.View.ELoopScrollList_ChallengeLoopHorizontalScrollRect.prefabSource.prefabName = "Item_ChallengeList";
            self.View.ELoopScrollList_ChallengeLoopHorizontalScrollRect.prefabSource.poolSize = 7;
            self.View.ELoopScrollList_ChallengeLoopHorizontalScrollRect.AddItemRefreshListener(((transform, i) =>
                    self.AddListItemRefreshListener(transform, i).Coroutine()));
		}

		public static void ShowWindow(this DlgChallengeMode self, ShowWindowData contextData = null)
		{
			self.ShowBg().Coroutine();
			self.ShowListScrollItem().Coroutine();
			self.ScrollToCurrentLevel().Coroutine();
			self.SetPlayerEnergy().Coroutine();
		}

		public static async ETTask ShowBg(this DlgChallengeMode self)
        {
            bool isARCameraEnable = ET.Client.ARSessionHelper.ChkARCameraEnable(self.DomainScene());
            isARCameraEnable = false;
            if (isARCameraEnable)
            {
                self.View.EG_bgARRectTransform.SetVisible(true);
                self.View.EG_bgRectTransform.SetVisible(false);
            }
            else
            {
                self.View.EG_bgARRectTransform.SetVisible(false);
                self.View.EG_bgRectTransform.SetVisible(true);
            }
        }

		public static async ETTask SetPlayerEnergy(this DlgChallengeMode self)
        {
			self.View.E_SelectButton.transform.Find("number").GetComponent<TMP_Text>().text = GlobalSettingCfgCategory.Instance.ARPVECfgTakePhsicalStrength.ToString();
			self.View.E_UnlockedButton.transform.Find("number").GetComponent<TMP_Text>().text = GlobalSettingCfgCategory.Instance.ARPVECfgTakePhsicalStrength.ToString();

			PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());
			self.View.E_EnergyImage.transform.Find("Text (TMP)").GetComponent<TMP_Text>().text = playerBaseInfoComponent.physicalStrength+"/"+GlobalSettingCfgCategory.Instance.UpperLimitOfPhysicalStrength;
        }

		public static async ETTask Back(this DlgChallengeMode self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(),SoundEffectType.Back);
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgChallengeMode>();
            await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgGameModeAR>();
        }

		public static async ETTask Select(this DlgChallengeMode self)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

			if (await ET.Client.UIManagerHelper.ChkAndShowtip(self.DomainScene(), GlobalSettingCfgCategory.Instance.ARPVECfgTakePhsicalStrength) == false)
            {
                return;
            }

			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgChallengeMode>();

			string battleCfgId = ET.GamePlayHelper.GetBattleCfgId(RoomType.AR, SubRoomType.ARPVE, self.selectIndex + 1);
			DlgARHall_ShowWindowData _DlgARHall_ShowWindowData = new()
            {
                playerStatus = PlayerStatus.Hall,
                RoomType = RoomType.AR,
                SubRoomType = SubRoomType.ARPVE,
                arRoomId = 0,
                battleCfgId = battleCfgId,
            };
			await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData);
		}

		public static async ETTask Unlocked(this DlgChallengeMode self)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);
		}

		public static async ETTask ShowListScrollItem(this DlgChallengeMode self)
        {
            PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());
			self.selectIndex = playerBaseInfoComponent.ChallengeClearLevel;
			int count = TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallenges(true).Count;
            self.AddUIScrollItems(ref self.ScrollItemChallengeList, count);
            self.View.ELoopScrollList_ChallengeLoopHorizontalScrollRect.SetVisible(true, count);
        }

		public static async ETTask ScrollToCurrentLevel(this DlgChallengeMode self)
		{
			PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());
			int index = Math.Max(0, self.selectIndex - 2);
			self.View.ELoopScrollList_ChallengeLoopHorizontalScrollRect.SrollToCellWithinTime(index, 0.5f);
		}

		public static async ETTask AddListItemRefreshListener(this DlgChallengeMode self, Transform transform, int index)
        {
			Scroll_Item_ChallengeList challengeList = self.ScrollItemChallengeList[index].BindTrans(transform);
			challengeList.ELabel_NormalTextMeshProUGUI.text = (index + 1).ToString();
			challengeList.ELabel_UnlockedTextMeshProUGUI.text = (index + 1).ToString();
			// if(index % 2 == 1){
			// 	challengeList.EButton_dotButton.transform.localPosition = new Vector3(-43, -84, 0);
			// 	challengeList.E_Normal_lineImage.transform.localScale = new Vector3(-1, 1, 1);
			// 	challengeList.E_Unlocked_lineImage.transform.localScale = new Vector3(1, 1, 1);
			// }
			// else{
			// 	challengeList.EButton_dotButton.transform.localPosition = new Vector3(-43, 118, 0);
			// 	challengeList.E_Normal_lineImage.transform.localScale = new Vector3(1, 1, 1);
			// 	challengeList.E_Unlocked_lineImage.transform.localScale = new Vector3(-1, 1, 1);
			// }
			PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());
			int clearLevel = playerBaseInfoComponent.ChallengeClearLevel;

			challengeList.E_SelectedImage.gameObject.SetActive(index == self.selectIndex);
			challengeList.E_NormalImage.gameObject.SetActive(index <= clearLevel);
			challengeList.E_UnlockedImage.gameObject.SetActive(index > clearLevel);
			challengeList.E_Normal_lineImage.gameObject.SetActive(index < clearLevel);
			challengeList.EG_Unlocked_lineRectTransform.gameObject.SetActive(index >= clearLevel);

			if(index == TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallenges(true).Count - 1){
				challengeList.E_Normal_lineImage.gameObject.SetActive(false);
				challengeList.EG_Unlocked_lineRectTransform.gameObject.SetActive(false);
			}
			challengeList.EButton_dotButton.AddListener(() => { self.SelectLevel(index).Coroutine(); });
		}

		public static async ETTask SelectLevel(this DlgChallengeMode self, int level)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);
            self.selectIndex = level;
			self.View.ELoopScrollList_ChallengeLoopHorizontalScrollRect.RefreshCells();
			PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());
			int clearLevel = playerBaseInfoComponent.ChallengeClearLevel;
			self.View.E_SelectButton.gameObject.SetActive(clearLevel >= level);
			self.View.E_UnlockedButton.gameObject.SetActive(clearLevel < level);
        }
	}
}
