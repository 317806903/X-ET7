using System.Collections.Generic;

namespace ET.Ability
{
    public class ActionPlayerHandlerAttribute: BaseAttribute
    {
    }

    [ActionPlayerHandler]
    public abstract class IActionPlayerHandler
    {
        public abstract ETTask Run(Scene scene, long playerId, string actionId, ActionPlayerContext actionPlayerContext);
    }
}