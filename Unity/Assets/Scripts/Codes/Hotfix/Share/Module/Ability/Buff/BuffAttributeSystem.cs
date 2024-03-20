using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET.Ability
{
    [FriendOf(typeof (BuffObj))]
    public static class BuffAttributeSystem
    {
        public static (ET.AbilityConfig.NumericType, float) _GetAttributeChg(this BuffObj self, BuffActionModifyAttribute buffActionModifyAttribute, int stackCount)
        {
            float chgValue = buffActionModifyAttribute.BaseValue + buffActionModifyAttribute.StackValue * stackCount;
            float maxChgValue = buffActionModifyAttribute.MaxChgValue;
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
            return (buffActionModifyAttribute.NumericType, chgValue);
        }

        public static void AddBuffWhenModifyAttribute(this BuffObj self, BuffActionModifyAttribute buffActionModifyAttribute)
        {
            (AbilityConfig.NumericType numericType, float value) = self._GetAttributeChg(buffActionModifyAttribute, self.stack);
            NumericComponent numericComponent = self.GetUnit().GetComponent<NumericComponent>();
            float newValue = numericComponent.GetAsFloat((int)numericType) + value;
            numericComponent.SetAsFloat((int)numericType, newValue);
        }

        public static void ChgBuffStackCountWhenModifyAttribute(this BuffObj self, BuffActionModifyAttribute buffActionModifyAttribute, int oldStackCount, int newStackCount)
        {
            if (oldStackCount == 0)
            {
                return;
            }
            if (oldStackCount == newStackCount)
            {
                return;
            }
            (AbilityConfig.NumericType numericType, float oldValue) = self._GetAttributeChg(buffActionModifyAttribute, oldStackCount);
            (AbilityConfig.NumericType numericType2, float newValue) = self._GetAttributeChg(buffActionModifyAttribute, newStackCount);
            NumericComponent numericComponent = self.GetUnit().GetComponent<NumericComponent>();
            float value = numericComponent.GetAsFloat((int)numericType) - oldValue + newValue;
            numericComponent.SetAsFloat((int)numericType, value);
        }

        public static void RemoveBuffWhenModifyAttribute(this BuffObj self, BuffActionModifyAttribute buffActionModifyAttribute)
        {
            (AbilityConfig.NumericType numericType, float value) = self._GetAttributeChg(buffActionModifyAttribute, self.stack);
            NumericComponent numericComponent = self.GetUnit().GetComponent<NumericComponent>();
            float newValue = numericComponent.GetAsFloat((int)numericType) - value;
            numericComponent.SetAsFloat((int)numericType, newValue);
        }

    }
}