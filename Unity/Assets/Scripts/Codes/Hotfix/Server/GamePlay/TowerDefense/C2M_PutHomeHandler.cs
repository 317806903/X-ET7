using System;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_PutHomeHandler : AMActorLocationRpcHandler<Unit, C2M_PutHome, M2C_PutHome>
	{
		protected override async ETTask Run(Unit unit, C2M_PutHome request, M2C_PutHome response)
		{
			long playerId = unit.Id;

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(unit.DomainScene());
			gamePlayTowerDefenseComponent.RemoveComponent<PutHomeComponent>();
			PutHomeComponent putHomeComponent = gamePlayTowerDefenseComponent.AddComponent<PutHomeComponent>();
			putHomeComponent.Init(request.UnitCfgId, request.Position);
			gamePlayTowerDefenseComponent.DealFriendTeamFlagType();
			gamePlayTowerDefenseComponent.TransToPutMonsterPoint();
			
			await ETTask.CompletedTask;
		}
	}
}