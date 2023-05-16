using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class ActionHandlerHelper
    {
        public static void CreateAction(Unit unit, string actionId, SelectHandle selectHandle)
        {
            unit.DomainScene().GetComponent<ActionHandlerComponent>().Run(unit, actionId, selectHandle);
        }
    }
}