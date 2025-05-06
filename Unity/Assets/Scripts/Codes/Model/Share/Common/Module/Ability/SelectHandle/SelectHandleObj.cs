using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Ability
{
    [ComponentOf(typeof(Unit))]
    public class SelectHandleObj: Entity, IAwake, IDestroy
    {
        public SelectHandle selectHandle;
        public bool isOnce;

    }
}