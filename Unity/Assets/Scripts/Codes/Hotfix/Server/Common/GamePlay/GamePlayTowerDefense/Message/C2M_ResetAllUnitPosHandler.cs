using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_ResetAllUnitPosHandler : AMActorLocationRpcHandler<Unit, C2M_ResetAllUnitPos, M2C_ResetAllUnitPos>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_ResetAllUnitPos request, M2C_ResetAllUnitPos response)
		{
			//Unit playerUnit = ET.GamePlayHelper.GetCurPlayerUnit(observerUnit);

			long playerId = observerUnit.Id;

			UnitComponent unitComponent = Ability.UnitHelper.GetUnitComponent(observerUnit);
			MultiMapSetSimple<UnitType, Unit> list = unitComponent.GetRecordListAll();
			if (list == null)
			{
				return;
			}

			foreach (var tmp in list)
			{
				foreach (Unit unit in tmp.Value)
				{
					Ability.UnitHelper.AddSyncData_UnitPosInfo(unit);
				}
			}
			await ETTask.CompletedTask;
		}

	}
}