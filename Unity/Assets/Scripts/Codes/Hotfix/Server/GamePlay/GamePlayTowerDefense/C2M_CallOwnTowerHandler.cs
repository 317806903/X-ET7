using ET.Ability;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_CallOwnTowerHandler : AMActorLocationRpcHandler<Unit, C2M_CallOwnTower, M2C_CallOwnTower>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_CallOwnTower request, M2C_CallOwnTower response)
		{
			Unit playerUnit = ET.GamePlayHelper.GetPlayerUnit(observerUnit);

			long playerId = observerUnit.Id;

			string towerUnitCfgId = request.TowerUnitCfgId;
			float3 position = request.Position;

			position = ET.RecastHelper.GetNearNavmeshPos(observerUnit, position);


			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(observerUnit.DomainScene());
			(bool bRet, string msg) = gamePlayTowerDefenseComponent.ChkCallPlayerTower(playerId, towerUnitCfgId);
			if (bRet == false)
			{
				response.Error = ErrorCode.ERR_LogicError;
				response.Message = msg;
			}
			else
			{
				bool success = gamePlayTowerDefenseComponent.CallPlayerTower(playerId, towerUnitCfgId, position);
				if (success == false)
				{
					response.Error = ErrorCode.ERR_LogicError;
					response.Message = "CallOwnTower Err";
				}
			}
			await ETTask.CompletedTask;
		}
	}
}