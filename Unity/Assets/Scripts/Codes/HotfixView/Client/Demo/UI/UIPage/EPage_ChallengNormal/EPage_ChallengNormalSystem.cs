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

#if UNITY_EDITOR
            self.View.E_DebugButton.SetVisible(true);
            self.View.E_DebugButton.AddListenerAsync(self.SetCurPveIndexWhenDebug);
#else
			self.View.E_DebugButton.SetVisible(false);
#endif

            self.View.ELoopScrollList_ChallengeLoopHorizontalScrollRect.prefabSource.prefabName = "Item_ChallengeList";
            self.View.ELoopScrollList_ChallengeLoopHorizontalScrollRect.prefabSource.poolSize = 7;
            self.View.ELoopScrollList_ChallengeLoopHorizontalScrollRect.AddItemRefreshListener(((transform, i) =>
                self.AddListItemRefreshListener(transform, i).Coroutine()));

            self.View.ELoopScrollList_RewardLoopHorizontalScrollRect.prefabSource.prefabName = "Item_ItemShow";
            self.View.ELoopScrollList_RewardLoopHorizontalScrollRect.prefabSource.poolSize = 3;
            self.View.ELoopScrollList_RewardLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
                self.AddTowerBuyListener(transform, i));

            self.View.ELoopScrollList_propLoopHorizontalScrollRect.prefabSource.prefabName = "Item_Monsters";
            self.View.ELoopScrollList_propLoopHorizontalScrollRect.prefabSource.poolSize = 3;
            self.View.ELoopScrollList_propLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
                self.AddMonsterListener(transform, i));

        }

        public static async ETTask ShowPage(this EPage_ChallengNormal self, ShowWindowData contextData = null)
		{
#if UNITY_EDITOR
            self.isAR = false;
#else
			self.isAR = true;
#endif
            self.View.uiTransform.SetVisible(true);

            await self.RefreshWhenBaseInfoChg();
            await self.ShowListScrollItem();
            await self.ScrollToCurrentLevel();
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
                int index = self.selectIndex;
                RoomTypeInfo roomTypeInfo = ET.GamePlayHelper.GetRoomTypeInfo(roomType, subRoomType, -1, index + 1, "");

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
                RoomTypeInfo roomTypeInfo = ET.GamePlayHelper.GetRoomTypeInfo(roomType, subRoomType, -1, index + 1, "");

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

            string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_ChallengeMode_Unlocked");
            ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
        }

        public static async ETTask ShowListScrollItem(this EPage_ChallengNormal self)
        {
            int count = TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallenges().Count;
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

            self.AddUIScrollItems(ref self.ScrollItemChallengeList, count);
            self.View.ELoopScrollList_ChallengeLoopHorizontalScrollRect.SetVisible(true, count);

            self.View.E_SelectButton.gameObject.SetActive(true);
            self.View.E_UnlockedButton.gameObject.SetActive(false);
        }

        public static async ETTask ScrollToCurrentLevel(this EPage_ChallengNormal self)
        {
            PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());
            int index = Math.Max(0, self.selectIndex - 2);
            self.View.ELoopScrollList_ChallengeLoopHorizontalScrollRect.SrollToCellWithinTime(index, 0.5f);
        }

        public static async ETTask AddListItemRefreshListener(this EPage_ChallengNormal self, Transform transform, int index)
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

            if (index == TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallenges().Count - 1)
            {
                challengeList.E_Normal_lineImage.gameObject.SetActive(false);
                challengeList.EG_Unlocked_lineRectTransform.gameObject.SetActive(false);
            }

            challengeList.EButton_dotButton.AddListener(() => { self.SelectLevel(index).Coroutine(); });

            challengeList.E_iconImage.SetVisible(self.CheckTowerReward(index + 1, clearLevel));
        }

        public static async ETTask SelectLevel(this EPage_ChallengNormal self, int level)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);
            self.selectIndex = level;
            self.View.ELoopScrollList_ChallengeLoopHorizontalScrollRect.RefreshCells();
            int clearLevel = await self.GetCurPveIndex();
            self.View.E_SelectButton.gameObject.SetActive(clearLevel >= level);
            self.View.E_UnlockedButton.gameObject.SetActive(clearLevel < level);
            self.RefreshLevelUI(clearLevel > level);
        }

        public static bool CheckTowerReward(this EPage_ChallengNormal self, int selectLevel, int clearLevel)
        {
            ChallengeLevelCfg challengeLevelCfg = TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallengeByIndex(selectLevel);
            if (clearLevel >= selectLevel)
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

        public static void RefreshLevelUI(this EPage_ChallengNormal self, bool bClear)
        {
            self.View.ELabel_LvTextMeshProUGUI.text =
                LocalizeComponent.Instance.GetTextValue("TextCode_Key_BattleEnd_ChallengeLevel", self.selectIndex + 1);

            ChallengeLevelCfg challengeLevelCfg =
                TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallengeByIndex(self.selectIndex + 1);

            self.itemList = new();
            if (bClear)
            {
                foreach (var item in challengeLevelCfg.RepeatRewardItemListShow)
                {
                    self.itemList.Add((item.Key, item.Value));
                }
            }
            else
            {
                foreach (var item in challengeLevelCfg.FirstRewardItemListShow)
                {
                    self.itemList.Add((item.Key, item.Value));
                }
            }

            self.View.EG_RewardRectTransform.SetVisible(self.itemList.Count != 0);
            self.View.E_line02Image.SetVisible(self.itemList.Count != 0);
            self.AddUIScrollItems(ref self.ScrollItemReward, self.itemList.Count);
            self.View.ELoopScrollList_RewardLoopHorizontalScrollRect.SetVisible(true, self.itemList.Count);

            List<string> monsterList = challengeLevelCfg.MonsterListShow;
            self.AddUIScrollItems(ref self.ScrollItemMonster, monsterList.Count);
            self.View.ELoopScrollList_propLoopHorizontalScrollRect.SetVisible(true, monsterList.Count);
        }

        public static async ETTask AddTowerBuyListener(this EPage_ChallengNormal self, Transform transform, int index)
        {
            transform.name = $"Item_TowerBattleBuy_{index}";
            Scroll_Item_ItemShow itemTowerBuy = self.ScrollItemReward[index].BindTrans(transform);

            int clearLevel = await self.GetCurPveIndex();
            ChallengeLevelCfg challengeLevelCfg = TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallengeByIndex(self.selectIndex + 1);

            string itemCfgId = self.itemList[index].itemCfgId;
            int itemNum = self.itemList[index].itemNum;

            await itemTowerBuy.Init(itemCfgId, true);
            itemTowerBuy.SetItemNum(itemNum);
            itemTowerBuy.ShowCheck(clearLevel >= challengeLevelCfg.Index);
        }

        public static void AddMonsterListener(this EPage_ChallengNormal self, Transform transform, int index)
        {
            transform.name = $"Item_Monster_{index}";
            Scroll_Item_Monsters itemMonster = self.ScrollItemMonster[index].BindTrans(transform);
            ChallengeLevelCfg challengeLevelCfg =
                TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallengeByIndex(self.selectIndex + 1);
            List<string> monsterList = challengeLevelCfg.MonsterListShow;

            string itemCfgId = monsterList[index];
            itemMonster.ShowMonsterItem(itemCfgId, true).Coroutine();
        }

        public static async ETTask<int> GetCurPveIndex(this EPage_ChallengNormal self)
        {
            PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());
            int clearLevel = playerBaseInfoComponent.ChallengeClearLevel;
            return clearLevel;
        }

        public static async ETTask SetCurPveIndexWhenDebug(this EPage_ChallengNormal self)
        {
            PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());
            playerBaseInfoComponent.ChallengeClearLevel = self.selectIndex;
            await ET.Client.PlayerCacheHelper.SaveMyPlayerModel(self.DomainScene(), PlayerModelType.BaseInfo, new (){"ChallengeClearLevel"});
            await self.ShowListScrollItem();
            //self.RefreshLevelUI(false);
        }



    }
}
