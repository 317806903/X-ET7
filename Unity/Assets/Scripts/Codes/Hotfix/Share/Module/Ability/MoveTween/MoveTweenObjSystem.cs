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

        [ObjectSystem]
        public class MoveTweenObjFixedUpdateSystem: FixedUpdateSystem<MoveTweenObj>
        {
            protected override void FixedUpdate(MoveTweenObj self)
            {
                if (self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
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
            self.lastPosition = self.GetUnit().Position;
            self.DoMoveTween(fixedDeltaTime);
        }

        public static void DoMoveTween(this MoveTweenObj self, float fixedDeltaTime)
        {
            if (self.moveTweenType is StraightMoveTweenType straightMoveTweenType)
            {
                ProfilerSample.BeginSample("StraightMoveTweenType");
                self.DoMoveTween_Straight(straightMoveTweenType, fixedDeltaTime);
                ProfilerSample.EndSample();
            }
            else if (self.moveTweenType is TrackingMoveTweenType trackingMoveTweenType)
            {
                ProfilerSample.BeginSample("TrackingMoveTweenType");
                self.DoMoveTween_Tracking(trackingMoveTweenType, fixedDeltaTime);
                ProfilerSample.EndSample();
            }
            else if (self.moveTweenType is AroundMoveTweenType aroundMoveTweenType)
            {
                ProfilerSample.BeginSample("AroundMoveTweenType");
                self.DoMoveTween_Around(aroundMoveTweenType, fixedDeltaTime);
                ProfilerSample.EndSample();
            }
            else if (self.moveTweenType is TargetMoveTweenType targetMoveTweenType)
            {
                if (self.selectHandle.selectHandleType == SelectHandleType.SelectDirection)
                {
                    ProfilerSample.BeginSample("TargetMoveTweenType SelectDirection");
                    self.DoMoveTween_Straight(self.moveTweenType as StraightMoveTweenType, fixedDeltaTime);
                    ProfilerSample.EndSample();
                }
                else
                {
                    ProfilerSample.BeginSample("TargetMoveTweenType");
                    self.DoMoveTween_Target(targetMoveTweenType, fixedDeltaTime);
                    ProfilerSample.EndSample();
                }
            }
            //Log.Debug($" DoMoveTween {self.GetUnit().Position} {self.GetUnit().Forward}");
        }

        /// <summary>
        /// 直线轨迹
        /// </summary>
        /// <param name="self"></param>
        /// <param name="moveTweenType"></param>
        /// <param name="fixedDeltaTime"></param>
        public static void DoMoveTween_Straight(this MoveTweenObj self, StraightMoveTweenType moveTweenType, float fixedDeltaTime)
        {
            float speed = moveTweenType.Speed;
            float acceleratedSpeed = moveTweenType.AcceleratedSpeed;
            self.speed = speed + self.timeElapsed * acceleratedSpeed;

            ProfilerSample.BeginSample($"DoMoveTween_Straight self.GetUnit().Forward");
            self.GetUnit().Forward = self.forward;
            ProfilerSample.EndSample();

            ProfilerSample.BeginSample($"DoMoveTween_Straight self.GetUnit().Position 11");
            float3 pos = self.GetUnit().Position;
            pos += math.normalize(self.forward) * self.speed * fixedDeltaTime;
            ProfilerSample.EndSample();
            ProfilerSample.BeginSample($"DoMoveTween_Straight self.GetUnit().Position 22");
            self.GetUnit().Position = pos;
            ProfilerSample.EndSample();
        }

        /// <summary>
        /// 追踪某个对象
        /// </summary>
        /// <param name="self"></param>
        /// <param name="moveTweenType"></param>
        /// <param name="fixedDeltaTime"></param>
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
                if (UnitHelper.ChkUnitAlive(targetUnit, true))
                {
                    dir = targetUnit.Position - unit.Position;
                }
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

        /// <summary>
        /// 环绕某个对象
        /// </summary>
        /// <param name="self"></param>
        /// <param name="moveTweenType"></param>
        /// <param name="fixedDeltaTime"></param>
        public static void DoMoveTween_Around(this MoveTweenObj self, AroundMoveTweenType moveTweenType, float fixedDeltaTime)
        {
            float3 targetPosition;
            SelectHandleType selectHandleType = self.selectHandle.selectHandleType;
            if (selectHandleType == SelectHandleType.SelectUnits)
            {
                if (self.selectHandle.unitIds.Count == 0)
                {
                    self.GetUnit().DestroyWithDeathShow();
                    return;
                }
                long targetUnitId = self.selectHandle.unitIds[0];
                Unit targetUnit = UnitHelper.GetUnit(self.DomainScene(), targetUnitId);
                if (targetUnit == null)
                {
                    self.GetUnit().DestroyWithDeathShow();
                    return;
                }
                targetPosition = targetUnit.Position + new float3(0, UnitHelper.GetBodyHeight(targetUnit) * 0.5f, 0);
            }
            else if (selectHandleType == SelectHandleType.SelectPosition)
            {
                targetPosition = self.selectHandle.position;
            }
            else
            {
                return;
            }
            Unit unit = self.GetUnit();
            float speed = moveTweenType.Speed;
            float acceleratedSpeed = moveTweenType.AcceleratedSpeed;
            float orgRadius = moveTweenType.Radius;
            float initAngle = moveTweenType.InitAngle;
            self.speed = speed + self.timeElapsed * acceleratedSpeed;

            float radiusAddSpeed = moveTweenType.RadiusAddSpeed;
            float radius = orgRadius + self.timeElapsed * radiusAddSpeed;

            float curAngle = initAngle + self.speed / orgRadius * self.timeElapsed;
            float3 curPosDir = math.mul(quaternion.RotateY(curAngle), math.forward());
            self.forward = math.mul(quaternion.RotateY(math.PI*0.5f), curPosDir);
            self.forward = math.normalize(self.forward);
            unit.Forward = self.forward;
            unit.Position = targetPosition + curPosDir * radius;
        }

        /// <summary>
        /// 特定目标轨迹
        /// </summary>
        /// <param name="self"></param>
        /// <param name="moveTweenType"></param>
        /// <param name="fixedDeltaTime"></param>
        public static void DoMoveTween_Target(this MoveTweenObj self, TargetMoveTweenType moveTweenType, float fixedDeltaTime)
        {
            float3 targetPosition;
            SelectHandleType selectHandleType = self.selectHandle.selectHandleType;
            if (selectHandleType == SelectHandleType.SelectUnits)
            {
                if (self.selectHandle.unitIds.Count > 0)
                {
                    long targetUnitId = self.selectHandle.unitIds[0];
                    Unit targetUnit = UnitHelper.GetUnit(self.DomainScene(), targetUnitId);
                    if (UnitHelper.ChkUnitAlive(targetUnit, true))
                    {
                        targetPosition = targetUnit.Position + new float3(0, UnitHelper.GetBodyHeight(targetUnit) * 0.5f, 0);
                        self.lastTargetPosition = targetPosition;
                    }
                    else
                    {
                        Unit bulletUnit = self.GetUnit();
                        if (bulletUnit.Position.Equals(self.lastTargetPosition))
                        {
                            self.GetUnit().DestroyWithDeathShow();
                            return;
                        }
                        targetPosition = self.lastTargetPosition;
                    }
                }
                else
                {
                    Unit unitTmp = self.GetUnit();
                    targetPosition = unitTmp.Position + new float3(0, UnitHelper.GetBodyHeight(unitTmp) * 0.5f, 0)+ self.forward * 100;
                }
            }
            else if (selectHandleType == SelectHandleType.SelectPosition)
            {
                targetPosition = self.selectHandle.position;
            }
            else
            {
                return;
            }
            Unit unit = self.GetUnit();
            float speed = moveTweenType.Speed;
            float acceleratedSpeed = moveTweenType.AcceleratedSpeed;
            self.speed = speed + self.timeElapsed * acceleratedSpeed;

            float3 dir = targetPosition - unit.Position;
            float dirLengthSq = math.lengthsq(dir);
            if (dirLengthSq == 0)
            {
            }
            else if (dirLengthSq <= self.speed * fixedDeltaTime * self.speed * fixedDeltaTime)
            {
                self.forward = math.normalize(dir);
                unit.Forward = self.forward;
                unit.Position = targetPosition;
            }
            else
            {
                self.forward = math.normalize(dir);
                unit.Forward = self.forward;
                unit.Position += self.forward * self.speed * fixedDeltaTime;
            }
        }
    }
}