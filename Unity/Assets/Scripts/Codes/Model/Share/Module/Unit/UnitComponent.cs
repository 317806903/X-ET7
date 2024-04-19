using System.Collections.Generic;
using ET.Ability;

namespace ET
{

	[ComponentOf(typeof(Scene))]
	public class UnitComponent: Entity, IAwake, IDestroy, IFixedUpdate
	{
		public MultiMapSetSimple<Unit, Unit> NeedSyncNoticeUnitAdds;
		public List<Unit> NeedSyncNoticeUnitAddsTmp = new();

		public MultiMapSetSimple<Unit, long> NeedSyncNoticeUnitRemoves;
		public List<long> NeedSyncNoticeUnitRemovesTmp = new();

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