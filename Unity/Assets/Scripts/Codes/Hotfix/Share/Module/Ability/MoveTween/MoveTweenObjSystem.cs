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
                self.moveTweenType = null;
                //self.selectHandle?.Dispose();
                self.selectHandle = null;
            }
        }

        [ObjectSystem]
        public class MoveTweenObjFixedUpdateSystem: FixedUpdateSystem<MoveTweenObj>
        {
            protected override void FixedUpdate(MoveTweenObj self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
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
            self.speed = moveTweenType.Speed;
            self.forward = selectHandle.direction;
            self.isNeedChkHoldTime = true;
            self.lastPosition = self.GetUnit().Position;
            self.holdTime = moveTweenType.HoldTime;

            if (self.moveTweenType is StraightMoveTweenType straightMoveTweenType)
            {
            }
            else if (self.moveTweenType is TrackingMoveTweenType trackingMoveTweenType)
            {
                self.selectHandle = ET.Ability.SelectHandle.Clone(selectHandle);
            }
            else if (self.moveTweenType is AroundMoveTweenType aroundMoveTweenType)
            {
                self.selectHandle = ET.Ability.SelectHandle.Clone(selectHandle);
            }
            else if (self.moveTweenType is TargetMoveTweenType targetMoveTweenType)
            {
                self.selectHandle = ET.Ability.SelectHandle.Clone(selectHandle);
            }
            else if (self.moveTweenType is TargetLimitTimeMoveTweenType targetLimitTimeMoveTweenType)
            {
                self.selectHandle = ET.Ability.SelectHandle.Clone(selectHandle);
            }
            else if (self.moveTweenType is TargetQuickMoveTweenType targetQuickMoveTweenType)
            {
                self.selectHandle = ET.Ability.SelectHandle.Clone(selectHandle);
            }
        }

        public static void ChgSelectHandle(this MoveTweenObj self, SelectHandle selectHandle)
        {
            self.timeElapsed = 0;
            if (self.moveTweenType is StraightMoveTweenType straightMoveTweenType)
            {
            }
            else if (self.moveTweenType is TrackingMoveTweenType trackingMoveTweenType)
            {
                self.selectHandle = ET.Ability.SelectHandle.Clone(selectHandle);
            }
            else if (self.moveTweenType is AroundMoveTweenType aroundMoveTweenType)
            {
                self.selectHandle = ET.Ability.SelectHandle.Clone(selectHandle);
            }
            else if (self.moveTweenType is TargetMoveTweenType targetMoveTweenType)
            {
                self.selectHandle = ET.Ability.SelectHandle.Clone(selectHandle);
            }
            else if (self.moveTweenType is TargetLimitTimeMoveTweenType targetLimitTimeMoveTweenType)
            {
                self.selectHandle = ET.Ability.SelectHandle.Clone(selectHandle);
            }
            else if (self.moveTweenType is TargetQuickMoveTweenType targetQuickMoveTweenType)
            {
                self.selectHandle = ET.Ability.SelectHandle.Clone(selectHandle);
            }
        }

        public static bool IsNeedChkTouch(this MoveTweenObj self)
        {
            if (self.selectHandle.selectHandleType != SelectHandleType.SelectUnits)
            {
                return true;
            }

            if (self.moveTweenType is StraightMoveTweenType straightMoveTweenType)
            {
                return true;
            }
            else if (self.moveTweenType is TrackingMoveTweenType trackingMoveTweenType)
            {
                return false;
            }
            else if (self.moveTweenType is AroundMoveTweenType aroundMoveTweenType)
            {
                return true;
            }
            else if (self.moveTweenType is TargetMoveTweenType targetMoveTweenType)
            {
                return false;
            }
            else if (self.moveTweenType is TargetLimitTimeMoveTweenType targetLimitTimeMoveTweenType)
            {
                return false;
            }
            else if (self.moveTweenType is TargetQuickMoveTweenType targetQuickMoveTweenType)
            {
                return false;
            }

            return true;
        }

        public static bool ChkCanTouchUnit(this MoveTweenObj self, Unit unit)
        {
            if (self.selectHandle.selectHandleType != SelectHandleType.SelectUnits)
            {
                return true;
            }

            if (self.moveTweenType is StraightMoveTweenType straightMoveTweenType)
            {
                return true;
            }
            else if (self.moveTweenType is TrackingMoveTweenType trackingMoveTweenType)
            {
                return false;
            }
            else if (self.moveTweenType is AroundMoveTweenType aroundMoveTweenType)
            {
                return true;
            }
            else if (self.moveTweenType is TargetMoveTweenType targetMoveTweenType)
            {
                return false;
            }
            else if (self.moveTweenType is TargetLimitTimeMoveTweenType targetLimitTimeMoveTweenType)
            {
                return false;
            }
            else if (self.moveTweenType is TargetQuickMoveTweenType targetQuickMoveTweenType)
            {
                return false;
            }

            return true;
        }

        public static Unit GetUnit(this MoveTweenObj self)
        {
            return self.GetParent<Unit>();
        }

        public static void FixedUpdate(this MoveTweenObj self, float fixedDeltaTime)
        {
            float timePassed = fixedDeltaTime;
            if (self.isNeedChkHoldTime)
            {
                if (self.ChkIsHoldTime(fixedDeltaTime))
                {
                    return;
                }
            }
            self.timeElapsed += timePassed;
            self.lastPosition = self.GetUnit().Position;
            self.DoMoveTween(fixedDeltaTime);
        }

        public static bool ChkIsHoldTime(this MoveTweenObj self, float fixedDeltaTime)
        {
            if (self.holdTime > 0)
            {
                self.holdTime -= fixedDeltaTime;
                return true;
            }
            self.isNeedChkHoldTime = false;
            return false;
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
            else if (self.moveTweenType is TargetMoveTweenType targetMoveTweenType)
            {
                if (self.selectHandle.selectHandleType == SelectHandleType.SelectDirection)
                {
                    self.DoMoveTween_Straight(self.moveTweenType as StraightMoveTweenType, fixedDeltaTime);
                }
                else
                {
                    self.DoMoveTween_Target(targetMoveTweenType, fixedDeltaTime);
                }
            }
            else if (self.moveTweenType is TargetLimitTimeMoveTweenType targetLimitTimeMoveTweenType)
            {
                if (self.selectHandle.selectHandleType == SelectHandleType.SelectDirection)
                {
                    self.DoMoveTween_Straight(self.moveTweenType as StraightMoveTweenType, fixedDeltaTime);
                }
                else
                {
                    self.DoMoveTween_TargetLimitTime(targetLimitTimeMoveTweenType, fixedDeltaTime);
                }
            }
            else if (self.moveTweenType is TargetQuickMoveTweenType targetQuickMoveTweenType)
            {
                if (self.selectHandle.selectHandleType == SelectHandleType.SelectDirection)
                {
                    self.DoMoveTween_Straight(self.moveTweenType as StraightMoveTweenType, fixedDeltaTime);
                }
                else
                {
                    self.DoMoveTween_TargetQuick(targetQuickMoveTweenType, fixedDeltaTime);
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

            self.GetUnit().Forward = self.forward;

            float3 pos = self.GetUnit().Position;
            pos += math.normalize(self.forward) * self.speed * fixedDeltaTime;
            self.GetUnit().Position = pos;
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

            if (self.selectHandle.unitIds.Count == 0)
            {
                unit.Position += unit.Forward * self.speed * fixedDeltaTime;
                return;
            }

            long targetUnitId = self.selectHandle.unitIds[0];
            targetUnit = UnitHelper.GetUnit(self.DomainScene(), targetUnitId);
            if (UnitHelper.ChkUnitAlive(targetUnit, true) == false)
            {
                unit.Position += unit.Forward * self.speed * fixedDeltaTime;
                return;
            }

            float3 dir = targetUnit.Position - unit.Position;
            float dirLengthSq = math.lengthsq(dir);

            if (dirLengthSq == 0)
            {
                if (targetUnit != null)
                {
                    if (UnitHelper.ChkIsBullet(unit))
                    {
#if UNITY_EDITOR
                        if (ET.GamePlayHelper.ChkIsFriend(targetUnit, unit))
                        {
                            Log.Error($"===== DoMoveTween_Target ChkIsFriend");
                        }
#endif
                        BulletObj bulletObj = unit.GetComponent<BulletObj>();
                        bulletObj.AddPreHitUnit(targetUnit.Id);
                    }
                }
                return;
            }
            else if (dirLengthSq <= self.speed * fixedDeltaTime * self.speed * fixedDeltaTime)
            {
                self.forward = math.normalize(dir);
                unit.Forward = self.forward;
                unit.Position = targetUnit.Position;

                if (targetUnit != null)
                {
                    if (UnitHelper.ChkIsBullet(unit))
                    {
#if UNITY_EDITOR
                        if (ET.GamePlayHelper.ChkIsFriend(targetUnit, unit))
                        {
                            Log.Error($"===== DoMoveTween_Target ChkIsFriend");
                        }
#endif
                        BulletObj bulletObj = unit.GetComponent<BulletObj>();
                        bulletObj.AddPreHitUnit(targetUnit.Id);
                    }
                }
                return;
            }

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
            Unit targetUnit = null;
            float3 targetPosition;
            SelectHandleType selectHandleType = self.selectHandle.selectHandleType;
            if (selectHandleType == SelectHandleType.SelectUnits)
            {
                if (self.selectHandle == null || self.selectHandle.unitIds == null)
                {
                    if (self.selectHandle == null)
                    {
                        Log.Error($"DoMoveTween_Target self.selectHandle == null");
                    }
                    else if (self.selectHandle.unitIds == null)
                    {
                        Log.Error($"DoMoveTween_Target self.selectHandle.unitIds == null");
                    }

                    Unit unitTmp = self.GetUnit();
                    targetPosition = unitTmp.Position + new float3(0, UnitHelper.GetBodyHeight(unitTmp) * 0.5f, 0)+ self.forward * 100;
                }
                else if (self.selectHandle.unitIds.Count > 0)
                {
                    long targetUnitId = self.selectHandle.unitIds[0];
                    targetUnit = UnitHelper.GetUnit(self.DomainScene(), targetUnitId);
                    if (UnitHelper.ChkUnitAlive(targetUnit, false))
                    {
                        targetPosition = targetUnit.Position + new float3(0, UnitHelper.GetBodyHeight(targetUnit) * 0.5f, 0);
                        self.lastTargetPosition = targetPosition;
                    }
                    else if (UnitHelper.ChkUnitAlive(targetUnit, true))
                    {
                        targetPosition = targetUnit.Position + new float3(0, UnitHelper.GetBodyHeight(targetUnit) * 0.5f, 0);
                        self.lastTargetPosition = targetPosition;

                        // Unit bulletUnit = self.GetUnit();
                        // if (bulletUnit.Position.Equals(self.lastTargetPosition))
                        // {
                        //     self.GetUnit().DestroyWithDeathShow();
                        //     return;
                        // }
                    }
                    else
                    {
                        // Unit bulletUnit = self.GetUnit();
                        // if (bulletUnit.Position.Equals(self.lastTargetPosition))
                        // {
                        //     self.GetUnit().DestroyWithDeathShow();
                        //     return;
                        // }
                        // targetPosition = self.lastTargetPosition;

                        Unit unitTmp = self.GetUnit();
                        targetPosition = unitTmp.Position + new float3(0, UnitHelper.GetBodyHeight(unitTmp) * 0.5f, 0)+ self.forward * 100;
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
                if (targetUnit != null)
                {
                    if (UnitHelper.ChkIsBullet(unit))
                    {
#if UNITY_EDITOR
                        if (ET.GamePlayHelper.ChkIsFriend(targetUnit, unit))
                        {
                            Log.Error($"===== DoMoveTween_Target ChkIsFriend");
                        }
#endif
                        BulletObj bulletObj = unit.GetComponent<BulletObj>();
                        bulletObj.AddPreHitUnit(targetUnit.Id);
                    }
                }
                return;
            }
            else if (dirLengthSq <= self.speed * fixedDeltaTime * self.speed * fixedDeltaTime)
            {
                self.forward = math.normalize(dir);
                unit.Forward = self.forward;
                unit.Position = targetPosition;

                if (targetUnit != null)
                {
                    if (UnitHelper.ChkIsBullet(unit))
                    {
#if UNITY_EDITOR
                        if (ET.GamePlayHelper.ChkIsFriend(targetUnit, unit))
                        {
                            Log.Error($"===== DoMoveTween_Target ChkIsFriend");
                        }
#endif
                        BulletObj bulletObj = unit.GetComponent<BulletObj>();
                        bulletObj.AddPreHitUnit(targetUnit.Id);
                    }
                }
                return;
            }
            else
            {
                self.forward = math.normalize(dir);
                unit.Forward = self.forward;
                unit.Position += self.forward * self.speed * fixedDeltaTime;
            }
        }

        public static void DoMoveTween_TargetLimitTime(this MoveTweenObj self, TargetLimitTimeMoveTweenType moveTweenType, float fixedDeltaTime)
        {
            Unit targetUnit = null;
            float3 targetPosition;
            SelectHandleType selectHandleType = self.selectHandle.selectHandleType;
            if (selectHandleType == SelectHandleType.SelectUnits)
            {
                if (self.selectHandle == null || self.selectHandle.unitIds == null)
                {
                    if (self.selectHandle == null)
                    {
                        Log.Error($"DoMoveTween_Target self.selectHandle == null");
                    }
                    else if (self.selectHandle.unitIds == null)
                    {
                        Log.Error($"DoMoveTween_Target self.selectHandle.unitIds == null");
                    }

                    Unit unitTmp = self.GetUnit();
                    targetPosition = unitTmp.Position + new float3(0, UnitHelper.GetBodyHeight(unitTmp) * 0.5f, 0)+ self.forward * 100;
                }
                else if (self.selectHandle.unitIds.Count > 0)
                {
                    long targetUnitId = self.selectHandle.unitIds[0];
                    targetUnit = UnitHelper.GetUnit(self.DomainScene(), targetUnitId);
                    if (UnitHelper.ChkUnitAlive(targetUnit, false))
                    {
                        targetPosition = targetUnit.Position + new float3(0, UnitHelper.GetBodyHeight(targetUnit) * 0.5f, 0);
                        self.lastTargetPosition = targetPosition;
                    }
                    else if (UnitHelper.ChkUnitAlive(targetUnit, true))
                    {
                        targetPosition = targetUnit.Position + new float3(0, UnitHelper.GetBodyHeight(targetUnit) * 0.5f, 0);
                        self.lastTargetPosition = targetPosition;

                        // Unit bulletUnit = self.GetUnit();
                        // if (bulletUnit.Position.Equals(self.lastTargetPosition))
                        // {
                        //     self.GetUnit().DestroyWithDeathShow();
                        //     return;
                        // }
                    }
                    else
                    {
                        // Unit bulletUnit = self.GetUnit();
                        // if (bulletUnit.Position.Equals(self.lastTargetPosition))
                        // {
                        //     self.GetUnit().DestroyWithDeathShow();
                        //     return;
                        // }
                        // targetPosition = self.lastTargetPosition;

                        Unit unitTmp = self.GetUnit();
                        targetPosition = unitTmp.Position + new float3(0, UnitHelper.GetBodyHeight(unitTmp) * 0.5f, 0)+ self.forward * 100;
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
            float3 dir = targetPosition - unit.Position;

            float speed = moveTweenType.Speed;
            float acceleratedSpeed = moveTweenType.AcceleratedSpeed;
            self.speed = speed + self.timeElapsed * acceleratedSpeed;

            if (moveTweenType.LimitTime <= self.timeElapsed)
            {
                if (targetUnit != null)
                {
                    if (UnitHelper.ChkIsBullet(unit))
                    {
#if UNITY_EDITOR
                        if (ET.GamePlayHelper.ChkIsFriend(targetUnit, unit))
                        {
                            Log.Error($"===== DoMoveTween_Target ChkIsFriend");
                        }
#endif
                        BulletObj bulletObj = unit.GetComponent<BulletObj>();
                        bulletObj.AddPreHitUnit(targetUnit.Id);
                    }
                }
                return;
            }
            else
            {
                var speedLimit = math.length(dir) / (moveTweenType.LimitTime-self.timeElapsed);
                if (self.speed < speedLimit)
                {
                    self.speed = speedLimit;
                }
            }

            float dirLengthSq = math.lengthsq(dir);
            if (dirLengthSq == 0)
            {
                if (targetUnit != null)
                {
                    if (UnitHelper.ChkIsBullet(unit))
                    {
#if UNITY_EDITOR
                        if (ET.GamePlayHelper.ChkIsFriend(targetUnit, unit))
                        {
                            Log.Error($"===== DoMoveTween_Target ChkIsFriend");
                        }
#endif
                        BulletObj bulletObj = unit.GetComponent<BulletObj>();
                        bulletObj.AddPreHitUnit(targetUnit.Id);
                    }
                }
                return;
            }
            else if (dirLengthSq <= self.speed * fixedDeltaTime * self.speed * fixedDeltaTime)
            {
                self.forward = math.normalize(dir);
                unit.Forward = self.forward;
                unit.Position = targetPosition;

                if (targetUnit != null)
                {
                    if (UnitHelper.ChkIsBullet(unit))
                    {
#if UNITY_EDITOR
                        if (ET.GamePlayHelper.ChkIsFriend(targetUnit, unit))
                        {
                            Log.Error($"===== DoMoveTween_Target ChkIsFriend");
                        }
#endif
                        BulletObj bulletObj = unit.GetComponent<BulletObj>();
                        bulletObj.AddPreHitUnit(targetUnit.Id);
                    }
                }
                return;
            }
            else
            {
                self.forward = math.normalize(dir);
                unit.Forward = self.forward;
                unit.Position += self.forward * self.speed * fixedDeltaTime;
            }
        }

        public static void DoMoveTween_TargetQuick(this MoveTweenObj self, TargetQuickMoveTweenType moveTweenType, float fixedDeltaTime)
        {
            Unit targetUnit = null;
            float3 targetPosition;
            SelectHandleType selectHandleType = self.selectHandle.selectHandleType;
            if (selectHandleType == SelectHandleType.SelectUnits)
            {
                if (self.selectHandle == null || self.selectHandle.unitIds == null)
                {
                    if (self.selectHandle == null)
                    {
                        Log.Error($"DoMoveTween_Target self.selectHandle == null");
                    }
                    else if (self.selectHandle.unitIds == null)
                    {
                        Log.Error($"DoMoveTween_Target self.selectHandle.unitIds == null");
                    }

                    self.GetUnit().DestroyWithDeathShow();
                    return;
                }
                else if (self.selectHandle.unitIds.Count > 0)
                {
                    long targetUnitId = self.selectHandle.unitIds[0];
                    targetUnit = UnitHelper.GetUnit(self.DomainScene(), targetUnitId);
                    if (UnitHelper.ChkUnitAlive(targetUnit, false))
                    {
                        targetPosition = targetUnit.Position + new float3(0, UnitHelper.GetBodyHeight(targetUnit) * 0.5f, 0);
                        self.lastTargetPosition = targetPosition;
                    }
                    else if (UnitHelper.ChkUnitAlive(targetUnit, true))
                    {
                        targetPosition = targetUnit.Position + new float3(0, UnitHelper.GetBodyHeight(targetUnit) * 0.5f, 0);
                        self.lastTargetPosition = targetPosition;
                    }
                    else
                    {
                        self.GetUnit().DestroyWithDeathShow();
                        return;
                    }
                }
                else
                {
                    self.GetUnit().DestroyWithDeathShow();
                    return;
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
            if (moveTweenType.LimitTime <= self.timeElapsed)
            {
                Unit unit = self.GetUnit();
                float3 dir = targetPosition - unit.Position;
                float dirLengthSq = math.lengthsq(dir);
                if (dirLengthSq == 0)
                {
                }
                else
                {
                    self.forward = math.normalize(dir);
                    unit.Forward = self.forward;
                    unit.Position = targetPosition;
                }

                if (targetUnit != null)
                {
                    if (UnitHelper.ChkIsBullet(unit))
                    {
#if UNITY_EDITOR
                        if (ET.GamePlayHelper.ChkIsFriend(targetUnit, unit))
                        {
                            Log.Error($"===== DoMoveTween_Target ChkIsFriend");
                        }
#endif
                        BulletObj bulletObj = unit.GetComponent<BulletObj>();
                        bulletObj.AddPreHitUnit(targetUnit.Id);
                    }
                }
                return;
            }
        }
    }
}