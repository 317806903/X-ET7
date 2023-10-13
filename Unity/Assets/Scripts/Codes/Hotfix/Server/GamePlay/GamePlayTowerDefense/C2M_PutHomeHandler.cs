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
			Unit playerUnit = ET.GamePlayHelper.GetPlayerUnit(observerUnit);

			long playerId = observerUnit.Id;
			string unitCfgId = request.UnitCfgId;
			float3 pos = request.Position;

			pos = ET.RecastHelper.GetNearNavmeshPos(observerUnit, pos);

			GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(observerUnit.DomainScene());
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(observerUnit.DomainScene());
			PutHomeComponent putHomeComponent = gamePlayTowerDefenseComponent.GetComponent<PutHomeComponent>();
			bool canPut = putHomeComponent.ChkPosition(pos);
			if (canPut == false)
			{
				response.Error = ErrorCode.ERR_LogicError;
				response.Message = "当前放置位置 没法连通大本营,请重新选位置";
			}
			else
			{
				//if (gamePlayComponent.isAR)
				{
					TeamFlagType playerTeamFlagType = gamePlayComponent.GetTeamFlagByPlayerId(playerId);
					gamePlayComponent.ResetPlayerBirthPos(playerTeamFlagType, pos);
				}
				putHomeComponent.InitHomeByPlayer(playerId, unitCfgId, pos);
			}

			await ETTask.CompletedTask;
		}
	}
}