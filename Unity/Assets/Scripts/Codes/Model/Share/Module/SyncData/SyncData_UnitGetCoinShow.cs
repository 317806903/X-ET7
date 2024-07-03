using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET
{
    public class SyncData_UnitGetCoinShow : Entity, IAwake, IDestroy
    {
        public List<long> unitId { get; set; }
        public List<CoinTypeInGame> coinType;
        public List<int> chgValue;
    }
}