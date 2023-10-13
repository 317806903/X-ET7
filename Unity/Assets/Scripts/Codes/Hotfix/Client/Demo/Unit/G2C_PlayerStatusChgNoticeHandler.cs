using System;
using System.Collections.Generic;
using ET.Ability;

namespace ET.Client
{
	[MessageHandler(SceneType.Client)]
	public class G2C_PlayerStatusChgNoticeHandler : AMHandler<G2C_PlayerStatusChgNotice>
	{
		protected override async ETTask Run(Session session, G2C_PlayerStatusChgNotice message)
		{
			Scene clientScene = session.DomainScene();

			PlayerComponent playerComponent = ET.Client.PlayerHelper.GetMyPlayerComponent(clientScene);
			playerComponent.PlayerGameMode = EnumHelper.FromString<PlayerGameMode>(message.PlayerGameMode);
			playerComponent.PlayerStatus = EnumHelper.FromString<PlayerStatus>(message.PlayerStatus);
			playerComponent.ARRoomType = EnumHelper.FromString<ARRoomType>(message.ARRoomType);
			playerComponent.RoomId = message.RoomId;

			await ETTask.CompletedTask;
		}
	}
}
