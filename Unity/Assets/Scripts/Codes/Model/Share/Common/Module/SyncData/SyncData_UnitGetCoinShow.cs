using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET
{
    public class SyncData_UnitGetCoinShow : Entity, IAwake, IDestroy
    {
        public List<long> unitId = new();
        public List<CoinTypeInGame> coinType = new();
        public List<int> chgValue = new();

        public List<(Unit unit, CoinTypeInGame coinType, int chgValue)> list = new();
    }
}