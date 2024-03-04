using System;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_MemberReturnRoomFromBattleHandler : AMActorLocationRpcHandler<Unit, C2M_MemberReturnRoomFromBattle, M2C_MemberReturnRoomFromBattle>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_MemberReturnRoomFromBattle request, M2C_MemberReturnRoomFromBattle response)
		{
			Unit playerUnit = ET.GamePlayHelper.GetPlayerUnit(observerUnit);

			long playerId = observerUnit.Id;

			// M2G_MemberReturnRoomFromBattle _M2G_MemberReturnRoomFromBattle = new()
			// {
			// 	BattleResult =
			// };
			// ActorLocationSenderOneType oneTypeLocationType = ActorLocationSenderComponent.Instance.Get(LocationType.Player);
			// await oneTypeLocationType.Call(playerId, _M2G_MemberReturnRoomFromBattle, observerUnit.DomainScene().InstanceId);

			observerUnit.RemoveLocation(LocationType.Unit).Coroutine();
			GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(observerUnit.DomainScene());
			gamePlayComponent.PlayerQuitBattle(playerId, true);

			await ETTask.CompletedTask;
		}
	}
}