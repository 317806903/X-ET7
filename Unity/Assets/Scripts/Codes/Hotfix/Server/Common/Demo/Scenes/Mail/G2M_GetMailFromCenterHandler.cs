using System;
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Mail)]
	public class G2M_GetMailFromCenterHandler : AMActorRpcHandler<Scene, G2M_GetMailFromCenter, M2G_GetMailFromCenter>
	{
		protected override async ETTask Run(Scene scene, G2M_GetMailFromCenter request, M2G_GetMailFromCenter response)
		{
			long playerId = request.PlayerId;

			MailManagerComponent mailManagerComponent = ET.Server.MailHelper.GetMailManager(scene);
			List<byte[]> componentBytes = mailManagerComponent.GetPlayerMailFromCenter(playerId);
			response.ComponentBytes = componentBytes;

			await ETTask.CompletedTask;
		}
	}
}