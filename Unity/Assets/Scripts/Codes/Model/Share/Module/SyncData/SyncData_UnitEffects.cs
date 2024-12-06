using System;
using System.Collections.Generic;

namespace ET
{
    public class SyncData_UnitEffects : Entity, IAwake, IDestroy
    {
        public List<long> unitId = new();
        public List<bool> isAddOrRemove = new();
        public List<long> unitEffectObjIds = new();
        public List<byte[]> unitEffectComponents = new();
    }
}