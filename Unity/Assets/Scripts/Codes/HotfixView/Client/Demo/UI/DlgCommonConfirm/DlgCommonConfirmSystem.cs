using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof (DlgCommonConfirm))]
    public static class DlgCommonConfirmSystem
    {
        public static void RegisterUIEvent(this DlgCommonConfirm self)
        {
        }

        public static void ShowWindow(this DlgCommonConfirm self, ShowWindowData contextData = null)
        {
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

        public static void Close(this DlgCommonConfirm self)
        {
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgCommonConfirm>();
        }

        public static void SetDefaultText(this DlgCommonConfirm self)
        {
            string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_Title");
            self.View.E_TitleTextTextMeshProUGUI.text = msg;
            msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_Confirm");
            self.View.E_SureTextTextMeshProUGUI.text = msg;
            msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_Confirm");
            self.View.E_OnlySureTextTextMeshProUGUI.text = msg;
            msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_Cancel");
            self.View.E_CancelTextTextMeshProUGUI.text = msg;
        }

        public static void ShowConfirmNoClose(this DlgCommonConfirm self, string confirmMsg, string sureText = null, string cancelText = null, string titleText = null)
        {
            self.SetDefaultText();
            self.View.EG_SureRectTransform.gameObject.SetActive(true);
            self.View.EG_ConfirmRectTransform.gameObject.SetActive(false);
            self.View.E_TextTextMeshProUGUI.text = confirmMsg;

            if (string.IsNullOrEmpty(sureText) == false)
            {
                self.View.E_SureTextTextMeshProUGUI.text = sureText;
                self.View.E_OnlySureTextTextMeshProUGUI.text = sureText;
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
            self.View.E_SureButton.AddListener(null);
        }

        public static void ShowOnlyConfirm(this DlgCommonConfirm self, string confirmMsg, Action confirmCallBack, string sureText = null, string cancelText = null, string titleText = null)
        {
            self.SetDefaultText();
            self.View.EG_SureRectTransform.gameObject.SetActive(true);
            self.View.EG_ConfirmRectTransform.gameObject.SetActive(false);
            self.View.E_TextTextMeshProUGUI.text = confirmMsg;

            if (string.IsNullOrEmpty(sureText) == false)
            {
                self.View.E_SureTextTextMeshProUGUI.text = sureText;
                self.View.E_OnlySureTextTextMeshProUGUI.text = sureText;
            }
            if (string.IsNullOrEmpty(cancelText) == false)
            {
                self.View.E_CancelTextTextMeshProUGUI.text = cancelText;
            }
            if (string.IsNullOrEmpty(titleText) == false)
            {
                self.View.E_TitleTextTextMeshProUGUI.text = titleText;
            }

            // self.View.E_BG_ClickButton.AddListener(() =>
            // {
            //     UIAudioManagerHelper.PlayUIAudioConfirm(self.DomainScene());
            //
            //     self.Close();
            //     confirmCallBack?.Invoke();
            // });
            self.View.E_SureButton.AddListener(() =>
            {
                UIAudioManagerHelper.PlayUIAudioConfirm(self.DomainScene());

                self.Close();
                confirmCallBack?.Invoke();
            });
        }

        public static void ShowConfirm(this DlgCommonConfirm self, string confirmMsg, Action confirmCallBack, Action cancelCallBack, string sureText = null, string cancelText = null, string titleText = null)
        {
            self.SetDefaultText();
            self.View.EG_SureRectTransform.gameObject.SetActive(false);
            self.View.EG_ConfirmRectTransform.gameObject.SetActive(true);
            self.View.E_TextTextMeshProUGUI.text = confirmMsg;

            if (string.IsNullOrEmpty(sureText) == false)
            {
                self.View.E_SureTextTextMeshProUGUI.text = sureText;
                self.View.E_OnlySureTextTextMeshProUGUI.text = sureText;
            }
            if (string.IsNullOrEmpty(cancelText) == false)
            {
                self.View.E_CancelTextTextMeshProUGUI.text = cancelText;
            }
            if (string.IsNullOrEmpty(titleText) == false)
            {
                self.View.E_TitleTextTextMeshProUGUI.text = titleText;
            }

            if (cancelCallBack == null)
            {
                self.View.E_BG_ClickButton.AddListener(()=>
                {
                    UIAudioManagerHelper.PlayUIAudioBack(self.DomainScene());
                    self.Close();
                });
            }
            else
            {
                self.View.E_BG_ClickButton.AddListener(null);
            }
            self.View.E_ConfirmSureButton.AddListener(() =>
            {
                UIAudioManagerHelper.PlayUIAudioConfirm(self.DomainScene());

                self.Close();
                confirmCallBack?.Invoke();
            });
            self.View.E_ConfirmCancelButton.AddListener(() =>
            {
                UIAudioManagerHelper.PlayUIAudioBack(self.DomainScene());

                self.Close();
                cancelCallBack?.Invoke();
            });
        }
    }
}