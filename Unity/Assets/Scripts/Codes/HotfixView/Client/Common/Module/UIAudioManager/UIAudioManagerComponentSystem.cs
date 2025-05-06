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
                self.resAudioCfgIds = new();
                self.resAudioPitchs = new();
                self.resAudioCfgIds_heighest = new();
                self.resAudioPitchs_heighest = new();
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
                    GameObjectPoolHelper.ReturnTransformToPool(self.root.transform);
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

            string resName = "AudioManagerSource";
            GameObject go = GameObjectPoolHelper.GetObjectFromPool(resName,true,1);

            self.root = go;
            go.transform.SetParent(GlobalComponent.Instance.ClientManagerRoot);
            go.transform.localPosition = UnityEngine.Vector3.zero;
            go.transform.localScale = UnityEngine.Vector3.one;

            self.audioSource = go.transform.Find("AudioUI").gameObject.GetComponent<AudioSource>();
            self.musicSource = go.transform.Find("Music").gameObject.GetComponent<AudioSource>();
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
            self.audioSource.volume = 1f;
            self.audioSource.PlayOneShot(audioClip);
            await ETTask.CompletedTask;
        }

        public static void PlayMusic(this UIAudioManagerComponent self, Dictionary<string, float> audioList)
        {
            if (audioList == null || audioList.Count == 0)
            {
                return;
            }

            if (self.resAudioCfgIds.Count == audioList.Count)
            {
                bool isSampleAudioCfgIdList = true;
                bool isSampleAudioPitchList = true;
                for (int i = 0; i < self.resAudioCfgIds.Count; i++)
                {
                    string resAudioCfgId = self.resAudioCfgIds[i];
                    float resAudioPitch = self.resAudioPitchs[i];
                    if (audioList.TryGetValue(resAudioCfgId, out var pitchTmp) == false)
                    {
                        isSampleAudioCfgIdList = false;
                        isSampleAudioPitchList = false;
                        break;
                    }
                    else
                    {
                        if (pitchTmp != resAudioPitch)
                        {
                            isSampleAudioPitchList = false;
                            self.resAudioPitchs[i] = pitchTmp;
                            break;
                        }
                    }
                }

                if (isSampleAudioCfgIdList && isSampleAudioPitchList)
                {
                    if (self.index == -1)
                    {
                        self.index = 0;
                    }
                    return;
                }
                else if (isSampleAudioCfgIdList)
                {
                    self.audioPitch = self.resAudioPitchs[self.index];
                    self.musicSource.pitch = self.audioPitch;
                    return;
                }
            }

            self.resAudioCfgIds.Clear();
            self.resAudioPitchs.Clear();
            foreach (KeyValuePair<string,float> item in audioList)
            {
                self.resAudioCfgIds.Add(item.Key);
                self.resAudioPitchs.Add(item.Value);
            }
            self.index = -1;
            self.musicSource.Stop();
            self.isMute = false;

            if (GameSettingComponent.Instance.GetIsOn(GameSettingType.Music) == false)
            {
                return;
            }

            self._PlayNextMusicOne().Coroutine();
        }

        public static void PlayHighestMusic(this UIAudioManagerComponent self, Dictionary<string, float> audioList, bool isLoop = true)
        {
            if (audioList == null)
            {
                return;
            }

            if (audioList.Count > 0 && self.resAudioCfgIds_heighest.Count == audioList.Count)
            {
                bool isSampleAudioCfgIdList = true;
                bool isSampleAudioPitchList = true;
                for (int i = 0; i < self.resAudioCfgIds_heighest.Count; i++)
                {
                    string resAudioCfgId = self.resAudioCfgIds_heighest[i];
                    float resAudioPitch = self.resAudioPitchs_heighest[i];
                    if (audioList.TryGetValue(resAudioCfgId, out var pitchTmp) == false)
                    {
                        isSampleAudioCfgIdList = false;
                        isSampleAudioPitchList = false;
                        break;
                    }
                    else
                    {
                        if (pitchTmp != resAudioPitch)
                        {
                            isSampleAudioPitchList = false;
                            self.resAudioPitchs_heighest[i] = pitchTmp;
                            break;
                        }
                    }
                }

                if (isSampleAudioCfgIdList && isSampleAudioPitchList)
                {
                    self.isLoop = isLoop;
                    return;
                }
                else if (isSampleAudioCfgIdList)
                {
                    self.isLoop = isLoop;
                    self.audioPitch = self.resAudioPitchs_heighest[self.index_heighest];
                    self.musicSource.pitch = self.audioPitch;
                    return;
                }
            }

            self.resAudioCfgIds_heighest.Clear();
            self.resAudioPitchs_heighest.Clear();
            foreach (KeyValuePair<string,float> item in audioList)
            {
                self.resAudioCfgIds_heighest.Add(item.Key);
                self.resAudioPitchs_heighest.Add(item.Value);
            }

            self.isLoop = isLoop;
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
                if (self.index >= resAudioCfgIds.Count)
                {
                    self.index = 0;
                }
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
                if (self.index_heighest >= resAudioCfgIds.Count)
                {
                    self.index_heighest = 0;
                }
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
            if (resAudioCfg != null)
            {
                self.audioPitch = self.resAudioPitchs_heighest[self.index_heighest];
                if (self.isLoop == false)
                {
                    self.resAudioCfgIds_heighest.RemoveAt(self.index_heighest);
                    self.resAudioPitchs_heighest.RemoveAt(self.index_heighest);
                    self.index_heighest = -1;
                }
            }
            else
            {
                resAudioCfg = self._GetNextMusic();
                if (resAudioCfg != null)
                {
                    self.audioPitch = self.resAudioPitchs[self.index];
                    self.isLoop = true;
                }
                else
                {
                    return;
                }
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
            self.musicSource.volume = 1f;
            self.musicSource.pitch = self.audioPitch;
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