using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof(MonsterWaveCallOnceComponent))]
    public static class MonsterWaveCallOnceComponentSystem
	{
		[ObjectSystem]
		public class MonsterWaveCallOnceComponentAwakeSystem : AwakeSystem<MonsterWaveCallOnceComponent>
		{
			protected override void Awake(MonsterWaveCallOnceComponent self)
			{
				self.unitId2MonsterCfgId = new();
				self.unitId2RewardGold = new();
				self.monsterWaveCallIsFinished = new();
			}
		}
	
		[ObjectSystem]
		public class MonsterWaveCallOnceComponentDestroySystem : DestroySystem<MonsterWaveCallOnceComponent>
		{
			protected override void Destroy(MonsterWaveCallOnceComponent self)
			{
				self.unitId2MonsterCfgId.Clear();
				self.unitId2RewardGold.Clear();
				self.monsterWaveCallIsFinished.Clear();
			}
		}

		[ObjectSystem]
		public class MonsterWaveCallOnceComponentFixedUpdateSystem: FixedUpdateSystem<MonsterWaveCallOnceComponent>
		{
			protected override void FixedUpdate(MonsterWaveCallOnceComponent self)
			{
				if (self.DomainScene().SceneType != SceneType.Map)
				{
					return;
				}

				float fixedDeltaTime = TimeHelper.FixedDetalTime;
				self.FixedUpdate(fixedDeltaTime);
			}
		}
		
		public static void Init(this MonsterWaveCallOnceComponent self, string monsterWaveRule, int index)
		{
			self.monsterWaveRule = monsterWaveRule;
			self.waveIndex = index;
			MonsterWaveCallCfg monsterWaveCallCfg = MonsterWaveCallCfgCategory.Instance.Get(self.monsterWaveRule, self.waveIndex);
			self.duration = monsterWaveCallCfg.Duration;
			self.timeElapsed = 0;
		}

		public static void FixedUpdate(this MonsterWaveCallOnceComponent self, float fixedDeltaTime)
		{
			if (self.duration > 0)
			{
				self.duration -= fixedDeltaTime;
			}
			
            float wasTimeElapsed = self.timeElapsed;
            self.timeElapsed += fixedDeltaTime;

            MonsterWaveCallCfg monsterWaveCallCfg = MonsterWaveCallCfgCategory.Instance.Get(self.monsterWaveRule, self.waveIndex);
            int count = monsterWaveCallCfg.Nodes.Count;
            //执行时间点内的事情
            for (int i = 0; i < count; i++)
            {
	            MonsterWaveCallNode monsterWaveCallNode = monsterWaveCallCfg.Nodes[i];
	            if (monsterWaveCallNode.TimeElapsed >= wasTimeElapsed)
	            {
		            self.monsterWaveCallIsFinished[monsterWaveCallNode] = false;
	            }
	            
                if (
	                monsterWaveCallNode.TimeElapsed < self.timeElapsed &&
	                monsterWaveCallNode.TimeElapsed >= wasTimeElapsed
                )
                {
	                self.CallMonster(monsterWaveCallNode).Coroutine();
                }
            }
            
		}

		public static float3 GetCallMonsterPosition(this MonsterWaveCallOnceComponent self)
		{
			return self.GetParent<MonsterWaveCallComponent>().GetCallMonsterPosition();
		}
		
		public static async ETTask CallMonster(this MonsterWaveCallOnceComponent self, MonsterWaveCallNode monsterWaveCallNode)
		{
			int leftNum = monsterWaveCallNode.TotalNum;
			while (leftNum > 0)
			{
				for (int i = 0; i < monsterWaveCallNode.OnceCallNum; i++)
				{
					Unit monsterUnit = self.CallMonsterOnce(monsterWaveCallNode.MonsterCfgId, monsterWaveCallNode.Level);
					self.unitId2MonsterCfgId.Add(monsterUnit.Id, monsterWaveCallNode.MonsterCfgId);
					self.unitId2RewardGold.Add(monsterUnit.Id, monsterWaveCallNode.RewardGold);
					
					leftNum -= 1;
					if (leftNum == 0)
					{
						break;
					}
				}

				await TimerComponent.Instance.WaitAsync((long) (monsterWaveCallNode.OnceIntervalTime * 1000));
			}

			self.monsterWaveCallIsFinished[monsterWaveCallNode] = true;
		}

		public static Unit CallMonsterOnce(this MonsterWaveCallOnceComponent self, string monsterCfgId, int level)
		{
			float3 pos = self.GetCallMonsterPosition();
			float3 randomPos = pos + new float3(RandomGenerator.RandFloat01(), 0, RandomGenerator.RandFloat01());
			float3 randomForward = new float3(RandomGenerator.RandFloat01(), 0, RandomGenerator.RandFloat01());
			Unit monsterUnit = UnitHelper_Create.CreateWhenServer_MonsterUnit(self.DomainScene(), monsterCfgId, level, randomPos, randomForward);
			return monsterUnit;
		}

		public static bool ChkMonsterCallFinish(this MonsterWaveCallOnceComponent self)
		{
			if (self.timeElapsed == 0)
			{
				return false;
			}
			foreach (var monsterWaveCall in self.monsterWaveCallIsFinished)
			{
				if (monsterWaveCall.Value == false)
				{
					return false;
				}
			}

			return true;
		}

		public static bool ChkMonsterCallAllClear(this MonsterWaveCallOnceComponent self)
		{
			if (self.ChkMonsterCallFinish() == false)
			{
				return false;
			}
			foreach (var unitId2RewardGold in self.unitId2RewardGold)
			{
				long unitId = unitId2RewardGold.Key;
				if (UnitHelper.ChkUnitAlive(self.DomainScene(), unitId))
				{
					return false;
				}
			}

			return true;
		}

	}
}