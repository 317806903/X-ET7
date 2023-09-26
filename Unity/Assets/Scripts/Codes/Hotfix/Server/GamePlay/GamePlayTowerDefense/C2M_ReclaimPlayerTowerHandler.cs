using ET.Ability;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_ReclaimPlayerTowerHandler : AMActorLocationRpcHandler<Unit, C2M_ReclaimPlayerTower, M2C_ReclaimPlayerTower>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_ReclaimPlayerTower request, M2C_ReclaimPlayerTower response)
		{
			Unit playerUnit = ET.GamePlayHelper.GetPlayerUnit(observerUnit);

			long playerId = observerUnit.Id;

			long towerUnitId = request.TowerUnitId;

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(observerUnit.DomainScene());
			(bool bRet, string msg) = gamePlayTowerDefenseComponent.ChkReclaimPlayerTower(playerId, towerUnitId);
			if (bRet == false)
			{
				response.Error = ErrorCode.ERR_LogicError;
				response.Message = msg;
			}
			else
			{
				bool success = gamePlayTowerDefenseComponent.ReclaimPlayerTower(playerId, towerUnitId);
				if (success == false)
				{
					response.Error = ErrorCode.ERR_LogicError;
					response.Message = "ReclaimPlayerTower Err";
				}
			}
			await ETTask.CompletedTask;
		}
	}
}