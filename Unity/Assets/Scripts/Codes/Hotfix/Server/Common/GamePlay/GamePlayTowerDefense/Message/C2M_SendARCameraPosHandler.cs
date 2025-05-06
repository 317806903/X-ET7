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
			//Unit playerUnit = ET.GamePlayHelper.GetCurPlayerUnit(observerUnit);
			Unit cameraPlayerUnit = ET.GamePlayHelper.GetCameraPlayerUnit(observerUnit);

			long playerId = observerUnit.Id;
			float3 ARCameraPosition = message.ARCameraPosition;
			cameraPlayerUnit.Position = ARCameraPosition;

			CameraPlayerUnitComponent cameraPlayerUnitComponent = cameraPlayerUnit.GetComponent<CameraPlayerUnitComponent>();
			if (cameraPlayerUnitComponent != null)
			{
				foreach (long playerSkillUnitId in cameraPlayerUnitComponent.skillIndex2PlayerSkillUnitId.Values)
				{
					Unit playerSkillUnit = Ability.UnitHelper.GetUnit(cameraPlayerUnit.DomainScene(), playerSkillUnitId);
					playerSkillUnit.Position = ARCameraPosition;
				}
			}


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