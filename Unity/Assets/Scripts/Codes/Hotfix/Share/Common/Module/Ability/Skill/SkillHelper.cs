using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class SkillHelper
    {
        public static SkillComponent GetSkillComponent(Unit unit)
        {
            SkillComponent skillComponent = unit.GetComponent<SkillComponent>();
            if (skillComponent == null)
            {
                skillComponent = unit.AddComponent<SkillComponent>();
            }
            return skillComponent;
        }

        public static (bool ret, string msg, SkillObj skillObj) LearnSkill(Unit unit, string skillCfgId, int skillLevel, ET.AbilityConfig.SkillSlotType skillSlotType)
        {
            SkillComponent skillComponent = GetSkillComponent(unit);
            var result = skillComponent.LearnSkill(skillCfgId, skillLevel, skillSlotType);
            if (result.ret)
            {
                float maxSkillDis = ET.Ability.SkillHelper.GetMaxSkillDis(unit, SkillSlotType.NormalAttack);
                NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
                numericComponent.SetAsFloat(NumericType.SkillDisBase, maxSkillDis);
            }
            return result;
        }

        public static void ForgetSkill(Unit unit, string skillCfgId, ET.AbilityConfig.SkillSlotType skillSlotType, int skillSlotIndex, ET.AbilityConfig.SkillGroupType skillGroupType)
        {
            SkillComponent skillComponent = GetSkillComponent(unit);
            skillComponent.ForgetSkill(skillCfgId, skillSlotType, skillSlotIndex, skillGroupType);
        }

        public static async ETTask<(bool ret, string msg)> CastSkill(Unit unit, string skillCfgId, SelectHandle selectHandleIn)
        {
            SkillComponent skillComponent = GetSkillComponent(unit);
            return await skillComponent.CastSkill(skillCfgId, selectHandleIn);
        }

        public static async ETTask<(bool ret, string msg)> RestoreSkillEnergy(Unit unit, string skillCfgId)
        {
            SkillComponent skillComponent = GetSkillComponent(unit);
            return await skillComponent.RestoreSkillEnergy(skillCfgId);
        }

        public static async ETTask ReplaceSkillTimeline(Unit unit, string newTimelineCfgId)
        {
            SkillComponent skillComponent = GetSkillComponent(unit);
            await skillComponent.ReplaceSkillTimeline(newTimelineCfgId);
        }

        public static (bool ret, string msg) ChkCanUseSkill(Unit unit, string skillCfgId)
        {
            SkillComponent skillComponent = GetSkillComponent(unit);
            return skillComponent.ChkCanUseSkill(skillCfgId);
        }

        public static (float, SkillObj, int) GetSkillAttackDis(Unit unit, HashSet<string> excludeSkillCfgIds, bool isGetMaxDisSkill)
        {
            SkillComponent skillComponent = GetSkillComponent(unit);
            return skillComponent.GetSkillAttackDis(excludeSkillCfgIds, isGetMaxDisSkill);
        }

        public static (float, SkillObj) GetSkillAttackDisWhenBlock(Unit unit, HashSet<string> excludeSkillCfgIds)
        {
            SkillComponent skillComponent = GetSkillComponent(unit);
            return skillComponent.GetSkillAttackDisWhenBlock(excludeSkillCfgIds);
        }

        public static List<SkillObj> GetSkillList(Unit unit, string skillCfgId, ET.AbilityConfig.SkillSlotType skillSlotType, int skillSlotIndex, ET.AbilityConfig.SkillGroupType skillGroupType)
        {
            SkillComponent skillComponent = GetSkillComponent(unit);
            return skillComponent.GetSkillList(skillCfgId, skillSlotType, skillSlotIndex, skillGroupType);
        }

        public static List<SkillObj> GetManualSkillList(Unit unit)
        {
            SkillComponent skillComponent = GetSkillComponent(unit);
            return skillComponent.GetManualSkillList();
        }

        public static SkillObj GetSkillObj(Unit unit, string skillCfgId)
        {
            SkillComponent skillComponent = GetSkillComponent(unit);
            return skillComponent.GetSkillObj(skillCfgId);
        }

        public static float GetMaxSkillDis(Unit unit, ET.AbilityConfig.SkillSlotType skillSlotType)
        {
            SkillComponent skillComponent = GetSkillComponent(unit);
            return skillComponent.GetMaxSkillDis(skillSlotType);
        }

    }
}