using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;

namespace ET
{
	[ComponentOf(typeof(GamePlayTowerDefenseComponent))]
	public class RestTimeComponent : Entity, IAwake, IDestroy, IFixedUpdate, ITransferClient
	{
		public float duration;
		public float timeElapsed = 0;
	}
}