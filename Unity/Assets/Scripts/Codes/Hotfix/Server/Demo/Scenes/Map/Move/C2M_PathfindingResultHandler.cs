
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_PathfindingResultHandler : AMActorLocationHandler<Unit, C2M_PathfindingResult>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_PathfindingResult message)
		{
			Unit playerUnit = ET.GamePlayHelper.GetPlayerUnit(observerUnit);
			if (playerUnit == null)
			{
				return;
			}

			// Unit myUnit = unit;
			// myUnit.GetComponent<PathfindingComponent>().NavMesh.OnRaycast(message.Position + new float3(0, 5000, 0), message.Position - new float3(0, 5000, 0));

			ET.Ability.MoveOrIdleHelper.DoMoveTargetPosition(playerUnit, message.Position).Coroutine();
			await ETTask.CompletedTask;
		}
	}
}