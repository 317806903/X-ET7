using System.Collections.Generic;
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
			float3 forward = request.Forward;

			Unit curTownUnit = ET.Ability.UnitHelper.GetUnit(observerUnit.DomainScene(), towerUnitId);

			GamePlayPkComponent gamePlayPkComponent = ET.GamePlayHelper.GetGamePlayPk(observerUnit.DomainScene());
			HashSet<Unit> downTowerList = gamePlayPkComponent.GetTowerListWhenStackedOnTop(curTownUnit);
			float3 dis = position - curTownUnit.Position;

			ET.Ability.UnitHelper.ResetPos(curTownUnit, position, forward);

			foreach (Unit towerUnit in downTowerList)
			{
				Ability.UnitHelper.ResetPos(towerUnit, towerUnit.Position + dis, float3.zero);
			}

			await ETTask.CompletedTask;
		}
	}
}