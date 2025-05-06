using System;
using System.Collections.Generic;
using ET.Ability;
using Unity.Mathematics;

namespace ET.Server
{
    [FriendOf(typeof (AttackTargetComponent))]
    public static class AttackTargetComponentSystem
    {
        [ObjectSystem]
        public class AttackTargetComponentFixedUpdateSystem: FixedUpdateSystem<AttackTargetComponent>
        {
            protected override void FixedUpdate(AttackTargetComponent self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static void FixedUpdate(this AttackTargetComponent self, float fixedDeltaTime)
        {
            if (++self.curFrameChk >= self.waitFrameChk)
            {
                self.curFrameChk = 0;

                self.ChkAttackTargetChg();
            }
        }

        public static void ChkAttackTargetChg(this AttackTargetComponent self)
        {
            Unit unit = self.GetUnit();
            if (unit == null)
            {
                return;
            }

            bool needNotice = false;
            SelectHandle selectHandle = ET.Ability.UnitHelper.GetSaveSelectHandle(unit);
            if (self.attackTargetUnitId == 0)
            {
                if (selectHandle == null
                    || selectHandle.selectHandleType != SelectHandleType.SelectUnits
                    || selectHandle.unitIds.Count == 0)
                {
                    return;
                }
                self.attackTargetUnitId = selectHandle.unitIds[0];
                needNotice = true;
            }
            else
            {
                if (selectHandle == null
                    || selectHandle.selectHandleType != SelectHandleType.SelectUnits
                    || selectHandle.unitIds.Count == 0)
                {
                    self.attackTargetUnitId = 0;
                    needNotice = true;
                }
                else
                {
                    if (self.attackTargetUnitId != selectHandle.unitIds[0])
                    {
                        self.attackTargetUnitId = selectHandle.unitIds[0];
                        needNotice = true;
                    }
                }
            }

            if (needNotice)
            {
                Ability.UnitHelper.AddSyncData_UnitComponent(unit, self.GetType());
            }
        }

    }
}