using System.Collections.Generic;
using System.Linq;

namespace ET
{
	[ComponentOf(typeof(GamePlayComponent))]
	public class GamePlayModeComponent : Entity, IAwake, IDestroy, IFixedUpdate
	{
		public string gamePlayModeCfgId;
	}
}