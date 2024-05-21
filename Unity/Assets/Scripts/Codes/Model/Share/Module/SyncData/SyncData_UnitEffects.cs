using System;
using System.Collections.Generic;

namespace ET
{
    public class SyncData_UnitEffects : Entity, IAwake, IDestroy
    {
        public List<long> unitId { get; set; }
        public List<bool> isAddOrRemove;
        public List<long> unitEffectObjIds;
        public List<byte[]> unitEffectComponents;
    }
}