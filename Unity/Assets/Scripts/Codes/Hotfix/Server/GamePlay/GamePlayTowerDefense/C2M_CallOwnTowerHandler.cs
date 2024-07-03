using System.Collections.Generic;
using ET.Ability;
using ET.AbilityConfig;
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

			string towerCfgId = request.TowerUnitCfgId;
			float3 position = request.Position;

			position = ET.RecastHelper.GetHitNavmeshPos(observerUnit.DomainScene(), position);
			if (position.Equals(float3.zero))
			{
				response.Error = ErrorCode.ERR_LogicError;
				response.Message = "not found position";
				return;
			}

			TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(towerCfgId);

			bool isAttackTower = ItemHelper.ChkIsAttackTower(towerCfgId);
			bool isCallMonster = ItemHelper.ChkIsCallMonster(towerCfgId);

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(observerUnit.DomainScene());
			(bool bRet, string msg) = gamePlayTowerDefenseComponent.ChkCallPlayerTower(playerId, towerCfgId);
			if (bRet == false)
			{
				response.Error = ErrorCode.ERR_LogicError;
				response.Message = msg;

				gamePlayTowerDefenseComponent.NoticeToClient(playerId);
			}
			else
			{
				if (isCallMonster)
				{
					(TeamFlagType teamFlagType, Unit homeUnit) = gamePlayTowerDefenseComponent.GetNearHostileHomeByPlayerId(playerId, position);

					if (homeUnit != null)
					{
						(bool canArrive, List<float3> pointList) = ET.RecastHelper.ChkArrive(observerUnit, position, homeUnit.Position);
						if (canArrive == false)
						{
							response.Error = ErrorCode.ERR_LogicError;
							response.Message = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsReachHome");
							return;
						}
					}
					else
					{
						response.Error = ErrorCode.ERR_LogicError;
						response.Message = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsNearHostileHome");
						return;
					}
				}

				bool success = gamePlayTowerDefenseComponent.CallPlayerTower(playerId, towerCfgId, position);
				if (success == false)
				{
					response.Error = ErrorCode.ERR_LogicError;
					response.Message = "CallOwnTower Err";

					gamePlayTowerDefenseComponent.NoticeToClient(playerId);
				}
			}
			await ETTask.CompletedTask;
		}
	}
}