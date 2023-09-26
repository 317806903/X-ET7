using System.Collections.Generic;
using System.ComponentModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET.AbilityConfig
{
	public partial class AICfgCategory
	{
		[ProtoIgnore]
		[BsonIgnore]
		public Dictionary<string, SortedDictionary<int, AICfg>> AIConfigs = new();

		public SortedDictionary<int, AICfg> GetAI(string aiCfgId)
		{
			return this.AIConfigs[aiCfgId];
		}

		partial void PostResolve()
		{
			foreach (var aICfg in this.DataList)
			{
				SortedDictionary<int, AICfg> aiNodeConfig;
				if (!this.AIConfigs.TryGetValue(aICfg.AIConfigId, out aiNodeConfig))
				{
					aiNodeConfig = new SortedDictionary<int, AICfg>();
					this.AIConfigs.Add(aICfg.AIConfigId, aiNodeConfig);
				}
				
				aiNodeConfig.Add(aICfg.Order, aICfg);
			}
		}
	}
}
