using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
	[ComponentOf(typeof(GamePlayComponent))]
	public class GamePlayPKComponent : GamePlayModeComponent
	{
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