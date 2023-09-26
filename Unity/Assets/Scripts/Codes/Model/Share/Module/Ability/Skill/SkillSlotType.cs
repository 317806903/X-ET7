namespace ET.Ability
{
    public enum SkillSlotType: byte
    {
        /// <summary>
        /// 普通攻击
        /// </summary>
        NormalAttack = 1,
        /// <summary>
        /// 主动技能
        /// </summary>
        InitiativeSkill = 2,
        /// <summary>
        /// 被动技能
        /// </summary>
        PassiveSkill = 3,
    }
}