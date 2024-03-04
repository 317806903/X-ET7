using System;
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class L2A_RemoveObjectLocationRequest_Map : AMActorRpcHandler<Scene, L2A_RemoveObjectLocationRequest, L2A_RemoveObjectLocationResponse>
	{
		protected override async ETTask Run(Scene scene, L2A_RemoveObjectLocationRequest request, L2A_RemoveObjectLocationResponse response)
		{
			int type = request.Type;
			long key = request.Key;

			ActorLocationSenderComponent.Instance?.Get(type).Remove(key);

			await ETTask.CompletedTask;
		}
	}
}