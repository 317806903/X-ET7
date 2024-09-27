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

		public MultiMapSetSimple<UnitType, Unit> recordDic;
		public HashSet<Unit> addRecordTmp;

		public bool IsStopActorMove { get; set; }
	}
}