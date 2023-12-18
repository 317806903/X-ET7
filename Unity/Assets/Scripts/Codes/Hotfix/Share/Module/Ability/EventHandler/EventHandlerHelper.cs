using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class EventHandlerHelper
    {
        public static void Run_Buff(Unit unit, AbilityBuffMonitorTriggerEvent abilityBuffMonitorTriggerEvent, Unit onAttackUnit, Unit beHurtUnit)
        {
            BuffHelper.EventHandler(unit, abilityBuffMonitorTriggerEvent, onAttackUnit, beHurtUnit);
        }

        public static void Run_Bullet(Unit unit, AbilityBulletMonitorTriggerEvent abilityBulletMonitorTriggerEvent, Unit onAttackUnit, Unit beHurtUnit)
        {
            BulletHelper.EventHandler(unit, abilityBulletMonitorTriggerEvent, onAttackUnit, beHurtUnit);
        }

        public static void Run_Aoe(Unit unit, AbilityAoeMonitorTriggerEvent abilityAoeMonitorTriggerEvent)
        {
            AoeHelper.EventHandler(unit, abilityAoeMonitorTriggerEvent);
        }

        public static void Run_Game(Scene scene, AbilityGameMonitorTriggerEvent abilityGameMonitorTriggerEvent, Unit onAttackUnit, Unit beHurtUnit)
        {
            GlobalBuffHelper.EventHandler(scene, abilityGameMonitorTriggerEvent, onAttackUnit, beHurtUnit);
        }
    }
}