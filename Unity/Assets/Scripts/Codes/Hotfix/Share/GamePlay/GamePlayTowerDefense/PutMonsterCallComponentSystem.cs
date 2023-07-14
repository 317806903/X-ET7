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

		public static void Init(this PutMonsterCallComponent self, long playerId, string unitCfgId, float3 monsterCallPos)
		{
			self.MonsterCallPos[playerId] = monsterCallPos;
			Unit monsterCallUnit = self.CreateMonsterCall(unitCfgId, monsterCallPos);
			self.MonsterCallUnitId[playerId] = monsterCallUnit.Id;

			self.ChkNextStep();
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
			gamePlayTowerDefenseComponent.Start();
		}

		public static Unit CreateMonsterCall(this PutMonsterCallComponent self, string unitCfgId, float3 monsterCallPos)
		{
			return GamePlayTowerDefenseHelper.CreateMonsterCall(self.DomainScene(), unitCfgId, monsterCallPos);
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