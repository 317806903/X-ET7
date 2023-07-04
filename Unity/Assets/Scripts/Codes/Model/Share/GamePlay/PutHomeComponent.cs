using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;

namespace ET
{
	[ComponentOf(typeof(GamePlayComponent))]
	public class PutHomeComponent : Entity, IAwake, IDestroy, ITransferClient
	{
		public float3 HomePos { get; set; }
		private EntityRef<Unit> homeUnit;
		public Unit HomeUnit
		{
			get
			{
				return this.homeUnit;
			}
			set
			{
				this.homeUnit = value;
			}
		}
	}
}