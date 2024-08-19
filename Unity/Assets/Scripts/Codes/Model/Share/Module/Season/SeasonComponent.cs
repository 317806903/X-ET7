using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ET
{
	[ComponentOf(typeof(SeasonManagerComponent))]
	public class SeasonComponent : Entity, IAwake, IDestroy, ISerializeToEntity
	{
		public int seasonIndex;
		public int seasonCfgId;
		[BsonIgnore]
		public SeasonInfoCfg cfg
		{
			get
			{
				return SeasonInfoCfgCategory.Instance.Get(this.seasonCfgId);
			}
		}
		public long initTime;
		public long startTime;
		public long endTime;
	}
}