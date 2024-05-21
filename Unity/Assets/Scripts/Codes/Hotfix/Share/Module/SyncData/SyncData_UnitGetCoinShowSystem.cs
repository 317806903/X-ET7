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
                self.unitId = new();
                self.coinType = new();
                self.chgValue = new();
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
            }
        }

        public static void Init(this SyncData_UnitGetCoinShow self, List<(Unit unit, CoinType coinType, int chgValue)> list)
        {
            self.unitId.Clear();
            self.coinType.Clear();
            self.chgValue.Clear();
            if (list == null)
            {
                return;
            }
            foreach ((Unit unit, CoinType coinType, int chgValue) in list)
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

        public static async ETTask DealByBytes(this SyncData_UnitGetCoinShow self, UnitComponent unitComponent)
        {
            ListComponent<(Unit unit, CoinType coinType, int chgValue)> list = ListComponent<(Unit unit, CoinType coinType, int chgValue)>.Create();
            int count = self.unitId.Count;
            for (int i = 0; i < count; i++)
            {
                long unitId = self.unitId[i];
                CoinType coinType = self.coinType[i];
                int chgValue = self.chgValue[i];
                Unit unit = unitComponent.Get(unitId);
                if (unit == null)
                {
                    continue;
                }
                list.Add((unit, coinType, chgValue));
            }

            EventType.SyncGetCoinShow _SyncGetCoinShow = new ()
            {
                list = list
            };
            EventSystem.Instance.Publish(unitComponent.DomainScene(), _SyncGetCoinShow);
            await ETTask.CompletedTask;
        }
    }
}