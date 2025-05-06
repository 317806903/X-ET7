using ET.Ability;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_PKMovePlayerHandler : AMActorLocationRpcHandler<Unit, C2M_PKMovePlayer, M2C_PKMovePlayer>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_PKMovePlayer request, M2C_PKMovePlayer response)
		{
			//Unit playerUnit = ET.GamePlayHelper.GetCurPlayerUnit(observerUnit);

			long playerId = observerUnit.Id;

			long towerUnitId = request.TowerUnitId;
			float3 position = request.Position;
			float3 forward = request.Forward;

			Unit curTownUnit = ET.Ability.UnitHelper.GetUnit(observerUnit.DomainScene(), towerUnitId);
			ET.Ability.UnitHelper.ResetPos(curTownUnit, position, forward);

			await ETTask.CompletedTask;
		}
	}
}