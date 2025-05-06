using System;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_RefreshBuyPlayerTowerHandler : AMActorLocationRpcHandler<Unit, C2M_RefreshBuyPlayerTower, M2C_RefreshBuyPlayerTower>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_RefreshBuyPlayerTower request, M2C_RefreshBuyPlayerTower response)
		{
			//Unit playerUnit = ET.GamePlayHelper.GetCurPlayerUnit(observerUnit);

			long playerId = observerUnit.Id;

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(observerUnit.DomainScene());
			(bool bRet, string msg) = gamePlayTowerDefenseComponent.ChkRefreshBuyPlayerTower(playerId);
			if (bRet == false)
			{
				response.Error = ErrorCode.ERR_LogicError;
				response.Message = msg;

				gamePlayTowerDefenseComponent.NoticeToClient(playerId);
			}
			else
			{
				bool success = gamePlayTowerDefenseComponent.RefreshBuyPlayerTower(playerId);
				if (success == false)
				{
					response.Error = ErrorCode.ERR_LogicError;
					response.Message = "RefreshBuyPlayerTower Err";

					gamePlayTowerDefenseComponent.NoticeToClient(playerId);
				}
			}
			await ETTask.CompletedTask;
		}
	}
}