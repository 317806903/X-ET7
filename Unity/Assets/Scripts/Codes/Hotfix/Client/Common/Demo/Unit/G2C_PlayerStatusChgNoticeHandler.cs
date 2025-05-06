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

			byte[] byts = message.PlayerStatusComponentBytes;
			Entity entity = MongoHelper.Deserialize<Entity>(byts);
			PlayerStatusHelper.RefreshMyPlayerStatus(clientScene, entity);

			await ETTask.CompletedTask;
		}
	}
}
