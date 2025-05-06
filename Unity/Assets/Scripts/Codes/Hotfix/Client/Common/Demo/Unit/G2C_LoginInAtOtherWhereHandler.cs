using System;
using System.Collections.Generic;
using ET.Ability;

namespace ET.Client
{
	[MessageHandler(SceneType.Client)]
	public class G2C_LoginInAtOtherWhereHandler : AMHandler<G2C_LoginInAtOtherWhere>
	{
		protected override async ETTask Run(Session session, G2C_LoginInAtOtherWhere message)
		{
			Scene clientScene = session.DomainScene();

			EventSystem.Instance.Publish(clientScene, new ClientEventType.NoticeUILoginInAtOtherWhere());

			await ETTask.CompletedTask;
		}
	}
}
