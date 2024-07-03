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
            self.View.EBtnRegular_UnselectedButton.AddListener(() => { self.ToggleButtons(true); self.SwitchPage( 0); });
            self.View.EBtnSeason_UnselectedButton.AddListener(() => { self.ToggleButtons(false);self.SwitchPage(1); });
        }

        //切换界面
        private static void SwitchPage(this DlgChallengeMode self, int pageIndex)
        {
            self.pageIndex = pageIndex;
            if (pageIndex == 0)
            {
                self.View.EPage_ChallengNormal.ShowPage();
                self.View.EPage_ChallengSeason.HidePage();
            }
            else
            {
                self.View.EPage_ChallengSeason.ShowPage();
                self.View.EPage_ChallengNormal.HidePage();
            }
        }

        //控制4个按钮的显影
        private static void ToggleButtons(this DlgChallengeMode self, bool isRegular)
        {
            self.View.EBtnRegularButton.SetVisible(isRegular);
            self.View.EBtnRegular_UnselectedButton.SetVisible(!isRegular);
            self.View.EBtnSeasonButton.SetVisible(!isRegular);
            self.View.EBtnSeason_UnselectedButton.SetVisible(isRegular);
        }


        //显示
        public static void ShowWindow(this DlgChallengeMode self, ShowWindowData contextData = null)
        {
            self.ShowBg().Coroutine();

            // 赛季按钮失活，常规按钮激活
            ToggleButtons(self, true);

            switch (self.pageIndex)
            {
                case 0:
                    self.View.EPage_ChallengNormal.ShowPage();
                    break;
                case 1:
                    self.View.EPage_ChallengSeason.ShowPage();
                    break;
            }
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

        //退出按钮
        public static async ETTask Back(this DlgChallengeMode self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Back);
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgChallengeMode>();
            await UIManagerHelper.EnterGameModeUI(self.DomainScene());
        }

    }
}