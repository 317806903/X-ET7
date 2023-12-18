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
                self.actionCallAutoUnitArea = null;
                if (self.unitIds != null)
                {
                    self.unitIds.Dispose();
                    self.unitIds = null;
                }
            }
        }

        public static (bool, ListComponent<long>) Check(this SelectHandleRecord self, Unit unit, bool isResetPos, float3 resetPos, ActionCallAutoUnitArea actionCallAutoUnitArea)
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
            if (self.actionCallAutoUnitArea == actionCallAutoUnitArea)
            {
                return (true, self.unitIds);
            }
            if (self.actionCallAutoUnitArea.SelectNum != actionCallAutoUnitArea.SelectNum)
            {
                return (false, null);
            }
            if (self.actionCallAutoUnitArea.IsFriend != actionCallAutoUnitArea.IsFriend)
            {
                return (false, null);
            }
            if (self.actionCallAutoUnitArea.IsOnlyPlayer != actionCallAutoUnitArea.IsOnlyPlayer)
            {
                return (false, null);
            }
            if (self.actionCallAutoUnitArea.OffSetInfo.NodeName != actionCallAutoUnitArea.OffSetInfo.NodeName)
            {
                return (false, null);
            }
            if (self.actionCallAutoUnitArea.OffSetInfo.OffSetPosition != actionCallAutoUnitArea.OffSetInfo.OffSetPosition)
            {
                return (false, null);
            }
            if (self.actionCallAutoUnitArea.OffSetInfo.RelateForward != actionCallAutoUnitArea.OffSetInfo.RelateForward)
            {
                return (false, null);
            }
            if (self.actionCallAutoUnitArea is ActionCallAutoUnitWhenUmbellate selfActionCallAutoUnitWhenUmbellate)
            {
                if (actionCallAutoUnitArea is ActionCallAutoUnitWhenUmbellate chkActionCallAutoUnitWhenUmbellate)
                {
                    if (selfActionCallAutoUnitWhenUmbellate.IsAngleFirst != chkActionCallAutoUnitWhenUmbellate.IsAngleFirst)
                    {
                        return (false, null);
                    }
                    if (selfActionCallAutoUnitWhenUmbellate.UmbellateArea.Radius != chkActionCallAutoUnitWhenUmbellate.UmbellateArea.Radius)
                    {
                        return (false, null);
                    }
                    if (selfActionCallAutoUnitWhenUmbellate.UmbellateArea.Angle != chkActionCallAutoUnitWhenUmbellate.UmbellateArea.Angle)
                    {
                        return (false, null);
                    }
                }
                else
                {
                    return (false, null);
                }
            }
            if (self.actionCallAutoUnitArea is ActionCallAutoUnitWhenRectangle selfActionCallAutoUnitWhenRectangle)
            {
                if (actionCallAutoUnitArea is ActionCallAutoUnitWhenRectangle chkActionCallAutoUnitWhenRectangle)
                {
                    if (selfActionCallAutoUnitWhenRectangle.RectangleArea.Width != chkActionCallAutoUnitWhenRectangle.RectangleArea.Width)
                    {
                        return (false, null);
                    }
                    if (selfActionCallAutoUnitWhenRectangle.RectangleArea.Length != chkActionCallAutoUnitWhenRectangle.RectangleArea.Length)
                    {
                        return (false, null);
                    }
                }
                else
                {
                    return (false, null);
                }
            }
            return (true, self.unitIds);
        }
    }
}
