using System;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_BattleRecoverCancelHandler : AMActorLocationRpcHandler<Unit, C2M_BattleRecoverCancel, M2C_BattleRecoverCancel>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_BattleRecoverCancel request, M2C_BattleRecoverCancel response)
		{
			Unit playerUnit = ET.GamePlayHelper.GetPlayerUnit(observerUnit);

			long playerId = observerUnit.Id;

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(observerUnit.DomainScene());
			await gamePlayTowerDefenseComponent.TransToGameEnd();
			
			await ETTask.CompletedTask;
		}
	}
}