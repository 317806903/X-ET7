using System;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_RefreshBuyPlayerTowerHandler : AMActorLocationRpcHandler<Unit, C2M_RefreshBuyPlayerTower, M2C_RefreshBuyPlayerTower>
	{
		protected override async ETTask Run(Unit unit, C2M_RefreshBuyPlayerTower request, M2C_RefreshBuyPlayerTower response)
		{
			long playerId = unit.Id;

			GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlayer(unit.DomainScene());
			ET.GamePlayHelper.RefreshBuyPlayerTower(gamePlayComponent, unit);
			
			await ETTask.CompletedTask;
		}
	}
}