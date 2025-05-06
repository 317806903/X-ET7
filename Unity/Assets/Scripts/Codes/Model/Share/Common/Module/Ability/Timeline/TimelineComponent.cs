using System.Collections.Generic;

namespace ET.Ability
{
	[ComponentOf(typeof(Unit))]
	public class TimelineComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
        public List<long> removeList;
        public bool isForeaching;
    }
}