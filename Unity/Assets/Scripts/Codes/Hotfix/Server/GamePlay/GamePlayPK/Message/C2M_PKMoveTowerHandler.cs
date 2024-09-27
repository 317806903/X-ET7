using ET.Ability;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_PKMoveTowerHandler : AMActorLocationRpcHandler<Unit, C2M_PKMoveTower, M2C_PKMoveTower>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_PKMoveTower request, M2C_PKMoveTower response)
		{
			//Unit playerUnit = ET.GamePlayHelper.GetCurPlayerUnit(observerUnit);

			long playerId = observerUnit.Id;

			long towerUnitId = request.TowerUnitId;
			float3 position = request.Position;

			Unit curTownUnit = ET.Ability.UnitHelper.GetUnit(observerUnit.DomainScene(), towerUnitId);
			ET.Ability.UnitHelper.ResetPos(curTownUnit, position, float3.zero);

			await ETTask.CompletedTask;
		}
	}
}