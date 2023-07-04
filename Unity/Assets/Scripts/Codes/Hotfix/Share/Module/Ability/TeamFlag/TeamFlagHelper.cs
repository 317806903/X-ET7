using System;
using System.Collections.Generic;

namespace ET.Ability
{
    public static class TeamFlagHelper
    {
        public static void AddFriendTeamFlag(Scene scene, List<TeamFlagType> teamFlagTypes, bool reset)
        {
            scene.GetComponent<TeamFlagComponent>().AddFriendTeamFlag(teamFlagTypes, reset);
        }
        
        public static bool ChkIsFriend(Unit curUnit, Unit targetUnit)
        {
            if (UnitHelper.ChkUnitAlive(curUnit) == false)
            {
                return false;
            }
            if (UnitHelper.ChkUnitAlive(targetUnit) == false)
            {
                return false;
            }
            TeamFlagType curTeamFlagType = curUnit.GetComponent<TeamFlagObj>().GetTeamFlagType();
            TeamFlagType targetTeamFlagType = targetUnit.GetComponent<TeamFlagObj>().GetTeamFlagType();
            return curUnit.DomainScene().GetComponent<TeamFlagComponent>().ChkIsFriend(curTeamFlagType, targetTeamFlagType);
        }
        
        public static TeamFlagType GetTeamFlagTypeBySeatIndex(int roomSeatIndex)
        {
            string key = $"TeamPlayer{roomSeatIndex + 1}";
            TeamFlagType teamFlagType = (TeamFlagType)Enum.Parse(typeof(TeamFlagType), key);
            return teamFlagType;
        }
        
    }
}