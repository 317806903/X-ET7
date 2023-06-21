

namespace ET.Server
{
	public static class SessionPlayerComponentSystem
	{
		public class SessionPlayerComponentDestroySystem: DestroySystem<SessionPlayerComponent>
		{
			protected override void Destroy(SessionPlayerComponent self)
			{
				self.DoDestroy().Coroutine();
            }
        }
		
		public static async ETTask DoDestroy(this SessionPlayerComponent self)
		{
			if (self.Player == null)
			{
				return;
			}
			long playerId = self.Player.Id;
			PlayerComponent playerComponent = self.DomainScene().GetComponent<PlayerComponent>();
			if (LocationProxyComponent.Instance == null)
			{
				return;
            }
			
            long locationActorId = await LocationProxyComponent.Instance.Get(LocationType.Player, playerId);
			if (locationActorId != self.Player.InstanceId)
			{
				return;
            }

            if (self.Parent == null)
            {
                Log.Debug("222222222222 self.Parent == null");
            }

            // 发送断线消息
            ActorLocationSenderOneType actorLocationSenderOneType = ActorLocationSenderComponent.Instance?.Get(LocationType.Unit);
			await actorLocationSenderOneType.Call(playerId, new G2M_SessionDisconnect());

			await self.Player.RemoveLocation(LocationType.Player);
			// ActorLocationSenderComponent.Instance?.Get(LocationType.Unit).Remove(playerId);
			// ActorLocationSenderComponent.Instance?.Get(LocationType.Player).Remove(playerId);

			playerComponent?.RemoveChild(playerId);
		}
	}
}
