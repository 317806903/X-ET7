using System;
using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof(DamageComponent))]
    [FriendOf(typeof(DamageInfo))]
    public static class DamageComponentSystem
    {
        [ObjectSystem]
        public class DamageComponentAwakeSystem: AwakeSystem<DamageComponent>
        {
            protected override void Awake(DamageComponent self)
            {
            }
        }

        [ObjectSystem]
        public class DamageComponentDestroySystem: DestroySystem<DamageComponent>
        {
            protected override void Destroy(DamageComponent self)
            {
            }
        }
        
        [ObjectSystem]
        public class DamageComponentFixedUpdateSystem: FixedUpdateSystem<DamageComponent>
        {
            protected override void FixedUpdate(DamageComponent self)
            {
                self.FixedUpdate();
            }
        }

        public static DamageInfo Add(this DamageComponent self, Unit attackerUnit, Unit targetUnit, Damage damage, float damageDegree, float 
        criticalRate, DamageSourceTag[] tags)
        {
            DamageInfo damageInfo = self.AddChild<DamageInfo>();
            damageInfo.Init(attackerUnit.Id, targetUnit.Id, damage, damageDegree, criticalRate, tags);
            return damageInfo;
        }
        
        public static void FixedUpdate(this DamageComponent self)
        {
            if (self.Children.Count <= 0)
            {
                return;
            }

            while (self.Children.Count > 0)
            {
                foreach (var damageInfos in self.Children)
                {
                    DamageInfo damageInfo = damageInfos.Value as DamageInfo;
                    damageInfo.DealWithDamage();
                    damageInfo.Dispose();
                    break;
                }
            }
        }
    }
}