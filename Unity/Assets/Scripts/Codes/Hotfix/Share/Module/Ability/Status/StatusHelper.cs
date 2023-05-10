using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class StatusHelper
    {
        public static void AddStatus(Unit unit, int statusCfgId)
        {
            unit.GetComponent<StatusComponent>().AddStatus(statusCfgId);
        }
        
    }
}