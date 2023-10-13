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
			Unit playerUnit = ET.GamePlayHelper.GetPlayerUnit(observerUnit);
			long playerId = observerUnit.Id;

			UnitComponent unitComponent = Ability.UnitHelper.GetUnitComponent(observerUnit);
			foreach (Unit unit in unitComponent.actorList)
			{
				TowerComponent towerComponent = unit.GetComponent<TowerComponent>();
				if (towerComponent != null)
				{
					if (towerComponent.playerId == playerId)
					{
						unit.DestroyNotDeathShow();
					}
				}
			}

			await ETTask.CompletedTask;
		}
	}
}