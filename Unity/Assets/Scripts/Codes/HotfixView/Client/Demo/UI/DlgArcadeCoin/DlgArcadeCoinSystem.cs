using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof (DlgArcadeCoin))]
    public static class DlgArcadeCoinSystem
    {
        //注册
        public static void RegisterUIEvent(this DlgArcadeCoin self)
        {
            //背景板
            self.View.E_BG_ClickButton.AddListener(self.OnBGClick);
            self.View.E_BG_PayQRCodeClickButton.AddListener(self.OnPayQRCodeBGClick);
            //加
            self.View.EButton_AddButton.AddListener(self.OnAddClick);
            //减
            self.View.EButton_SubButton.AddListener(self.OnSubClick);

            // 添加确认按钮点击事件的监听器
            self.View.EButton_OKButton.AddListener(() =>
            {
                UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);
                self._ShowPayQRCode().Coroutine();
            });
        }

        //显示
        public static void ShowWindow(this DlgArcadeCoin self, ShowWindowData contextData = null)
        {
            self.SetDefalut();
            self.ShowBg().Coroutine();
        }

        //隐藏
        public static void HideWindow(this DlgArcadeCoin self)
        {
        }

        public static void SetDefalut(this DlgArcadeCoin self)
        {
            string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_ArcadeCoin_Title");
            self.View.E_CoinTitleTextMeshProUGUI.SetText(msg);
        }

        //UIManagerHelper调用
        public static void ShowDlgArcade(this DlgArcadeCoin self, int defaultNum, Action SureBtnCallBak)
        {
            self.SureBtnCallBak = SureBtnCallBak;

            //根据外部数据刷新面板
            self.UpdateArcadeCoin(defaultNum);

            self.View.EG_PayQRCodeRootRectTransform.SetVisible(false);
        }

        //背景相关
        public static async ETTask ShowBg(this DlgArcadeCoin self)
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

        public static void UpdateArcadeCoin(this DlgArcadeCoin self, int arcadeCoinNum)
        {
            self.arcadeCoinNum = Math.Clamp(arcadeCoinNum, 1, 20);

            self.View.ELabel_CoinNumTextMeshProUGUI.SetText(self.arcadeCoinNum.ToString());

            string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_ArcadeCoin_Msg", ET.Client.PayHelper.GetMoneyValue(self.arcadeCoinNum),
                self.arcadeCoinNum);
            self.View.Elabel_MsgTextMeshProUGUI.SetText(msg);
            self.View.E_QRCodeTextBottomTextMeshProUGUI.SetText(msg);
        }

        public static async ETTask RefreshWhenBaseInfoChg(this DlgArcadeCoin self)
        {
            await self._PayCallBack();
        }

        public static async ETTask _ShowPayQRCode(this DlgArcadeCoin self)
        {
            (bool bRet, string msg, PayComponent payComponent) = await ET.Client.PayHelper.GetNewPayOrder(self.DomainScene(), self.arcadeCoinNum);
            if (bRet)
            {
                string sWxUrl = payComponent.sWXUrl;
                payComponent.Dispose();

                self.View.E_PayQRCodeImgRawImage.texture = ET.QRCodeHelper.CreateQRCode(sWxUrl, 545);
                self.View.EG_PayQRCodeRootRectTransform.SetVisible(true);
            }
            else
            {
                UIManagerHelper.ShowTip(self.DomainScene(), msg);
            }
        }

        public static async ETTask _PayCallBack(this DlgArcadeCoin self)
        {
            self.SureBtnCallBak?.Invoke();

            self.HideDlgArcade().Coroutine();
        }

        public static void OnBGClick(this DlgArcadeCoin self)
        {
            self.HideDlgArcade().Coroutine();
        }

        public static void OnPayQRCodeBGClick(this DlgArcadeCoin self)
        {
            self.View.EG_PayQRCodeRootRectTransform.SetVisible(false);
        }

        public static async ETTask HideDlgArcade(this DlgArcadeCoin self)
        {
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgArcadeCoin>();
        }

        //加减法按钮监听函数
        public static void OnAddClick(this DlgArcadeCoin self)
        {
            self.UpdateArcadeCoin(self.arcadeCoinNum + 1);
        }

        public static void OnSubClick(this DlgArcadeCoin self)
        {
            self.UpdateArcadeCoin(self.arcadeCoinNum - 1);
        }
    }
}