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
				self.NeedSyncNoticeUnitAdds = new();
				self.NeedSyncNoticeUnitRemoves = new();
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
				self.NeedSyncNoticeUnitAdds.Clear();
				self.NeedSyncNoticeUnitRemoves.Clear();
				self.waitRemoveList.Clear();
				self.waitRemoveList = null;
				self.observerList.Dispose();
				self.observerList = null;
				self.playerList.Dispose();
				self.playerList = null;
				self.actorList.Dispose();
				self.actorList = null;
				self.npcList.Dispose();
				self.npcList = null;
				self.sceneObjList.Dispose();
				self.sceneObjList = null;
				self.bulletList.Dispose();
				self.bulletList = null;
				self.aoeList.Dispose();
				self.aoeList = null;
				self.sceneEffectList.Dispose();
				self.sceneEffectList = null;
			}
		}

		[ObjectSystem]
		public class UnitComponentFixedUpdateSystem: FixedUpdateSystem<UnitComponent>
		{
			protected override void FixedUpdate(UnitComponent self)
			{
				if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
				{
					return;
				}

				float fixedDeltaTime = TimeHelper.FixedDetalTime;
				self.FixedUpdate(fixedDeltaTime);
			}
		}

		public static void FixedUpdate(this UnitComponent self, float fixedDeltaTime)
		{
			self.DoUnitHit(fixedDeltaTime);

			self.DoUnitRemove();

			self.SyncNoticeUnitAdd().Coroutine();
			self.SyncNoticeUnitRemove().Coroutine();
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

			ET.Ability.UnitHelper.AddUnitDelayRemove(self.DomainScene(), unit);

			GamePlayHelper.RemoveUnitInfo(unit);

			HashSetComponent<Unit> recordList = self.GetRecordList(unit.Type);
			recordList.Remove(unit);
			unit?.Dispose();
		}

		public static void AddSyncNoticeUnitAdd(this UnitComponent self, Unit beNoticeUnit, Unit unit)
		{
			if (unit == null)
			{
				return;
			}
			if (self.NeedSyncNoticeUnitAdds.Contains(beNoticeUnit,unit))
			{
				return;
			}

			self.NeedSyncNoticeUnitAdds.Add(beNoticeUnit,unit);
		}

		public static void AddSyncNoticeUnitRemove(this UnitComponent self, Unit beNoticeUnit, long unitId)
		{
			if (self.NeedSyncNoticeUnitRemoves.Contains(beNoticeUnit, unitId))
			{
				return;
			}

			self.NeedSyncNoticeUnitRemoves.Add(beNoticeUnit, unitId);
		}

		public static async ETTask SyncNoticeUnitAdd(this UnitComponent self)
		{
			if (self.NeedSyncNoticeUnitAdds.Count == 0)
				return;

			await ETTask.CompletedTask;
			while (self.NeedSyncNoticeUnitAdds.Count > 0)
			{
				Unit beNoticeUnit = null;
				foreach (var needSyncNoticeUnitAdd in self.NeedSyncNoticeUnitAdds)
				{
					beNoticeUnit = needSyncNoticeUnitAdd.Key;

					EventType.SyncNoticeUnitAdds _SyncNoticeUnitAdds = new ()
					{
						beNoticeUnit = beNoticeUnit,
						units = new(),
					};

					self.NeedSyncNoticeUnitAddsTmp.Clear();
					int maxCount = 500;
					//同步单位状态（位置、方向、）
					foreach (Unit unit in needSyncNoticeUnitAdd.Value)
					{
						if (unit == null || unit.IsDisposed)
						{
							self.NeedSyncNoticeUnitAddsTmp.Add(unit);
							continue;
						}

						_SyncNoticeUnitAdds.units.Add(unit);
						self.NeedSyncNoticeUnitAddsTmp.Add(unit);
						if (maxCount-- <= 0)
						{
							break;
						}
					}

					if (_SyncNoticeUnitAdds.units.Count > 0)
					{
						try
						{
							EventSystem.Instance.Publish(self.DomainScene(), _SyncNoticeUnitAdds);
						}
						catch (Exception e)
						{
							Log.Error(e);
						}
					}

					if (self.NeedSyncNoticeUnitAddsTmp.Count > 0)
					{
						break;
					}
				}
				foreach (Unit unit in self.NeedSyncNoticeUnitAddsTmp)
				{
					self.NeedSyncNoticeUnitAdds.Remove(beNoticeUnit, unit);
				}
				self.NeedSyncNoticeUnitAddsTmp.Clear();
			}
		}

		public static async ETTask SyncNoticeUnitRemove(this UnitComponent self)
		{
			if (self.NeedSyncNoticeUnitRemoves.Count == 0)
				return;

			await ETTask.CompletedTask;
			while (self.NeedSyncNoticeUnitRemoves.Count > 0)
			{
				Unit beNoticeUnit = null;
				foreach (var needSyncNoticeUnitRemove in self.NeedSyncNoticeUnitRemoves)
				{
					beNoticeUnit = needSyncNoticeUnitRemove.Key;

					EventType.SyncNoticeUnitRemoves _SyncNoticeUnitRemoves = new ()
					{
						beNoticeUnit = beNoticeUnit,
						unitIds = new(),
					};

					self.NeedSyncNoticeUnitRemovesTmp.Clear();
					int maxCount = 500;
					foreach (long unitId in needSyncNoticeUnitRemove.Value)
					{
						_SyncNoticeUnitRemoves.unitIds.Add(unitId);
						self.NeedSyncNoticeUnitRemovesTmp.Add(unitId);
						if (maxCount-- <= 0)
						{
							break;
						}
					}

					if (_SyncNoticeUnitRemoves.unitIds.Count > 0)
					{
						try
						{
							EventSystem.Instance.Publish(self.DomainScene(), _SyncNoticeUnitRemoves);
						}
						catch (Exception e)
						{
							Log.Error(e);
						}
					}

					if (self.NeedSyncNoticeUnitRemovesTmp.Count > 0)
					{
						break;
					}
				}
				foreach (long unitId in self.NeedSyncNoticeUnitRemovesTmp)
				{
					self.NeedSyncNoticeUnitRemoves.Remove(beNoticeUnit, unitId);
				}
				self.NeedSyncNoticeUnitRemovesTmp.Clear();
			}
		}

	}
}