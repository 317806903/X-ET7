using System.Collections.Generic;
namespace ET
{
	[ComponentOf(typeof(Scene))]
	public class RealmGetGatePlayerCountComponent : Entity, IAwake, IDestroy
	{
		public int CheckInteral = 120000;
		public long RepeatedTimer;
		public Dictionary<StartSceneConfig, int> GateId2Count;
	}
}