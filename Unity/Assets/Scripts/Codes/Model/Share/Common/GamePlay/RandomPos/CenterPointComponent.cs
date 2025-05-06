using System.Collections.Generic;
using System.Linq;
using ET.Ability;
using MongoDB.Bson.Serialization.Attributes;
using Unity.Mathematics;

namespace ET
{
	[ComponentOf(typeof(GamePlayComponent))]
	public class CenterPointComponent : Entity, IAwake, IDestroy
	{
		public float nearDis;
		public float3 centerPoint;
	}
}