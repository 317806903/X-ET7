using System;
using System.Collections.Generic;
using ET.Ability;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_PutHomeHandler : AMActorLocationRpcHandler<Unit, C2M_PutHome, M2C_PutHome>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_PutHome request, M2C_PutHome response)
		{
			//Unit playerUnit = ET.GamePlayHelper.GetCurPlayerUnit(observerUnit);

			long playerId = observerUnit.Id;
			string unitCfgId = request.UnitCfgId;
			float3 pos = request.Position;

			GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(observerUnit.DomainScene());
			(bool isLoadMeshFinished, bool isLoadMeshError) = gamePlayComponent.ChkNavMeshReady();
			if (isLoadMeshError)
			{
				response.Error = ErrorCode.ERR_LogicError;
				response.Message = "NavMesh Error";
				return;
			}
			else if (isLoadMeshFinished == false)
			{
				response.Error = ErrorCode.ERR_LogicError;
				response.Message = "NavMesh not ready";
				return;
			}

			pos = ET.RecastHelper.GetNearNavmeshPos(observerUnit, pos);

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(observerUnit.DomainScene());
			PutHomeComponent putHomeComponent = gamePlayTowerDefenseComponent.GetComponent<PutHomeComponent>();
			bool canPut = putHomeComponent.ChkPosition(pos);
			if (canPut == false)
			{
				response.Error = ErrorCode.ERR_LogicError;
				response.Message = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Tip_NoWayToConnectHome");
			}
			else
			{
				bool bRet = putHomeComponent.InitHomeByPlayer(playerId, unitCfgId, pos);
				if (bRet)
				{
					TeamFlagType playerTeamFlagType = gamePlayComponent.GetTeamFlagByPlayerId(playerId);
					gamePlayComponent.ResetPlayerBirthPos(playerTeamFlagType, pos);
				}
			}

			await ETTask.CompletedTask;
		}
	}
}