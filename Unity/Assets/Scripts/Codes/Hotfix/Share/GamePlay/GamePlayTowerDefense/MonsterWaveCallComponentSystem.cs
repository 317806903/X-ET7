using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
	[Invoke(TimerInvokeType.GamePlayChkMonsterWaveCallAllClear)]
	public class MonsterWaveCallTimer: ATimer<MonsterWaveCallComponent>
	{
		protected override void Run(MonsterWaveCallComponent self)
		{
			try
			{
				self.ChkMonsterCallAllClear();
			}
			catch (Exception e)
			{
				Log.Error($"MonsterWaveCallTimer timer error: {self.Id}\n{e}");
			}
		}
	}

    [FriendOf(typeof(MonsterWaveCallComponent))]
    public static class MonsterWaveCallComponentSystem
	{
		[ObjectSystem]
		public class MonsterWaveCallComponentAwakeSystem : AwakeSystem<MonsterWaveCallComponent>
		{
			protected override void Awake(MonsterWaveCallComponent self)
			{
				self.sortWaveIndex = new();
				self.waveMonsterCallList = new();

				self.unitId2MonsterCfgId = new();
				self.unitId2RewardGold = new();
			}
		}

		[ObjectSystem]
		public class MonsterWaveCallComponentDestroySystem : DestroySystem<MonsterWaveCallComponent>
		{
			protected override void Destroy(MonsterWaveCallComponent self)
			{
				TimerComponent.Instance.Remove(ref self.Timer);
				self.waveMonsterCallList?.Clear();
				self.unitId2MonsterCfgId?.Clear();
				self.unitId2RewardGold?.Clear();
			}
		}

		public static void Init(this MonsterWaveCallComponent self, string monsterWaveRule)
		{
			self.monsterWaveRule = monsterWaveRule;

			self.totalCount = 0;
			List<TowerDefense_MonsterWaveCallRuleCfg> list = TowerDefense_MonsterWaveCallRuleCfgCategory.Instance.DataList;

			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].WaveRule == self.monsterWaveRule)
				{
					self.totalCount++;
					self.sortWaveIndex.Add(list[i].WaveIndex);
				}
			}
			self.sortWaveIndex.Sort();

			self.curIndex = -1;

		}

		public static float3 GetCallMonsterPosition(this MonsterWaveCallComponent self, long playerId)
		{
			return self.GetGamePlayTowerDefense().GetCallMonsterPosition(playerId);
		}

		public static int GetWaveRewardGold(this MonsterWaveCallComponent self)
		{
			int waveIndex = self.sortWaveIndex[self.curIndex];
			TowerDefense_MonsterWaveCallRuleCfg monsterWaveCallCfg = TowerDefense_MonsterWaveCallRuleCfgCategory.Instance.Get(self.monsterWaveRule, waveIndex);

			return monsterWaveCallCfg.WaveRewardGold;
		}

		public static void DoNextMonsterWaveCall(this MonsterWaveCallComponent self)
		{
			self.Timer = TimerComponent.Instance.NewRepeatedTimer(500, TimerInvokeType.GamePlayChkMonsterWaveCallAllClear, self);
			self.curIndex++;
			self.DoMonsterWaveCall();
		}

		public static bool ChkIsGameEnd(this MonsterWaveCallComponent self)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();

			return gamePlayTowerDefenseComponent.ChkIsGameEnd();
		}

		public static void DoMonsterWaveCall(this MonsterWaveCallComponent self)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
			List<long> playerList = gamePlayTowerDefenseComponent.GetPlayerList();
			foreach (long playerId in playerList)
			{
				MonsterWaveCallOnceComponent monsterWaveCallOnceComponent = self.AddChild<MonsterWaveCallOnceComponent>();
				int waveIndex = self.sortWaveIndex[self.curIndex];
				monsterWaveCallOnceComponent.Init(playerId, self.monsterWaveRule, waveIndex);

				self.duration = monsterWaveCallOnceComponent.duration;
				if (self.waveMonsterCallList.ContainsKey(playerId) == false)
				{
					self.waveMonsterCallList[playerId] = new();
				}
				self.waveMonsterCallList[playerId].Add(self.curIndex, monsterWaveCallOnceComponent);
			}
		}

		public static GamePlayTowerDefenseComponent GetGamePlayTowerDefense(this MonsterWaveCallComponent self)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
			return gamePlayTowerDefenseComponent;
		}

		public static Unit CallMonsterOnce(this MonsterWaveCallComponent self, long playerId, string monsterCfgId, int level, int rewardGold)
		{
			float3 pos = self.GetCallMonsterPosition(playerId);
			float3 randomPos = pos + new float3(RandomGenerator.RandFloat01(), 0, RandomGenerator.RandFloat01());
			float3 randomForward = new float3(RandomGenerator.RandFloat01(), 0, RandomGenerator.RandFloat01());


			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
			TeamFlagType teamFlagType = gamePlayTowerDefenseComponent.GetMonsterTeamFlagTypeByPlayer(playerId);

			Unit monsterUnit = ET.GamePlayTowerDefenseHelper.CreateMonster(self.DomainScene(), playerId, monsterCfgId, level, randomPos, randomForward, teamFlagType);

			self.RecordUnit2Monster(monsterUnit.Id, monsterCfgId, rewardGold);

			return monsterUnit;
		}

		public static void RecordUnit2Monster(this MonsterWaveCallComponent self, long unitId, string monsterCfgId, int rewardGold)
		{
			if (string.IsNullOrEmpty(monsterCfgId) == false)
			{
				self.unitId2MonsterCfgId.Add(unitId, monsterCfgId);
			}
			self.unitId2RewardGold.Add(unitId, rewardGold);
		}

		public static string GetMonsterCfgIdByUnitId(this MonsterWaveCallComponent self, long unitId)
		{
			if (self.unitId2MonsterCfgId.ContainsKey(unitId) == false)
			{
				return "";
			}
			return self.unitId2MonsterCfgId[unitId];
		}

		public static int GetMonsterRewardGoldByUnitId(this MonsterWaveCallComponent self, long unitId)
		{
			if (self.unitId2RewardGold.ContainsKey(unitId) == false)
			{
				return 0;
			}
			return self.unitId2RewardGold[unitId];
		}

		public static void ChkMonsterCallAllClear(this MonsterWaveCallComponent self)
		{
			bool allPlayerWaveMonsterClear = true;
			foreach (var playerWaveMonsterCall in self.waveMonsterCallList)
			{
				bool playerWaveMonsterClear = true;
				foreach (var waveMonsterCall in playerWaveMonsterCall.Value)
				{
					MonsterWaveCallOnceComponent monsterWaveCallOnceComponent = waveMonsterCall.Value;
					self.duration = monsterWaveCallOnceComponent.duration;
					if (monsterWaveCallOnceComponent.ChkMonsterCallAllClear() == false)
					{
						playerWaveMonsterClear = false;
						break;
					}
					else
					{
					}
				}

				if (playerWaveMonsterClear == false)
				{
					allPlayerWaveMonsterClear = false;
					break;
				}
			}

			if (allPlayerWaveMonsterClear)
			{
				while (self.Children.Count > 0)
				{
					foreach (var child in self.Children)
					{
						child.Value.Dispose();
						break;
					}
				}
				self.waveMonsterCallList.Clear();

				if (self.curIndex == self.totalCount - 1)
				{
					self.GetGamePlayTowerDefense().TransToGameEnd().Coroutine();
				}
				else
				{
					self.GetGamePlayTowerDefense().TransToRestTime().Coroutine();
				}
				TimerComponent.Instance.Remove(ref self.Timer);
			}
		}
	}
}