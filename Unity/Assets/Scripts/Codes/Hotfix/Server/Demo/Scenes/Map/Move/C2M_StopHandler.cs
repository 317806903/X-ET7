﻿namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_StopHandler : AMActorLocationHandler<Unit, C2M_Stop>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_Stop message)
		{
			Unit playerUnit = ET.GamePlayHelper.GetCurPlayerUnit(observerUnit);

			playerUnit.Stop(WaitTypeError.Destroy);
			await ETTask.CompletedTask;
		}
	}
}