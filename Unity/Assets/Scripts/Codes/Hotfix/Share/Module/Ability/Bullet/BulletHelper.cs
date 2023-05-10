using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class BulletHelper
    {
        public static void AddBullet(Unit unit, int bulletCfgId)
        {
        }
        
        public static void EventHandler(Unit unit, AbilityBulletMonitorTriggerEvent abilityBulletMonitorTriggerEvent)
        {
            unit.GetComponent<BulletObj>().EventHandler(abilityBulletMonitorTriggerEvent);
        }
    }
}