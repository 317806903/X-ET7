

using System;

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

            try
            {
	            // 发送断线消息
	            ActorLocationSenderOneType actorLocationSenderOneType = ActorLocationSenderComponent.Instance?.Get(LocationType.Unit);
	            await actorLocationSenderOneType.Call(playerId, new G2M_SessionDisconnect());
            }
            catch (Exception e)
            {
	            Log.Error(e);
            }

			try
			{
				PlayerStatusComponent playerStatusComponent = self.Player.GetComponent<PlayerStatusComponent>();
				long roomId = playerStatusComponent.RoomId;
				StartSceneConfig roomSceneConfig = StartSceneConfigCategory.Instance.GetRoomManager(self.DomainZone());
				R2G_QuitRoom _R2G_QuitRoom = (R2G_QuitRoom) await ActorMessageSenderComponent.Instance.Call(roomSceneConfig.InstanceId, new G2R_QuitRoom()
				{
					PlayerId = playerId,
					RoomId = roomId,
				});
			}
			catch (Exception e)
			{
				Log.Error(e);
			}

			await self.Player.RemoveLocation(LocationType.Player);

			playerComponent?.RemoveChild(playerId);
		}
	}
}
