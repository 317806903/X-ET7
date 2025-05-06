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

            float chgValue = 0;
            {
                int waveIndex = monsterComponent.waveIndex;
                float chgValueByWaveIndex = buffActionModifyAttributeByWaveIndex.BaseValueWaveIndex * waveIndex + buffActionModifyAttributeByWaveIndex.StackValueWaveIndex * stackCount * waveIndex;
                chgValue += chgValueByWaveIndex;
            }
            {
                int stageWaveIndex = monsterComponent.stageWaveIndex;
                if (stageWaveIndex > 0)
                {
                    float chgValueByStageNum = buffActionModifyAttributeByWaveIndex.BaseValueStageNum * stageWaveIndex + buffActionModifyAttributeByWaveIndex.StackValueStageNum * stackCount * stageWaveIndex;
                    chgValue += chgValueByStageNum;
                }
            }
            {
                int circleWaveIndex = monsterComponent.circleWaveIndex;
                if (circleWaveIndex > 0)
                {
                    float chgValueByCircleWaveIndex = buffActionModifyAttributeByWaveIndex.BaseValueCircleWaveIndex * circleWaveIndex + buffActionModifyAttributeByWaveIndex.StackValueCircleWaveIndex * stackCount * circleWaveIndex;
                    chgValue += chgValueByCircleWaveIndex;
                }
            }
            {
                int circleIndex = monsterComponent.circleIndex;
                int circleNum = monsterComponent.circleNum;
                if (circleNum > 0)
                {
                    float chgValueByCircleNumIndex = buffActionModifyAttributeByWaveIndex.BaseValueCircleNumIndex * circleIndex + buffActionModifyAttributeByWaveIndex.StackValueCircleNumIndex * stackCount * circleIndex;
                    float chgValueByCircleNum = buffActionModifyAttributeByWaveIndex.BaseValueCircleNum * circleNum + buffActionModifyAttributeByWaveIndex.StackValueCircleNum * stackCount * circleNum;
                    chgValue += chgValueByCircleNumIndex + chgValueByCircleNum;
                }
            }

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

        public static bool ChgBuffStackCountWhenModifyAttributeByWaveIndex(this BuffObj self, BuffActionModifyAttributeByWaveIndex buffActionModifyAttributeByWaveIndex, int oldStackCount, int newStackCount)
        {
            if (oldStackCount == 0)
            {
                return false;
            }
            if (oldStackCount == newStackCount)
            {
                return false;
            }
            (AbilityConfig.NumericType numericType, float oldValue) = self._GetAttributeChg(buffActionModifyAttributeByWaveIndex, oldStackCount);
            (AbilityConfig.NumericType numericType2, float newValue) = self._GetAttributeChg(buffActionModifyAttributeByWaveIndex, newStackCount);
            NumericComponent numericComponent = self.GetUnit().GetComponent<NumericComponent>();
            float value = numericComponent.GetAsFloat((int)numericType) - oldValue + newValue;
            numericComponent.SetAsFloat((int)numericType, value);
            return true;
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