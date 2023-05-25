
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_CreateMonsterHandler : AMActorLocationHandler<Unit, C2M_CreateMonster>
	{
		protected override async ETTask Run(Unit unit, C2M_CreateMonster message)
		{
			float3 forward = new float3(0, 0, 1);
			Unit monsterUnit = ET.Ability.UnitHelper_Create.CreateWhenServer_Monster(unit.DomainScene(), message.Position, forward);
			// EventSystem.Instance.Invoke<SyncUnits>(new SyncUnits(){
			// 	units = new List<Unit>(){monsterUnit},
			// });
			//
			await ETTask.CompletedTask;
		}
	}
}