using System;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_BattleRecoverConfirmHandler : AMActorLocationRpcHandler<Unit, C2M_BattleRecoverConfirm, M2C_BattleRecoverConfirm>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_BattleRecoverConfirm request, M2C_BattleRecoverConfirm response)
		{
			Unit playerUnit = ET.GamePlayHelper.GetPlayerUnit(observerUnit);

			long playerId = observerUnit.Id;

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(observerUnit.DomainScene());
			gamePlayTowerDefenseComponent.DealRecover();

			await ETTask.CompletedTask;
		}
	}
}