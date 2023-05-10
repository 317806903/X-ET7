using System.Collections.Generic;

namespace ET.Ability
{
    public class ActionHandlerAttribute: BaseAttribute
    {
    }
    
    [ActionHandler]
    public abstract class IActionHandler
    {
        public abstract void Run(Unit unit, string actionId, Dictionary<string, object> param);
    }
}