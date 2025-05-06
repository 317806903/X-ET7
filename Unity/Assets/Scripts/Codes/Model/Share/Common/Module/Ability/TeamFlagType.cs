namespace ET.Ability
{
    public enum TeamFlagType
    {
        None = 0,
        /// <summary>
        /// 全局怪物
        /// </summary>
        Monster = 1 << 0,
        Monster1 = 1 << 1,
        Monster2 = 1 << 2,
        Monster3 = 1 << 3,
        Monster4 = 1 << 4,
        Monster5 = 1 << 5,
        /// <summary>
        /// 全局势力
        /// </summary>
        TeamGlobal = 1 << 6,
        TeamGlobal1 = 1 << 7,
        TeamGlobal2 = 1 << 8,
        TeamGlobal3 = 1 << 9,
        TeamGlobal4 = 1 << 10,
        TeamGlobal5 = 1 << 11,
        /// <summary>
        /// 一个player一个势力
        /// </summary>
        TeamPlayer = 1 << 12,
        TeamPlayer1 = 1 << 13,
        TeamPlayer2 = 1 << 14,
        TeamPlayer3 = 1 << 15,
        TeamPlayer4 = 1 << 16,
        TeamPlayer5 = 1 << 17,

        /// <summary>
        /// 一个playerSkill一个势力
        /// </summary>
        TeamPlayerSkill = 1 << 18,
        TeamPlayerSkill1 = 1 << 19,
        TeamPlayerSkill2 = 1 << 20,
        TeamPlayerSkill3 = 1 << 21,
        TeamPlayerSkill4 = 1 << 22,
        TeamPlayerSkill5 = 1 << 23,

        TeamWildMonster = 1 << 24,

        NPC = 1 << 29,
        SceneEffect = 1 << 30,
    }
}