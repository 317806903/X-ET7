using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
    [ComponentOf(typeof(Unit))]
	public class BuffComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
        public List<BuffObj> removeList;
        public MultiMap<AbilityBuffMonitorTriggerEvent, BuffObj> monitorTriggerList;
    }
}