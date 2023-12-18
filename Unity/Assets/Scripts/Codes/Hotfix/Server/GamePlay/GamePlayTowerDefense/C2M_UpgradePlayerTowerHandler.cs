using System.Collections.Generic;
using ET.Ability;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_UpgradePlayerTowerHandler : AMActorLocationRpcHandler<Unit, C2M_UpgradePlayerTower, M2C_UpgradePlayerTower>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_UpgradePlayerTower request, M2C_UpgradePlayerTower response)
		{
			Unit playerUnit = ET.GamePlayHelper.GetPlayerUnit(observerUnit);

			long playerId = observerUnit.Id;

			long towerUnitId = request.TowerUnitId;
			bool onlyChkPool = request.OnlyChkPool == 1?true:false;

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(observerUnit.DomainScene());
			(bool bRet, string msg, Dictionary<string, int> costTowers, List<long> existTowerUnitIds) = gamePlayTowerDefenseComponent.ChkUpgradePlayerTower(playerId, towerUnitId, onlyChkPool);
			if (bRet == false)
			{
				response.Error = ErrorCode.ERR_LogicError;
				response.Message = msg;
			}
			else
			{
				bool success = gamePlayTowerDefenseComponent.UpgradePlayerTower(playerId, towerUnitId, onlyChkPool);
				if (success == false)
				{
					response.Error = ErrorCode.ERR_LogicError;
					response.Message = "UpgradePlayerTower Err";
				}
			}
			await ETTask.CompletedTask;
		}
	}
}