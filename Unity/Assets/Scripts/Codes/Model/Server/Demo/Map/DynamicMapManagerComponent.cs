using System.Collections.Generic;
using System.Linq;

namespace ET.Server
{
	[ComponentOf(typeof(Scene))]
	public class DynamicMapManagerComponent : Entity, IAwake, IDestroy
	{
		public Dictionary<long, long> dynamicMapList;
		public HashSet<int> dynamicUsedIndex;
		public long RepeatedTimer;
	}
}