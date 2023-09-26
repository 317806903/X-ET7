using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class RotateHelper
    {
        /// <summary>
        /// 指定unit的旋转
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="actionCfg_FaceTo"></param>
        /// <param name="selectHandle"></param>
        public static void DealRotate(Unit unit, ActionCfg_FaceTo actionCfg_FaceTo, SelectHandle selectHandle)
        {
            float incrementRotate = 0;
            if (selectHandle.selectHandleType == SelectHandleType.SelectUnits)
            {
                // int count = selectHandle.unitIds.Count;
                // foreach (var unitId in selectHandle.unitIds)
                // {
                //     Unit unitSelect = UnitHelper.GetUnit(unit.DomainScene(), unitId);
                //     incrementRotate += ET.Ability.UnitHelper.GetTargetUnitAngle(unit, unitSelect);
                // }
                // incrementRotate /= count;
                foreach (var unitId in selectHandle.unitIds)
                {
                    Unit unitSelect = UnitHelper.GetUnit(unit.DomainScene(), unitId);
                    if (unitSelect == null)
                    {
                        continue;
                    }
                    incrementRotate = ET.Ability.UnitHelper.GetTargetUnitRadian(unit, unitSelect);
                    break;
                }
            }
            else if (selectHandle.selectHandleType == SelectHandleType.SelectPosition)
            {
                incrementRotate = ET.Ability.UnitHelper.GetTargetPosRadian(unit, selectHandle.position);
            }
            else if (selectHandle.selectHandleType == SelectHandleType.SelectDirection)
            {
                incrementRotate = ET.Ability.UnitHelper.GetTargetDirRadian(unit, selectHandle.direction);
            }

            // if (math.abs(incrementRotate) <= 0.02f)
            // {
            //     return;
            // }

            FaceToType forceSetType = actionCfg_FaceTo.FaceToType;
            _DealRotateAngle(unit, incrementRotate, forceSetType);
        }
        
        /// <summary>
        /// 指定unit的面向
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="forwardDirection"></param>
        /// <param name="forceSetType"></param>
        public static void DealRotate(Unit unit, float3 forwardDirection, FaceToType forceSetType)
        {
            if (forceSetType == FaceToType.Forward_ForceSetFaceImmediately)
            {
                ForceSetForward(unit, forwardDirection);
                return;
            }
            float incrementRotate = ET.Ability.UnitHelper.GetTargetDirRadian(unit, forwardDirection);
            // if (math.abs(incrementRotate) <= 0.02f)
            // {
            //     return;
            // }

            _DealRotateAngle(unit, incrementRotate, forceSetType);
        }
        
        /// <summary>
        /// 指定unit的面向
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="angle"></param>
        /// <param name="forceSetType"></param>
        public static void _DealRotateAngle(Unit unit, float angle, FaceToType forceSetType)
        {
            float incrementRotate = angle;
            // if (math.abs(incrementRotate) <= 0.02f)
            // {
            //     return;
            // }

            if (forceSetType == FaceToType.Forward_ForceSetFaceImmediately)
            {
                ForceSetRotateImmediately(unit, incrementRotate);
            }
            else if (forceSetType == FaceToType.Forward_ForceSetFace)
            {
                ForceSetRotate(unit, incrementRotate);
            }
            else if (forceSetType == FaceToType.Forward_AddRotate)
            {
                AddRotate(unit, incrementRotate);
            }
            else if (forceSetType == FaceToType.Back_ForceSetFaceImmediately)
            {
                ForceSetRotateImmediately(unit, incrementRotate + math.PI);
            }
            else if (forceSetType == FaceToType.Back_ForceSetFace)
            {
                ForceSetRotate(unit, incrementRotate + math.PI);
            }
            else if (forceSetType == FaceToType.Back_AddRotate)
            {
                AddRotate(unit, incrementRotate + math.PI);
            }
            else if (forceSetType == FaceToType.Around_ForceSetFaceImmediately)
            {
                ForceSetRotateImmediately(unit, incrementRotate + math.PI * 0.5f);
            }
            else if (forceSetType == FaceToType.Around_ForceSetFace)
            {
                ForceSetRotate(unit, incrementRotate + math.PI * 0.5f);
            }
            else if (forceSetType == FaceToType.Around_AddRotate)
            {
                AddRotate(unit, incrementRotate + math.PI * 0.5f);
            }
            else if (forceSetType == FaceToType.Random_ForceSetFaceImmediately)
            {
                int random = RandomGenerator.RandomNumber(0, 360);
                incrementRotate = random * math.PI / 180;
                ForceSetRotateImmediately(unit, incrementRotate);
            }
            else if (forceSetType == FaceToType.Random_ForceSetFace)
            {
                int random = RandomGenerator.RandomNumber(0, 360);
                incrementRotate = random * math.PI / 180;
                ForceSetRotate(unit, incrementRotate);
            }
            else if (forceSetType == FaceToType.Random_AddRotate)
            {
                int random = RandomGenerator.RandomNumber(0, 360);
                incrementRotate = random * math.PI / 180;
                AddRotate(unit, incrementRotate);
            }
        }
        
        public static RotateComponent GetRotateComponent(Unit unit)
        {
            RotateComponent rotateComponent = unit.GetComponent<RotateComponent>();
            if (rotateComponent == null)
            {
                rotateComponent = unit.AddComponent<RotateComponent>();
            }

            return rotateComponent;
        }
        
        public static void AddRotate(Unit unit, float incrementRotate)
        {
            GetRotateComponent(unit).AddRotate(incrementRotate);
        }
        
        public static void ForceSetRotate(Unit unit, float forceRotateAngle)
        {
            GetRotateComponent(unit).ForceSetRotate(forceRotateAngle);
        }
        
        public static void ForceSetRotateImmediately(Unit unit, float forceRotateAngle)
        {
            GetRotateComponent(unit).ForceSetRotateImmediately(forceRotateAngle);
        }
        
        public static void ForceSetForward(Unit unit, float3 forwardDirection)
        {
            GetRotateComponent(unit).ForceSetForward(forwardDirection);
        }
    }
}