using System.Collections.Generic;
using System.Diagnostics;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
    public static class SyncData_UnitFloatingTextSystem
    {
        public class AwakeSystem: AwakeSystem<SyncData_UnitFloatingText>
        {
            protected override void Awake(SyncData_UnitFloatingText self)
            {
            }
        }

        [ObjectSystem]
        public class SyncData_UnitFloatingTextDestroySystem: DestroySystem<SyncData_UnitFloatingText>
        {
            protected override void Destroy(SyncData_UnitFloatingText self)
            {
                self.unitId.Clear();
                self.floatingTextActionId.Clear();
                self.showNum.Clear();
                self.isOnlySelfShow.Clear();
                self.list.Clear();
            }
        }

        public static void Init(this SyncData_UnitFloatingText self, HashSet<(Unit unit, string floatingTextActionId, int showNum, bool isOnlySelfShow)> list)
        {
            self.unitId.Clear();
            self.floatingTextActionId.Clear();
            self.showNum.Clear();
            self.isOnlySelfShow.Clear();
            if (list == null)
            {
                return;
            }
            foreach ((Unit unit, string floatingTextActionId, int showNum, bool isOnlySelfShow) in list)
            {
                if (unit == null)
                {
                    continue;
                }
                self.unitId.Add(unit.Id);
                self.floatingTextActionId.Add(floatingTextActionId);
                self.showNum.Add(showNum);
                self.isOnlySelfShow.Add(isOnlySelfShow);
            }
        }

        public static void DealByBytes(this SyncData_UnitFloatingText self, UnitComponent unitComponent)
        {
            self.list.Clear();
            int count = self.unitId.Count;
            for (int i = 0; i < count; i++)
            {
                long unitId = self.unitId[i];
                string floatingTextActionId = self.floatingTextActionId[i];
                int showNum = self.showNum[i];
                bool isOnlySelfShow = self.isOnlySelfShow[i];
                Unit unit = unitComponent.Get(unitId);
                if (unit == null)
                {
                    continue;
                }
                self.list.Add((unit, floatingTextActionId, showNum, isOnlySelfShow));
            }

            EventType.SyncFloatingText _SyncFloatingText = new ()
            {
                list = self.list,
            };
            EventSystem.Instance.Publish(unitComponent.DomainScene(), _SyncFloatingText);
        }
    }
}
