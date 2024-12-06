using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using Unity.Mathematics;

namespace ET
{
	[ComponentOf(typeof(GamePlayComponent))]
	public class GamePlayPkComponent : GamePlayModeComponentBase
	{
		[BsonIgnore]
		public bool isInitClient;
		[BsonIgnore]
		public long lastSendTime;
		[BsonIgnore]
		public GamePlayPKCfg model
		{
			get
			{
				return GamePlayPKCfgCategory.Instance.Get(this.gamePlayModeCfgId);
			}
		}

		public long ownerPlayerId { get; set; }

	}
}