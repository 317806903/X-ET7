using System;
using ET.AbilityConfig;

namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_CreateRoomHandler : AMRpcHandler<C2G_CreateRoom, G2C_CreateRoom>
	{
		protected override async ETTask Run(Session session, C2G_CreateRoom request, G2C_CreateRoom response)
		{
			Player player = session.GetComponent<SessionPlayerComponent>().Player;
			long playerId = player.Id;
			string battleCfgId = request.BattleCfgId;
			int RoomType = request.RoomType;
			int SubRoomType = request.SubRoomType;

			StartSceneConfig roomSceneConfig = StartSceneConfigCategory.Instance.GetRoomManager(session.DomainZone());

			R2G_CreateRoom _R2G_CreateRoom = (R2G_CreateRoom) await ActorMessageSenderComponent.Instance.Call(roomSceneConfig.InstanceId, new G2R_CreateRoom()
			{
				PlayerId = playerId,
				BattleCfgId = battleCfgId,
				RoomType = RoomType,
				SubRoomType = SubRoomType,
			});

			response.Error = _R2G_CreateRoom.Error;
			response.Message = _R2G_CreateRoom.Message;
			response.RoomId = _R2G_CreateRoom.RoomId;

			if (response.Error == ET.ErrorCode.ERR_Success)
			{
				PlayerStatusComponent playerStatusComponent = player.GetComponent<PlayerStatusComponent>();
				playerStatusComponent.PlayerStatus = PlayerStatus.Room;
				playerStatusComponent.RoomId = _R2G_CreateRoom.RoomId;
				playerStatusComponent.RoomType = (RoomType)RoomType;
				playerStatusComponent.SubRoomType = (SubRoomType)SubRoomType;

				await playerStatusComponent.NoticeClient();
			}
			await ETTask.CompletedTask;
		}
	}
}