using System.Collections.Generic;
using System.Diagnostics;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
    public static class SyncData_DamageShowSystem
    {
        public class AwakeSystem: AwakeSystem<SyncData_DamageShow>
        {
            protected override void Awake(SyncData_DamageShow self)
            {
                self.unitId = new();
                self.damageValue = new();
                self.isCrt = new();
            }
        }

        [ObjectSystem]
        public class SyncData_DamageShowDestroySystem: DestroySystem<SyncData_DamageShow>
        {
            protected override void Destroy(SyncData_DamageShow self)
            {
                self.unitId.Clear();
                self.damageValue.Clear();
                self.isCrt.Clear();
            }
        }

        public static void Init(this SyncData_DamageShow self, List<(Unit unit, int damageValue, bool isCrt)> list)
        {
            self.unitId.Clear();
            self.damageValue.Clear();
            self.isCrt.Clear();
            if (list == null)
            {
                return;
            }
            foreach ((Unit unit, int damageValue, bool isCrt) in list)
            {
                if (unit == null)
                {
                    continue;
                }
                self.unitId.Add(unit.Id);
                self.damageValue.Add(damageValue);
                self.isCrt.Add(isCrt);
            }
        }

        public static async ETTask DealByBytes(this SyncData_DamageShow self, UnitComponent unitComponent)
        {
            ListComponent<(Unit unit, int damageValue, bool isCrt)> list = ListComponent<(Unit unit, int damageValue, bool isCrt)>.Create();
            int count = self.unitId.Count;
            for (int i = 0; i < count; i++)
            {
                long unitId = self.unitId[i];
                int damageValue = self.damageValue[i];
                bool isCrt = self.isCrt[i];
                Unit unit = unitComponent.Get(unitId);
                if (unit == null)
                {
                    continue;
                }
                list.Add((unit, damageValue, isCrt));
            }

            EventType.SyncDamageShow _SyncDamageShow = new ()
            {
                list = list
            };
            EventSystem.Instance.Publish(unitComponent.DomainScene(), _SyncDamageShow);
            await ETTask.CompletedTask;
        }
    }
}