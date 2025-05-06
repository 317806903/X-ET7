using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof(DlgCommonConfirm))]
    public static class DlgCommonConfirmSystem
    {
        // 注册 UI 事件
        public static void RegisterUIEvent(this DlgCommonConfirm self)
        {
        }

        // 显示通用确认对话框窗口
        public static async ETTask ShowWindow(this DlgCommonConfirm self, ShowWindowData contextData = null)
        {
            // 播放 UI 弹出音效
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.PopUp);
            // 检查 AR 相机是否启用
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

        // 隐藏通用确认对话框窗口
        public static void HideWindow(this DlgCommonConfirm self)
        {
        }

        // 关闭通用确认对话框
        public static void Close(this DlgCommonConfirm self)
        {
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgCommonConfirm>();
        }

        public static bool ChkCanClickBg(this DlgCommonConfirm self)
        {
            if (self.dlgShowTime < TimeHelper.ClientNow() - (long)(1000 * 1f))
            {
                return true;
            }
            return false;
        }

        // 设置默认文本
        public static void SetDefaultText(this DlgCommonConfirm self)
        {
            self.dlgShowTime = TimeHelper.ClientNow();

            string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_Title");
            self.View.E_TitleTextTextMeshProUGUI.text = msg;
            msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_Confirm");
            self.View.E_SureTextTextMeshProUGUI.text = msg;
            msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_Confirm");
            self.View.E_OnlySureTextTextMeshProUGUI.text = msg;
            msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_Cancel");
            self.View.E_CancelTextTextMeshProUGUI.text = msg;
        }

        public static void SetDetailText(this DlgCommonConfirm self, string detailText)
        {
            self.View.E_TextSimpleTextMeshProUGUI.SetVisible(true);
            self.View.E_TextSimpleTextMeshProUGUI.text = detailText;
            LayoutRebuilder.ForceRebuildLayoutImmediate(self.View.E_TextSimpleTextMeshProUGUI.rectTransform);
            float sizeRootY = self.View.EGDetailRootRectTransform.sizeDelta.y;
            float sizeTextY = self.View.E_TextSimpleTextMeshProUGUI.preferredHeight;
            if (sizeRootY < sizeTextY)
            {
                self.View.E_TextSimpleTextMeshProUGUI.SetVisible(false);
                self.View.EGDetailScrollRectTransform.SetVisible(true);
                self.View.E_TextTextMeshProUGUI.text = detailText;
            }
            else
            {
                self.View.E_TextSimpleTextMeshProUGUI.SetVisible(true);
                self.View.EGDetailScrollRectTransform.SetVisible(false);
            }
        }

        // 显示通用确认对话框，但不关闭
        public static void ShowConfirmNoClose(this DlgCommonConfirm self, string confirmMsg, string sureText = null, string titleText = null)
        {
            // 设置默认文本
            self.SetDefaultText();
            // 激活确认按钮，禁用取消按钮
            self.View.EG_SureRectTransform.gameObject.SetActive(true);
            self.View.EG_ConfirmRectTransform.gameObject.SetActive(false);
            // 设置确认消息文本
            self.SetDetailText(confirmMsg);
            // 如果指定了确认按钮的文本，则设置确认按钮和仅确认文本的文本
            if (string.IsNullOrEmpty(sureText) == false)
            {
                self.View.E_SureTextTextMeshProUGUI.text = sureText;
                self.View.E_OnlySureTextTextMeshProUGUI.text = sureText;
            }
            // 如果指定了对话框标题的文本，则设置对话框标题的文本
            if (string.IsNullOrEmpty(titleText) == false)
            {
                self.View.E_TitleTextTextMeshProUGUI.text = titleText;
            }
            // 清除确认按钮和背景点击按钮的监听器
            self.View.E_BG_ClickButton.AddListener(null);
            self.View.E_SureButton.AddListener(null);
        }

        /// <summary>
        /// 在通用确认对话框上显示仅有确认按钮的内容，并设置相应的文本和回调。
        /// </summary>
        /// <param name="self">通用确认对话框对象</param>
        /// <param name="confirmMsg">要显示的确认消息</param>
        /// <param name="confirmCallBack">确认按钮点击后执行的回调方法</param>
        /// <param name="sureText">确认按钮的文本（可选，默认为null）</param>
        /// <param name="titleText">对话框标题的文本（可选，默认为null）</param>
        public static void ShowOnlyConfirm(this DlgCommonConfirm self, string confirmMsg, Action confirmCallBack, string sureText = null, string titleText = null)
        {
            // 设置默认文本
            self.SetDefaultText();
            // 激活确认按钮，禁用取消按钮
            self.View.EG_SureRectTransform.gameObject.SetActive(true);
            self.View.EG_ConfirmRectTransform.gameObject.SetActive(false);
            // 设置确认消息文本
            self.SetDetailText(confirmMsg);
            // 如果指定了确认按钮的文本，则设置确认按钮和仅确认文本的文本
            if (string.IsNullOrEmpty(sureText) == false)
            {
                self.View.E_SureTextTextMeshProUGUI.text = sureText;
                self.View.E_OnlySureTextTextMeshProUGUI.text = sureText;
            }
            // 如果指定了对话框标题的文本，则设置对话框标题的文本
            if (string.IsNullOrEmpty(titleText) == false)
            {
                self.View.E_TitleTextTextMeshProUGUI.text = titleText;
            }
            self.View.E_BG_ClickButton.AddListener(null);
            // 添加确认按钮点击事件的监听器
            self.View.E_SureButton.AddListener(() =>
            {
                // 播放确认按钮点击的 UI 音效
                UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);
                // 关闭对话框并执行确认回调方法
                self.Close();
                confirmCallBack?.Invoke();
            });
        }

        // 显示带有确认和取消按钮的通用确认对话框
        public static void ShowConfirm(this DlgCommonConfirm self, string confirmMsg, Action confirmCallBack, Action cancelCallBack, string sureText = null, string cancelText = null, string titleText = null)
        {
            // 设置默认文本
            self.SetDefaultText();
            // 激活取消按钮，禁用确认按钮
            self.View.EG_SureRectTransform.gameObject.SetActive(false);
            self.View.EG_ConfirmRectTransform.gameObject.SetActive(true);
            // 设置确认消息文本
            self.SetDetailText(confirmMsg);
            // 如果指定了确认按钮的文本，则设置确认按钮和仅确认文本的文本
            if (string.IsNullOrEmpty(sureText) == false)
            {
                self.View.E_SureTextTextMeshProUGUI.text = sureText;
                self.View.E_OnlySureTextTextMeshProUGUI.text = sureText;
            }
            // 如果指定了取消按钮的文本，则设置取消按钮的文本
            if (string.IsNullOrEmpty(cancelText) == false)
            {
                self.View.E_CancelTextTextMeshProUGUI.text = cancelText;
            }
            // 如果指定了对话框标题的文本，则设置对话框标题的文本
            if (string.IsNullOrEmpty(titleText) == false)
            {
                self.View.E_TitleTextTextMeshProUGUI.text = titleText;
            }
            // 如果取消回调为空，则关闭对话框
            if (cancelCallBack == null)
            {
                self.View.E_BG_ClickButton.AddListener(() =>
                {
                    if (self.ChkCanClickBg() == false)
                    {
                        return;
                    }
                    // 关闭对话框
                    self.Close();
                });
            }
            else
            {
                // 否则清除背景点击按钮的监听器
                self.View.E_BG_ClickButton.AddListener(null);
            }
            // 添加确认按钮点击事件的监听器
            self.View.E_ConfirmSureButton.AddListener(() =>
            {
                // 播放确认按钮点击的 UI 音效
                UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);
                // 关闭对话框并执行确认回调方法
                self.Close();
                confirmCallBack?.Invoke();
            });
            // 添加取消按钮点击事件的监听器
            self.View.E_ConfirmCancelButton.AddListener(() =>
            {
                // 播放取消按钮点击的 UI 音效
                UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);
                // 关闭对话框并执行取消回调方法
                self.Close();
                cancelCallBack?.Invoke();
            });
        }
    }
}
