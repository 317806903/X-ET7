using System;
using System.Collections.Generic;
using System.Linq;
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
            foreach (string skillId in self.skillCDs.Keys.ToArray())
            {
                if (self.skillCDs[skillId] > 0)
                {
                    self.skillCDs[skillId] = math.max(0, self.skillCDs[skillId] - fixedDeltaTime);
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

        public static Unit GetUnit(this SkillComponent self)
        {
            return self.GetParent<Unit>();
        }

        public static (bool ret, string msg) CastSkill(this SkillComponent self, string skillId)
        {
            var result = self.ChkCanUseSkill(skillId);
            if (result.ret == false)
            {
                return result;
            }

            SkillCfg skillCfg = SkillCfgCategory.Instance.Get(skillId);

            SelectHandle selectHandle = SelectHandleHelper.GetSelectHandle(self.GetUnit(), skillCfg.SkillSelectAction);

            TimelineHelper.CreateTimeline(self.GetUnit(), skillCfg.TimelineId, selectHandle);

            self.CostSkill(skillId);
            self.skillCDs[skillId] = skillCfg.Cd;
            return (true, "");
        }

        public static (bool ret, string msg) ChkCanUseSkill(this SkillComponent self, string skillId)
        {
            float cd = self.GetSkillCD(skillId);
            if (cd > 0)
            {
                string msg = $"CD中 {cd}";
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
            return self.skillCDs[skillId];
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