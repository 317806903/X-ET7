using System;


namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_ChgRoomMemberTeamHandler : AMRpcHandler<C2G_ChgRoomMemberTeam, G2C_ChgRoomMemberTeam>
	{
		protected override async ETTask Run(Session session, C2G_ChgRoomMemberTeam request, G2C_ChgRoomMemberTeam response)
		{
			Player player = session.GetComponent<SessionPlayerComponent>().Player;
			PlayerStatusComponent playerStatusComponent = player.GetComponent<PlayerStatusComponent>();
			long playerId = player.Id;
			long roomId = playerStatusComponent.RoomId;
			int newTeam = request.NewTeam;

			StartSceneConfig roomSceneConfig = StartSceneConfigCategory.Instance.GetRoomManager(session.DomainZone());

			R2G_ChgRoomMemberTeam _R2G_ChgRoomMemberTeam = (R2G_ChgRoomMemberTeam) await ActorMessageSenderComponent.Instance.Call(roomSceneConfig.InstanceId, new G2R_ChgRoomMemberTeam()
			{
				PlayerId = playerId,
				RoomId = roomId,
				NewTeam = newTeam,
			});

			response.Error = _R2G_ChgRoomMemberTeam.Error;
			response.Message = _R2G_ChgRoomMemberTeam.Message;

			await ETTask.CompletedTask;
		}
	}
}