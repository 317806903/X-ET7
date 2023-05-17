using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class BuffHelper
    {
        public static void AddBuff(Unit unit, int buffCfgId)
        {
            BuffComponent buffComponent = unit.GetComponent<BuffComponent>();
            if (buffComponent == null)
            {
                buffComponent = unit.AddComponent<BuffComponent>();
            }
            buffComponent.AddBuff(buffCfgId);
        }
        
        public static void EventHandler(Unit unit, AbilityBuffMonitorTriggerEvent abilityBuffMonitorTriggerEvent)
        {
            unit.GetComponent<BuffComponent>()?.EventHandler(abilityBuffMonitorTriggerEvent);
        }
    }
}