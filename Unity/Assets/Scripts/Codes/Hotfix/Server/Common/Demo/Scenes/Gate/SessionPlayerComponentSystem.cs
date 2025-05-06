

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

		public static Session GetSession(this SessionPlayerComponent self)
		{
			return self.GetParent<Session>();
		}

		public static async ETTask DoDestroy(this SessionPlayerComponent self)
		{
			if (self.GetSession().IsTimeOut == false)
			{
				return;
			}

			if (self.Player == null)
			{
				return;
			}
			long playerId = self.Player.Id;
			if (LocationProxyComponent.Instance == null)
			{
				return;
            }
			if (ConfigComponent.Instance == null)
			{
				return;
            }

			long sceneInstanceId = self.DomainScene().InstanceId;

			PlayerComponent playerComponent = self.DomainScene().GetComponent<PlayerComponent>();

			Player player = playerComponent.GetChild<Player>(playerId);
			if (player != null)
			{
				PlayerSessionComponent playerSessionComponent = player.GetComponent<PlayerSessionComponent>();
				if (playerSessionComponent.Session != null && playerSessionComponent.Session != self.GetSession())
				{
					return;
				}
			}

            long locationActorId = await LocationProxyComponent.Instance.Get(LocationType.Player, playerId, sceneInstanceId);
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
	            await actorLocationSenderOneType?.Call(playerId, new G2M_SessionDisconnect(), sceneInstanceId);
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
				if (roomSceneConfig != null)
				{
					R2G_QuitRoom _R2G_QuitRoom = (R2G_QuitRoom) await ActorMessageSenderComponent.Instance.Call(roomSceneConfig.InstanceId, new G2R_QuitRoom()
					{
						PlayerId = playerId,
						RoomId = roomId,
					});
				}
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
