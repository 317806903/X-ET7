using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using ET.AbilityConfig;

namespace ET.Client
{
     [FriendOf(typeof(EPage_ChallengSeason))]
    public static class EPage_ChallengSeasonSystem
    {
        public static void RegisterUIEvent(this EPage_ChallengSeason self)
        {
            self.View.E_SelectButton.AddListenerAsync(self.Select);
            self.View.E_UnlockedButton.AddListenerAsync(self.Unlocked);

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

        public static void ShowPage(this EPage_ChallengSeason self, ShowWindowData contextData = null)
        {
#if UNITY_EDITOR
            self.isAR = false;
#else
			self.isAR = true;
#endif
            self.View.uiTransform.SetVisible(true);

            self.seasonId = ET.Client.SeasonHelper.GetSeasonId(self.DomainScene());

            self.RefreshWhenBaseInfoChg().Coroutine();
            self.ShowListScrollItem().Coroutine();
            self.ScrollToCurrentLevel().Coroutine();

            //WJTODO修改赛季剩余时间
        }

        public static void HidePage(this EPage_ChallengSeason self)
        {
            self.View.uiTransform.SetVisible(false);

        }

        public static async ETTask RefreshWhenBaseInfoChg(this EPage_ChallengSeason self)
        {
            await self.SetPlayerEnergy();
        }

        public static async ETTask SetPlayerEnergy(this EPage_ChallengSeason self)
        {
            self.View.E_SelectButton.transform.Find("number").ShowPhysicalCostText(self.DomainScene(), GlobalSettingCfgCategory.Instance.ARPVECfgTakePhsicalStrength).Coroutine();
            self.View.E_UnlockedButton.transform.Find("number").ShowPhysicalCostText(self.DomainScene(), GlobalSettingCfgCategory.Instance.ARPVECfgTakePhsicalStrength).Coroutine();
        }

        public static async ETTask Select(this EPage_ChallengSeason self)
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
                int index = self.selectIndex;
                RoomTypeInfo roomTypeInfo = ET.GamePlayHelper.GetRoomTypeInfo(roomType, subRoomType, self.seasonId, index + 1, "");

                DlgARHall_ShowWindowData _DlgARHall_ShowWindowData = new()
                {
                    ARHallType = ARHallType.CreateRoomWithOutARSceneId,
                    roomId = 0,
                    roomTypeInfo = roomTypeInfo,
                };
                await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData);
            }
            else
            {
                roomType = RoomType.Normal;
                subRoomType = SubRoomType.NormalPVE;
                int index = self.selectIndex;
                RoomTypeInfo roomTypeInfo = ET.GamePlayHelper.GetRoomTypeInfo(roomType, subRoomType, self.seasonId, index + 1, "");

                (bool result, long roomId) = await RoomHelper.CreateRoomAsync(self.ClientScene(), roomTypeInfo);
                if (result)
                {
                    await ET.Client.UIManagerHelper.EnterRoomUI(self.DomainScene());
                }
            }
        }

        public static async ETTask Unlocked(this EPage_ChallengSeason self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

            string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_ChallengeMode_Unlocked");
            ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
        }

        public static async ETTask ShowListScrollItem(this EPage_ChallengSeason self)
        {
            int count = SeasonChallengeLevelCfgCategory.Instance.GetChallenges(self.seasonId).Count;
            self.selectIndex = await self.GetCurPveIndex();
            if (self.selectIndex == count)
            {
                self.selectIndex = self.selectIndex - 1;
                self.RefreshLevelUI(true);
            }
            else
            {
                self.RefreshLevelUI(false);
            }

            self.AddUIScrollItemsPage(ref self.ScrollItemChallengeList, count);
            self.View.ELoopScrollList_ChallengeLoopHorizontalScrollRect.SetVisible(true, count);

            self.View.E_SelectButton.gameObject.SetActive(true);
            self.View.E_UnlockedButton.gameObject.SetActive(false);
        }

        public static async ETTask ScrollToCurrentLevel(this EPage_ChallengSeason self)
        {
            int index = Math.Max(0, self.selectIndex - 2);
            self.View.ELoopScrollList_ChallengeLoopHorizontalScrollRect.SrollToCellWithinTime(index, 0.5f);
        }

        public static async ETTask AddListItemRefreshListener(this EPage_ChallengSeason self, Transform transform, int index)
        {
            Scroll_Item_ChallengeList challengeList = self.ScrollItemChallengeList[index].BindTrans(transform);
            challengeList.ELabel_NormalTextMeshProUGUI.text = (index + 1).ToString();
            challengeList.ELabel_UnlockedTextMeshProUGUI.text = (index + 1).ToString();

            int clearLevel = await self.GetCurPveIndex();

            challengeList.E_SelectedImage.gameObject.SetActive(index == self.selectIndex);
            challengeList.E_NormalImage.gameObject.SetActive(index <= clearLevel);
            challengeList.E_UnlockedImage.gameObject.SetActive(index > clearLevel);
            challengeList.E_Normal_lineImage.gameObject.SetActive(index < clearLevel);
            challengeList.EG_Unlocked_lineRectTransform.gameObject.SetActive(index >= clearLevel);

            if (index == SeasonChallengeLevelCfgCategory.Instance.GetChallenges(self.seasonId).Count - 1)
            {
                challengeList.E_Normal_lineImage.gameObject.SetActive(false);
                challengeList.EG_Unlocked_lineRectTransform.gameObject.SetActive(false);
            }

            challengeList.EButton_dotButton.AddListener(() => { self.SelectLevel(index).Coroutine(); });

            challengeList.E_iconImage.SetVisible(self.CheckTowerReward(index + 1, clearLevel));
        }

        public static async ETTask SelectLevel(this EPage_ChallengSeason self, int level)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);
            self.selectIndex = level;
            self.View.ELoopScrollList_ChallengeLoopHorizontalScrollRect.RefreshCells();

            int clearLevel = await self.GetCurPveIndex();
            self.View.E_SelectButton.gameObject.SetActive(clearLevel >= level);
            self.View.E_UnlockedButton.gameObject.SetActive(clearLevel < level);
            self.RefreshLevelUI(clearLevel > level);
        }

        public static bool CheckTowerReward(this EPage_ChallengSeason self, int selectLevel, int clearLevel)
        {
            ChallengeLevelCfg challengeLevelCfg =
                SeasonChallengeLevelCfgCategory.Instance.GetChallengeByIndex(self.seasonId, selectLevel);
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

        public static void RefreshLevelUI(this EPage_ChallengSeason self, bool bClear)
        {
            self.View.ELabel_LvTextMeshProUGUI.text =
                LocalizeComponent.Instance.GetTextValue("TextCode_Key_BattleEnd_ChallengeLevel", self.selectIndex + 1);

            ChallengeLevelCfg challengeLevelCfg =
                SeasonChallengeLevelCfgCategory.Instance.GetChallengeByIndex(self.seasonId, self.selectIndex + 1);
            List<string> rewardList;
            rewardList = ET.DropItemRuleHelper.GetPreviewDropItems(challengeLevelCfg.FirstClearDropItem);
            self.View.EG_RewardRectTransform.SetVisible(rewardList.Count != 0);
            self.View.E_line02Image.SetVisible(rewardList.Count != 0);
            self.AddUIScrollItemsPage(ref self.ScrollItemReward, rewardList.Count);
            self.View.ELoopScrollList_RewardLoopHorizontalScrollRect.SetVisible(true, rewardList.Count);

            List<string> monsterList = challengeLevelCfg.MonsterListShow;
            self.AddUIScrollItemsPage(ref self.ScrollItemMonster, monsterList.Count);
            self.View.ELoopScrollList_propLoopHorizontalScrollRect.SetVisible(true, monsterList.Count);
        }

        public static async void AddTowerBuyListener(this EPage_ChallengSeason self, Transform transform, int index)
        {
            transform.name = $"Item_TowerBuy_{index}";
            Scroll_Item_TowerBuy itemTowerBuy = self.ScrollItemReward[index].BindTrans(transform);
            itemTowerBuy.EImage_TowerBuyShowImage.SetVisible(true);

            int clearLevel = await self.GetCurPveIndex();
            ChallengeLevelCfg challengeLevelCfg =
                SeasonChallengeLevelCfgCategory.Instance.GetChallengeByIndex(self.seasonId, self.selectIndex + 1);
            List<string> list;
            list = ET.DropItemRuleHelper.GetPreviewDropItems(challengeLevelCfg.FirstClearDropItem);

            string itemCfgId = list[index];
            itemTowerBuy.ShowBagItem(itemCfgId, true);

            itemTowerBuy.SetCheckMark(clearLevel >= challengeLevelCfg.Index);
        }

        public static void AddMonsterListener(this EPage_ChallengSeason self, Transform transform, int index)
        {
            transform.name = $"Item_Monster_{index}";
            Scroll_Item_Monsters itemMonster = self.ScrollItemMonster[index].BindTrans(transform);
            ChallengeLevelCfg challengeLevelCfg =
                SeasonChallengeLevelCfgCategory.Instance.GetChallengeByIndex(self.seasonId, self.selectIndex + 1);
            List<string> monsterList = challengeLevelCfg.MonsterListShow;

            string itemCfgId = monsterList[index];
            itemMonster.ShowMonsterItem(itemCfgId, true);
        }

        public static async ETTask<int> GetCurPveIndex(this EPage_ChallengSeason self)
        {
            PlayerSeasonInfoComponent playerSeasonInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerSeasonInfo(self.DomainScene());
            int pveIndex = playerSeasonInfoComponent.pveIndex;
            return pveIndex;
        }


    }
}
