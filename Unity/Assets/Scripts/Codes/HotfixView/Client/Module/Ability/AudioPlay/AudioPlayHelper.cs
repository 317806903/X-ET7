using System.Collections.Generic;
using ET.Ability.AbilityEventType;
using ET.AbilityConfig;
using Unity.Mathematics;

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
            AudioPlayComponent audioPlayComponent = unit.GetComponent<AudioPlayComponent>();
            return audioPlayComponent.AddPlayAudioObj(actionCfg_PlayAudio);
        }

        // public static void StopAudio(Unit unit)
        // {
        //     AudioPlayComponent audioPlayComponent = unit.GetComponent<AudioPlayComponent>();
        //     audioPlayComponent.RemoveEffectByKey();
        // }
    }
}