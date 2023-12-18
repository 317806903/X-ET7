using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (BuffObj))]
    public static class BuffMotionSystem
    {
        public static void AddBuffMotionList(this BuffComponent self, BuffObj buffObj)
        {
            bool needRecordMotion = false;
            foreach (BuffAction buffAction in buffObj.buffActions)
            {
                if (buffAction is BuffActionModifyMotion buffActionModifyMotion)
                {
                    needRecordMotion = true;
                    break;
                }
            }

            if (needRecordMotion)
            {
                self.buffMotionList.Add(buffObj);
            }
        }

        public static void RemoveBuffMotionList(this BuffComponent self, BuffObj buffObj)
        {
            self.buffMotionList.Remove(buffObj);
        }

        public static float3 GetMotionSpeedVector(this BuffComponent self)
        {
            float3 speedVector = float3.zero;
            float maxSpeedVectorValueSq = 0;
            foreach (BuffObj buffObj in self.buffMotionList)
            {
                bool bCanBeMotion = self._ChkCanBeMotion(ref buffObj.actionContext);
                if (bCanBeMotion)
                {
                    float3 curMotionSpeedVector = buffObj.GetMotionSpeedVector();
                    if (math.lengthsq(curMotionSpeedVector) > maxSpeedVectorValueSq)
                    {
                        maxSpeedVectorValueSq = math.lengthsq(curMotionSpeedVector);
                    }
                    speedVector += buffObj.GetMotionSpeedVector();
                }
            }

            if (maxSpeedVectorValueSq > 0)
            {
                float3 dir = math.normalize(speedVector);
                speedVector = dir * math.sqrt(maxSpeedVectorValueSq);
            }
            return speedVector;
        }

        public static float3 GetMotionSpeedVector(this BuffObj self)
        {
            if (self.isEnabled == false)
            {
                return float3.zero;
            }

            float3 speedVector = float3.zero;
            foreach (BuffAction buffAction in self.buffActions)
            {
                if (buffAction is BuffActionModifyMotion buffActionModifyMotion)
                {
                    speedVector += self._GetMotionChg(buffActionModifyMotion, self.stack);
                }
            }

            return speedVector;
        }

        public static float3 _GetMotionChg(this BuffObj self, BuffActionModifyMotion buffActionModifyMotion, int stackCount)
        {
            float speed = buffActionModifyMotion.Speed;
            float acceleratedSpeed = buffActionModifyMotion.AcceleratedSpeed;
            float curSpeed = speed + acceleratedSpeed * self.timeElapsed;
            float3 curVectory = float3.zero;
            if (buffActionModifyMotion is BuffActionModifyMotionHorizontal buffActionModifyMotionHorizontal)
            {
                Unit unit = self.GetUnit();
                if (self.actionContext.motionUnitId == unit.Id)
                {

                }
                if (buffActionModifyMotionHorizontal.MotionTargetType == MotionTargetType.MotionUnitId)
                {
                    Unit motionUnit = UnitHelper.GetUnit(self.DomainScene(), self.actionContext.motionUnitId);
                    if (motionUnit == null)
                    {
                        curVectory = float3.zero;
                    }
                    else
                    {
                        curVectory = motionUnit.Position - unit.Position;
                    }
                }
                else if (buffActionModifyMotionHorizontal.MotionTargetType == MotionTargetType.MotionPosition)
                {
                    curVectory = self.actionContext.motionPosition - unit.Position;
                }
                else if (buffActionModifyMotionHorizontal.MotionTargetType == MotionTargetType.MotionDirection)
                {
                    curVectory = self.actionContext.motionDirection;
                }
            }
            else if (buffActionModifyMotion is BuffActionModifyMotionVertical)
            {
                curVectory = new float3(0, 1, 0);
            }
            else
            {
                Log.Error($"---ET.Ability.BuffMotionSystem._GetMotionChg Error");
            }

            if (buffActionModifyMotion is BuffActionModifyMotionHorizontal_Forward)
            {
                curVectory = math.normalize(curVectory);
            }
            else if (buffActionModifyMotion is BuffActionModifyMotionHorizontal_Back)
            {
                curVectory = -math.normalize(curVectory);
            }
            else if (buffActionModifyMotion is BuffActionModifyMotionVertical)
            {
                curVectory = math.normalize(curVectory);
            }
            else
            {
                curVectory = math.normalize(curVectory);
            }

            return curVectory * curSpeed;
        }

        public static void AddBuffWhenModifyMotion(this BuffObj self, BuffActionModifyMotion buffActionModifyMotion)
        {
        }

        public static void ChgBuffStackCountWhenModifyMotion(this BuffObj self, BuffActionModifyMotion buffActionModifyMotion, int oldStackCount, int newStackCount)
        {
            if (oldStackCount == newStackCount)
            {
                return;
            }
        }

        public static void RemoveBuffWhenModifyMotion(this BuffObj self, BuffActionModifyMotion buffActionModifyMotion)
        {
        }

    }
}