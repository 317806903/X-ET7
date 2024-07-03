using System.Collections.Generic;

namespace ET.Ability
{
    public class ActionGameHandlerAttribute: BaseAttribute
    {
    }

    [ActionGameHandler]
    public abstract class IActionGameHandler
    {
        public abstract ETTask Run(Scene scene, string actionId, float delayTime, ActionGameContext actionGameContext);
    }
}