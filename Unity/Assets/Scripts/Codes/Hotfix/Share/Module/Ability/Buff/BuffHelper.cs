using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class BuffHelper
    {
        public static void AddBuff(Unit unit, string buffCfgId)
        {
            BuffComponent buffComponent = unit.GetComponent<BuffComponent>();
            if (buffComponent == null)
            {
                buffComponent = unit.AddComponent<BuffComponent>();
            }
            buffComponent.AddBuff(buffCfgId);
        }
        
        public static void EventHandler(Unit unit, AbilityBuffMonitorTriggerEvent abilityBuffMonitorTriggerEvent, Unit onHitUnit, Unit beHurtUnit)
        {
            unit.GetComponent<BuffComponent>()?.EventHandler(abilityBuffMonitorTriggerEvent, onHitUnit, beHurtUnit);
        }
    }
}