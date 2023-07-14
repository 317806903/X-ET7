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
				self.NeedSyncNumericUnits = new();
				self.NeedSyncPosUnits = new();
				self.waitRemoveList = new();
				self.observerList = HashSetComponent<Unit>.Create();
				self.playerList = HashSetComponent<Unit>.Create();
				self.actorList = HashSetComponent<Unit>.Create();
				self.npcList = HashSetComponent<Unit>.Create();
				self.sceneObjList = HashSetComponent<Unit>.Create();
				self.bulletList = HashSetComponent<Unit>.Create();
				self.aoeList = HashSetComponent<Unit>.Create();
				self.sceneEffectList = HashSetComponent<Unit>.Create();
			}
		}
	
		[ObjectSystem]
		public class UnitComponentDestroySystem : DestroySystem<UnitComponent>
		{
			protected override void Destroy(UnitComponent self)
			{
				self.NeedSyncNumericUnits.Dispose();
				self.NeedSyncPosUnits.Dispose();
				self.waitRemoveList.Clear();
				self.observerList.Dispose();
				self.playerList.Dispose();
				self.actorList.Dispose();
				self.npcList.Dispose();
				self.sceneObjList.Dispose();
				self.bulletList.Dispose();
				self.aoeList.Dispose();
				self.sceneEffectList.Dispose();
			}
		}

		[ObjectSystem]
		public class UnitComponentFixedUpdateSystem: FixedUpdateSystem<UnitComponent>
		{
			protected override void FixedUpdate(UnitComponent self)
			{
				if (self.DomainScene().SceneType != SceneType.Map)
				{
					return;
				}

				float fixedDeltaTime = TimeHelper.FixedDetalTime;
				self.FixedUpdate(fixedDeltaTime);
			}
		}

		public static void FixedUpdate(this UnitComponent self, float fixedDeltaTime)
		{
			ProfilerSample.BeginSample($"DoUnitHit");
			self.DoUnitHit(fixedDeltaTime);
			ProfilerSample.EndSample();
			
			ProfilerSample.BeginSample($"DoUnitRemove");
			self.DoUnitRemove();
			ProfilerSample.EndSample();
			
			ProfilerSample.BeginSample($"SyncPosUnit");
			self.SyncPosUnit();
			ProfilerSample.EndSample();
			
			ProfilerSample.BeginSample($"SyncNumericUnit");
			self.SyncNumericUnit();
			ProfilerSample.EndSample();
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
				case UnitType.ObserverUnit:
					recordList = self.observerList;
					break;
				case UnitType.PlayerUnit:
					recordList = self.playerList;
					break;
				case UnitType.ActorUnit:
					recordList = self.actorList;
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
				case UnitType.Aoe:
					recordList = self.aoeList;
					break;
				case UnitType.SceneEffect:
					recordList = self.sceneEffectList;
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
			
			GamePlayHelper.RemoveUnitInfo(unit);
			
			HashSetComponent<Unit> recordList = self.GetRecordList(unit.Type);
			recordList.Remove(unit);
			unit?.Dispose();
		}
		
		public static void AddSyncPosUnit(this UnitComponent self, Unit unit)
		{
			if (self.NeedSyncPosUnits.Contains(unit))
			{
				return;
			}

			if (unit.GetComponent<AOIEntity>() == null)
			{
				return;
			}
			self.NeedSyncPosUnits.Add(unit);
		}
		
		public static void AddSyncNumericUnit(this UnitComponent self, Unit unit)
		{
			if (self.NeedSyncNumericUnits.Contains(unit))
			{
				return;
			}
			if (unit.GetComponent<AOIEntity>() == null)
			{
				return;
			}
			self.NeedSyncNumericUnits.Add(unit);
		}
		
		public static void SyncPosUnit(this UnitComponent self)
		{
            if (self.NeedSyncPosUnits.Count == 0)
                return;
            
            EventType.SyncPosUnits _SyncPosUnits = new ()
            {
	            units = ListComponent<Unit>.Create(),
            };
			//同步单位状态（位置、方向、）
            foreach (Unit unit in self.NeedSyncPosUnits)
            {
                //if(unit.IsDisposed || (unit.Type == UnitType.Player || unit.Type == UnitType.Monster || unit.Type == UnitType.NPC))
                if(unit.IsDisposed)
	                continue;
                MoveByPathComponent moveByPathComponent = unit.GetComponent<MoveByPathComponent>();
                if (moveByPathComponent != null)
                {
	                if (moveByPathComponent.IsArrived() == false)
	                {
		                continue;
	                }
                }
                
                _SyncPosUnits.units.Add(unit);
            }

            if (_SyncPosUnits.units.Count > 0)
            {
	            EventSystem.Instance.Publish(self.DomainScene(), _SyncPosUnits);
            }
            self.NeedSyncPosUnits.Clear();
		}
		
		public static void SyncNumericUnit(this UnitComponent self)
		{
            if (self.NeedSyncNumericUnits.Count == 0)
                return;
            
            EventType.SyncNumericUnits _SyncNumericUnits = new ()
            {
	            units = ListComponent<Unit>.Create(),
            };
            foreach (Unit unit in self.NeedSyncNumericUnits)
            {
                if(unit.IsDisposed)
	                continue;
                
                _SyncNumericUnits.units.Add(unit);
            }
            if (_SyncNumericUnits.units.Count > 0)
            {
	            EventSystem.Instance.Publish(self.DomainScene(), _SyncNumericUnits);
            }
            self.NeedSyncNumericUnits.Clear();
		}
	}
}