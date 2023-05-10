using System.Collections.Generic;

namespace ET.Ability
{
    [ComponentOf(typeof(Unit))]
	public class EventHandlerComponent: Entity, IAwake, IDestroy
    {
    }
}