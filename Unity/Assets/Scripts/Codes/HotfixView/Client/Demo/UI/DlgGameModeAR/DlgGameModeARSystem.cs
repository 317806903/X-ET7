using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Xml.Schema;
using ET.AbilityConfig;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Invoke(TimerInvokeType.GameModeARTimer)]
    public class DlgGameModeARTimer: ATimer<DlgGameModeAR>
    {
        protected override void Run(DlgGameModeAR self)
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

    [FriendOf(typeof (DlgGameModeAR))]
    public static class DlgGameModeARSystem
    {
        public static void RegisterUIEvent(this DlgGameModeAR self)
        {
            self.View.E_EndlessChallengeButton.AddListenerAsync(self.EnterAREndlessChallenge);
            self.View.E_PVEButton.AddListenerAsync(self.EnterARPVE);
            self.View.E_PVPButton.AddListenerAsync(self.EnterARPVP);
            self.View.E_ScanCodeButton.AddListenerAsync(self.EnterScanCode);

            self.View.ES_AvatarShow.ClickAvatarIconBtn(self.ClickAvatar);

            self.View.E_BagsButton.AddListenerAsync(self.ClickBags);
            self.View.E_BattleDeckButton.AddListenerAsync(self.ClickBattleDeck);
            self.View.E_SeasonButton.AddListenerAsync(self.ClickRank);
            self.View.E_SettingButton.AddListenerAsync(self.GameSetting);

            self.View.E_BtnMailButton.AddListenerAsync(async () =>
            {
                await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgMail>();
            });

            //添加问卷按钮点击事件
            self.View.E_QuestionnaireButton.AddListenerAsync(self.ClickQuerstionare);
        }

        public static async ETTask ShowWindow(this DlgGameModeAR self, ShowWindowData contextData = null)
        {
#if UNITY_EDITOR
            self.isAR = false;
#else
			self.isAR = true;
#endif

            self.ShowBg().Coroutine();
            self.ShowFunctionMenuLock().Coroutine();
            self._ShowWindow().Coroutine();
            self.ChkNeedShowSeasonChg().Coroutine();
            self.ChkNeedShowGuide().Coroutine();


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

        public static async ETTask ChkNeedShowSeasonChg(this DlgGameModeAR self)
        {
            if (ET.SceneHelper.ChkIsDemoShow())
            {
                return;
            }

            PlayerBaseInfoComponent playerBaseInfoComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());
            if (playerBaseInfoComponent.seasonIndex != SeasonHelper.GetSeasonIndex(self.DomainScene()))
            {

                await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgSeasonNotice>();

                playerBaseInfoComponent.seasonIndex = SeasonHelper.GetSeasonIndex(self.DomainScene());
                playerBaseInfoComponent.seasonCfgId = SeasonHelper.GetSeasonCfgId(self.DomainScene());

                await ET.Client.PlayerCacheHelper.SaveMyPlayerModel(self.DomainScene(), PlayerModelType.BaseInfo, new (){"seasonIndex", "seasonCfgId"});

                UIRedDotHelper.ShowRedDotNode(self.DomainScene(), UIRedDotType.PVESeason);
                await ET.Client.PlayerCacheHelper.SetUIRedDotType(self.DomainScene(), UIRedDotType.PVESeason, true);
            }
        }

        public static async ETTask ChkNeedShowGuide(this DlgGameModeAR self)
        {
            if (ET.SceneHelper.ChkIsDemoShow())
            {
                return;
            }

            await ET.Client.FunctionMenu.ChkNeedShowFunctionMenuGuide(self.DomainScene());
        }

        public static async ETTask _ShowWindow(this DlgGameModeAR self)
        {
            PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());

            self.Timer = TimerComponent.Instance.NewRepeatedTimer(1000, TimerInvokeType.GameModeARTimer, self);

            await self.RefreshWhenSeasonRemainChg();

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

        public static async ETTask ShowFunctionMenuLock(this DlgGameModeAR self)
        {
            Transform transformLock = null;
            // transformLock = self.View.E_AvatarButton.transform.Find("icon_Lock");
            // await UIManagerHelper.ShowFunctionMenuLockOne(self.DomainScene(), "FunctionMenu_PersonInfo", transformLock);

            transformLock = self.View.E_BagsButton.transform.Find("icon_Lock");
            await UIManagerHelper.ShowFunctionMenuLockOne(self.DomainScene(), "FunctionMenu_BackPack", transformLock);

            transformLock = self.View.E_BattleDeckButton.transform.Find("icon_Lock");
            await UIManagerHelper.ShowFunctionMenuLockOne(self.DomainScene(), "FunctionMenu_BattleDeck", transformLock);

            transformLock = self.View.E_SeasonButton.transform.Find("icon_Lock");
            await UIManagerHelper.ShowFunctionMenuLockOne(self.DomainScene(), "FunctionMenu_Rank", transformLock);


            transformLock = self.View.E_PVEButton.transform.Find("icon_Lock");
            await UIManagerHelper.ShowFunctionMenuLockOne(self.DomainScene(), "FunctionMenu_ARPVE", transformLock);

            transformLock = self.View.E_EndlessChallengeButton.transform.Find("icon_Lock");
            await UIManagerHelper.ShowFunctionMenuLockOne(self.DomainScene(), "FunctionMenu_AREndlessChallenge", transformLock);

            transformLock = self.View.E_PVPButton.transform.Find("icon_Lock");
            await UIManagerHelper.ShowFunctionMenuLockOne(self.DomainScene(), "FunctionMenu_ARPVP", transformLock);
        }

        public static async ETTask<FunctionMenuStatus> GetMyFunctionMenuOne(this DlgGameModeAR self, string functionMenuCfgId)
        {
            PlayerFunctionMenuComponent playerFunctionMenuComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerFunctionMenu(self.DomainScene());
            FunctionMenuStatus functionMenuStatus = playerFunctionMenuComponent.GetStatus(functionMenuCfgId);
            return functionMenuStatus;
        }

        public static async ETTask EnterAREndlessChallenge(this DlgGameModeAR self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

            if (await ET.Client.UIManagerHelper.ChkPhsicalAndShowtip(self.DomainScene(), ET.GamePlayHelper.GetPhysicalCostEndlessChallenge()) == false)
            {
                return;
            }

            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameModeAR>();

            int seasonCfgId = ET.Client.SeasonHelper.GetSeasonCfgId(self.DomainScene());
            if (self.isAR)
            {
                RoomType roomType = RoomType.AR;
                SubRoomType subRoomType = SubRoomType.AREndlessChallenge;
                RoomTypeInfo roomTypeInfo = ET.GamePlayHelper.GetRoomTypeInfo(roomType, subRoomType, seasonCfgId);
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
                RoomType roomType = RoomType.Normal;
                SubRoomType subRoomType = SubRoomType.NormalEndlessChallenge;
                RoomTypeInfo roomTypeInfo = ET.GamePlayHelper.GetRoomTypeInfo(roomType, subRoomType, seasonCfgId);
                (bool result, long roomId) = await RoomHelper.CreateRoomAsync(self.ClientScene(), roomTypeInfo);
                if (result)
                {
                    await ET.Client.UIManagerHelper.EnterRoomUI(self.DomainScene());
                }
            }
        }

        public static async ETTask EnterARPVE(this DlgGameModeAR self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameModeAR>();

            await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgChallengeMode>();
        }

        public static async ETTask EnterARPVP(this DlgGameModeAR self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

            bool bRet = await ET.Client.UIManagerHelper.ChkPhsicalAndShowtip(self.DomainScene(), ET.GamePlayHelper.GetPhysicalCostPVP());
            if (bRet == false)
            {
                return;
            }

            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameModeAR>();

            if (self.isAR)
            {
                RoomType roomType = RoomType.AR;
                SubRoomType subRoomType = SubRoomType.ARPVP;
                RoomTypeInfo roomTypeInfo = ET.GamePlayHelper.GetRoomTypeInfo(roomType, subRoomType);
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
                RoomType roomType = RoomType.Normal;
                SubRoomType subRoomType = SubRoomType.NormalPVP;
                RoomTypeInfo roomTypeInfo = ET.GamePlayHelper.GetRoomTypeInfo(roomType, subRoomType);
                (bool result, long roomId) = await RoomHelper.CreateRoomAsync(self.ClientScene(), roomTypeInfo);
                if (result)
                {
                    await ET.Client.UIManagerHelper.EnterRoomUI(self.DomainScene());
                }
            }
        }

        public static async ETTask EnterScanCode(this DlgGameModeAR self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);
#if UNITY_EDITOR
            return;
#endif
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameModeAR>();

            RoomType roomType = RoomType.AR;
            SubRoomType subRoomType = SubRoomType.ARScanCode;
            RoomTypeInfo roomTypeInfo = ET.GamePlayHelper.GetRoomTypeInfo(roomType, subRoomType);
            DlgARHall_ShowWindowData _DlgARHall_ShowWindowData = new()
            {
                ARHallType = ARHallType.ScanQRCode,
                roomId = 0,
                roomTypeInfo = roomTypeInfo,
            };
            await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData);
        }


        public static async ETTask ClickAvatar(this DlgGameModeAR self)
        {
            self.TrackFunctionClicked("personalInfo");
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);
            await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgPersonalInformation>();
        }



        public static async ETTask ClickRank(this DlgGameModeAR self)
        {
            self.TrackFunctionClicked("rank");
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameModeAR>();
            await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgRankPowerupSeason>();
        }

        public static async ETTask ClickBags(this DlgGameModeAR self)
        {
            self.TrackFunctionClicked("bag");
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameModeAR>();
            await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBag>();
        }

        public static async ETTask ClickBattleDeck(this DlgGameModeAR self)
        {
            self.TrackFunctionClicked("battleDeck");
            //UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameModeAR>();
            await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBattleDeck>();
        }

        public static async ETTask ClickQuerstionare(this DlgGameModeAR self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);
            UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindow<DlgQuestionnaire>();
            await ETTask.CompletedTask;
        }

        public static async ETTask GameSetting(this DlgGameModeAR self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(),SoundEffectType.Back);

            UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgGameModeSetting>().Coroutine();
        }

        public static async ETTask RefreshWhenBaseInfoChg(this DlgGameModeAR self)
        {
            await self.UpdatePhysical();
        }

        public static async ETTask UpdatePhysical(this DlgGameModeAR self)
        {

            self.View.ELabel_PVEPhysicalStrengthTextMeshProUGUI.transform.parent.SetVisible(GlobalSettingCfgCategory.Instance.PhysicalStrengthShow);
            self.View.ELabel_EndlessPhysicalStrengthTextMeshProUGUI.transform.parent.SetVisible(GlobalSettingCfgCategory.Instance.PhysicalStrengthShow);
            self.View.ELabel_PVPPhysicalStrengthTextMeshProUGUI.transform.parent.SetVisible(GlobalSettingCfgCategory.Instance.PhysicalStrengthShow);

            self.View.ELabel_PVEPhysicalStrengthTextMeshProUGUI.ShowPhysicalCostText(self.DomainScene(), ET.GamePlayHelper.GetPhysicalCostPVE()).Coroutine();
            self.View.ELabel_PVPPhysicalStrengthTextMeshProUGUI.ShowPhysicalCostText(self.DomainScene(), ET.GamePlayHelper.GetPhysicalCostPVP()).Coroutine();
            self.View.ELabel_EndlessPhysicalStrengthTextMeshProUGUI.ShowPhysicalCostText(self.DomainScene(), ET.GamePlayHelper.GetPhysicalCostEndlessChallenge()).Coroutine();
        }

        public static async ETTask RefreshWhenFunctionMenuChg(this DlgGameModeAR self)
        {
            await self.ShowFunctionMenuLock();
            await self.ChkNeedShowGuide();
        }

        public static async ETTask RefreshWhenSeasonRemainChg(this DlgGameModeAR self)
        {
            string textTime = ET.Client.SeasonHelper.GetSeasonLeftTime(self.DomainScene());
            self.View.E_SeasonLeftTimeTextMeshProUGUI.text = textTime;
        }

        public static async ETTask RefreshWhenSeasonIndexChg(this DlgGameModeAR self)
        {
            await self.ChkNeedShowSeasonChg();
        }

        public static void HideWindow(this DlgGameModeAR self)
        {
            TimerComponent.Instance?.Remove(ref self.Timer);
        }

        public static void TrackFunctionClicked(this DlgGameModeAR self, string name){
            EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
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