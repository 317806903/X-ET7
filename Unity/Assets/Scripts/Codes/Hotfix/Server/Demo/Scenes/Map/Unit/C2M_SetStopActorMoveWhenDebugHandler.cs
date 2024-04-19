
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_SetStopActorMoveWhenDebugHandler : AMActorLocationHandler<Unit, C2M_SetStopActorMoveWhenDebug>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_SetStopActorMoveWhenDebug message)
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
			bool isStopActorMove = message.IsStopActorMove == 1?true:false;

			UnitComponent unitComponent = ET.Ability.UnitHelper.GetUnitComponent(observerUnit);
			unitComponent.IsStopActorMove = isStopActorMove;

			await ETTask.CompletedTask;
		}
	}
}