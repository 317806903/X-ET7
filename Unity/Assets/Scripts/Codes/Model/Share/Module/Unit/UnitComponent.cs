using System.Collections.Generic;

namespace ET
{
	
	[ComponentOf(typeof(Scene))]
	public class UnitComponent: Entity, IAwake, IDestroy, IFixedUpdate
	{
		public ListComponent<Unit> NeedSyncUnits;
		public List<long> waitRemoveList;
		public HashSetComponent<Unit> playerList;
		public HashSetComponent<Unit> monsterList;
		public HashSetComponent<Unit> npcList;
		public HashSetComponent<Unit> sceneObjList;
		public HashSetComponent<Unit> bulletList;
	}
}