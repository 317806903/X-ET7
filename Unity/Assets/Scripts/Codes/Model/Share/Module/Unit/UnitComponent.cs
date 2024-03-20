using System.Collections.Generic;
using ET.Ability;

namespace ET
{

	[ComponentOf(typeof(Scene))]
	public class UnitComponent: Entity, IAwake, IDestroy, IFixedUpdate
	{
		public int waitFrameSyncPos = 2;
		public int curFrameSyncPos = 0;

		public int waitFrameSyncNumericKey = 4;
		public int curFrameSyncNumericKey = 0;

		public int waitFrameSyncNumeric = 4;
		public int curFrameSyncNumeric = 0;

		public MultiMapSetSimple<long, int> NeedSyncNumericUnitsKey;
		public List<long> NeedSyncNumericUnitsKeyTmp = new();
		public HashSetComponent<Unit> NeedSyncNumericUnits;
		public List<Unit> NeedSyncNumericUnitsTmp = new();

		public MultiMapSetSimple<Unit, Unit> NeedSyncNoticeUnitAdds;
		public List<Unit> NeedSyncNoticeUnitAddsTmp = new();

		public MultiMapSetSimple<Unit, long> NeedSyncNoticeUnitRemoves;
		public List<long> NeedSyncNoticeUnitRemovesTmp = new();

		public HashSetComponent<Unit> NeedSyncPosUnits;
		public List<Unit> NeedSyncPosUnitsTmp = new();

		public List<long> waitRemoveList;

		public HashSetComponent<Unit> observerList;
		public HashSetComponent<Unit> playerList;
		public HashSetComponent<Unit> actorList;
		public HashSetComponent<Unit> npcList;
		public HashSetComponent<Unit> sceneObjList;
		public HashSetComponent<Unit> bulletList;
		public HashSetComponent<Unit> aoeList;
		public HashSetComponent<Unit> sceneEffectList;

		public bool IsStopActorMove { get; set; }
	}
}