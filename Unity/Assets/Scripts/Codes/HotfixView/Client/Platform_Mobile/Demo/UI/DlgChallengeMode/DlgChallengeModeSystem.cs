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
        //注册
        public static void RegisterUIEvent(this DlgChallengeMode self)
        {
            self.View.E_QuitBattleButton.AddListenerAsync(self.Back);

            self.View.EBtnRegularButton.AddListener(() => {self.SwitchPage(0); });
            self.View.EBtnSeasonButton.AddListener(() => { self.SwitchPage(1); });
            self.View.EBtnRegular_UnselectedButton.AddListener(() => { self.SetToggleButtonsActive(true); self.SwitchPage( 0); });
            self.View.EBtnSeason_UnselectedButton.AddListener(() => { self.SetToggleButtonsActive(false);self.SwitchPage(1); });
        }

        //切换界面
        private static void SwitchPage(this DlgChallengeMode self, int pageIndex)
        {
            self.pageIndex = pageIndex;
            if (pageIndex == 0)
            {
                self.View.EPage_ChallengNormal.ShowPage().Coroutine();
                self.View.EPage_ChallengSeason.HidePage();
            }
            else
            {

                UIManagerHelper.HideUIRedDot(self.DomainScene(), UIRedDotType.PVESeason).Coroutine();

                self.View.EPage_ChallengSeason.ShowPage().Coroutine();
                self.View.EPage_ChallengNormal.HidePage();
            }
        }

        //控制4个按钮的显影
        private static void SetToggleButtonsActive(this DlgChallengeMode self, bool isRegular)
        {
            self.View.EBtnRegularButton.SetVisible(isRegular);
            self.View.EBtnRegular_UnselectedButton.SetVisible(!isRegular);
            self.View.EBtnSeasonButton.SetVisible(!isRegular);
            self.View.EBtnSeason_UnselectedButton.SetVisible(isRegular);
        }

        public static async ETTask PreLoadWindow(this DlgChallengeMode self, ShowWindowData contextData, Action finished)
        {
            switch (self.pageIndex)
            {
                case 0:
                    await self.View.EPage_ChallengNormal.PreLoadWindow();
                    break;
                case 1:
                    await self.View.EPage_ChallengSeason.PreLoadWindow();
                    break;
            }

            if (self.IsDisposed)
            {
                return;
            }
            finished?.Invoke();
        }

        //显示
        public static async ETTask ShowWindow(this DlgChallengeMode self, ShowWindowData contextData = null)
        {
            self.ShowBg().Coroutine();

            DlgChallengeMode_ShowWindowData showWindowData = (DlgChallengeMode_ShowWindowData)contextData;
            if (showWindowData != null)
            {
                self.pageIndex = showWindowData.pageIndex;
            }

            SeasonComponent seasonComponent = ET.Client.SeasonHelper.GetSeasonComponent(self.DomainScene());
            self.View.ETxtSeasonDesTextMeshProUGUI.SetText(seasonComponent.cfg.Desc);

            switch (self.pageIndex)
            {
                case 0:
                {
                    // 赛季按钮失活，常规按钮激活
                    self.SetToggleButtonsActive(true);
                    EPage_ChallengNormal_ShowWindowData showWindowDataNormal = null;
                    if (showWindowData != null && showWindowData.roomTypeInfo != null)
                    {
                        showWindowDataNormal = new();
                        showWindowDataNormal.roomTypeInfo = showWindowData.roomTypeInfo;
                    }
                    self.View.EPage_ChallengNormal.ShowPage(showWindowDataNormal).Coroutine();
                    self.View.EPage_ChallengSeason.HidePage();
                    break;
                }
                case 1:
                {
                    self.SetToggleButtonsActive(false);
                    EPage_ChallengSeason_ShowWindowData showWindowDataSeason = null;
                    if (showWindowData != null && showWindowData.roomTypeInfo != null)
                    {
                        showWindowDataSeason = new();
                        showWindowDataSeason.roomTypeInfo = showWindowData.roomTypeInfo;
                    }
                    self.View.EPage_ChallengSeason.ShowPage(showWindowDataSeason).Coroutine();
                    self.View.EPage_ChallengNormal.HidePage();
                    break;
                }
            }
        }

        public static void HideWindow(this DlgChallengeMode self)
        {

        }

        //背景
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

        //外部调用（设置select/unlock两个按钮状态）
        public static async ETTask RefreshWhenBaseInfoChg(this DlgChallengeMode self)
        {
            switch (self.pageIndex)
            {
                case 0:
                    self.View.EPage_ChallengNormal.RefreshWhenBaseInfoChg().Coroutine();
                    break;
                case 1:
                    self.View.EPage_ChallengSeason.RefreshWhenBaseInfoChg().Coroutine();
                    break;
            }
        }

        public static async ETTask RefreshWhenSeasonRemainChg(this DlgChallengeMode self)
        {
            self.View.EPage_ChallengSeason.RefreshWhenSeasonRemainChg().Coroutine();
        }

        //退出按钮
        public static async ETTask Back(this DlgChallengeMode self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Back);
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgChallengeMode>();
            await UIManagerHelper.EnterGameModeUI(self.DomainScene());
        }

    }
}