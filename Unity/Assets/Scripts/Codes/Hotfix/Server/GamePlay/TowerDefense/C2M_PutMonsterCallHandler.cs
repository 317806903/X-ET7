using System;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_PutMonsterCallHandler : AMActorLocationRpcHandler<Unit, C2M_PutMonsterCall, M2C_PutMonsterCall>
	{
		protected override async ETTask Run(Unit unit, C2M_PutMonsterCall request, M2C_PutMonsterCall response)
		{
			long playerId = unit.Id;

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(unit.DomainScene());
			PutMonsterCallComponent putMonsterCallComponent = gamePlayTowerDefenseComponent.GetComponent<PutMonsterCallComponent>();
			if (putMonsterCallComponent == null)
			{
				putMonsterCallComponent = gamePlayTowerDefenseComponent.AddComponent<PutMonsterCallComponent>();
			}
			putMonsterCallComponent.Init(playerId, request.UnitCfgId, request.Position);
			
			await ETTask.CompletedTask;
		}
	}
}