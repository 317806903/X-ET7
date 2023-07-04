using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Gate)]
	public class M2G_QuitBattleHandler : AMActorRpcHandler<Player, M2G_MemberQuitBattle, G2M_MemberQuitBattle>
	{
		protected override async ETTask Run(Player player, M2G_MemberQuitBattle request, G2M_MemberQuitBattle response)
		{
			PlayerStatusComponent playerStatusComponent = player.GetComponent<PlayerStatusComponent>();
			playerStatusComponent.PlayerStatus = PlayerStatus.Hall;
			if (playerStatusComponent.PlayerGameMode == PlayerGameMode.Room
			|| playerStatusComponent.PlayerGameMode == PlayerGameMode.ARRoom)
			{
				long playerId = player.Id;
				long roomId = playerStatusComponent.RoomId;

				StartSceneConfig roomSceneConfig = StartSceneConfigCategory.Instance.GetRoomManager(player.DomainZone());

				R2G_QuitRoom _R2G_QuitRoom = (R2G_QuitRoom) await ActorMessageSenderComponent.Instance.Call(roomSceneConfig
						.InstanceId, new G2R_QuitRoom()
				{
					PlayerId = playerId,
					RoomId = roomId,
				});

				playerStatusComponent.RoomId = 0;
			}
			
			await playerStatusComponent.NoticeClient();
			
			await ETTask.CompletedTask;
		}
	}
}