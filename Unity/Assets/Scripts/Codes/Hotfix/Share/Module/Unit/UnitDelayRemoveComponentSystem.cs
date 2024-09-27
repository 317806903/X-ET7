using ET.Ability;
using System;
using System.Collections.Generic;

namespace ET
{
    [FriendOf(typeof(UnitDelayRemoveComponent))]
    [FriendOf(typeof(Unit))]
    public static class UnitDelayRemoveComponentSystem
	{
		[ObjectSystem]
		public class UnitDelayRemoveComponentAwakeSystem : AwakeSystem<UnitDelayRemoveComponent>
		{
			protected override void Awake(UnitDelayRemoveComponent self)
			{
				self.unitList = new();
				self.unitRemoveTimeList = new();
			}
		}

		[ObjectSystem]
		public class UnitDelayRemoveComponentDestroySystem : DestroySystem<UnitDelayRemoveComponent>
		{
			protected override void Destroy(UnitDelayRemoveComponent self)
			{
				self.unitList.Clear();
				self.unitRemoveTimeList.Clear();
			}
		}

		[ObjectSystem]
		public class UnitDelayRemoveComponentFixedUpdateSystem: FixedUpdateSystem<UnitDelayRemoveComponent>
		{
			protected override void FixedUpdate(UnitDelayRemoveComponent self)
			{
				if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
				{
					return;
				}

				float fixedDeltaTime = TimeHelper.FixedDetalTime;
				self.FixedUpdate(fixedDeltaTime);
			}
		}

		public static void FixedUpdate(this UnitDelayRemoveComponent self, float fixedDeltaTime)
		{
			if (++self.curFrameRemove >= self.waitFrameRemove)
			{
				self.curFrameRemove = 0;

				bool bContinue = false;
				do
				{
					bContinue = self.DoRemoveUnit();
				}
				while (bContinue);
			}
		}

		public static bool DoRemoveUnit(this UnitDelayRemoveComponent self)
		{
			if (self.unitList.Count == 0)
			{
				return false;
			}

			long removeTime = self.unitRemoveTimeList.Peek();
			if (removeTime > TimeHelper.ServerNow())
			{
				return false;
			}
			self.unitRemoveTimeList.Dequeue();

			long unitId = self.unitList.Dequeue();
			self.RemoveChild(unitId);

			return true;
		}

		public static Unit Get(this UnitDelayRemoveComponent self, long id)
		{
			Unit unit = self.GetChild<Unit>(id);
			return unit;
		}

		public static void AddRemoveUnit(this UnitDelayRemoveComponent self, Unit unit)
		{
			if (UnitHelper.ChkIsBullet(unit) || UnitHelper.ChkIsAoe(unit))
			{
				Unit unitRemove = self.AddChildWithId<Unit, string>(unit.Id, unit.CfgId);
				unitRemove.level = unit.level;
				unitRemove.Type = unit.Type;
				unitRemove.Position = unit.Position;
				unitRemove.Forward = unit.Forward;

				self.unitList.Enqueue(unitRemove.Id);
				self.unitRemoveTimeList.Enqueue(TimeHelper.ServerNow() + 5 * 1000);
			}
			else if (UnitHelper.ChkIsPlayer(unit)
			         || UnitHelper.ChkIsCameraPlayer(unit)
			         || UnitHelper.ChkIsActor(unit))
			{
				Unit unitRemove = self.AddChildWithId<Unit, string>(unit.Id, unit.CfgId);
				unitRemove.level = unit.level;
				unitRemove.Type = unit.Type;
				unitRemove.Position = unit.Position;
				unitRemove.Forward = unit.Forward;

				NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
				NumericComponent numericComponentRemove = unitRemove.AddComponent<NumericComponent>();
				ET.Ability.UnitHelper_Create.CopyUnitNumeric(numericComponent, numericComponentRemove);
				numericComponentRemove.NumericDic[NumericType.Hp] = 0;

				self.unitList.Enqueue(unitRemove.Id);
				if (unit.GetComponent<TowerComponent>() != null)
				{
					self.unitRemoveTimeList.Enqueue(TimeHelper.ServerNow() + self.towerNextRemoveTime * 1000);
				}
				else
				{
					self.unitRemoveTimeList.Enqueue(TimeHelper.ServerNow() + self.actorNextRemoveTime * 1000);
				}
			}

		}

	}
}