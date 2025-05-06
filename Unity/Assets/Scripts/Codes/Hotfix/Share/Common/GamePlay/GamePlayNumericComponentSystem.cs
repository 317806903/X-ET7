using System;
using System.Collections.Generic;
using ET.Ability;
using ET.AbilityConfig;

namespace ET
{
    [FriendOf(typeof (GamePlayNumericComponent))]
    public static class GamePlayNumericComponentSystem
    {
        [ObjectSystem]
        public class GamePlayNumericComponentAwakeSystem: AwakeSystem<GamePlayNumericComponent>
        {
            protected override void Awake(GamePlayNumericComponent self)
            {
                self.playerId2Numeric = new();
                self.homeTeamFlag2Numeric = new();
            }
        }

        [ObjectSystem]
        public class GamePlayNumericComponentDestroySystem: DestroySystem<GamePlayNumericComponent>
        {
            protected override void Destroy(GamePlayNumericComponent self)
            {
                self.playerId2Numeric.Clear();
                self.homeTeamFlag2Numeric.Clear();
            }
        }

        public static void ChgPlayerNumeric(this GamePlayNumericComponent self, long playerId, GameNumericType numericType, float chgValue, bool isReset)
        {
            NumericComponent numericComponent;
            if (self.playerId2Numeric.TryGetValue(playerId, out long childId) == false)
            {
                numericComponent = self.AddChild<NumericComponent>();
                self.playerId2Numeric.Add(playerId, numericComponent.Id);
            }
            else
            {
                numericComponent = self.GetChild<NumericComponent>(childId);
            }
            float newValue;
            if (isReset)
            {
                newValue = chgValue;
            }
            else
            {
                newValue = numericComponent.GetAsFloat((int)numericType) + chgValue;
            }
            numericComponent.SetAsFloat((int)numericType, newValue);
        }

        public static float GetPlayerNumeric(this GamePlayNumericComponent self, long playerId, GameNumericType numericType)
        {
            if (self.playerId2Numeric.TryGetValue(playerId, out long childId) == false)
            {
                return 0;
            }
            NumericComponent numericComponent = self.GetChild<NumericComponent>(childId);
            if (numericComponent == null)
            {
                return 0;
            }

            return numericComponent.GetAsFloat((int)numericType);
        }

        public static void ChgHomeTeamFlagTypeNumeric(this GamePlayNumericComponent self, TeamFlagType teamFlagType, GameNumericType numericType, float chgValue, bool isReset)
        {
            NumericComponent numericComponent;
            if (self.homeTeamFlag2Numeric.TryGetValue(teamFlagType, out long childId) == false)
            {
                numericComponent = self.AddChild<NumericComponent>();
                self.homeTeamFlag2Numeric.Add(teamFlagType, numericComponent.Id);
            }
            else
            {
                numericComponent = self.GetChild<NumericComponent>(childId);
            }
            float newValue;
            if (isReset)
            {
                newValue = chgValue;
            }
            else
            {
                newValue = numericComponent.GetAsFloat((int)numericType) + chgValue;
            }
            numericComponent.SetAsFloat((int)numericType, newValue);
        }

        public static float GetHomeTeamFlagTypeNumeric(this GamePlayNumericComponent self, TeamFlagType teamFlagType, GameNumericType numericType)
        {
            if (self.homeTeamFlag2Numeric.TryGetValue(teamFlagType, out long childId) == false)
            {
                return 0;
            }
            NumericComponent numericComponent = self.GetChild<NumericComponent>(childId);
            if (numericComponent == null)
            {
                return 0;
            }

            return numericComponent.GetAsFloat((int)numericType);
        }

    }
}