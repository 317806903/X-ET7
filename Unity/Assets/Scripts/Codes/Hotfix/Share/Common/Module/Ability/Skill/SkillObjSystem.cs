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
                //if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                if (self.IsDisposed)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static void FixedUpdate(this SkillObj self, float fixedDeltaTime)
        {
            self.DealCDCountDownByTime(fixedDeltaTime);
            self.DealCurEnergyNumByTime(fixedDeltaTime);
        }

        public static void DealCDCountDownByTime(this SkillObj self, float fixedDeltaTime)
        {
            bool isNeedNoticeClient = false;
            float cdCountDown = math.max(0, self.cdCountDown - fixedDeltaTime);
            if (self.cdCountDown > 0 && cdCountDown == 0)
            {
                isNeedNoticeClient = true;
            }
            self.cdCountDown = cdCountDown;

            if (isNeedNoticeClient)
            {
                self.NoticeClient();
            }
        }

        public static void SetEnergyFull(this SkillObj self)
        {
            self.totalEnergyNum = self._GetSkillTotalEnergyNum();
            self.curEnergyNum = self.totalEnergyNum;
        }

        public static void DealCurEnergyNumByTime(this SkillObj self, float fixedDeltaTime)
        {
            if (SkillConsumeCfgCategory.Instance.Contain(self.skillCfgId) == false)
            {
                return;
            }
            SkillConsumeCfg skillConsumeCfg = SkillConsumeCfgCategory.Instance.Get(self.skillCfgId);

            if (skillConsumeCfg.RestoreEnergyByTime <= 0)
            {
                return;
            }
            if (self.curEnergyNum >= self.totalEnergyNum)
            {
                self.curEnergyNum = self.totalEnergyNum;
                return;
            }
            self.curEnergyNum = math.clamp(self.curEnergyNum + skillConsumeCfg.RestoreEnergyByTime * fixedDeltaTime, 0, self.totalEnergyNum);
        }

        public static void DealCurEnergyNumByWave(this SkillObj self)
        {
            if (SkillConsumeCfgCategory.Instance.Contain(self.skillCfgId) == false)
            {
                return;
            }
            SkillConsumeCfg skillConsumeCfg = SkillConsumeCfgCategory.Instance.Get(self.skillCfgId);
            if (skillConsumeCfg.RestoreEnergyByWave <= 0)
            {
                return;
            }
            if (self.curEnergyNum >= self.totalEnergyNum)
            {
                self.curEnergyNum = self.totalEnergyNum;
                return;
            }
            self.curEnergyNum = math.clamp(self.curEnergyNum + skillConsumeCfg.RestoreEnergyByWave, 0, self.totalEnergyNum);
            if (self.curEnergyNum >= self.totalEnergyNum)
            {
                self.curEnergyNum = self.totalEnergyNum;
            }

            self.NoticeClient();

        }

        public static Unit GetUnit(this SkillObj self)
        {
            return self.GetParent<SkillComponent>().GetParent<Unit>();
        }

        public static Unit GetCommonEnergyUnit(this SkillObj self)
        {
            Unit unit = self.GetUnit();
            Unit casterUnit = ET.UnitSystem.GetCaster(unit);
            if (casterUnit != null)
            {
                return casterUnit;
            }
            return unit;
        }

        public static void Init(this SkillObj self, string skillCfgId, int skillLevel, SkillSlotType skillSlotType)
        {
            self.skillCfgId = skillCfgId;
            self.skillLevel = skillLevel;
            self.skillSlotType = skillSlotType;

            SkillCfg skillCfg = self.model;
            float radius = UnitHelper.GetBodyRadius(self.GetUnit());
            self.skillDis = math.max(skillCfg.Dis, radius+1f);

            NumericComponent numericComponent = self.AddComponent<NumericComponent>();
            numericComponent.SetAsFloat(NumericType.SkillCDBase, skillCfg.Cd);
            numericComponent.SetAsFloat(NumericType.SkillDisBase, skillCfg.Dis);

            if (SkillConsumeCfgCategory.Instance.Contain(self.skillCfgId))
            {
                SkillConsumeCfg skillConsumeCfg = SkillConsumeCfgCategory.Instance.Get(self.skillCfgId);
                numericComponent.SetAsFloat(NumericType.TotalEnergyModifyBase, skillConsumeCfg.TotalEnergy);
            }

            self.SetEnergyFull();
            self.AddRestoreEnergy();
        }

        public static void AddRestoreEnergy(this SkillObj self)
        {
            if (SkillConsumeCfgCategory.Instance.Contain(self.skillCfgId) == false)
            {
                return;
            }
            SkillConsumeCfg skillConsumeCfg = SkillConsumeCfgCategory.Instance.Get(self.skillCfgId);
            if (skillConsumeCfg.TotalEnergy <= 0)
            {
                return;
            }
            if (skillConsumeCfg.RestoreEnergyByWave <= 0)
            {
                return;
            }
            EventSystem.Instance.Publish(self.DomainScene(), new ET.Ability.AbilityTriggerEventType.GamePlayTowerDefense_AddRestoreEnergy()
            {
                skillObj = self,
            });
        }

        public static void ResetSkillSlotIndex(this SkillObj self, int skillSlotIndex)
        {
            self.skillSlotIndex = skillSlotIndex;
        }

        public static async ETTask DealLearnActionIds(this SkillObj self)
        {
            SkillCfg skillCfg = self.model;

            if (skillCfg.LearnActionId.Count > 0)
            {
                Unit unit = self.GetUnit();

                bool isReady = await ET.AOIHelper.ChkAOIReady(self, unit);
                if (isReady == false)
                {
                    return;
                }
                if (self.IsDisposed)
                {
                    return;
                }

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

        public static async ETTask<(TimelineObj timelineObj, ActionContext actionContext)> CastSkill(this SkillObj self, SelectHandle selectHandleShow = null)
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
            SelectHandle selectHandle = null;
            if (selectHandleShow != null)
            {
                SelectObjectCfg selectObjectCfgShow = ManualSkillCfgCategory.Instance.Get(self.model.Id).SkillSelectAction_Ref;
                selectHandle = SelectHandleHelper.CreateSelectHandleWhenClient(self.GetUnit(), skillCfg.SkillSelectAction_Ref, ref actionContext, selectHandleShow, selectObjectCfgShow);
            }
            else
            {
                selectHandle = SelectHandleHelper.CreateSelectHandle(self.GetUnit(), null, skillCfg.SkillSelectAction_Ref, ref actionContext, false, true);
            }

            if (ET.Ability.SelectHandleHelper.ChkIsNullSelectHandle(selectHandle))
            {
                return (null, actionContext);
            }

            TimelineObj timelineObj = await TimelineHelper.CreateTimeline(self.GetUnit(), skillCfg.TimelineId);
            timelineObj.InitActionContext(ref actionContext);

            self.CostSkill();
            float skillCD = self._GetSkillCD();
            self.cdTotal = skillCD;
            self.cdCountDown = self.cdTotal;



            self.NoticeClient();

            return (timelineObj, actionContext);
        }

        public static async ETTask<(bool ret, string msg)> RestoreSkillEnergy(this SkillObj self)
        {
            await ETTask.CompletedTask;
            if (SkillConsumeCfgCategory.Instance.Contain(self.skillCfgId) == false)
            {
                return (false, "not find");
            }

            if (self.curEnergyNum < self.totalEnergyNum)
            {
                self.curEnergyNum = self.totalEnergyNum;

                self.NoticeClient();
            }

            if (self.GetCurCommonEnergyNum() < self.GetCommonEnergyFullNum())
            {
                self.SetCommonEnergyFull();

                self.NoticeClient();
            }
            return (true, "");
        }

        public static (bool ret, string msg) ChkCanUseSkill(this SkillObj self)
        {
            float cd = self.GetSkillCDCountDown();
            if (cd > 0)
            {
                string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Skill_IsCD");
                return (false, msg);
            }

            var result = self.ChkSkillCost();
            if (result.ret == false)
            {
                string msg = result.msg;
                return (false, msg);
            }

            if (self.skillSlotType == SkillSlotType.InitiativeSkill || self.skillSlotType == SkillSlotType.ManualSkill)
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

        public static float _GetSkillTotalEnergyNum(this SkillObj self)
        {
            NumericComponent numericComponent = self.GetComponent<NumericComponent>();
            float totalEnergyNum = numericComponent.GetAsFloat(NumericType.TotalEnergyModify);
            return totalEnergyNum;
        }

        public static void ResetSkillCDCountDown(this SkillObj self)
        {
            float cdTotal = self._GetSkillCD();
            float cdCountDown = self.cdCountDown;
            if (self.cdTotal >= cdTotal)
            {
                cdCountDown = math.min(cdCountDown, cdTotal);
            }
            else
            {
                cdCountDown += cdTotal - self.cdTotal;
                cdCountDown = math.min(cdCountDown, cdTotal);
            }

            if (MathHelper.IsEqualFloat(self.cdTotal, cdTotal) == false)
            {
                self.cdTotal = cdTotal;
                self.NoticeClient();
            }
            if (MathHelper.IsEqualFloat(self.cdCountDown, cdCountDown) == false)
            {
                self.cdCountDown = cdCountDown;
                self.NoticeClient();
            }
        }

        public static void ResetSkillTotalEnergyNum(this SkillObj self)
        {
            var totalEnergyNum = self._GetSkillTotalEnergyNum();

            if (MathHelper.IsEqualFloat(self.totalEnergyNum, totalEnergyNum) == false)
            {
                self.totalEnergyNum = totalEnergyNum;
                self.NoticeClient();
            }
        }

        public static float GetSkillDis(this SkillObj self)
        {
            return self.skillDis;
        }

        public static void ResetSkillDis(this SkillObj self)
        {
            NumericComponent numericComponent = self.GetComponent<NumericComponent>();
            float skillDis = numericComponent.GetAsFloat(NumericType.SkillDis);
            float radius = UnitHelper.GetBodyRadius(self.GetUnit());
            skillDis = math.max(skillDis, radius+1f);
            if (MathHelper.IsEqualFloat(self.skillDis, skillDis) == false)
            {
                self.skillDis = skillDis;
                self.NoticeClient();
            }
        }

        public static (bool ret, string msg) ChkSkillCost(this SkillObj self)
        {
            if (SkillConsumeCfgCategory.Instance.Contain(self.skillCfgId) == false)
            {
                return (true, "");
            }
            SkillConsumeCfg skillConsumeCfg = SkillConsumeCfgCategory.Instance.Get(self.skillCfgId);
            if (skillConsumeCfg.ConsumeEnergy > 0)
            {
                if (self.curEnergyNum < skillConsumeCfg.ConsumeEnergy)
                {
                    string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Skill_EnergyNotEnough");
                    return (false, msg);
                }
            }

            if (skillConsumeCfg.ConsumeCommonEnergy > 0)
            {
                if (self.GetCurCommonEnergyNum() < skillConsumeCfg.ConsumeCommonEnergy)
                {
                    string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Skill_CommonEnergyNotEnough");
                    return (false, msg);
                }
            }

            return (true, "");
        }

        public static float GetCurCommonEnergyNum(this SkillObj self)
        {
            return self.GetCommonEnergyUnit().GetComponent<SkillComponent>().GetCurCommonEnergyNum();
        }

        public static void CostCommonEnergyNum(this SkillObj self, float costComonEnergyNum)
        {
            self.GetCommonEnergyUnit().GetComponent<SkillComponent>().CostCommonEnergyNum(costComonEnergyNum);
        }

        public static void SetCommonEnergyFull(this SkillObj self)
        {
            self.GetCommonEnergyUnit().GetComponent<SkillComponent>().SetCommonEnergyFull();
        }

        public static int GetCommonEnergyFullNum(this SkillObj self)
        {
            return self.GetCommonEnergyUnit().GetComponent<SkillComponent>().GetCommonEnergyFullNum();
        }

        public static void NoticeClient(this SkillObj self)
        {
            if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
            {
                return;
            }

            if (self.skillSlotType != SkillSlotType.ManualSkill)
            {
                return;
            }

            SkillComponent skillComponent = self.GetParent<SkillComponent>();
            skillComponent.NoticeClient();
            if (self.GetCommonEnergyUnit() != self.GetUnit())
            {
                self.GetCommonEnergyUnit().GetComponent<SkillComponent>().NoticeClient();
            }
        }

        public static bool CostSkill(this SkillObj self)
        {
            if (SkillConsumeCfgCategory.Instance.Contain(self.skillCfgId) == false)
            {
                return true;
            }
            SkillConsumeCfg skillConsumeCfg = SkillConsumeCfgCategory.Instance.Get(self.skillCfgId);
            if (skillConsumeCfg.ConsumeEnergy > 0)
            {
                if (self.curEnergyNum < skillConsumeCfg.ConsumeEnergy)
                {
                    return false;
                }
                else
                {
                    self.curEnergyNum = math.max(self.curEnergyNum - skillConsumeCfg.ConsumeEnergy, 0);
                }
            }

            if (skillConsumeCfg.ConsumeCommonEnergy > 0)
            {
                if (self.GetCurCommonEnergyNum() < skillConsumeCfg.ConsumeCommonEnergy)
                {
                    return false;
                }
                else
                {
                    self.CostCommonEnergyNum(skillConsumeCfg.ConsumeCommonEnergy);
                }
            }

            return true;
        }
    }
}