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
                self.skillSlotType2SkillObjs = new();
                self.skillCfgId2SkillObjs = new();
                self.sortPrioritySkillObjs = new();
            }
        }

        [ObjectSystem]
        public class SkillComponentDestroySystem: DestroySystem<SkillComponent>
        {
            protected override void Destroy(SkillComponent self)
            {
                self.skillSlotType2SkillObjs.Clear();
                self.skillCfgId2SkillObjs.Clear();
                self.sortPrioritySkillObjs.Clear();
            }
        }

        [ObjectSystem]
        public class SkillComponentFixedUpdateSystem: FixedUpdateSystem<SkillComponent>
        {
            protected override void FixedUpdate(SkillComponent self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static void FixedUpdate(this SkillComponent self, float fixedDeltaTime)
        {
        }

        public static (bool ret, string msg) LearnSkill(this SkillComponent self, string skillCfgId, int skillLevel, SkillSlotType skillSlotType)
        {
            if (self.GetSkillObj(skillCfgId) != null)
            {
                string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Skill_AlreadyLearned");
                return (false, msg);
            }

            if (SkillCfgCategory.Instance.Contain(skillCfgId) == false)
            {
                string msg = $"SkillCfgCategory.Instance.Contain({skillCfgId}) == false";
                return (false, msg);
            }

            SkillCfg skillCfg = SkillCfgCategory.Instance.Get(skillCfgId);
            if (string.IsNullOrEmpty(skillCfg.TimelineId))
            {
                skillSlotType = SkillSlotType.PassiveSkill;
            }

            SkillObj skillObj = self.AddChild<SkillObj>();

            self.skillSlotType2SkillObjs.Add(skillSlotType, skillObj.Id);
            self.skillCfgId2SkillObjs.Add(skillCfgId, skillObj.Id);
            self.ReSortPrioritySkillObjs();

            skillObj.Init(skillCfgId, skillLevel, skillSlotType);

            return (true, "");
        }

        public static Unit GetUnit(this SkillComponent self)
        {
            return self.GetParent<Unit>();
        }

        public static SkillObj GetSkillObj(this SkillComponent self, string skillCfgId)
        {
            if (self.skillCfgId2SkillObjs.TryGetValue(skillCfgId, out long skillId))
            {
                return self.GetChild<SkillObj>(skillId);
            }
            return null;
        }

        public static async ETTask<(bool ret, string msg)> CastSkill(this SkillComponent self, string skillCfgId)
        {
            SkillCfg skillCfg = SkillCfgCategory.Instance.Get(skillCfgId);
            if (string.IsNullOrEmpty(skillCfg.TimelineId))
            {
                return (false, $"skillId[{skillCfgId} TimelineId=null]");
            }

            var result = self.ChkCanUseSkill(skillCfgId);
            if (result.ret == false)
            {
                return result;
            }

            SkillObj skillObj = self.GetSkillObj(skillCfgId);
            if (skillObj == null)
            {
                string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Skill_HaveNotLearned", skillCfgId);
                return (false, msg);
            }

            ET.Ability.MoveOrIdleHelper.StopMove(self.GetUnit());

            TimelineObj timelineObj = await skillObj.CastSkill();

            self.CurSkillTimelineObj = timelineObj;

            EventSystem.Instance.Publish(self.DomainScene(), new AbilityTriggerEventType.SkillOnCast()
            {
                unit = self.GetUnit(),
            });

            return (true, "");
        }

        public static async ETTask ReplaceSkillTimeline(this SkillComponent self, string newTimelineCfgId)
        {
            if (self.CurSkillTimelineObj == null)
            {
                return;
            }

            TimelineObj timelineObj = await ET.Ability.TimelineHelper.ReplaceTimeline(self.GetUnit(), self.CurSkillTimelineObj.Id, newTimelineCfgId);
            self.CurSkillTimelineObj = timelineObj;
        }

        public static (bool ret, string msg) ChkCanUseSkill(this SkillComponent self, string skillCfgId)
        {
            if (self.CurSkillTimelineObj != null)
            {
                string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Skill_LastSkillIsBeingReleased");
                return (false, msg);
            }

            SkillObj skillObj = self.GetSkillObj(skillCfgId);
            if (skillObj == null)
            {
                string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Skill_HaveNotLearned", skillCfgId);
                return (false, msg);
            }

            var result = skillObj.ChkCanUseSkill();
            if (result.ret == false)
            {
                return result;
            }

            return (true, "");
        }

        public static void ReSortPrioritySkillObjs(this SkillComponent self)
        {
            self.sortPrioritySkillObjs.Clear();
            if (self.skillSlotType2SkillObjs.TryGetValue(SkillSlotType.InitiativeSkill, out List<long> skillObjs))
            {
                self.sortPrioritySkillObjs.AddRange(skillObjs);
            }
            if (self.skillSlotType2SkillObjs.TryGetValue(SkillSlotType.NormalAttack, out skillObjs))
            {
                self.sortPrioritySkillObjs.AddRange(skillObjs);
            }
        }

        public static List<long> GetSkillListBySkillSlotType(this SkillComponent self, SkillSlotType skillSlotType)
        {
            if (self.skillSlotType2SkillObjs.TryGetValue(skillSlotType, out List<long> skillObjs))
            {
                return skillObjs;
            }
            return null;
        }

        public static (float, SkillObj) GetSkillAttackDis(this SkillComponent self)
        {
            float dis = 0;
            for (int i = 0; i < self.sortPrioritySkillObjs.Count; i++)
            {
                long skillId = self.sortPrioritySkillObjs[i];
                SkillObj skillObj = self.GetChild<SkillObj>(skillId);
                if (dis < skillObj.GetSkillDis())
                {
                    dis = skillObj.GetSkillDis();
                }
                var result = skillObj.ChkCanUseSkill();
                if (result.ret)
                {
                    return (skillObj.GetSkillDis(), skillObj);
                }
            }

            return (dis, null);
        }

        public static float GetMaxSkillDis(this SkillComponent self, ET.Ability.SkillSlotType skillSlotType)
        {
            List<long> list = self.GetSkillListBySkillSlotType(skillSlotType);
            float dis = 0;
            if (list != null)
            {
                foreach (long skillId in list)
                {
                    SkillObj skillObj = self.GetChild<SkillObj>(skillId);
                    if (dis < skillObj.GetSkillDis())
                    {
                        dis = skillObj.GetSkillDis();
                    }
                }
            }

            return dis;
        }

        public static List<SkillObj> GetSkillList(this SkillComponent self, string skillCfgId, ET.Ability.SkillSlotType skillSlotType)
        {
            ListComponent<SkillObj> skillList = ListComponent<SkillObj>.Create();
            if (string.IsNullOrEmpty(skillCfgId) == false)
            {
                SkillObj skillObj = self.GetSkillObj(skillCfgId);
                skillList.Add(skillObj);
            }
            else
            {
                List<long> list = self.GetSkillListBySkillSlotType(skillSlotType);
                if (list != null)
                {
                    foreach (long skillId in list)
                    {
                        SkillObj skillObj = self.GetChild<SkillObj>(skillId);
                        skillList.Add(skillObj);
                    }
                }
            }
            return skillList;
        }

    }
}