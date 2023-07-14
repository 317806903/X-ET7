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
        }
        
        public static void InitActionContext(this TimelineObj self, ActionContext actionContext)
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

            // //判断有没有返回点
            // if (
            //     self.model.chargeGoBack.atDuration < self.timeElapsed &&
            //     self.model.chargeGoBack.atDuration >= wasTimeElapsed
            // )
            {
                //if (self.casterUnit != null)
                {
                    // TODO zpb
                    // ChaState cs = self.caster.GetComponent<ChaState>();
                    // if (cs.charging == true)
                    // {
                    //     self.timeElapsed = self.model.chargeGoBack.gotoDuration;
                    //     continue;
                    // }
                }
            }

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
                    SelectHandle curSelectHandle = SelectHandleHelper.CreateSelectHandle(self.GetUnit(), null, timelineNode.ActionCallParam);

                    (bool bRet1, bool isChgSelect1, SelectHandle newSelectHandle1) = ConditionHandleHelper.ChkCondition(self.GetUnit(), curSelectHandle, timelineNode.ActionCondition1, self.actionContext);
                    if (isChgSelect1)
                    {
                        curSelectHandle = newSelectHandle1;
                    }
                    (bool bRet2, bool isChgSelect2, SelectHandle newSelectHandle2) = ConditionHandleHelper.ChkCondition(self.GetUnit(), curSelectHandle, timelineNode.ActionCondition2, self.actionContext);
                    if (isChgSelect2)
                    {
                        curSelectHandle = newSelectHandle2;
                    }

                    if (bRet1 && bRet2)
                    {
                        ActionHandlerHelper.CreateAction(self.GetUnit(), null, timelineNode.ActionId, timelineNode.DelayTime, curSelectHandle, self
                        .actionContext);
                    }
                }
            }
        }
        
        
    }
}