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
            PlayVibrate(actionCfg_PlayAudio);
            return PlayAudio(unit, actionCfg_PlayAudio);
        }

        public static AudioPlayObj PlayAudio(Unit unit, ActionCfg_PlayAudio actionCfg_PlayAudio)
        {
            if (GameSettingComponent.Instance.GetIsOn(GameSettingType.Audio) == false)
            {
                return null;
            }

            AudioPlayComponent audioPlayComponent = unit.GetComponent<AudioPlayComponent>();
            if (audioPlayComponent == null)
            {
                return null;
            }
            return audioPlayComponent.AddPlayAudioObj(actionCfg_PlayAudio);
        }

        public static void PlayVibrate(ActionCfg_PlayAudio actionCfg_PlayAudio)
        {
            VibrationType vibrationType = actionCfg_PlayAudio.VibrationType;
            if (vibrationType == VibrationType.None)
            {
                return;
            }
            MoreMountains.NiceVibrations.HapticTypes hapticTypes = (MoreMountains.NiceVibrations.HapticTypes)Enum.Parse(typeof(MoreMountains.NiceVibrations.HapticTypes), vibrationType.ToString());
            PlayVibrate(hapticTypes);
        }

        public static void PlayVibrate(MoreMountains.NiceVibrations.HapticTypes hapticTypes)
        {
            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(hapticTypes, false, true);
        }

    }
}