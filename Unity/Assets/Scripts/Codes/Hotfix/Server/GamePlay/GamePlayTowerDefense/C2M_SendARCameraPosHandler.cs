using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_SendARCameraPosHandler : AMActorLocationRpcHandler<Unit, C2M_SendARCameraPos, M2C_SendARCameraPos>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_SendARCameraPos request, M2C_SendARCameraPos response)
		{
			Unit playerUnit = ET.GamePlayHelper.GetPlayerUnit(observerUnit);

			long playerId = observerUnit.Id;
			float3 ARCameraPosition = request.ARCameraPosition;
			float3 ARCameraHitPosition = request.ARCameraHitPosition;

			if (ARCameraHitPosition.Equals(float3.zero))
			{
				return;
			}
			observerUnit.Position = (ARCameraPosition + ARCameraHitPosition) * 0.5f;

			await ETTask.CompletedTask;
		}

	}
}