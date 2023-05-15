using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (SkillComponent))]
    public static class SkillComponentSystem
    {
        [ObjectSystem]
        public class SkillComponentAwakeSystem: AwakeSystem<SkillComponent>
        {
            protected override void Awake(SkillComponent self)
            {
                self.skillList = new();
                self.skillCDs = new();
                self.skillOrgCDs = new();
                self.skillLevels = new();
            }
        }

        [ObjectSystem]
        public class SkillComponentDestroySystem: DestroySystem<SkillComponent>
        {
            protected override void Destroy(SkillComponent self)
            {
                self.skillList.Clear();
                self.skillCDs.Clear();
                self.skillOrgCDs.Clear();
                self.skillLevels.Clear();
            }
        }

        public static void FixedUpdate(this SkillComponent self, float fixedDeltaTime)
        {
            foreach (var skill in self.skillCDs)
            {
                if (skill.Value > 0)
                {
                    self.skillCDs[skill.Key] = math.min(0, skill.Value - fixedDeltaTime);
                }
            }
        }

        public static void LearnSkill(this SkillComponent self, string skillId, int skillLevel, SkillSlotType skillSlotType)
        {
            self.skillList.Add(skillSlotType, skillId);
            self.skillCDs.Add(skillId, 0);
            SkillCfg skillCfg = SkillCfgCategory.Instance.Get(skillId);
            self.skillOrgCDs.Add(skillId, skillCfg.Cd);
            self.skillLevels.Add(skillId, skillLevel);
        }

        public static (bool ret, string msg) CastSkill(this SkillComponent self, string skillId)
        {
            var result = self.ChkCanUseSkill(skillId);
            if (result.ret == false)
            {
                return result;
            }

            SkillCfg skillCfg = SkillCfgCategory.Instance.Get(skillId);
            TimelineHelper.CreateTimeline(self.GetParent<Unit>(), skillCfg.TimelineId);

            self.CostSkill(skillId);
            self.skillCDs[skillId] = skillCfg.Cd;
            return (true, "");
        }

        public static (bool ret, string msg) ChkCanUseSkill(this SkillComponent self, string skillId)
        {
            if (self.GetSkillCD(skillId) > 0)
            {
                string msg = "CD中";
                return (false, msg);
            }

            var result = self.ChkSkillCost(skillId);
            if (result.ret == false)
            {
                string msg = result.msg;
                return (false, msg);
            }

            return (true, "");
        }

        public static float GetSkillCD(this SkillComponent self, string skillId)
        {
            return 0;
        }

        public static (bool ret, string msg) ChkSkillCost(this SkillComponent self, string skillId)
        {
            return (true, "");
        }

        public static bool CostSkill(this SkillComponent self, string skillId)
        {
            return true;
        }
    }
}