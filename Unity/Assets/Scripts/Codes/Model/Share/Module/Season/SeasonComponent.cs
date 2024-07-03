using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ET
{
	public enum SeasonStatus
	{
		/// <summary>
		/// 赛季中
		/// </summary>
		InSeason,
		/// <summary>
		/// 赛季结算中
		/// </summary>
		SettlementSeason,
		/// <summary>
		/// 赛季等待中
		/// </summary>
		WaitingNewSeason,
	}
	[ComponentOf(typeof(Scene))]
	public class SeasonComponent : Entity, IAwake, IDestroy
	{
		public int seasonId;
		[BsonIgnore]
		public SeasonInfoCfg cfg
		{
			get
			{
				return SeasonInfoCfgCategory.Instance.Get(this.seasonId);
			}
		}
		public long startTime;
		[BsonIgnore]
		public long endTime
		{
			get
			{
				return cfg.EndTime;
			}
		}
		public SeasonStatus seasonStatus;

		public List<(int seasonId, long startTime, long endTime)> seasonList;
	}
}