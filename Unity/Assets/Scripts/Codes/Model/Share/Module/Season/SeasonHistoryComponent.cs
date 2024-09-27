using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ET
{
	[ChildOf(typeof(SeasonComponent))]
	public class SeasonHistoryComponent : Entity, IAwake, IDestroy
	{
		public int seasonIndex;
		public int seasonCfgId;
		public string startTime;
		public string endTime;
		public string initTime;
		public string recordTime;
	}
}