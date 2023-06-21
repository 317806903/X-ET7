using System.Collections.Generic;
using System.Linq;

namespace ET.Server
{
	[ComponentOf(typeof(Scene))]
	public class DynamicMapManagerComponent : Entity, IAwake, IDestroy
	{
		public HashSet<long> dynamicMapList;
		public HashSet<int> dynamicUsedIndex;
		public long RepeatedTimer;
	}
}