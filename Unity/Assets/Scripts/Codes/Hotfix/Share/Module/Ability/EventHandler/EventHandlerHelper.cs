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
        
        public static void Run(Unit unit, AbilityBulletMonitorTriggerEvent abilityBulletMonitorTriggerEvent, Unit onHitUnit, Unit beHurtUnit)
        {
            BulletHelper.EventHandler(unit, abilityBulletMonitorTriggerEvent, onHitUnit, beHurtUnit);
        }
        
        public static void Run(Unit unit, AbilityAoeMonitorTriggerEvent abilityAoeMonitorTriggerEvent)
        {
            AoeHelper.EventHandler(unit, abilityAoeMonitorTriggerEvent);
        }
    }
}