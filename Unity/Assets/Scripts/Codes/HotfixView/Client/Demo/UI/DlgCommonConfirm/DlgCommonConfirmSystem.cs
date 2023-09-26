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
        }

        public static void Close(this DlgCommonConfirm self)
        {
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgCommonConfirm>();
        }

        public static void ShowConfirmNoClose(this DlgCommonConfirm self, string confirmMsg)
        {
            self.View.EG_SureRectTransform.gameObject.SetActive(true);
            self.View.EG_ConfirmRectTransform.gameObject.SetActive(false);
            self.View.E_TextTextMeshProUGUI.text = confirmMsg;
            self.View.EGBackGroundButton.AddListener(null);
            self.View.E_SureButton.AddListener(null);
        }

        public static void ShowConfirm(this DlgCommonConfirm self, string confirmMsg, Action confirmCallBack)
        {
            self.View.EG_SureRectTransform.gameObject.SetActive(true);
            self.View.EG_ConfirmRectTransform.gameObject.SetActive(false);
            self.View.E_TextTextMeshProUGUI.text = confirmMsg;
            self.View.EGBackGroundButton.AddListener(() =>
            {
                ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioConfirm(self.DomainScene());

                self.Close();
                confirmCallBack?.Invoke();
            });
            self.View.E_SureButton.AddListener(() =>
            {
                ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioConfirm(self.DomainScene());

                self.Close();
                confirmCallBack?.Invoke();
            });
        }

        public static void ShowConfirm(this DlgCommonConfirm self, string confirmMsg, Action confirmCallBack, Action cancelCallBack)
        {
            self.View.EG_SureRectTransform.gameObject.SetActive(false);
            self.View.EG_ConfirmRectTransform.gameObject.SetActive(true);
            self.View.E_TextTextMeshProUGUI.text = confirmMsg;
            if (cancelCallBack == null)
            {
                self.View.EGBackGroundButton.AddListener(()=>
                {
                    ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioBack(self.DomainScene());
                    self.Close();
                });
            }
            else
            {
                self.View.EGBackGroundButton.AddListener(null);
            }
            self.View.E_ConfirmSureButton.AddListener(() =>
            {
                ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioConfirm(self.DomainScene());

                self.Close();
                confirmCallBack?.Invoke();
            });
            self.View.E_ConfirmCancelButton.AddListener(() =>
            {
                ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioBack(self.DomainScene());

                self.Close();
                cancelCallBack?.Invoke();
            });
        }
    }
}