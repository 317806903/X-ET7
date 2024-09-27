using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using ET.AbilityConfig;
using TMPro;

namespace ET.Client
{
    [FriendOf(typeof(DlgRankPowerupSeason))]
    public static class DlgRankPowerupSeasonSystem
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="self"></param>
        public static void RegisterUIEvent(this DlgRankPowerupSeason self)
        {
            self.View.E_QuitRankButton.AddListenerAsync(self.Back);

            self.View.EBtnTabLeaderboardButton.AddListener(() => { self.SwitchPage( 0); });
            self.View.EBtnTabPowerupsButton.AddListener(() => { self.SwitchPage(1); });
            self.View.EBtnTabLeaderboardUnselectedButton.AddListener(() => { self.ToggleButtons(true); self.SwitchPage(0); });
            self.View.EBtnTabPowerups_UnselectedButton.AddListener(() => { self.ToggleButtons(false); self.SwitchPage(1); });

            //头像框
            self.View.ELoopListView_FrameLoopHorizontalScrollRect.prefabSource.prefabName = "Item_Frame";
            self.View.ELoopListView_FrameLoopHorizontalScrollRect.prefabSource.poolSize = 4;
            self.View.ELoopListView_FrameLoopHorizontalScrollRect.AddItemRefreshListener(async (transform, i) =>
            {
                await self.AddFrameItemRefreshListener(transform, i);
                self.View.ELoopListView_FrameLoopHorizontalScrollRect.SetSrcollMiddle().Coroutine();
            });

            //赛季奖励塔
            self.View.ELoopListView_CardsLoopHorizontalScrollRect.prefabSource.prefabName = "Item_TowerBuy";
            self.View.ELoopListView_CardsLoopHorizontalScrollRect.prefabSource.poolSize = 4;
            self.View.ELoopListView_CardsLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
            {
                self.AddTowerBuyListener(transform, i).Coroutine();
                self.View.ELoopListView_CardsLoopHorizontalScrollRect.SetSrcollMiddle().Coroutine();
            });

