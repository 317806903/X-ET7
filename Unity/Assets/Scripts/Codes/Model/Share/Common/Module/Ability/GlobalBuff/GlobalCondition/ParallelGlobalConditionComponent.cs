using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
    [ChildOf(typeof(GlobalConditionManagerComponent))]
	public class ParallelGlobalConditionComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
        public bool status;
    }
}