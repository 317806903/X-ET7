using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class CasterHelper
    {
        public static void AddCaster(Unit unit, Unit caster)
        {
            CasterComponent casterComponent = unit.GetComponent<CasterComponent>();
            if (casterComponent == null)
            {
                casterComponent = unit.AddComponent<CasterComponent>();
            }
            casterComponent.AddCaster(caster.Id);
        }

        public static Unit GetCaster(Unit unit)
        {
            CasterComponent casterComponent = unit.GetComponent<CasterComponent>();
            if (casterComponent == null)
            {
                return null;
            }

            return casterComponent.GetCaster();
        }

    }
}