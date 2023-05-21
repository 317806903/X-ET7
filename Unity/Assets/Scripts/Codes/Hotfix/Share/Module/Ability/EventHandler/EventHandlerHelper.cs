using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class EventHandlerHelper
    {
        public static void Run(Unit unit, AbilityBuffMonitorTriggerEvent abilityBuffMonitorTriggerEvent, Unit onHitUnit, Unit beHurtUnit)
        {
            BuffHelper.EventHandler(unit, abilityBuffMonitorTriggerEvent, onHitUnit, beHurtUnit);
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