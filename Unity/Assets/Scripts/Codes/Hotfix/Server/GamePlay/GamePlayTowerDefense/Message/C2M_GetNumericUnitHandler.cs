using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_GetNumericUnitHandler : AMActorLocationHandler<Unit, C2M_GetNumericUnit>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_GetNumericUnit message)
		{
			//Unit playerUnit = ET.GamePlayHelper.GetCurPlayerUnit(observerUnit);

			long playerId = observerUnit.Id;
			List<long> UnitIdList = message.UnitIdList;
			List<int> NumericKeyList = message.NumericKeyList;

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