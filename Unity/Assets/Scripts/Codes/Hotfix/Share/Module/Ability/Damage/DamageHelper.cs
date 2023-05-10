using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof (Unit))]
    public static class DamageHelper
    {
        public static DamageInfo CreateDamageInfo(Unit unit, long targetUnitId, Damage damage, float damageDegree, float criticalRate,
        DamageInfoTag[] tags)
        {
            Scene scene = unit.DomainScene();
            return scene.GetComponent<DamageComponent>().Run(targetUnitId, damage, damageDegree, criticalRate, tags);
        }
    }
}