using System.Collections.Generic;
using ET.Ability;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_ClearAllMonsterHandler : AMActorLocationRpcHandler<Unit, C2M_ClearAllMonster, M2C_ClearAllMonster>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_ClearAllMonster request, M2C_ClearAllMonster response)
		{
			//Unit playerUnit = ET.GamePlayHelper.GetCurPlayerUnit(observerUnit);
			long playerId = observerUnit.Id;

			UnitComponent unitComponent = Ability.UnitHelper.GetUnitComponent(observerUnit);
			foreach (Unit unit in unitComponent.GetRecordList(UnitType.ActorUnit))
			{
				TowerComponent towerComponent = unit.GetComponent<TowerComponent>();
				if (towerComponent != null)
				{
					if (towerComponent.playerId != playerId)
					{
						unit.DestroyNotDeathShow();
					}
				}
				else
				{
					unit.DestroyNotDeathShow();
				}
			}

			await ETTask.CompletedTask;
		}
	}
}