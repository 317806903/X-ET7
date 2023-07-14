using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using Unity.Mathematics;

namespace ET
{
	[ComponentOf(typeof(GamePlayTowerDefenseComponent))]
	public class PutHomeComponent : Entity, IAwake, IDestroy, ITransferClient
	{
		[BsonIgnore]
		public long Timer;
		public float3 HomePos { get; set; }
		private EntityRef<Unit> homeUnit;
		[BsonIgnore]
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