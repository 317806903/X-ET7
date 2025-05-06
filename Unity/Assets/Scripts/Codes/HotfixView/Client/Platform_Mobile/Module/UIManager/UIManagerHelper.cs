using System;
using System.Collections.Generic;
using DynamicAtlas;
using ET.AbilityConfig;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    public static partial class UIManagerHelper
    {
        public static bool ChkIsOVRCamera()
        {
            return false;
        }

        public static bool ChkIsAR()
        {
            return Application.isMobilePlatform;
        }

        public static bool ChkIsDebug()
        {
            return Application.isMobilePlatform == false;
        }

        public static void ShowItemInfoWnd(Scene scene, string itemCfgId, Vector3 pos, bool isShowStatus, bool isLock)
        {
            if (string.IsNullOrEmpty(itemCfgId))
            {
                return;
            }
            if (ET.ItemHelper.ChkIsTower(itemCfgId))
            {
                ShowTowerDetails(scene, itemCfgId, isShowStatus, isLock);
            }
            else if (ET.ItemHelper.ChkIsSkill(itemCfgId))
            {
                ShowSkillDetails(scene, itemCfgId, isShowStatus, isLock);
            }
            else
            {
                _ShowSimpleItemWnd(scene, itemCfgId, pos);
            }
        }

        public static void ShowTowerDetails(Scene scene, string itemCfgId, bool isShowStatus, bool isLock)
        {
            if (string.IsNullOrEmpty(itemCfgId))
            {
                return;
            }
            if (ET.ItemHelper.ChkIsTower(itemCfgId) == false)
            {
                return;
            }

            ShowData_DlgTowerDetails _ShowData_DlgTowerDetails= new ();
            _ShowData_DlgTowerDetails.towerCfgId = itemCfgId;
            _ShowData_DlgTowerDetails.isShowStatus = isShowStatus;
            _ShowData_DlgTowerDetails.isLock = isLock;
            UIManagerHelper.GetUIComponent(scene).ShowWindow<DlgTowerDetails>(_ShowData_DlgTowerDetails);
        }

        public static void _ShowSimpleItemWnd(Scene scene, string itemCfgId, Vector3 pos)
        {
            if (string.IsNullOrEmpty(itemCfgId))
            {
                return;
            }
            if (ET.ItemHelper.ChkIsTower(itemCfgId))
            {
                return;
            }

            string itemDesc = ET.ItemHelper.GetItemDesc(itemCfgId);
            ShowDescTips(scene, itemDesc, pos, true, false).Coroutine();
        }

        public static async ETTask ShowDescTips(Scene scene, string itemDesc, Vector3 pos, bool tipTextAlignmentMid, bool notNeedClickBg)
        {
            if (string.IsNullOrEmpty(itemDesc))
            {
                return;
            }
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            DlgDescTips_ShowWindowData _DlgDescTips_ShowWindowData = new()
            {
                Pos = pos,
                Desc = itemDesc,
                tipTextAlignmentMid = tipTextAlignmentMid,
                notNeedClickBg = notNeedClickBg,
            };
            await _UIComponent.ShowWindowAsync<DlgDescTips>(_DlgDescTips_ShowWindowData);
        }

        public static void HideDescTips(Scene scene)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            _UIComponent.HideWindow<DlgDescTips>();
        }

        public static void ShowSkillDetails(Scene scene, string skillCfgId, bool isShowStatus, bool isLock)
        {
            if (string.IsNullOrEmpty(skillCfgId))
            {
                return;
            }

            ShowData_DlgSkillDetails _ShowData_DlgSkillDetails= new ();
            _ShowData_DlgSkillDetails.skillCfgId = skillCfgId;
            _ShowData_DlgSkillDetails.isShowStatus = isShowStatus;
            _ShowData_DlgSkillDetails.isLock = isLock;
            UIManagerHelper.GetUIComponent(scene).ShowWindow<DlgSkillDetails>(_ShowData_DlgSkillDetails);
        }

        #region 弹窗

        // 定义一个公共静态方法，用于显示密码框
        public static void ShowPassword(Scene scene, string msgStr, string passwordStr, Action SureBtnCallBak, string titleStr = null)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);

            DlgPassword _dlgPassword = _UIComponent.GetDlgLogic<DlgPassword>(true, true);

            //没有获取到对话框
            if (_dlgPassword == null)
            {
                _UIComponent.ShowWindow<DlgPassword>();
                _dlgPassword = _UIComponent.GetDlgLogic<DlgPassword>(true);
            }

            // 如果成功获取到通用密码对话框
            if (_dlgPassword != null)
            {
                _dlgPassword.ShowPassword(msgStr, passwordStr, SureBtnCallBak, titleStr);
            }
        }

        // 定义一个公共静态方法，用于显示DlgArcadeCoin
        public static void ShowDlgArcade(Scene scene, int defaultNum, Action SureBtnCallBak)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);

            DlgArcadeCoin _dlgArcadeCoin = _UIComponent.GetDlgLogic<DlgArcadeCoin>(true, true);

            //没有获取到
            if (_dlgArcadeCoin == null)
            {
                _UIComponent.ShowWindow<DlgArcadeCoin>();
                _dlgArcadeCoin = _UIComponent.GetDlgLogic<DlgArcadeCoin>(true);
            }

            // 如果成功获取到通用密码对话框
            if (_dlgArcadeCoin != null)
            {
                _dlgArcadeCoin.ShowDlgArcade(defaultNum, SureBtnCallBak);
            }
        }

        #endregion

        #region Room相关

        public static bool IsInRoomUI(Scene scene)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene.DomainScene());
            var _DlgRoom = _UIComponent.GetDlgLogic<DlgRoom>(true);
            if (_DlgRoom != null)
            {
                Log.Debug($"IsInRoomUI true DlgRoom");
                return true;
            }

            var _DlgARRoom = _UIComponent.GetDlgLogic<DlgARRoom>(true);
            if (_DlgARRoom != null)
            {
                Log.Debug($"IsInRoomUI true DlgARRoom");
                return true;
            }

            var _DlgARRoomPVE = _UIComponent.GetDlgLogic<DlgARRoomPVE>(true);
            if (_DlgARRoomPVE != null)
            {
                Log.Debug($"IsInRoomUI true DlgARRoomPVE");
                return true;
            }

            var _DlgARRoomPVESeason = _UIComponent.GetDlgLogic<DlgARRoomPVESeason>(true);
            if (_DlgARRoomPVESeason != null)
            {
                Log.Debug($"IsInRoomUI true DlgARRoomPVESeason");
                return true;
            }

            var _DlgARRoomPVP = _UIComponent.GetDlgLogic<DlgARRoomPVP>(true);
            if (_DlgARRoomPVP != null)
            {
                Log.Debug($"IsInRoomUI true DlgARRoomPVP");
                return true;
            }

            Log.Debug($"IsInRoomUI false");
            return false;
        }

        public static async ETTask EnterRoomUI(Scene scene)
        {
            UIAudioManagerHelper.PlayUIAudio(scene, SoundEffectType.JoinRoom);
            UIManagerHelper.GetUIComponent(scene).HideAllShownWindow();
            PlayerStatusComponent playerStatusComponent = PlayerStatusHelper.GetMyPlayerStatusComponent(scene);
            RoomTypeInfo roomTypeInfo = playerStatusComponent.RoomTypeInfo;
            RoomType roomType = roomTypeInfo.roomType;
            SubRoomType subRoomType = roomTypeInfo.subRoomType;
            int seasonCfgId = roomTypeInfo.seasonCfgId;
            if (playerStatusComponent.PlayerStatus != PlayerStatus.Room || playerStatusComponent.RoomId == 0)
            {
                if (roomType == RoomType.Normal)
                {
                    await UIManagerHelper.EnterGameModeUI(scene);
                }
                else if (roomType == RoomType.AR)
                {
                    await UIManagerHelper.EnterGameModeUI(scene);
                }
                return;
            }

            if (roomType == RoomType.Normal)
            {
                if (subRoomType == SubRoomType.NormalPVE)
                {
                    if (seasonCfgId > 0)
                    {
                        await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARRoomPVESeason>();
                    }
                    else
                    {
                        await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARRoomPVE>();
                    }
                }
                else if (subRoomType == SubRoomType.NormalPVP)
                {
                    await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARRoomPVP>();
                }
                else if (subRoomType == SubRoomType.NormalEndlessChallenge)
                {
                    await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARRoom>();
                }
                else if (subRoomType == SubRoomType.NormalScanMesh)
                {
                    await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARRoom>();
                }
                else
                {
                    await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgRoom>();
                }
            }
            else if (roomType == RoomType.AR && subRoomType == SubRoomType.ARPVP)
            {
                await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARRoomPVP>();
            }
            else if (roomType == RoomType.AR && subRoomType == SubRoomType.ARPVE)
            {
                if (seasonCfgId > 0)
                {
                    await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARRoomPVESeason>();
                }
                else
                {
                    await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARRoomPVE>();
                }
            }
            else
            {
                await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARRoom>();
            }
        }

        public static async ETTask ExitRoomUI(Scene scene)
        {
            await EnterGameModeUI(scene);
        }

        #endregion

        #region 主界面相关

        public static async ETTask EnterGameModeUI(Scene scene)
        {
            ET.Client.ARSessionHelper.ResetMainCamera(scene, false);

            UIManagerHelper.GetUIComponent(scene).HideAllShownWindow();

            if (DebugConnectComponent.Instance.IsDebugMode)
            {
                await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgGameMode>();
            }
            else
            {
                if (ET.SceneHelper.ChkIsGameModeArcade())
                {
                    await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgGameModeArcade>();
                }
                else
                {
                    await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgGameModeAR>();
                }
            }
        }

        #endregion

        #region 战斗界面相关

        public static bool IsInBattleUI(Scene scene)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene.DomainScene());
            var _DlgBattle = _UIComponent.GetDlgLogic<DlgBattle>(true);
            if (_DlgBattle != null)
            {
                Log.Debug($"IsInBattleUI true DlgBattle");
                return true;
            }

            var _DlgBattleTower = _UIComponent.GetDlgLogic<DlgBattleTower>(true);
            if (_DlgBattleTower != null)
            {
                Log.Debug($"IsInBattleUI true DlgBattleTower");
                return true;
            }

            var _DlgBattleTowerAR = _UIComponent.GetDlgLogic<DlgBattleTowerAR>(true);
            if (_DlgBattleTowerAR != null)
            {
                Log.Debug($"IsInBattleUI true DlgBattleTowerAR");
                return true;
            }

            Log.Debug($"IsInBattleUI false");
            return false;
        }

        public static async ETTask ShowBattleUI(Scene scene)
        {
            await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgBattleTowerAR>();
        }

        public static async ETTask ShowBattleUIReady(Scene scene, bool isShow)
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

        public static async ETTask ShowBattleUIQuit(Scene scene, bool isShow)
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

        #endregion

        #region 模型被注视
        public static void DealModelWhenHover(Transform transCollider, Action<bool> doHover)
        {
        }
        #endregion
    }
}