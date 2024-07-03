using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
    [ChildOf(typeof(SequenceGlobalConditionComponent))]
	public class GlobalConditionObj: Entity, IAwake, IDestroy, IFixedUpdate
    {
        public ChkTriggerBase globalCondition { get; set; }

        public int curCount;
        public bool status;
    }
}