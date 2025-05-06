using System.Collections.Generic;

namespace ET
{
    public class SyncData_UnitBaseInfo : Entity, IAwake, IDestroy
    {
        public long serverTime;
        public List<long> unitId = new();
        public List<string> unitCfgId = new();
        public List<int> level = new();
        public List<int> unitType = new();
        public List<int> posX = new();
        public List<int> posY = new();
        public List<int> posZ = new();
        public List<int> rotationX = new();
        public List<int> rotationY = new();
        public List<int> rotationZ = new();
        public List<int> rotationW = new();
    }
}