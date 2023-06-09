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
        
        [ObjectSystem]
        public class SkillComponentFixedUpdateSystem: FixedUpdateSystem<SkillComponent>
        {
            protected override void FixedUpdate(SkillComponent self)
            {
                if (self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }
                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
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
            if (self.skillLevels.ContainsKey(skillId))
            {
                return;
            }
            self.skillList.Add(skillSlotType, skillId);
            self.skillCDs.Add(skillId, 0);
            SkillCfg skillCfg = SkillCfgCategory.Instance.Get(skillId);
            self.skillOrgCDs.Add(skillId, skillCfg.Cd);
            self.skillLevels.Add(skillId, skillLevel);

            if (skillCfg.LearnActionId.Count > 0)
            {
                SelectHandle selectHandle = SelectHandleHelper.CreateSelectHandle(self.GetUnit(), skillCfg.SkillSelectAction);
                ActionContext actionContext = new ActionContext()
                {
                    unitId = self.GetUnit().Id,
                    skillCfgId = skillId,
                    skillLevel = self.skillLevels[skillId],
                };
                foreach (var actionId in skillCfg.LearnActionId)
                {
                    ActionHandlerHelper.CreateAction(self.GetUnit(), actionId, 0, selectHandle, actionContext);
                }
            }
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

            if (string.IsNullOrEmpty(skillCfg.TimelineId))
            {
                return (false, $"skillId[{skillId} TimelineId=null]");
            }
            SelectHandle selectHandle = SelectHandleHelper.CreateSelectHandle(self.GetUnit(), skillCfg.SkillSelectAction);

            TimelineObj timelineObj= TimelineHelper.CreateTimeline(self.GetUnit(), skillCfg.TimelineId, selectHandle);
            timelineObj.InitActionContext(new ActionContext()
            {
                unitId = self.GetUnit().Id,
                skillCfgId = skillId,
                skillLevel = self.skillLevels[skillId],
            });
            EventSystem.Instance.Publish(self.DomainScene(), new AbilityTriggerEventType.SkillOnCast()
            {
                unit = self.GetUnit(),
                skillCfgId = skillId,
                timeline = timelineObj,
            });

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