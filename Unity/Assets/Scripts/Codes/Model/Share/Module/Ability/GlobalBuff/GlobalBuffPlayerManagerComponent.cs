using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
    [ComponentOf(typeof(GlobalBuffManagerComponent))]
	public class GlobalBuffPlayerManagerComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
        public List<long> removeList;

    }
}