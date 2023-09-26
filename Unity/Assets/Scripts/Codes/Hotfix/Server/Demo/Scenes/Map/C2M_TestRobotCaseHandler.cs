using System;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_TestRobotCaseHandler : AMActorLocationRpcHandler<Unit, C2M_TestRobotCase, M2C_TestRobotCase>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_TestRobotCase request, M2C_TestRobotCase response)
		{
			Unit playerUnit = ET.GamePlayHelper.GetPlayerUnit(observerUnit);

			response.N = request.N;
			await ETTask.CompletedTask;
		}
	}
}