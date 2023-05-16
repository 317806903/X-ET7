
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_CreateMonsterHandler : AMActorLocationHandler<Unit, C2M_CreateMonster>
	{
		protected override async ETTask Run(Unit unit, C2M_CreateMonster message)
		{
			M2C_CreateUnits createUnits = new () { Units = new List<UnitInfo>() };
			Unit monsterUnit = UnitFactory.Create(unit.DomainScene(), 0, UnitType.Monster, message.Position);
			createUnits.Units.Add(UnitHelper.CreateUnitInfo(monsterUnit));
			MessageHelper.Broadcast(unit, createUnits);

			await ETTask.CompletedTask;
		}
	}
}