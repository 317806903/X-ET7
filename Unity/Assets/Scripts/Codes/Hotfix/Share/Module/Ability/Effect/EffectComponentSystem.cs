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
                self.effectShowType2EffectObjId = new();
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
                self.effectShowType2EffectObjId.Clear();
                self.effectShowType2EffectObjId = null;
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

        public static EffectObj AddEffect(this EffectComponent self, long casterUnitId, ActionCfg_EffectCreate actionCfgCreateEffect, bool isScaleByUnit)
        {
            EffectShowType effectShowType = actionCfgCreateEffect.EffectShowType;
            if (effectShowType == EffectShowType.Send2Receives || effectShowType == EffectShowType.Send2Receive2Receive)
            {
                EffectObj effectObj = null;
                string effectKey = actionCfgCreateEffect.Key;
                if (string.IsNullOrEmpty(effectKey))
                {
                    effectKey = actionCfgCreateEffect.Id;
                }

                if (self.recordEffectList.TryGetValue(effectKey, out var effectObjIds))
                {
                    if (effectObjIds.Count > 0)
                    {
                        foreach (var effectObjId in effectObjIds)
                        {
                            effectObj = self.GetChild<EffectObj>(effectObjId);
                            if (effectObj != null && effectObj.casterUnitId == casterUnitId && effectObj.CfgId == actionCfgCreateEffect.ResEffectId)
                            {
                                effectObj.ResetTime(actionCfgCreateEffect);
                                return effectObj;
                            }
                        }
                    }
                }

                effectObj = self.AddChild<EffectObj>();
                effectObj.Init(casterUnitId, actionCfgCreateEffect, isScaleByUnit);

                self.recordEffectList.Add(effectKey, effectObj.Id);

                self.effectShowType2EffectObjId.Add(effectShowType, effectObj.Id);

                return effectObj;
            }
            else
            {
                EffectObj effectObj = self.AddChild<EffectObj>();
                effectObj.Init(casterUnitId, actionCfgCreateEffect, isScaleByUnit);

                if (string.IsNullOrEmpty(actionCfgCreateEffect.Key) == false)
                {
                    self.recordEffectList.Add(actionCfgCreateEffect.Key, effectObj.Id);
                }

                self.effectShowType2EffectObjId.Add(effectShowType, effectObj.Id);
                return effectObj;
            }
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

                    self.effectShowType2EffectObjId.Remove(effectObj.effectShowType, effectObj.Id);
                    effectObj.WillDestroy();
                }
                self.recordEffectList.Remove(key);
            }
        }

        public static List<long> GetEffectObjIdsByEffectShowType(this EffectComponent self, EffectShowType effectShowType)
        {
            self.effectShowType2EffectObjId.TryGetValue(effectShowType, out List<long> effectObjList);
            return effectObjList;
        }

        public static void NoticeClientRemoveEffect(this EffectComponent self, EffectObj effectObj)
        {
            if (effectObj == null)
            {
                return;
            }
            EventType.SyncUnitEffects _SyncUnitEffects = new()
            {
                unit = effectObj.GetUnit(),
                isAddEffect = false,
                effectObjId = effectObj.Id,
            };
            EventSystem.Instance.Publish(self.DomainScene(), _SyncUnitEffects);
        }

        public static void NoticeClientRefreshEffectObj(this EffectComponent self, EffectObj effectObj)
        {
            if (effectObj == null)
            {
                return;
            }
            EventType.SyncUnitEffects _SyncUnitEffects = new()
            {
                unit = effectObj.GetUnit(),
                isAddEffect = true,
                effectObj = effectObj,
                isOnlySelfShow = false,
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
                self.effectShowType2EffectObjId.Remove(effectObj.effectShowType, effectObj.Id);

                self.NoticeClientRemoveEffect(effectObj);
                effectObj.Dispose();
            }

            self.removeList.Clear();
        }
    }
}