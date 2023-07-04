using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;

namespace ET
{
	[ComponentOf(typeof(GamePlayComponent))]
	public class PutMonsterCallComponent : Entity, IAwake, IDestroy, ITransferClient
	{
		public float3 MonsterCallPos { get; set; }
		private EntityRef<Unit> monsterCallUnit;
		public Unit MonsterCallUnit
		{
			get
			{
				return this.monsterCallUnit;
			}
			set
			{
				this.monsterCallUnit = value;
			}
		}
	}
}