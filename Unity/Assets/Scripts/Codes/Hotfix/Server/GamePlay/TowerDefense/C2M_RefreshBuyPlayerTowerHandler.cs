using System;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_RefreshBuyPlayerTowerHandler : AMActorLocationRpcHandler<Unit, C2M_RefreshBuyPlayerTower, M2C_RefreshBuyPlayerTower>
	{
		protected override async ETTask Run(Unit unit, C2M_RefreshBuyPlayerTower request, M2C_RefreshBuyPlayerTower response)
		{
			long playerId = unit.Id;

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(unit.DomainScene());
			bool success = gamePlayTowerDefenseComponent.RefreshBuyPlayerTower(unit);
			if (success == false)
			{
				response.Error = ErrorCode.ERR_LogicError;
				response.Message = "RefreshBuyPlayerTower Err";
			}
			await ETTask.CompletedTask;
		}
	}
}