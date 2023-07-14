using System.Collections.Generic;
using System.Linq;
using ET.Ability;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ET
{
	[ComponentOf(typeof(GamePlayComponent))]
	public class GamePlayFriendTeamFlagCompent : Entity, IAwake, IDestroy, ITransferClient
	{
		/// <summary>
		/// 此阵营的友方信息
		/// </summary>
		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public Dictionary<TeamFlagType, int> teamFriendDic;
		
		/// <summary>
		/// playerId对应的阵营
		/// </summary>
		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public Dictionary<long, TeamFlagType> playerId2TeamFlag;
		
		/// <summary>
		/// unitId对应的阵营
		/// </summary>
		[BsonIgnore]
		public Dictionary<long, TeamFlagType> unitId2TeamFlag;
	}
}