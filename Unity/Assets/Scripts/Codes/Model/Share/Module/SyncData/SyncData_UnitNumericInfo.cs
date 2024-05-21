using System.Collections.Generic;
namespace ET
{
    public class SyncData_UnitNumericInfo : Entity, IAwake, IDestroy
    {
        public List<long> unitId { get; set; }
        public List<int> KVCount;
        public List<int> KVKey;
        public List<long> KVValue;
    }
}