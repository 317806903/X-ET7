using System;
using System.Collections.Generic;
using ET.Ability;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_GetMonsterCall2HeadQuarterPathHandler : AMActorLocationRpcHandler<Unit, C2M_GetMonsterCall2HeadQuarterPath, M2C_GetMonsterCall2HeadQuarterPath>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_GetMonsterCall2HeadQuarterPath request, M2C_GetMonsterCall2HeadQuarterPath response)
		{
			Unit playerUnit = ET.GamePlayHelper.GetPlayerUnit(observerUnit);

			long playerId = observerUnit.Id;
			TeamFlagType homeTeamFlagType = (TeamFlagType)request.HomeTeamFlagType;
			float3 startPos = request.Position;

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(observerUnit.DomainScene());
			PutHomeComponent putHomeComponent = gamePlayTowerDefenseComponent.GetComponent<PutHomeComponent>();
			float3 homePos = putHomeComponent.GetHomeUnitByTeamFlagType(homeTeamFlagType).Position;

			List<float3> points = ET.RecastHelper.GetArrivePath(observerUnit, startPos, homePos);
			response.Position = homePos;
			response.Points = points;

			await ETTask.CompletedTask;
		}
	}
}