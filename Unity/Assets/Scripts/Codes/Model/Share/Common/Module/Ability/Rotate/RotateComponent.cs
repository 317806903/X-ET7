using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Ability
{
    [ComponentOf(typeof(Unit))]
	public class RotateComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
        public float rotateDirectionInput;
        public List<RotateObj> removeList;
    }
}