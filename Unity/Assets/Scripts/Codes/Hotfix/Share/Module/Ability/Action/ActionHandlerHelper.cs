using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class ActionHandlerHelper
    {
        public static void CreateAction(Unit unit, Unit resetPosByUnit, string actionId, float delayTime, SelectHandle selectHandle, ref ActionContext actionContext)
        {
            if (unit == null)
            {
#if UNITY_EDITOR
                Log.Error($"CreateAction unit == null");
#endif
                return;
            }
            unit.DomainScene().GetComponent<ActionHandlerComponent>().Run(unit, resetPosByUnit, actionId, delayTime, selectHandle, actionContext).Coroutine();
        }
    }
}