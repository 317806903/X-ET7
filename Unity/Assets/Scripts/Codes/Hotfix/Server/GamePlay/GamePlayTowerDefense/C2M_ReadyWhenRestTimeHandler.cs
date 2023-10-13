using ET.Ability;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_ReadyWhenRestTimeHandler : AMActorLocationRpcHandler<Unit, C2M_ReadyWhenRestTime, M2C_ReadyWhenRestTime>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_ReadyWhenRestTime request, M2C_ReadyWhenRestTime response)
		{
			Unit playerUnit = ET.GamePlayHelper.GetPlayerUnit(observerUnit);

			long playerId = observerUnit.Id;

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(observerUnit.DomainScene());
			gamePlayTowerDefenseComponent.SetReadyWhenRestTime(playerId);
			await ETTask.CompletedTask;
		}
	}
}