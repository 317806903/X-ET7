
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_CreateMonsterHandler : AMActorLocationHandler<Unit, C2M_CreateMonster>
	{
		protected override async ETTask Run(Unit unit, C2M_CreateMonster message)
		{
			Unit monsterUnit = UnitFactory.Create(unit.DomainScene(), 0, UnitType.Monster, message.Position);
			// EventSystem.Instance.Invoke<SyncUnits>(new SyncUnits(){
			// 	units = new List<Unit>(){monsterUnit},
			// });
			//
			await ETTask.CompletedTask;
		}
	}
}