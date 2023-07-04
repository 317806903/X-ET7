using ET.Ability;
using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof(PutMonsterCallComponent))]
    public static class PutMonsterCallComponentSystem
	{
		[ObjectSystem]
		public class PutMonsterCallComponentAwakeSystem : AwakeSystem<PutMonsterCallComponent>
		{
			protected override void Awake(PutMonsterCallComponent self)
			{
			}
		}
	
		[ObjectSystem]
		public class PutMonsterCallComponentDestroySystem : DestroySystem<PutMonsterCallComponent>
		{
			protected override void Destroy(PutMonsterCallComponent self)
			{
			}
		}

		public static void Init(this PutMonsterCallComponent self, string unitCfgId, float3 monsterCallPos)
		{
			self.MonsterCallPos = monsterCallPos;
			self.CreateMonsterCall(unitCfgId);
		}

		public static void CreateMonsterCall(this PutMonsterCallComponent self, string unitCfgId)
		{
			float3 forward = new float3(0, 0, 1);
			Unit monsterCallUnit = UnitHelper_Create.CreateWhenServer_MonsterCallUnit(self.DomainScene(), unitCfgId, self.MonsterCallPos, forward);
			self.MonsterCallUnit = monsterCallUnit;
		}

		public static Unit GetMonsterCallUnit(this PutMonsterCallComponent self)
		{
			return self.MonsterCallUnit;
		}

		public static float3 GetPosition(this PutMonsterCallComponent self)
		{
			if (self.MonsterCallUnit != null)
			{
				return self.MonsterCallUnit.Position;
			}
			return self.MonsterCallPos;
		}

	}
}