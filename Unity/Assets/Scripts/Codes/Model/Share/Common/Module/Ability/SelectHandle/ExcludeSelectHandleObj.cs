using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Ability
{
    [ComponentOf(typeof(Unit))]
    public class ExcludeSelectHandleObj: Entity, IAwake, IDestroy
    {
        public HashSet<long> excludeUnitList;
    }
}