using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (TeamFlagComponent))]
    public static class TeamFlagComponentSystem
    {
        [ObjectSystem]
        public class TeamFlagComponentAwakeSystem: AwakeSystem<TeamFlagComponent>
        {
            protected override void Awake(TeamFlagComponent self)
            {
            }
        }

        [ObjectSystem]
        public class TeamFlagComponentDestroySystem: DestroySystem<TeamFlagComponent>
        {
            protected override void Destroy(TeamFlagComponent self)
            {
            }
        }

        public static void AddTeamFlag(this TeamFlagComponent self, long playerId, TeamFlagType teamFlagType)
        {
            self.playerId = playerId;
            self.teamFlagType = teamFlagType;
        }

        public static TeamFlagType GetTeamFlag(this TeamFlagComponent self)
        {
            return self.teamFlagType;
        }

        public static long GetPlayerId(this TeamFlagComponent self)
        {
            return self.playerId;
        }

        /// <summary>
        /// 获取unit
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Unit GetUnit(this TeamFlagComponent self)
        {
            return self.GetParent<Unit>();
        }
    }
}