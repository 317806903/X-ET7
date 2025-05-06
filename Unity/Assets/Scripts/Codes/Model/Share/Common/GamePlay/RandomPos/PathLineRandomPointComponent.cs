using System.Collections.Generic;
using System.Linq;
using ET.Ability;
using MongoDB.Bson.Serialization.Attributes;
using Unity.Mathematics;

namespace ET
{
	[ComponentOf(typeof(GamePlayComponent))]
	public class PathLineRandomPointComponent : Entity, IAwake, IDestroy
	{
		public bool isInit;
		public List<float3> randomPointList;
		public float pointDis;
		public float nearDis;
	}
}