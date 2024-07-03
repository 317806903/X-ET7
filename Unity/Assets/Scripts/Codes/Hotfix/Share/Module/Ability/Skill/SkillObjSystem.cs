using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (SkillObj))]
    public static class SkillObjSystem
    {
        [ObjectSystem]
        public class SkillObjAwakeSystem: AwakeSystem<SkillObj>
        {
            protected override void Awake(SkillObj self)
            {
            }
        }

        [ObjectSystem]
        public class SkillObjDestroySystem: DestroySystem<SkillObj>
        {
            protected override void Destroy(SkillObj self)
            {
            }
        }

        [ObjectSystem]
        public class SkillObjFixedUpdateSystem: FixedUpdateSystem<SkillObj>
        {
            protected override void FixedUpdate(SkillObj self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static void FixedUpdate(this SkillObj self, float fixedDeltaTime)
        {
            self.cdCountDown = math.max(0, self.cdCountDown - fixedDeltaTime);
        }

        public static Unit GetUnit(this SkillObj self)
        {
            return self.GetParent<SkillComponent>().GetParent<Unit>();
        }

        public static void Init(this SkillObj self, string skillCfgId, int skillLevel, SkillSlotType skillSlotType)
        {
            self.skillCfgId = skillCfgId;
            self.skillLevel = skillLevel;
            self.skillSlotType = skillSlotType;

            SkillCfg skillCfg = self.model;
            self.skillDis = skillCfg.Dis;

            NumericComponent numericComponent = self.AddComponent<NumericComponent>();
            numericComponent.SetAsFloat(NumericType.SkillCDBase, skillCfg.Cd);
            numericComponent.SetAsFloat(NumericType.SkillDisBase, skillCfg.Dis);
        }

        public static void ResetSkillSlotIndex(this SkillObj self, int skillSlotIndex)
        {
            self.skillSlotIndex = skillSlotIndex;
        }

        public static void DealLearnActionIds(this SkillObj self)
        {
            SkillCfg skillCfg = self.model;

            if (skillCfg.LearnActionId.Count > 0)
            {
                SelectHandle selectHandleSelf = SelectHandleHelper.CreateUnitSelfSelectHandle(self.GetUnit());
                ActionContext actionContext = new ActionContext()
                {
                    unitId = self.GetUnit().Id,
                    skillCfgId = self.skillCfgId,
                    skillDis = self.GetSkillDis(),
                    skillSlotType = self.skillSlotType,
                    skillSlotIndex = self.skillSlotIndex,
                    skillGroupType = self.model.SkillGroupType,
                    skillLevel = self.skillLevel,
                };
                foreach (var actionId in skillCfg.LearnActionId)
                {
                    ActionHandlerHelper.CreateAction(self.GetUnit(), null, actionId, 0.1f, selectHandleSelf, ref actionContext);
                }
            }
        }

        public static async ETTask<TimelineObj> CastSkill(this SkillObj self)
        {
            ET.Ability.UnitHelper.ClearOnceSelectHandle(self.GetUnit());
            ET.Ability.UnitHelper.ClearExcludeSelectHandle(self.GetUnit());

            SkillCfg skillCfg = self.model;
            ActionContext actionContext = new ActionContext()
            {
                unitId = self.GetUnit().Id,
                skillCfgId = self.skillCfgId,
                skillDis = self.GetSkillDis(),
                skillSlotType = self.skillSlotType,
                skillSlotIndex = self.skillSlotIndex,
                skillGroupType = self.model.SkillGroupType,
                skillLevel = self.skillLevel,
            };
            SelectHandle selectHandle = SelectHandleHelper.CreateSelectHandle(self.GetUnit(), null, skillCfg.SkillSelectAction_Ref, ref actionContext);

            TimelineObj timelineObj = await TimelineHelper.CreateTimeline(self.GetUnit(), skillCfg.TimelineId);
            timelineObj.InitActionContext(ref actionContext);

            self.CostSkill();
            self.cdCountDown = self._GetSkillCD();

            return timelineObj;
        }

        public static (bool ret, string msg) ChkCanUseSkill(this SkillObj self)
        {
            float cd = self.GetSkillCDCountDown();
            if (cd > 0)
            {
                string msg = $"CD中 {cd}";
                return (false, msg);
            }

            var result = self.ChkSkillCost();
            if (result.ret == false)
            {
                string msg = result.msg;
                return (false, msg);
            }

            if (self.skillSlotType == SkillSlotType.InitiativeSkill)
            {
                bool bRet = BuffHelper.ChkCanSkillCastInput(self.GetUnit());
                if (bRet == false)
                {
                    string msg = "cannot Cast InitiativeSkill";
                    return (false, msg);
                }
            }
            else if (self.skillSlotType == SkillSlotType.NormalAttack)
            {
                bool bRet = BuffHelper.ChkCanNormalAttack(self.GetUnit());
                if (bRet == false)
                {
                    string msg = "cannot NormalAttack";
                    return (false, msg);
                }
            }

            return (true, "");
        }

        public static float GetSkillCDCountDown(this SkillObj self)
        {
            return self.cdCountDown;
        }

        public static float _GetSkillCD(this SkillObj self)
        {
            NumericComponent numericComponent = self.GetComponent<NumericComponent>();
            float skillCD = numericComponent.GetAsFloat(NumericType.SkillCD);
            return skillCD;
        }

        public static void ResetSkillCDCountDown(this SkillObj self)
        {
            float skillCD = self._GetSkillCD();
            self.cdCountDown = math.min(self.cdCountDown, skillCD);
        }

        public static float GetSkillDis(this SkillObj self)
        {
            return self.skillDis;
        }

        public static void ResetSkillDis(this SkillObj self)
        {
            NumericComponent numericComponent = self.GetComponent<NumericComponent>();
            float skillDis = numericComponent.GetAsFloat(NumericType.SkillDis);
            self.skillDis = skillDis;
        }

        public static (bool ret, string msg) ChkSkillCost(this SkillObj self)
        {
            return (true, "");
        }

        public static bool CostSkill(this SkillObj self)
        {
            return true;
        }
    }
}