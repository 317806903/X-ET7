﻿using System;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;

namespace ET.Ability.Client
{
    [FriendOf(typeof (EffectShowComponent))]
    [FriendOf(typeof (EffectShowObj))]
    public static class EffectShowComponentSystem
    {
        [ObjectSystem]
        public class EffectShowComponentAwakeSystem: AwakeSystem<EffectShowComponent>
        {
            protected override void Awake(EffectShowComponent self)
            {
                self.curExistEffectList = new();
                self.waitRemoveEffectList = new();
            }
        }

        [ObjectSystem]
        public class EffectShowComponentDestroySystem: DestroySystem<EffectShowComponent>
        {
            protected override void Destroy(EffectShowComponent self)
            {
                self.curExistEffectList.Clear();
                self.waitRemoveEffectList.Clear();
            }
        }

        [ObjectSystem]
        public class EffectShowComponentUpdate: UpdateSystem<EffectShowComponent>
        {
            protected override void Update(EffectShowComponent self)
            {
				float fixedDeltaTime = TimeHelper.FixedDetalTime;
				self.Update(fixedDeltaTime);
            }
        }

        public static Unit GetUnit(this EffectShowComponent self)
        {
            Unit unit = self.GetParent<Unit>();
            return unit;
        }

        public static EffectShowObj AddEffectShow(this EffectShowComponent self, EffectObj effectObj)
        {
            EffectShowObj effectShowObj = self.AddChild<EffectShowObj>();
            effectShowObj.Init(effectObj).Coroutine();
            self.curExistEffectList[effectObj.Id] = effectShowObj;

            if (string.IsNullOrEmpty(effectObj.PlayAudioActionId) == false)
            {
                AudioPlayObj audioPlayObj = AudioPlayHelper.PlayAudio(self.GetUnit(), effectObj.PlayAudioActionId);
                if (audioPlayObj != null)
                {
                    effectShowObj.RefAudioPlayObj = audioPlayObj;
                }
            }

            return effectShowObj;
        }

        public static void RemoveEffectShow(this EffectShowComponent self, long effectObjId)
        {
            if (self.curExistEffectList.ContainsKey(effectObjId))
            {
                self.curExistEffectList[effectObjId].Dispose();
                self.curExistEffectList.Remove(effectObjId);
            }
        }

        public static void Update(this EffectShowComponent self, float fixedDeltaTime)
        {
            EffectComponent effectComponent = self.GetUnit().GetComponent<EffectComponent>();
            if (effectComponent == null)
            {
                return;
            }

            foreach (var effectShowObjs in self.curExistEffectList)
            {
                effectShowObjs.Value.UpdateEffect(false, fixedDeltaTime);
            }

            //self.CreateOrRemove(effectComponent, fixedDeltaTime);
            self.ChkOnly(effectComponent, fixedDeltaTime);
        }

        public static void CreateOrRemove(this EffectShowComponent self, EffectComponent effectComponent, float fixedDeltaTime)
        {
            if (self.curFrame++ < self.waitFrame)
            {
                return;
            }
            else
            {
                self.curFrame = 0;
            }

            self.waitRemoveEffectList.Clear();
            foreach (var effectShowObjs in self.curExistEffectList)
            {
                self.waitRemoveEffectList.Add(effectShowObjs.Key);
            }

            foreach (var effectObjs in effectComponent.Children)
            {
                EffectObj effectObj = effectObjs.Value as EffectObj;
                if (self.curExistEffectList.ContainsKey(effectObj.Id))
                {
                    self.waitRemoveEffectList.Remove(effectObj.Id);
                    self.curExistEffectList[effectObj.Id].Refresh(effectObj);
                    continue;
                }
                else
                {
                    self.AddEffectShow(effectObj);
                }
            }

            if (self.waitRemoveEffectList.Count > 0)
            {
                foreach (var effectObjId in self.waitRemoveEffectList)
                {
                    self.RemoveEffectShow(effectObjId);
                }
                self.waitRemoveEffectList.Clear();
            }
        }

        public static void ChkOnly(this EffectShowComponent self, EffectComponent effectComponent, float fixedDeltaTime)
        {
            if (self.curFrameOnly++ < self.waitFrameOnly)
            {
                return;
            }
            else
            {
                self.curFrameOnly = 0;
            }

            EffectShowChgComponent effectShowChgComponent = self.GetUnit().GetComponent<EffectShowChgComponent>();
            if (effectShowChgComponent == null)
            {
                return;
            }

            self.waitRemoveEffectList.Clear();

            foreach (long effectObjId in effectShowChgComponent.chgEffectList)
            {
                EffectObj effectObj = effectComponent.GetChild<EffectObj>(effectObjId);
                if (effectObj != null)
                {
                    if (self.curExistEffectList.ContainsKey(effectObj.Id))
                    {
                        self.curExistEffectList[effectObj.Id].Refresh(effectObj);
                        continue;
                    }
                    else
                    {
                        self.AddEffectShow(effectObj);
                    }
                }
                else
                {
                    self.waitRemoveEffectList.Add(effectObjId);
                }
            }
            effectShowChgComponent.chgEffectList.Clear();
            if (self.waitRemoveEffectList.Count > 0)
            {
                foreach (var effectObjId in self.waitRemoveEffectList)
                {
                    self.RemoveEffectShow(effectObjId);
                }
                self.waitRemoveEffectList.Clear();
            }
        }
    }
}