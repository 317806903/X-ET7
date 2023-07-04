using System;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_BuyPlayerTowerHandler : AMActorLocationRpcHandler<Unit, C2M_BuyPlayerTower, M2C_BuyPlayerTower>
	{
		protected override async ETTask Run(Unit unit, C2M_BuyPlayerTower request, M2C_BuyPlayerTower response)
		{
			long playerId = unit.Id;

			GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlayer(unit.DomainScene());
			ET.GamePlayHelper.BuyPlayerTower(gamePlayComponent, unit, request.Index);

			await ETTask.CompletedTask;
		}
	}
}