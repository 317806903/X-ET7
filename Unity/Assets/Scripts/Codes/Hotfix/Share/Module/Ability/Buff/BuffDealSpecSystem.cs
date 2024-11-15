﻿using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    public static class BuffDealSpecSystem
    {
        public static void DealSpecWhenAddBuff(this BuffObj self, bool forceDeal)
        {
            if (self.isEnabled == false && forceDeal == false)
            {
                return;
            }
            bool isAttributeChg = false;
            foreach (BuffAction buffAction in self.buffActions)
            {
                if (buffAction is BuffActionModifyAttribute buffActionModifyAttribute)
                {
                    isAttributeChg = true;
                    self.AddBuffWhenModifyAttribute(buffActionModifyAttribute);
                }
                else if (buffAction is BuffActionModifyAttributeByWaveIndex buffActionModifyAttributeByWaveIndex)
                {
                    isAttributeChg = true;
                    self.AddBuffWhenModifyAttributeByWaveIndex(buffActionModifyAttributeByWaveIndex);
                }
                else if (buffAction is BuffActionModifySkillAttribute buffActionModifySkillAttribute)
                {
                    self.AddBuffWhenModifySkillAttribute(buffActionModifySkillAttribute);
                }
                else if (buffAction is BuffActionModifyMotion buffActionModifyMotion)
                {
                    self.AddBuffWhenModifyMotion(buffActionModifyMotion);
                }
            }

            if (isAttributeChg)
            {
                self.KeepHpLessMaxHp();
            }
        }

        public static void DealSpecWhenRemoveBuff(this BuffObj self, bool forceDeal)
        {
            if (self.isEnabled == false && forceDeal == false)
            {
                return;
            }

            bool isAttributeChg = false;
            foreach (BuffAction buffAction in self.buffActions)
            {
                if (buffAction is BuffActionModifyAttribute buffActionModifyAttribute)
                {
                    isAttributeChg = true;
                    self.RemoveBuffWhenModifyAttribute(buffActionModifyAttribute);
                }
                else if (buffAction is BuffActionModifyAttributeByWaveIndex buffActionModifyAttributeByWaveIndex)
                {
                    isAttributeChg = true;
                    self.RemoveBuffWhenModifyAttributeByWaveIndex(buffActionModifyAttributeByWaveIndex);
                }
                else if (buffAction is BuffActionModifySkillAttribute buffActionModifySkillAttribute)
                {
                    self.RemoveBuffWhenModifySkillAttribute(buffActionModifySkillAttribute);
                }
                else if (buffAction is BuffActionModifyMotion buffActionModifyMotion)
                {
                    self.RemoveBuffWhenModifyMotion(buffActionModifyMotion);
                }
            }
            if (isAttributeChg)
            {
                self.KeepHpLessMaxHp();
            }
        }

        public static void DealSpecWhenSetEnable(this BuffObj self, bool isEnabled)
        {
            if (isEnabled)
            {
                self.DealSpecWhenAddBuff(true);
            }
            else
            {
                self.DealSpecWhenRemoveBuff(true);
            }
        }

        public static void DealSpecWhenChgBuffStackCount(this BuffObj self, int oldStackCount, int newStackCount)
        {
            if (self.isEnabled == false)
            {
                return;
            }

            bool isAttributeChg = false;
            foreach (BuffAction buffAction in self.buffActions)
            {
                if (buffAction is BuffActionModifyAttribute buffActionModifyAttribute)
                {
                    isAttributeChg = true;
                    self.ChgBuffStackCountWhenModifyAttribute(buffActionModifyAttribute, oldStackCount, newStackCount);
                }
                else if (buffAction is BuffActionModifyAttributeByWaveIndex buffActionModifyAttributeByWaveIndex)
                {
                    isAttributeChg = true;
                    self.ChgBuffStackCountWhenModifyAttributeByWaveIndex(buffActionModifyAttributeByWaveIndex, oldStackCount, newStackCount);
                }
                else if (buffAction is BuffActionModifySkillAttribute buffActionModifySkillAttribute)
                {
                    self.ChgBuffStackCountWhenModifySkillAttribute(buffActionModifySkillAttribute, oldStackCount, newStackCount);
                }
                else if (buffAction is BuffActionModifyMotion buffActionModifyMotion)
                {
                    self.ChgBuffStackCountWhenModifyMotion(buffActionModifyMotion, oldStackCount, newStackCount);
                }
            }

            if (isAttributeChg)
            {
                self.KeepHpLessMaxHp();
            }
        }

        public static async ETTask DealSelfEffectWhenAddBuff(this BuffObj self)
        {
            if (self.model.SelfEffectList_Ref.Count == 0)
            {
                return;
            }
            if (self.selfEffectList == null)
            {
                self.selfEffectList = new();
            }
            foreach (ActionCfg_EffectCreate actionCfgEffectCreate in self.model.SelfEffectList_Ref)
            {
                EffectObj effectObj = ET.Ability.EffectHelper.AddSelfEffect(self.GetUnit(), actionCfgEffectCreate);
                if (effectObj != null)
                {
                    self.selfEffectList.Add(effectObj);
                }

                while (IdGenerater.Instance.ChkGenerateIdFull())
                {
                    await TimerComponent.Instance.WaitFrameAsync();
                }
            }
        }

        public static void DealSelfEffectWhenRemoveBuff(this BuffObj self)
        {
            if (self.selfEffectList == null)
            {
                return;
            }
            foreach (EffectObj effectObj in self.selfEffectList)
            {
                if (effectObj != null)
                {
                    effectObj.WillDestroy();
                }
            }
            self.selfEffectList.Clear();
        }

        public static void DealSelfEffectWhenSetEnable(this BuffObj self, bool isEnabled)
        {
            if (isEnabled)
            {
                self.DealSelfEffectWhenAddBuff().Coroutine();
            }
            else
            {
                self.DealSelfEffectWhenRemoveBuff();
            }
        }

        public static void KeepHpLessMaxHp(this BuffObj self)
        {
            NumericComponent numericComponent = self.GetUnit().GetComponent<NumericComponent>();
            numericComponent.KeepHpLessMaxHp();
        }

    }
}