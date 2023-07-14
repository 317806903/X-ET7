using System.Collections.Generic;

namespace ET.Ability
{
    public class ActionHandlerAttribute: BaseAttribute
    {
    }
    
    [ActionHandler]
    public abstract class IActionHandler
    {
        public abstract ETTask Run(Unit unit, Unit resetPosByUnit, string actionId, float delayTime, SelectHandle selectHandle, ActionContext actionContext);
    }
}