using System;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_BuyPlayerTowerHandler : AMActorLocationRpcHandler<Unit, C2M_BuyPlayerTower, M2C_BuyPlayerTower>
	{
		protected override async ETTask Run(Unit unit, C2M_BuyPlayerTower request, M2C_BuyPlayerTower response)
		{
			long playerId = unit.Id;

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(unit.DomainScene());
			bool success = gamePlayTowerDefenseComponent.BuyPlayerTower(unit, request.Index);
			if (success == false)
			{
				response.Error = ErrorCode.ERR_LogicError;
				response.Message = "BuyPlayerTower Err";
			}
			await ETTask.CompletedTask;
		}
	}
}