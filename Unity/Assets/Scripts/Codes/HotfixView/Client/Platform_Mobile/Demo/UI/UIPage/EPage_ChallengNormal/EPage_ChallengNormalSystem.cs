using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using ET.AbilityConfig;

namespace ET.Client
{
	[FriendOf(typeof(EPage_ChallengNormal))]
	public static class EPage_ChallengNormalSystem
	{
		public static void RegisterUIEvent(this EPage_ChallengNormal self)
		{
            self.View.E_SelectButton.AddListenerAsync(self.Select);
            self.View.E_UnlockedButton.AddListenerAsync(self.Unlocked);

            if (ET.Client.UIManagerHelper.ChkIsDebug())
            {
                self.View.E_DebugButton.SetVisible(true);
                self.View.E_DebugButton.AddListenerAsync(self.SetCurPveIndexWhenDebug);
            }
            else
            {
                self.View.E_DebugButton.SetVisible(false);
            }

            self.View.ELoopScrollList_ChallengeLoopHorizontalScrollRect.prefabSource.prefabName = "Item_ChallengeList";
            self.View.ELoopScrollList_ChallengeLoopHorizontalScrollRect.prefabSource.poolSize = 7;
            self.View.ELoopScrollList_ChallengeLoopHorizontalScrollRect.AddItemRefreshListener(((transform, i) =>
                self.AddListItemRefreshListener(transform, i).Coroutine()));

            self.View.ELoopScrollList_RewardLoopHorizontalScrollRect.prefabSource.prefabName = "Item_ItemShow";
            self.View.ELoopScrollList_RewardLoopHorizontalScrollRect.prefabSource.poolSize = 3;
            self.View.ELoopScrollList_RewardLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
                self.AddTowerBuyListener(transform, i).Coroutine());

            self.View.E_default_EasyButton.AddListenerAsync(async () =>
            {
                await self.SwitchDifficulty(PVELevelDifficulty.Easy);
            });

            self.View.E_default_NormalButton.AddListenerAsync(async () =>
            {
                await self.SwitchDifficulty(PVELevelDifficulty.Normal);
            });

            self.View.E_default_HardButton.AddListenerAsync(async () =>
            {
                await self.SwitchDifficulty(PVELevelDifficulty.Hard);
            });

            self.View.E_default_ExtremeButton.AddListenerAsync(async () =>
            {
                await self.SwitchDifficulty(PVELevelDifficulty.Hell);
            });

        }

