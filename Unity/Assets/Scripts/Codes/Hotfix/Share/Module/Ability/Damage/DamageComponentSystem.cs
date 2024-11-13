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
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static DamageInfo Add(this DamageComponent self, Unit attackerUnit, Unit targetUnit, Damage damage, bool isCrit, ref ActionContext actionContext)
        {
            DamageInfo damageInfo = self.AddChild<DamageInfo>();
            damageInfo.Init(attackerUnit.Id, targetUnit.Id, damage, isCrit, ref actionContext);
            return damageInfo;
        }

        public static void FixedUpdate(this DamageComponent self, float fixedDeltaTime)
        {
            if (self.Children.Count <= 0)
            {
                return;
            }

            while (self.Children.Count > 0)
            {
                foreach (var obj in self.Children.Values)
                {
                    DamageInfo damageInfo = obj as DamageInfo;
                    damageInfo.DealWithDamage();
                    damageInfo.Dispose();
                    break;
                }
            }
        }
    }
}