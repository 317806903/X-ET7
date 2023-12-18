using System;
using System.Collections.Generic;

namespace ET.Ability
{
	[ComponentOf(typeof(Scene))]
	public class ActionPlayerHandlerComponent: Entity, IAwake, IDestroy
	{
		public Dictionary<string, IActionPlayerHandler> dic;
	}
}