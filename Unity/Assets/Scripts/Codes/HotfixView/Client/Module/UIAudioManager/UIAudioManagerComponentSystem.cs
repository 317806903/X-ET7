using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using ET.Client;
using Unity.Mathematics;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

namespace ET.Ability.Client
{
    [FriendOf(typeof (UIAudioManagerComponent))]
    public static class UIAudioManagerComponentSystem
    {
        [ObjectSystem]
        public class UIAudioManagerComponentAwakeSystem: AwakeSystem<UIAudioManagerComponent>
        {
            protected override void Awake(UIAudioManagerComponent self)
            {
                self.Init();
            }
        }

        [ObjectSystem]
        public class UIAudioManagerComponentDestroySystem: DestroySystem<UIAudioManagerComponent>
        {
            protected override void Destroy(UIAudioManagerComponent self)
            {
                if (self.root != null)
                {
                    GameObject.Destroy(self.root);
                    self.root = null;
                }
            }
        }

        [ObjectSystem]
        public class UIAudioManagerComponentFixedUpdateSystem: FixedUpdateSystem<UIAudioManagerComponent>
        {
            protected override void FixedUpdate(UIAudioManagerComponent self)
            {
                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static void Init(this UIAudioManagerComponent self)
        {
            GameObject go = new GameObject("UIAudioManager");
            self.root = go;
            go.transform.SetParent(GlobalComponent.Instance.ClientManagerRoot);
            go.transform.localPosition = UnityEngine.Vector3.zero;
            go.transform.localScale = UnityEngine.Vector3.one;
            self.audioSource = go.AddComponent<AudioSource>();
            self.musicSource = go.AddComponent<AudioSource>();
        }

        public static async ETTask PlayUIAudio(this UIAudioManagerComponent self, string resAudioCfgId)
        {
            ResAudioCfg resAudioCfg = ResAudioCfgCategory.Instance.Get(resAudioCfgId);
            if (resAudioCfg == null)
            {
                Log.Error($"UIAudioManagerComponentSystem.PlayUIAudio resAudioCfg == null[{resAudioCfgId}]");
                return;
            }

            AudioClip audioClip = ResComponent.Instance.LoadAsset<AudioClip>(resAudioCfg.ResName);
            self.audioSource.PlayOneShot(audioClip);
        }

        public static void PlayMusic(this UIAudioManagerComponent self, List<string> resAudioCfgIds)
        {
            self.resAudioCfgIds = resAudioCfgIds;
            self.musicSource.Stop();

            self._PlayNextMusicOne().Coroutine();
        }

        public static async ETTask _PlayNextMusicOne(this UIAudioManagerComponent self)
        {
            int index = RandomGenerator.RandomNumber(0, self.resAudioCfgIds.Count);
            while (self.resAudioCfgIds.Count > 1 && index == self.resAudioCfgIds.Count)
            {
                index = RandomGenerator.RandomNumber(0, self.resAudioCfgIds.Count);
            }
            self.index = index;

            for (int i = self.index; i < self.resAudioCfgIds.Count; i++)
            {
                ResAudioCfg resAudioCfg = ResAudioCfgCategory.Instance.Get(self.resAudioCfgIds[self.index]);
                if (resAudioCfg == null)
                {
                    Log.Error($"PlayNextMusicOne resAudioCfg == null[{self.resAudioCfgIds[self.index]}]");
                    continue;
                }

                self.index = i + 1;
                await self._PlayMusicOne(resAudioCfg);
                return;
            }

            self.index = 0;
        }

        public static async ETTask _PlayMusicOne(this UIAudioManagerComponent self, ResAudioCfg resAudioCfg)
        {
            AudioClip audioClip = ResComponent.Instance.LoadAsset<AudioClip>(resAudioCfg.ResName);
            self.musicSource.clip = audioClip;
            self.musicSource.playOnAwake = false;
            self.musicSource.spatialBlend = 0;
            self.musicSource.loop = false;
            self.musicSource.mute = false;
            self.musicSource.volume = 0.1f;
            self.musicSource.pitch = 1;
            self.musicSource.Play();
            await ETTask.CompletedTask;
        }

        public static void FixedUpdate(this UIAudioManagerComponent self, float fixedDeltaTime)
        {
            if (self.resAudioCfgIds == null || self.resAudioCfgIds.Count == 0)
            {
                return;
            }
            if (self.musicSource.isPlaying)
            {
                return;
            }

            self._PlayNextMusicOne().Coroutine();
        }

    }
}