using System.Collections.Generic;

namespace ET.Ability
{
    [ComponentOf(typeof(Unit))]
	public class BuffComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
        public List<BuffObj> removeList;
        public MultiMapSet<AbilityBuffMonitorTriggerEvent, BuffObj> monitorTriggerList;
    }
}