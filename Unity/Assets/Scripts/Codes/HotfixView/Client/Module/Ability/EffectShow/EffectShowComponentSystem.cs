using System;
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
        public class EffectComponentAwakeSystem: AwakeSystem<EffectShowComponent>
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
        public class EffectShowComponentFixedUpdate: FixedUpdateSystem<EffectShowComponent>
        {
            protected override void FixedUpdate(EffectShowComponent self)
            {
                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static EffectShowObj AddEffectShow(this EffectShowComponent self, EffectObj effectObj)
        {
            EffectShowObj effectShowObj = self.AddChild<EffectShowObj>();
            effectShowObj.Init(effectObj).Coroutine();
            self.curExistEffectList[effectObj.Id] = effectShowObj;
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

        public static void FixedUpdate(this EffectShowComponent self, float fixedDeltaTime)
        {
            EffectComponent effectComponent = self.Parent.GetComponent<EffectComponent>();
            if (effectComponent == null)
            {
                return;
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
                    continue;
                }
                else
                {
                    self.AddEffectShow(effectObj);
                }
            }
            
            if (self.waitRemoveEffectList.Count <= 0)
            {
                return;
            }

            foreach (var effectObjId in self.waitRemoveEffectList)
            {
                self.RemoveEffectShow(effectObjId);
            }
            self.waitRemoveEffectList.Clear();
        }
    }
}