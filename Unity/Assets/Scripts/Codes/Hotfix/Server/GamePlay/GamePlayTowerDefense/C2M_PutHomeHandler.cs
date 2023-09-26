using System;
using System.Collections.Generic;
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
			gamePlayTowerDefenseComponent.RemoveComponent<PutHomeComponent>();
			PutHomeComponent putHomeComponent = gamePlayTowerDefenseComponent.AddComponent<PutHomeComponent>();
			//if (gamePlayComponent.isAR)
			{
				gamePlayComponent.ResetPlayerBirthPos(pos);
			}
			putHomeComponent.Init(unitCfgId, pos);
			gamePlayTowerDefenseComponent.DealFriendTeamFlagType();
			await gamePlayTowerDefenseComponent.TransToPutMonsterPoint();

			await ETTask.CompletedTask;
		}

	}
}