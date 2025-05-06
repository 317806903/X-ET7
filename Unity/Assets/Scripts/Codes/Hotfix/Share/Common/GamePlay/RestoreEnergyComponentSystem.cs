using ET.Ability;
using System;
using System.Collections.Generic;

namespace ET
{
    [FriendOf(typeof(RestoreEnergyComponent))]
    public static class RestoreEnergyComponentSystem
	{
		[ObjectSystem]
		public class RestoreEnergyComponentAwakeSystem : AwakeSystem<RestoreEnergyComponent>
		{
			protected override void Awake(RestoreEnergyComponent self)
			{
			}
		}

		[ObjectSystem]
		public class RestoreEnergyComponentDestroySystem : DestroySystem<RestoreEnergyComponent>
		{
			protected override void Destroy(RestoreEnergyComponent self)
			{
			}
		}

		[ObjectSystem]
		public class RestoreEnergyComponentFixedUpdateSystem: FixedUpdateSystem<RestoreEnergyComponent>
		{
			protected override void FixedUpdate(RestoreEnergyComponent self)
			{
				if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
				{
					return;
				}

				float fixedDeltaTime = TimeHelper.FixedDetalTime;
				self.FixedUpdate(fixedDeltaTime);
			}
		}

		public static GamePlayComponent GetGamePlay(this RestoreEnergyComponent self)
		{
			GamePlayComponent gamePlayComponent = self.GetParent<GamePlayComponent>();
			return gamePlayComponent;
		}

		public static GamePlayTowerDefenseComponent GetGamePlayTowerDefense(this RestoreEnergyComponent self)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlay().GetComponent<GamePlayTowerDefenseComponent>();
			return gamePlayTowerDefenseComponent;
		}

		public static void Add(this RestoreEnergyComponent self, SkillComponent skillComponent)
		{
			self.skillComponentList.Add(skillComponent);
		}

		public static void Add(this RestoreEnergyComponent self, SkillObj skillObj)
		{
			self.skillObjList.Add(skillObj);
		}

		public static void FixedUpdate(this RestoreEnergyComponent self, float fixedDeltaTime)
		{
			if (self.GetGamePlayTowerDefense() != null)
			{
				if (self.GetGamePlayTowerDefense().ChkIsGameEnd()
				    || self.GetGamePlayTowerDefense().ChkIsGameWaitRescan()
				    || self.GetGamePlayTowerDefense().ChkIsGameRecoverSuccess()
				    || self.GetGamePlayTowerDefense().ChkIsGameRecover()
				    || self.GetGamePlayTowerDefense().ChkIsGameRecovering())
				{
					return;
				}
			}

			self.ClearNotExist();

			// if (self.GetGamePlayTowerDefense().ChkIsGameInTheBattle())
			// {
			// 	self.DealCurEnergyNumByTime(fixedDeltaTime);
			// 	self.DealCurCommonEnergyNumByTime(fixedDeltaTime);
			// }

		}

		public static void DealCurEnergyNumByTime(this RestoreEnergyComponent self, float fixedDeltaTime)
		{
			foreach (EntityRef<SkillObj> entityRef in self.skillObjList)
			{
				SkillObj skillObj = entityRef;
				if (skillObj != null)
				{
					skillObj.DealCurEnergyNumByTime(fixedDeltaTime);
				}
			}
		}

		public static void DealCurCommonEnergyNumByTime(this RestoreEnergyComponent self, float fixedDeltaTime)
		{
			foreach (EntityRef<SkillComponent> entityRef in self.skillComponentList)
			{
				SkillComponent skillComponent = entityRef;
				if (skillComponent != null)
				{
					skillComponent.DealCurCommonEnergyNumByTime(fixedDeltaTime);
				}
			}
		}

		public static void DealCurEnergyNumByWave(this RestoreEnergyComponent self)
		{
			foreach (EntityRef<SkillObj> entityRef in self.skillObjList)
			{
				SkillObj skillObj = entityRef;
				if (skillObj != null)
				{
					skillObj.DealCurEnergyNumByWave();
				}
			}
		}

		public static void DealCurCommonEnergyNumByWave(this RestoreEnergyComponent self)
		{
			foreach (EntityRef<SkillComponent> entityRef in self.skillComponentList)
			{
				SkillComponent skillComponent = entityRef;
				if (skillComponent != null)
				{
					skillComponent.DealCurCommonEnergyNumByWave();
				}
			}
		}

		public static void ClearNotExist(this RestoreEnergyComponent self)
		{
			if (++self.curFrameChk >= self.waitFrameChk)
			{
				self.curFrameChk = 0;
				self._ClearNotExist();
			}
		}

		public static void _ClearNotExist(this RestoreEnergyComponent self)
		{
			if (self.skillComponentList.Count == 0 && self.skillObjList.Count == 0)
			{
				return;
			}

			self.skillComponentListClear.Clear();
			foreach (EntityRef<SkillComponent> entityRef in self.skillComponentList)
			{
				SkillComponent skillComponent = entityRef;
				if (skillComponent == null)
				{
					self.skillComponentListClear.Add(entityRef);
				}
			}

			foreach (EntityRef<SkillComponent> entityRef in self.skillComponentListClear)
			{
				self.skillComponentList.Remove(entityRef);
			}


			self.skillObjListClear.Clear();
			foreach (EntityRef<SkillObj> entityRef in self.skillObjList)
			{
				SkillObj skillObj = entityRef;
				if (skillObj == null)
				{
					self.skillObjListClear.Add(entityRef);
				}
			}

			foreach (EntityRef<SkillObj> entityRef in self.skillObjListClear)
			{
				self.skillObjList.Remove(entityRef);
			}
		}

	}
}