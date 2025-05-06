using System;
using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof (ExcludeSelectHandleObj))]
    public static class ExcludeSelectHandleObjSystem
    {
        [ObjectSystem]
        public class ExcludeSelectHandleObjAwakeSystem: AwakeSystem<ExcludeSelectHandleObj>
        {
            protected override void Awake(ExcludeSelectHandleObj self)
            {
                self.excludeUnitList = new();
            }
        }

        [ObjectSystem]
        public class ExcludeSelectHandleObjDestroySystem: DestroySystem<ExcludeSelectHandleObj>
        {
            protected override void Destroy(ExcludeSelectHandleObj self)
            {
                self.excludeUnitList.Clear();
            }
        }

        public static Unit GetUnit(this ExcludeSelectHandleObj self)
        {
            Unit unit = self.GetParent<Unit>();
            return unit;
        }

        public static void SaveExcludeSelectHandle(this ExcludeSelectHandleObj self, SelectHandle selectHandle)
        {
            foreach (long unitId in selectHandle.unitIds)
            {
                self.excludeUnitList.Add(unitId);
            }
        }

        public static void ClearExcludeSelectHandle(this ExcludeSelectHandleObj self)
        {
            self.excludeUnitList.Clear();
        }

        public static HashSet<long> GetSaveExcludeSelectHandle(this ExcludeSelectHandleObj self)
        {
            return self.excludeUnitList;
        }
    }
}