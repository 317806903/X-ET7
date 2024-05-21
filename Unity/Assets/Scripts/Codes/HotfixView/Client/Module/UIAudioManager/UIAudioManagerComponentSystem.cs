using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using ET.Client;
using Unity.Mathematics;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

namespace ET.Client
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

            await self.PlayUIAudioByPath(resAudioCfg.ResName);
            await ETTask.CompletedTask;
        }

        public static async ETTask PlayUIAudioByPath(this UIAudioManagerComponent self, string audioPath)
        {
            if (GameSettingComponent.Instance.GetIsOn(GameSettingType.Audio) == false)
            {
                return;
            }

            AudioClip audioClip = ResComponent.Instance.LoadAsset<AudioClip>(audioPath);
            if (audioClip == null)
            {
                return;
            }
            self.audioSource.PlayOneShot(audioClip);
            await ETTask.CompletedTask;
        }

        public static void PlayMusic(this UIAudioManagerComponent self, List<string> resAudioCfgIds)
        {
            self.resAudioCfgIds = resAudioCfgIds;
            self.index = -1;
            self.musicSource.Stop();
            self.isMute = false;

            if (GameSettingComponent.Instance.GetIsOn(GameSettingType.Music) == false)
            {
                return;
            }

            self._PlayNextMusicOne().Coroutine();
        }

        public static void PlayHighestMusic(this UIAudioManagerComponent self, List<string> resAudioCfgIds)
        {
            self.resAudioCfgIds_heighest = resAudioCfgIds;
            self.index_heighest = -1;
            self.musicSource.Stop();
            self.isMute = false;

            if (GameSettingComponent.Instance.GetIsOn(GameSettingType.Music) == false)
            {
                return;
            }

            self._PlayNextMusicOne().Coroutine();
        }

        public static void StopMusic(this UIAudioManagerComponent self)
        {
            self.isMute = true;
            self.musicSource.mute = true;
        }

        public static void ResumeMusic(this UIAudioManagerComponent self)
        {
            self.isMute = false;
            self.musicSource.mute = false;
        }

        public static ResAudioCfg _GetNextMusic(this UIAudioManagerComponent self)
        {
            List<string> resAudioCfgIds = self.resAudioCfgIds;
            if (resAudioCfgIds == null || resAudioCfgIds.Count == 0)
            {
                return null;
            }

            int index = RandomGenerator.RandomNumber(0, resAudioCfgIds.Count);
            while (resAudioCfgIds.Count > 1 && index == self.index)
            {
                index = RandomGenerator.RandomNumber(0, resAudioCfgIds.Count);
            }
            self.index = index;

            for (int i = self.index; i < resAudioCfgIds.Count; i++)
            {
                ResAudioCfg resAudioCfg = ResAudioCfgCategory.Instance.Get(resAudioCfgIds[self.index]);
                if (resAudioCfg == null)
                {
                    Log.Error($"PlayNextMusicOne resAudioCfg == null[{resAudioCfgIds[self.index]}]");
                    continue;
                }

                self.index = i + 1;
                return resAudioCfg;
            }

            self.index = 0;
            return null;
        }

        public static ResAudioCfg _GetNextMusicHeighest(this UIAudioManagerComponent self)
        {
            List<string> resAudioCfgIds = self.resAudioCfgIds_heighest;
            if (resAudioCfgIds == null || resAudioCfgIds.Count == 0)
            {
                return null;
            }

            int index = RandomGenerator.RandomNumber(0, resAudioCfgIds.Count);
            while (resAudioCfgIds.Count > 1 && index == self.index_heighest)
            {
                index = RandomGenerator.RandomNumber(0, resAudioCfgIds.Count);
            }
            self.index_heighest = index;

            for (int i = self.index_heighest; i < resAudioCfgIds.Count; i++)
            {
                ResAudioCfg resAudioCfg = ResAudioCfgCategory.Instance.Get(resAudioCfgIds[self.index_heighest]);
                if (resAudioCfg == null)
                {
                    Log.Error($"PlayNextMusicOne resAudioCfg == null[{resAudioCfgIds[self.index]}]");
                    continue;
                }

                self.index_heighest = i + 1;
                return resAudioCfg;
            }

            self.index_heighest = -1;
            return null;
        }

        public static bool _ChkNextMusic(this UIAudioManagerComponent self)
        {
            ResAudioCfg resAudioCfg = self._GetNextMusicHeighest();
            if (resAudioCfg == null)
            {
                resAudioCfg = self._GetNextMusic();
            }
            if (resAudioCfg == null)
            {
                return false;
            }
            return true;
        }

        public static async ETTask _PlayNextMusicOne(this UIAudioManagerComponent self)
        {
            ResAudioCfg resAudioCfg = self._GetNextMusicHeighest();
            if (resAudioCfg == null)
            {
                resAudioCfg = self._GetNextMusic();
            }
            if (resAudioCfg == null)
            {
                return;
            }
            await self._PlayMusicOne(resAudioCfg);
        }

        public static async ETTask _PlayMusicOne(this UIAudioManagerComponent self, ResAudioCfg resAudioCfg)
        {
            AudioClip audioClip = ResComponent.Instance.LoadAsset<AudioClip>(resAudioCfg.ResName);
            self.musicSource.clip = audioClip;
            self.musicSource.playOnAwake = false;
            self.musicSource.spatialBlend = 0;
            self.musicSource.loop = false;
            self.musicSource.mute = self.isMute;
            self.musicSource.volume = 0.1f;
            self.musicSource.pitch = 1;
            self.musicSource.Play();
            await ETTask.CompletedTask;
        }

        public static void FixedUpdate(this UIAudioManagerComponent self, float fixedDeltaTime)
        {

            if (GameSettingComponent.Instance.GetIsOn(GameSettingType.Music) == false)
            {
                return;
            }

            if (self.musicSource.isPlaying)
            {
                return;
            }

            if (self._ChkNextMusic() == false)
            {
                return;
            }
            self._PlayNextMusicOne().Coroutine();
        }
    }
}