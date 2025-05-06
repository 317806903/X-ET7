using System.Collections.Generic;
using System.Linq;
using ET.Ability;
using MongoDB.Bson.Serialization.Attributes;
using Unity.Mathematics;

namespace ET
{
	[ComponentOf(typeof(RandomPointManagerComponent))]
	public class MapRandomPointComponent : Entity, IAwake, IDestroy
	{
		public List<float3> randomPointList;
		public float pointDis;
	}
}