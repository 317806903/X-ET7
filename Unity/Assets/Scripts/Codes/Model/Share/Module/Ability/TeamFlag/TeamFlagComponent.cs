using System.Collections.Generic;

namespace ET.Ability
{
	[ComponentOf(typeof(Scene))]
	public class TeamFlagComponent: Entity, IAwake, IDestroy
    {
        public Dictionary<TeamFlagType, int> teamFriendDic;
    }
}