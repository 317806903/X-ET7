using System;
using System.Collections.Generic;
using ET.Ability;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_CallMonsterHandler : AMActorLocationRpcHandler<Unit, C2M_CallMonster, M2C_CallMonster>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_CallMonster request, M2C_CallMonster response)
		{
			//Unit playerUnit = ET.GamePlayHelper.GetCurPlayerUnit(observerUnit);

			string itemCfgId = request.MonsterUnitCfgId;

			bool isMonster = ET.ItemHelper.ChkIsMonster(itemCfgId);
			bool isTower = ET.ItemHelper.ChkIsTower(itemCfgId);

			float3 position = request.Position;
			position = ET.RecastHelper.GetHitNavmeshPos(observerUnit.DomainScene(), position);
			if (position.Equals(float3.zero))
			{
				response.Error = ErrorCode.ERR_LogicError;
				response.Message = "not found position";
				return;
			}

			int count = request.Count;
			string createActionIds = request.CreateActionIds;
			List<string> createActionList = new();
			if (string.IsNullOrEmpty(createActionIds) == false)
			{
				var tmp1 = createActionIds.Split(new char[] { ',' ,';','|'}, StringSplitOptions.None);
				createActionList.AddRange(tmp1);
			}
			for (int i = 0; i < count; i++)
			{
				float3 forward = new float3(0, 0, 1);
				if (isMonster)
				{
					Unit monsterUnit = ET.GamePlayPKHelper.CreateMonster(observerUnit.DomainScene(), itemCfgId, 1, position, forward);
					ET.GamePlayHelper.DoCreateActions(monsterUnit, createActionList).Coroutine();
				}

				if (isTower)
				{
					List<Unit> unitList = ET.GamePlayPKHelper.CreateTower(observerUnit.DomainScene(), observerUnit.Id, itemCfgId, position, true);
					foreach (Unit unit in unitList)
					{
						ET.GamePlayHelper.DoCreateActions(unit, createActionList).Coroutine();
					}
				}
			}

			await ETTask.CompletedTask;
		}
	}
}