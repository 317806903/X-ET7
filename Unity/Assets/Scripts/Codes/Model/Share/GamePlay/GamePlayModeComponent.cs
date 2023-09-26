using System.Collections.Generic;
using System.Linq;

namespace ET
{
	[ComponentOf(typeof(GamePlayComponent))]
	public class GamePlayModeComponent : Entity, IAwake, IDestroy, IFixedUpdate, IUpdate
	{
		public string gamePlayModeCfgId;
	}
}