using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET.Ability
{
    [FriendOf(typeof (BuffObj))]
    public static class BuffAttributeByCircleWaveIndexSystem
    {
        public static (ET.AbilityConfig.NumericType, float) _GetAttributeChg(this BuffObj self, BuffActionModifyAttributeByCircleWaveIndex buffActionModifyAttributeByCircleWaveIndex, int stackCount)
        {
            Unit unit = self.GetUnit();
            MonsterComponent monsterComponent = unit.GetComponent<MonsterComponent>();
            if (monsterComponent == null)
            {
                return (buffActionModifyAttributeByCircleWaveIndex.NumericType, 0);
            }

            int circleWaveIndex = monsterComponent.circleWaveIndex;
            if (circleWaveIndex <= 0)
            {
                return (buffActionModifyAttributeByCircleWaveIndex.NumericType, 0);
            }

            float chgValue = buffActionModifyAttributeByCircleWaveIndex.BaseValue * circleWaveIndex + buffActionModifyAttributeByCircleWaveIndex.StackValue * stackCount * circleWaveIndex;
            float maxChgValue = buffActionModifyAttributeByCircleWaveIndex.MaxChgValue;
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
            return (buffActionModifyAttributeByCircleWaveIndex.NumericType, chgValue);
        }

        public static void AddBuffWhenModifyAttributeByCircleWaveIndex(this BuffObj self, BuffActionModifyAttributeByCircleWaveIndex buffActionModifyAttributeByCircleWaveIndex)
        {
            (AbilityConfig.NumericType numericType, float value) = self._GetAttributeChg(buffActionModifyAttributeByCircleWaveIndex, self.stack);
            NumericComponent numericComponent = self.GetUnit().GetComponent<NumericComponent>();
            float newValue = numericComponent.GetAsFloat((int)numericType) + value;
            numericComponent.SetAsFloat((int)numericType, newValue);
        }

        public static void ChgBuffStackCountWhenModifyAttributeByCircleWaveIndex(this BuffObj self, BuffActionModifyAttributeByCircleWaveIndex buffActionModifyAttributeByCircleWaveIndex, int oldStackCount, int newStackCount)
        {
            if (oldStackCount == 0)
            {
                return;
            }
            if (oldStackCount == newStackCount)
            {
                return;
            }
            (AbilityConfig.NumericType numericType, float oldValue) = self._GetAttributeChg(buffActionModifyAttributeByCircleWaveIndex, oldStackCount);
            (AbilityConfig.NumericType numericType2, float newValue) = self._GetAttributeChg(buffActionModifyAttributeByCircleWaveIndex, newStackCount);
            NumericComponent numericComponent = self.GetUnit().GetComponent<NumericComponent>();
            float value = numericComponent.GetAsFloat((int)numericType) - oldValue + newValue;
            numericComponent.SetAsFloat((int)numericType, value);
        }

        public static void RemoveBuffWhenModifyAttributeByCircleWaveIndex(this BuffObj self, BuffActionModifyAttributeByCircleWaveIndex buffActionModifyAttributeByCircleWaveIndex)
        {
            (AbilityConfig.NumericType numericType, float value) = self._GetAttributeChg(buffActionModifyAttributeByCircleWaveIndex, self.stack);
            NumericComponent numericComponent = self.GetUnit().GetComponent<NumericComponent>();
            float newValue = numericComponent.GetAsFloat((int)numericType) - value;
            numericComponent.SetAsFloat((int)numericType, newValue);
        }

    }
}