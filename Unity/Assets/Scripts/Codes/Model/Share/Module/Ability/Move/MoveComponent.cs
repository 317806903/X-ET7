using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Ability
{
    [ComponentOf(typeof(Unit))]
	public class MoveComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
    }
}