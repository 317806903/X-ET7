using ET.Ability;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_MovePlayerTowerHandler : AMActorLocationRpcHandler<Unit, C2M_MovePlayerTower, M2C_MovePlayerTower>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_MovePlayerTower request, M2C_MovePlayerTower response)
		{
			//Unit playerUnit = ET.GamePlayHelper.GetCurPlayerUnit(observerUnit);

			long playerId = observerUnit.Id;

			long towerUnitId = request.TowerUnitId;
			float3 position = request.Position;
			float3 forward = request.Forward;

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(observerUnit.DomainScene());
			(bool bRet, string msg) = gamePlayTowerDefenseComponent.ChkMovePlayerTower(playerId, towerUnitId, position);
			if (bRet == false)
			{
				response.Error = ErrorCode.ERR_LogicError;
				response.Message = msg;

				gamePlayTowerDefenseComponent.NoticeToClient(playerId);
			}
			else
			{
				bool success = gamePlayTowerDefenseComponent.MovePlayerTower(playerId, towerUnitId, position, forward);
				if (success == false)
				{
					response.Error = ErrorCode.ERR_LogicError;
					response.Message = "MovePlayerTower Err";

					gamePlayTowerDefenseComponent.NoticeToClient(playerId);
				}
			}
			await ETTask.CompletedTask;
		}
	}
}