using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof (DlgPassword))]
    public static class DlgPasswordSystem
    {
        public static void RegisterUIEvent(this DlgPassword self)
        {
            self.View.E_BG_ClickButton.AddListener(self.OnBGClick);
        }

        public static async ETTask ShowWindow(this DlgPassword self, ShowWindowData contextData = null)
        {
            self.ShowBg().Coroutine();
        }

        public static void HideWindow(this DlgPassword self)
        {
            //清除数据
            self.View.E_InputFieldPassTMP_InputField.text = null;
        }

        // 设置默认文本
        public static void SetDefaultText(this DlgPassword self)
        {
            string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Password_Title");
            self.View.E_DigPassTitleTextMeshProUGUI.text = msg;
        }

        public static void ShowPassword(this DlgPassword self, string msgStr, string passwordStr, Action SureBtnCallBak, string titleStr = null)
        {
            self.SetDefaultText();

            self.View.ELabel_MsgTextMeshProUGUI.SetText(msgStr);

            if (string.IsNullOrEmpty(titleStr) == false)
            {
                self.View.E_DigPassTitleTextMeshProUGUI.SetText(titleStr);
            }

            self.View.EButton_OKButton.AddListener(() =>
            {
                UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

                //检测输入的密码和正确密码是否一致
                if (self.IsPasswordRight(passwordStr))
                {
                    SureBtnCallBak?.Invoke();
                    self.OnBGClick();
                }
                //密码错误
                else
                {
                    string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_PasswordError");
                    UIManagerHelper.ShowTip(self.DomainScene(), msg);
                }
            });
        }

        public static async ETTask ShowBg(this DlgPassword self)
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

        public static void OnBGClick(this DlgPassword self)
        {
            self.HideDlgPassword().Coroutine();
        }

        public static async ETTask HideDlgPassword(this DlgPassword self)
        {
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgPassword>();
        }

        public static bool IsPasswordRight(this DlgPassword self, string passwordStr)
        {
            return self.View.E_InputFieldPassTMP_InputField.text.Equals(passwordStr);
        }
    }
}