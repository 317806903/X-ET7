using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class SkillHelper
    {
        public static void LearnSkill(Unit castUnit, string skillId, int skillLevel, SkillSlotType skillSlotType)
        {
            SkillComponent skillComponent = castUnit.GetComponent<SkillComponent>();
            if (skillComponent == null)
            {
                skillComponent = castUnit.AddComponent<SkillComponent>();
            }
            skillComponent.LearnSkill(skillId, skillLevel, skillSlotType);
        }
        
        public static (bool ret, string msg) CastSkill(Unit castUnit, string skillId)
        {
            SkillComponent skillComponent = castUnit.GetComponent<SkillComponent>();
            if (skillComponent == null)
            {
                skillComponent = castUnit.AddComponent<SkillComponent>();
            }
            return skillComponent.CastSkill(skillId);
        }
        
        public static (bool ret, string msg) ChkCanUseSkill(Unit castUnit, string skillId)
        {
            return castUnit.GetComponent<SkillComponent>().ChkCanUseSkill(skillId);
        }

        public static float GetSkillCD(Unit castUnit, string skillId)
        {
            return castUnit.GetComponent<SkillComponent>().GetSkillCD(skillId);
        }

        public static (bool ret, string msg) ChkSkillCost(Unit castUnit, string skillId)
        {
            return castUnit.GetComponent<SkillComponent>().ChkSkillCost(skillId);
        }
    }
}