        public static async ETTask SwitchDifficulty(this EPage_ChallengNormal self, PVELevelDifficulty pveLevelDifficulty)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);
            self.pveLevelDifficulty = pveLevelDifficulty;

            var visibilitySettings = new Dictionary<PVELevelDifficulty, (bool easy, bool normal, bool hard, bool extreme)>
            {
                { PVELevelDifficulty.Easy, (true, false, false, false) },
                { PVELevelDifficulty.Normal, (false, true, false, false) },
                { PVELevelDifficulty.Hard, (false, false, true, false) },
                { PVELevelDifficulty.Hell, (false, false, false, true) }
            };

            if (visibilitySettings.TryGetValue(pveLevelDifficulty, out var settings))
            {
                SetImageVisibility(settings.easy, settings.normal, settings.hard, settings.extreme);
                if (pveLevelDifficulty == PVELevelDifficulty.Hell)
                {
                    bool isPassHard = await self.ChkIsPassPVELevel(self.selectIndex, PVELevelDifficulty.Hard);
                    self.RefreshSelectBtnUI(isPassHard);
                }
                else
                {
                    bool isLock = await self.ChkIsLockPVELevel(self.selectIndex);
                    self.RefreshSelectBtnUI(!isLock);
                }
            }
            else {
                Log.Error($"Don't have {pveLevelDifficulty} difficulty!");
            }

            void SetImageVisibility(bool easySelected, bool normalSelected, bool hardSelected, bool extremeSelected)
            {
                self.View.E_select_EasyImage.SetVisible(false);
                self.View.E_default_EasyImage.SetVisible(false);
                self.View.E_default_NormalImage.SetVisible(false);
                self.View.E_select_NormalImage.SetVisible(false);
                self.View.E_default_HardImage.SetVisible(false);
                self.View.E_select_HardImage.SetVisible(false);
                self.View.E_default_ExtremeImage.SetVisible(false);
                self.View.E_select_ExtremeImage.SetVisible(false);
                bool hasEasy = TowerDefense_ChallengeLevelCfgCategory.Instance.HasPVEDiffculty(PVELevelDifficulty.Easy);
                bool hasNormal = TowerDefense_ChallengeLevelCfgCategory.Instance.HasPVEDiffculty(PVELevelDifficulty.Normal);
                bool hasHard = TowerDefense_ChallengeLevelCfgCategory.Instance.HasPVEDiffculty(PVELevelDifficulty.Hard);
                bool hasExtreme = TowerDefense_ChallengeLevelCfgCategory.Instance.HasPVEDiffculty(PVELevelDifficulty.Hell);
                if (hasEasy)
                {
                    self.View.E_select_EasyImage.SetVisible(easySelected);
                    self.View.E_default_EasyImage.SetVisible(!easySelected);
                }

                if (hasNormal)
                {
                    self.View.E_select_NormalImage.SetVisible(normalSelected);
                    self.View.E_default_NormalImage.SetVisible(!normalSelected);
                }

                if (hasHard)
                {
                    self.View.E_select_HardImage.SetVisible(hardSelected);
                    self.View.E_default_HardImage.SetVisible(!hardSelected);
                }

                if (hasExtreme)
                {
                    self.View.E_select_ExtremeImage.SetVisible(extremeSelected);
                    self.View.E_default_ExtremeImage.SetVisible(!extremeSelected);
                }
            }

            // 刷新奖励列表的UI
            self.RefreshRewardsUI();

            // 刷新 backstory description
            self.RefreshBackStoryDesc();
        }

        public static async ETTask PreLoadWindow(this EPage_ChallengNormal self)
        {
            PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());

        }

        public static async ETTask ShowPage(this EPage_ChallengNormal self, ShowWindowData contextData = null)
        {
            if (ET.Client.UIManagerHelper.ChkIsAR() == false)
            {
                self.isAR = false;
            }
            else
            {
                self.isAR = true;
            }
            self.View.uiTransform.SetVisible(true);

            EPage_ChallengNormal_ShowWindowData showWindowData = (EPage_ChallengNormal_ShowWindowData)contextData;
            if (showWindowData != null && showWindowData.roomTypeInfo != null)
            {
                self.selectIndex = showWindowData.roomTypeInfo.pveIndex;
                self.pveLevelDifficulty = showWindowData.roomTypeInfo.pveLevelDifficulty;
            }

            await self.RefreshWhenBaseInfoChg();
            await self.ShowListScrollItem(false);
            await self.ScrollToCurrentLevel();
            await self.SwitchDifficulty(self.pveLevelDifficulty);
        }

        public static void HidePage(this EPage_ChallengNormal self)
        {
            self.View.uiTransform.SetVisible(false);

        }

        public static async ETTask RefreshWhenBaseInfoChg(this EPage_ChallengNormal self)
        {
            await self.SetPlayerEnergy();
        }

        public static async ETTask SetPlayerEnergy(this EPage_ChallengNormal self)
        {
            self.View.E_SelectButton.transform.Find("number").ShowPhysicalCostText(self.DomainScene(), ET.GamePlayHelper.GetPhysicalCostPVE()).Coroutine();
            self.View.E_UnlockedButton.transform.Find("number").ShowPhysicalCostText(self.DomainScene(), ET.GamePlayHelper.GetPhysicalCostPVE()).Coroutine();
        }

        public static async ETTask Select(this EPage_ChallengNormal self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

            if (await ET.Client.UIManagerHelper.ChkPhsicalAndShowtip(self.DomainScene(),
                    ET.GamePlayHelper.GetPhysicalCostPVE()) == false)
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
                int pveLevel = self.selectIndex;
                RoomTypeInfo roomTypeInfo = ET.GamePlayHelper.GetRoomTypeInfo(roomType, subRoomType, -1, pveLevel, self.pveLevelDifficulty, "");


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
                int pveLevel = self.selectIndex;
                RoomTypeInfo roomTypeInfo = ET.GamePlayHelper.GetRoomTypeInfo(roomType, subRoomType, -1, pveLevel, self.pveLevelDifficulty, "");


                (bool result, long roomId) = await RoomHelper.CreateRoomAsync(self.ClientScene(), roomTypeInfo);
                if (result)
                {
                    await ET.Client.UIManagerHelper.EnterRoomUI(self.DomainScene());
                }
            }
        }

        public static async ETTask Unlocked(this EPage_ChallengNormal self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);
            string tipMsg = string.Empty;
            if (self.pveLevelDifficulty == PVELevelDifficulty.Hell)
            {
                tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_ChallengeMode_Hard_Unlocked");
            }
            else
            {
                tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_ChallengeMode_Unlocked");
            }
            ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
        }

        public static async ETTask ShowListScrollItem(this EPage_ChallengNormal self, bool isResetSelectIndex)
        {
            if (isResetSelectIndex || self.selectIndex == 0)
            {
                self.selectIndex = await self.GetLastUnLockPVELevel();
                if (self.pveLevelDifficulty == 0)
                {
                    self.pveLevelDifficulty = PVELevelDifficulty.Easy;
                }
            }
            int count = TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallengesCount();
            if(!((self.selectIndex >= 1) && (self.selectIndex <= count)))
            {
                Log.Error($"selectIndex is {self.selectIndex}");
            }

            // 更新奖励列表
            self.RefreshRewardsUI();

            // 更新Level列表
            self.AddUIScrollItems(ref self.ScrollItemChallengeList, count);
            self.View.ELoopScrollList_ChallengeLoopHorizontalScrollRect.SetVisible(true, count);
        }

        public static async ETTask ScrollToCurrentLevel(this EPage_ChallengNormal self)
        {
            int index = Math.Max(0, self.selectIndex - 2);
            self.View.ELoopScrollList_ChallengeLoopHorizontalScrollRect.SrollToCellWithinTime(index, 0.3f);
        }

        public static async ETTask AddListItemRefreshListener(this EPage_ChallengNormal self, Transform transform, int index)
        {
            Scroll_Item_ChallengeList challengeList = self.ScrollItemChallengeList[index].BindTrans(transform);

            int pveLevel = index + 1;
            challengeList.ELabel_NormalTextMeshProUGUI.text = pveLevel.ToString();
            challengeList.ELabel_UnlockedTextMeshProUGUI.text = pveLevel.ToString();

            bool isLock = await self.ChkIsLockPVELevel(pveLevel);
            bool isPass = await self.ChkIsPassPVELevel(pveLevel, self.pveLevelDifficulty);
            challengeList.E_SelectedImage.gameObject.SetActive(pveLevel == self.selectIndex);
            challengeList.E_NormalImage.gameObject.SetActive(!isLock);
            challengeList.E_UnlockedImage.gameObject.SetActive(isLock);
            challengeList.E_Normal_lineImage.gameObject.SetActive(!isLock);
            challengeList.EG_Unlocked_lineRectTransform.gameObject.SetActive(isLock);

            challengeList.EG_DifficultyMode_iconRectTransform.gameObject.SetActive(true);

            // 设置难度图标
            challengeList.E_icon_easyImage.gameObject.SetActive(false);
            challengeList.E_icon_normalImage.gameObject.SetActive(false);
            challengeList.E_icon_hardImage.gameObject.SetActive(false);
            challengeList.E_icon_extremeImage.gameObject.SetActive(false);

            if (await self.ChkIsPassPVELevel(pveLevel, PVELevelDifficulty.Hell))
            {
                challengeList.E_icon_extremeImage.gameObject.SetActive(true);
            }
            else if (await self.ChkIsPassPVELevel(pveLevel, PVELevelDifficulty.Hard))
            {
                challengeList.E_icon_hardImage.gameObject.SetActive(true);
            }
            else if (await self.ChkIsPassPVELevel(pveLevel, PVELevelDifficulty.Normal))
            {
                challengeList.E_icon_normalImage.gameObject.SetActive(true);
            }
            else if (await self.ChkIsPassPVELevel(pveLevel, PVELevelDifficulty.Easy))
            {
                challengeList.E_icon_easyImage.gameObject.SetActive(true);
            }

            if (index == TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallengesCount() - 1)
            {
                challengeList.E_Normal_lineImage.gameObject.SetActive(false);
                challengeList.EG_Unlocked_lineRectTransform.gameObject.SetActive(false);
            }

            challengeList.EButton_dotButton.AddListener(() => { self.SelectLevel(pveLevel).Coroutine(); });

            challengeList.E_Rewards_iconImage.SetVisible(await self.CheckTowerReward(pveLevel, self.pveLevelDifficulty));
        }

        public static async ETTask SelectLevel(this EPage_ChallengNormal self, int level)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);
            self.selectIndex = level;
            self.View.ELoopScrollList_ChallengeLoopHorizontalScrollRect.RefreshCells();

            if (self.pveLevelDifficulty == PVELevelDifficulty.Hell)
            {
                bool isPassHard = await self.ChkIsPassPVELevel(self.selectIndex, PVELevelDifficulty.Hard);
                Log.Debug($"ZHIGUO: isPassHard:{isPassHard}");
                self.RefreshSelectBtnUI(isPassHard);
            }
            else
            {
                bool isLock = await self.ChkIsLockPVELevel(level);
                self.RefreshSelectBtnUI(!isLock);
            }
            self.RefreshRewardsUI();
            self.RefreshBackStoryDesc();
        }

        public static void RefreshSelectBtnUI(this EPage_ChallengNormal self, bool showSelect)
        {
            self.View.E_SelectButton.gameObject.SetActive(showSelect);
            self.View.E_UnlockedButton.gameObject.SetActive(!showSelect);
        }

        public static async ETTask<bool> CheckTowerReward(this EPage_ChallengNormal self, int selectLevel, PVELevelDifficulty pveLevelDifficulty)
        {
            ChallengeLevelCfg challengeLevelCfg = TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallengeByIndex(selectLevel, pveLevelDifficulty);
            bool isPass = await self.ChkIsPassPVELevel(selectLevel, pveLevelDifficulty);
            if (isPass)
            {
                foreach (var item in challengeLevelCfg.RepeatRewardItemListShow)
                {
                    if (ET.ItemHelper.ChkIsToken(item.Key) == false)
                    {
                        return true;
                    }
                }
            }
            else
            {
                foreach (var item in challengeLevelCfg.FirstRewardItemListShow)
                {
                    if (ET.ItemHelper.ChkIsToken(item.Key) == false)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static async void RefreshRewardsUI(this EPage_ChallengNormal self)
        {
            var allDropItems = await self.GetAllDropItemDic(self.selectIndex, self.pveLevelDifficulty);

            self.itemList = new();

            foreach (var item in allDropItems)
            {
                self.itemList.Add((item.Key, item.Value));
            }

            self.View.EG_RewardRectTransform.SetVisible(self.itemList.Count != 0);
            self.AddUIScrollItems(ref self.ScrollItemReward, self.itemList.Count);
            self.View.ELoopScrollList_RewardLoopHorizontalScrollRect.SetVisible(true, self.itemList.Count);
        }

        public static async ETTask AddTowerBuyListener(this EPage_ChallengNormal self, Transform transform, int index)
        {
            transform.name = $"Item_TowerBattleBuy_{index}";
            Scroll_Item_ItemShow itemTowerBuy = self.ScrollItemReward[index].BindTrans(transform);

            string itemCfgId = self.itemList[index].itemCfgId;
            int itemNum = self.itemList[index].itemNum;

            await itemTowerBuy.Init(itemCfgId, true);
            itemTowerBuy.SetItemNum(itemNum);

            bool isPass = await self.ChkIsPassPVELevel(self.selectIndex, PVELevelDifficulty.Easy);
            itemTowerBuy.ShowCheck(isPass);
        }

        public static async ETTask SetCurPveIndexWhenDebug(this EPage_ChallengNormal self)
        {
            PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());

            playerBaseInfoComponent.pveLevelInfo = new();
            for (int i = 1; i < self.selectIndex; i++)
            {
                playerBaseInfoComponent.pveLevelInfo[i] = self.pveLevelDifficulty;
            }
            int count = TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallengesCount();
            if (self.selectIndex == count)
            {
                playerBaseInfoComponent.pveLevelInfo[self.selectIndex] = self.pveLevelDifficulty;
            }
            if (self.pveLevelDifficulty == PVELevelDifficulty.Hell)
            {
                playerBaseInfoComponent.pveLevelInfo[self.selectIndex] = PVELevelDifficulty.Hard;
            }

            await ET.Client.PlayerCacheHelper.SaveMyPlayerModel(self.DomainScene(), PlayerModelType.BaseInfo, new() { "pveLevelInfo" });
            await self.ShowListScrollItem(false);
            if (self.pveLevelDifficulty == PVELevelDifficulty.Hell)
            {
                bool isPassHard = await self.ChkIsPassPVELevel(self.selectIndex, PVELevelDifficulty.Hard);
                self.RefreshSelectBtnUI(isPassHard);
            }
            else
            {
                bool isLock = await self.ChkIsLockPVELevel(self.selectIndex);
                self.RefreshSelectBtnUI(!isLock);
            }
            await self.ScrollToCurrentLevel();
        }

        public static async ETTask<bool> ChkIsPassPVELevel(this EPage_ChallengNormal self, int pveLevel, PVELevelDifficulty pveLevelDifficulty)
        {
            PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());

            return playerBaseInfoComponent.ChkIsPassPVELevel(pveLevel, pveLevelDifficulty);
        }

        public static async ETTask<bool> ChkIsLockPVELevel(this EPage_ChallengNormal self, int pveLevel)
        {
            PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());

            return playerBaseInfoComponent.ChkIsLockPVELevel(pveLevel);
        }

        public static async ETTask<int> GetLastUnLockPVELevel(this EPage_ChallengNormal self)
        {
            PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());
            return playerBaseInfoComponent.GetLastUnLockPVELevel();
        }

        public static async ETTask<Dictionary<string, int>> GetAllDropItemDic(this EPage_ChallengNormal self, int pveLevel, PVELevelDifficulty curPveLevelDifficulty)
        {
            PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());
            return playerBaseInfoComponent.GetAllDropItemDic(pveLevel, curPveLevelDifficulty, true);
        }

        public static void RefreshBackStoryDesc(this EPage_ChallengNormal self)
        {
            ChallengeLevelCfg challengeLevelCfg =
                TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallengeByIndex(self.selectIndex, self.pveLevelDifficulty);

            if (challengeLevelCfg != null)
            {
                self.View.ELable_Text_Backstory_describeTextMeshProUGUI.text = challengeLevelCfg.Desc;
            }
            else
            {
                Log.Error("Failed to get ChallengeLevelCfg");
            }
        }

    }
}
