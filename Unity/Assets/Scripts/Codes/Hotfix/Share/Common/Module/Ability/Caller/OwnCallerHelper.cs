using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class OwnCallerHelper
    {
        public static void AddOwnCaller(Unit unit, Unit caller)
        {
            OwnCallerComponent ownCallerComponent = unit.GetComponent<OwnCallerComponent>();
            if (ownCallerComponent == null)
            {
                ownCallerComponent = unit.AddComponent<OwnCallerComponent>();
            }
            ownCallerComponent.AddOwnCaller(caller);
        }

        public static HashSet<long> GetOwnCaller(Unit unit, bool ownCallActor, bool ownBullet, bool ownAoe, bool ownCallSkillCaster)
        {
            OwnCallerComponent ownCallerComponent = unit.GetComponent<OwnCallerComponent>();
            if (ownCallerComponent == null)
            {
                return null;
            }
            HashSet<long> unitList = ownCallerComponent.GetOwnCaller(ownCallActor, ownBullet, ownAoe, ownCallSkillCaster);
            return unitList;
        }

        public static int GetOwnCallerCount(Unit unit, bool ownCallActor, bool ownBullet, bool ownAoe, bool ownCallSkillCaster)
        {
            OwnCallerComponent ownCallerComponent = unit.GetComponent<OwnCallerComponent>();
            if (ownCallerComponent == null)
            {
                return 0;
            }
            return ownCallerComponent.GetOwnCallerCount(ownCallActor, ownBullet, ownAoe, ownCallSkillCaster);
        }

    }
}