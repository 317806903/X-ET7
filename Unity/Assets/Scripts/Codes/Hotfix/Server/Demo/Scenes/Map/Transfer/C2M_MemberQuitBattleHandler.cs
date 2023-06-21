using System;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_MemberQuitBattleHandler : AMActorLocationRpcHandler<Unit, C2M_MemberQuitBattle, M2C_MemberQuitBattle>
	{
		protected override async ETTask Run(Unit unit, C2M_MemberQuitBattle request, M2C_MemberQuitBattle response)
		{
			long playerId = unit.Id;

			M2G_MemberQuitBattle _M2G_MemberQuitBattle = new();
			ActorLocationSenderOneType oneTypeLocationType = ActorLocationSenderComponent.Instance.Get(LocationType.Player);
			await oneTypeLocationType.Call(playerId, _M2G_MemberQuitBattle);
			
			unit.RemoveLocation(LocationType.Unit).Coroutine();
			unit.Dispose();
			
			await ETTask.CompletedTask;
		}
	}
}