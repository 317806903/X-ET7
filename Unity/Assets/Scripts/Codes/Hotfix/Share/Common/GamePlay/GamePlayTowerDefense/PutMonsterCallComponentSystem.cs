﻿using ET.Ability;
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
				self.MonsterCallPos = new();
				self.MonsterCallUnitId = new();
			}
		}

		[ObjectSystem]
		public class PutMonsterCallComponentDestroySystem : DestroySystem<PutMonsterCallComponent>
		{
			protected override void Destroy(PutMonsterCallComponent self)
			{
				self.MonsterCallPos?.Clear();
				self.MonsterCallUnitId?.Clear();
			}
		}

		public static bool Init(this PutMonsterCallComponent self, long playerId, string unitCfgId, float3 monsterCallPos, float3 forward)
		{
			if (self.MonsterCallUnitId.ContainsKey(playerId))
			{
				return false;
			}
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
			TeamFlagType teamFlagType = gamePlayTowerDefenseComponent.GetMonsterTeamFlagTypeByPlayer(playerId);

			Unit monsterCallUnit = self.CreateMonsterCall(teamFlagType, playerId, unitCfgId, monsterCallPos, forward);
			if (gamePlayTowerDefenseComponent.isTowerDefenseTeamOne)
			{
				List<long> playerList = gamePlayTowerDefenseComponent.GetPlayerList();
				foreach (long playerIdTmp in playerList)
				{
					self.MonsterCallPos[playerIdTmp] = monsterCallPos;
					self.MonsterCallUnitId[playerIdTmp] = monsterCallUnit.Id;
				}
			}
			else
			{
				self.MonsterCallPos[playerId] = monsterCallPos;
				self.MonsterCallUnitId[playerId] = monsterCallUnit.Id;
			}

			self.ChkNextStep();
			return true;
		}

		public static Dictionary<long, long> GetMonsterCallUnitList(this PutMonsterCallComponent self)
		{
			return self.MonsterCallUnitId;
		}

		public static void ResetByPlayer(this PutMonsterCallComponent self, long playerId)
		{
			if (self.MonsterCallUnitId.TryGetValue(playerId, out long monsterCallUnitId))
			{
				Unit monsterCallUnit = UnitHelper.GetUnit(self.DomainScene(), monsterCallUnitId);
				monsterCallUnit.DestroyNotDeathShow();
				self.MonsterCallUnitId.Remove(playerId);
				self.MonsterCallPos.Remove(playerId);
			}
		}

		public static void InitWhenPVP(this PutMonsterCallComponent self, float3 midPos, float3 forward)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();

			TeamFlagType teamFlagType = TeamFlagType.Monster1;
			string unitCfgId = "Unit_MonsterCall";
			float3 monsterCallPos = midPos;
			Unit monsterCallUnit = self.CreateMonsterCall(teamFlagType, -1, unitCfgId, monsterCallPos, forward);
			List<long> playerList = gamePlayTowerDefenseComponent.GetPlayerList();
			foreach (long playerId in playerList)
			{
				self.MonsterCallPos[playerId] = monsterCallPos;
				self.MonsterCallUnitId[playerId] = monsterCallUnit.Id;
			}

			self.ChkNextStep();
		}

		public static string GetMonsterCallCount(this PutMonsterCallComponent self)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();

			List<long> playerList = gamePlayTowerDefenseComponent.GetPlayerList();
			string count = $"({self.MonsterCallUnitId.Count}/{playerList.Count})";
			return count;
		}

		public static void ChkNextStep(this PutMonsterCallComponent self)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();

			List<long> playerList = gamePlayTowerDefenseComponent.GetPlayerList();
			foreach (long playerId in playerList)
			{
				if (self.MonsterCallUnitId.ContainsKey(playerId) == false)
				{
					gamePlayTowerDefenseComponent.NoticeToClientAll();
					return;
				}
			}

			gamePlayTowerDefenseComponent.FinishedPutMonsterPoint().Coroutine();
		}

		public static GamePlayTowerDefenseComponent GetGamePlayTowerDefense(this PutMonsterCallComponent self)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
			return gamePlayTowerDefenseComponent;
		}

		public static Unit CreateMonsterCall(this PutMonsterCallComponent self, TeamFlagType teamFlagType, long playerId, string unitCfgId, float3 monsterCallPos, float3 forward)
		{
			return GamePlayTowerDefenseHelper.CreateMonsterCall(self.DomainScene(), playerId, unitCfgId, monsterCallPos, forward, teamFlagType);
		}

		public static float3 GetPosition(this PutMonsterCallComponent self, long playerId)
		{
			if (self.MonsterCallUnitId.ContainsKey(playerId))
			{
				long unitId = self.MonsterCallUnitId[playerId];
				Unit unit = ET.Ability.UnitHelper.GetUnit(self.DomainScene(), unitId);
				if (unit != null)
				{
					return unit.Position;
				}
			}
			return self.MonsterCallPos[playerId];
		}

		public static long GetCallMonsterUnitId(this PutMonsterCallComponent self, long playerId)
		{
			if (self.MonsterCallUnitId.ContainsKey(playerId))
			{
				return self.MonsterCallUnitId[playerId];
			}
			return 0;
		}

	}
}