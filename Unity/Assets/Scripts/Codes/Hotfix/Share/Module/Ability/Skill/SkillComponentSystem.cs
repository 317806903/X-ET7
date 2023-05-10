using System;
using System.Collections.Generic;
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

        public static void LearnSkill(this SkillComponent self, int skillId, int skillLevel, SkillSlotType skillSlotType)
        {
            self.skillList.Add(skillSlotType, skillId);
            self.skillCDs.Add(skillId, 0);
            SkillModel skillModel = new SkillModel(); //skillId
            self.skillOrgCDs.Add(skillId, skillModel.skillCD);
            self.skillLevels.Add(skillId, skillLevel);
        }

        public static (bool ret, string msg) CastSkill(this SkillComponent self, int skillId)
        {
            var result = self.ChkCanUseSkill(skillId);
            if (result.ret == false)
            {
                return result;
            }

            SkillModel skillModel = new SkillModel(); //skillId
            int timelineId = skillModel.timelineId;
            TimelineHelper.CreateTimeline(self.GetParent<Unit>(), timelineId);

            self.CostSkill(skillId);
            self.skillCDs[skillId] = skillModel.skillCD;
            return (true, "");
        }

        public static (bool ret, string msg) ChkCanUseSkill(this SkillComponent self, int skillId)
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

        public static float GetSkillCD(this SkillComponent self, int skillId)
        {
            return 0;
        }

        public static (bool ret, string msg) ChkSkillCost(this SkillComponent self, int skillId)
        {
            return (true, "");
        }

        public static bool CostSkill(this SkillComponent self, int skillId)
        {
            return true;
        }
    }
}