using System;
using System.Collections.Generic;

namespace ET.Ability
{
	[ComponentOf(typeof(Scene))]
	public class ActionHandlerComponent: Entity, IAwake, IDestroy
	{
        [StaticField]
		public static ActionHandlerComponent Instance;
		public Dictionary<string, Action<ActionHandlerComponent, string, long, long>> dic;
	}
}