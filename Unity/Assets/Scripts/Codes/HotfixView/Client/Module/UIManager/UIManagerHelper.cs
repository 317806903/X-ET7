using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    public static class UIManagerHelper
    {
        public static UIComponent GetUIComponent(Scene scene)
        {
            Scene currentScene = null;
            Scene clientScene = null;
            if (scene == scene.ClientScene())
            {
                currentScene = scene.GetComponent<CurrentScenesComponent>().Scene;
                clientScene = scene;
            }
            else
            {
                currentScene = scene;
                clientScene = scene.ClientScene();
            }

            UIComponent _UIComponent = clientScene.GetComponent<UIComponent>();
            if (_UIComponent == null)
            {
                _UIComponent = clientScene.AddComponent<UIComponent>();
            }
            return _UIComponent;
        }

        public static void ShowCommonLoading(Scene scene)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            DlgCommonLoading _DlgCommonLoading = _UIComponent.GetDlgLogic<DlgCommonLoading>(true);
            if (_DlgCommonLoading == null)
            {
                _UIComponent.ShowWindow<DlgCommonLoading>();
                _DlgCommonLoading = _UIComponent.GetDlgLogic<DlgCommonLoading>(true);
            }
            if (_DlgCommonLoading != null)
            {
                _DlgCommonLoading.Show();
            }
        }

        public static void HideCommonLoading(Scene scene, bool bForceHide)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            DlgCommonLoading _DlgCommonLoading = _UIComponent.GetDlgLogic<DlgCommonLoading>(true);
            if (_DlgCommonLoading == null)
            {
                _UIComponent.ShowWindow<DlgCommonLoading>();
                _DlgCommonLoading = _UIComponent.GetDlgLogic<DlgCommonLoading>(true);
            }
            if (_DlgCommonLoading != null)
            {
                _DlgCommonLoading.Hide(bForceHide);
            }
        }

        public static void ShowTip(Scene scene, string tipMsg)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            DlgCommonTip _DlgCommonTip = _UIComponent.GetDlgLogic<DlgCommonTip>(true);
            if (_DlgCommonTip == null)
            {
                _UIComponent.ShowWindow<DlgCommonTip>();
                _DlgCommonTip = _UIComponent.GetDlgLogic<DlgCommonTip>(true);
            }
            if (_DlgCommonTip != null)
            {
                _DlgCommonTip.ShowTip(tipMsg);
            }
        }
        
        public static void ShowTipNode(Scene scene, string tipMsg)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            DlgCommonTipNode _DlgCommonTipNode = _UIComponent.GetDlgLogic<DlgCommonTipNode>(true);
            if (_DlgCommonTipNode == null)
            {
                _UIComponent.ShowWindow<DlgCommonTipNode>();
                _DlgCommonTipNode = _UIComponent.GetDlgLogic<DlgCommonTipNode>(true);
            }
            if (_DlgCommonTipNode != null)
            {
                _DlgCommonTipNode.ShowTip(tipMsg);
            }
        }

        public static void HideConfirm(Scene scene)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            _UIComponent.HideWindow<DlgCommonConfirm>();
        }

        public static void PreLoadConfirm(Scene scene)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            if (_UIComponent != null)
            {
                DlgCommonConfirm _DlgCommonConfirm = _UIComponent.GetDlgLogic<DlgCommonConfirm>(false);
                if (_DlgCommonConfirm == null)
                {
                    _UIComponent.ShowWindow<DlgCommonConfirm>();
                    _UIComponent.HideWindow<DlgCommonConfirm>();
                }
            }
        }

        public static void ShowConfirmNoClose(Scene scene, string confirmMsg, string sureText = null, string cancelText = null, string titleText = null)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            DlgCommonConfirm _DlgCommonConfirm = _UIComponent.GetDlgLogic<DlgCommonConfirm>(true);
            if (_DlgCommonConfirm == null)
            {
                _UIComponent.ShowWindow<DlgCommonConfirm>();
                _DlgCommonConfirm = _UIComponent.GetDlgLogic<DlgCommonConfirm>(true);
            }
            if (_DlgCommonConfirm != null)
            {
                _DlgCommonConfirm.ShowConfirmNoClose(confirmMsg, sureText, cancelText, titleText);
            }
        }

        public static void ShowOnlyConfirm(Scene scene, string confirmMsg, Action confirmCallBack, string sureText = null, string cancelText = null, string titleText = null)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            DlgCommonConfirm _DlgCommonConfirm = _UIComponent.GetDlgLogic<DlgCommonConfirm>(true);
            if (_DlgCommonConfirm == null)
            {
                _UIComponent.ShowWindow<DlgCommonConfirm>();
                _DlgCommonConfirm = _UIComponent.GetDlgLogic<DlgCommonConfirm>(true);
            }
            if (_DlgCommonConfirm != null)
            {
                _DlgCommonConfirm.ShowOnlyConfirm(confirmMsg, confirmCallBack, sureText, cancelText, titleText);
            }
        }

        public static void ShowConfirm(Scene scene, string confirmMsg, Action confirmCallBack, Action cancelCallBack, string sureText = null, string cancelText = null, string titleText = null)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            DlgCommonConfirm _DlgCommonConfirm = _UIComponent.GetDlgLogic<DlgCommonConfirm>(true);
            if (_DlgCommonConfirm == null)
            {
                _UIComponent.ShowWindow<DlgCommonConfirm>();
                _DlgCommonConfirm = _UIComponent.GetDlgLogic<DlgCommonConfirm>(true);
            }
            if (_DlgCommonConfirm != null)
            {
                _DlgCommonConfirm.ShowConfirm(confirmMsg, confirmCallBack, cancelCallBack, sureText, cancelText, titleText);
            }
        }

        public static async ETTask<Sprite> LoadSprite(string imgPath)
        {
            Sprite sprite = await ResComponent.Instance.LoadAssetAsync<Sprite>(imgPath);
            return sprite;
        }

        public static async ETTask SetMyIcon(this Image image, Scene scene)
        {
            PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(scene);
            List<string> avatarIconList = ET.Client.PlayerHelper.GetAvatarIconList();
            await image.SetImageByPath(avatarIconList[playerBaseInfoComponent.IconIndex]);
        }

        public static async ETTask SetPlayerIcon(this Image image, Scene scene, long playerId)
        {
            PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetOtherPlayerBaseInfo(scene, playerId);
            List<string> avatarIconList = ET.Client.PlayerHelper.GetAvatarIconList();
            await image.SetImageByPath(avatarIconList[playerBaseInfoComponent.IconIndex]);
        }

        public static async ETTask SetImageByPath(this Image image, string imgPath)
        {
            Sprite sprite = await ResComponent.Instance.LoadAssetAsync<Sprite>(imgPath);
            image.sprite = sprite;
        }

        public static void SetImageGray(this Image image, bool isGray)
        {
            Material material = ResComponent.Instance.LoadAsset<Material>("UIGray");
            image.material = isGray ? material : null;
        }

        public static async ETTask EnterRoom(Scene scene)
        {
            UIAudioManagerHelper.PlayUIAudio(scene, SoundEffectType.JoinRoom);
            UIManagerHelper.GetUIComponent(scene).HideAllShownWindow();
            PlayerStatusComponent playerStatusComponent = PlayerHelper.GetMyPlayerStatusComponent(scene);
            if (playerStatusComponent.PlayerStatus != PlayerStatus.Room || playerStatusComponent.RoomId == 0)
            {
                if (playerStatusComponent.RoomType == RoomType.Normal)
                {
                    await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgGameMode>();
                }
                else if (playerStatusComponent.RoomType == RoomType.AR)
                {
                    await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgGameModeAR>();
                }
                return;
            }

            if (playerStatusComponent.RoomType == RoomType.Normal)
            {
                await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgRoom>();
            }
            else if (playerStatusComponent.RoomType == RoomType.AR && playerStatusComponent.SubRoomType == SubRoomType.ARPVP)
            {
                await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARRoomPVP>();
            }
            else
            {
                await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARRoom>();
            }
        }

        public static async ETTask ExitRoom(Scene scene)
        {
            ET.Client.ARSessionHelper.ResetMainCamera(scene, false);

            UIManagerHelper.GetUIComponent(scene).HideAllShownWindow();

            if (DebugConnectComponent.Instance.IsDebugMode)
            {
                await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgGameMode>();
            }
            else
            {
                await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgGameModeAR>();
            }
            // PlayerStatusComponent playerStatusComponent = PlayerHelper.GetMyPlayerStatusComponent(scene);
            // if (playerStatusComponent.RoomType == RoomType.Normal)
            // {
            //     await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgGameMode>();
            // }
            // else if (playerStatusComponent.RoomType == RoomType.AR)
            // {
            //     await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgGameModeAR>();
            // }
        }

        public static async ETTask<bool> ChkAndShowtip(Scene scene, int takePhsicalStrength)
        {
            PlayerBaseInfoComponent playerBaseInfoComponent =
                    await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(scene);
            if (playerBaseInfoComponent.GetPhysicalStrength() < takePhsicalStrength)
            {
                UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
                _UIComponent.ShowWindow<DlgPhysicalStrengthTip>();
                DlgPhysicalStrengthTip _DlgPhysicalStrengthTip = _UIComponent.GetDlgLogic<DlgPhysicalStrengthTip>(true);
                _DlgPhysicalStrengthTip.SetText(takePhsicalStrength.ToString());
                return false;
            }

            return true;
        }

    }
}