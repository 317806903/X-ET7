using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof (Unit))]
    public static class DamageHelper
    {
        public static DamageInfo CreateDamageInfo(long targetUnitId, Damage damage, float damageDegree, float criticalRate,
        DamageInfoTag[] tags)
        {
            return DamageComponent.Instance.Run(targetUnitId, damage, damageDegree, criticalRate, tags);
        }
    }
}