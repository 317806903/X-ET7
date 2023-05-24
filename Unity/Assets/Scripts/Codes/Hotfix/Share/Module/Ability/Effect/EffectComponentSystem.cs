using System;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (EffectComponent))]
    [FriendOf(typeof (EffectObj))]
    public static class EffectComponentSystem
    {
        [ObjectSystem]
        public class EffectComponentAwakeSystem: AwakeSystem<EffectComponent>
        {
            protected override void Awake(EffectComponent self)
            {
                self.removeList = new();
                self.recordEffectList = new();
            }
        }

        [ObjectSystem]
        public class EffectComponentDestroySystem: DestroySystem<EffectComponent>
        {
            protected override void Destroy(EffectComponent self)
            {
                self.removeList.Clear();
                self.recordEffectList.Clear();
            }
        }

        public static EffectObj AddEffect(this EffectComponent self, long unitId, string key, string effectCfgId, float duration, string nodeName, Vector3 
        offSetPosition, Vector3 
        relateForward)
        {
            EffectObj effectObj = self.AddChild<EffectObj>();
            float3 offSetPosition1 = new float3(offSetPosition.X, offSetPosition.Y, offSetPosition.Z);
            float3 relateForward1 = new float3(relateForward.X, relateForward.Y, relateForward.Z);
            effectObj.Init(unitId, key, effectCfgId, duration, nodeName, offSetPosition1, relateForward1);
            if (string.IsNullOrEmpty(key) == false)
            {
                self.recordEffectList[key] = effectObj;
            }
            return effectObj;
        }

        public static void RemoveEffectByKey(this EffectComponent self, string key)
        {
            if (self.recordEffectList.ContainsKey(key))
            {
                self.NoticeClientRemoveEffect(self.recordEffectList[key]);
                self.recordEffectList[key].Dispose();
                self.recordEffectList.Remove(key);
            }
        }

        public static void NoticeClientRemoveEffect(this EffectComponent self, EffectObj effectObj)
        {
            EventSystem.Instance.Invoke<SyncUnitEffects>(new SyncUnitEffects(){
                unit = effectObj.GetUnit(),
                isSceneEffect = false,
                isAddEffect = false,
                effectObjId = effectObj.Id,
            });
        }

        public static void FixedUpdate(this EffectComponent self, float fixedDeltaTime)
        {
            if (self.Children.Count <= 0)
            {
                return;
            }

            self.removeList.Clear();
            foreach (var effectObjs in self.Children)
            {
                EffectObj effectObj = effectObjs.Value as EffectObj;
                effectObj.FixedUpdate(fixedDeltaTime);

                if (effectObj.ChkNeedRemove())
                {
                    self.removeList.Add(effectObj);
                }
            }

            int count = self.removeList.Count;
            for (int i = 0; i < count; i++)
            {
                string key = self.removeList[i].key;
                if (self.recordEffectList.ContainsKey(key))
                {
                    self.recordEffectList.Remove(key);
                }
                self.NoticeClientRemoveEffect(self.removeList[i]);
                self.removeList[i].Dispose();
            }

            self.removeList.Clear();
        }
    }
}