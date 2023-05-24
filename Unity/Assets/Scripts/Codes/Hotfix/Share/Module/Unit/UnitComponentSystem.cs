using ET.Ability;
using System;
using System.Collections.Generic;

namespace ET
{
    [FriendOf(typeof(UnitComponent))]
    [FriendOf(typeof(Unit))]
    public static class UnitComponentSystem
	{
		[ObjectSystem]
		public class UnitComponentAwakeSystem : AwakeSystem<UnitComponent>
		{
			protected override void Awake(UnitComponent self)
			{
				self.NeedSyncUnits = new();
				self.waitRemoveList = new();
				self.playerList = HashSetComponent<Unit>.Create();
				self.monsterList = HashSetComponent<Unit>.Create();
				self.npcList = HashSetComponent<Unit>.Create();
				self.sceneObjList = HashSetComponent<Unit>.Create();
				self.bulletList = HashSetComponent<Unit>.Create();
			}
		}
	
		[ObjectSystem]
		public class UnitComponentDestroySystem : DestroySystem<UnitComponent>
		{
			protected override void Destroy(UnitComponent self)
			{
				self.NeedSyncUnits.Dispose();
				self.waitRemoveList.Clear();
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
			self.GetComponent<MoveTweenObj>()?.FixedUpdate(fixedDeltaTime);
		}

		public static void FixedUpdate(this UnitComponent self)
		{
			if (self.DomainScene().SceneType != SceneType.Map)
			{
				return;
			}
			
			float fixedDeltaTime = TimeHelper.FixedDetalTime;
			self.DoUnitHit(fixedDeltaTime);
			foreach (var child in self.Children)
			{
				Unit unit = child.Value as Unit;
				unit.FixedUpdate(fixedDeltaTime);
			}
			self.DoUnitRemove();
			self.SyncUnit();
		}

		public static void DoUnitRemove(this UnitComponent self)
		{
			for (int i = 0; i < self.waitRemoveList.Count; i++)
			{
				self.Remove(self.waitRemoveList[i]);
			}

			self.waitRemoveList.Clear();
		}
		
		public static HashSetComponent<Unit> GetRecordList(this UnitComponent self, UnitType unitType)
		{
			HashSetComponent<Unit> recordList;
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
			HashSetComponent<Unit> recordList = self.GetRecordList(unit.Type);
			recordList.Add(unit);
		}

		public static Unit Get(this UnitComponent self, long id)
		{
			Unit unit = self.GetChild<Unit>(id);
			return unit;
		}

		public static void AddWaitRemove(this UnitComponent self, Unit unit)
		{
			self.waitRemoveList.Add(unit.Id);
		}
		
		public static void Remove(this UnitComponent self, long id)
		{
			Unit unit = self.GetChild<Unit>(id);
			if (unit == null)
			{
				return;
			}
			HashSetComponent<Unit> recordList = self.GetRecordList(unit.Type);
			recordList.Remove(unit);
			unit?.Dispose();
		}
		
		public static void AddSyncUnit(this UnitComponent self, Unit unit)
		{
			self.NeedSyncUnits.Add(unit);
		}
		
		public static void SyncUnit(this UnitComponent self)
		{
            if (self.NeedSyncUnits.Count == 0)
                return;
            
			//同步单位状态（位置、方向、）
            foreach (Unit unit in self.NeedSyncUnits)
            {
                if(unit.IsDisposed || unit.Type != UnitType.Bullet)
	                continue;
                EventSystem.Instance.Invoke<SyncUnits>(new SyncUnits(){
	                units = new List<Unit>(){unit},
                });
            }
            self.NeedSyncUnits.Clear();
		}
	}
}