using System;
using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof (TeamFlagObj))]
    public static class TeamFlagObjSystem
    {
        [ObjectSystem]
        public class TeamFlagObjAwakeSystem: AwakeSystem<TeamFlagObj>
        {
            protected override void Awake(TeamFlagObj self)
            {
            }
        }

        [ObjectSystem]
        public class TeamFlagObjDestroySystem: DestroySystem<TeamFlagObj>
        {
            protected override void Destroy(TeamFlagObj self)
            {
            }
        }

        public static void Init(this TeamFlagObj self, TeamFlagType teamFlagType)
        {
            self.teamFlagType = teamFlagType;
        }
        
        public static TeamFlagType GetTeamFlagType(this TeamFlagObj self)
        {
            return self.teamFlagType;
        }

    }
}