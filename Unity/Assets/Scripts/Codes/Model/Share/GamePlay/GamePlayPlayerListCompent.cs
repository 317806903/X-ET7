using System.Collections.Generic;
using System.Linq;
using ET.Ability;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
	[ComponentOf(typeof(GamePlayComponent))]
	public class GamePlayPlayerListComponent : Entity, IAwake, IDestroy, ITransferClient
	{
		/// <summary>
		/// playerId对应的unitId列表
		/// </summary>
		[BsonIgnore]
		public MultiMapSet<long, long> playerId2UnitIds;
		
		/// <summary>
		/// unitId对应playerId
		/// </summary>
		[BsonIgnore]
		public Dictionary<long, long> unitId2PlayerId;
		
		/// <summary>
		/// playerId是否已经退出战斗
		/// </summary>
		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public Dictionary<long, bool> playerId2IsQuit;
		
		/// <summary>
		/// playerId对应的出生点
		/// </summary>
		[BsonIgnore]
		public Dictionary<long, float3> playerId2BirthPos;
		
		/// <summary>
		/// playerId对应的货币列表
		/// </summary>
		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public MultiDictionary<long, string, int> playerId2CoinList;
	}
}