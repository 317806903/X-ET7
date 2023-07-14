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

			clientScene.GetComponent<PlayerComponent>().PlayerGameMode = EnumHelper.FromString<PlayerGameMode>(message.PlayerGameMode);
			clientScene.GetComponent<PlayerComponent>().PlayerStatus = EnumHelper.FromString<PlayerStatus>(message.PlayerStatus);
			clientScene.GetComponent<PlayerComponent>().RoomId = message.RoomId;

			await ETTask.CompletedTask;
		}
	}
}
