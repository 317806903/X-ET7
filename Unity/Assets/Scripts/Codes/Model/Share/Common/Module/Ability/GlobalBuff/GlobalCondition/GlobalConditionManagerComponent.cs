using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
    [ComponentOf()]
	public class GlobalConditionManagerComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
        public bool status;
        public HashSet<ET.AbilityConfig.GlobalBuffTriggerEvent> monitorTriggerList;
    }
}