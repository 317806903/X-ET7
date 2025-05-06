using ET.Ability;
using System;
using System.Collections.Generic;
using System.Xml.Schema;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof(GamePlayPkComponent))]
    [FriendOf(typeof(Unit))]
    public static class GamePlayPKComponentSystem
	{
		[ObjectSystem]
		public class GamePlayPKComponentAwakeSystem : AwakeSystem<GamePlayPkComponent>
		{
			protected override void Awake(GamePlayPkComponent self)
			{
			}
		}

		[ObjectSystem]
		public class GamePlayPKComponentDestroySystem : DestroySystem<GamePlayPkComponent>
		{
			protected override void Destroy(GamePlayPkComponent self)
			{
			}
		}

		[ObjectSystem]
		public class GamePlayPKComponentFixedUpdateSystem: FixedUpdateSystem<GamePlayPkComponent>
		{
			protected override void FixedUpdate(GamePlayPkComponent self)
			{
				if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
				{
					return;
				}

				float fixedDeltaTime = TimeHelper.FixedDetalTime;
				self.FixedUpdate(fixedDeltaTime);
			}
		}

		public static void FixedUpdate(this GamePlayPkComponent self, float fixedDeltaTime)
		{
		}

		public static async ETTask Init(this GamePlayPkComponent self, long ownerPlayerId, string gamePlayModeCfgId, RoomTypeInfo roomTypeInfo)
		{
			await ETTask.CompletedTask;
			self.gamePlayModeCfgId = gamePlayModeCfgId;
			self.roomTypeInfo = roomTypeInfo;
			self.ownerPlayerId = ownerPlayerId;

		}

		/// <summary>
		/// 处理阵营关系
		/// </summary>
		/// <param name="self"></param>
		public static void DealFriendTeamFlagType(this GamePlayPkComponent self)
		{
			GamePlayComponent gamePlayComponent = self.GetGamePlay();
			gamePlayComponent.DealFriendTeamFlag(null, false, true);

            //TeamGlobal, TeamPlayer1, TeamPlayerSkill1, TeamPlayer2, TeamPlayerSkill2 之间设成 友好
            {
	            List<TeamFlagType> teamFlagTypes = new();
	            teamFlagTypes.Add(TeamFlagType.TeamGlobal);
	            teamFlagTypes.Add(TeamFlagType.TeamPlayer);
	            teamFlagTypes.Add(TeamFlagType.TeamPlayerSkill);
	            teamFlagTypes.Add(TeamFlagType.TeamPlayer1);
	            teamFlagTypes.Add(TeamFlagType.TeamPlayerSkill1);
	            gamePlayComponent.DealFriendTeamFlag(teamFlagTypes, false, false);
            }
            {
	            List<TeamFlagType> teamFlagTypes = new();
	            teamFlagTypes.Add(TeamFlagType.TeamGlobal);
	            teamFlagTypes.Add(TeamFlagType.TeamPlayer);
	            teamFlagTypes.Add(TeamFlagType.TeamPlayerSkill);
	            teamFlagTypes.Add(TeamFlagType.TeamPlayer2);
	            teamFlagTypes.Add(TeamFlagType.TeamPlayerSkill2);
	            gamePlayComponent.DealFriendTeamFlag(teamFlagTypes, false, false);
            }
            {
	            List<TeamFlagType> teamFlagTypes = new();
	            teamFlagTypes.Add(TeamFlagType.TeamGlobal);
	            teamFlagTypes.Add(TeamFlagType.TeamPlayer);
	            teamFlagTypes.Add(TeamFlagType.TeamPlayerSkill);
	            teamFlagTypes.Add(TeamFlagType.TeamPlayer3);
	            teamFlagTypes.Add(TeamFlagType.TeamPlayerSkill3);
	            gamePlayComponent.DealFriendTeamFlag(teamFlagTypes, false, false);
            }
            {
	            List<TeamFlagType> teamFlagTypes = new();
	            teamFlagTypes.Add(TeamFlagType.TeamGlobal);
	            teamFlagTypes.Add(TeamFlagType.TeamPlayer);
	            teamFlagTypes.Add(TeamFlagType.TeamPlayerSkill);
	            teamFlagTypes.Add(TeamFlagType.TeamPlayer4);
	            teamFlagTypes.Add(TeamFlagType.TeamPlayerSkill4);
	            gamePlayComponent.DealFriendTeamFlag(teamFlagTypes, false, false);
            }
            {
	            List<TeamFlagType> teamFlagTypes = new();
	            teamFlagTypes.Add(TeamFlagType.TeamGlobal);
	            teamFlagTypes.Add(TeamFlagType.TeamPlayer);
	            teamFlagTypes.Add(TeamFlagType.TeamPlayerSkill);
	            teamFlagTypes.Add(TeamFlagType.TeamPlayer5);
	            teamFlagTypes.Add(TeamFlagType.TeamPlayerSkill5);
	            gamePlayComponent.DealFriendTeamFlag(teamFlagTypes, false, false);
            }

            //Monster, Monster1, Monster2...MonsterN 之间设成 友好
            {
	            List<TeamFlagType> teamFlagTypes = new();
	            teamFlagTypes.Add(TeamFlagType.Monster);
	            teamFlagTypes.Add(TeamFlagType.Monster1);
	            teamFlagTypes.Add(TeamFlagType.Monster2);
	            teamFlagTypes.Add(TeamFlagType.Monster3);
	            teamFlagTypes.Add(TeamFlagType.Monster4);
	            teamFlagTypes.Add(TeamFlagType.Monster5);
	            gamePlayComponent.DealFriendTeamFlag(teamFlagTypes, false, false);
            }
		}

		public static async ETTask DoReadyForBattle(this GamePlayPkComponent self)
		{
			await self.Start();
			await ETTask.CompletedTask;
		}

		public static async ETTask Start(this GamePlayPkComponent self)
		{
			GamePlayComponent gamePlayComponent = self.GetGamePlay();
			gamePlayComponent.Start();

			await self.ChkLoadMesh();

			self.DealFriendTeamFlagType();
			self.NoticeToClientAll();
			await ETTask.CompletedTask;
		}

		public static async ETTask ChkLoadMesh(this GamePlayPkComponent self)
		{
			self.isLoadMeshFinished = false;
			long curTime = TimeHelper.ServerNow();
			GamePlayComponent gamePlayComponent = self.GetGamePlay();
			while (true)
			{
				(bool isLoadMeshFinished, bool isLoadMeshError) = gamePlayComponent.ChkNavMeshReady();
				if (isLoadMeshError)
				{
					Log.Error($"ET.GamePlayPKComponentSystem.ChkLoadMesh isLoadMeshError");
					return;
				}
				else if (isLoadMeshFinished == false)
				{
					await TimerComponent.Instance.WaitAsync(800);
					if (self.IsDisposed)
					{
						return;
					}

#if  UNITY_EDITOR
					if (curTime < TimeHelper.ServerNow() - 5000)
#else
                    if (curTime < TimeHelper.ServerNow() - 20000)
#endif
					{
						Log.Error($"-- ET.GamePlayTowerDefenseComponentSystem.TransToWaitMeshFinished curTime < TimeHelper.ServerNow() - 20000");
						return;
					}
				}
				else
				{
					break;
				}
			}

			EventSystem.Instance.Publish(self.DomainScene(), new ET.Ability.AbilityTriggerEventType.GamePlay_Status_LoadMeshFinished_First());

			self.WaitLoadMeshFinished_Next().Coroutine();

			self.isLoadMeshFinished = true;
		}

		public static async ETTask WaitLoadMeshFinished_Next(this GamePlayPkComponent self)
		{
			bool bRet = await self.GetGamePlay().ChkPlayerExist();
			if (bRet == false)
			{
				return;
			}

			await TimerComponent.Instance.WaitAsync(1000);
			EventSystem.Instance.Publish(self.DomainScene(), new ET.Ability.AbilityTriggerEventType.GamePlay_Status_LoadMeshFinished());
		}

		public static async ETTask TransToGameSuccess(this GamePlayPkComponent self)
		{
			//self.GamePlayTowerDefenseStatus = GamePlayTowerDefenseStatus.GameSuccess;

			await self.GameEnd();

			GamePlayComponent gamePlayComponent = self.GetGamePlay();
			await gamePlayComponent.GameEnd();

			self.NoticeToClientAll();
		}

		public static void DealUnitBeKill(this GamePlayPkComponent self, Unit attackerUnit, Unit beKillUnit)
		{
			long beKillUnitPlayerId = GamePlayHelper.GetPlayerIdByUnitId(beKillUnit);
			if (beKillUnitPlayerId != (long)ET.PlayerId.PlayerNone)
			{
				TowerComponent towerComponent = beKillUnit.GetComponent<TowerComponent>();
				if (towerComponent != null)
				{
					HashSet<Unit> downTowerList = self.GetTowerListWhenStackedOnTop(beKillUnit);
					float curUnitHeight = UnitHelper.GetBodyHeight(beKillUnit);
					foreach (Unit towerUnit in downTowerList)
					{
						Ability.UnitHelper.ResetPos(towerUnit, towerUnit.Position - new float3(0, curUnitHeight, 0), float3.zero);
					}

				}

				return;
			}
		}

		public static void DealUnitCallActor(this GamePlayPkComponent self, Unit unit, Unit beCallUnit)
		{
		}

		public static async ETTask GameEnd(this GamePlayPkComponent self)
		{
			await ETTask.CompletedTask;
		}

		public static HashSet<Unit> GetTowerListWhenStackedOnTop(this GamePlayPkComponent self, Unit curUnit)
		{
			float3 curUnitPos = curUnit.Position;
			float curUnitHeight = Ability.UnitHelper.GetBodyHeight(curUnit);
			float curUnitRadius = Ability.UnitHelper.GetBodyRadius(curUnit);
			HashSet<long> ignoreTowerUnitIds = HashSetComponent<long>.Create();
			ignoreTowerUnitIds.Add(curUnit.Id);
			return self.GetTowerListWhenStackedOnTop(curUnitPos, curUnitHeight, curUnitRadius, ignoreTowerUnitIds);
		}

		public static HashSet<Unit> GetTowerListWhenStackedOnTop(this GamePlayPkComponent self, float3 curUnitPos, float curUnitHeight, float curUnitRadius, HashSet<long> ignoreTowerUnitIds)
		{
			HashSet<Unit> list = HashSetComponent<Unit>.Create();
			do
			{
				var unitList = self.GetTowerOnceWhenStackedOnTop(curUnitPos, curUnitHeight, curUnitRadius, ignoreTowerUnitIds);
				if (unitList.Count == 0)
				{
					break;
				}
				foreach (Unit unit in unitList)
				{
					if (unit != null)
					{
						list.Add(unit);
						curUnitPos = unit.Position;
						curUnitHeight = Ability.UnitHelper.GetBodyHeight(unit);
						curUnitRadius = Ability.UnitHelper.GetBodyRadius(unit);
						ignoreTowerUnitIds.Add(unit.Id);
					}
					else
					{
						break;
					}
				}
			}
			while (true);

			return list;
		}

		public static HashSet<Unit> GetTowerOnceWhenStackedOnTop(this GamePlayPkComponent self, float3 curUnitPos, float curUnitHeight, float curUnitRadius, HashSet<long> ignoreTowerUnitIds)
		{
			HashSet<Unit> unitList = HashSetComponent<Unit>.Create();
			UnitComponent unitComponent = Ability.UnitHelper.GetUnitComponent(self.DomainScene());
			HashSet<Unit> list = unitComponent.GetRecordList(UnitType.ActorUnit);
			if (list == null)
			{
				return unitList;
			}
			foreach (Unit unit in list)
			{
				if (Ability.UnitHelper.ChkUnitAlive(unit, false) == false)
				{
					continue;
				}
				if (ignoreTowerUnitIds.Contains(unit.Id))
				{
					continue;
				}
				bool isNear = Ability.UnitHelper.ChkIsStackedOnTop(curUnitPos, curUnitHeight, unit, curUnitRadius);
				if (isNear)
				{
					unitList.Add(unit);
				}
			}

			return unitList;
		}

		public static bool ChkMovePlayerTowerNeedDownTower(this GamePlayPkComponent self, long towerUnitId, float3 position)
		{
			Unit curUnit = Ability.UnitHelper.GetUnit(self.DomainScene(), towerUnitId);
			float3 curUnitPos = curUnit.Position;
			float curUnitHeight = Ability.UnitHelper.GetBodyHeight(curUnit);
			float curUnitRadius = Ability.UnitHelper.GetBodyRadius(curUnit);

			HashSet<long> ignoreTowerUnitIds = HashSetComponent<long>.Create();
			ignoreTowerUnitIds.Add(curUnit.Id);
			var unitList = self.GetTowerOnceWhenStackedOnTop(curUnitPos, curUnitHeight, curUnitRadius, ignoreTowerUnitIds);
			if (unitList.Count == 0)
			{
				return false;
			}
			curUnitPos = position;
			var unitListNew = self.GetTowerOnceWhenStackedOnTop(curUnitPos, curUnitHeight, curUnitRadius, ignoreTowerUnitIds);
			foreach (Unit unit in unitList)
			{
				if (unitListNew.Contains(unit) == false)
				{
					return true;
				}
			}
			return false;
		}

	}
}