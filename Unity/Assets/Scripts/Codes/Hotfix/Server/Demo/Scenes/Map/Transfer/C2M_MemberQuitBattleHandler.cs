using System;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_MemberQuitBattleHandler : AMActorLocationRpcHandler<Unit, C2M_MemberQuitBattle, M2C_MemberQuitBattle>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_MemberQuitBattle request, M2C_MemberQuitBattle response)
		{
			Unit playerUnit = ET.GamePlayHelper.GetPlayerUnit(observerUnit);

			long playerId = observerUnit.Id;

			M2G_MemberQuitBattle _M2G_MemberQuitBattle = new();
			ActorLocationSenderOneType oneTypeLocationType = ActorLocationSenderComponent.Instance.Get(LocationType.Player);
			await oneTypeLocationType.Call(playerId, _M2G_MemberQuitBattle);

			observerUnit.RemoveLocation(LocationType.Unit).Coroutine();
			GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(observerUnit.DomainScene());
			gamePlayComponent.PlayerQuitBattle(playerId, true);

			await ETTask.CompletedTask;
		}
	}
}