using System;
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class R2M_GetDynamicMapCountHandler : AMActorRpcHandler<Scene, R2M_GetDynamicMapCount, M2R_GetDynamicMapCount>
	{
		protected override async ETTask Run(Scene scene, R2M_GetDynamicMapCount request, M2R_GetDynamicMapCount response)
		{
			DynamicMapManagerComponent dynamicMapManagerComponent = scene.GetComponent<DynamicMapManagerComponent>();
			if (dynamicMapManagerComponent != null)
			{
				response.DynamicMapCount = dynamicMapManagerComponent.dynamicUsedIndexList.Count;
			}
			else
			{
				response.DynamicMapCount = 0;
			}

			await ETTask.CompletedTask;
		}
	}
}