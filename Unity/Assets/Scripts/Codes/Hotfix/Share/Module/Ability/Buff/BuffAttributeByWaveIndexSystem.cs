using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET.Ability
{
    [FriendOf(typeof (BuffObj))]
    public static class BuffAttributeByWaveIndexSystem
    {
        public static (ET.AbilityConfig.NumericType, float) _GetAttributeChg(this BuffObj self, BuffActionModifyAttributeByWaveIndex buffActionModifyAttributeByWaveIndex, int stackCount)
        {
            Unit unit = self.GetUnit();
            MonsterComponent monsterComponent = unit.GetComponent<MonsterComponent>();
            if (monsterComponent == null)
            {
                return (buffActionModifyAttributeByWaveIndex.NumericType, 0);
            }

            int waveIndex = monsterComponent.waveIndex;

            float chgValue = buffActionModifyAttributeByWaveIndex.BaseValue * waveIndex + buffActionModifyAttributeByWaveIndex.StackValue * stackCount * waveIndex;
            float maxChgValue = buffActionModifyAttributeByWaveIndex.MaxChgValue;
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
            return (buffActionModifyAttributeByWaveIndex.NumericType, chgValue);
        }

        public static void AddBuffWhenModifyAttributeByWaveIndex(this BuffObj self, BuffActionModifyAttributeByWaveIndex buffActionModifyAttributeByWaveIndex)
        {
            (AbilityConfig.NumericType numericType, float value) = self._GetAttributeChg(buffActionModifyAttributeByWaveIndex, self.stack);
            NumericComponent numericComponent = self.GetUnit().GetComponent<NumericComponent>();
            float newValue = numericComponent.GetAsFloat((int)numericType) + value;
            numericComponent.SetAsFloat((int)numericType, newValue);
        }

        public static void ChgBuffStackCountWhenModifyAttributeByWaveIndex(this BuffObj self, BuffActionModifyAttributeByWaveIndex buffActionModifyAttributeByWaveIndex, int oldStackCount, int newStackCount)
        {
            if (oldStackCount == 0)
            {
                return;
            }
            if (oldStackCount == newStackCount)
            {
                return;
            }
            (AbilityConfig.NumericType numericType, float oldValue) = self._GetAttributeChg(buffActionModifyAttributeByWaveIndex, oldStackCount);
            (AbilityConfig.NumericType numericType2, float newValue) = self._GetAttributeChg(buffActionModifyAttributeByWaveIndex, newStackCount);
            NumericComponent numericComponent = self.GetUnit().GetComponent<NumericComponent>();
            float value = numericComponent.GetAsFloat((int)numericType) - oldValue + newValue;
            numericComponent.SetAsFloat((int)numericType, value);
        }

        public static void RemoveBuffWhenModifyAttributeByWaveIndex(this BuffObj self, BuffActionModifyAttributeByWaveIndex buffActionModifyAttributeByWaveIndex)
        {
            (AbilityConfig.NumericType numericType, float value) = self._GetAttributeChg(buffActionModifyAttributeByWaveIndex, self.stack);
            NumericComponent numericComponent = self.GetUnit().GetComponent<NumericComponent>();
            float newValue = numericComponent.GetAsFloat((int)numericType) - value;
            numericComponent.SetAsFloat((int)numericType, newValue);
        }

    }
}