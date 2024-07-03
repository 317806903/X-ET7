namespace ET.Ability
{
    public enum TeamFlagType
    {
        None = 0,
        /// <summary>
        /// 全局怪物
        /// </summary>
        Monster1 = 1 << 0,
        Monster2 = 1 << 1,
        Monster3 = 1 << 2,
        Monster4 = 1 << 3,
        Monster5 = 1 << 4,
        /// <summary>
        /// 全局势力
        /// </summary>
        TeamGlobal1 = 1 << 5,
        TeamGlobal2 = 1 << 6,
        TeamGlobal3 = 1 << 7,
        TeamGlobal4 = 1 << 8,
        TeamGlobal5 = 1 << 9,
        /// <summary>
        /// 一个player一个势力
        /// </summary>
        TeamPlayer1 = 1 << 10,
        TeamPlayer2 = 1 << 11,
        TeamPlayer3 = 1 << 12,
        TeamPlayer4 = 1 << 13,
        TeamPlayer5 = 1 << 14,

        NPC = 1 << 29,
        SceneEffect = 1 << 30,
    }
}