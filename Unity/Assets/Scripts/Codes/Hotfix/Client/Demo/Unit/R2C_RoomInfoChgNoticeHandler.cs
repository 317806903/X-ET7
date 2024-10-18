using System.Collections.Generic;
using System.Net.Mime;
using ET.Ability;

namespace ET.Client
{
	[MessageHandler(SceneType.Client)]
	public class R2C_RoomInfoChgNoticeHandler : AMHandler<R2C_RoomInfoChgNotice>
	{
		protected override async ETTask Run(Session session, R2C_RoomInfoChgNotice message)
		{
			Scene clientScene = session.DomainScene();
			Scene currentScene = session.DomainScene().CurrentScene();

			PlayerStatusComponent playerStatusComponent = ET.Client.PlayerStatusHelper.GetMyPlayerStatusComponent(clientScene);
			long roomId = playerStatusComponent.RoomId;
			(bool roomExist, RoomComponent roomComponent) = await RoomHelper.GetRoomInfoAsync(clientScene, roomId);

			EventSystem.Instance.Publish(clientScene, new EventType.RoomInfoChg());

#if UNITY_EDITOR
			if (roomComponent.dynamicMapInstanceId > 0)
			{
				var instanceIdStruct = new InstanceIdStruct(roomComponent.dynamicMapInstanceId);
				Log.Error($"--zpb Process={instanceIdStruct.Process} Value={instanceIdStruct.Value}");
			}
#endif

			await ETTask.CompletedTask;
		}
	}
}
