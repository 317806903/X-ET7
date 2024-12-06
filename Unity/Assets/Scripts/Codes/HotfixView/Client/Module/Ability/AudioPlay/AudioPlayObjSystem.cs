using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using ET.Client;
using Unity.Mathematics;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

namespace ET.Ability.Client
{
    [FriendOf(typeof (AudioPlayObj))]
    public static class AudioPlayObjSystem
    {
        [ObjectSystem]
        public class AudioPlayObjAwakeSystem: AwakeSystem<AudioPlayObj>
        {
            protected override void Awake(AudioPlayObj self)
            {
            }
        }

        [ObjectSystem]
        public class AudioPlayObjDestroySystem: DestroySystem<AudioPlayObj>
        {
            protected override void Destroy(AudioPlayObj self)
            {
                if (self.go != null)
                {
                    AudioSource audioSource = self.go.GetComponent<AudioSource>();
                    audioSource.Stop();
                    audioSource.clip = null;
                    audioSource.loop = false;
                    //UnityEngine.Object.Destroy(self.go);
                    GameObjectPoolHelper.ReturnTransformToPool(self.go.transform);
                    self.go = null;
                }
            }
        }

        [ObjectSystem]
        public class AudioPlayObjFixedUpdate: FixedUpdateSystem<AudioPlayObj>
        {
            protected override void FixedUpdate(AudioPlayObj self)
            {
                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static async ETTask _Init(this AudioPlayObj self)
        {
            Unit unit = self.GetParent<AudioPlayComponent>()?.GetParent<Unit>();
            if (unit == null)
            {
                Log.Error($"AudioPlayObjSystem.Init unit == null");
            }
            else
            {
                bool bRet = await ET.Client.UnitViewHelper.ChkGameObjectShowReady(self, unit);
                if (bRet == false)
                {
                    return;
                }
                GameObjectShowComponent gameObjectShowComponent = unit.GetComponent<GameObjectShowComponent>();

                ResEffectCfg resEffectCfg = ResEffectCfgCategory.Instance.Get("ResEffect_AudioBattleSource");
                GameObject go = GameObjectPoolHelper.GetObjectFromPool(resEffectCfg.ResName,true,10);
                if (go == null)
                {
                    Log.Error($"AudioPlayObjSystem.Init go == null when resName={resEffectCfg.ResName}");
                }
                self.go = go;
                Transform tran = gameObjectShowComponent.GetGo().transform;
                go.transform.SetParent(tran);
                go.transform.localScale = UnityEngine.Vector3.one;
                go.transform.localPosition = UnityEngine.Vector3.zero;
                go.transform.localEulerAngles = UnityEngine.Vector3.zero;
            }
        }

        public static void FixedUpdate(this AudioPlayObj self, float fixedDeltaTime)
        {
            if (self.go == null)
            {
                return;
            }
            float timePassed = fixedDeltaTime;
            if (self.permanent == false) self.duration -= timePassed;
            self.timeElapsed += timePassed;

            if (self.duration <= 0)
            {
                self.Dispose();
            }
        }

        public static async ETTask PlayAudio(this AudioPlayObj self, ActionCfg_PlayAudio actionCfg_PlayAudio)
        {
            while (TimeHelper.ClientNow() > TimeHelper.ClientFrameTime() + 200)
            {
                //await TimerComponent.Instance.WaitFrameAsync();
                await TimerComponent.Instance.WaitAsync(200);
                if (self.IsDisposed)
                {
                    return;
                }
            }

            if (self.go == null)
            {
                await self._Init();
            }

            if (self.IsDisposed || self.go == null)
            {
                return;
            }

            self.timeElapsed = 0;
            self.permanent = actionCfg_PlayAudio.Duration == -1? true : false;
            self.duration = actionCfg_PlayAudio.Duration == -1? 1 : actionCfg_PlayAudio.Duration;

            AudioSource audioSource = self.go.GetComponent<AudioSource>();

            ResAudioCfg resAudioCfg = actionCfg_PlayAudio.ResId_Ref;
            AudioClip audioClip = await ResComponent.Instance.LoadAssetAsync<AudioClip>(resAudioCfg.ResName);
            audioSource.clip = audioClip;
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 1f;
            audioSource.loop = actionCfg_PlayAudio.IsLoop;
            audioSource.mute = false;
            audioSource.volume = 1;
            audioSource.pitch = 1;
            audioSource.maxDistance = ET.GamePlayHelper.GetAudioMaxDis(self.DomainScene());
            audioSource.Play();
        }

    }
}