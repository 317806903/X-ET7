using ET.Ability;
using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof(PutHomeComponent))]
    public static class PutHomeComponentSystem
	{
		[ObjectSystem]
		public class PutHomeComponentAwakeSystem : AwakeSystem<PutHomeComponent>
		{
			protected override void Awake(PutHomeComponent self)
			{
			}
		}
	
		[ObjectSystem]
		public class PutHomeComponentDestroySystem : DestroySystem<PutHomeComponent>
		{
			protected override void Destroy(PutHomeComponent self)
			{
			}
		}

		public static void Init(this PutHomeComponent self, string unitCfgId, float3 homePos)
		{
			self.HomePos = homePos;
			self.CreateHome(unitCfgId);
		}

		public static void CreateHome(this PutHomeComponent self, string unitCfgId)
		{
			TeamFlagType teamFlagType = TeamFlagType.TeamGlobal1;
			float3 forward = new float3(0, 0, 1);
			Unit headQuarterUnit = UnitHelper_Create.CreateWhenServer_HeadQuarterUnit(self.DomainScene(), unitCfgId, teamFlagType, self.HomePos, forward);
			self.HomeUnit = headQuarterUnit;
		}
		
		public static Unit GetHomeUnit(this PutHomeComponent self)
		{
			return self.HomeUnit;
		}
		
		public static float3 GetPosition(this PutHomeComponent self)
		{
			if (self.HomeUnit != null)
			{
				return self.HomeUnit.Position;
			}
			return self.HomePos;
		}
	}
}