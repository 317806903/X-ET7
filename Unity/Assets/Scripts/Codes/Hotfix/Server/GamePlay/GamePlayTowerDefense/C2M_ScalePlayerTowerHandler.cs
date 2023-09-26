using ET.Ability;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_ScalePlayerTowerHandler : AMActorLocationRpcHandler<Unit, C2M_ScalePlayerTower, M2C_ScalePlayerTower>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_ScalePlayerTower request, M2C_ScalePlayerTower response)
		{
			Unit playerUnit = ET.GamePlayHelper.GetPlayerUnit(observerUnit);

			long playerId = observerUnit.Id;

			long towerUnitId = request.TowerUnitId;

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(observerUnit.DomainScene());
			bool success = gamePlayTowerDefenseComponent.ScalePlayerTower(playerId, towerUnitId);
			if (success == false)
			{
				response.Error = ErrorCode.ERR_LogicError;
				response.Message = "ScalePlayerTower Err";
			}
			await ETTask.CompletedTask;
		}
	}
}