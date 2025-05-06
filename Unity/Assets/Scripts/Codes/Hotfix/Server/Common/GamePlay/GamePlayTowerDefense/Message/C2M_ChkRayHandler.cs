using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_ChkRayHandler : AMActorLocationRpcHandler<Unit, C2M_ChkRay, M2C_ChkRay>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_ChkRay request, M2C_ChkRay response)
		{
			//Unit playerUnit = ET.GamePlayHelper.GetCurPlayerUnit(observerUnit);

			long playerId = observerUnit.Id;
			float3 startPosition = request.StartPosition;
			float3 endPosition = request.EndPosition;

			var(bRet, hitPos) = ET.RecastHelper.OnRaycast(observerUnit.DomainScene(), startPosition, endPosition);
			response.HitRet = bRet? 1 : 0;
			response.HitPosition = hitPos;

			await ETTask.CompletedTask;
		}

	}
}