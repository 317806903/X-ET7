using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
	[Invoke(TimerInvokeType.GamePlayChkHomeAlive)]
	public class PutHomeComponentTimer: ATimer<PutHomeComponent>
	{
		protected override void Run(PutHomeComponent self)
		{
			try
			{
				self.ChkHomeAlive();
			}
			catch (Exception e)
			{
				Log.Error($"PutHomeComponentTimer timer error: {self.Id}\n{e}");
			}
		}
	}

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
				TimerComponent.Instance.Remove(ref self.Timer);
			}
		}

		public static void Init(this PutHomeComponent self, string unitCfgId, float3 homePos)
		{
			self.HomePos = homePos;
			self.CreateHome(unitCfgId);
			
			self.Timer = TimerComponent.Instance.NewRepeatedTimer(500, TimerInvokeType.GamePlayChkHomeAlive, self);
		}

		public static void CreateHome(this PutHomeComponent self, string unitCfgId)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
			int hp = gamePlayTowerDefenseComponent.model.HomeLife;

			self.HomeUnit = GamePlayTowerDefenseHelper.CreateHome(self.DomainScene(), unitCfgId, self.HomePos, hp);
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
		
		public static bool ChkHomeAlive(this PutHomeComponent self)
		{
			if (ET.Ability.UnitHelper.ChkUnitAlive(self.HomeUnit))
			{
				return true;
			}

			self.GetParent<GamePlayTowerDefenseComponent>().TransToGameFailed();
			TimerComponent.Instance.Remove(ref self.Timer);
			return false;
		}
	}
}