using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using ET.AbilityConfig;

namespace ET.Client
{   //主页设置面板
    [FriendOf(typeof(DlgGameModeSetting))]
    public static class DlgGameModeSettingSystem
    {
        //控件事件监听
        public static void RegisterUIEvent(this DlgGameModeSetting self)
        {
            // 背景与关闭按钮
            self.View.E_BG_ClickButton.AddListener(() =>
            {
                if (self.ChkCanClickBg() == false)
                {
                    return;
                }
                self.OnBGClick();
            });
            self.View.E_BtnCloseButton.AddListener(self.OnBGClick);

            //Community按钮
            self.View.E_DiscordButton.AddListener(self.ClickDiscord);
            self.View.E_PrivacyPolicyButton.AddListener(self.ClickPrivacyPolicy);

            // 音乐开关
            EventTriggerListener.Get(self.View.EG_Button_MusicRectTransform.gameObject).onClick.AddListener((go, xx) =>
            {
                self.ChgStatus_Music();
            });

            // 音效开关
            EventTriggerListener.Get(self.View.EG_Button_AudioRectTransform.gameObject).onClick.AddListener((go, xx) =>
            {
                self.ChgStatus_Audio();
            });

            // 伤害显示开关
            EventTriggerListener.Get(self.View.EG_Button_DamagerShowRectTransform.gameObject).onClick.AddListener((go, xx) =>
            {
                self.ChgStatus_DamageShow();
            });

            //教程按钮
            self.View.E_ButtonTutorialsButton.AddListenerAsync(self.ClickTutorial);

            //语言选择
            self.View.E_btn_LanguageButton.AddListenerAsync(self.OnClickLanugage);

        }

        // 显示
        public static async ETTask ShowWindow(this DlgGameModeSetting self, ShowWindowData contextData = null)
        {
            self.dlgShowTime = TimeHelper.ClientNow();

            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.PopUp);
            self.ShowBg();
            self.SetSwitchOnOffUI();
            await self.SetCurLanguageText();
        }

        public static bool ChkCanClickBg(this DlgGameModeSetting self)
        {
            if (self.dlgShowTime < TimeHelper.ClientNow() - (long)(1000 * 1f))
            {
                return true;
            }
            return false;
        }

        // 检查 AR 相机是否启用后设置不同的背景
        public static void ShowBg(this DlgGameModeSetting self)
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

        // 隐藏窗口
        public static void HideWindow(this DlgGameModeSetting self)
        {
        }

        #region  控制UI控件的显隐
        public static void SetSwitchOnOffUI(this DlgGameModeSetting self)
        {
            self.SetSwitchOnOff_Music();
            self.SetSwitchOnOff_Audio();
            self.SetSwitchOnOff_DamageShow();
        }
        // 设置音乐开关UI
        public static void SetSwitchOnOff_Music(this DlgGameModeSetting self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);
            bool isOn = GameSettingComponent.Instance.GetIsOn(GameSettingType.Music);
            self.View.EG_Music_OnRectTransform.SetVisible(isOn);
            self.View.EG_Music_OffRectTransform.SetVisible(!isOn);
        }
        // 设置音频开关UI
        public static void SetSwitchOnOff_Audio(this DlgGameModeSetting self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);
            bool isOn = GameSettingComponent.Instance.GetIsOn(GameSettingType.Audio);
            self.View.EG_Audio_OnRectTransform.SetVisible(isOn);
            self.View.EG_Audio_OffRectTransform.SetVisible(!isOn);
        }
        // 设置伤害显示开关UI
        public static void SetSwitchOnOff_DamageShow(this DlgGameModeSetting self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);
            bool isOn = GameSettingComponent.Instance.GetIsOn(GameSettingType.DamageShow);
            self.View.EG_DamageShow_OnRectTransform.SetVisible(isOn);
            self.View.EG_DamageShow_OffRectTransform.SetVisible(!isOn);
        }
        #endregion

        #region 事件监听函数
        // 改变音乐状态
        public static void ChgStatus_Music(this DlgGameModeSetting self)
        {
            bool isOn = GameSettingComponent.Instance.GetIsOn(GameSettingType.Music);
            GameSettingComponent.Instance.SetIsOn(GameSettingType.Music, !isOn);
            UIAudioManagerHelper.ResetMusicStatus(self.DomainScene());
            self.SetSwitchOnOffUI();
        }
        // 改变音频状态
        public static void ChgStatus_Audio(this DlgGameModeSetting self)
        {
            bool isOn = GameSettingComponent.Instance.GetIsOn(GameSettingType.Audio);
            GameSettingComponent.Instance.SetIsOn(GameSettingType.Audio, !isOn);
            self.SetSwitchOnOffUI();
        }
        // 改变伤害显示状态
        public static void ChgStatus_DamageShow(this DlgGameModeSetting self)
        {
            bool isOn = GameSettingComponent.Instance.GetIsOn(GameSettingType.DamageShow);
            GameSettingComponent.Instance.SetIsOn(GameSettingType.DamageShow, !isOn);
            self.SetSwitchOnOffUI();
        }
        // 当背景被点击时，关闭窗口
        public static void OnBGClick(this DlgGameModeSetting self)
        {
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameModeSetting>();

        }
        // 教程按钮
        public static async ETTask ClickTutorial(this DlgGameModeSetting self)
        {
            self.TrackFunctionClicked("tutorial");
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

            await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgTutorials>();
        }
        // 社区（Community）按钮
        public static void ClickDiscord(this DlgGameModeSetting self)
        {
            self.TrackFunctionClicked("discord");
            string url = ChannelSettingComponent.Instance.GetDiscordURL();
            ET.Client.UIManagerHelper.ShowUrl(self.DomainScene(), url);
        }

        public static void ClickPrivacyPolicy(this DlgGameModeSetting self)
        {
            self.TrackFunctionClicked("PrivacyPolicy");
            string url = ChannelSettingComponent.Instance.GetPrivacyPolicyURL();
            ET.Client.UIManagerHelper.ShowUrl(self.DomainScene(), url);
        }


        #endregion


        public static void TrackFunctionClicked(this DlgGameModeSetting self, string name)
        {
            EventSystem.Instance.Publish(self.DomainScene(), new ClientEventType.NoticeEventLogging()
            {
                eventName = "FunctionClicked",
                properties = new()
                {
                    {"function_name", name},
                }
            });
        }

        //语言选择按钮
        public static async ETTask OnClickLanugage(this DlgGameModeSetting self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);
            UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindow<DlgLanguageChoose>();
            await ETTask.CompletedTask;
        }
        //显示当前的语言类型
        public static async ETTask SetCurLanguageText(this DlgGameModeSetting self)
        {
            LanguageType curLanguageType = LocalizeComponent.Instance.CurrentLanguage;
            self.SetLanguageText(curLanguageType);
            await ETTask.CompletedTask;
        }

        public static void SetLanguageText(this DlgGameModeSetting self, LanguageType languageType)
        {
            string keyCode = $"TextCode_key_LanguageChoose_{languageType.ToString()}";
            string languageText = LocalizeComponent.Instance.GetTextValue(keyCode);
            self.View.E_LanguageTextTextMeshProUGUI.text = languageText;            
        }
    }
}
