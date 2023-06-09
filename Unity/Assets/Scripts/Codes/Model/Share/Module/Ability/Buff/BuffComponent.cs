using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
    [ComponentOf(typeof(Unit))]
	public class BuffComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
        public List<BuffObj> removeList;
        public MultiMap<AbilityBuffMonitorTriggerEvent, BuffObj> monitorTriggerList;
        public MultiMap<BuffTagType, BuffObj> buffTagTypeList;
        public MultiMap<BuffTagType, BuffObj> buffImmuneTagTypeList;
        public MultiMap<BuffTagGroupType, BuffObj> buffTagGroupTypeList;
        public MultiMap<BuffTagGroupType, BuffObj> buffImmuneTagGroupTypeList;
        public MultiMap<BuffType, BuffObj> buffTypeList;
        public MultiMap<ControlState, BuffObj> buffControlStateList;
    }
}