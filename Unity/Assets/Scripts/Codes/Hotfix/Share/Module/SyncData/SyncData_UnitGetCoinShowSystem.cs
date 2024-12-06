using System.Collections.Generic;
using System.Diagnostics;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
    public static class SyncData_UnitGetCoinShowSystem
    {
        public class AwakeSystem: AwakeSystem<SyncData_UnitGetCoinShow>
        {
            protected override void Awake(SyncData_UnitGetCoinShow self)
            {
            }
        }

        [ObjectSystem]
        public class SyncData_UnitGetCoinShowDestroySystem: DestroySystem<SyncData_UnitGetCoinShow>
        {
            protected override void Destroy(SyncData_UnitGetCoinShow self)
            {
                self.unitId.Clear();
                self.coinType.Clear();
                self.chgValue.Clear();
                self.list.Clear();
            }
        }

        public static void Init(this SyncData_UnitGetCoinShow self, List<(Unit unit, CoinTypeInGame coinType, int chgValue)> list)
        {
            self.unitId.Clear();
            self.coinType.Clear();
            self.chgValue.Clear();
            if (list == null)
            {
                return;
            }
            foreach ((Unit unit, CoinTypeInGame coinType, int chgValue) in list)
            {
                if (unit == null)
                {
                    continue;
                }
                self.unitId.Add(unit.Id);
                self.coinType.Add(coinType);
                self.chgValue.Add(chgValue);
            }
        }

        public static void DealByBytes(this SyncData_UnitGetCoinShow self, UnitComponent unitComponent)
        {
            self.list.Clear();
            int count = self.unitId.Count;
            for (int i = 0; i < count; i++)
            {
                long unitId = self.unitId[i];
                CoinTypeInGame coinType = self.coinType[i];
                int chgValue = self.chgValue[i];
                Unit unit = unitComponent.Get(unitId);
                if (unit == null)
                {
                    continue;
                }
                self.list.Add((unit, coinType, chgValue));
            }

            EventType.SyncGetCoinShow _SyncGetCoinShow = new ()
            {
                list = self.list
            };
            EventSystem.Instance.Publish(unitComponent.DomainScene(), _SyncGetCoinShow);
        }
    }
}