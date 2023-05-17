using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class AoeHelper
    {
        public static void AddAoe(Unit unit, int aoeCfgId)
        {
        }
        
        public static void EventHandler(Unit unit, AbilityAoeMonitorTriggerEvent abilityAoeMonitorTriggerEvent)
        {
            unit.GetComponent<AoeObj>()?.EventHandler(abilityAoeMonitorTriggerEvent);
        }
    }
}