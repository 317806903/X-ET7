using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_GetNumericUnitHandler : AMActorLocationRpcHandler<Unit, C2M_GetNumericUnit, M2C_GetNumericUnit>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_GetNumericUnit request, M2C_GetNumericUnit response)
		{
			Unit playerUnit = ET.GamePlayHelper.GetPlayerUnit(observerUnit);

			long playerId = observerUnit.Id;
			List<long> UnitIdList = request.UnitIdList;
			List<int> NumericKeyList = request.NumericKeyList;

			for (int i = 0; i < UnitIdList.Count; i++)
			{
				long unitId = UnitIdList[i];
				Unit unit = ET.Ability.UnitHelper.GetUnit(observerUnit.DomainScene(), unitId);
				if (NumericKeyList == null || NumericKeyList.Count == 0)
				{
					Ability.UnitHelper.AddSyncNumericUnit(unit);
				}
				else
				{
					foreach (int key in NumericKeyList)
					{
						Ability.UnitHelper.AddSyncNumericUnitByKey(unit, key);
					}
				}
			}

			await ETTask.CompletedTask;
		}

	}
}