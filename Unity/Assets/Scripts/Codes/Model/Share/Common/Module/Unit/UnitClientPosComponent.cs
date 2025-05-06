using System.Collections.Generic;
using ET.Ability;
using Unity.Mathematics;

namespace ET
{
	[ComponentOf(typeof(Unit))]
	public class UnitClientPosComponent: Entity, IAwake, IDestroy
	{
		public float3 clientPosition;
		public long serverTime;
		public long targetPosClientTime;
		public float targetPosClientNeedTime;
	}
}