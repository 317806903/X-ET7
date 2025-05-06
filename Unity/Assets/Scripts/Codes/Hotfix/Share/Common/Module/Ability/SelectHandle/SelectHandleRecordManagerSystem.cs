using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (SelectHandleRecordManager))]
    public static class SelectHandleRecordManagerSystem
    {
        [ObjectSystem]
        public class SelectHandleRecordManagerAwakeSystem: AwakeSystem<SelectHandleRecordManager>
        {
            protected override void Awake(SelectHandleRecordManager self)
            {
                self.time2ChildId = new();
                self.unitId2ChildId = new();
                self.removeList = new();
            }
        }

        [ObjectSystem]
        public class SelectHandleRecordManagerDestroySystem: DestroySystem<SelectHandleRecordManager>
        {
            protected override void Destroy(SelectHandleRecordManager self)
            {
                self.time2ChildId.Clear();
                self.unitId2ChildId.Clear();
                self.removeList.Clear();
            }
        }

        [ObjectSystem]
        public class SelectHandleRecordManagerFixedUpdateSystem: FixedUpdateSystem<SelectHandleRecordManager>
        {
            protected override void FixedUpdate(SelectHandleRecordManager self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static void FixedUpdate(this SelectHandleRecordManager self, float fixedDeltaTime)
        {
            self.removeList.Clear();

            foreach (var item in self.time2ChildId)
            {
                if (item.Key < TimeHelper.ClientFrameTime())
                {
                    foreach (long childId in item.Value)
                    {
                        self.RemoveChild(childId);
                        long unitId = self.unitId2ChildId.GetKeyByValue(childId);
                        self.unitId2ChildId.RemoveByKey(unitId);
                    }
                    self.removeList.Add(item.Key);
                }
            }

            for (int i = 0; i < self.removeList.Count; i++)
            {
                self.time2ChildId.Remove(self.removeList[i]);
            }

            self.removeList.Clear();
        }

        public static void DoRecordUnitsByArea(this SelectHandleRecordManager self, Unit unit, bool isResetPos, float3 resetPos, bool isResetForward, float3 resetForward, SelectObjectCfg selectObjectCfg, SelectHandle selectHandle)
        {
            if (selectHandle.selectHandleType != SelectHandleType.SelectUnits)
            {
                return;
            }
            if (selectHandle.unitIds == null || selectHandle.unitIds.Count == 0)
            {
                return;
            }
            SelectHandleRecord selectHandleRecord = self.AddChild<SelectHandleRecord>();
            selectHandleRecord.unitId = unit.Id;
            selectHandleRecord.isResetPos = isResetPos;
            selectHandleRecord.resetPos = resetPos;
            selectHandleRecord.isResetForward = isResetForward;
            selectHandleRecord.resetForward = resetForward;
            selectHandleRecord.selectObjectCfgId = selectObjectCfg.Id;
            selectHandleRecord.unitIds = ListComponent<long>.Create();
            selectHandleRecord.unitIds.AddRange(selectHandle.unitIds);

            self.time2ChildId.Add(TimeHelper.ClientFrameTime(), selectHandleRecord.Id);
            self.unitId2ChildId.RemoveByKey(unit.Id);
            self.unitId2ChildId.Add(unit.Id, selectHandleRecord.Id);
        }

        public static (bool, ListComponent<long>) ChkRecordUnitsByArea(this SelectHandleRecordManager self, Unit unit, bool isResetPos, float3 resetPos, bool isResetForward, float3 resetForward, SelectObjectCfg selectObjectCfg)
        {
            if (selectObjectCfg.IsNeedChkExcludeTarget || selectObjectCfg.IsSaveExcludeTarget)
            {
                return (false, null);
            }
            if (self.unitId2ChildId.ContainsKey(unit.Id) == false)
            {
                return (false, null);
            }
            long selectHandleRecordId = self.unitId2ChildId.GetValueByKey(unit.Id);
            SelectHandleRecord selectHandleRecord = self.GetChild<SelectHandleRecord>(selectHandleRecordId);
            if (selectHandleRecord == null)
            {
                return (false, null);
            }
            return selectHandleRecord.Check(unit, isResetPos, resetPos, isResetForward, resetForward, selectObjectCfg);
        }
    }
}