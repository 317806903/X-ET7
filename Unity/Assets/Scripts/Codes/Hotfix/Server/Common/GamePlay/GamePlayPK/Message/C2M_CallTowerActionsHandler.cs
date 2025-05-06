using System;
using System.Collections.Generic;
using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_CallTowerActionsHandler : AMActorLocationRpcHandler<Unit, C2M_CallTowerActions, M2C_CallTowerActions>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_CallTowerActions request, M2C_CallTowerActions response)
		{
			//Unit playerUnit = ET.GamePlayHelper.GetCurPlayerUnit(observerUnit);

			long towerUnitId = request.TowerUnitId;

			string addActionIds = request.AddActionIds;
			addActionIds = addActionIds.Replace(" ", "").Trim();
			List<string> createActionList = new();
			if (string.IsNullOrEmpty(addActionIds) == false)
			{
				var tmp1 = addActionIds.Split(new char[] { '，' ,'；', ',' ,';','|'}, StringSplitOptions.None);
				foreach (string tmp12 in tmp1)
				{
					if (ItemGiftCfgCategory.Instance.Contain(tmp12))
					{
						var actionIds = ItemGiftCfgCategory.Instance.Get(tmp12).ActionIds;
						createActionList.AddRange(actionIds);
					}
					else
					{
						createActionList.Add(tmp12);
					}
				}
			}

			Unit unit = Ability.UnitHelper.GetUnit(observerUnit.DomainScene(), towerUnitId);
			ET.GamePlayHelper.DoCreateActions(unit, createActionList).Coroutine();
			await ETTask.CompletedTask;
		}
	}
}