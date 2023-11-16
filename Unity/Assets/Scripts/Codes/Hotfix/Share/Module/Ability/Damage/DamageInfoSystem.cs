using System;
using System.Collections.Generic;
using Unity.Mathematics;

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

        public static void Init(this DamageInfo self, long attackerUnitId, long targetUnitId, Damage damage, float damageDegree, float criticalRate, DamageSourceTag[] tags)
        {
            self.attackerUnitId = attackerUnitId;
            self.defenderUnitId = targetUnitId;
            self.damage = damage;
        }

        ///<summary>
        ///这里再结合是否闪避，是否暴击进行处理
        ///</summary>
        public static int DamageValue(this DamageInfo self){
            return self.damage.Overall();
        }

        /// <summary>
        /// 获取攻击者对应 的对象(player或monster)
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Unit GetAttackerPlayerUnit(this DamageInfo self)
        {
            Unit unit = GetAttackerUnit(self);
            return UnitHelper.GetCasterActorUnit(unit);
        }

        /// <summary>
        /// 获取攻击者
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Unit GetAttackerUnit(this DamageInfo self)
        {
            Scene scene = self.DomainScene();
            Unit unit = UnitHelper.GetUnit(scene, self.attackerUnitId);
            return unit;
        }

        /// <summary>
        /// 获取受击者
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Unit GetDefenderUnit(this DamageInfo self)
        {
            Scene scene = self.DomainScene();
            Unit unit = UnitHelper.GetUnit(scene, self.defenderUnitId);
            return unit;
        }

        public static void DealWithDamage(this DamageInfo self)
        {
            Scene scene = self.DomainScene();
            //如果目标已经挂了，就直接return了
            if (UnitHelper.ChkUnitAlive(scene, self.defenderUnitId) == false)
                return;

            Unit attackerUnit = self.GetAttackerUnit();
            Unit defenderUnit = self.GetDefenderUnit();
            EventSystem.Instance.Publish(scene, new AbilityTriggerEventType.DamageBeforeOnHit()
                {
                    attackerUnit = attackerUnit,
                    defenderUnit = defenderUnit,
                    damageInfo = self,
                });
            EventSystem.Instance.Publish(scene, new AbilityTriggerEventType.DamageAfterOnHit()
                {
                    attackerUnit = attackerUnit,
                    defenderUnit = defenderUnit,
                    damageInfo = self,
                });
            if (self.CanBeKilledByDamageInfo(defenderUnit) == true)
            {
                EventSystem.Instance.Publish(scene, new AbilityTriggerEventType.DamageBeforeOnKill()
                    {
                        attackerUnit = attackerUnit,
                        defenderUnit = defenderUnit,
                        damageInfo = self,
                    });
            }

            int damageValue = self.DamageValue();

            NumericComponent numericComponent = defenderUnit.GetComponent<NumericComponent>();
            int curHp = numericComponent.GetAsInt(NumericType.Hp);
            int maxHp = numericComponent.GetAsInt(NumericType.MaxHp);
            int newHp = math.min(math.max(0, curHp - damageValue), maxHp);
            numericComponent.SetAsInt(NumericType.HpBase, newHp);

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

            if (UnitHelper.ChkUnitAlive(defenderUnit) == false)
            {
                EventSystem.Instance.Publish(scene, new AbilityTriggerEventType.DamageAfterOnKill()
                {
                    attackerUnit = attackerUnit,
                    defenderUnit = defenderUnit,
                    damageInfo = self,
                });
                defenderUnit.DestroyWithDeathShow();

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

        public static bool CanBeKilledByDamageInfo(this DamageInfo self, Unit targetUnit)
        {
            NumericComponent numericComponent = targetUnit.GetComponent<NumericComponent>();
            int curHp = numericComponent.GetAsInt(NumericType.Hp);
            int damageValue = self.damage.Overall();
            if (damageValue >= curHp)
            {
                return true;
            }
            return false;
        }
    }
}