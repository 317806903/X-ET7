using System;
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Mail)]
	public class G2M_InsertMailToCenterHandler : AMActorRpcHandler<Scene, G2M_InsertMailToCenter, M2G_InsertMailToCenter>
	{
		protected override async ETTask Run(Scene scene, G2M_InsertMailToCenter request, M2G_InsertMailToCenter response)
		{
			byte[] componentBytes = request.ComponentBytes;

			MailManagerComponent mailManagerComponent = ET.Server.MailHelper.GetMailManager(scene);
			mailManagerComponent.InsertMailToCenter(componentBytes);

			await ETTask.CompletedTask;
		}
	}
}