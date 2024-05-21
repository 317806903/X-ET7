using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET
{
    public class SyncData_DamageShow : Entity, IAwake, IDestroy
    {
        public List<long> unitId { get; set; }
        public List<int> damageValue;
        public List<bool> isCrt;
    }
}