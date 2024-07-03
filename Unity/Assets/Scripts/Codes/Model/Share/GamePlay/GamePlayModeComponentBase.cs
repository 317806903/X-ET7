using System.Collections.Generic;
using System.Linq;

namespace ET
{
	[ComponentOf(typeof(GamePlayComponent))]
	public class GamePlayModeComponentBase : Entity, IAwake, IDestroy, IFixedUpdate, IUpdate
	{
		public RoomTypeInfo roomTypeInfo;
		public string gamePlayModeCfgId;
	}
}