using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (SelectHandleRecord))]
    public static class SelectHandleRecordSystem
    {
        [ObjectSystem]
        public class SelectHandleRecordAwakeSystem: AwakeSystem<SelectHandleRecord>
        {
            protected override void Awake(SelectHandleRecord self)
            {
            }
        }

        [ObjectSystem]
        public class SelectHandleRecordDestroySystem: DestroySystem<SelectHandleRecord>
        {
            protected override void Destroy(SelectHandleRecord self)
            {
                if (self.unitIds != null)
                {
                    self.unitIds.Dispose();
                    self.unitIds = null;
                }
            }
        }

        public static (bool, ListComponent<long>) Check(this SelectHandleRecord self, Unit unit, bool isResetPos, float3 resetPos, SelectObjectConfig selectObjectConfig)
        {
            if (self.unitId != unit.Id)
            {
                return (false, null);
            }
            if (self.isResetPos != isResetPos)
            {
                return (false, null);
            }
            if (self.resetPos.Equals(resetPos) == false)
            {
                return (false, null);
            }
            if (self.selectObjectCfgId != selectObjectConfig.Id)
            {
                return (false, null);
            }
            return (true, self.unitIds);
        }
    }
}
