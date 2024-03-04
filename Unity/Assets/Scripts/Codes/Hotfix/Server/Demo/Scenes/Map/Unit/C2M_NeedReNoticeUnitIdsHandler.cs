
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_NeedReNoticeUnitIdsHandler : AMActorLocationHandler<Unit, C2M_NeedReNoticeUnitIds>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_NeedReNoticeUnitIds message)
		{
			// Unit playerUnit = ET.GamePlayHelper.GetPlayerUnit(observerUnit);
			// if (playerUnit == null)
			// {
			// 	return;
			// }

			List<long> unitIds = message.UnitIds;
			if (unitIds == null)
			{
				return;
			}
			for (int i = 0; i < unitIds.Count; i++)
			{
				Unit unit = ET.Ability.UnitHelper.GetUnit(observerUnit.DomainScene(), unitIds[i]);
				ET.Ability.UnitHelper.AddSyncNoticeUnitAdd(observerUnit, unit);
			}

			await ETTask.CompletedTask;
		}
	}
}