using System;


namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_LoginGateHandler : AMRpcHandler<C2G_LoginGate, G2C_LoginGate>
	{
		protected override async ETTask Run(Session session, C2G_LoginGate request, G2C_LoginGate response)
		{
			Scene scene = session.DomainScene();
			string account = scene.GetComponent<GateSessionKeyComponent>().Get(request.Key);
			if (account == null)
			{
				response.Error = ErrorCore.ERR_ConnectGateKeyError;
				response.Message = "Gate key验证失败!";
				return;
			}
			
			session.RemoveComponent<SessionAcceptTimeoutComponent>();

			PlayerComponent playerComponent = scene.GetComponent<PlayerComponent>();
			//Player player = playerComponent.AddChild<Player, string>(account);

			long playerId = 0;
			Player player;
			if (string.IsNullOrEmpty(account))
			{
				player = playerComponent.AddChild<Player, string>(account);
				playerId = player.Id;
			}
			else
			{
				playerId = Convert.ToInt32(account);
				await LocationProxyComponent.Instance.RemoveLocation(playerId, LocationType.Player);
				player = playerComponent.GetChild<Player>(playerId);
				if (player != null)
				{
                    player.GetComponent<PlayerSessionComponent>()?.Session?.Dispose();
                    player.RemoveComponent<PlayerSessionComponent>();
					player.RemoveComponent<MailBoxComponent>();
					player.RemoveComponent<GateMapComponent>();
				}
				else
				{
					player = playerComponent.AddChildWithId<Player, string>(playerId, account);
				}
			}

			PlayerStatusComponent playerStatusComponent = player.GetComponent<PlayerStatusComponent>();
			if (playerStatusComponent == null)
			{
				playerStatusComponent = player.AddComponent<PlayerStatusComponent>();
				playerStatusComponent.PlayerStatus = PlayerStatus.Hall;
				playerStatusComponent.RoomId = 0;
			}
			

			StartSceneConfig roomSceneConfig = StartSceneConfigCategory.Instance.GetRoomManager(session.DomainZone());

			R2G_GetRoomIdByPlayer _R2G_GetRoomIdByPlayer = (R2G_GetRoomIdByPlayer) await ActorMessageSenderComponent.Instance.Call(roomSceneConfig.InstanceId, new G2R_GetRoomIdByPlayer()
			{
				PlayerId = playerId,
			});

			if (_R2G_GetRoomIdByPlayer.RoomId == 0)
			{
				playerStatusComponent.PlayerStatus = PlayerStatus.Hall;
				playerStatusComponent.RoomId = 0;
			}
			else
			{
				RoomStatus roomStatus = (RoomStatus) Enum.Parse(typeof (RoomStatus), _R2G_GetRoomIdByPlayer.RoomStatus);
				if (roomStatus == RoomStatus.Idle)
				{
					playerStatusComponent.PlayerStatus = PlayerStatus.Room;
				}
				else
				{
					playerStatusComponent.PlayerStatus = PlayerStatus.Battle;
				}
				playerStatusComponent.RoomId = _R2G_GetRoomIdByPlayer.RoomId;
			}
			
            player.AddComponent<PlayerSessionComponent>().Session = session;
			player.AddComponent<MailBoxComponent, MailboxType>(MailboxType.GateSession);
			await player.AddLocation(LocationType.Player);
			
			session.AddComponent<SessionPlayerComponent>().Player = player;

			response.PlayerId = playerId;
			response.PlayerStatus = playerStatusComponent.PlayerStatus.ToString();
			response.RoomId = playerStatusComponent.RoomId;
			await ETTask.CompletedTask;
		}
	}
}