using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof (DlgCommonConfirmHighest))]
    public static class DlgCommonConfirmHighestSystem
    {
        public static void RegisterUIEvent(this DlgCommonConfirmHighest self)
        {
        }

        public static void ShowWindow(this DlgCommonConfirmHighest self, ShowWindowData contextData = null)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.PopUp);
        }

        public static void HideWindow(this DlgCommonConfirmHighest self)
        {
        }

        public static void Close(this DlgCommonConfirmHighest self)
        {
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgCommonConfirmHighest>();
        }

        public static void SetDefaultText(this DlgCommonConfirmHighest self)
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

        public static void ShowConfirmNoClose(this DlgCommonConfirmHighest self, string confirmMsg, string sureText = null, string titleText = null)
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
            if (string.IsNullOrEmpty(titleText) == false)
            {
                self.View.E_TitleTextTextMeshProUGUI.text = titleText;
            }

            self.View.E_BG_ClickButton.AddListener(null);
            self.View.E_SureButton.AddListener(null);
        }

        public static void ShowOnlyConfirm(this DlgCommonConfirmHighest self, string confirmMsg, Action confirmCallBack, string sureText = null, string titleText = null)
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

            if (string.IsNullOrEmpty(titleText) == false)
            {
                self.View.E_TitleTextTextMeshProUGUI.text = titleText;
            }

            // self.View.E_BG_ClickButton.AddListener(() =>
            // {
            //     UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);
            //
            //     self.Close();
            //     confirmCallBack?.Invoke();
            // });
            self.View.E_SureButton.AddListener(() =>
            {
                UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

                self.Close();
                confirmCallBack?.Invoke();
            });
        }

        public static void ShowConfirm(this DlgCommonConfirmHighest self, string confirmMsg, Action confirmCallBack, Action cancelCallBack, string sureText = null, string cancelText = null, string titleText = null)
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
                    //UIAudioManagerHelper.PlayUIAudio(self.DomainScene(),SoundEffectType.Back);
                    self.Close();
                });
            }
            else
            {
                self.View.E_BG_ClickButton.AddListener(null);
            }
            self.View.E_ConfirmSureButton.AddListener(() =>
            {
                UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

                self.Close();
                confirmCallBack?.Invoke();
            });
            self.View.E_ConfirmCancelButton.AddListener(() =>
            {
                UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

                self.Close();
                cancelCallBack?.Invoke();
            });
        }
    }
}