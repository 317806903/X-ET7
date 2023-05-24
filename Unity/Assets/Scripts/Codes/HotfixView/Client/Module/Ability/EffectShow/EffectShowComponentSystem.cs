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
                self.recordEffectList = new();
                self.recordExistEffectList = new();
            }
        }

        [ObjectSystem]
        public class EffectShowComponentDestroySystem: DestroySystem<EffectShowComponent>
        {
            protected override void Destroy(EffectShowComponent self)
            {
                self.recordEffectList.Clear();
                self.recordExistEffectList.Clear();
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
            effectShowObj.Init(effectObj);
            self.recordEffectList[effectObj.Id] = effectShowObj;
            return effectShowObj;
        }

        public static void RemoveEffectShow(this EffectShowComponent self, long effectObjId)
        {
            if (self.recordEffectList.ContainsKey(effectObjId))
            {
                self.recordEffectList[effectObjId].Dispose();
                self.recordEffectList.Remove(effectObjId);
            }
        }

        public static void FixedUpdate(this EffectShowComponent self, float fixedDeltaTime)
        {
            EffectComponent effectComponent = self.Parent.GetComponent<EffectComponent>();
            if (effectComponent == null)
            {
                return;
            }
            self.recordExistEffectList.Clear();
            foreach (var effectShowObjs in self.recordEffectList)
            {
                self.recordExistEffectList.Add(effectShowObjs.Key);
            }
            
            foreach (var effectObjs in effectComponent.Children)
            {
                EffectObj effectObj = effectObjs.Value as EffectObj;
                if (self.recordEffectList.ContainsKey(effectObj.Id))
                {
                    self.recordExistEffectList.Remove(effectObj.Id);
                    continue;
                }
                else
                {
                    self.AddEffectShow(effectObj);
                }
            }
            
            if (self.recordExistEffectList.Count <= 0)
            {
                return;
            }

            foreach (var effectObjId in self.recordExistEffectList)
            {
                self.RemoveEffectShow(effectObjId);
            }
            self.recordExistEffectList.Clear();
        }
    }
}