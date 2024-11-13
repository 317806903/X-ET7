using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using ET.Client;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Ability.Client
{
    [FriendOf(typeof (Unit))]
    public static class AudioPlayHelper
    {
        public static AudioPlayObj PlayAudio(Unit unit, string playAudioActionId)
        {
            if (string.IsNullOrEmpty(playAudioActionId))
            {
                return null;
            }
            ActionCfg_PlayAudio actionCfg_PlayAudio = ActionCfg_PlayAudioCategory.Instance.Get(playAudioActionId);
            return PlayAudio(unit, actionCfg_PlayAudio);
        }

        public static AudioPlayObj PlayAudio(Unit unit, ActionCfg_PlayAudio actionCfg_PlayAudio)
        {
            if (GameSettingComponent.Instance.GetIsOn(GameSettingType.Audio) == false)
            {
                return null;
            }

            AudioPlayComponent audioPlayComponent = unit.GetComponent<AudioPlayComponent>();
            return audioPlayComponent.AddPlayAudioObj(actionCfg_PlayAudio);
        }

        // public static void StopAudio(Unit unit)
        // {
        //     AudioPlayComponent audioPlayComponent = unit.GetComponent<AudioPlayComponent>();
        //     audioPlayComponent.RemoveEffectByKey();
        // }

        public static void PlayVibrate()
        {
            Handheld.Vibrate();
        }

        public static void PlayVibrate(float time)
        {
#if UNITY_ANDROID || UNITY_IOS
            if (Application.platform == RuntimePlatform.Android)
            {
                if (CheckVibratePermission())
                {
                    _PlayVibrateWhenAndroid(time);
                }
                else
                {
                    RequestVibratePermission();
                }
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                Handheld.Vibrate();
            }
#endif
        }

        public static bool CheckVibratePermission()
        {
            return UnityEngine.Android.Permission.HasUserAuthorizedPermission("android.permission.VIBRATE");
        }

        public static void RequestVibratePermission()
        {
            UnityEngine.Android.Permission.RequestUserPermission("android.permission.VIBRATE");
        }

        public static void _PlayVibrateWhenAndroid(float time)
        {
#if UNITY_ANDROID || UNITY_IOS
            if (Application.platform == RuntimePlatform.Android)
            {
                try
                {
                    using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                    {
                        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                        AndroidJavaObject vibrator = unityActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");

                        // 检查设备是否支持震动
                        if (vibrator.Call<bool>("hasVibrator"))
                        {
                            Log.Error($"--zpb start vibration.");
                            long millisecond = (long)(time * 1000f);
                            vibrator.Call("vibrate", millisecond);
                            Log.Error($"--zpb start vibration End");
                        }
                        else
                        {
                            Log.Error($"--zpb This device does not support vibration.");
                            Handheld.Vibrate();
                        }
                    }
                }
                catch (Exception e)
                {
                }
            }
#endif
        }
    }
}