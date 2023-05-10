using System.Collections.Generic;

namespace ET.Ability
{
	[ComponentOf(typeof(Scene))]
	public class DamageComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
    }
}