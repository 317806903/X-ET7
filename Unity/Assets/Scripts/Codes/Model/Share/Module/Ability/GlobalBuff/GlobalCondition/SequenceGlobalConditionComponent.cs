using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
    [ChildOf(typeof(ParallelGlobalConditionComponent))]
	public class SequenceGlobalConditionComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
        public bool status;
    }
}