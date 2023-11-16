using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof (DlgGameModeAR))]
    public static class DlgGameModeARSystem
    {
        public static void RegisterUIEvent(this DlgGameModeAR self)
        {
            self.View.E_PVEButton.AddListenerAsync(self.EnterAREndlessChallenge);
            //self.View.E_PVEButton.AddListenerAsync(self.EnterARPVE);
            self.View.E_PVPButton.AddListenerAsync(self.EnterARPVP);
            self.View.E_ScanCodeButton.AddListenerAsync(self.EnterScanCode);
            self.View.E_AvatarButton.AddListenerAsync(self.ClickAvatar);
            self.View.E_tutorialButton.AddListenerAsync(self.ClickTutorial);
            self.View.E_ReturnLoginButton.AddListenerAsync(self.ReturnLogin);
            self.View.E_RankButton.AddListenerAsync(self.ClickRank);
        }

        public static void ShowWindow(this DlgGameModeAR self, ShowWindowData contextData = null)
        {
            self.ShowBg().Coroutine();
            self._ShowWindow().Coroutine();
        }

        public static async ETTask ShowBg(this DlgGameModeAR self)
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

        public static async ETTask _ShowWindow(this DlgGameModeAR self)
        {
            PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());
            self.View.E_PVENameUITextLocalizeMonoView.DynamicSet(playerBaseInfoComponent.EndlessChallengeScore);
            self.View.E_PlayerNameTextMeshProUGUI.text = playerBaseInfoComponent.PlayerName;

            await self.View.E_PlayerIcoImage.SetMyIcon(self.DomainScene());
        }

        public static async ETTask EnterAREndlessChallenge(this DlgGameModeAR self)
        {
            UIAudioManagerHelper.PlayUIAudioConfirm(self.DomainScene());

            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameModeAR>();

            DlgARHall_ShowWindowData _DlgARHall_ShowWindowData = new()
            {
                playerStatus = PlayerStatus.Hall,
                RoomType = RoomType.AR,
                SubRoomType = SubRoomType.AREndlessChallenge,
                arRoomId = 0,
            };
            await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData);
        }

        public static async ETTask EnterARPVE(this DlgGameModeAR self)
        {
            UIAudioManagerHelper.PlayUIAudioConfirm(self.DomainScene());

            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameModeAR>();

            DlgARHall_ShowWindowData _DlgARHall_ShowWindowData = new()
            {
                playerStatus = PlayerStatus.Hall,
                RoomType = RoomType.AR,
                SubRoomType = SubRoomType.ARPVE,
                arRoomId = 0,
            };
            await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData);
        }

        public static async ETTask EnterARPVP(this DlgGameModeAR self)
        {
            UIAudioManagerHelper.PlayUIAudioConfirm(self.DomainScene());

            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameModeAR>();

            DlgARHall_ShowWindowData _DlgARHall_ShowWindowData = new()
            {
                playerStatus = PlayerStatus.Hall,
                RoomType = RoomType.AR,
                SubRoomType = SubRoomType.ARPVP,
                arRoomId = 0,
            };
            await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData);
        }

        public static async ETTask EnterScanCode(this DlgGameModeAR self)
        {
            UIAudioManagerHelper.PlayUIAudioConfirm(self.DomainScene());

            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameModeAR>();

            DlgARHall_ShowWindowData _DlgARHall_ShowWindowData = new()
            {
                playerStatus = PlayerStatus.Hall,
                RoomType = RoomType.AR,
                SubRoomType = SubRoomType.ARScanCode,
                arRoomId = 0,
            };
            await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData);
        }

        public static async ETTask EnterTutorialFirst(this DlgGameModeAR self)
        {
            UIAudioManagerHelper.PlayUIAudioConfirm(self.DomainScene());

            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameModeAR>();

            DlgARHall_ShowWindowData _DlgARHall_ShowWindowData = new()
            {
                playerStatus = PlayerStatus.Hall,
                RoomType = RoomType.AR,
                SubRoomType = SubRoomType.ARTutorialFirst,
                arRoomId = 0,
            };
            await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData);
        }

        public static async ETTask ClickAvatar(this DlgGameModeAR self)
        {
            UIAudioManagerHelper.PlayUIAudioConfirm(self.DomainScene());
            await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgPersonalInformation>();
        }

        public static async ETTask ClickTutorial(this DlgGameModeAR self)
        {
            UIAudioManagerHelper.PlayUIAudioConfirm(self.DomainScene());

            Application.OpenURL("http://artd.corp.deepmirror.com/");
        }

        public static async ETTask ReturnLogin(this DlgGameModeAR self)
        {
            UIAudioManagerHelper.PlayUIAudioBack(self.DomainScene());

            PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());

            string msgTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_AreYouLeaving_Des", playerBaseInfoComponent.PlayerName);
            string sureTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_AreYouLeaving_Confirm");
            string cancelTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_AreYouLeaving_Cancel");
            string titleTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_AreYouLeaving_Title");
            ET.Client.UIManagerHelper.ShowConfirm(self.DomainScene(), msgTxt, () => { LoginHelper.LoginOut(self.ClientScene()).Coroutine(); }, null,
                sureTxt, cancelTxt, titleTxt);
        }

        public static async ETTask ClickRank(this DlgGameModeAR self)
        {
            await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgRankEndlessChallenge>();
        }
    }
}