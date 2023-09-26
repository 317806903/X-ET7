using System;
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class R2M_DestroyDynamicMapHandler : AMActorRpcHandler<Scene, R2M_DestroyDynamicMap, M2R_DestroyDynamicMap>
	{
		protected override async ETTask Run(Scene scene, R2M_DestroyDynamicMap request, M2R_DestroyDynamicMap response)
		{
			DynamicMapManagerComponent dynamicMapManagerComponent = scene.GetComponent<DynamicMapManagerComponent>();
			if (dynamicMapManagerComponent == null)
			{
				return;
			}

			await dynamicMapManagerComponent.DestroyDynamicMap(request.DynamicMapInstanceId);

			await ETTask.CompletedTask;
		}
	}
}