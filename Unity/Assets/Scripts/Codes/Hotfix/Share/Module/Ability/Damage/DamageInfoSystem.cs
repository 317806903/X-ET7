using System;
using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof (DamageInfo))]
    public static class DamageInfoSystem
    {
        [ObjectSystem]
        public class DamageInfoAwakeSystem: AwakeSystem<DamageInfo>
        {
            protected override void Awake(DamageInfo self)
            {
            }
        }

        [ObjectSystem]
        public class DamageInfoDestroySystem: DestroySystem<DamageInfo>
        {
            protected override void Destroy(DamageInfo self)
            {
            }
        }

        public static void Init(this DamageInfo self, long targetUnitId, Damage damage, float damageDegree, float criticalRate, DamageInfoTag[] tags)
        {
        }
        
        
        public static bool isHeal(this DamageInfo self){
            return false;
        }
        
        ///<summary>
        ///从策划脚本获得最终的伤害值
        ///</summary>
        public static int DamageValue(this DamageInfo self, bool asHeal){
            return 111;
        }
        
        public static void DealWithDamage(this DamageInfo self)
        {
            //如果目标已经挂了，就直接return了
            if (UnitHelper.ChkUnitAlive(self.defenderUnitId) == false)
                return;

            Unit defenderUnit = UnitHelper.GetUnit(self.defenderUnitId);
            EventSystem.Instance.Publish(self.DomainScene(), new AbilityTriggerEventType.DamageBeforeOnHit()
                {
                    damageInfoId = self.Id,
                    attackerUnitId = self.attackerUnitId,
                    defenderUnitId = self.defenderUnitId,
                });
            EventSystem.Instance.Publish(self.DomainScene(), new AbilityTriggerEventType.DamageAfterOnHit()
                {
                    damageInfoId = self.Id,
                    attackerUnitId = self.attackerUnitId,
                    defenderUnitId = self.defenderUnitId,
                });
            if (defenderUnit.CanBeKilledByDamageInfo(self) == true)
            {
                EventSystem.Instance.Publish(self.DomainScene(), new AbilityTriggerEventType.DamageBeforeOnKill()
                    {
                        damageInfoId = self.Id,
                        attackerUnitId = self.attackerUnitId,
                        defenderUnitId = self.defenderUnitId,
                    });
            }

            //最后根据结果处理：如果是治疗或者角色非无敌，才会对血量进行调整。
            bool isHeal = self.isHeal();
            int dVal = self.DamageValue(isHeal);
            //if (isHeal == true)
            {
                // if (self.requireDoHurt() == true && defenderChaState.CanBeKilledByDamageInfo(self) == false)
                // {
                //     UnitAnim ua = defenderChaState.GetComponent<UnitAnim>();
                //     if (ua) ua.Play("Hurt");
                // }
                //
                // defenderChaState.ModResource(new ChaResource(-dVal));
                // //按游戏设计的规则跳数字，如果要有暴击，也可以丢在策划脚本函数（lua可以返回多参数）也可以随便怎么滴
                // SceneVariants.PopUpNumberOnCharacter(self.defender, Mathf.Abs(dVal), isHeal);
            }

            //if (false)
            {
                EventSystem.Instance.Publish(self.DomainScene(), new AbilityTriggerEventType.DamageAfterOnKill()
                {
                    damageInfoId = self.Id,
                    attackerUnitId = self.attackerUnitId,
                    defenderUnitId = self.defenderUnitId,
                });
            }
            //
            // //伤害流程走完，添加buff
            // for (int i = 0; i < self.addBuffs.Count; i++)
            // {
            //     GameObject toCha = self.addBuffs[i].targetUnitId;
            //     ChaState toChaState = toCha.Equals(self.attackerUnitId)? attackerChaState : defenderChaState;
            //
            //     if (toChaState != null && toChaState.dead == false)
            //     {
            //         toChaState.AddBuff(self.addBuffs[i]);
            //     }
            // }
        }
    }
}