using System;
using System.Collections.Generic;
using System.Xml.Schema;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (MoveTweenObj))]
    public static class MoveTweenObjSystem
    {
        [ObjectSystem]
        public class MoveTweenObjAwakeSystem: AwakeSystem<MoveTweenObj>
        {
            protected override void Awake(MoveTweenObj self)
            {
            }
        }

        [ObjectSystem]
        public class MoveTweenObjDestroySystem: DestroySystem<MoveTweenObj>
        {
            protected override void Destroy(MoveTweenObj self)
            {
            }
        }

        public static void Init(this MoveTweenObj self, long unitId, MoveTweenType moveTweenType, SelectHandle selectHandle)
        {
            self.unitId = unitId;
            self.moveTweenType = moveTweenType;
            self.selectHandle = selectHandle;
            self.speed = moveTweenType.Speed;
            self.forward = selectHandle.direction;
        }

        public static Unit GetUnit(this MoveTweenObj self)
        {
            return self.GetParent<Unit>();
        }

        public static void FixedUpdate(this MoveTweenObj self, float fixedDeltaTime)
        {
            float timePassed = fixedDeltaTime;
            self.timeElapsed += timePassed;
            self.DoMoveTween(fixedDeltaTime);
        }

        public static void DoMoveTween(this MoveTweenObj self, float fixedDeltaTime)
        {
            if (self.moveTweenType is StraightMoveTweenType straightMoveTweenType)
            {
                self.DoMoveTween_Straight(straightMoveTweenType, fixedDeltaTime);
            }
            else if (self.moveTweenType is TrackingMoveTweenType trackingMoveTweenType)
            {
                self.DoMoveTween_Tracking(trackingMoveTweenType, fixedDeltaTime);
            }
            else if (self.moveTweenType is AroundMoveTweenType aroundMoveTweenType)
            {
                self.DoMoveTween_Around(aroundMoveTweenType, fixedDeltaTime);
            }
            //Log.Debug($" DoMoveTween {self.GetUnit().Position} {self.GetUnit().Forward}");
        }
        
        public static void DoMoveTween_Straight(this MoveTweenObj self, StraightMoveTweenType moveTweenType, float fixedDeltaTime)
        {
            float speed = moveTweenType.Speed;
            float acceleratedSpeed = moveTweenType.AcceleratedSpeed;
            self.speed = speed + self.timeElapsed * acceleratedSpeed;
            self.GetUnit().Forward = self.forward;
            self.GetUnit().Position += math.normalize(self.forward) * self.speed * fixedDeltaTime;
        }
        
        public static void DoMoveTween_Tracking(this MoveTweenObj self, TrackingMoveTweenType moveTweenType, float fixedDeltaTime)
        {
            Unit targetUnit;
            SelectHandleType selectHandleType = self.selectHandle.selectHandleType;
            if (selectHandleType != SelectHandleType.SelectUnits)
            {
                return;
            }
            
            Unit unit = self.GetUnit();
            float speed = moveTweenType.Speed;
            float acceleratedSpeed = moveTweenType.AcceleratedSpeed;
            float rotateAngle = moveTweenType.RotateAngle;
            self.speed = speed + self.timeElapsed * acceleratedSpeed;
            
            float3 dir = float3.zero;
            if (self.selectHandle.unitIds.Count == 0)
            {
            }
            else
            {
                long targetUnitId = self.selectHandle.unitIds[0];
                targetUnit = UnitHelper.GetUnit(self.DomainScene(), targetUnitId);
                dir = targetUnit.Position - unit.Position;
            }

            if (dir.Equals(float3.zero) == false)
            {
                dir = math.normalize(dir);
                float angleTmp = math.degrees(math.acos(math.clamp(math.dot(unit.Forward, dir), -1, 1)));
                if (angleTmp > 0)
                {
                    if (angleTmp > rotateAngle)
                    {
                        angleTmp = rotateAngle;
                    }

                    self.forward = math.lerp(unit.Forward, dir, rotateAngle * fixedDeltaTime / angleTmp);
                    self.forward = math.normalize(self.forward);
                    unit.Forward = self.forward;
                }
            }
            unit.Position += unit.Forward * self.speed * fixedDeltaTime;
        }
        
        public static void DoMoveTween_Around(this MoveTweenObj self, AroundMoveTweenType moveTweenType, float fixedDeltaTime)
        {
            float3 targetPosition;
            SelectHandleType selectHandleType = self.selectHandle.selectHandleType;
            if (selectHandleType == SelectHandleType.SelectUnits)
            {
                long targetUnitId = self.selectHandle.unitIds[0];
                Unit targetUnit = UnitHelper.GetUnit(self.DomainScene(), targetUnitId);
                targetPosition = targetUnit.Position;
            }
            else if (selectHandleType == SelectHandleType.SelectPosition)
            {
                targetPosition = self.selectHandle.position;
            }
            Unit unit = self.GetUnit();
            float speed = moveTweenType.Speed;
            float acceleratedSpeed = moveTweenType.AcceleratedSpeed;
            float radius = moveTweenType.Radius;
            self.speed = speed + self.timeElapsed * acceleratedSpeed;
            //
            //
            // float3 dir = targetUnit.Position - self.GetUnit().Position;
            // float angleTmp = math.degrees(math.acos(math.dot(self.GetUnit().Forward, dir)));
            // if (angleTmp > rotateAngle)
            // {
            //     angleTmp = rotateAngle;
            // }
            //
            // unit.Forward = math.lerp(unit.Forward, dir, rotateAngle * fixedDeltaTime / angleTmp);
            // self.forward = unit.Forward;
            // unit.Position += math.normalize(unit.Forward) * self.speed * fixedDeltaTime;
        }
    }
}