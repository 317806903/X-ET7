using System;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_MemberReturnRoomFromBattleHandler : AMActorLocationRpcHandler<Unit, C2M_MemberReturnRoomFromBattle, M2C_MemberReturnRoomFromBattle>
	{
		protected override async ETTask Run(Unit unit, C2M_MemberReturnRoomFromBattle request, M2C_MemberReturnRoomFromBattle response)
		{
			long playerId = unit.Id;

			M2G_MemberReturnRoomFromBattle _M2G_MemberReturnRoomFromBattle = new();
			ActorLocationSenderOneType oneTypeLocationType = ActorLocationSenderComponent.Instance.Get(LocationType.Player);
			await oneTypeLocationType.Call(playerId, _M2G_MemberReturnRoomFromBattle);
			
			unit.RemoveLocation(LocationType.Unit).Coroutine();
			GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(unit.DomainScene());
			gamePlayComponent.PlayerQuitBattle(playerId, true);

			await ETTask.CompletedTask;
		}
	}
}