using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class RotateHelper
    {
        public static void AddRotate(Unit unit, SelectHandle selectHandle, bool forceSet)
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
                    incrementRotate = ET.Ability.UnitHelper.GetTargetUnitAngle(unit, unitSelect);
                    break;
                }
            }
            else if (selectHandle.selectHandleType == SelectHandleType.SelectPosition)
            {
                incrementRotate = ET.Ability.UnitHelper.GetTargetPosAngle(unit, selectHandle.position);
            }
            else if (selectHandle.selectHandleType == SelectHandleType.SelectDirection)
            {
                incrementRotate = ET.Ability.UnitHelper.GetTargetDirAngle(unit, selectHandle.direction);
            }

            if (math.abs(incrementRotate) <= 0.05f)
            {
                return;
            }
            if (forceSet)
            {
                ForceSetRotate(unit, incrementRotate);
            }
            else
            {
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
            Log.Debug($"AddRotate {incrementRotate}");
            GetRotateComponent(unit).AddRotate(incrementRotate);
        }
        
        public static void ForceSetRotate(Unit unit, float rotateAngle)
        {
            GetRotateComponent(unit).ForceSetRotate(rotateAngle);
        }
        
        public static void ForceSetRotate(Unit unit, float3 rotateDirection)
        {
            GetRotateComponent(unit).ForceSetRotate(rotateDirection);
        }
    }
}