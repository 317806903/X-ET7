﻿using System;
using System.Collections.Generic;
using System.Numerics;
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
                self.moveTweenCfgId = null;
                //self.selectHandle?.Dispose();
                self.selectHandle = null;
            }
        }

        [ObjectSystem]
        public class MoveTweenObjFixedUpdateSystem: FixedUpdateSystem<MoveTweenObj>
        {
            protected override void FixedUpdate(MoveTweenObj self)
            {
                // if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                // {
                //     return;
                // }
                if (self.IsDisposed)
                {
                    return;
                }

                if (self.DomainScene().SceneType != SceneType.Map && self.DomainScene().SceneType != SceneType.Current)
                {
                    return;
                }

                self.FixedUpdate();
            }
        }

        public static void Init(this MoveTweenObj self, long unitId, string moveTweenCfgId, SelectHandle selectHandle)
        {
            self.moveTweenCfgId = moveTweenCfgId;

            self.isNeedChkHoldTime = true;
            self.startPosition = self.GetUnit().Position;
            self.lastPosition = self.GetUnit().Position;

            if (self.moveTweenType is StayMoveTweenType stayMoveTweenType)
            {
                float3 newPosition = selectHandle.position;
                float3 newForward = selectHandle.direction;
                self.SetUnitPos(newPosition);
                self.startPosition = self.GetUnit().Position;
                self.lastPosition = self.GetUnit().Position;
                if (stayMoveTweenType.KeepHorizontal)
                {
                    if (newForward.Equals(float3.zero) == false)
                    {
                        newForward.y = 0;
                        newForward = math.normalize(newForward);
                        self.SetUnitForward(newForward);
                    }
                    else
                    {
                        float3 forward = self.GetUnit().Forward;
                        forward.y = 0;
                        forward = math.normalize(forward);
                        self.SetUnitForward(forward);
                    }
                }
                else
                {
                    if (newForward.Equals(float3.zero) == false)
                    {
                        newForward = math.normalize(newForward);
                        self.SetUnitForward(newForward);
                    }
                }
            }
            else if (self.moveTweenType is StayOnGroundMoveTweenType stayOnGroundMoveTweenType)
            {
                float3 newPosition = selectHandle.position;
                float3 newForward = selectHandle.direction;
                self.SetUnitPos(newPosition);
                self.startPosition = self.GetUnit().Position;
                self.lastPosition = self.GetUnit().Position;
                float3 hitNavmeshPos = ET.RecastHelper.GetHitNavmeshPos(self.DomainScene(), self.GetUnit().Position);
                if (hitNavmeshPos.Equals(float3.zero) == false)
                {
                    self.SetUnitPos(hitNavmeshPos);
                    self.startPosition = self.GetUnit().Position;
                    self.lastPosition = self.GetUnit().Position;
                }
                if (stayOnGroundMoveTweenType.KeepHorizontal)
                {
                    if (newForward.Equals(float3.zero) == false)
                    {
                        newForward.y = 0;
                        newForward = math.normalize(newForward);
                        self.SetUnitForward(newForward);
                    }
                    else
                    {
                        float3 forward = self.GetUnit().Forward;
                        forward.y = 0;
                        forward = math.normalize(forward);
                        self.SetUnitForward(forward);
                    }
                }
                else
                {
                    if (newForward.Equals(float3.zero) == false)
                    {
                        newForward = math.normalize(newForward);
                        self.SetUnitForward(newForward);
                    }
                }
            }
            else if (self.moveTweenType is SpeedMoveTweenType speedMoveTweenType)
            {
                if (selectHandle.direction.Equals(float3.zero))
                {
                    self.forward = selectHandle.position - self.GetUnit().Position;
                    if (self.forward.Equals(float3.zero))
                    {
                        self.forward = self.GetUnit().Forward;
                    }
                    self.forward = math.normalize(self.forward);
                    self.SetUnitForward(self.forward);
                }
                else
                {
                    self.forward = selectHandle.direction;
                    self.forward = math.normalize(self.forward);
                    self.SetUnitForward(self.forward);
                }

                self.startTime = TimeHelper.ServerNow() + (long)(math.max(0, speedMoveTweenType.HoldTime) * 1000);
                self.speed = speedMoveTweenType.Speed;

                if (self.moveTweenType is StraightMoveTweenType straightMoveTweenType)
                {
                    if (straightMoveTweenType.IsDownFromAir)
                    {
                        float3 newPosition = selectHandle.position + new float3(0, straightMoveTweenType.HeightWhenAir, 0);
                        self.SetUnitPos(newPosition);
                        self.startPosition = self.GetUnit().Position;
                        self.lastPosition = self.GetUnit().Position;

                        self.forward = selectHandle.position - newPosition;
                        self.forward = math.normalize(self.forward);
                        self.SetUnitForward(self.forward);
                    }
                    if (straightMoveTweenType.KeepHorizontal)
                    {
                        self.forward.y = 0;
                        self.forward = math.normalize(self.forward);
                        self.SetUnitForward(self.forward);
                    }
                    self.selectHandle = ET.Ability.SelectHandle.Clone(selectHandle);
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
                    if (targetMoveTweenType.IsDownFromAir)
                    {
                        if (selectHandle.unitIds != null && selectHandle.unitIds.Count > 0)
                        {
                            long targetUnitId = selectHandle.unitIds[0];
                            Unit targetUnit = UnitHelper.GetUnit(self.DomainScene(), targetUnitId);
                            if (targetUnit != null)
                            {
                                float3 newPosition = targetUnit.Position + new float3(0, targetMoveTweenType.HeightWhenAir, 0);
                                self.SetUnitPos(newPosition);
                                self.startPosition = self.GetUnit().Position;
                                self.lastPosition = self.GetUnit().Position;
                                self.forward = targetUnit.Position - newPosition;
                                self.forward = math.normalize(self.forward);
                                self.SetUnitForward(self.forward);
                            }
                        }
                    }
                    self.selectHandle = ET.Ability.SelectHandle.Clone(selectHandle);
                }
                else if (self.moveTweenType is TargetLimitTimeMoveTweenType targetLimitTimeMoveTweenType)
                {
                    if (targetLimitTimeMoveTweenType.IsDownFromAir)
                    {
                        if (selectHandle.unitIds != null && selectHandle.unitIds.Count > 0)
                        {
                            long targetUnitId = selectHandle.unitIds[0];
                            Unit targetUnit = UnitHelper.GetUnit(self.DomainScene(), targetUnitId);
                            if (targetUnit != null)
                            {
                                float3 newPosition = targetUnit.Position + new float3(0, targetLimitTimeMoveTweenType.HeightWhenAir, 0);
                                self.SetUnitPos(newPosition);
                                self.startPosition = self.GetUnit().Position;
                                self.lastPosition = self.GetUnit().Position;
                                self.forward = targetUnit.Position - newPosition;
                                self.forward = math.normalize(self.forward);
                                self.SetUnitForward(self.forward);
                            }
                        }
                    }
                    self.selectHandle = ET.Ability.SelectHandle.Clone(selectHandle);
                }
                else if (self.moveTweenType is TargetQuickMoveTweenType targetQuickMoveTweenType)
                {
                    self.selectHandle = ET.Ability.SelectHandle.Clone(selectHandle);
                }
                else if (self.moveTweenType is ParabolaMoveTweenType parabolaMoveTweenType)
                {
                    self.selectHandle = ET.Ability.SelectHandle.Clone(selectHandle);
                }
            }

            self.isInit = true;
        }

        public static float3 GetTargetUnitPos(this MoveTweenObj self, Unit targetUnit)
        {
            return targetUnit.GetUnitClientPos();
        }

        public static void SetUnitPos(this MoveTweenObj self, float3 pos)
        {
            Unit unit = self.GetUnit();
            if (self.DomainScene().SceneType != SceneType.Map)
            {
                unit.SetPositionWhenClient(pos);
            }
            else
            {
                unit.Position = pos;
            }
        }

        public static void SetUnitForward(this MoveTweenObj self, float3 forward)
        {
            if (forward.Equals(float3.zero))
            {
                return;
            }
            Unit unit = self.GetUnit();
            quaternion rotation;
            if (forward.x == 0 && forward.z == 0)
            {
                forward.x = 0.0001f;
                forward = math.normalize(forward);
            }

            rotation = quaternion.LookRotation(forward, math.up());
            if (self.DomainScene().SceneType != SceneType.Map)
            {
                unit.SetRotationWhenClient(rotation);
            }
            else
            {
                unit.Rotation = rotation;
            }
        }

        public static void ChgSelectHandle(this MoveTweenObj self, SelectHandle selectHandle)
        {
            self.timeElapsed = 0;
            if (self.moveTweenType is SpeedMoveTweenType speedMoveTweenType)
            {
                if (self.isNeedChkHoldTime)
                {
                    self.startTime = TimeHelper.ServerNow() + (long)(math.max(0, speedMoveTweenType.HoldTime) * 1000);
                }
                else
                {
                    self.startTime = TimeHelper.ServerNow();
                }
            }
            else
            {
                self.startTime = TimeHelper.ServerNow();
            }
            self.startPosition = self.GetUnit().Position;
            self.lastPosition = self.GetUnit().Position;
            if (self.moveTweenType is StayMoveTweenType stayMoveTweenType)
            {
            }
            else if (self.moveTweenType is StayOnGroundMoveTweenType stayOnGroundMoveTweenType)
            {
                float3 hitNavmeshPos = ET.RecastHelper.GetHitNavmeshPos(self.DomainScene(), self.GetUnit().Position);
                if (hitNavmeshPos.Equals(float3.zero) == false)
                {
                    self.SetUnitPos(hitNavmeshPos);
                    self.startPosition = hitNavmeshPos;
                    self.lastPosition = hitNavmeshPos;
                }
            }
            else if (self.moveTweenType is StraightMoveTweenType straightMoveTweenType)
            {
                self.selectHandle = ET.Ability.SelectHandle.Clone(selectHandle);
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
            else if (self.moveTweenType is ParabolaMoveTweenType parabolaMoveTweenType)
            {
                self.selectHandle = ET.Ability.SelectHandle.Clone(selectHandle);
            }
        }

        public static bool IsNeedDealHit(this MoveTweenObj self)
        {
            if (self.DomainScene().SceneType == SceneType.Map)
            {
                return true;
            }
            if (self.DomainScene().SceneType == SceneType.Current)
            {
                return false;
            }
            return false;
        }

        public static bool IsNeedChkTouch(this MoveTweenObj self)
        {
            if (self.selectHandle != null && self.selectHandle.selectHandleType != SelectHandleType.SelectUnits)
            {
                return true;
            }

            if (self.moveTweenType is StayMoveTweenType stayMoveTweenType)
            {
                return false;
            }
            else if (self.moveTweenType is StayOnGroundMoveTweenType stayOnGroundMoveTweenType)
            {
                return false;
            }
            else if (self.moveTweenType is StraightMoveTweenType straightMoveTweenType)
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
            else if (self.moveTweenType is ParabolaMoveTweenType parabolaMoveTweenType)
            {
                return false;
            }

            return true;
        }

        public static bool ChkCanTouchUnit(this MoveTweenObj self, Unit unit)
        {
            if (self.selectHandle != null && self.selectHandle.selectHandleType != SelectHandleType.SelectUnits)
            {
                return true;
            }

            if (self.moveTweenType is StayMoveTweenType stayMoveTweenType)
            {
                return false;
            }
            else if (self.moveTweenType is StayOnGroundMoveTweenType stayOnGroundMoveTweenType)
            {
                return false;
            }
            else if (self.moveTweenType is StraightMoveTweenType straightMoveTweenType)
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
            else if (self.moveTweenType is ParabolaMoveTweenType parabolaMoveTweenType)
            {
                return false;
            }

            return true;
        }

        public static Unit GetUnit(this MoveTweenObj self)
        {
            return self.GetParent<Unit>();
        }

        public static void FixedUpdate(this MoveTweenObj self)
        {
            if (self.isNeedChkHoldTime)
            {
                if (TimeHelper.ServerNow() <= self.startTime)
                {
                    return;
                }
                self.isNeedChkHoldTime = false;
            }

            float newTimeElapsed = (TimeHelper.ServerNow() - self.startTime) * 0.001f;
            float fixedDeltaTime = newTimeElapsed - self.timeElapsed;
            self.timeElapsed = newTimeElapsed;

            self.lastPosition = self.GetUnit().Position;
            if (self.isInit == false)
            {
                self.DoInitForClient();
            }
            self.DoMoveTween(fixedDeltaTime);
        }

        public static void DoInitForClient(this MoveTweenObj self)
        {
            self.startPosition = ET.Ability.UnitHelper.TranServerPos2ClientPos(self.DomainScene(), self.startPosition);
            self.lastPosition = ET.Ability.UnitHelper.TranServerPos2ClientPos(self.DomainScene(), self.lastPosition);
            self.lastTargetPosition = ET.Ability.UnitHelper.TranServerPos2ClientPos(self.DomainScene(), self.lastTargetPosition);
            self.forward = ET.Ability.UnitHelper.TranServerForward2ClientForward(self.forward);
            if (self.selectHandle != null)
            {
                self.selectHandle.position = ET.Ability.UnitHelper.TranServerPos2ClientPos(self.DomainScene(), self.selectHandle.position);
                self.selectHandle.direction = ET.Ability.UnitHelper.TranServerForward2ClientForward(self.selectHandle.direction);
            }

            self.isInit = true;
        }

        public static void DoMoveTween(this MoveTweenObj self, float fixedDeltaTime)
        {
            if (self.moveTweenType is StayMoveTweenType stayMoveTweenType)
            {
            }
            else if (self.moveTweenType is StayOnGroundMoveTweenType stayOnGroundMoveTweenType)
            {
            }
            else if (self.moveTweenType is StraightMoveTweenType straightMoveTweenType)
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
            else if (self.moveTweenType is ParabolaMoveTweenType parabolaMoveTweenType)
            {
                self.DoMoveTween_Parabola(parabolaMoveTweenType, fixedDeltaTime);
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
            float3 targetPosition = float3.zero;
            SelectHandleType selectHandleType = self.selectHandle.selectHandleType;
            if (selectHandleType == SelectHandleType.SelectPosition)
            {
                targetPosition = self.selectHandle.position;
            }
            Unit unit = self.GetUnit();
            float speed = moveTweenType.Speed;
            float acceleratedSpeed = moveTweenType.AcceleratedSpeed;
            self.speed = (speed + self.timeElapsed * acceleratedSpeed);
            if(!targetPosition.Equals(float3.zero)){
                float3 dir = targetPosition - unit.Position;
                float dirLengthSq = math.lengthsq(dir);
                float3 nextPos = unit.Position + math.normalize(self.forward) * self.speed * fixedDeltaTime;
                if (dirLengthSq <= 0.01f){
                    self.SetUnitPos(targetPosition + math.normalize(self.forward) * self.speed * fixedDeltaTime);
                    return;
                }else if (math.dot((nextPos - targetPosition), self.forward) > 0 || dirLengthSq <= self.speed * fixedDeltaTime * self.speed * fixedDeltaTime){
                    self.SetUnitPos(targetPosition);
                    return;
                }
            }
            float3 pos = unit.Position + math.normalize(self.forward) * self.speed * fixedDeltaTime;
            //self.SetUnitForward(self.forward);
            self.SetUnitPos(pos);
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

            float resScale = ET.Ability.UnitHelper.GetGameResScale(self.DomainScene());
            self.speed *= resScale;

            if (self.selectHandle.unitIds.Count == 0)
            {
                float3 pos2 = unit.Position + unit.Forward * self.speed * fixedDeltaTime;
                self.SetUnitPos(pos2);
                return;
            }

            long targetUnitId = self.selectHandle.unitIds[0];
            targetUnit = UnitHelper.GetUnit(self.DomainScene(), targetUnitId);
            if (UnitHelper.ChkUnitAlive(targetUnit, true) == false)
            {
                float3 pos2 = unit.Position + unit.Forward * self.speed * fixedDeltaTime;
                self.SetUnitPos(pos2);
                return;
            }

            float3 dir = self.GetTargetUnitPos(targetUnit) - unit.Position;
            float dirLengthSq = math.lengthsq(dir);

            if (dirLengthSq == 0)
            {
                if (self.IsNeedDealHit() && targetUnit != null)
                {
                    if (UnitHelper.ChkIsBullet(unit))
                    {
#if UNITY_EDITOR
                        if (self.DomainScene().SceneType == SceneType.Map)
                        {
                            if (UnitHelper.ChkUnitAlive(targetUnit))
                            {
                                if (ET.GamePlayHelper.ChkIsFriend(targetUnit, unit))
                                {
                                    Log.Error($"===== DoMoveTween_Target ChkIsFriend");
                                }
                            }
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
                float3 pos2 = self.GetTargetUnitPos(targetUnit);
                self.SetUnitForward(self.forward);
                self.SetUnitPos(pos2);

                if (self.IsNeedDealHit() && targetUnit != null)
                {
                    if (UnitHelper.ChkIsBullet(unit))
                    {
#if UNITY_EDITOR
                        if (self.DomainScene().SceneType == SceneType.Map)
                        {
                            if (UnitHelper.ChkUnitAlive(targetUnit))
                            {
                                if (ET.GamePlayHelper.ChkIsFriend(targetUnit, unit))
                                {
                                    Log.Error($"===== DoMoveTween_Target ChkIsFriend");
                                }
                            }
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
                self.SetUnitForward(self.forward);
            }

            float3 pos = unit.Position + unit.Forward * self.speed * fixedDeltaTime;
            self.SetUnitPos(pos);
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

                targetPosition = UnitHelper.GetBeAttackPointPos(targetUnit);
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

            float resScale = ET.Ability.UnitHelper.GetGameResScale(self.DomainScene());
            self.speed *= resScale;
            orgRadius *= resScale;
            radius *= resScale;

            float curAngle;
            if (orgRadius == 0)
            {
                curAngle = initAngle + self.speed * self.timeElapsed;
            }
            else
            {
                curAngle = initAngle + self.speed / orgRadius * self.timeElapsed;
            }
            float3 curPosDir = math.mul(quaternion.RotateY(curAngle), math.forward());
            self.forward = math.mul(quaternion.RotateY(math.PI * 0.5f), curPosDir);
            self.forward = math.normalize(self.forward);

            float3 pos = targetPosition + curPosDir * radius;
            self.SetUnitForward(self.forward);
            self.SetUnitPos(pos);
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
                    targetPosition = UnitHelper.GetBeAttackPointPos(unitTmp) + self.forward * 100;
                }
                else if (self.selectHandle.unitIds.Count > 0)
                {
                    long targetUnitId = self.selectHandle.unitIds[0];
                    targetUnit = UnitHelper.GetUnit(self.DomainScene(), targetUnitId);
                    if (UnitHelper.ChkUnitAlive(targetUnit, false))
                    {
                        targetPosition = UnitHelper.GetBeAttackPointPos(targetUnit);
                        self.lastTargetPosition = targetPosition;
                    }
                    else if (UnitHelper.ChkUnitAlive(targetUnit, true))
                    {
                        targetPosition = UnitHelper.GetBeAttackPointPos(targetUnit);
                        self.lastTargetPosition = targetPosition;
                    }
                    else
                    {
                        Unit unitTmp = self.GetUnit();
                        targetPosition = UnitHelper.GetBeAttackPointPos(unitTmp) + self.forward * 100;
                    }
                }
                else
                {
                    Unit unitTmp = self.GetUnit();
                    targetPosition = UnitHelper.GetBeAttackPointPos(unitTmp) + self.forward * 100;
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

            float resScale = ET.Ability.UnitHelper.GetGameResScale(self.DomainScene());
            self.speed *= resScale;

            float3 dir = targetPosition - unit.Position;
            float dirLengthSq = math.lengthsq(dir);
            if (dirLengthSq == 0)
            {
                if (self.IsNeedDealHit() && targetUnit != null)
                {
                    if (UnitHelper.ChkIsBullet(unit))
                    {
#if UNITY_EDITOR
                        if (self.DomainScene().SceneType == SceneType.Map)
                        {
                            if (UnitHelper.ChkUnitAlive(targetUnit))
                            {
                                if (ET.GamePlayHelper.ChkIsFriend(targetUnit, unit))
                                {
                                    Log.Error($"===== DoMoveTween_Target ChkIsFriend");
                                }
                            }
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
                self.SetUnitForward(self.forward);
                self.SetUnitPos(targetPosition);

                if (self.IsNeedDealHit() && targetUnit != null)
                {
                    if (UnitHelper.ChkIsBullet(unit))
                    {
#if UNITY_EDITOR
                        if (self.DomainScene().SceneType == SceneType.Map)
                        {
                            if (UnitHelper.ChkUnitAlive(targetUnit))
                            {
                                if (ET.GamePlayHelper.ChkIsFriend(targetUnit, unit))
                                {
                                    Log.Error($"===== DoMoveTween_Target ChkIsFriend");
                                }
                            }
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
                float3 pos = unit.Position + self.forward * self.speed * fixedDeltaTime;
                self.SetUnitForward(self.forward);
                self.SetUnitPos(pos);
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
                    targetPosition = UnitHelper.GetBeAttackPointPos(unitTmp) + self.forward * 100;
                }
                else if (self.selectHandle.unitIds.Count > 0)
                {
                    long targetUnitId = self.selectHandle.unitIds[0];
                    targetUnit = UnitHelper.GetUnit(self.DomainScene(), targetUnitId);
                    if (UnitHelper.ChkUnitAlive(targetUnit, false))
                    {
                        targetPosition = UnitHelper.GetBeAttackPointPos(targetUnit);
                        self.lastTargetPosition = targetPosition;
                    }
                    else if (UnitHelper.ChkUnitAlive(targetUnit, true))
                    {
                        targetPosition = UnitHelper.GetBeAttackPointPos(targetUnit);
                        self.lastTargetPosition = targetPosition;
                    }
                    else
                    {
                        Unit unitTmp = self.GetUnit();
                        targetPosition = UnitHelper.GetBeAttackPointPos(unitTmp) + self.forward * 100;
                    }
                }
                else
                {
                    Unit unitTmp = self.GetUnit();
                    targetPosition = UnitHelper.GetBeAttackPointPos(unitTmp) + self.forward * 100;
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

            float resScale = ET.Ability.UnitHelper.GetGameResScale(self.DomainScene());
            self.speed *= resScale;

            if (moveTweenType.LimitTime <= self.timeElapsed)
            {
                if (self.IsNeedDealHit() && targetUnit != null)
                {
                    if (UnitHelper.ChkIsBullet(unit))
                    {
#if UNITY_EDITOR
                        if (self.DomainScene().SceneType == SceneType.Map)
                        {
                            if (UnitHelper.ChkUnitAlive(targetUnit))
                            {
                                if (ET.GamePlayHelper.ChkIsFriend(targetUnit, unit))
                                {
                                    Log.Error($"===== DoMoveTween_Target ChkIsFriend");
                                }
                            }
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
                var speedLimit = math.length(dir) / (moveTweenType.LimitTime - self.timeElapsed);
                if (self.speed < speedLimit)
                {
                    self.speed = speedLimit;
                }
            }

            float dirLengthSq = math.lengthsq(dir);
            if (dirLengthSq == 0)
            {
                if (self.IsNeedDealHit() && targetUnit != null)
                {
                    if (UnitHelper.ChkIsBullet(unit))
                    {
#if UNITY_EDITOR
                        if (self.DomainScene().SceneType == SceneType.Map)
                        {
                            if (UnitHelper.ChkUnitAlive(targetUnit))
                            {
                                if (ET.GamePlayHelper.ChkIsFriend(targetUnit, unit))
                                {
                                    Log.Error($"===== DoMoveTween_Target ChkIsFriend");
                                }
                            }
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
                self.SetUnitForward(self.forward);
                self.SetUnitPos(targetPosition);

                if (self.IsNeedDealHit() && targetUnit != null)
                {
                    if (UnitHelper.ChkIsBullet(unit))
                    {
#if UNITY_EDITOR
                        if (self.DomainScene().SceneType == SceneType.Map)
                        {
                            if (UnitHelper.ChkUnitAlive(targetUnit))
                            {
                                if (ET.GamePlayHelper.ChkIsFriend(targetUnit, unit))
                                {
                                    Log.Error($"===== DoMoveTween_Target ChkIsFriend");
                                }
                            }
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
                float3 pos = unit.Position + self.forward * self.speed * fixedDeltaTime;
                self.SetUnitForward(self.forward);
                self.SetUnitPos(pos);
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
                        targetPosition = UnitHelper.GetBeAttackPointPos(targetUnit);
                        self.lastTargetPosition = targetPosition;
                    }
                    else if (UnitHelper.ChkUnitAlive(targetUnit, true))
                    {
                        targetPosition = UnitHelper.GetBeAttackPointPos(targetUnit);
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
                    self.SetUnitForward(self.forward);
                    self.SetUnitPos(targetPosition);
                }

                if (self.IsNeedDealHit() && targetUnit != null)
                {
                    if (UnitHelper.ChkIsBullet(unit))
                    {
#if UNITY_EDITOR
                        if (self.DomainScene().SceneType == SceneType.Map)
                        {
                            if (UnitHelper.ChkUnitAlive(targetUnit))
                            {
                                if (ET.GamePlayHelper.ChkIsFriend(targetUnit, unit))
                                {
                                    Log.Error($"===== DoMoveTween_Target ChkIsFriend");
                                }
                            }
                        }
#endif
                        BulletObj bulletObj = unit.GetComponent<BulletObj>();
                        bulletObj.AddPreHitUnit(targetUnit.Id);
                    }
                }

                return;
            }
        }

        public static void DoMoveTween_Parabola(this MoveTweenObj self, ParabolaMoveTweenType moveTweenType, float fixedDeltaTime)
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
                        targetPosition = UnitHelper.GetBeAttackPointPos(targetUnit);
                        self.lastTargetPosition = targetPosition;
                    }
                    else if (UnitHelper.ChkUnitAlive(targetUnit, true))
                    {
                        targetPosition = UnitHelper.GetBeAttackPointPos(targetUnit);
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
                    self.GetUnit().DestroyWithDeathShow();
                    return;
                }
            }
            else if (selectHandleType == SelectHandleType.SelectPosition)
            {
                targetPosition = self.selectHandle.position;
                if (targetPosition.Equals(float3.zero))
                {
                    self.GetUnit().DestroyWithDeathShow();
                    return;
                }
            }
            else
            {
                return;
            }

            float speed = moveTweenType.Speed;
            float acceleratedSpeed = moveTweenType.AcceleratedSpeed;
            float parabolaHeight = moveTweenType.ParabolaHeight;

            float resScale = ET.Ability.UnitHelper.GetGameResScale(self.DomainScene());
            speed *= resScale;
            acceleratedSpeed *= resScale;
            parabolaHeight *= resScale;

            float3 disTmp = targetPosition - self.startPosition;
            float disY = disTmp.y;
            disTmp.y = 0;
            float disX = math.length(disTmp);

            float totalTime = 0;
            if (acceleratedSpeed == 0)
            {
                totalTime = disX / speed;
            }
            else
            {
                float a1 = 0.5f * acceleratedSpeed;
                float b1 = speed;
                float c1 = -disX;
                totalTime = (-b1 + math.sqrt(b1 * b1 - 4 * a1 * c1)) / (2 * a1);
            }

            if (totalTime < 0.5f)
            {
                totalTime = 0.5f;
                if (acceleratedSpeed == 0)
                {
                    speed = disX / totalTime;
                }
                else
                {
                    speed = (disX - 0.5f * acceleratedSpeed * totalTime * totalTime) / totalTime;
                }
            }

            Unit unit = self.GetUnit();
            if (totalTime <= self.timeElapsed)
            {
                self.SetUnitForward(self.forward);
                self.SetUnitPos(targetPosition);

                if (self.IsNeedDealHit() && UnitHelper.ChkIsBullet(unit))
                {
                    BulletObj bulletObj = unit.GetComponent<BulletObj>();
                    if (targetUnit != null)
                    {
#if UNITY_EDITOR
                        if (self.DomainScene().SceneType == SceneType.Map)
                        {
                            if (UnitHelper.ChkUnitAlive(targetUnit))
                            {
                                if (ET.GamePlayHelper.ChkIsFriend(targetUnit, unit))
                                {
                                    Log.Error($"===== DoMoveTween_Target ChkIsFriend");
                                }
                            }
                        }
#endif
                        bulletObj.AddPreHitUnit(targetUnit.Id);
                    }
                    else
                    {
                        bulletObj.SetPreHitPos();
                    }
                }
            }
            else
            {
                float dis2Top;
                if (disY < 0)
                {
                    dis2Top = parabolaHeight;
                }
                else
                {
                    dis2Top = math.abs(disY) + parabolaHeight;
                }

                float a = totalTime / (4 * dis2Top);
                float b = -1;
                float c = disY / totalTime;
                float speedYStart = (-b + math.sqrt(b * b - 4 * a * c)) / (2 * a);
                float gravity = 0.5f * speedYStart * speedYStart / dis2Top;

                float curSpeedY = speedYStart - gravity * self.timeElapsed;
                float curSpeedX = speed + acceleratedSpeed * self.timeElapsed;

                float3 curSpeed = math.normalize(disTmp) * curSpeedX + new float3(0, curSpeedY, 0);
                self.forward = math.normalize(curSpeed);
                self.speed = math.length(curSpeed);

                self.SetUnitForward(self.forward);
                //unit.Position += self.forward * self.speed * fixedDeltaTime;
                float3 newPos = self.startPosition +
                    (math.normalize(disTmp) * speed * self.timeElapsed + 0.5f * acceleratedSpeed * self.timeElapsed * self.timeElapsed) +
                    new float3(0, speedYStart * self.timeElapsed - 0.5f * gravity * self.timeElapsed * self.timeElapsed, 0);
                self.SetUnitPos(newPos);
            }
        }
    }
}