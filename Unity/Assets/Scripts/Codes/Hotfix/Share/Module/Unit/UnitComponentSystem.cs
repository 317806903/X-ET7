using ET.Ability;
using System;

namespace ET
{
    [FriendOf(typeof(UnitComponent))]
    public static class UnitComponentSystem
	{
		[ObjectSystem]
		public class UnitComponentAwakeSystem : AwakeSystem<UnitComponent>
		{
			protected override void Awake(UnitComponent self)
			{
				self.playerList = HashSetComponent<long>.Create();
				self.monsterList = HashSetComponent<long>.Create();
				self.npcList = HashSetComponent<long>.Create();
				self.sceneObjList = HashSetComponent<long>.Create();
				self.bulletList = HashSetComponent<long>.Create();
			}
		}
	
		[ObjectSystem]
		public class UnitComponentDestroySystem : DestroySystem<UnitComponent>
		{
			protected override void Destroy(UnitComponent self)
			{
				self.playerList.Dispose();
				self.monsterList.Dispose();
				self.npcList.Dispose();
				self.sceneObjList.Dispose();
				self.bulletList.Dispose();
			}
		}

		[ObjectSystem]
		public class UnitComponentFixedUpdateSystem: FixedUpdateSystem<UnitComponent>
		{
			protected override void FixedUpdate(UnitComponent self)
			{
				self.FixedUpdate();
			}
		}

		public static void FixedUpdate(this Unit self, float fixedDeltaTime)
		{
			self.GetComponent<TimelineComponent>()?.FixedUpdate(fixedDeltaTime);
			self.GetComponent<BuffComponent>()?.FixedUpdate(fixedDeltaTime);
			self.GetComponent<SkillComponent>()?.FixedUpdate(fixedDeltaTime);
			self.GetComponent<BulletObj>()?.FixedUpdate(fixedDeltaTime);
			self.GetComponent<AoeObj>()?.FixedUpdate(fixedDeltaTime);
			self.GetComponent<EffectComponent>()?.FixedUpdate(fixedDeltaTime);
			self.GetComponent<MoveComponent>()?.FixedUpdate(fixedDeltaTime);
			self.GetComponent<RotateComponent>()?.FixedUpdate(fixedDeltaTime);
		}

		public static void FixedUpdate(this UnitComponent self)
		{
			float fixedDeltaTime = TimeHelper.FixedDetalTime;
			foreach (var child in self.Children)
			{
				Unit unit = child.Value as Unit;
				unit.FixedUpdate(fixedDeltaTime);
			}
		}
		
		public static HashSetComponent<long> GetRecordList(this UnitComponent self, UnitType unitType)
		{
			HashSetComponent<long> recordList;
			switch (unitType)
			{
				case UnitType.Player:
					recordList = self.playerList;
					break;
				case UnitType.Monster:
					recordList = self.monsterList;
					break;
				case UnitType.NPC:
					recordList = self.npcList;
					break;
				case UnitType.SceneObj:
					recordList = self.sceneObjList;
					break;
				case UnitType.Bullet:
					recordList = self.bulletList;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			return recordList;
		}

		public static void Add(this UnitComponent self, Unit unit)
		{
			HashSetComponent<long> recordList = self.GetRecordList(unit.Type);
			recordList.Add(unit.Id);
		}

		public static Unit Get(this UnitComponent self, long id)
		{
			Unit unit = self.GetChild<Unit>(id);
			return unit;
		}

		public static void Remove(this UnitComponent self, long id)
		{
			Unit unit = self.GetChild<Unit>(id);
			if (unit == null)
			{
				return;
			}
			HashSetComponent<long> recordList = self.GetRecordList(unit.Type);
			recordList.Remove(unit.Id);
			unit?.Dispose();
		}
	}
}