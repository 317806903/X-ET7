using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET.Ability
{
    [FriendOf(typeof (BuffObj))]
    public static class BuffSkillAttributeSystem
    {
        public static (ET.AbilityConfig.SkillNumericType, float) _GetSkillAttributeChg(this BuffObj self, BuffActionModifySkillAttribute buffActionModifySkillAttribute, int stackCount)
        {
            float chgValue = buffActionModifySkillAttribute.BaseValue + buffActionModifySkillAttribute.StackValue * stackCount;
            float maxChgValue = buffActionModifySkillAttribute.MaxChgValue;
            if (maxChgValue != -1)
            {
                if (chgValue > 0)
                {
                    maxChgValue = Math.Abs(maxChgValue);
                    chgValue = Math.Min(chgValue, maxChgValue);
                }
                else
                {
                    maxChgValue = -Math.Abs(maxChgValue);
                    chgValue = Math.Max(chgValue, maxChgValue);
                }
            }
            return (buffActionModifySkillAttribute.NumericType, chgValue);
        }

        public static List<SkillObj> _GetSkillList(this BuffObj self, BuffActionModifySkillAttribute buffActionModifySkillAttribute)
        {
            string skillCfgId = buffActionModifySkillAttribute.SkillId;
            ET.AbilityConfig.SkillSlotType skillSlotType = buffActionModifySkillAttribute.SkillSlotType;
            int skillSlotIndex = buffActionModifySkillAttribute.SkillSlotIndex;
            ET.AbilityConfig.SkillGroupType skillGroupType = buffActionModifySkillAttribute.SkillGroupType;
            Unit unit = self.GetUnit();
            return ET.Ability.SkillHelper.GetSkillList(unit, skillCfgId, skillSlotType, skillSlotIndex, skillGroupType);
        }

        public static void AddBuffWhenModifySkillAttribute(this BuffObj self, BuffActionModifySkillAttribute buffActionModifySkillAttribute)
        {
            (AbilityConfig.SkillNumericType numericType, float value) = self._GetSkillAttributeChg(buffActionModifySkillAttribute, self.stack);
            foreach (SkillObj skillObj in self._GetSkillList(buffActionModifySkillAttribute))
            {
                NumericComponent numericComponent = skillObj.GetComponent<NumericComponent>();
                float newValue = numericComponent.GetAsFloat((int)numericType) + value;
                numericComponent.SetAsFloat((int)numericType, newValue);
                if (numericType == SkillNumericType.SkillCDBase ||
                    numericType == SkillNumericType.SkillCDAdd ||
                    numericType == SkillNumericType.SkillCDPct ||
                    numericType == SkillNumericType.SkillCDFinalAdd ||
                    numericType == SkillNumericType.SkillCDFinalPct)
                {
                    skillObj.ResetSkillCDCountDown();
                }
                else if (numericType == SkillNumericType.SkillDisBase ||
                         numericType == SkillNumericType.SkillDisAdd ||
                         numericType == SkillNumericType.SkillDisPct ||
                         numericType == SkillNumericType.SkillDisFinalAdd ||
                         numericType == SkillNumericType.SkillDisFinalPct)
                {
                    skillObj.ResetSkillDis();
                }
                else if (
                    numericType == SkillNumericType.TotalEnergyModifyAdd ||
                    numericType == SkillNumericType.TotalEnergyModifyPct ||
                    numericType == SkillNumericType.TotalEnergyModifyFinalAdd ||
                    numericType == SkillNumericType.TotalEnergyModifyFinalPct)
                {
                    skillObj.ResetSkillTotalEnergyNum();
                }
            }
        }

        public static bool ChgBuffStackCountWhenModifySkillAttribute(this BuffObj self, BuffActionModifySkillAttribute buffActionModifySkillAttribute, int oldStackCount, int newStackCount)
        {
            if (oldStackCount == 0)
            {
                return false;
            }
            if (oldStackCount == newStackCount)
            {
                return false;
            }
            (AbilityConfig.SkillNumericType numericType, float oldValue) = self._GetSkillAttributeChg(buffActionModifySkillAttribute, oldStackCount);
            (AbilityConfig.SkillNumericType numericType2, float newValue) = self._GetSkillAttributeChg(buffActionModifySkillAttribute, newStackCount);

            foreach (SkillObj skillObj in self._GetSkillList(buffActionModifySkillAttribute))
            {
                NumericComponent numericComponent = skillObj.GetComponent<NumericComponent>();
                float value = numericComponent.GetAsFloat((int)numericType) - oldValue + newValue;
                numericComponent.SetAsFloat((int)numericType, value);
                if (numericType == SkillNumericType.SkillCDBase ||
                    numericType == SkillNumericType.SkillCDAdd ||
                    numericType == SkillNumericType.SkillCDPct ||
                    numericType == SkillNumericType.SkillCDFinalAdd ||
                    numericType == SkillNumericType.SkillCDFinalPct)
                {
                    skillObj.ResetSkillCDCountDown();
                }
                else if (numericType == SkillNumericType.SkillDisBase ||
                         numericType == SkillNumericType.SkillDisAdd ||
                         numericType == SkillNumericType.SkillDisPct ||
                         numericType == SkillNumericType.SkillDisFinalAdd ||
                         numericType == SkillNumericType.SkillDisFinalPct)
                {
                    skillObj.ResetSkillDis();
                }
                else if (
                    numericType == SkillNumericType.TotalEnergyModifyAdd ||
                    numericType == SkillNumericType.TotalEnergyModifyPct ||
                    numericType == SkillNumericType.TotalEnergyModifyFinalAdd ||
                    numericType == SkillNumericType.TotalEnergyModifyFinalPct)
                {
                    skillObj.ResetSkillTotalEnergyNum();
                }
            }
            return true;
        }

        public static void RemoveBuffWhenModifySkillAttribute(this BuffObj self, BuffActionModifySkillAttribute buffActionModifySkillAttribute)
        {
            (AbilityConfig.SkillNumericType numericType, float value) = self._GetSkillAttributeChg(buffActionModifySkillAttribute, self.stack);
            foreach (SkillObj skillObj in self._GetSkillList(buffActionModifySkillAttribute))
            {
                NumericComponent numericComponent = skillObj.GetComponent<NumericComponent>();
                float newValue = numericComponent.GetAsFloat((int)numericType) - value;
                numericComponent.SetAsFloat((int)numericType, newValue);
                if (numericType == SkillNumericType.SkillCDBase ||
                    numericType == SkillNumericType.SkillCDAdd ||
                    numericType == SkillNumericType.SkillCDPct ||
                    numericType == SkillNumericType.SkillCDFinalAdd ||
                    numericType == SkillNumericType.SkillCDFinalPct)
                {
                    skillObj.ResetSkillCDCountDown();
                }
                else if (numericType == SkillNumericType.SkillDisBase ||
                    numericType == SkillNumericType.SkillDisAdd ||
                    numericType == SkillNumericType.SkillDisPct ||
                    numericType == SkillNumericType.SkillDisFinalAdd ||
                    numericType == SkillNumericType.SkillDisFinalPct)
                {
                    skillObj.ResetSkillDis();
                }
                else if (
                    numericType == SkillNumericType.TotalEnergyModifyAdd ||
                    numericType == SkillNumericType.TotalEnergyModifyPct ||
                    numericType == SkillNumericType.TotalEnergyModifyFinalAdd ||
                    numericType == SkillNumericType.TotalEnergyModifyFinalPct)
                {
                    skillObj.ResetSkillTotalEnergyNum();
                }
            }
        }

    }
}