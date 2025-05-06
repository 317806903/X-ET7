using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET
{
    public class SyncData_DamageShow : Entity, IAwake, IDestroy
    {
        public List<long> unitId = new();
        public List<int> damageValue = new();
        public List<bool> isCrt = new();

        public List<(Unit unit, int damageValue, bool isCrt)> list = new();
    }
}