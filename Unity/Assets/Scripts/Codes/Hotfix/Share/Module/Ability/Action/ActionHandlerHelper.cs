using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class ActionHandlerHelper
    {
        public static void CreateAction(Unit unit, string actionId, float delayTime, SelectHandle selectHandle, ActionContext actionContext)
        {
            unit.DomainScene().GetComponent<ActionHandlerComponent>().Run(unit, actionId, delayTime, selectHandle, actionContext);
        }
    }
}