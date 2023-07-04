using System.Collections.Generic;

namespace ET
{
	
	[ComponentOf(typeof(Scene))]
	public class UnitComponent: Entity, IAwake, IDestroy, IFixedUpdate
	{
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