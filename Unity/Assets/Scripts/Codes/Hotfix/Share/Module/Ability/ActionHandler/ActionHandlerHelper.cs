using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class ActionHandlerHelper
    {
        public static void CreateAction(string actionId, long fromUnitId, long toUnitId)
        {
            ActionHandlerComponent.Instance.Run(actionId, fromUnitId, toUnitId);
        }
    }
}