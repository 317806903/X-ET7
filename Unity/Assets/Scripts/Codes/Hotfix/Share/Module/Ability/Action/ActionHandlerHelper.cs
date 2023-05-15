using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class ActionHandlerHelper
    {
        public static void CreateAction(Unit unit, string actionId, Dictionary<string, object> param)
        {
            unit.DomainScene().GetComponent<ActionHandlerComponent>().Run(unit, actionId, param);
        }
    }
}