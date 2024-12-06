using System;
using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
    [FriendOf(typeof(TimelineObj))]
    public static class TimelineObjSystem
    {
        [ObjectSystem]
        public class TimelineObjAwakeSystem: AwakeSystem<TimelineObj>
        {
            protected override void Awake(TimelineObj self)
            {
            }
        }

        [ObjectSystem]
        public class TimelineObjDestroySystem: DestroySystem<TimelineObj>
        {
            protected override void Destroy(TimelineObj self)
            {
            }
        }

        public static void Init(this TimelineObj self, string timelineCfgId, long casterId)
        {
            self.CfgId = timelineCfgId;
            self.casterUnitId = casterId;
            self.timeScale = 1.00f;
            self.duration = self.model.Duration;
            self.timeElapsed = 0;
        }

        public static void InitActionContext(this TimelineObj self, ref ActionContext actionContext)
        {
            actionContext.timelineCfgId = self.CfgId;
            actionContext.timelineId = self.Id;
            self.actionContext = actionContext;
        }

        public static Unit GetUnit(this TimelineObj self)
        {
            return UnitHelper.GetUnit(self.DomainScene(), self.casterUnitId);
        }

        public static void FixedUpdate(this TimelineObj self, float fixedDeltaTime)
        {
            float wasTimeElapsed = self.timeElapsed;
            self.timeElapsed += fixedDeltaTime * self.timeScale;

            int count = self.model.Nodes.Count;
            //执行时间点内的事情
            for (int i = 0; i < count; i++)
            {
                AbilityConfig.TimelineNode timelineNode = self.model.Nodes[i];
                if (
                    timelineNode.TimeElapsed < self.timeElapsed &&
                    timelineNode.TimeElapsed >= wasTimeElapsed
                )
                {
                    bool bRetChk = ET.Ability.ActionHandlerHelper.ChkActionCondition(self.GetUnit(), timelineNode.ChkCondition1, timelineNode.ChkCondition2, timelineNode.ChkCondition1SelectObj_Ref, timelineNode.ChkCondition2SelectObj_Ref, ref self.actionContext);
                    if (bRetChk == false)
                    {
                        continue;
                    }

                    SelectHandle curSelectHandle = SelectHandleHelper.CreateSelectHandle(self.GetUnit(), null, timelineNode.ActionCallParam_Ref, ref self.actionContext);
                    if (curSelectHandle == null)
                    {
                        continue;
                    }

                    bool bRet = ET.Ability.ActionHandlerHelper.DoActionTriggerHandler(self.GetUnit(), self.GetUnit(), timelineNode.DelayTime, timelineNode.ActionId, timelineNode.FilterCondition1, timelineNode.FilterCondition2, curSelectHandle, null, ref self.actionContext);
                    if (bRet)
                    {
                        foreach (string actionId in timelineNode.ActionId)
                        {
                            if (actionId.StartsWith("TimelineJumpTime"))
                            {
                                self.timelineJumpNum++;
                                if (self.timelineJumpNum > 10)
                                {
                                    self.timeElapsed = wasTimeElapsed + 0.0001f;
                                    self.timelineJumpNum = 0;
                                }
                                return;
                            }
                        }
                    }

                }
            }
        }


    }
}