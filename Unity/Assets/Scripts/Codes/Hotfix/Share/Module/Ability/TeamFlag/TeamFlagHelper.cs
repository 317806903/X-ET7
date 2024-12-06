using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class TeamFlagHelper
    {
        public static void AddTeamFlag(long playerId, Unit unit, TeamFlagType teamFlagType)
        {
            TeamFlagComponent TeamFlagComponent = unit.GetComponent<TeamFlagComponent>();
            if (TeamFlagComponent == null)
            {
                TeamFlagComponent = unit.AddComponent<TeamFlagComponent>();
            }
            TeamFlagComponent.AddTeamFlag(playerId, teamFlagType);
        }

        public static TeamFlagType GetTeamFlag(Unit unit)
        {
            TeamFlagComponent TeamFlagComponent = unit.GetComponent<TeamFlagComponent>();
            if (TeamFlagComponent == null)
            {
                return TeamFlagType.None;
            }

            return TeamFlagComponent.GetTeamFlag();
        }

        public static long GetPlayerId(Unit unit)
        {
            TeamFlagComponent TeamFlagComponent = unit.GetComponent<TeamFlagComponent>();
            if (TeamFlagComponent == null)
            {
                return -1;
            }

            return TeamFlagComponent.GetPlayerId();
        }

    }
}