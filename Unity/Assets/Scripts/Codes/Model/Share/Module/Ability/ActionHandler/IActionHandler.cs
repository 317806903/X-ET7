using System.Collections.Generic;

namespace ET.Ability
{
    public class ActionHandlerAttribute: BaseAttribute
    {
    }
    
    [ActionHandler]
    public abstract class IActionHandler
    {
        public abstract ETTask Run(Unit unit, string actionId, Dictionary<string, object> param);
    }
}