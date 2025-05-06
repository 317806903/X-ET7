using System.Collections.Generic;
using System.ComponentModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET.AbilityConfig
{
	public partial class SeasonBringUpCfgCategory
	{
		[ProtoIgnore]
		[BsonIgnore]
		public Dictionary<string, int> SeasonBringUpCfgMaxLevel = new();


		public int GetMaxLevel(string seasonBringUpCfgId)
		{
			if (this.SeasonBringUpCfgMaxLevel.TryGetValue(seasonBringUpCfgId, out var maxLevel))
			{
				return maxLevel;
			}
			return 0;
		}

		public SeasonBringUpCfg GetSeasonBringUpCfg(string seasonBringUpCfgId, int level)
		{
			return this.Get(seasonBringUpCfgId, level);
		}

		public SeasonBringUpCfg GetNextSeasonBringUpCfg(string seasonBringUpCfgId, int level)
		{
			return this.Get(seasonBringUpCfgId, level + 1);
		}

        partial void PostResolve()
		{
			foreach (var seasonBringUpCfg in this.DataList)
			{
				string id = seasonBringUpCfg.Id;
				if (this.SeasonBringUpCfgMaxLevel.TryGetValue(id, out var totalNum))
				{
				}
				if (seasonBringUpCfg.Level > totalNum)
				{
					this.SeasonBringUpCfgMaxLevel[id] = seasonBringUpCfg.Level;
				}
			}
		}
	}
}
