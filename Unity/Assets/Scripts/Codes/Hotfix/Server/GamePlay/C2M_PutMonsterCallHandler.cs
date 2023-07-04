using System;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_PutMonsterCallHandler : AMActorLocationRpcHandler<Unit, C2M_PutMonsterCall, M2C_PutMonsterCall>
	{
		protected override async ETTask Run(Unit unit, C2M_PutMonsterCall request, M2C_PutMonsterCall response)
		{
			long playerId = unit.Id;

			GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlayer(unit.DomainScene());
			gamePlayComponent.RemoveComponent<PutMonsterCallComponent>();
			PutMonsterCallComponent putMonsterCallComponent = gamePlayComponent.AddComponent<PutMonsterCallComponent>();
			putMonsterCallComponent.Init(request.UnitCfgId, request.Position);
			gamePlayComponent.TransToRestTime();
			
			await ETTask.CompletedTask;
		}
	}
}