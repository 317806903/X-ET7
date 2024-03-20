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

			self.View.ELoopScrollList_RewardLoopHorizontalScrollRect.prefabSource.prefabName = "Item_TowerBuy";
            self.View.ELoopScrollList_RewardLoopHorizontalScrollRect.prefabSource.poolSize = 3;
            self.View.ELoopScrollList_RewardLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
                    self.AddTowerBuyListener(transform, i));
			
			self.View.ELoopScrollList_propLoopHorizontalScrollRect.prefabSource.prefabName = "Item_Monsters";
            self.View.ELoopScrollList_propLoopHorizontalScrollRect.prefabSource.poolSize = 3;
            self.View.ELoopScrollList_propLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
                    self.AddMonsterListener(transform, i));
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
			int count = TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallenges(true).Count;
			self.selectIndex = playerBaseInfoComponent.ChallengeClearLevel;
			if(self.selectIndex == count){
				self.selectIndex = self.selectIndex - 1;
				self.RefreshLevelUI(true);
			}else{
				self.RefreshLevelUI(false);
			}
            self.AddUIScrollItems(ref self.ScrollItemChallengeList, count);
            self.View.ELoopScrollList_ChallengeLoopHorizontalScrollRect.SetVisible(true, count);

			self.View.E_SelectButton.gameObject.SetActive(true);
			self.View.E_UnlockedButton.gameObject.SetActive(false);
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

			challengeList.E_iconImage.SetVisible(self.CheckTowerReward(index + 1, clearLevel));
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
			self.RefreshLevelUI(clearLevel > level);
        }

		public static bool CheckTowerReward(this DlgChallengeMode self, int selectLevel, int clearLevel){
			TowerDefense_ChallengeLevelCfg challengeLevelCfg =
						TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallengeByIndex(true, selectLevel);
			List<string> rewardList;
			if(clearLevel >= selectLevel){
				rewardList = ET.DropItemRuleHelper.GetPreviewDropItems(challengeLevelCfg.RepeatClearDropItem);
			}else{
				rewardList = ET.DropItemRuleHelper.GetPreviewDropItems(challengeLevelCfg.FirstClearDropItem);
			}
			
			foreach(string itemId in rewardList){
				if(ItemHelper.ChkIsTower(itemId)){
					return true;
				}
			}
			return false;
		}

		public static void RefreshLevelUI(this DlgChallengeMode self, bool bClear){
			self.View.ELabel_LvTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_BattleEnd_ChallengeLevel", self.selectIndex+1);
			
			TowerDefense_ChallengeLevelCfg challengeLevelCfg =
						TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallengeByIndex(true, self.selectIndex+1);
			List<string> rewardList;
			// if(bClear){
			// 	rewardList = ET.DropItemRuleHelper.GetPreviewDropItems(challengeLevelCfg.RepeatClearDropItem);
			// }else{
				rewardList = ET.DropItemRuleHelper.GetPreviewDropItems(challengeLevelCfg.FirstClearDropItem);
			// }
			self.View.EG_RewardRectTransform.SetVisible(rewardList.Count != 0);
			self.View.E_line02Image.SetVisible(rewardList.Count != 0);
			self.AddUIScrollItems(ref self.ScrollItemReward, rewardList.Count);
            self.View.ELoopScrollList_RewardLoopHorizontalScrollRect.SetVisible(true, rewardList.Count);

			List<string> monsterList = challengeLevelCfg.MonsterList;
			self.AddUIScrollItems(ref self.ScrollItemMonster, monsterList.Count);
            self.View.ELoopScrollList_propLoopHorizontalScrollRect.SetVisible(true, monsterList.Count);
		}

		public static async void AddTowerBuyListener(this DlgChallengeMode self, Transform transform, int index)
        {
            transform.name = $"Item_TowerBuy_{index}";
            Scroll_Item_TowerBuy itemTowerBuy = self.ScrollItemReward[index].BindTrans(transform);
            itemTowerBuy.EImage_TowerBuyShowImage.SetVisible(true);
			PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());
			int clearLevel = playerBaseInfoComponent.ChallengeClearLevel;
			TowerDefense_ChallengeLevelCfg challengeLevelCfg =
						TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallengeByIndex(true, self.selectIndex+1);
			List<string> list;
			// if(clearLevel > challengeLevelCfg.Index){
			// 	list = ET.DropItemRuleHelper.GetPreviewDropItems(challengeLevelCfg.RepeatClearDropItem);
			// }else{
				list = ET.DropItemRuleHelper.GetPreviewDropItems(challengeLevelCfg.FirstClearDropItem);
			// }

            string towerCfgId = list[index];
            string itemCfgId = list[index];
            string towerName = ItemHelper.GetItemName(itemCfgId);
            TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(towerCfgId);

            string icon = ItemHelper.GetItemIcon(itemCfgId);
            if (string.IsNullOrEmpty(icon) == false)
            {
                Sprite sprite = ResComponent.Instance.LoadAsset<Sprite>(icon);
                itemTowerBuy.EButton_IconImage.sprite = sprite;
            }

			itemTowerBuy.EImage_PurchasedImage.SetVisible(false);
			itemTowerBuy.ELabel_ContentText.text = $"{towerCfg.BuyTowerCostGold}";
			itemTowerBuy.ELabel_ContentText.gameObject.SetActive(false);
			itemTowerBuy.EImage_BuyBGImage.gameObject.SetActive(true);
			itemTowerBuy.EButton_BuyButton.gameObject.SetActive(true);
			itemTowerBuy.EButton_BuyButton.gameObject.transform.Find("CoinIcon").gameObject.SetActive(false);
			itemTowerBuy.EButton_BuyButton.AddListener(()=>
				{
					self.ShowDetails(itemCfgId);
				});

            itemTowerBuy.EButton_nameTextMeshProUGUI.text = $"{towerName}";
            itemTowerBuy.SetLevel(itemCfgId);
            itemTowerBuy.SetLabels(itemCfgId);
            itemTowerBuy.SetQuality(itemCfgId);
			itemTowerBuy.SetCheckMark(clearLevel >= challengeLevelCfg.Index);
        }

		public static void ShowDetails(this DlgChallengeMode self, string itemCfgId)
		{
			UIComponent _UIComponent = UIManagerHelper.GetUIComponent(self.DomainScene());
			_UIComponent.ShowWindow<DlgDetails>();
			DlgDetails _DlgDetails = _UIComponent.GetDlgLogic<DlgDetails>(true);
			if (_DlgDetails != null)
			{
				_DlgDetails.SetCurItemCfgId(itemCfgId);
			}
		}

		public static void AddMonsterListener(this DlgChallengeMode self, Transform transform, int index)
        {
			transform.name = $"Item_Monster_{index}";
            Scroll_Item_Monsters itemMonster = self.ScrollItemMonster[index].BindTrans(transform);
			TowerDefense_ChallengeLevelCfg challengeLevelCfg =
						TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallengeByIndex(true, self.selectIndex+1);
			List<string> monsterList = challengeLevelCfg.MonsterList;

			string monsterCfgId = monsterList[index];
			string itemCfgId = monsterList[index];
			string icon = ItemHelper.GetItemIcon(itemCfgId);
            if (string.IsNullOrEmpty(icon) == false)
            {
                Sprite sprite = ResComponent.Instance.LoadAsset<Sprite>(icon);
                itemMonster.EImage_MonsterImage.sprite = sprite;
            }
			ET.EventTriggerListener.Get(itemMonster.EButton_SelectButton.gameObject).onPress.AddListener(async (go, eventData) =>
            {
				TowerDefense_MonsterCfg monsterCfg = TowerDefense_MonsterCfgCategory.Instance.Get(monsterCfgId);
				DlgDescTips_ShowWindowData _DlgDescTips_ShowWindowData = new()
				{
					Pos = transform.position + Vector3.up,
					Desc = LocalizeComponent.Instance.GetTextValue(monsterCfg.Desc),
				};
				await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgDescTips>(_DlgDescTips_ShowWindowData);
			});
			ET.EventTriggerListener.Get(itemMonster.EButton_SelectButton.gameObject).onExit.AddListener(async (go, eventData) =>
            {
				UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgDescTips>();
			});
		}
	}
}
