using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
	[ComponentOf(typeof(GamePlayTowerDefenseComponent))]
	public class GameRecoverOnceComponent : Entity, IAwake, IDestroy, IFixedUpdate, ITransferClient
	{
		public GameRecoverType gameRecoverType;

		public long timeOutTime;
		public int recoverCostArcadeCoinNum;

		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public Dictionary<long, PlayerRecoverStatus> playerRecoverStatusDic;
	}
}