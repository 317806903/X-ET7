using System;


namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_GetRoomListHandler : AMRpcHandler<C2G_GetRoomList, G2C_GetRoomList>
	{
		protected override async ETTask Run(Session session, C2G_GetRoomList request, G2C_GetRoomList response)
		{
			Player player = session.GetComponent<SessionPlayerComponent>().Player;
			long playerId = player.Id;
			int isARRoom = request.IsARRoom;

			StartSceneConfig roomSceneConfig = StartSceneConfigCategory.Instance.GetRoomManager(session.DomainZone());

			R2G_GetRoomList _R2G_GetRoomList = (R2G_GetRoomList) await ActorMessageSenderComponent.Instance.Call(roomSceneConfig.InstanceId, new G2R_GetRoomList()
			{
				IsARRoom = isARRoom,
			});
			
			response.Error = _R2G_GetRoomList.Error;
			response.Message = _R2G_GetRoomList.Message;
			response.RoomInfos = _R2G_GetRoomList.RoomInfos;

			await ETTask.CompletedTask;
		}
	}
}