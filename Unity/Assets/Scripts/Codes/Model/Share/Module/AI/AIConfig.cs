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
		public Dictionary<string, SortedDictionary<string, AICfg>> AIConfigs = new();

		public SortedDictionary<string, AICfg> GetAI(string aiCfgId)
		{
			return this.AIConfigs[aiCfgId];
		}

		partial void PostResolve()
		{
			foreach (var kv in this.GetAll())
			{
				SortedDictionary<string, AICfg> aiNodeConfig;
				if (!this.AIConfigs.TryGetValue(kv.Value.AIConfigId, out aiNodeConfig))
				{
					aiNodeConfig = new SortedDictionary<string, AICfg>();
					this.AIConfigs.Add(kv.Value.AIConfigId, aiNodeConfig);
				}
				
				aiNodeConfig.Add(kv.Key, kv.Value);
			}
		}
	}
}
