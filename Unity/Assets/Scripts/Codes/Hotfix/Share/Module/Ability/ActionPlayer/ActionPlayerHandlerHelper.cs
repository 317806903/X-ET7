using System.Collections.Generic;

namespace ET.Ability
{
    public static class ActionPlayerHandlerHelper
    {
        public static void CreateAction(Scene scene, long playerId, string actionId, ref ActionPlayerContext actionPlayerContext)
        {
            scene.GetComponent<ActionPlayerHandlerComponent>().Run(playerId, actionId, actionPlayerContext).Coroutine();
        }
    }
}