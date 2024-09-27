
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_PathfindingResultHandler : AMActorLocationHandler<Unit, C2M_PathfindingResult>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_PathfindingResult message)
		{
			Unit playerUnit = ET.GamePlayHelper.GetCurPlayerUnit(observerUnit);
			if (playerUnit == null)
			{
				return;
			}

			ET.Ability.MoveOrIdleHelper.DoMoveTargetPosition(playerUnit, message.Position).Coroutine();
			await ETTask.CompletedTask;
		}
	}
}