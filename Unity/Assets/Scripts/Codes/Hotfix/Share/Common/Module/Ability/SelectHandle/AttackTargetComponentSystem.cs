using System;
using System.Collections.Generic;
using ET.Ability;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof (AttackTargetComponent))]
    public static class AttackTargetComponentSystem
    {
        [ObjectSystem]
        public class AttackTargetComponentAwakeSystem: AwakeSystem<AttackTargetComponent>
        {
            protected override void Awake(AttackTargetComponent self)
            {
                self.Init();
            }
        }

        [ObjectSystem]
        public class AttackTargetComponentDestroySystem: DestroySystem<AttackTargetComponent>
        {
            protected override void Destroy(AttackTargetComponent self)
            {
            }
        }

        public static void Init(this AttackTargetComponent self)
        {
            Unit unit = self.GetUnit();
            TowerComponent towerComponent = unit.GetComponent<TowerComponent>();
            if (towerComponent != null)
            {
                self.maxTiltedUpAngle = towerComponent.model.MaxTiltedUpAngle;
                self.maxTiltedDownAngle = towerComponent.model.MaxTiltedDownAngle;
            }
        }

        public static Unit GetUnit(this AttackTargetComponent self)
        {
            Unit unit = self.GetParent<Unit>();
            return unit;
        }

        public static Unit GetAttackTargetUnit(this AttackTargetComponent self)
        {
            if (self.attackTargetUnitId == 0)
            {
                return null;
            }

            return UnitHelper.GetUnit(self.DomainScene(), self.attackTargetUnitId);
        }

        public static float GetAttackTargetAngle(this AttackTargetComponent self)
        {
            float targetAngle;
            Unit unit = self.GetUnit();
            if (unit == null)
            {
                return 0;
            }

            Unit attackTargetUnit = self.GetAttackTargetUnit();
            if (ET.Ability.UnitHelper.ChkUnitAlive(attackTargetUnit) == false)
            {
                targetAngle = 0;
            }
            else
            {
                float3 beAttackTargetPos = Ability.UnitHelper.GetBeAttackPointPos(attackTargetUnit);
                float3 attackPos = Ability.UnitHelper.GetAttackPointPos(unit);
                bool isTiltedUp = false;
                float maxAngle = self.maxTiltedDownAngle;
                if (beAttackTargetPos.y > attackPos.y)
                {
                    isTiltedUp = true;
                    maxAngle = self.maxTiltedUpAngle;
                }

                float angle = ET.MathHelper.CalculateAngle(beAttackTargetPos - attackPos, unit.Forward);
                if (isTiltedUp)
                {
                    angle = -angle;
                    if (angle < -maxAngle)
                    {
                        angle = -maxAngle;
                    }
                }
                else
                {
                    if (angle > maxAngle)
                    {
                        angle = maxAngle;
                    }
                }
                targetAngle = angle;
            }
            return targetAngle;
        }

    }
}