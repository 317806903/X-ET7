using System.Collections.Generic;
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

			PlayerComponent playerStatusComponent = clientScene.GetComponent<PlayerComponent>();
			long roomId = playerStatusComponent.RoomId;
			await RoomHelper.GetRoomInfoAsync(clientScene, roomId);
			
			EventSystem.Instance.Publish(clientScene, new EventType.RoomInfoChg());

			await ETTask.CompletedTask;
		}
	}
}
