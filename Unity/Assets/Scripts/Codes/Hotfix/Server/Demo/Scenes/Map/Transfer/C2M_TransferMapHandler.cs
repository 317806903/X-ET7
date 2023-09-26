using System;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_TransferMapHandler : AMActorLocationRpcHandler<Unit, C2M_TransferMap, M2C_TransferMap>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_TransferMap request, M2C_TransferMap response)
		{
			await ETTask.CompletedTask;

			Unit playerUnit = ET.GamePlayHelper.GetPlayerUnit(observerUnit);

			string currentMap = observerUnit.DomainScene().Name;
			string toMap = null;
			if (currentMap == "Map1")
			{
				toMap = "Map2";
			}
			else
			{
				toMap = "Map1";
			}

			StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(observerUnit.DomainScene().Zone, toMap);

			TransferHelper.TransferAtFrameFinish(false, observerUnit.DomainScene(), observerUnit, startSceneConfig.InstanceId, toMap).Coroutine();
		}
	}
}