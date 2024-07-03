using System.Collections.Generic;
using System.Linq;
using ET.Ability;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
	[ComponentOf(typeof(GamePlayComponent))]
	public class GamePlayNumericComponent : Entity, IAwake, IDestroy, IFixedUpdate
	{
		[BsonIgnore]
		public Dictionary<long, long> playerId2Numeric;
		[BsonIgnore]
		public Dictionary<TeamFlagType, long> homeTeamFlag2Numeric;
	}
}