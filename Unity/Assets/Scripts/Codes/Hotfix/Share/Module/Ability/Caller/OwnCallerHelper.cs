using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class OwnCallerHelper
    {
        public static OwnCallerComponent GetOwnCallerComponent(Unit unit)
        {
            OwnCallerComponent OwnCallerComponent = unit.GetComponent<OwnCallerComponent>();
            if (OwnCallerComponent == null)
            {
                OwnCallerComponent = unit.AddComponent<OwnCallerComponent>();
            }
            return OwnCallerComponent;
        }

        public static void AddOwnCaller(Unit unit, Unit caller)
        {
            OwnCallerComponent OwnCallerComponent = GetOwnCallerComponent(unit);
            OwnCallerComponent.AddOwnCaller(caller);
        }

        public static HashSet<long> GetOwnCaller(Unit unit, bool ownCallActor, bool ownBullet, bool ownAoe)
        {
            OwnCallerComponent OwnCallerComponent = GetOwnCallerComponent(unit);
            HashSet<long> unitList = OwnCallerComponent.GetOwnCaller(ownCallActor, ownBullet, ownAoe);
            return unitList;
        }

    }
}