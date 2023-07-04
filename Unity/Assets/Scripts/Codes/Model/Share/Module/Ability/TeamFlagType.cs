namespace ET.Ability
{
    public enum TeamFlagType
    {
        /// <summary>
        /// 全局怪物
        /// </summary>
        Monster = 1 << 0,
        /// <summary>
        /// 全局势力
        /// </summary>
        TeamGlobal1 = 1 << 1,
        TeamGlobal2 = 1 << 2,
        TeamGlobal3 = 1 << 3,
        TeamGlobal4 = 1 << 4,
        TeamGlobal5 = 1 << 5,
        /// <summary>
        /// 一个player一个势力，如果是友军则通过ET.Ability.TeamFlagComponentSystem.AddFriendTeamFlag进行设置
        /// </summary>
        TeamPlayer1 = 1 << 6,
        TeamPlayer2 = 1 << 7,
        TeamPlayer3 = 1 << 8,
        TeamPlayer4 = 1 << 9,
        TeamPlayer5 = 1 << 10,
        TeamPlayer6 = 1 << 11,
        TeamPlayer7 = 1 << 12,
        TeamPlayer8 = 1 << 13,
        TeamPlayer9 = 1 << 14,
        TeamPlayer10 = 1 << 15,
        TeamPlayer11 = 1 << 16,
        TeamPlayer12 = 1 << 17,
        TeamPlayer13 = 1 << 18,
        TeamPlayer14 = 1 << 19,
        TeamPlayer15 = 1 << 20,
        TeamPlayer16 = 1 << 21,
        TeamPlayer17 = 1 << 22,
        TeamPlayer18 = 1 << 23,
        TeamPlayer19 = 1 << 24,
        TeamPlayer20 = 1 << 25,
    }
}