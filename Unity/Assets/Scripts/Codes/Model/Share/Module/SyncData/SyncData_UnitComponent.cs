using System;
using System.Collections.Generic;

namespace ET
{
    public class SyncData_UnitComponent : Entity, IAwake, IDestroy
    {
        public List<long> unitId { get; set; }
        public List<int> unitComponentCount;
        public List<byte[]> unitComponents;
    }
}