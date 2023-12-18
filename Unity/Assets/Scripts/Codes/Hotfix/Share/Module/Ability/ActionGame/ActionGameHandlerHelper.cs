using System.Collections.Generic;

namespace ET.Ability
{
    public static class ActionGameHandlerHelper
    {
        public static void CreateAction(Scene scene, string actionId, ref ActionGameContext actionGameContext)
        {
            scene.GetComponent<ActionGameHandlerComponent>().Run(actionId, actionGameContext).Coroutine();
        }
    }
}