            //赛季怪物
            self.View.ELoopListView_MonsersLoopHorizontalScrollRect.prefabSource.prefabName = "Item_Monsters";
            self.View.ELoopListView_MonsersLoopHorizontalScrollRect.prefabSource.poolSize = 4;
            self.View.ELoopListView_MonsersLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
            {
                self.AddMonsterListener(transform, i);
                self.View.ELoopListView_MonsersLoopHorizontalScrollRect.SetSrcollMiddle().Coroutine();
            });
        }

        /// <summary>
        /// 切换界面
        /// </summary>
        /// <param name="self"></param>
        /// <param name="pageIndex"></param>
        private static void SwitchPage(this DlgRankPowerupSeason self, int pageIndex)
        {
            self.pageIndex = pageIndex;
            if (pageIndex == 0)
            {
                self.View.EPage_Rank.ShowPage().Coroutine();
                self.View.EPage_Powerup.HidePage();
            }
            else
            {
                self.View.EPage_Powerup.ShowPage().Coroutine();
                self.View.EPage_Rank.HidePage();
            }
        }

        /// <summary>
        /// 控制4个按钮的显影
        /// </summary>
        /// <param name="self"></param>
        /// <param name="isRankPage"></param>
        private static void ToggleButtons(this DlgRankPowerupSeason self, bool isRankPage)
        {
            self.View.EBtnTabLeaderboardButton.SetVisible(isRankPage);
            self.View.EBtnTabLeaderboardUnselectedButton.SetVisible(!isRankPage);
            self.View.EBtnTabPowerupsButton.SetVisible(!isRankPage);
            self.View.EBtnTabPowerups_UnselectedButton.SetVisible(isRankPage);
        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="self"></param>
        /// <param name="contextData"></param>
        public static async ETTask ShowWindow(this DlgRankPowerupSeason self, ShowWindowData contextData = null)
        {
            self.ShowBg().Coroutine();
            self.SetTitleTxt().Coroutine();
            self.ToggleButtons( true);


            self.pageIndex = 0;
            switch (self.pageIndex)
            {
                case 0:
                    self.View.EPage_Rank.ShowPage().Coroutine();
                    self.View.EPage_Powerup.HidePage();
                    break;
                case 1:
                    self.View.EPage_Powerup.ShowPage().Coroutine();
                    self.View.EPage_Rank.HidePage();
                    break;
            }
            SeasonComponent seasonComponent = ET.Client.SeasonHelper.GetSeasonComponent(self.DomainScene());
            self.avatarFrameList = seasonComponent.cfg.RewardItemListShow;

            self.seasonCfgId = ET.Client.SeasonHelper.GetSeasonCfgId(self.DomainScene());
            self.SetEloopNumber(true).Coroutine();


            self.View.ELoopListView_CardsLoopHorizontalScrollRect.RefreshCells();
            self.View.ELoopListView_FrameLoopHorizontalScrollRect.RefreshCells();
            self.View.ELoopListView_MonsersLoopHorizontalScrollRect.RefreshCells();
        }

        /// <summary>
        /// 设置赛季标题的名字文本
        /// </summary>
        /// <param name="self"></param>
        /// <param name="contextData"></param>
        public static async ETTask SetTitleTxt(this DlgRankPowerupSeason self)
        {
            SeasonComponent seasonComponent = ET.Client.SeasonHelper.GetSeasonComponent(self.DomainScene());
            string textName = seasonComponent.cfg.Name;
            self.View.ETxtTitleTextMeshProUGUI.SetText(textName);

            string textTime = ET.Client.SeasonHelper.GetSeasonLeftTime(self.DomainScene());
            self.View.ETxtTimeTextMeshProUGUI.SetText(textTime);
            await ETTask.CompletedTask;
        }

        public static async ETTask RefreshWhenDiamondChg(this DlgRankPowerupSeason self)
        {
            await self.View.EPage_Powerup.RefreshWhenDiamondChg();
            await ETTask.CompletedTask;
        }

        public static async ETTask RefreshWhenSeasonRemainChg(this DlgRankPowerupSeason self)
        {
            await self.SetTitleTxt();
            await ETTask.CompletedTask;
        }



        #region 控件事件监听函数
        /// <summary>
        /// 背景
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static async ETTask ShowBg(this DlgRankPowerupSeason self)
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
            await ETTask.CompletedTask;
        }

        /// <summary>
        /// 退出按钮
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static async ETTask Back(this DlgRankPowerupSeason self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Back);
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgRankPowerupSeason>();
            await UIManagerHelper.EnterGameModeUI(self.DomainScene());
        }

        /// <summary>
        /// 塔刷新
        /// </summary>
        /// <param name="self"></param>
        /// <param name="transform"></param>
        /// <param name="index"></param>
        public static async ETTask AddTowerBuyListener(this DlgRankPowerupSeason self, Transform transform, int index)
        {
            List<string> list;
            SeasonComponent seasonComponent = ET.Client.SeasonHelper.GetSeasonComponent(self.DomainScene());

            transform.name = $"Item_TowerBuy_{index}";
            Scroll_Item_TowerBuy itemTowerBuy = self.ScrollItemReward[index].BindTrans(transform);
            itemTowerBuy.EG_TowerBuyShowRectTransform.SetVisible(true);

            //int clearLevel = await self.GetCurPveIndex();
            //ChallengeLevelCfg challengeLevelCfg =
            //    SeasonChallengeLevelCfgCategory.Instance.GetChallengeByIndex(self.seasonCfgId, self.selectIndex + 1);

            list = seasonComponent.cfg.TowerListShow;

            string itemCfgId = list[index];
            await itemTowerBuy.ShowBagItem(itemCfgId, true);

            //WJTODO 获取玩家背包是否有该塔
            //itemTowerBuy.SetCheckMark(clearLevel >= challengeLevelCfg.Index);
        }

        /// <summary>
        /// 怪物刷新
        /// </summary>
        /// <param name="self"></param>
        /// <param name="transform"></param>
        /// <param name="index"></param>
        public static  void AddMonsterListener(this DlgRankPowerupSeason self, Transform transform, int index)
        {
            SeasonComponent seasonComponent = ET.Client.SeasonHelper.GetSeasonComponent(self.DomainScene());

            transform.name = $"Item_Monster_{index}";
            Scroll_Item_Monsters itemMonster = self.ScrollItemMonster[index].BindTrans(transform);
            List<string> monsterList;
            monsterList = seasonComponent.cfg.MonsterListShow;

            string itemCfgId = monsterList[index];
            itemMonster.ShowMonsterItem(itemCfgId, true).Coroutine();
        }

        /// <summary>
        /// 头像框的刷新
        /// </summary>
        /// <param name="self"></param>
        /// <param name="transform"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static async ETTask AddFrameItemRefreshListener(this DlgRankPowerupSeason self, Transform transform, int index)
        {
            transform.gameObject.name = "Item_Frame" + index;
            Scroll_Item_Frame itemFrame = self.ScrollItemFrameIcons[index].BindTrans(transform);
            string itemCfgId = null;
            itemCfgId = self.avatarFrameList[index];
            itemFrame.ShowFrameItem(itemCfgId, true);
            await itemFrame.EImage_FrameImage.SetImageByItemCfgId(self, itemCfgId);
            itemFrame.EIcon_SelectedImage.gameObject.SetActive(false);
        }

        #endregion

        /// <summary>
        /// 获取当前pve关卡
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static async ETTask<int> GetCurPveIndex(this DlgRankPowerupSeason self)
        {
            PlayerSeasonInfoComponent playerSeasonInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerSeasonInfo(self.DomainScene());
            int pveIndex = playerSeasonInfoComponent.pveIndex;
            return pveIndex;
        }

        /// <summary>
        /// 设置循环列表数量
        /// </summary>
        /// <param name="self"></param>
        /// <param name="bClear"></param>
        public static async ETTask SetEloopNumber(this DlgRankPowerupSeason self, bool bClear)
        {
            List<string> list;
            SeasonComponent seasonComponent = ET.Client.SeasonHelper.GetSeasonComponent(self.DomainScene());
            list = seasonComponent.cfg.TowerListShow;
            self.AddUIScrollItems(ref self.ScrollItemReward, list.Count);
            self.View.ELoopListView_CardsLoopHorizontalScrollRect.SetVisible(true, list.Count);

            list = seasonComponent.cfg.MonsterListShow;
            self.AddUIScrollItems(ref self.ScrollItemMonster, list.Count);
            self.View.ELoopListView_MonsersLoopHorizontalScrollRect.SetVisible(true, list.Count);

            list = self.avatarFrameList;
            self.AddUIScrollItems(ref self.ScrollItemFrameIcons, list.Count);
            self.View.ELoopListView_FrameLoopHorizontalScrollRect.SetVisible(true, list.Count);
        }
    }
}