using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class UnitHelper
    {
        public static UnitComponent GetUnitComponent(Unit unit)
        {
            return unit.DomainScene().GetComponent<UnitComponent>();
        }
        
        public static UnitComponent GetUnitComponent(Scene scene)
        {
            return scene.GetComponent<UnitComponent>();
        }
        
        public static Unit GetUnit(Scene scene, long unitId)
        {
            Unit unit = GetUnitComponent(scene).Get(unitId);
            return unit;
        }
        
        public static bool ChkUnitAlive(Scene scene, long unitId)
        {
            Unit unit = GetUnit(scene, unitId);
            if (unit == null)
            {
                return false;
            }
            return true;
        }
        
        public static bool ChkIsBullet(Scene scene, long unitId)
        {
            Unit unit = GetUnit(scene, unitId);
            if (unit == null)
            {
                return false;
            }

            if (unit.Type == UnitType.Bullet)
            {
                return true;
            }
            return false;
        }
        
        public static bool ChkIsAoe(Scene scene, long unitId)
        {
            Unit unit = GetUnit(scene, unitId);
            if (unit == null)
            {
                return false;
            }

            if (unit.Type == UnitType.Aoe)
            {
                return true;
            }
            return false;
        }
    }
}