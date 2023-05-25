using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class EventHandlerHelper
    {
        public static void Run(Unit unit, AbilityBuffMonitorTriggerEvent abilityBuffMonitorTriggerEvent, Unit onAttackUnit, Unit beHurtUnit)
        {
            BuffHelper.EventHandler(unit, abilityBuffMonitorTriggerEvent, onAttackUnit, beHurtUnit);
        }
        
        public static void Run(Unit unit, AbilityBulletMonitorTriggerEvent abilityBulletMonitorTriggerEvent, Unit onAttackUnit, Unit beHurtUnit)
        {
            BulletHelper.EventHandler(unit, abilityBulletMonitorTriggerEvent, onAttackUnit, beHurtUnit);
        }
        
        public static void Run(Unit unit, AbilityAoeMonitorTriggerEvent abilityAoeMonitorTriggerEvent)
        {
            AoeHelper.EventHandler(unit, abilityAoeMonitorTriggerEvent);
        }
    }
}