using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
	[ComponentOf(typeof(GamePlayTowerDefenseComponent))]
	public class MonsterWaveCallComponent : Entity, IAwake, IDestroy, ITransferClient
	{
		[BsonIgnore]
		public long Timer;
		public string monsterWaveRule { get; set; }
		/// <summary>
		/// 总刷怪波数
		/// </summary>
		public int totalCount;
		/// <summary>
		/// 剩余时间
		/// </summary>
		public float duration;
		/// <summary>
		/// 按顺序存放波次配置对应 waveIndex(假设配置不连续)
		/// </summary>
		public List<int> sortWaveIndex;
		/// <summary>
		/// 波次序号，表示当前是哪一波(按照0,1,2,3..连续数字来计算的)
		/// </summary>
		public int curIndex;

		public int circleWaveIndex;
		public int circleNum;
		public int circleIndex;
		/// <summary>
		/// 波次序号对应刷怪逻辑
		/// </summary>
		[BsonIgnore]
		public Dictionary<long, Dictionary<int, MonsterWaveCallOnceComponent>> waveMonsterCallList;
	}
}