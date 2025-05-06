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
						if (towerUnitId == -1)
						{
							unit.DestroyNotDeathShow();
						}
						else if (towerUnitId == unit.Id)
						{
							GamePlayPkComponent gamePlayPkComponent = ET.GamePlayHelper.GetGamePlayPk(observerUnit.DomainScene());
							HashSet<Unit> downTowerList = gamePlayPkComponent.GetTowerListWhenStackedOnTop(unit);
							float curUnitHeight = Ability.UnitHelper.GetBodyHeight(unit);
							foreach (Unit towerUnit in downTowerList)
							{
								Ability.UnitHelper.ResetPos(towerUnit, towerUnit.Position - new float3(0, curUnitHeight, 0), float3.zero);
							}

							unit.DestroyNotDeathShow();
						}
					}
				}
			}

			await ETTask.CompletedTask;
		}
	}
}