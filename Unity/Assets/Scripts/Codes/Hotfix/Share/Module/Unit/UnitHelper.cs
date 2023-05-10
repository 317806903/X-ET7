using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class UnitHelper
    {
        public static Unit GetUnit(long unitId)
        {
            Unit unit = UnitComponent.Instance.GetChild<Unit>(unitId);
            return unit;
        }
        
        public static bool ChkUnitAlive(long unitId)
        {
            Unit unit = GetUnit(unitId);
            if (unit == null)
            {
                return false;
            }
            return true;
        }
        
        public static bool ChkIsBullet(long unitId)
        {
            Unit unit = GetUnit(unitId);
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
        
        public static bool ChkIsAoe(long unitId)
        {
            Unit unit = GetUnit(unitId);
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