using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
	[Invoke(TimerInvokeType.ChkMonsterWaveCallAllClear)]
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
			}
		}
	
		[ObjectSystem]
		public class MonsterWaveCallComponentDestroySystem : DestroySystem<MonsterWaveCallComponent>
		{
			protected override void Destroy(MonsterWaveCallComponent self)
			{
				TimerComponent.Instance.Remove(ref self.Timer);
				self.waveMonsterCallList?.Clear();
			}
		}

		public static void Init(this MonsterWaveCallComponent self, string monsterWaveRule)
		{
			self.monsterWaveRule = monsterWaveRule;

			self.totalCount = 0;
			List<MonsterWaveCallCfg> list = MonsterWaveCallCfgCategory.Instance.DataList;
			
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

		public static float3 GetCallMonsterPosition(this MonsterWaveCallComponent self)
		{
			return self.GetParent<GamePlayComponent>().GetCallMonsterPosition();
		}

		public static void DoNextMonsterWaveCall(this MonsterWaveCallComponent self)
		{
			if (self.curIndex == -1)
			{
				self.Timer = TimerComponent.Instance.NewRepeatedTimer(500, TimerInvokeType.ChkMonsterWaveCallAllClear, self);
			}
			self.curIndex++;
			self.DoMonsterWaveCall();
		}

		public static void DoMonsterWaveCall(this MonsterWaveCallComponent self)
		{
			MonsterWaveCallOnceComponent monsterWaveCallOnceComponent = self.AddChildWithId<MonsterWaveCallOnceComponent>(self.curIndex);
			int waveIndex = self.sortWaveIndex[self.curIndex];
			monsterWaveCallOnceComponent.Init(self.monsterWaveRule, waveIndex);
			
			self.duration = monsterWaveCallOnceComponent.duration;
			self.waveMonsterCallList.Add(self.curIndex, monsterWaveCallOnceComponent);
		}

		public static void ChkMonsterCallAllClear(this MonsterWaveCallComponent self)
		{
			foreach (var waveMonsterCall in self.waveMonsterCallList)
			{
				MonsterWaveCallOnceComponent monsterWaveCallOnceComponent = waveMonsterCall.Value;
				self.duration = monsterWaveCallOnceComponent.duration;
				if (monsterWaveCallOnceComponent.ChkMonsterCallAllClear())
				{
					monsterWaveCallOnceComponent.Dispose();
					self.waveMonsterCallList.Remove(waveMonsterCall.Key);
					
					if (self.curIndex == self.totalCount - 1)
					{
						self.GetParent<GamePlayComponent>().TransToGameSuccess();
					}
					else
					{
						self.GetParent<GamePlayComponent>().TransToRestTime();
					}

					return;
				}
			}
		}
	}
}