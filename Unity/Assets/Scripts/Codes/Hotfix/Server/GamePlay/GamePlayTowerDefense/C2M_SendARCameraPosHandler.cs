using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_SendARCameraPosHandler : AMActorLocationHandler<Unit, C2M_SendARCameraPos>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_SendARCameraPos message)
		{
			Unit playerUnit = ET.GamePlayHelper.GetPlayerUnit(observerUnit);

			long playerId = observerUnit.Id;
			float3 ARCameraPosition = message.ARCameraPosition;
			float3 ARCameraHitPosition = message.ARCameraHitPosition;

			if (ARCameraHitPosition.Equals(float3.zero))
			{
				return;
			}
			observerUnit.Position = (ARCameraPosition + ARCameraHitPosition) * 0.5f;

			await ETTask.CompletedTask;
		}

	}
}