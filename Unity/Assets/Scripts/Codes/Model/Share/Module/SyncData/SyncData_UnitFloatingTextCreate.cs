using System.Collections.Generic;

namespace ET
{
    public class SyncData_UnitFloatingText : Entity, IAwake, IDestroy
    {
        public List<long> unitId = new();
        public List<string> floatingTextActionId = new();
        public List<int> showNum = new();
        public List<bool> isOnlySelfShow = new();

        public List<(Unit unit, string floatingTextActionId, int showNum, bool isOnlySelfShow)> list = new();
    }
}