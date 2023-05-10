using System.Collections.Generic;

namespace ET.Ability
{
	[ComponentOf(typeof(Scene))]
	public class TimelineComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
        [StaticField]
        public static TimelineComponent Instance;

        public List<long> removeList;
    }
}