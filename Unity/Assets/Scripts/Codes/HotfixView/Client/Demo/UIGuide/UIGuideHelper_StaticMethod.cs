using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using UnityEngine;
using UIGuide;

namespace ET.Client
{
    public static class UIGuideHelper_StaticMethod
    {
        public static long recordTime;

        public static async ETTask<bool> ChkTowerPutSuccess(UIGuideStepComponent guideStepComponent)
        {
            guideStepComponent.guideConditionStatus.TryGetValue(GuideConditionStaticMethodType.ChkTowerPutSuccess, out bool status);
            return status;
        }

        public static async ETTask<bool> ChkTowerScaleSuccess(UIGuideStepComponent guideStepComponent)
        {
            guideStepComponent.guideConditionStatus.TryGetValue(GuideConditionStaticMethodType.ChkTowerScaleSuccess, out bool status);
            return status;
        }

        public static async ETTask<bool> ChkTowerReclaimSuccess(UIGuideStepComponent guideStepComponent)
        {
            guideStepComponent.guideConditionStatus.TryGetValue(GuideConditionStaticMethodType.ChkTowerReclaimSuccess, out bool status);
            return status;
        }

        public static async ETTask<bool> ChkTowerUpgradeSuccess(UIGuideStepComponent guideStepComponent)
        {
            guideStepComponent.guideConditionStatus.TryGetValue(GuideConditionStaticMethodType.ChkTowerUpgradeSuccess, out bool status);
            return status;
        }

        public static async ETTask<bool> ChkTowerMoveSuccess(UIGuideStepComponent guideStepComponent)
        {
            guideStepComponent.guideConditionStatus.TryGetValue(GuideConditionStaticMethodType.ChkTowerMoveSuccess, out bool status);
            return status;
        }

        public static async ETTask<bool> ChkWaitTime(Scene scene, string param)
        {
            await ETTask.CompletedTask;
            if (!float.TryParse(param, out float waitTime))
            {
                return true;
            }

            if (recordTime == 0)
            {
                recordTime = TimeHelper.ServerNow() + (long)(waitTime * 1000);
            }

            if (recordTime > TimeHelper.ServerNow())
            {
                return false;
            }

            recordTime = 0;

            return true;
        }

        public static async ETTask<bool> ChkARMeshShow(Scene scene, string param)
        {
            await ETTask.CompletedTask;

            return true;
        }

        public static async ETTask<bool> ChkIsNotShowStory(Scene scene)
        {
            DlgBeginnersGuideStory _DlgBeginnersGuideStory = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgBeginnersGuideStory>(true);
            if (_DlgBeginnersGuideStory == null)
            {
                return true;
            }

            return false;
        }

        public static async ETTask<bool> ChkIsNotShowVideo(Scene scene)
        {
            DlgTutorials _DlgVideoShow = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgTutorials>(true);
            if (_DlgVideoShow != null)
            {
                return false;
            }

            DlgTutorialOne _DlgTutorialOne = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgTutorialOne>(true);
            if (_DlgTutorialOne != null)
            {
                return false;
            }

            return true;
        }

        //-----------------------------------------------------------------------------------------------

        public static async ETTask ShowStory(Scene scene)
        {
            await UIManagerHelper.GetUIComponent(scene)
                .ShowWindowAsync<DlgBeginnersGuideStory>(new DlgBeginnersGuideStory_ShowWindowData() { finishCallBack = null, });
        }

        public static async ETTask ShowVideo(Scene scene, string tutorialCfgId)
        {
            if (string.IsNullOrEmpty(tutorialCfgId))
            {
                await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgTutorials>();
            }
            else
            {
                DlgTutorialOne_ShowWindowData _DlgTutorialOne_ShowWindowData = new();
                _DlgTutorialOne_ShowWindowData.tutorialCfgId = tutorialCfgId;
                await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgTutorialOne>(_DlgTutorialOne_ShowWindowData);
            }
        }

        public static async ETTask EnterGuideBattleTutorialFirst(Scene scene)
        {
            UIManagerHelper.GetUIComponent(scene).HideAllShownWindow();

            RoomType roomType = RoomType.AR;
            SubRoomType subRoomType = SubRoomType.ARTutorialFirst;
            RoomTypeInfo roomTypeInfo = ET.GamePlayHelper.GetRoomTypeInfo(roomType, subRoomType);
            DlgARHall_ShowWindowData _DlgARHall_ShowWindowData = new()
            {
                ARHallType = ARHallType.CreateRoomWithOutARSceneId,
                roomTypeInfo = roomTypeInfo,
                roomId = 0,
            };
            await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData);
        }

