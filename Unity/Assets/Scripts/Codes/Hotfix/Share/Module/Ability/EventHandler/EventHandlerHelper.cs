using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class EventHandlerHelper
    {
        public static void Run(Unit unit, AbilityBuffMonitorTriggerEvent abilityBuffMonitorTriggerEvent)
        {
            BuffHelper.EventHandler(unit, abilityBuffMonitorTriggerEvent);
        }
        
        public static void Run(Unit unit, AbilityBulletMonitorTriggerEvent abilityBulletMonitorTriggerEvent)
        {
            BulletHelper.EventHandler(unit, abilityBulletMonitorTriggerEvent);
        }
        
        public static void Run(Unit unit, AbilityAoeMonitorTriggerEvent abilityAoeMonitorTriggerEvent)
        {
            unit.GetComponent<EventHandlerComponent>().RunAoe(abilityAoeMonitorTriggerEvent);
        }
    }
}