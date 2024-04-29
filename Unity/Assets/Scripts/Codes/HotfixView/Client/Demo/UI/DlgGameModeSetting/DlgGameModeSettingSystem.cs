using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof(DlgGameModeSetting))]
    public static class DlgGameModeSettingSystem
    {
        public static void RegisterUIEvent(this DlgGameModeSetting self)
        {
            self.View.E_BG_ClickButton.AddListener(self.OnBGClick);

            EventTriggerListener.Get(self.View.EG_Toggle_MusicRectTransform.gameObject).onClick.AddListener((go, xx) =>
            {
                self.ChgStatus_Music();
            });

            EventTriggerListener.Get(self.View.EG_Toggle_AudioRectTransform.gameObject).onClick.AddListener((go, xx) =>
            {
                self.ChgStatus_Audio();
            });

            EventTriggerListener.Get(self.View.EG_Toggle_DamageShowRectTransform.gameObject).onClick.AddListener((go, xx) =>
            {
                self.ChgStatus_DamageShow();
            });

        }

        public static void ShowWindow(this DlgGameModeSetting self, ShowWindowData contextData = null)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.PopUp);

            self.ShowBg();
            self.SetSwitchOnOffUI();
        }

        public static void ShowBg(this DlgGameModeSetting self)
        {
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

        public static void OnBGClick(this DlgGameModeSetting self)
        {
            self.Close();
        }

        public static void HideWindow(this DlgGameModeSetting self)
        {
        }

        public static void Close(this DlgGameModeSetting self)
        {
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameModeSetting>();
        }

        public static void SetSwitchOnOffUI(this DlgGameModeSetting self)
        {
            self.SetSwitchOnOff_Music();
            self.SetSwitchOnOff_Audio();
            self.SetSwitchOnOff_DamageShow();
        }

        public static void SetSwitchOnOff_Music(this DlgGameModeSetting self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

            bool isOn = GameSettingComponent.Instance.GetIsOn(GameSettingType.Music);

            self.View.EG_Music_OnRectTransform.SetVisible(isOn);
            self.View.EG_Music_OffRectTransform.SetVisible(!isOn);
        }

        public static void SetSwitchOnOff_Audio(this DlgGameModeSetting self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

            bool isOn = GameSettingComponent.Instance.GetIsOn(GameSettingType.Audio);

            self.View.EG_Audio_OnRectTransform.SetVisible(isOn);
            self.View.EG_Audio_OffRectTransform.SetVisible(!isOn);
        }

        public static void SetSwitchOnOff_DamageShow(this DlgGameModeSetting self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

            bool isOn = GameSettingComponent.Instance.GetIsOn(GameSettingType.DamageShow);

            self.View.EG_DamageShow_OnRectTransform.SetVisible(isOn);
            self.View.EG_DamageShow_OffRectTransform.SetVisible(!isOn);
        }

        public static void ChgStatus_Music(this DlgGameModeSetting self)
        {
            bool isOn = GameSettingComponent.Instance.GetIsOn(GameSettingType.Music);
            GameSettingComponent.Instance.SetIsOn(GameSettingType.Music, !isOn);

            UIAudioManagerHelper.ResetMusicStatus(self.DomainScene());

            self.SetSwitchOnOffUI();
        }

        public static void ChgStatus_Audio(this DlgGameModeSetting self)
        {
            bool isOn = GameSettingComponent.Instance.GetIsOn(GameSettingType.Audio);
            GameSettingComponent.Instance.SetIsOn(GameSettingType.Audio, !isOn);

            self.SetSwitchOnOffUI();
        }

        public static void ChgStatus_DamageShow(this DlgGameModeSetting self)
        {
            bool isOn = GameSettingComponent.Instance.GetIsOn(GameSettingType.DamageShow);
            GameSettingComponent.Instance.SetIsOn(GameSettingType.DamageShow, !isOn);

            self.SetSwitchOnOffUI();
        }

    }
}
