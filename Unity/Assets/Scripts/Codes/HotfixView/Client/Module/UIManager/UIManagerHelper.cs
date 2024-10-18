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

        public static void ShowCommonLoading(Scene scene, bool bForceShow)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            DlgCommonLoading _DlgCommonLoading = _UIComponent.GetDlgLogic<DlgCommonLoading>(true, true);
            if (_DlgCommonLoading == null)
            {
                _UIComponent.ShowWindow<DlgCommonLoading>();
                _DlgCommonLoading = _UIComponent.GetDlgLogic<DlgCommonLoading>(true);
            }
            if (_DlgCommonLoading != null)
            {
                _DlgCommonLoading.Show(bForceShow);
            }
        }

        public static void HideCommonLoading(Scene scene, bool bForceHide)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            DlgCommonLoading _DlgCommonLoading = _UIComponent.GetDlgLogic<DlgCommonLoading>(true, true);
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
            DlgCommonTip _DlgCommonTip = _UIComponent.GetDlgLogic<DlgCommonTip>(true, true);
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

        public static void ShowTipTopShow(Scene scene, string tipMsg)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            DlgCommonTipTopShow _DlgCommonTipTopShow = _UIComponent.GetDlgLogic<DlgCommonTipTopShow>(true, true);
            if (_DlgCommonTipTopShow == null)
            {
                _UIComponent.ShowWindow<DlgCommonTipTopShow>();
                _DlgCommonTipTopShow = _UIComponent.GetDlgLogic<DlgCommonTipTopShow>(true);
            }
            if (_DlgCommonTipTopShow != null)
            {
                _DlgCommonTipTopShow.ShowTip(tipMsg);
            }
        }

        public static void HideConfirm(Scene scene)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            _UIComponent.HideWindow<DlgCommonConfirm>();
        }

        public static void HideChoose(Scene scene)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            _UIComponent.HideWindow<DlgCommonChoose>();
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

        public static void ShowConfirmNoClose(Scene scene, string confirmMsg, string sureText = null, string titleText = null)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            DlgCommonConfirm _DlgCommonConfirm = _UIComponent.GetDlgLogic<DlgCommonConfirm>(true, true);
            if (_DlgCommonConfirm == null)
            {
                _UIComponent.ShowWindow<DlgCommonConfirm>();
                _DlgCommonConfirm = _UIComponent.GetDlgLogic<DlgCommonConfirm>(true);
            }
            if (_DlgCommonConfirm != null)
            {
                _DlgCommonConfirm.ShowConfirmNoClose(confirmMsg, sureText, titleText);
            }
        }

        public static void ShowConfirmNoCloseHighest(Scene scene, string confirmMsg, string sureText = null, string titleText = null)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            DlgCommonConfirmHighest _DlgCommonConfirmHighest = _UIComponent.GetDlgLogic<DlgCommonConfirmHighest>(true, true);
            if (_DlgCommonConfirmHighest == null)
            {
                _UIComponent.ShowWindow<DlgCommonConfirmHighest>();
                _DlgCommonConfirmHighest = _UIComponent.GetDlgLogic<DlgCommonConfirmHighest>(true);
            }
            if (_DlgCommonConfirmHighest != null)
            {
                _DlgCommonConfirmHighest.ShowConfirmNoClose(confirmMsg, sureText, titleText);
            }
        }

        public static void ShowOnlyConfirm(Scene scene, string confirmMsg, Action confirmCallBack, string sureText = null, string titleText = null)
        {

            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);

            DlgCommonConfirm _DlgCommonConfirm = _UIComponent.GetDlgLogic<DlgCommonConfirm>(true, true);

            // 如果找不到通用确认对话框的逻辑对象
            if (_DlgCommonConfirm == null)
            {

                _UIComponent.ShowWindow<DlgCommonConfirm>();
                _DlgCommonConfirm = _UIComponent.GetDlgLogic<DlgCommonConfirm>(true);
            }

            // 如果成功获取到通用确认对话框的逻辑对象
            if (_DlgCommonConfirm != null)
            {

                _DlgCommonConfirm.ShowOnlyConfirm(confirmMsg, confirmCallBack, sureText, titleText);
            }
        }


        public static void ShowOnlyConfirmHighest(Scene scene, string confirmMsg, Action confirmCallBack, string sureText = null, string titleText = null)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            DlgCommonConfirmHighest _DlgCommonConfirmHighest = _UIComponent.GetDlgLogic<DlgCommonConfirmHighest>(true, true);
            if (_DlgCommonConfirmHighest == null)
            {
                _UIComponent.ShowWindow<DlgCommonConfirmHighest>();
                _DlgCommonConfirmHighest = _UIComponent.GetDlgLogic<DlgCommonConfirmHighest>(true);
            }
            if (_DlgCommonConfirmHighest != null)
            {
                _DlgCommonConfirmHighest.ShowOnlyConfirm(confirmMsg, confirmCallBack, sureText, titleText);
            }
        }

        public static void ShowConfirm(Scene scene, string confirmMsg, Action confirmCallBack, Action cancelCallBack, string sureText = null, string cancelText = null, string titleText = null)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            DlgCommonConfirm _DlgCommonConfirm = _UIComponent.GetDlgLogic<DlgCommonConfirm>(true, true);
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

        public static void ShowConfirmHighest(Scene scene, string confirmMsg, Action confirmCallBack, Action cancelCallBack, string sureText = null, string cancelText = null, string titleText = null)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            DlgCommonConfirmHighest _DlgCommonConfirmHighest = _UIComponent.GetDlgLogic<DlgCommonConfirmHighest>(true, true);
            if (_DlgCommonConfirmHighest == null)
            {
                _UIComponent.ShowWindow<DlgCommonConfirmHighest>();
                _DlgCommonConfirmHighest = _UIComponent.GetDlgLogic<DlgCommonConfirmHighest>(true);
            }
            if (_DlgCommonConfirmHighest != null)
            {
                _DlgCommonConfirmHighest.ShowConfirm(confirmMsg, confirmCallBack, cancelCallBack, sureText, cancelText, titleText);
            }
        }

        public static void ShowWhenNoChoose(Scene scene, string showMsg, string timeoutMsg, float timeoutTime, Action confirmCallBack, string titleText = null, bool isCloseAfterChoose = true)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            DlgCommonChoose _DlgCommonChoose = _UIComponent.GetDlgLogic<DlgCommonChoose>(true, true);
            if (_DlgCommonChoose == null)
            {
                _UIComponent.ShowWindow<DlgCommonChoose>();
                _DlgCommonChoose = _UIComponent.GetDlgLogic<DlgCommonChoose>(true);
            }
            if (_DlgCommonChoose != null)
            {
                _DlgCommonChoose.ShowWhenNoChoose(showMsg, timeoutMsg, timeoutTime, confirmCallBack, titleText, isCloseAfterChoose);
            }
        }

        public static void ShowWhenOneChoose(Scene scene, string showMsg, string timeoutMsg, float timeoutTime, Action confirmCallBack, string confirmText = null, string titleText = null, bool isCloseAfterChoose = true)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            DlgCommonChoose _DlgCommonChoose = _UIComponent.GetDlgLogic<DlgCommonChoose>(true, true);
            if (_DlgCommonChoose == null)
            {
                _UIComponent.ShowWindow<DlgCommonChoose>();
                _DlgCommonChoose = _UIComponent.GetDlgLogic<DlgCommonChoose>(true);
            }
            if (_DlgCommonChoose != null)
            {
                _DlgCommonChoose.ShowWhenOneChoose(showMsg, timeoutMsg, timeoutTime, confirmCallBack,confirmText, titleText, isCloseAfterChoose);
            }
        }

        public static void ShowWhenTwoChoose(Scene scene, string showMsg, string timeoutMsg, float timeoutTime, Action confirmCallBack, Action cancelCallBack, string confirmText = null, string cancelText = null, string titleText = null, bool isTimeOutConfirm = true, bool isCloseAfterChoose = true)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            DlgCommonChoose _DlgCommonChoose = _UIComponent.GetDlgLogic<DlgCommonChoose>(true, true);
            if (_DlgCommonChoose == null)
            {
                _UIComponent.ShowWindow<DlgCommonChoose>();
                _DlgCommonChoose = _UIComponent.GetDlgLogic<DlgCommonChoose>(true);
            }
            if (_DlgCommonChoose != null)
            {
                _DlgCommonChoose.ShowWhenTwoChoose(showMsg, timeoutMsg, timeoutTime, confirmCallBack,cancelCallBack, confirmText, cancelText, titleText, isTimeOutConfirm, isCloseAfterChoose);
            }
        }

        public static void ShowUrl(Scene scene, string url, bool isWebView = true)
        {
            if (string.IsNullOrEmpty(url))
            {
                return;
            }
            if (Application.isMobilePlatform == false)
            {
                Application.OpenURL(url);
                return;
            }
            if (isWebView == false)
            {
                Application.OpenURL(url);
                return;
            }

            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            DlgCommonWebView _DlgCommonWebView = _UIComponent.GetDlgLogic<DlgCommonWebView>(true, true);
            if (_DlgCommonWebView == null)
            {
                _UIComponent.ShowWindow<DlgCommonWebView>();
                _DlgCommonWebView = _UIComponent.GetDlgLogic<DlgCommonWebView>(true);
            }
            if (_DlgCommonWebView != null)
            {
                _DlgCommonWebView.ShowWebView(url);
            }
        }

        public static async ETTask<Sprite> LoadSprite(string imgPath)
        {
            if (string.IsNullOrEmpty(imgPath))
            {
#if UNITY_EDITOR
                Log.Error($"ET.Client.UIManagerHelper.LoadSprite imgPath is null");
#endif
                return null;
            }
            Sprite sprite = await ResComponent.Instance.LoadAssetAsync<Sprite>(imgPath);
            return sprite;
        }

        /// <summary>
        /// 设置自身的头像
        /// </summary>
        /// <param name="image"></param>
        /// <param name="scene"></param>
        /// <returns></returns>
        public static async ETTask SetMyIcon(this Image image, Entity entity)
        {
            PlayerBaseInfoComponent playerBaseInfoComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(entity.DomainScene());
            List<string> avatarIconList = ET.Client.PlayerStatusHelper.GetAvatarIconList();
            await image.SetImageByPath(entity, avatarIconList[playerBaseInfoComponent.IconIndex]);


        }

        /// <summary>
        /// 设置自己的头像框
        /// </summary>
        /// <param name="image"></param>
        /// <param name="scene"></param>
        /// <returns></returns>
        public static async ETTask SetMyFrame(this Image image, Entity entity)
        {
            PlayerBaseInfoComponent playerBaseInfoComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(entity.DomainScene());
            //WJ 设置玩家的头像框
            if (string.IsNullOrEmpty(playerBaseInfoComponent.AvatarFrameItemCfgId) == false)
            {
                ItemCfg itemCfg = ItemCfgCategory.Instance.Get(playerBaseInfoComponent.AvatarFrameItemCfgId);
                await image.SetImageByResIconCfgId(entity, itemCfg.Icon);
            }

        }

        /// <summary>
        /// 设置其他玩家的头像
        /// </summary>
        /// <param name="image"></param>
        /// <param name="scene"></param>
        /// <param name="playerId">玩家的ID</param>
        /// <returns></returns>
        public static async ETTask SetOtherPlayerIcon(this Image image, Entity entity, long playerId)
        {
            PlayerBaseInfoComponent playerBaseInfoComponent = await ET.Client.PlayerCacheHelper.GetOtherPlayerBaseInfo(entity.DomainScene(), playerId);
            List<string> avatarIconList = ET.Client.PlayerStatusHelper.GetAvatarIconList();
            await image.SetImageByPath(entity, avatarIconList[playerBaseInfoComponent.IconIndex]);

        }

        /// <summary>
        /// 设置其他玩家的头像框
        /// </summary>
        /// <param name="image"></param>
        /// <param name="scene"></param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public static async ETTask SetOtherPlayerFrame(this Image image, Entity entity, long playerId)
        {
            PlayerBaseInfoComponent playerBaseInfoComponent = await ET.Client.PlayerCacheHelper.GetOtherPlayerBaseInfo(entity.DomainScene(), playerId);
            if (string.IsNullOrEmpty(playerBaseInfoComponent.AvatarFrameItemCfgId) == false)
            {
                ItemCfg itemCfg = ItemCfgCategory.Instance.Get(playerBaseInfoComponent.AvatarFrameItemCfgId);
                await image.SetImageByResIconCfgId(entity, itemCfg.Icon);
            }

        }

        public static async ETTask SetImageByItemCfgId(this Image image, Entity entity, string itemCfgId, bool needSetNativeSize = false)
        {
            //Log.Error($"zpb SetImageByItemCfgId itemCfgId {itemCfgId}");
            string resName = ItemHelper.GetItemIcon(itemCfgId);
            await image.SetImageByPath(entity, resName, needSetNativeSize);
        }

        public static async ETTask SetImageByResIconCfgId(this Image image, Entity entity, string resIconCfgId, bool needSetNativeSize = false, bool hideAtFirst = true)
        {
            ResIconCfg resIconCfg = ResIconCfgCategory.Instance.Get(resIconCfgId);
            await image.SetImageByPath(entity, resIconCfg.ResName, needSetNativeSize, hideAtFirst);
        }

        public static async ETTask SetImageByPath(this Image image, Entity entity, string imgPath, bool needSetNativeSize = false, bool hideAtFirst = true)
        {
            if (image == null)
            {
                return;
            }
            if (entity == null || entity.IsDisposed)
            {
                return;
            }
            if (string.IsNullOrEmpty(imgPath))
            {
                image.sprite = null;
#if UNITY_EDITOR
                Log.Error($"ET.Client.UIManagerHelper.SetImageByPath imgPath is null");
#endif
                return;
            }

            if (hideAtFirst)
            {
                image.enabled = false;
            }

            Sprite sprite = await ResComponent.Instance.LoadAssetAsync<Sprite>(imgPath);
            if (image == null)
            {
                return;
            }
            if (entity == null || entity.IsDisposed)
            {
                return;
            }
            DynamicImage dynamicImage = image.gameObject.GetComponent<DynamicImage>();
            if (dynamicImage != null && dynamicImage.ChkStatus() && dynamicImage.ChkTextureCanDynamic(sprite.texture))
            {
                dynamicImage.SetImage(imgPath, sprite.texture);
            }
            else
            {
                image.sprite = sprite;
                if (needSetNativeSize)
                {
                    image.SetNativeSize();
                }
            }
            if (hideAtFirst)
            {
                image.enabled = true;
            }
        }

        public static void SetImageGray(this Image image, bool isGray)
        {
            Material material = ResComponent.Instance.LoadAsset<Material>("UIGray");
            image.material = isGray ? material : null;
        }

        public static void SetImageGray(this Transform trans, bool isGray)
        {
            Image[] imgs = trans.gameObject.GetComponentsInChildren<Image>();
            foreach (Image img in imgs)
            {
                img.SetImageGray(isGray);
            }
        }

        public static async ETTask LoadBG(this Image image, Entity entity)
        {
            //image.sprite = null;
            bool bFindBlackBG = false;
            Transform blackBG = image.transform.Find("BlackBG");
            if (blackBG != null)
            {
                bFindBlackBG = true;
            }
            if (bFindBlackBG)
            {
                blackBG.gameObject.SetActive(true);
            }
            else
            {
                image.color = new Color(1, 1, 1, 0.2f);
            }
            List<string> bgList = GlobalSettingCfgCategory.Instance.BGList;
            string bg = RandomGenerator.RandomArray(bgList);
            await image.SetImageByResIconCfgId(entity, bg, false, false);
            if (image == null)
            {
                return;
            }
            if (bFindBlackBG)
            {
                blackBG.gameObject.SetActive(false);
            }
            else
            {
                image.color = new Color(1, 1, 1, 1);
            }
        }

        public static void ShowItemInfoWnd(Scene scene, string itemCfgId, Vector3 pos)
        {
            if (string.IsNullOrEmpty(itemCfgId))
            {
                return;
            }
            if (ItemHelper.ChkIsTower(itemCfgId))
            {
                _ShowTowerItemWnd(scene, itemCfgId);
            }
            else
            {
                _ShowSimpleItemWnd(scene, itemCfgId, pos);
            }
        }

        public static void _ShowTowerItemWnd(Scene scene, string itemCfgId)
        {
            if (string.IsNullOrEmpty(itemCfgId))
            {
                return;
            }
            if (ItemHelper.ChkIsTower(itemCfgId) == false)
            {
                return;
            }

            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            _UIComponent.ShowWindow<DlgItemDetails>();
            DlgItemDetails _DlgItemDetails = _UIComponent.GetDlgLogic<DlgItemDetails>(true);
            if (_DlgItemDetails != null)
            {
                _DlgItemDetails.SetCurItemCfgId(itemCfgId);
            }
        }

        public static void _ShowSimpleItemWnd(Scene scene, string itemCfgId, Vector3 pos)
        {
            if (string.IsNullOrEmpty(itemCfgId))
            {
                return;
            }
            if (ItemHelper.ChkIsTower(itemCfgId))
            {
                return;
            }

            string itemDesc = ItemHelper.GetItemDesc(itemCfgId);
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

        public static async ETTask<bool> ChkPhsicalAndShowtip(Scene scene, int takePhsicalStrength)
        {
            PlayerBaseInfoComponent playerBaseInfoComponent =
                    await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(scene);
            if(playerBaseInfoComponent._ChkPhysicalStrength(-takePhsicalStrength) == false)
            {
                string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_PhysicalStrength_GetMore", takePhsicalStrength);
                UIManagerHelper.ShowConfirm(scene, msg, () =>
                {
                    UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgPhysicalStrength>().Coroutine();
                }, null);
                return false;
            }
            return true;
        }

        public static async ETTask<bool> ChkCoinEnoughOrShowtip(Scene scene, int arcadeCoinNum, Action finishCallBack = null)
        {
            int curArcadeCoin = await PlayerCacheHelper.GetTokenArcadeCoin(scene);
            PlayerBaseInfoComponent playerBaseInfoComponent =
                    await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(scene);
            if(curArcadeCoin < arcadeCoinNum)
            {
                string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_PhysicalStrength_GetMore", arcadeCoinNum);
                UIManagerHelper.ShowConfirm(scene, msg, () =>
                {
                    UIManagerHelper.ShowDlgArcade(scene, arcadeCoinNum - curArcadeCoin, finishCallBack);
                }, null);
                return false;
            }
            return true;
        }

        public static async ETTask<bool> ChkDiamondAndShowtip(Scene scene, int needDiamond, bool needTip = true)
        {
            int curDiamond = await PlayerCacheHelper.GetTokenDiamond(scene);
            if(curDiamond < needDiamond)
            {
                if (needTip)
                {
                    string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_PhysicalStrength_GetMore", needDiamond);
                    UIManagerHelper.ShowConfirm(scene, msg, () =>
                    {
                        //UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgPhysicalStrength>().Coroutine();
                    }, null);
                }
                return false;
            }
            return true;
        }

        public static async ETTask ShowFunctionMenuLockOne(Scene scene, string functionMenuCfgId, Transform transformLock)
        {
            if (transformLock == null)
            {
#if UNITY_EDITOR
                Log.Error($"ET.Client.UIManagerHelper.ShowFunctionMenuLockOne transformLock == null");
#endif
                return;
            }

            FunctionMenuCfg functionMenuCfg = FunctionMenuCfgCategory.Instance.Get(functionMenuCfgId);
            if (functionMenuCfg == null)
            {
#if UNITY_EDITOR
                Log.Error($"ET.Client.UIManagerHelper.ShowFunctionMenuLockOne functionMenuCfg[{functionMenuCfgId}] == null");
#endif
                return;
            }
            PlayerFunctionMenuComponent playerFunctionMenuComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerFunctionMenu(scene);
            FunctionMenuStatus functionMenuStatus = playerFunctionMenuComponent.GetStatus(functionMenuCfgId);

            if (ET.SceneHelper.ChkIsGameModeArcade())
            {
                functionMenuStatus = FunctionMenuStatus.Openned;
            }
            else if (ET.SceneHelper.ChkIsDemoShow())
            {
                functionMenuStatus = FunctionMenuStatus.Openned;
            }

            if (functionMenuCfg.IsOpenSoon)
            {
                transformLock.gameObject.SetActive(true);

                ET.EventTriggerListener.Get(transformLock.gameObject).onClick.AddListener((go, xx) =>
                {
                    string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_FunctionMenu_IsOpenSoon");
                    UIManagerHelper.ShowTip(scene, tipMsg);
                });

                Transform lockTextTrans = transformLock.Find("Text");
                if (lockTextTrans != null)
                {
                    var tmpText = lockTextTrans.gameObject.GetComponent<TMPro.TextMeshProUGUI>();
                    if (tmpText != null)
                    {
                        tmpText.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_FunctionMenu_IsOpenSoon");
                    }
                    else
                    {
                        var text = lockTextTrans.gameObject.GetComponent<Text>();
                        if (text != null)
                        {
                            text.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_FunctionMenu_IsOpenSoon");
                        }
                    }
                }
            }
            else if (functionMenuStatus == FunctionMenuStatus.Lock)
            {
                transformLock.gameObject.SetActive(true);

                ET.EventTriggerListener.Get(transformLock.gameObject).onClick.AddListener((go, xx) =>
                {
                    string tipMsg = GetFunctionMenuConditionDesc(functionMenuCfgId);
                    UIManagerHelper.ShowTip(scene, tipMsg);
                });

                Transform lockTextTrans = transformLock.Find("Text");
                if (lockTextTrans != null)
                {
                    var tmpText = lockTextTrans.gameObject.GetComponent<TMPro.TextMeshProUGUI>();
                    if (tmpText != null)
                    {
                        tmpText.text = GetFunctionMenuConditionDesc(functionMenuCfgId);
                    }
                    else
                    {
                        var text = lockTextTrans.gameObject.GetComponent<Text>();
                        if (text != null)
                        {
                            text.text = GetFunctionMenuConditionDesc(functionMenuCfgId);
                        }
                    }
                }
            }
            else
            {
                transformLock.gameObject.SetActive(false);
            }
        }

        public static string GetFunctionMenuConditionDesc(string functionMenuCfgId)
        {
            FunctionMenuCfg functionMenuCfg = FunctionMenuCfgCategory.Instance.Get(functionMenuCfgId);
            string descKey = "";
            switch (functionMenuCfg.OpenCondition)
            {
                case FunctionMenuConditionDefault functionMenuConditionDefault:
                    descKey = "Text_Key_FunctionMenu_OpenCondition_Default";
                    return LocalizeComponent.Instance.GetTextValue(descKey);
                    break;
                case FunctionMenuConditionTutorialFirstFinished functionMenuConditionTutorialFirstFinished:
                    descKey = "Text_Key_FunctionMenu_OpenCondition_TutorialFirstFinished";
                    return LocalizeComponent.Instance.GetTextValue(descKey);
                    break;
                case FunctionMenuConditionBattleNumARAny functionMenuConditionBattleNumARAny:
                    descKey = "Text_Key_FunctionMenu_OpenCondition_BattleNumARAny";
                    return LocalizeComponent.Instance.GetTextValue(descKey, functionMenuConditionBattleNumARAny.BattleNum);
                    break;
                case FunctionMenuConditionBattleNumARPVE functionMenuConditionBattleNumArpve:
                    descKey = "Text_Key_FunctionMenu_OpenCondition_BattleNumARPVE";
                    return LocalizeComponent.Instance.GetTextValue(descKey, functionMenuConditionBattleNumArpve.BattleNum);
                    break;
                case FunctionMenuConditionBattleNumARPVP functionMenuConditionBattleNumArpvp:
                    descKey = "Text_Key_FunctionMenu_OpenCondition_BattleNumARPVP";
                    return LocalizeComponent.Instance.GetTextValue(descKey, functionMenuConditionBattleNumArpvp.BattleNum);
                    break;
                case FunctionMenuConditionBattleNumAREndlessChallenge functionMenuConditionBattleNumAREndlessChallenge:
                    descKey = "Text_Key_FunctionMenu_OpenCondition_BattleNumAREndlessChallenge";
                    return LocalizeComponent.Instance.GetTextValue(descKey, functionMenuConditionBattleNumAREndlessChallenge.BattleNum);
                    break;
                case FunctionMenuConditionIndexWhenARPVE functionMenuConditionIndexWhenArpve:
                    descKey = "Text_Key_FunctionMenu_OpenCondition_IndexWhenARPVE";
                    return LocalizeComponent.Instance.GetTextValue(descKey, functionMenuConditionIndexWhenArpve.Index);
                    break;
                case FunctionMenuConditionIndexWhenAREndlessChallenge functionMenuConditionIndexWhenAREndlessChallenge:
                    descKey = "Text_Key_FunctionMenu_OpenCondition_IndexWhenAREndlessChallenge";
                    return LocalizeComponent.Instance.GetTextValue(descKey, functionMenuConditionIndexWhenAREndlessChallenge.Index);
                    break;
                default:
                    descKey = "Text_Key_FunctionMenu_OpenCondition_Default";
                    return LocalizeComponent.Instance.GetTextValue(descKey);
                    break;
            }
            return "";
        }

        #region 金币、体力 显示
        public static async ETTask ShowTokenArcadeCoinCostText(this Transform textTrans, Scene scene, int costValue)
        {
            int curArcadeCoin = await PlayerCacheHelper.GetTokenArcadeCoin(scene);
            bool isEnough = curArcadeCoin >= costValue;
            textTrans.ChgTMPColor(isEnough);
            textTrans.ChgTMPText($"{costValue}");
        }

        public static async ETTask ShowTokenDiamondCostText(this Transform textTrans, Scene scene, int costValue)
        {
            int curDiamond = await PlayerCacheHelper.GetTokenDiamond(scene);
            bool isEnough = curDiamond >= costValue;
            textTrans.ChgTMPColor(isEnough);
            textTrans.ChgTMPText($"{costValue}");
        }

        public static async ETTask ShowCoinCostTextInBattleTower(this Transform textTrans, Scene scene, int costValue)
        {
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(scene);
            long playerId = PlayerStatusHelper.GetMyPlayerId(scene);
            int curGoldValue = (int)gamePlayComponent.GetPlayerCoin(playerId, CoinTypeInGame.Gold);

            bool isEnough = curGoldValue >= costValue;
            textTrans.ChgTMPColor(isEnough);
            textTrans.ChgTMPText($"{costValue}");
            await ETTask.CompletedTask;
        }

        public static async ETTask ShowPhysicalCostText(this Transform textTrans, Scene scene, int costValue)
        {
            PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(scene);
            bool isEnough = playerBaseInfoComponent.GetPhysicalStrength() >= costValue;
            textTrans.ChgTMPColor(isEnough);
            textTrans.ChgTMPText($"{costValue}");
        }

        public static async ETTask ShowTokenArcadeCoinCostText(this TextMeshProUGUI textMeshProUGUI, Scene scene, int costValue)
        {
            int curArcadeCoin = await PlayerCacheHelper.GetTokenArcadeCoin(scene);
            bool isEnough = curArcadeCoin >= costValue;
            textMeshProUGUI.ChgTMPColor(isEnough);
            textMeshProUGUI.ChgTMPText($"{costValue}");
        }

        public static async ETTask ShowTokenDiamondCostText(this TextMeshProUGUI textMeshProUGUI, Scene scene, int costValue)
        {
            int curDiamond = await PlayerCacheHelper.GetTokenDiamond(scene);
            bool isEnough = curDiamond >= costValue;
            textMeshProUGUI.ChgTMPColor(isEnough);
            textMeshProUGUI.ChgTMPText($"{costValue}");
        }

        public static async ETTask ShowCoinCostTextInBattleTower(this TextMeshProUGUI textMeshProUGUI, Scene scene, int costValue)
        {
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(scene);
            long playerId = PlayerStatusHelper.GetMyPlayerId(scene);
            int curGoldValue = (int)gamePlayComponent.GetPlayerCoin(playerId, CoinTypeInGame.Gold);

            bool isEnough = curGoldValue >= costValue;
            textMeshProUGUI.ChgTMPColor(isEnough);
            textMeshProUGUI.ChgTMPText($"{costValue}");
            await ETTask.CompletedTask;
        }

        public static async ETTask ShowPhysicalCostText(this TextMeshProUGUI textMeshProUGUI, Scene scene, int costValue)
        {
            PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(scene);
            bool isEnough = playerBaseInfoComponent.GetPhysicalStrength() >= costValue;
            textMeshProUGUI.ChgTMPColor(isEnough);
            textMeshProUGUI.ChgTMPText($"{costValue}");
        }

        #endregion

        #region 在client展示ARMesh

        public static async ETTask ShowARMesh(Scene scene)
        {
            if (Application.isEditor == false)
            {
                return;
            }

            GamePlayComponent gamePlayComponent = ET.Client.GamePlayHelper.GetGamePlay(scene);
            if (gamePlayComponent == null)
            {
                return;
            }

            if (gamePlayComponent.isTestARMesh)
            {
                gamePlayComponent._ARMeshDownLoadUrl = gamePlayComponent.isTestARMeshUrl;

                // Draco bytes
                byte[] bytes = await gamePlayComponent.DownloadFileBytesAsync(gamePlayComponent._ARMeshDownLoadUrl);
                if (bytes == null)
                {
                    return;
                }
                if (gamePlayComponent.IsDisposed)
                {
                    return;
                }
                MeshHelper.MeshData meshData = MeshHelper.GetMeshDataFromBytes(bytes);
                CreateMeshFromData(meshData, gamePlayComponent.GetARScale());
            }
            else if (gamePlayComponent.isTestARObj)
            {
                gamePlayComponent._ARMeshDownLoadUrl = gamePlayComponent.isTestARObjUrl;

                string content = await gamePlayComponent.DownloadFileTextAsync(gamePlayComponent._ARMeshDownLoadUrl);
                if (string.IsNullOrEmpty(content))
                {
                    return;
                }
                if (gamePlayComponent.IsDisposed)
                {
                    return;
                }
                ET.LoadMesh.ObjMesh objInstace = new ET.LoadMesh.ObjMesh();
                objInstace = objInstace.LoadFromObj(content);

                CreateMesh(objInstace.VertexArray, objInstace.TriangleArray, objInstace.NormalArray, objInstace.UVArray, gamePlayComponent.GetARScale());
            }
            else if(gamePlayComponent.ChkIsAR() == false)
            {
                return;
            }

        }

        public static Mesh CreateMeshFromData(MeshHelper.MeshData meshData, float3 scale)
        {
            var vertices = new Vector3[meshData.vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = meshData.vertices[i];
            }
            Vector3[] normals = null;
            if (meshData.normals != null)
            {
                normals = new Vector3[meshData.normals.Length];
                for (int i = 0; i < normals.Length; i++)
                {
                    normals[i] = meshData.normals[i];
                }
            }

            Vector2[] uv = null;
            if (meshData.uv != null)
            {
                uv = new Vector2[meshData.uv.Length];
                for (int i = 0; i < meshData.uv.Length; i++)
                {
                    uv[i] = meshData.uv[i];
                }
            }

            return CreateMesh(vertices, meshData.triangles, normals, uv, scale);
        }

        public static Mesh CreateMesh(Vector3[] verticesIn, int[] trianglesIn, Vector3[] normalsIn, Vector2[] uvIn, float3 scale)
        {
            Mesh mesh = new Mesh();
            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            if (verticesIn != null)
            {
                var vertices = new Vector3[verticesIn.Length];
                for (int i = 0; i < vertices.Length; i++)
                {
                    vertices[i] = verticesIn[i] * scale;
                }
                mesh.vertices = vertices;
            }
            if (trianglesIn != null)
            {
                mesh.triangles = trianglesIn;
            }
            if (normalsIn != null)
            {
                var normals = new Vector3[normalsIn.Length];
                for (int i = 0; i < normalsIn.Length; i++)
                {
                    normals[i] = normalsIn[i];
                }

                mesh.normals = normals;
            }
            if (uvIn != null)
            {
                var uv = new Vector2[uvIn.Length];
                for (int i = 0; i < uvIn.Length; i++)
                {
                    uv[i] = uvIn[i];
                }
                mesh.uv = uv;
            }

            mesh.RecalculateNormals();
            mesh.RecalculateUVDistributionMetric(0);
            mesh.RecalculateBounds();
            mesh.Optimize();
            mesh.UploadMeshData(false);

            Material material = null;
            GameObject zpbTestObj = GameObject.Find("zpbTestObj");
            if (zpbTestObj != null)
            {
                material = zpbTestObj.GetComponent<MeshRenderer>().material;
                GameObject.Destroy(zpbTestObj);
            }
            zpbTestObj = new GameObject("zpbTestObj");
            GameObject.DontDestroyOnLoad(zpbTestObj);
            zpbTestObj.transform.localScale = new Vector3(1, 1, 1);
            zpbTestObj.layer = LayerMask.NameToLayer("Map");
            zpbTestObj.AddComponent<MeshCollider>().sharedMesh = mesh;
            zpbTestObj.AddComponent<MeshRenderer>().material = material;
            zpbTestObj.AddComponent<MeshFilter>().sharedMesh = mesh;
            // Mesh without wireframe data.
            return mesh;
        }


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

        #region RedDot

        public static async ETTask DealPlayerUIRedDotType(Scene scene, bool isNeedInit)
        {
            if (isNeedInit)
            {
                foreach (var item in PlayerOtherInfoComponent.UIRedDot2Parent)
                {
                    UIRedDotType uiRedDotType = item.Key;
                    UIRedDotType uiRedDotTypeParent = item.Value;
                    ET.Client.UIRedDotHelper.AddRedDotNode(scene, uiRedDotTypeParent, uiRedDotType, false);
                }
            }

            PlayerOtherInfoComponent playerOtherInfoComponent = await PlayerCacheHelper.GetMyPlayerOtherInfo(scene);
            foreach (var item in playerOtherInfoComponent.uiRedDotTypeDic)
            {
                if (item.Value)
                {
                    UIRedDotHelper.ShowRedDotNode(scene, item.Key);
                }
                else
                {
                    UIRedDotHelper.HideRedDotNode(scene, item.Key);
                }
            }
        }

        public static void HideRedDotNodeLocal(Scene scene, UIRedDotType uiRedDotType)
        {
            UIRedDotHelper.HideRedDotNode(scene, uiRedDotType);
        }

        public static async ETTask HideUIRedDot(Scene scene, UIRedDotType uiRedDotType, string itemCfgId = "", string skillCfgId = "")
        {
            if (uiRedDotType != UIRedDotType.None)
            {
                UIRedDotHelper.HideRedDotNode(scene, uiRedDotType);
            }
            await ET.Client.PlayerCacheHelper.SetUIRedDotType(scene, uiRedDotType, itemCfgId, skillCfgId);
        }

        #endregion
    }
}