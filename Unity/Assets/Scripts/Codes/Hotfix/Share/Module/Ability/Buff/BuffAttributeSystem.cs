using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET.Ability
{
    [FriendOf(typeof (BuffObj))]
    public static class BuffAttributeSystem
    {
        public static (ET.AbilityConfig.NumericType, float) GetAttributeChg(this BuffObj self, int stackCount)
        {
            BuffActionModifyAttribute buffActionModifyAttribute = self.buffAction as BuffActionModifyAttribute;
            float chgValue = buffActionModifyAttribute.BaseValue + buffActionModifyAttribute.StackValue * stackCount;
            return (buffActionModifyAttribute.NumericType, chgValue);
        }

        public static void AddBuffWhenModifyAttribute(this BuffObj self)
        {
            if (self.isEnabled == false)
            {
                return;
            }
            if (self.buffAction is BuffActionModifyAttribute == false)
            {
                return;
            }
            
            (AbilityConfig.NumericType numericType, float value) = self.GetAttributeChg(self.stack);
            NumericComponent numericComponent = self.GetUnit().GetComponent<NumericComponent>();
            float newValue = numericComponent.GetAsFloat((int)numericType) + value;
            numericComponent.Set((int)numericType, newValue);
        }
        
        public static void ChgBuffStackCountWhenModifyAttribute(this BuffObj self, int oldStackCount, int newStackCount)
        {
            if (self.isEnabled == false)
            {
                return;
            }
            if (self.buffAction is BuffActionModifyAttribute == false)
            {
                return;
            }

            if (oldStackCount == newStackCount)
            {
                return;
            }
            (AbilityConfig.NumericType numericType, float oldValue) = self.GetAttributeChg(oldStackCount);
            (AbilityConfig.NumericType numericType2, float newValue) = self.GetAttributeChg(newStackCount);
            NumericComponent numericComponent = self.GetUnit().GetComponent<NumericComponent>();
            float value = numericComponent.GetAsFloat((int)numericType) - oldValue + newValue;
            numericComponent.Set((int)numericType, value);
        }
        
        public static void RemoveBuffWhenModifyAttribute(this BuffObj self)
        {
            if (self.isEnabled == false)
            {
                return;
            }
            if (self.buffAction is BuffActionModifyAttribute == false)
            {
                return;
            }

            (AbilityConfig.NumericType numericType, float value) = self.GetAttributeChg(self.stack);
            NumericComponent numericComponent = self.GetUnit().GetComponent<NumericComponent>();
            float newValue = numericComponent.GetAsFloat((int)numericType) - value;
            numericComponent.Set((int)numericType, newValue);
        }

    }
}