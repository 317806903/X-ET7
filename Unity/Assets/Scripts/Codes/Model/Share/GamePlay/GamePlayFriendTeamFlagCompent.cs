using System.Collections.Generic;
using System.Linq;
using ET.Ability;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

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

		[BsonIgnore]
		public List<float3> playerColorList = new()
		{
			new float3(0.2f, 0.46f, 1f),			//蓝
			new float3(0.4f, 1f, 0.75f),			//青
			new float3(0.75f, 0f, 0.75f),			//紫
			new float3(0.95f, 0.94f, 0.05f),		//黄
			new float3(1f, 0.42f, 0f),			//橙
			new float3(1f, 0.53f, 0.76f),			//粉
			new float3(0.64f, 0.71f, 0.28f),		//草绿
			new float3(0.4f, 0.85f, 0.97f),		//天蓝
			new float3(0f, 0.52f, 0.13f),			//绿
			new float3(0.65f, 0.42f, 0f),			//褐
		};

		/// <summary>
		/// playerId对应的颜色
		/// </summary>
		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public Dictionary<long, float3> playerId2Color;
	}
}