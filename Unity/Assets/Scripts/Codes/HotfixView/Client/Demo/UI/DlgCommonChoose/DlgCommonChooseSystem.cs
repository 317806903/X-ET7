using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Invoke(TimerInvokeType.DlgCommonChooseTimer)]
    public class DlgCommonChooseTimer: ATimer<DlgCommonChoose>
    {
        protected override void Run(DlgCommonChoose self)
        {
            try
            {
                if (self.IsDisposed)
                {
                    TimerComponent.Instance?.Remove(ref self.Timer);
                    return;
                }
                self.Update();
            }
            catch (Exception e)
            {
                Log.Error($"DlgCommonChoose timer error: {self.Id}\n{e}");
            }
        }
    }

    [FriendOf(typeof(DlgCommonChoose))]
    public static class DlgCommonChooseSystem
    {
        public static void RegisterUIEvent(this DlgCommonChoose self)
        {
        }

        public static void ShowWindow(this DlgCommonChoose self, ShowWindowData contextData = null)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.PopUp);
            bool isARCameraEnable = ET.Client.ARSessionHelper.ChkARCameraEnable(self.DomainScene());
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

        public static void HideWindow(this DlgCommonChoose self)
        {
            TimerComponent.Instance?.Remove(ref self.Timer);
        }

        public static void Close(this DlgCommonChoose self)
        {
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgCommonChoose>();
        }

        public static void Update(this DlgCommonChoose self)
        {
            long leftTime = self.timeoutTime - TimeHelper.ClientNow();
            if (leftTime <= 0)
            {
                leftTime = 0;
                self.DealTimeOut();
                return;
            }

            int time = (int)(leftTime * 0.001f);
            self.View.E_LimitTimeTextTextMeshProUGUI.text = string.Format(self.timeoutMsg, time);
        }

        public static void DealTimeOut(this DlgCommonChoose self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

            self.timeOutCallBack?.Invoke();
            if (self.isCloseAfterChoose)
            {
                self.Close();
            }
        }

        public static void SetDefaultText(this DlgCommonChoose self)
        {
            string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_CommonChoose_Title");
            self.View.E_TitleTextTextMeshProUGUI.text = msg;
            msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_CommonChoose_Confirm");
            self.View.E_SureTextTextMeshProUGUI.text = msg;
            msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_CommonChoose_Cancel");
            self.View.E_CancelTextTextMeshProUGUI.text = msg;

            self.Update();
        }

        public static void ShowWhenNoChoose(this DlgCommonChoose self, string showMsg, string timeoutMsg, float timeoutTime, Action confirmCallBack, string titleText = null, bool isCloseAfterChoose = true)
        {
            self.timeoutMsg = timeoutMsg;
            self.timeoutTime = TimeHelper.ClientNow() + (int)(timeoutTime * 1000);
            self.confirmCallBack = confirmCallBack;
            self.timeOutCallBack = self.confirmCallBack;
            self.isCloseAfterChoose = isCloseAfterChoose;

            self.SetDefaultText();
            self.View.E_ConfirmSureButton.SetVisible(false);
            self.View.E_ConfirmCancelButton.SetVisible(false);

            self.View.E_TextTextMeshProUGUI.text = showMsg;

            if (string.IsNullOrEmpty(titleText) == false)
            {
                self.View.E_TitleTextTextMeshProUGUI.text = titleText;
            }

            self.View.E_BG_ClickButton.AddListener(null);


            TimerComponent.Instance?.Remove(ref self.Timer);
            self.Timer = TimerComponent.Instance.NewRepeatedTimer(500, TimerInvokeType.DlgCommonChooseTimer, self);
        }

        public static void ShowWhenOneChoose(this DlgCommonChoose self, string showMsg, string timeoutMsg, float timeoutTime, Action confirmCallBack, string confirmText = null, string titleText = null, bool isCloseAfterChoose = true)
        {
            self.timeoutMsg = timeoutMsg;
            self.timeoutTime = TimeHelper.ClientNow() + (int)(timeoutTime * 1000);
            self.confirmCallBack = confirmCallBack;
            self.timeOutCallBack = self.confirmCallBack;
            self.isCloseAfterChoose = isCloseAfterChoose;

            self.SetDefaultText();

            self.View.E_ConfirmSureButton.SetVisible(true);
            self.View.E_ConfirmCancelButton.SetVisible(false);

            self.View.E_TextTextMeshProUGUI.text = showMsg;

            if (string.IsNullOrEmpty(confirmText) == false)
            {
                self.View.E_SureTextTextMeshProUGUI.text = confirmText;
            }

            if (string.IsNullOrEmpty(titleText) == false)
            {
                self.View.E_TitleTextTextMeshProUGUI.text = titleText;
            }
            self.View.E_BG_ClickButton.AddListener(null);

            self.View.E_ConfirmSureButton.AddListener(() =>
            {
                UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

                self.confirmCallBack?.Invoke();
                if (self.isCloseAfterChoose)
                {
                    self.Close();
                }
            });

            TimerComponent.Instance?.Remove(ref self.Timer);
            self.Timer = TimerComponent.Instance.NewRepeatedTimer(500, TimerInvokeType.DlgCommonChooseTimer, self);
        }

        public static void ShowWhenTwoChoose(this DlgCommonChoose self, string showMsg, string timeoutMsg, float timeoutTime, Action confirmCallBack, Action cancelCallBack, string confirmText = null, string cancelText = null, string titleText = null, bool isTimeOutConfirm = true, bool isCloseAfterChoose = true)
        {
            self.timeoutMsg = timeoutMsg;
            self.timeoutTime = TimeHelper.ClientNow() + (int)(timeoutTime * 1000);
            self.confirmCallBack = confirmCallBack;
            self.cancelCallBack = cancelCallBack;
            self.isTimeOutConfirm = isTimeOutConfirm;
            self.isCloseAfterChoose = isCloseAfterChoose;
            if (self.isTimeOutConfirm)
            {
                self.timeOutCallBack = self.confirmCallBack;
            }
            else
            {
                self.timeOutCallBack = self.cancelCallBack;
            }

            self.SetDefaultText();
            self.View.E_ConfirmSureButton.SetVisible(true);
            self.View.E_ConfirmCancelButton.SetVisible(true);

            self.View.E_TextTextMeshProUGUI.text = showMsg;

            if (string.IsNullOrEmpty(confirmText) == false)
            {
                self.View.E_SureTextTextMeshProUGUI.text = confirmText;
            }

            if (string.IsNullOrEmpty(cancelText) == false)
            {
                self.View.E_CancelTextTextMeshProUGUI.text = cancelText;
            }

            if (string.IsNullOrEmpty(titleText) == false)
            {
                self.View.E_TitleTextTextMeshProUGUI.text = titleText;
            }
            self.View.E_BG_ClickButton.AddListener(null);

            self.View.E_ConfirmSureButton.AddListener(() =>
            {
                UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

                self.confirmCallBack?.Invoke();
                if (self.isCloseAfterChoose)
                {
                    self.Close();
                }
            });
            self.View.E_ConfirmCancelButton.AddListener(() =>
            {
                UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

                self.cancelCallBack?.Invoke();
                if (self.isCloseAfterChoose)
                {
                    self.Close();
                }
            });

            TimerComponent.Instance?.Remove(ref self.Timer);
            self.Timer = TimerComponent.Instance.NewRepeatedTimer(500, TimerInvokeType.DlgCommonChooseTimer, self);
        }

    }
}
