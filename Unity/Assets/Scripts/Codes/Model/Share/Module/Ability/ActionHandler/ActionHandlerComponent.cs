using System;
using System.Collections.Generic;

namespace ET.Ability
{
	[ComponentOf(typeof(Scene))]
	public class ActionHandlerComponent: Entity, IAwake, IDestroy
	{
		public Dictionary<string, IActionHandler> dic;
		public Dictionary<string, IActionHandler> actionId2ActionHandle;
	}
}