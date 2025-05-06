using System.Collections.Generic;
using ET.Ability;

namespace ET
{
	[ComponentOf(typeof(Scene))]
	public class UnitDelayRemoveComponent: Entity, IAwake, IDestroy, IFixedUpdate
	{
		public int waitFrameRemove = 150;
		public int curFrameRemove = 0;
		public int actorNextRemoveTime = 60;
		public int towerNextRemoveTime = 120;

		public Queue<long> unitList;
		public Queue<long> unitRemoveTimeList;
	}
}