        public static async ETTask EnterGuideBattlePVEFirst(Scene scene)
        {
            UIManagerHelper.GetUIComponent(scene).HideAllShownWindow();

            RoomType roomType = RoomType.AR;
            SubRoomType subRoomType = SubRoomType.ARPVE;
            RoomTypeInfo roomTypeInfo = ET.GamePlayHelper.GetRoomTypeInfo(roomType, subRoomType, -1, 1);
            DlgARHall_ShowWindowData _DlgARHall_ShowWindowData = new()
            {
                ARHallType = ARHallType.CreateRoomWithOutARSceneId,
                roomTypeInfo = roomTypeInfo,
                roomId = 0,
            };
            await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData);
        }

        public static async ETTask ShowPointTower(Scene scene)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(scene);
            if (gamePlayTowerDefenseComponent == null)
            {
                return;
            }

            PlayerOwnerTowersComponent playerOwnerTowersComponent = gamePlayTowerDefenseComponent.GetComponent<PlayerOwnerTowersComponent>();
            if (playerOwnerTowersComponent == null)
            {
                return;
            }

            long myPlayerId = PlayerStatusHelper.GetMyPlayerId(scene);
            if (playerOwnerTowersComponent.playerId2unitTowerId.ContainsKey(myPlayerId) == false)
            {
                return;
            }

            UnitComponent unitComponent = ET.Client.UnitHelper.GetUnitComponent(scene);
            foreach (var unitId in playerOwnerTowersComponent.playerId2unitTowerId[myPlayerId])
            {
                bool bUnitExist = await ET.Client.UnitHelper.ChkUnitExist(playerOwnerTowersComponent, unitId);
                if (bUnitExist == false)
                {
                    continue;
                }
                Unit unit = unitComponent.Get(unitId);

                bool bRet = await ET.Client.UnitViewHelper.ChkGameObjectShowReady(playerOwnerTowersComponent, unit);
                if (bRet == false)
                {
                    return;
                }

                unit.AddComponent<PointTowerComponent>();
                break;
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask HidePointTower(Scene scene)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(scene);
            if (gamePlayTowerDefenseComponent == null)
            {
                return;
            }

            PlayerOwnerTowersComponent playerOwnerTowersComponent = gamePlayTowerDefenseComponent.GetComponent<PlayerOwnerTowersComponent>();
            if (playerOwnerTowersComponent == null)
            {
                return;
            }

            long myPlayerId = PlayerStatusHelper.GetMyPlayerId(scene);
            if (playerOwnerTowersComponent.playerId2unitTowerId.ContainsKey(myPlayerId) == false)
            {
                return;
            }

            UnitComponent unitComponent = ET.Client.UnitHelper.GetUnitComponent(scene);
            foreach (var unitId in playerOwnerTowersComponent.playerId2unitTowerId[myPlayerId])
            {
                bool bUnitExist = await ET.Client.UnitHelper.ChkUnitExist(playerOwnerTowersComponent, unitId);
                if (bUnitExist == false)
                {
                    continue;
                }
                Unit unit = unitComponent.Get(unitId);

                bool bRet = await ET.Client.UnitViewHelper.ChkGameObjectShowReady(playerOwnerTowersComponent, unit);
                if (bRet == false)
                {
                    return;
                }

                unit.RemoveComponent<PointTowerComponent>();
                break;
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask HideTowerInfo(Scene scene)
        {
            UIManagerHelper.GetUIComponent(scene).HideWindow<DlgBattleTowerHUD>();
            await ETTask.CompletedTask;
        }

        public static async ETTask ShowBattleTowerReady(Scene scene, bool isShow)
        {
            DlgBattleTowerAR dlgBattleTowerAR = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgBattleTowerAR>();
            if (dlgBattleTowerAR != null)
            {
                dlgBattleTowerAR.View.EG_ReadyRectTransform.SetVisible(isShow);
            }

            DlgBattleTower dlgBattleTower = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgBattleTower>();
            if (dlgBattleTower != null)
            {
                dlgBattleTower.View.EG_ReadyRectTransform.SetVisible(isShow);
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask ShowBattleTowerQuit(Scene scene, bool isShow)
        {
            DlgBattleTowerAR dlgBattleTowerAR = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgBattleTowerAR>();
            if (dlgBattleTowerAR != null)
            {
                dlgBattleTowerAR.View.E_GameSettingButton.SetVisible(isShow);
            }

            DlgBattleTower dlgBattleTower = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgBattleTower>();
            if (dlgBattleTower != null)
            {
                dlgBattleTower.View.E_GameSettingButton.SetVisible(isShow);
            }

            await ETTask.CompletedTask;
        }

        public static void ShowScanQuit(Scene scene, bool isShow)
        {
            ARSessionComponent arSessionComponent = ET.Client.ARSessionHelper.GetARSession(scene);
            arSessionComponent.ShowQuit(isShow);
        }

        public static async ETTask ShowScanVideo(Scene scene, bool isShow)
        {
            if (isShow)
            {
                await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgVideoShowSmall>();
            }
            else
            {
                DlgVideoShowSmall _DlgVideoShowSmall = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgVideoShowSmall>(true);
                _DlgVideoShowSmall?.Stop();
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask BackToGameModeAR(Scene scene)
        {
            UIAudioManagerHelper.PlayUIAudio(scene, SoundEffectType.Click);

            await RoomHelper.MemberReturnRoomFromBattleAsync(scene);
            await RoomHelper.QuitRoomAsync(scene);
            await SceneHelper.EnterHall(scene, false, false);

            await ETTask.CompletedTask;
        }
    }
}