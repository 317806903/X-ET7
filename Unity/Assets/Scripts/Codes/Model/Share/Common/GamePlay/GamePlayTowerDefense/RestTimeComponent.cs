using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
	[ComponentOf(typeof(GamePlayTowerDefenseComponent))]
	public class RestTimeComponent : Entity, IAwake, IDestroy, IFixedUpdate, ITransferClient
	{
		public float duration;
		public float timeElapsed = 0;

		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public Dictionary<long, bool> isPlayerReadyForBattle;
	}
}