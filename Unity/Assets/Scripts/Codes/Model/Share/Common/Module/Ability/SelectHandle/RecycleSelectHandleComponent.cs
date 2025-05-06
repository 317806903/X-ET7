using System.Collections.Generic;
using ET.Ability;

namespace ET.Ability
{

	[ComponentOf(typeof(Scene))]
	public class RecycleSelectHandleComponent: Entity, IAwake, IDestroy, IFixedUpdate
	{
		public int waitFrameRecycleSelectHandle = 60;
		public int curFrameRecycleSelectHandle = 0;

		public int nextRecycleTime = 5;

		public MultiMapSimple<long, SelectHandle> time2SelectHandle;
		public Queue<long> timeList;
	}
}