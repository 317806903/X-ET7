using System;
using System.Collections.Generic;
using ET.Ability;

namespace ET.Client
{
	[MessageHandler(SceneType.Client)]
	public class G2C_BeKickMemberOutRoomHandler : AMHandler<G2C_BeKickMemberOutRoom>
	{
		protected override async ETTask Run(Session session, G2C_BeKickMemberOutRoom message)
		{
			Scene clientScene = session.DomainScene();

            EventSystem.Instance.Publish(clientScene, new EventType.BeKickedRoom());

			await ETTask.CompletedTask;
		}
	}
}
