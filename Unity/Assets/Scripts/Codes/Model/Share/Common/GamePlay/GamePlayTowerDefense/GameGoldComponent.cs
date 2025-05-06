using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
	[ComponentOf(typeof(GamePlayTowerDefenseComponent))]
	public class GameGoldComponent : Entity, IAwake, IDestroy, IFixedUpdate
	{
		public long nextIncreaseTime;
		public int increaseNum;
		public Dictionary<long, float> playerId2TeamScale;

		public int initNumOfGoldGrowth;
        public int numOfGoldGrowthTeam1;
		public int numOfGoldGrowthTeam2;
	}
}
