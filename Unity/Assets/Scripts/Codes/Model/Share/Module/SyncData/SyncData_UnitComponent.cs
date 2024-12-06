using System;
using System.Collections.Generic;

namespace ET
{
    public class SyncData_UnitComponent : Entity, IAwake, IDestroy
    {
        public List<long> unitId = new();
        public List<int> unitComponentCount = new();
        public List<byte[]> unitComponents = new();
        public List<long> deleteUnitComponents = new();
    }
}