using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class EventHandlerHelper
    {
        public static void Run_Buff(Unit unit, AbilityConfig.BuffTriggerEvent abilityBuffMonitorTriggerEvent, Unit onAttackUnit, Unit beHurtUnit)
        {
            BuffHelper.EventHandler(unit, abilityBuffMonitorTriggerEvent, onAttackUnit, beHurtUnit);
        }

        public static void Run_Bullet(Unit unit, AbilityConfig.BulletTriggerEvent abilityBulletMonitorTriggerEvent, Unit onAttackUnit, Unit beHurtUnit)
        {
            BulletHelper.EventHandler(unit, abilityBulletMonitorTriggerEvent, onAttackUnit, beHurtUnit);
        }

        public static void Run_Aoe(Unit unit, AbilityConfig.AoeTriggerEvent abilityAoeMonitorTriggerEvent)
        {
            AoeHelper.EventHandler(unit, abilityAoeMonitorTriggerEvent);
        }

        public static void Run_Game(Scene scene, ET.AbilityConfig.GlobalBuffTriggerEvent
            abilityGameMonitorTriggerEvent, ref ActionGameContext actionGameContext)
        {
            GlobalBuffHelper.EventHandler(scene, abilityGameMonitorTriggerEvent, ref actionGameContext);
        }
    }
}