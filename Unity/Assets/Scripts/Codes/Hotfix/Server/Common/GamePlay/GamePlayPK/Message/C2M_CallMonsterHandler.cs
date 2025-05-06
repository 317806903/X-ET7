using System;
using System.Collections.Generic;
using ET.Ability;
using ET.AbilityConfig;
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
			float3 forward = request.Forward;

			int count = request.Count;
			string createActionIds = request.CreateActionIds;
			createActionIds = createActionIds.Replace(" ", "").Trim();
			List<string> createActionList = new();
			if (string.IsNullOrEmpty(createActionIds) == false)
			{
				var tmp1 = createActionIds.Split(new char[] { '，' ,'；',',' ,';','|'}, StringSplitOptions.None);
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
			for (int i = 0; i < count; i++)
			{
				if (isMonster)
				{
					Unit monsterUnit = ET.GamePlayPKHelper.CreateMonster(observerUnit.DomainScene(), itemCfgId, 1, position, forward);
					ET.GamePlayHelper.DoCreateActions(monsterUnit, createActionList).Coroutine();
				}

				if (isTower)
				{
					List<Unit> unitList = ET.GamePlayPKHelper.CreateTower(observerUnit.DomainScene(), observerUnit.Id, itemCfgId, position, forward, true);
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