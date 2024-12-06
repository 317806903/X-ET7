using System;
using System.Collections.Generic;
using ET.Ability;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_CallTowerHandler : AMActorLocationRpcHandler<Unit, C2M_CallTower, M2C_CallTower>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_CallTower request, M2C_CallTower response)
		{
			//Unit playerUnit = ET.GamePlayHelper.GetCurPlayerUnit(observerUnit);

			string towerUnitCfgId = request.TowerUnitCfgId;
			float3 position = request.Position;
			// position = ET.RecastHelper.GetHitNavmeshPos(observerUnit.DomainScene(), position);
			// if (position.Equals(float3.zero))
			// {
			// 	response.Error = ErrorCode.ERR_LogicError;
			// 	response.Message = "not found position";
			// 	return;
			// }

			string createActionIds = request.CreateActionIds;
			List<string> createActionList = new();
			if (string.IsNullOrEmpty(createActionIds) == false)
			{
				var tmp1 = createActionIds.Split(new char[] { ',' ,';','|'}, StringSplitOptions.None);
				createActionList.AddRange(tmp1);
			}

			List<Unit> unitList = ET.GamePlayPKHelper.CreateTower(observerUnit.DomainScene(), observerUnit.Id, towerUnitCfgId, position, false);
			foreach (Unit unit in unitList)
			{
				ET.GamePlayHelper.DoCreateActions(unit, createActionList).Coroutine();
			}
			await ETTask.CompletedTask;
		}
	}
}