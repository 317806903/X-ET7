using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [ComponentOf(typeof(Scene))]
    public class SelectHandleRecordManager: Entity, IAwake, IDestroy, IFixedUpdate
    {
        public MultiMapSimple<long, long> time2ChildId;
        public DoubleMap<long, long> unitId2ChildId;
        public List<long> removeList;
    }
}