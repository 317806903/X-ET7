using System.Collections.Generic;
using ET.Ability;

namespace ET
{

	[ComponentOf(typeof(Scene))]
	public class UnitComponent: Entity, IAwake, IDestroy, IFixedUpdate
	{
		public int waitFrameSyncPos = 1;
		public int curFrameSyncPos = 0;

		public int waitFrameSyncNumeric = 4;
		public int curFrameSyncNumeric = 0;

		public int waitFrameRecycleSelectHandle = 4;
		public int curFrameRecycleSelectHandle = 0;

		public HashSetComponent<SelectHandle> waitRecycleSelectHandles;

		public HashSetComponent<Unit> NeedSyncNumericUnits;
		public HashSetComponent<Unit> NeedSyncPosUnits;
		public List<long> waitRemoveList;
		public HashSetComponent<Unit> observerList;
		public HashSetComponent<Unit> playerList;
		public HashSetComponent<Unit> actorList;
		public HashSetComponent<Unit> npcList;
		public HashSetComponent<Unit> sceneObjList;
		public HashSetComponent<Unit> bulletList;
		public HashSetComponent<Unit> aoeList;
		public HashSetComponent<Unit> sceneEffectList;
	}
}