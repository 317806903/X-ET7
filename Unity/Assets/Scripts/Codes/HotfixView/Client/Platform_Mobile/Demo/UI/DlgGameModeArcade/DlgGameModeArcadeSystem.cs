using System.Collections;
using System.Collections.Generic;
using System;
using ET.AbilityConfig;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Invoke(TimerInvokeType.GameModeArcadeTimer)]
    public class DlgGameModeArcadeTimer: ATimer<DlgGameModeArcade>
    {
        protected override void Run(DlgGameModeArcade self)
        {
            try
            {
                if (self.IsDisposed)
                {
                    TimerComponent.Instance?.Remove(ref self.Timer);
                    return;
                }

            }
            catch (Exception e)
            {
                Log.Error($"move timer error: {self.Id}\n{e}");
            }
        }
    }

    [FriendOf(typeof (DlgGameModeArcade))]
    public static class DlgGameModeArcadeSystem
    {
        public static void RegisterUIEvent(this DlgGameModeArcade self)
        {
            self.View.E_EndlessChallengeButton.AddListenerAsync(self.EnterAREndlessChallenge);
            self.View.E_MasterScanMeshButton.AddListenerAsync(self.EnterScanMesh);
            self.View.E_PVPButton.AddListenerAsync(self.EnterARPVP);
            self.View.E_ScanCodeButton.AddListenerAsync(self.EnterScanCode);

            //self.View.E_AvatarButton.AddListenerAsync(self.ClickAvatar);
            self.View.ES_AvatarShow.ClickAvatarIconBtn(self.ClickAvatar);

            self.View.E_BagsButton.AddListenerAsync(self.ClickBags);
            self.View.E_BattleDeckButton.AddListenerAsync(self.ClickBattleDeck);
            self.View.E_RankButton.AddListenerAsync(self.ClickRank);
            self.View.E_TutorialButton.AddListenerAsync(self.ClickTutorial);
            self.View.E_GameSettingButton.AddListenerAsync(self.GameSetting);
        }

        public static async ETTask ShowWindow(this DlgGameModeArcade self, ShowWindowData contextData = null)
        {
            if (ET.Client.UIManagerHelper.ChkIsAR() == false)
            {
                self.isAR = false;
            }
            else
            {
                self.isAR = true;
            }

            self.ShowBg().Coroutine();
            self.ShowFunctionMenuLock().Coroutine();
            self._ShowWindow().Coroutine();
        }

        public static async ETTask ShowBg(this DlgGameModeArcade self)
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

        public static async ETTask _ShowWindow(this DlgGameModeArcade self)
        {
            self.View.E_RedDotImage.SetVisible(false);
            PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());
            self.View.E_PlayerNameTextMeshProUGUI.text = playerBaseInfoComponent.PlayerName;
            await self.View.E_PlayerIcoImage.SetMyIcon(self.DomainScene());

            self.Timer = TimerComponent.Instance.NewRepeatedTimer(1000, TimerInvokeType.            GameModeArcadeTimer, self);
            RankShowComponent rankShowComponent = await ET.Client.RankHelper.GetRankShow(self.DomainScene(), RankType.EndlessChallenge, false);
            (int myRank, long score) = rankShowComponent.GetMyRank();
            self.View.ELabel_WavesUITextLocalizeMonoView.DynamicSet(score);
            if (myRank == -1 || myRank > 1000)
            {
                self.View.ELabel_RankUITextLocalizeMonoView.DynamicSet("1000+");
            }
            else
            {
                self.View.ELabel_RankUITextLocalizeMonoView.DynamicSet(myRank);
            }

            self.View.ES_AvatarShow.ShowMyAvatarIcon().Coroutine();

            await self.UpdatePhysical();
        }

        public static async ETTask ShowFunctionMenuLock(this DlgGameModeArcade self)
        {
            Transform transformLock = self.View.E_AvatarButton.transform.Find("icon_Lock");
            await UIManagerHelper.ShowFunctionMenuLockOne(self.DomainScene(), "FunctionMenu_PersonInfo", transformLock);

            transformLock = self.View.E_BagsButton.transform.Find("icon_Lock");
            await UIManagerHelper.ShowFunctionMenuLockOne(self.DomainScene(), "FunctionMenu_BackPack", transformLock);

            transformLock = self.View.E_BattleDeckButton.transform.Find("icon_Lock");
            await UIManagerHelper.ShowFunctionMenuLockOne(self.DomainScene(), "FunctionMenu_BattleDeck", transformLock);

            transformLock = self.View.E_RankButton.transform.Find("icon_Lock");
            await UIManagerHelper.ShowFunctionMenuLockOne(self.DomainScene(), "FunctionMenu_Rank", transformLock);

            transformLock = self.View.E_TutorialButton.transform.Find("icon_Lock");
            await UIManagerHelper.ShowFunctionMenuLockOne(self.DomainScene(), "FunctionMenu_Tutorial", transformLock);

            transformLock = self.View.E_EndlessChallengeButton.transform.Find("icon_Lock");
            await UIManagerHelper.ShowFunctionMenuLockOne(self.DomainScene(), "FunctionMenu_AREndlessChallenge", transformLock);

            transformLock = self.View.E_PVPButton.transform.Find("icon_Lock");
            await UIManagerHelper.ShowFunctionMenuLockOne(self.DomainScene(), "FunctionMenu_ARPVP", transformLock);
        }

        public static async ETTask<FunctionMenuStatus> GetMyFunctionMenuOne(this DlgGameModeArcade self, string functionMenuCfgId)
        {
            PlayerFunctionMenuComponent playerFunctionMenuComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerFunctionMenu(self.DomainScene());
            FunctionMenuStatus functionMenuStatus = playerFunctionMenuComponent.GetStatus(functionMenuCfgId);
            return functionMenuStatus;
        }

        public static async ETTask EnterAREndlessChallenge(this DlgGameModeArcade self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

            if (self.isAR)
            {
                if (string.IsNullOrEmpty(ARSessionHelper.GetArcadeARSceneId()))
                {
                    string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_ArcadeARSceneId_NotExist");
                    ET.Client.UIManagerHelper.ShowOnlyConfirm(self.DomainScene(), msg, () =>
                    {
                    });
                    return;
                }

                UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameModeArcade>();

                RoomType roomType = RoomType.AR;
                SubRoomType subRoomType = SubRoomType.AREndlessChallenge;
                RoomTypeInfo roomTypeInfo = ET.GamePlayHelper.GetRoomTypeInfo(roomType, subRoomType);
                DlgARHall_ShowWindowData _DlgARHall_ShowWindowData = new()
                {
                    ARHallType = ARHallType.CreateRoomWithARSceneId,
                    roomTypeInfo = roomTypeInfo,
                    arSceneId = ARSessionHelper.GetArcadeARSceneId(),
                    arSceneMeshId = ARSessionHelper.GetArcadeARSceneMeshId(),
                };
                await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData);
            }
            else
            {
                RoomType roomType = RoomType.Normal;
                SubRoomType subRoomType = SubRoomType.NormalEndlessChallenge;
                RoomTypeInfo roomTypeInfo = ET.GamePlayHelper.GetRoomTypeInfo(roomType, subRoomType);
                (bool result, long roomId) = await RoomHelper.CreateRoomAsync(self.ClientScene(), roomTypeInfo);
                if (result)
                {
                    UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameModeArcade>();

                    await ET.Client.UIManagerHelper.EnterRoomUI(self.DomainScene());
                }
            }
        }

        public static async ETTask EnterARPVP(this DlgGameModeArcade self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

            if (self.isAR)
            {
                if (string.IsNullOrEmpty(ARSessionHelper.GetArcadeARSceneId()))
                {
                    string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_ArcadeARSceneId_NotExist");
                    ET.Client.UIManagerHelper.ShowOnlyConfirm(self.DomainScene(), msg, () =>
                    {
                    });
                    return;
                }

                UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameModeArcade>();

                RoomType roomType = RoomType.AR;
                SubRoomType subRoomType = SubRoomType.ARPVP;
                RoomTypeInfo roomTypeInfo = ET.GamePlayHelper.GetRoomTypeInfo(roomType, subRoomType);
                DlgARHall_ShowWindowData _DlgARHall_ShowWindowData = new()
                {
                    ARHallType = ARHallType.CreateRoomWithARSceneId,
                    roomTypeInfo = roomTypeInfo,
                    arSceneId = ARSessionHelper.GetArcadeARSceneId(),
                    arSceneMeshId = ARSessionHelper.GetArcadeARSceneMeshId(),
                };
                await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData);
            }
            else
            {
                RoomType roomType = RoomType.Normal;
                SubRoomType subRoomType = SubRoomType.NormalPVP;
                RoomTypeInfo roomTypeInfo = ET.GamePlayHelper.GetRoomTypeInfo(roomType, subRoomType);
                (bool result, long roomId) = await RoomHelper.CreateRoomAsync(self.ClientScene(), roomTypeInfo);
                if (result)
                {
                    UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameModeArcade>();

                    await ET.Client.UIManagerHelper.EnterRoomUI(self.DomainScene());
                }
            }
        }

        public static async ETTask EnterScanCode(this DlgGameModeArcade self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameModeArcade>();

            RoomType roomType = RoomType.AR;
            SubRoomType subRoomType = SubRoomType.ARScanCode;
            RoomTypeInfo roomTypeInfo = ET.GamePlayHelper.GetRoomTypeInfo(roomType, subRoomType);
            DlgARHall_ShowWindowData _DlgARHall_ShowWindowData = new()
            {
                ARHallType = ARHallType.ScanQRCode,
                roomTypeInfo = roomTypeInfo,
                roomId = 0,
            };
            await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData);
        }

        public static async ETTask EnterScanMesh(this DlgGameModeArcade self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

            string msgStr = LocalizeComponent.Instance.GetTextValue("TextCode_Key_ArcadeScanMesh_Tip");
            string passwordStr = GlobalSettingCfgCategory.Instance.GameModeArcadeMasterPassword;
            string titleStr = null;
            Action SureBtnCallBak = async () =>
            {
                UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameModeArcade>();

                if (self.isAR)
                {
                    RoomType roomType = RoomType.AR;
                    SubRoomType subRoomType = SubRoomType.ArcadeScanMesh;
                    RoomTypeInfo roomTypeInfo = ET.GamePlayHelper.GetRoomTypeInfo(roomType, subRoomType);
                    DlgARHall_ShowWindowData _DlgARHall_ShowWindowData = new()
                    {
                        ARHallType = ARHallType.CreateRoomWithOutARSceneId,
                        roomTypeInfo = roomTypeInfo,
                    };
                    await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData);
                }
                else
                {
                    RoomType roomType = RoomType.Normal;
                    SubRoomType subRoomType = SubRoomType.NormalScanMesh;
                    RoomTypeInfo roomTypeInfo = ET.GamePlayHelper.GetRoomTypeInfo(roomType, subRoomType);
                    (bool result, long roomId) = await RoomHelper.CreateRoomAsync(self.ClientScene(), roomTypeInfo);
                    if (result)
                    {
                        await ET.Client.UIManagerHelper.EnterRoomUI(self.DomainScene());
                    }
                }
            };

            ET.Client.UIManagerHelper.ShowPassword(self.DomainScene(), msgStr, passwordStr, SureBtnCallBak, titleStr);
        }

        public static async ETTask ClickAvatar(this DlgGameModeArcade self)
        {
            self.TrackFunctionClicked("personalInfo");
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

            string msgStr = LocalizeComponent.Instance.GetTextValue("TextCode_Key_ArcadePersonalInfo_Tip");
            string passwordStr = GlobalSettingCfgCategory.Instance.GameModeArcadeMasterPassword;
            string titleStr = null;
            Action SureBtnCallBak = async () =>
            {
                await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgPersonalInformation>();
            };

            ET.Client.UIManagerHelper.ShowPassword(self.DomainScene(), msgStr, passwordStr, SureBtnCallBak, titleStr);
        }

        public static async ETTask ClickTutorial(this DlgGameModeArcade self)
        {
            self.TrackFunctionClicked("tutorial");
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

            await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgTutorials>();
        }

        public static async ETTask GameSetting(this DlgGameModeArcade self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(),SoundEffectType.Back);

            UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgGameModeSetting>().Coroutine();
        }

        public static async ETTask ClickRank(this DlgGameModeArcade self)
        {
            self.TrackFunctionClicked("rank");
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameModeArcade>();
            await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgRankEndlessChallenge>();
        }

        public static async ETTask ClickBags(this DlgGameModeArcade self)
        {
            self.TrackFunctionClicked("bag");
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameModeArcade>();
            await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBag>();
        }

        public static async ETTask ClickBattleDeck(this DlgGameModeArcade self)
        {
            self.TrackFunctionClicked("battleDeck");
            //UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameModeArcade>();
            await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBattleDeck>();
        }

        public static void ClickDiscord(this DlgGameModeArcade self)
        {
            self.TrackFunctionClicked("discord");
            string url = ChannelSettingComponent.Instance.GetDiscordURL();
            ET.Client.UIManagerHelper.ShowUrl(self.DomainScene(), url);
        }

        public static async ETTask RefreshWhenBaseInfoChg(this DlgGameModeArcade self)
        {
            await self.UpdatePhysical();
        }

        public static async ETTask UpdatePhysical(this DlgGameModeArcade self)
        {
            self.View.ELabel_EndlessPhysicalStrengthTextMeshProUGUI.transform.parent.SetVisible(true);
            self.View.ELabel_PVPPhysicalStrengthTextMeshProUGUI.transform.parent.SetVisible(true);

            self.View.ELabel_PVPPhysicalStrengthTextMeshProUGUI.ShowTokenArcadeCoinCostText(self.DomainScene(), GlobalSettingCfgCategory.Instance.GameModeArcadePVPCostWhenStart).Coroutine();
            self.View.ELabel_EndlessPhysicalStrengthTextMeshProUGUI.ShowTokenArcadeCoinCostText(self.DomainScene(), GlobalSettingCfgCategory.Instance.GameModeArcadeEndlessChallengeCostWhenStart).Coroutine();
        }

        public static async ETTask RefreshWhenFunctionMenuChg(this DlgGameModeArcade self)
        {
            await self.ShowFunctionMenuLock();
        }

        public static void HideWindow(this DlgGameModeArcade self)
        {
            TimerComponent.Instance?.Remove(ref self.Timer);
        }

        public static void TrackFunctionClicked(this DlgGameModeArcade self, string name){
            EventSystem.Instance.Publish(self.DomainScene(), new ClientEventType.NoticeEventLogging()
            {
                eventName = "FunctionClicked",
                properties = new()
                {
                    {"function_name", name},
                }
            });
        }
    }
}