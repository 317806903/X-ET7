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
			//Unit playerUnit = ET.GamePlayHelper.GetCurPlayerUnit(observerUnit);

			long playerId = observerUnit.Id;

			string towerCfgId = request.TowerUnitCfgId;
			float3 position = request.Position;

			// position = ET.RecastHelper.GetHitNavmeshPos(observerUnit.DomainScene(), position);
			// if (position.Equals(float3.zero))
			// {
			// 	response.Error = ErrorCode.ERR_LogicError;
			// 	response.Message = "not found position";
			// 	return;
			// }

			float3 forward = request.Forward;

			bool isAttackTower = ET.ItemHelper.ChkIsAttackTower(towerCfgId);
			bool isCallMonster = ET.ItemHelper.ChkIsCallMonster(towerCfgId);

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
					float3 homePosition = gamePlayTowerDefenseComponent.GetHomePosition(playerId);

					(TeamFlagType teamFlagType, Unit nearHostileHomeUnit) = gamePlayTowerDefenseComponent.GetNearHostileHomeByPlayerId(playerId, position);

					if (nearHostileHomeUnit != null)
					{
						if (math.distancesq(homePosition, position) > math.distancesq(nearHostileHomeUnit.Position, position))
						{
							response.Error = ErrorCode.ERR_LogicError;
							response.Message = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutTowerCallMonster_TooNearHostileHome");
							return;
						}

						(bool canArrive, List<float3> pointList) = ET.RecastHelper.ChkArrive(observerUnit, position, nearHostileHomeUnit.Position);
						if (canArrive == false)
						{
							response.Error = ErrorCode.ERR_LogicError;
							response.Message = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPut_IsNotReachHome");
							return;
						}
					}
					else
					{
						response.Error = ErrorCode.ERR_LogicError;
						response.Message = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutTowerCallMonster_CanotFindNearHostileHome");
						return;
					}
				}

				bool success = gamePlayTowerDefenseComponent.CallPlayerTower(playerId, towerCfgId, position, forward);
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