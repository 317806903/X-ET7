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
			Unit playerUnit = ET.GamePlayHelper.GetPlayerUnit(observerUnit);

			string monsterUnitCfgId = request.MonsterUnitCfgId;
			float3 position = request.Position;
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
				Unit monsterUnit = ET.GamePlayPKHelper.CreateMonster(observerUnit.DomainScene(), monsterUnitCfgId, 1, position, forward);
				ET.GamePlayHelper.DoCreateActions(monsterUnit, createActionList);
			}

			await ETTask.CompletedTask;
		}
	}
}