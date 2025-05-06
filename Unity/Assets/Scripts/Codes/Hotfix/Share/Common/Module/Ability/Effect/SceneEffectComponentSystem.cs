using System;
using System.Collections.Generic;
using System.Numerics;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (SceneEffectComponent))]
    [FriendOf(typeof (EffectObj))]
    public static class SceneEffectComponentSystem
    {
        [ObjectSystem]
        public class SceneEffectComponentAwakeSystem: AwakeSystem<SceneEffectComponent>
        {
            protected override void Awake(SceneEffectComponent self)
            {
                self.removeList = new();
                self.removeKeyList = new();
                self.recordEffectList = new();
                self.recordUnitId2EffectList = new();
            }
        }

        [ObjectSystem]
        public class SceneEffectComponentDestroySystem: DestroySystem<SceneEffectComponent>
        {
            protected override void Destroy(SceneEffectComponent self)
            {
                self.removeList.Clear();
                self.removeKeyList.Clear();
                self.recordEffectList.Clear();
                self.recordUnitId2EffectList.Clear();
            }
        }

        [ObjectSystem]
        public class SceneEffectComponentFixedUpdateSystem: FixedUpdateSystem<SceneEffectComponent>
        {
            protected override void FixedUpdate(SceneEffectComponent self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static void RecordEffect(this SceneEffectComponent self, Unit unit, string key, EffectObj effectObj)
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }

            self.recordEffectList.Add(key, effectObj);

            if (self.recordUnitId2EffectList.TryGetValue(unit.Id, out var recordKey2Obj) == false)
            {
                recordKey2Obj = new();
                self.recordUnitId2EffectList.Add(unit.Id, recordKey2Obj);
            }
            recordKey2Obj.Add(key, effectObj);
        }

        public static bool ChkCanAddEffect(this SceneEffectComponent self, string key, int maxKeyNum)
        {
            if (self.recordEffectList.TryGetValue(key, out var effectObjList) == false)
            {
                return true;
            }

            if (effectObjList.Count >= maxKeyNum)
            {
                return false;
            }

            return true;
        }

        public static void RemoveEffectByKey(this SceneEffectComponent self, Unit unit, string key)
        {
            if (self.recordUnitId2EffectList.TryGetValue(unit.Id, out var recordKey2Obj) == false)
            {
                return;
            }

            if (recordKey2Obj.ContainsKey(key) == false)
            {
                return;
            }
            foreach (EntityRef<EffectObj> effectObjRef in recordKey2Obj[key])
            {
                self.recordEffectList.Remove(key, effectObjRef);

                EffectObj effectObj = effectObjRef;
                effectObj?.WillDestroy();
            }
            recordKey2Obj.Remove(key);
        }

        public static void FixedUpdate(this SceneEffectComponent self, float fixedDeltaTime)
        {
            if (++self.curFrameChk >= self.waitFrameChk)
            {
                self.curFrameChk = 0;

                self.ClearNotExist();
            }
        }

        public static void ClearNotExist(this SceneEffectComponent self)
        {
            self.removeKeyList.Clear();
            foreach (var item in self.recordEffectList)
            {
                string key = item.Key;
                foreach (EntityRef<EffectObj> effectObjRef in item.Value)
                {
                    EffectObj effectObj = effectObjRef;
                    if (effectObj == null)
                    {
                        self.removeKeyList.Add((key, effectObjRef));
                    }
                }
            }
            for (int i = 0; i < self.removeKeyList.Count; i++)
            {
                self.recordEffectList.Remove(self.removeKeyList[i].key, self.removeKeyList[i].effectObjRef);
            }
            self.removeKeyList.Clear();

            self.removeList.Clear();
            foreach (var tmp1 in self.recordUnitId2EffectList)
            {
                long unitId = tmp1.Key;
                Unit unitEffect = UnitHelper.GetUnit(self.DomainScene(), unitId);
                if (UnitHelper.ChkUnitAlive(unitEffect) == false)
                {
                    self.removeList.Add(unitId);
                }
            }
            for (int i = 0; i < self.removeList.Count; i++)
            {
                long unitId = self.removeList[i];
                self.recordUnitId2EffectList.Remove(unitId);
            }
            self.removeList.Clear();
        }
    }
}