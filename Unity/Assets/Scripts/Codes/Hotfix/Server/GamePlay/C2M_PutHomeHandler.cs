using System;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_PutHomeHandler : AMActorLocationRpcHandler<Unit, C2M_PutHome, M2C_PutHome>
	{
		protected override async ETTask Run(Unit unit, C2M_PutHome request, M2C_PutHome response)
		{
			long playerId = unit.Id;

			GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlayer(unit.DomainScene());
			gamePlayComponent.RemoveComponent<PutHomeComponent>();
			PutHomeComponent putHomeComponent = gamePlayComponent.AddComponent<PutHomeComponent>();
			putHomeComponent.Init(request.UnitCfgId, request.Position);
			gamePlayComponent.DealFriendTeamFlagType();
			gamePlayComponent.TransToPutMonsterPoint();
			
			await ETTask.CompletedTask;
		}
	}
}