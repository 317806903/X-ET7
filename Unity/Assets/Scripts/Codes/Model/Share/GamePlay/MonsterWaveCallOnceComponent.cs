using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
	[ComponentOf(typeof(MonsterWaveCallComponent))]
	public class MonsterWaveCallOnceComponent : Entity, IAwake, IDestroy, IFixedUpdate
	{
		public string monsterWaveRule;
		public int waveIndex;
		public float duration;
		public float timeElapsed = 0;
		public Dictionary<long, string> unitId2MonsterCfgId;
		public Dictionary<long, int> unitId2RewardGold;
		public Dictionary<MonsterWaveCallNode, bool> monsterWaveCallIsFinished;
	}
}