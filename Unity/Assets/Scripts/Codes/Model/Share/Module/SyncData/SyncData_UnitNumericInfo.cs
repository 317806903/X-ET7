using System.Collections.Generic;
namespace ET
{
    public class SyncData_UnitNumericInfo : Entity, IAwake, IDestroy
    {
        public List<long> unitId = new();
        public List<int> KVCount = new();
        public List<int> KVKey = new();
        public List<long> KVValue = new();

        public List<Unit> list = new();
    }
}