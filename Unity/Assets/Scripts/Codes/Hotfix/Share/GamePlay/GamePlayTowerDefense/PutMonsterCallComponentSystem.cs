using ET.Ability;
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

			self.MonsterCallPos[playerId] = monsterCallPos;
			Unit monsterCallUnit = self.CreateMonsterCall(teamFlagType, unitCfgId, monsterCallPos, forward);
			self.MonsterCallUnitId[playerId] = monsterCallUnit.Id;

			self.ChkNextStep();
			return true;
		}

		public static void InitWhenPVP(this PutMonsterCallComponent self, float3 midPos)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();

			TeamFlagType teamFlagType = TeamFlagType.Monster1;
			string unitCfgId = "Unit_MonsterCall";
			float3 monsterCallPos = midPos;
			float3 forward = new float3(0, 0, 1);
			Unit monsterCallUnit = self.CreateMonsterCall(teamFlagType, unitCfgId, monsterCallPos, forward);
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

			EventSystem.Instance.Publish(self.DomainScene(), new ET.Ability.AbilityTriggerEventType.GamePlayTowerDefense_Status_PutMonsterPointEnd());

			gamePlayTowerDefenseComponent.DoNextStep().Coroutine();
		}

		public static GamePlayTowerDefenseComponent GetGamePlayTowerDefense(this PutMonsterCallComponent self)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
			return gamePlayTowerDefenseComponent;
		}

		public static Unit CreateMonsterCall(this PutMonsterCallComponent self, TeamFlagType teamFlagType, string unitCfgId, float3 monsterCallPos, float3 forward)
		{
			return GamePlayTowerDefenseHelper.CreateMonsterCall(self.DomainScene(), unitCfgId, monsterCallPos, forward, teamFlagType);
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

	}
}