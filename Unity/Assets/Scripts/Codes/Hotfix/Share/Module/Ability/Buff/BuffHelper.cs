using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class BuffHelper
    {
        public static void AddBuff(Unit unit, int buffCfgId)
        {
            unit.GetComponent<BuffComponent>().AddBuff(buffCfgId);
        }
        
        public static void EventHandler(Unit unit, AbilityBuffMonitorTriggerEvent abilityBuffMonitorTriggerEvent)
        {
            unit.GetComponent<BuffComponent>().EventHandler(abilityBuffMonitorTriggerEvent);
        }
    }
}