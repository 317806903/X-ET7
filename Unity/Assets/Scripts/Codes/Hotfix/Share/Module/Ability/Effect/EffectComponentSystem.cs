using System;
using System.Collections.Generic;
using System.Numerics;
using ET.AbilityConfig;
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
                self.removeList = null;
                self.recordEffectList.Clear();
                self.recordEffectList = null;
            }
        }

        [ObjectSystem]
        public class EffectComponentFixedUpdateSystem: FixedUpdateSystem<EffectComponent>
        {
            protected override void FixedUpdate(EffectComponent self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static Unit GetUnit(this EffectComponent self)
        {
            return self.GetParent<Unit>();
        }

        public static bool ChkCanAddEffect(this EffectComponent self, Unit unitEffect, string key, int maxKeyNum)
        {
            if (string.IsNullOrEmpty(key) == false)
            {
                if (self.recordEffectList.ContainsKey(key) && self.recordEffectList[key].Count >= maxKeyNum)
                {
                    return false;
                }
            }
            return true;
        }

        public static EffectObj AddEffect(this EffectComponent self, long unitId, string key, int maxKeyNum, string effectCfgId, string playAudioActionId, float duration, OffSetInfo offSetInfo, bool isScaleByUnit)
        {
            EffectObj effectObj = self.AddChild<EffectObj>();
            effectObj.Init(unitId, key, effectCfgId, playAudioActionId, duration, offSetInfo, isScaleByUnit);

            if (string.IsNullOrEmpty(key) == false)
            {
                self.recordEffectList.Add(key, effectObj.Id);
            }

            return effectObj;
        }

        public static void RemoveEffectByKey(this EffectComponent self, string key)
        {
            if (self.recordEffectList.ContainsKey(key))
            {
                self.recordEffectList.TryGetValue(key, out List<long> effectList);
                foreach (long effectObjId in effectList)
                {
                    EffectObj effectObj = self.GetChild<EffectObj>(effectObjId);
                    // self.NoticeClientRemoveEffect(effectObj);
                    // effectObj.Dispose();
                    effectObj.WillDestroy();
                }
                self.recordEffectList.Remove(key);
            }
        }

        public static void NoticeClientRemoveEffect(this EffectComponent self, EffectObj effectObj)
        {
            EventType.SyncUnitEffects _SyncUnitEffects = new()
            {
                unit = effectObj.GetUnit(),
                isAddEffect = false,
                effectObjId = effectObj.Id,
            };
            EventSystem.Instance.Publish(self.DomainScene(), _SyncUnitEffects);
        }

        public static void FixedUpdate(this EffectComponent self, float fixedDeltaTime)
        {
            if (self.Children.Count <= 0)
            {
                Unit unit = self.GetParent<Unit>();
                if (UnitHelper.ChkIsSceneEffect(unit))
                {
                    unit.DestroyWithDeathShow();
                }

                return;
            }

            self.removeList.Clear();
            foreach (var obj in self.Children.Values)
            {
                EffectObj effectObj = obj as EffectObj;
                effectObj.FixedUpdate(fixedDeltaTime);

                if (effectObj.ChkNeedRemove())
                {
                    self.removeList.Add(effectObj);
                }
            }

            int count = self.removeList.Count;
            for (int i = 0; i < count; i++)
            {
                EffectObj effectObj = self.removeList[i];
                string key = effectObj.key;
                if (self.recordEffectList.ContainsKey(key))
                {
                    self.recordEffectList.Remove(key, effectObj.Id);
                }

                self.NoticeClientRemoveEffect(effectObj);
                effectObj.Dispose();
            }

            self.removeList.Clear();
        }
    }
}