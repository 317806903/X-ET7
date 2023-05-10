using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class EffectHelper
    {
        public static void AddEffect(Unit unit, string effectCfgId)
        {
            unit.GetComponent<EffectComponent>().AddEffect(effectCfgId);
        }
    }
}