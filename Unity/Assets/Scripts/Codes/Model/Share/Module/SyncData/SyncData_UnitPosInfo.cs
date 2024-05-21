using System.Collections.Generic;

namespace ET
{
    public class SyncData_UnitPosInfo : Entity, IAwake, IDestroy
    {
        public long serverTime;
        public List<long> unitId { get; set; }
        public List<int> posX;
        public List<int> posY;
        public List<int> posZ;
        public List<int> rotationX;
        public List<int> rotationY;
        public List<int> rotationZ;
        public List<int> rotationW;
    }
}