using System.Collections.Generic;
using ET.Ability;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_ClearMyTowerHandler : AMActorLocationRpcHandler<Unit, C2M_ClearMyTower, M2C_ClearMyTower>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_ClearMyTower request, M2C_ClearMyTower response)
		{
			//Unit playerUnit = ET.GamePlayHelper.GetCurPlayerUnit(observerUnit);
			long playerId = observerUnit.Id;
			long towerUnitId = request.TowerUnitId;

			UnitComponent unitComponent = Ability.UnitHelper.GetUnitComponent(observerUnit);
			HashSet<Unit> list = unitComponent.GetRecordList(UnitType.ActorUnit);
			if (list == null)
			{
				return;
			}
			foreach (Unit unit in list)
			{
				TowerComponent towerComponent = unit.GetComponent<TowerComponent>();
				if (towerComponent != null)
				{
					if (towerComponent.playerId == playerId)
					{
						if (towerUnitId == -1 || towerUnitId == unit.Id)
						{
							unit.DestroyNotDeathShow();
						}
					}
				}
			}

			await ETTask.CompletedTask;
		}
	}
}