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

        public static DamageInfo Run(this DamageComponent self, long targetUnitId, Damage damage, float damageDegree, float criticalRate, DamageInfoTag[] tags)
        {
            DamageInfo damageInfo = self.AddChild<DamageInfo>();
            damageInfo.Init(targetUnitId, damage, damageDegree, criticalRate, tags);
            return damageInfo;
        }
        
        // ///<summary>
        // ///添加一个damageInfo
        // ///<param name="attacker">攻击者，可以为null</param>
        // ///<param name="target">挨打对象</param>
        // ///<param name="damage">基础伤害值</param>
        // ///<param name="damageDegree">伤害的角度</param>
        // ///<param name="criticalRate">暴击率，0-1</param>
        // ///<param name="tags">伤害信息类型</param>
        // ///</summary>
        // public void DoDamage(GameObject attacker, GameObject target, Damage damage, float damageDegree, float criticalRate, DamageInfoTag[] tags){
        //     this.damageInfos.Add(new DamageInfo(
        //         attacker, target, damage, damageDegree, criticalRate, tags
        //     ));
        // }


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