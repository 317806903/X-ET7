using System;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_BuyPlayerTowerHandler : AMActorLocationRpcHandler<Unit, C2M_BuyPlayerTower, M2C_BuyPlayerTower>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_BuyPlayerTower request, M2C_BuyPlayerTower response)
		{
			Unit playerUnit = ET.GamePlayHelper.GetPlayerUnit(observerUnit);

			long playerId = observerUnit.Id;

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(observerUnit.DomainScene());
			(bool bRet, string msg) = gamePlayTowerDefenseComponent.ChkBuyPlayerTower(playerId, request.Index);
			if (bRet == false)
			{
				response.Error = ErrorCode.ERR_LogicError;
				response.Message = msg;

				gamePlayTowerDefenseComponent.NoticeToClient(playerId);
			}
			else
			{
				bool success = gamePlayTowerDefenseComponent.BuyPlayerTower(playerId, request.Index);
				if (success == false)
				{
					response.Error = ErrorCode.ERR_LogicError;
					response.Message = "BuyPlayerTower Err";

					gamePlayTowerDefenseComponent.NoticeToClient(playerId);
				}
			}
			await ETTask.CompletedTask;
		}
	}
}