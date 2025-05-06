using System;
using System.Collections.Generic;
using System.Numerics;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability.Client
{
    [FriendOf(typeof (AudioPlayComponent))]
    [FriendOf(typeof (AudioPlayObj))]
    public static class AudioPlayComponentSystem
    {
        [ObjectSystem]
        public class AudioPlayComponentAwakeSystem: AwakeSystem<AudioPlayComponent>
        {
            protected override void Awake(AudioPlayComponent self)
            {
            }
        }

        [ObjectSystem]
        public class AudioPlayComponentDestroySystem: DestroySystem<AudioPlayComponent>
        {
            protected override void Destroy(AudioPlayComponent self)
            {
            }
        }

        public static AudioPlayObj AddPlayAudioObj(this AudioPlayComponent self, ActionCfg_PlayAudio actionCfg_PlayAudio)
        {
            AudioPlayObj audioPlayObj = self.AddChild<AudioPlayObj>();
            audioPlayObj.PlayAudio(actionCfg_PlayAudio).Coroutine();
            return audioPlayObj;
        }

    }
}