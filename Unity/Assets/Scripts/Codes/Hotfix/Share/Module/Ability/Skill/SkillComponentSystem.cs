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
                self.skillSlotTypeNone2SkillObjs = new();
                self.skillGroupType2SkillObjs = new();
                self.skillObjs2SkillSlotType = new();
                self.skillCfgId2SkillObjs = new();
                self.sortPrioritySkillObjs = new();
                self.manualSkillObjs = new();

                self.SetCommonEnergyFull();
                self.AddRestoreEnergy();
            }
        }

        [ObjectSystem]
        public class SkillComponentDestroySystem: DestroySystem<SkillComponent>
        {
            protected override void Destroy(SkillComponent self)
            {
                self.skillSlotType2SkillObjs?.Clear();
                self.skillSlotTypeNone2SkillObjs?.Clear();
                self.skillGroupType2SkillObjs?.Clear();
                self.skillObjs2SkillSlotType?.Clear();
                self.skillCfgId2SkillObjs?.Clear();
                self.sortPrioritySkillObjs?.Clear();
                self.manualSkillObjs?.Clear();
            }
        }

        [ObjectSystem]
        public class SkillComponentFixedUpdateSystem: FixedUpdateSystem<SkillComponent>
        {
            protected override void FixedUpdate(SkillComponent self)
            {
                //if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                if (self.IsDisposed)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static void FixedUpdate(this SkillComponent self, float fixedDeltaTime)
        {
            self.DealCurCommonEnergyNumByTime(fixedDeltaTime);
        }

        public static float GetCurCommonEnergyNum(this SkillComponent self)
        {
            return self.curCommonEnergyNum;
        }

        public static void CostCommonEnergyNum(this SkillComponent self, float costComonEnergyNum)
        {
            self.curCommonEnergyNum = math.max(self.curCommonEnergyNum - costComonEnergyNum, 0);
        }

        public static void SetCommonEnergyFull(this SkillComponent self)
        {
            UnitCfg unitCfg = self.GetUnit().model;
            if (unitCfg == null)
            {
                return;
            }
            self.curCommonEnergyNum = unitCfg.TotalCommonEnergy;
        }

        public static void AddRestoreEnergy(this SkillComponent self)
        {
            UnitCfg unitCfg = self.GetUnit().model;
            if (unitCfg == null)
            {
                return;
            }

            if (unitCfg.TotalCommonEnergy <= 0)
            {
                return;
            }
            if (unitCfg.RestoreCommonEnergyByWave <= 0)
            {
                return;
            }
            EventSystem.Instance.Publish(self.DomainScene(), new ET.Ability.AbilityTriggerEventType.GamePlayTowerDefense_AddRestoreEnergy()
            {
                skillComponent = self,
            });
        }

        public static int GetCommonEnergyFullNum(this SkillComponent self)
        {
            UnitCfg unitCfg = self.GetUnit().model;
            if (unitCfg == null)
            {
                return 0;
            }
            return unitCfg.TotalCommonEnergy;
        }

        public static void DealCurCommonEnergyNumByTime(this SkillComponent self, float fixedDeltaTime)
        {
            UnitCfg unitCfg = self.GetUnit().model;
            if (unitCfg == null)
            {
                return;
            }
            float restoreCommonEnergyByTime = unitCfg.RestoreCommonEnergyByTime;
            if (restoreCommonEnergyByTime <= 0)
            {
                return;
            }
            if (self.curCommonEnergyNum >= unitCfg.TotalCommonEnergy)
            {
                return;
            }

            self.curCommonEnergyNum = math.clamp(self.curCommonEnergyNum + restoreCommonEnergyByTime * fixedDeltaTime, 0, unitCfg.TotalCommonEnergy);
        }

        public static void DealCurCommonEnergyNumByWave(this SkillComponent self)
        {
            UnitCfg unitCfg = self.GetUnit().model;
            if (unitCfg == null)
            {
                return;
            }
            float restoreCommonEnergyByWave = unitCfg.RestoreCommonEnergyByWave;
            if (restoreCommonEnergyByWave <= 0)
            {
                return;
            }
            if (self.curCommonEnergyNum >= unitCfg.TotalCommonEnergy)
            {
                return;
            }
            self.curCommonEnergyNum = math.clamp(self.curCommonEnergyNum + restoreCommonEnergyByWave, 0, unitCfg.TotalCommonEnergy);
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

            if (skillCfg.SkillGroupType != SkillGroupType.None && self.skillGroupType2SkillObjs.TryGetValue(skillCfg.SkillGroupType, out long curSkillObjId))
            {
                SkillObj curSkillObj = self.GetChild<SkillObj>(curSkillObjId);
                if (curSkillObj.model.Priority > skillCfg.Priority)
                {
                    string msg = $"curSkillObj.model.Priority[{curSkillObj.skillCfgId} {curSkillObj.model.Priority}] > skillCfg.Priority[{skillCfg.Id} {skillCfg.Priority}]";
                    return (false, msg);
                }
                else
                {
                    self.ForgetSkill(curSkillObj.skillCfgId, SkillSlotType.None, -1, SkillGroupType.None);
                }
            }

            SkillObj skillObj = self.AddChild<SkillObj>();
            skillObj.Init(skillCfgId, skillLevel, skillSlotType);

            self.skillSlotType2SkillObjs.Add(skillSlotType, skillObj.Id);
            self.skillSlotTypeNone2SkillObjs.Add(SkillSlotType.None, skillObj.Id);
            if (skillCfg.SkillGroupType != SkillGroupType.None)
            {
                self.skillGroupType2SkillObjs.Add(skillCfg.SkillGroupType, skillObj.Id);
            }
            self.skillCfgId2SkillObjs.Add(skillCfgId, skillObj.Id);
            self.skillObjs2SkillSlotType.Add(skillObj.Id, skillSlotType);
            self.ReSortManualSkillObjs();
            self.ReSortPrioritySkillObjs();

            skillObj.DealLearnActionIds().Coroutine();

            self.NoticeClient();

            return (true, "");
        }

        public static void ForgetSkill(this SkillComponent self, string skillCfgId, ET.AbilityConfig.SkillSlotType skillSlotType, int skillSlotIndex, ET.AbilityConfig.SkillGroupType skillGroupType)
        {
            List<SkillObj> skillList = self.GetSkillList(skillCfgId, skillSlotType, skillSlotIndex, skillGroupType);
            foreach (SkillObj skillObj in skillList)
            {
                long curSkillObjId = skillObj.Id;
                self.skillObjs2SkillSlotType.TryGetValue(curSkillObjId, out SkillSlotType curSkillSlotType);
                self.skillObjs2SkillSlotType.Remove(curSkillObjId);
                self.skillSlotType2SkillObjs.Remove(curSkillSlotType, curSkillObjId);
                self.skillSlotTypeNone2SkillObjs.Remove(SkillSlotType.None, curSkillObjId);
                self.skillGroupType2SkillObjs.Remove(skillObj.model.SkillGroupType);
                self.skillCfgId2SkillObjs.Remove(skillCfgId);
                self.RemoveChild(curSkillObjId);
            }
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

        public static async ETTask<(bool ret, string msg)> CastSkill(this SkillComponent self, string skillCfgId, SelectHandle selectHandleIn)
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

            TimelineObj timelineObj = await skillObj.CastSkill(selectHandleIn);
            if (timelineObj == null)
            {
                return (false, "no target");
            }
            self.CurSkillTimelineObj = timelineObj;

            EventSystem.Instance.Publish(self.DomainScene(), new AbilityTriggerEventType.SkillOnCast()
            {
                unit = self.GetUnit(),
            });

            return (true, "");
        }

        public static async ETTask<(bool ret, string msg)> RestoreSkillEnergy(this SkillComponent self, string skillCfgId)
        {
            if (string.IsNullOrEmpty(skillCfgId))
            {
                bool ret;
                string msg;
                List<SkillObj> skillList = self.GetManualSkillList();
                foreach (SkillObj skillObj in skillList)
                {
                    (ret, msg) = await skillObj.RestoreSkillEnergy();
                    if (ret == false)
                    {
                        return (ret, msg);
                    }
                }
                return (true, "");
            }
            else
            {
                SkillObj skillObj = self.GetSkillObj(skillCfgId);
                if (skillObj == null)
                {
                    string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Skill_HaveNotLearned", skillCfgId);
                    return (false, msg);
                }

                return await skillObj.RestoreSkillEnergy();
            }
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

        public static void ReSortManualSkillObjs(this SkillComponent self)
        {
            self.manualSkillObjs.Clear();
            if (self.skillSlotType2SkillObjs.TryGetValue(SkillSlotType.ManualSkill, out List<long> skillObjs))
            {
                for (int i = 0; i < skillObjs.Count; i++)
                {
                    long skillId = skillObjs[i];
                    SkillObj skillObj = self.GetChild<SkillObj>(skillId);
                    skillObj.ResetSkillSlotIndex(i);
                    if (string.IsNullOrEmpty(skillObj.model.TimelineId) == false)
                    {
                        self.manualSkillObjs.Add(skillId);
                    }
                }
            }
        }

        public static void ReSortPrioritySkillObjs(this SkillComponent self)
        {
            self.sortPrioritySkillObjs.Clear();
            if (self.skillSlotType2SkillObjs.TryGetValue(SkillSlotType.InitiativeSkill, out List<long> skillObjs))
            {
                for (int i = 0; i < skillObjs.Count; i++)
                {
                    long skillId = skillObjs[i];
                    SkillObj skillObj = self.GetChild<SkillObj>(skillId);
                    skillObj.ResetSkillSlotIndex(i);
                    if (string.IsNullOrEmpty(skillObj.model.TimelineId) == false)
                    {
                        self.sortPrioritySkillObjs.Add(skillId);
                    }
                }
            }
            if (self.skillSlotType2SkillObjs.TryGetValue(SkillSlotType.NormalAttack, out skillObjs))
            {
                for (int i = 0; i < skillObjs.Count; i++)
                {
                    long skillId = skillObjs[i];
                    SkillObj skillObj = self.GetChild<SkillObj>(skillId);
                    skillObj.ResetSkillSlotIndex(i);
                    if (string.IsNullOrEmpty(skillObj.model.TimelineId) == false)
                    {
                        self.sortPrioritySkillObjs.Add(skillId);
                    }
                }
            }
            if (self.skillSlotType2SkillObjs.TryGetValue(SkillSlotType.PassiveSkill, out skillObjs))
            {
                for (int i = 0; i < skillObjs.Count; i++)
                {
                    long skillId = skillObjs[i];
                    SkillObj skillObj = self.GetChild<SkillObj>(skillId);
                    skillObj.ResetSkillSlotIndex(i);
                    if (string.IsNullOrEmpty(skillObj.model.TimelineId) == false)
                    {
                        self.sortPrioritySkillObjs.Add(skillId);
                    }
                }
            }
        }

        public static List<long> GetSkillListBySkillSlotType(this SkillComponent self, SkillSlotType skillSlotType, int skillSlotIndex)
        {
            if (skillSlotType == SkillSlotType.None)
            {
                if (self.skillSlotTypeNone2SkillObjs.TryGetValue(skillSlotType, out List<long> skillObjs))
                {
                    return skillObjs;
                }
            }
            else
            {
                if (self.skillSlotType2SkillObjs.TryGetValue(skillSlotType, out List<long> skillObjs))
                {
                    if (skillSlotIndex == -1)
                    {
                        return skillObjs;
                    }
                    else
                    {
                        if (skillSlotIndex >= skillObjs.Count)
                        {
                            return null;
                        }
                        ListComponent<long> skillObjList = ListComponent<long>.Create();
                        skillObjList.Add(skillObjs[skillSlotIndex]);
                        return skillObjList;
                    }
                }
                else
                {
                    if (skillSlotType == SkillSlotType.NormalAttack)
                    {
                        if (self.skillSlotType2SkillObjs.TryGetValue(SkillSlotType.PassiveSkill, out skillObjs))
                        {
                            if (skillSlotIndex == -1)
                            {
                                return skillObjs;
                            }
                            else
                            {
                                if (skillSlotIndex >= skillObjs.Count)
                                {
                                    return null;
                                }
                                ListComponent<long> skillObjList = ListComponent<long>.Create();
                                skillObjList.Add(skillObjs[skillSlotIndex]);
                                return skillObjList;
                            }
                        }
                    }
                }
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
                var result = skillObj.ChkCanUseSkill();
                if (result.ret)
                {
                    if (dis < skillObj.GetSkillDis())
                    {
                        dis = skillObj.GetSkillDis();
                    }
                    return (skillObj.GetSkillDis(), skillObj);
                }
                else
                {
                    if (self.skillObjs2SkillSlotType[skillId] == SkillSlotType.NormalAttack)
                    {
                        if (dis < skillObj.GetSkillDis())
                        {
                            dis = skillObj.GetSkillDis();
                        }
                    }
                }
            }

            return (dis, null);
        }

        public static float GetMaxSkillDis(this SkillComponent self, ET.AbilityConfig.SkillSlotType skillSlotType)
        {
            List<long> list = self.GetSkillListBySkillSlotType(skillSlotType, -1);
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

        public static List<SkillObj> GetSkillList(this SkillComponent self, string skillCfgId, ET.AbilityConfig.SkillSlotType skillSlotType, int skillSlotIndex, ET.AbilityConfig.SkillGroupType skillGroupType)
        {
            ListComponent<SkillObj> skillList = ListComponent<SkillObj>.Create();
            if (string.IsNullOrEmpty(skillCfgId) == false)
            {
                SkillObj skillObj = self.GetSkillObj(skillCfgId);
                skillList.Add(skillObj);
            }
            else
            {
                if (skillGroupType == SkillGroupType.None)
                {
                    List<long> list = self.GetSkillListBySkillSlotType(skillSlotType, skillSlotIndex);
                    if (list != null)
                    {
                        foreach (long skillObjId in list)
                        {
                            SkillObj skillObj = self.GetChild<SkillObj>(skillObjId);
                            skillList.Add(skillObj);
                        }
                    }
                }
                else
                {
                    if (self.skillGroupType2SkillObjs.TryGetValue(skillGroupType, out long curSkillObjId))
                    {
                        SkillObj skillObj = self.GetChild<SkillObj>(curSkillObjId);
                        skillList.Add(skillObj);
                    }
                }
            }
            return skillList;
        }

        public static List<SkillObj> GetManualSkillList(this SkillComponent self)
        {
            ListComponent<SkillObj> skillList = ListComponent<SkillObj>.Create();
            foreach (long curSkillObjId in self.manualSkillObjs)
            {
                SkillObj skillObj = self.GetChild<SkillObj>(curSkillObjId);
                skillList.Add(skillObj);
            }
            return skillList;
        }

        public static void NoticeClient(this SkillComponent self)
        {
            Unit unit = self.GetUnit();
            if (UnitHelper.ChkIsPlayer(unit)
                || UnitHelper.ChkIsCameraPlayer(unit))
            {
                Ability.UnitHelper.AddSyncData_UnitComponent(self.GetUnit(), self.GetType());
            }
        }

    }
}