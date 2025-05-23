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
			//Unit playerUnit = ET.GamePlayHelper.GetCurPlayerUnit(observerUnit);

			long playerId = observerUnit.Id;

			long towerUnitId = request.TowerUnitId;
			bool onlyChkPool = request.OnlyChkPool == 1?true:false;
			string towerCfgId = request.TowerCfgId;

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(observerUnit.DomainScene());

			if (towerUnitId == 0)
			{
				(bool bRet, string msg, Dictionary<string, int> costTowers, List<long> existTowerUnitIds) = gamePlayTowerDefenseComponent.ChkUpgradePlayerTower(playerId, towerCfgId, onlyChkPool);
				if (bRet == false)
				{
					response.Error = ErrorCode.ERR_LogicError;
					response.Message = msg;

					gamePlayTowerDefenseComponent.NoticeToClient(playerId);
				}
				else
				{
					bool success = gamePlayTowerDefenseComponent.UpgradePlayerTower(playerId, towerCfgId, onlyChkPool);
					if (success == false)
					{
						response.Error = ErrorCode.ERR_LogicError;
						response.Message = "UpgradePlayerTower Err";

						gamePlayTowerDefenseComponent.NoticeToClient(playerId);
					}
				}
			}
			else
			{
				(bool bRet, string msg, Dictionary<string, int> costTowers, List<long> existTowerUnitIds) = gamePlayTowerDefenseComponent.ChkUpgradePlayerTower(playerId, towerUnitId, onlyChkPool);
				if (bRet == false)
				{
					response.Error = ErrorCode.ERR_LogicError;
					response.Message = msg;

					gamePlayTowerDefenseComponent.NoticeToClient(playerId);
				}
				else
				{
					bool success = gamePlayTowerDefenseComponent.UpgradePlayerTower(playerId, towerUnitId, onlyChkPool);
					if (success == false)
					{
						response.Error = ErrorCode.ERR_LogicError;
						response.Message = "UpgradePlayerTower Err";

						gamePlayTowerDefenseComponent.NoticeToClient(playerId);
					}
				}
			}
			await ETTask.CompletedTask;
		}
	}
}