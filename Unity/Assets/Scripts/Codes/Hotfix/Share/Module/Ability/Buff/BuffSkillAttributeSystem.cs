using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET.Ability
{
    [FriendOf(typeof (BuffObj))]
    public static class BuffSkillAttributeSystem
    {
        public static (ET.AbilityConfig.NumericType, float) _GetSkillAttributeChg(this BuffObj self, BuffActionModifySkillAttribute buffActionModifySkillAttribute, int stackCount)
        {
            float chgValue = buffActionModifySkillAttribute.BaseValue + buffActionModifySkillAttribute.StackValue * stackCount;
            float maxChgValue = buffActionModifySkillAttribute.MaxChgValue;
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
            return (buffActionModifySkillAttribute.NumericType, chgValue);
        }

        public static List<SkillObj> _GetSkillList(this BuffObj self, BuffActionModifySkillAttribute buffActionModifySkillAttribute)
        {
            string skillCfgId = buffActionModifySkillAttribute.SkillId;
            ET.AbilityConfig.SkillSlotType skillSlotType = buffActionModifySkillAttribute.SkillSlotType;
            Unit unit = self.GetUnit();
            return ET.Ability.SkillHelper.GetSkillList(unit, skillCfgId, (ET.Ability.SkillSlotType)skillSlotType);
        }

        public static void AddBuffWhenModifySkillAttribute(this BuffObj self, BuffActionModifySkillAttribute buffActionModifySkillAttribute)
        {
            (AbilityConfig.NumericType numericType, float value) = self._GetSkillAttributeChg(buffActionModifySkillAttribute, self.stack);
            foreach (SkillObj skillObj in self._GetSkillList(buffActionModifySkillAttribute))
            {
                NumericComponent numericComponent = skillObj.GetComponent<NumericComponent>();
                float newValue = numericComponent.GetAsFloat((int)numericType) + value;
                numericComponent.SetAsFloat((int)numericType, newValue);
            }
        }

        public static void ChgBuffStackCountWhenModifySkillAttribute(this BuffObj self, BuffActionModifySkillAttribute buffActionModifySkillAttribute, int oldStackCount, int newStackCount)
        {
            if (oldStackCount == newStackCount)
            {
                return;
            }
            (AbilityConfig.NumericType numericType, float oldValue) = self._GetSkillAttributeChg(buffActionModifySkillAttribute, oldStackCount);
            (AbilityConfig.NumericType numericType2, float newValue) = self._GetSkillAttributeChg(buffActionModifySkillAttribute, newStackCount);

            foreach (SkillObj skillObj in self._GetSkillList(buffActionModifySkillAttribute))
            {
                NumericComponent numericComponent = skillObj.GetComponent<NumericComponent>();
                float value = numericComponent.GetAsFloat((int)numericType) - oldValue + newValue;
                numericComponent.SetAsFloat((int)numericType, value);
            }
        }

        public static void RemoveBuffWhenModifySkillAttribute(this BuffObj self, BuffActionModifySkillAttribute buffActionModifySkillAttribute)
        {
            (AbilityConfig.NumericType numericType, float value) = self._GetSkillAttributeChg(buffActionModifySkillAttribute, self.stack);
            foreach (SkillObj skillObj in self._GetSkillList(buffActionModifySkillAttribute))
            {
                NumericComponent numericComponent = skillObj.GetComponent<NumericComponent>();
                float newValue = numericComponent.GetAsFloat((int)numericType) - value;
                numericComponent.SetAsFloat((int)numericType, newValue);
            }
        }

    }
}