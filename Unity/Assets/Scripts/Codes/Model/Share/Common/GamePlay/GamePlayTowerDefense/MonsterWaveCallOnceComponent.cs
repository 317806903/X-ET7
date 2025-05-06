using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using Unity.Mathematics;

namespace ET
{
	[ComponentOf(typeof(MonsterWaveCallComponent))]
	public class MonsterWaveCallOnceComponent : Entity, IAwake, IDestroy, IFixedUpdate
	{
		public long playerId;
		public string monsterWaveRuleCfgId;
		public int waveIndex;
		public float monsterWaveNumScalePercent;
		public float monsterWaveLevelScalePercent;
		public float waveRewardGoldScalePercent;
		public float duration;
		public float timeElapsed = 0;
		public HashSet<long> monsterWaveUnitList;
		[BsonIgnore]
		public Dictionary<MonsterWaveCallNode, bool> monsterWaveCallIsFinished;

		public bool waitingDestroy;
	}
}