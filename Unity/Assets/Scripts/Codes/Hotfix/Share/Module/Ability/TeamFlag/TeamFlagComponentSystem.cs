using System;
using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof (TeamFlagComponent))]
    [FriendOf(typeof (TeamFlagObj))]
    public static class TeamFlagComponentSystem
    {
        [ObjectSystem]
        public class TeamFlagComponentAwakeSystem: AwakeSystem<TeamFlagComponent>
        {
            protected override void Awake(TeamFlagComponent self)
            {
                self.teamFriendDic = new();
            }
        }

        [ObjectSystem]
        public class TeamFlagComponentDestroySystem: DestroySystem<TeamFlagComponent>
        {
            protected override void Destroy(TeamFlagComponent self)
            {
                self.teamFriendDic.Clear();
            }
        }

        public static void AddFriendTeamFlag(this TeamFlagComponent self, List<TeamFlagType> teamFlagTypes, bool reset)
        {
            int newFriendTypes = 0;
            for (int i = 0; i < teamFlagTypes.Count; i++)
            {
                newFriendTypes |= (int)teamFlagTypes[i];
            }
            for (int i = 0; i < teamFlagTypes.Count; i++)
            {
                TeamFlagType teamFlagType = teamFlagTypes[i];
                if (reset == false && self.teamFriendDic.TryGetValue(teamFlagType, out int friendType))
                {
                    self.teamFriendDic[teamFlagType] = friendType | newFriendTypes;
                }
                else
                {
                    self.teamFriendDic[teamFlagType] = newFriendTypes;
                }
            }
        }
        
        public static bool ChkIsFriend(this TeamFlagComponent self, TeamFlagType curTeamFlagType, TeamFlagType targetTeamFlagType)
        {
            bool isFriend = (self.teamFriendDic[curTeamFlagType] & (int)targetTeamFlagType) > 0;
            return isFriend;
        }
    }
}