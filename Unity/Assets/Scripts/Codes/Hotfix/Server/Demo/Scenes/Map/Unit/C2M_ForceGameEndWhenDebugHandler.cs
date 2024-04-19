
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_ForceGameEndWhenDebugHandler : AMActorLocationHandler<Unit, C2M_ForceGameEndWhenDebug>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_ForceGameEndWhenDebug message)
		{
			// Unit playerUnit = ET.GamePlayHelper.GetPlayerUnit(observerUnit);
			// if (playerUnit == null)
			// {
			// 	return;
			// }

			if (observerUnit == null)
			{
				return;
			}

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(observerUnit.DomainScene());
			await gamePlayTowerDefenseComponent.TransToGameEnd();

			await ETTask.CompletedTask;
		}
	}
}