using System;
using System.Collections.Generic;

namespace ET.Ability
{
	[ComponentOf(typeof(Scene))]
	public class ActionGameHandlerComponent: Entity, IAwake, IDestroy
	{
		public Dictionary<string, IActionGameHandler> dic;
		public HashSet<string> actionIdIsChk;
		public Dictionary<string, IActionGameHandler> actionId2ActionHandle;
	}
}