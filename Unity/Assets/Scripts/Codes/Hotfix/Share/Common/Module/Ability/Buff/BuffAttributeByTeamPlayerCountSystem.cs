using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET.Ability
{
    [FriendOf(typeof (BuffObj))]
    public static class BuffAttributeByTeamPlayerCountSystem
    {
        public static (ET.AbilityConfig.NumericType, float) _GetAttributeChg(this BuffObj self, BuffActionModifyAttributeByTeamPlayerCount buffActionModifyAttributeByTeamPlayerCount, int stackCount)
        {
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(self.DomainScene());
            if (gamePlayComponent == null)
            {
                return (buffActionModifyAttributeByTeamPlayerCount.NumericType, 0);
            }

            Unit unit = self.GetUnit();
            TeamFlagType teamFlagType = TeamFlagHelper.GetTeamFlag(unit);
            TeamFlagType homeTeamFlagType = GamePlayHelper.GetHomeTeamFlagType(teamFlagType);

            List<long> playerList = gamePlayComponent.GetPlayerList();
            int playerCount = 0;
            foreach (long playerId in playerList)
            {
                TeamFlagType homeTeamFlagTypeTmp = GamePlayHelper.GetHomeTeamFlagTypeByPlayer(self.DomainScene(), playerId);
                if (homeTeamFlagType == homeTeamFlagTypeTmp)
                {
                    playerCount++;
                }
            }

            float chgValue = 0;
            {
                int teamPlayerCount = playerCount - 1;
                float chgValueByTeamPlayerCount = buffActionModifyAttributeByTeamPlayerCount.BaseValueTeamPlayerCount * teamPlayerCount + buffActionModifyAttributeByTeamPlayerCount.StackValueTeamPlayerCount * stackCount * teamPlayerCount;
                chgValue += chgValueByTeamPlayerCount;
            }

            float maxChgValue = buffActionModifyAttributeByTeamPlayerCount.MaxChgValue;
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
            return (buffActionModifyAttributeByTeamPlayerCount.NumericType, chgValue);
        }

        public static void AddBuffWhenModifyAttributeByTeamPlayerCount(this BuffObj self, BuffActionModifyAttributeByTeamPlayerCount buffActionModifyAttributeByTeamPlayerCount)
        {
            (AbilityConfig.NumericType numericType, float value) = self._GetAttributeChg(buffActionModifyAttributeByTeamPlayerCount, self.stack);
            NumericComponent numericComponent = self.GetUnit().GetComponent<NumericComponent>();
            float newValue = numericComponent.GetAsFloat((int)numericType) + value;
            numericComponent.SetAsFloat((int)numericType, newValue);
        }

        public static bool ChgBuffStackCountWhenModifyAttributeByTeamPlayerCount(this BuffObj self, BuffActionModifyAttributeByTeamPlayerCount buffActionModifyAttributeByTeamPlayerCount, int oldStackCount, int newStackCount)
        {
            if (oldStackCount == 0)
            {
                return false;
            }
            if (oldStackCount == newStackCount)
            {
                return false;
            }
            (AbilityConfig.NumericType numericType, float oldValue) = self._GetAttributeChg(buffActionModifyAttributeByTeamPlayerCount, oldStackCount);
            (AbilityConfig.NumericType numericType2, float newValue) = self._GetAttributeChg(buffActionModifyAttributeByTeamPlayerCount, newStackCount);
            NumericComponent numericComponent = self.GetUnit().GetComponent<NumericComponent>();
            float value = numericComponent.GetAsFloat((int)numericType) - oldValue + newValue;
            numericComponent.SetAsFloat((int)numericType, value);
            return true;
        }

        public static void RemoveBuffWhenModifyAttributeByTeamPlayerCount(this BuffObj self, BuffActionModifyAttributeByTeamPlayerCount buffActionModifyAttributeByTeamPlayerCount)
        {
            (AbilityConfig.NumericType numericType, float value) = self._GetAttributeChg(buffActionModifyAttributeByTeamPlayerCount, self.stack);
            NumericComponent numericComponent = self.GetUnit().GetComponent<NumericComponent>();
            float newValue = numericComponent.GetAsFloat((int)numericType) - value;
            numericComponent.SetAsFloat((int)numericType, newValue);
        }

    }
}