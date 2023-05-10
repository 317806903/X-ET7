using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Ability
{
    [ComponentOf(typeof(Unit))]
	public class MoveComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
        public float3 moveDirectionInput;
        public List<MoveObj> removeList;
    }
}