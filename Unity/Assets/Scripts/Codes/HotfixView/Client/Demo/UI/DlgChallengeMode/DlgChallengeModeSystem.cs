using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using ET.AbilityConfig;
using TMPro;

namespace ET.Client
{
    [FriendOf(typeof (DlgChallengeMode))]
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
#if UNITY_EDITOR
            self.isAR = false;
#else
			self.isAR = true;
#endif
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

        public static async ETTask RefreshWhenBaseInfoChg(this DlgChallengeMode self)
        {
            await self.SetPlayerEnergy();
        }

        public static async ETTask SetPlayerEnergy(this DlgChallengeMode self)
        {
            self.View.E_SelectButton.transform.Find("number").ShowPhysicalCostText(self.DomainScene(), GlobalSettingCfgCategory.Instance.ARPVECfgTakePhsicalStrength).Coroutine();
            self.View.E_UnlockedButton.transform.Find("number").ShowPhysicalCostText(self.DomainScene(), GlobalSettingCfgCategory.Instance.ARPVECfgTakePhsicalStrength).Coroutine();
        }

        public static async ETTask Back(this DlgChallengeMode self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Back);
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgChallengeMode>();
            await UIManagerHelper.EnterGameModeUI(self.DomainScene());
        }

        public static async ETTask Select(this DlgChallengeMode self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

            if (await ET.Client.UIManagerHelper.ChkPhsicalAndShowtip(self.DomainScene(),
                    GlobalSettingCfgCategory.Instance.ARPVECfgTakePhsicalStrength) == false)
            {
                return;
            }

            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgChallengeMode>();

            RoomType roomType = RoomType.Normal;
            SubRoomType subRoomType = SubRoomType.NormalPVE;
            string battleCfgId;
            if (self.isAR)
            {
                roomType = RoomType.AR;
                subRoomType = SubRoomType.ARPVE;
                battleCfgId = ET.GamePlayHelper.GetBattleCfgId(roomType, subRoomType, self.selectIndex + 1);

                DlgARHall_ShowWindowData _DlgARHall_ShowWindowData = new()
                {
                    ARHallType = ARHallType.CreateRoomWithOutARSceneId,
                    RoomType = roomType,
                    SubRoomType = subRoomType,
                    roomId = 0,
                    battleCfgId = battleCfgId,
                };
                await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData);
            }
            else
            {
                roomType = RoomType.Normal;
                subRoomType = SubRoomType.NormalPVE;
                battleCfgId = ET.GamePlayHelper.GetBattleCfgId(roomType, subRoomType, self.selectIndex + 1);
                (bool result, long roomId) = await RoomHelper.CreateRoomAsync(self.ClientScene(), battleCfgId, roomType, subRoomType);
                if (result)
                {
                    //UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgChallengeMode>();
                    await ET.Client.UIManagerHelper.EnterRoomUI(self.DomainScene());
                }
            }
        }

        public static async ETTask Unlocked(this DlgChallengeMode self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

            string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_ChallengeMode_Unlocked");
            ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
        }

        public static async ETTask ShowListScrollItem(this DlgChallengeMode self)
        {
            PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());
            int count = TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallenges(self.isAR).Count;
            self.selectIndex = playerBaseInfoComponent.ChallengeClearLevel;
            if (self.selectIndex == count)
            {
                self.selectIndex = self.selectIndex - 1;
                self.RefreshLevelUI(true);
            }
            else
            {
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

            if (index == TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallenges(self.isAR).Count - 1)
            {
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

        public static bool CheckTowerReward(this DlgChallengeMode self, int selectLevel, int clearLevel)
        {
            TowerDefense_ChallengeLevelCfg challengeLevelCfg =
                TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallengeByIndex(self.isAR, selectLevel);
            List<string> rewardList;
            if (clearLevel >= selectLevel)
            {
                rewardList = ET.DropItemRuleHelper.GetPreviewDropItems(challengeLevelCfg.RepeatClearDropItem);
            }
            else
            {
                rewardList = ET.DropItemRuleHelper.GetPreviewDropItems(challengeLevelCfg.FirstClearDropItem);
            }

            foreach (string itemId in rewardList)
            {
                if (ItemHelper.ChkIsTower(itemId))
                {
                    return true;
                }
            }

            return false;
        }

        public static void RefreshLevelUI(this DlgChallengeMode self, bool bClear)
        {
            self.View.ELabel_LvTextMeshProUGUI.text =
                LocalizeComponent.Instance.GetTextValue("TextCode_Key_BattleEnd_ChallengeLevel", self.selectIndex + 1);

            TowerDefense_ChallengeLevelCfg challengeLevelCfg =
                TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallengeByIndex(self.isAR, self.selectIndex + 1);
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
                TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallengeByIndex(self.isAR, self.selectIndex + 1);
            List<string> list;
            // if(clearLevel > challengeLevelCfg.Index){
            // 	list = ET.DropItemRuleHelper.GetPreviewDropItems(challengeLevelCfg.RepeatClearDropItem);
            // }else{
            list = ET.DropItemRuleHelper.GetPreviewDropItems(challengeLevelCfg.FirstClearDropItem);
            // }

            string itemCfgId = list[index];
            itemTowerBuy.ShowBagItem(itemCfgId, true);

            itemTowerBuy.SetCheckMark(clearLevel >= challengeLevelCfg.Index);
        }

        public static void AddMonsterListener(this DlgChallengeMode self, Transform transform, int index)
        {
            transform.name = $"Item_Monster_{index}";
            Scroll_Item_Monsters itemMonster = self.ScrollItemMonster[index].BindTrans(transform);
            TowerDefense_ChallengeLevelCfg challengeLevelCfg =
                TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallengeByIndex(self.isAR, self.selectIndex + 1);
            List<string> monsterList = challengeLevelCfg.MonsterList;

            string itemCfgId = monsterList[index];
            itemMonster.ShowMonsterItem(itemCfgId, true);
        }
    }
}