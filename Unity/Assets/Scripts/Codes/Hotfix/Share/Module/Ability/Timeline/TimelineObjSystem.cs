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

        public static void Init(this TimelineObj self, string timelineCfgId, long casterId, SelectHandle selectHandle)
        {
            self.model = TimelineCfgCategory.Instance.Get(timelineCfgId);
            self.casterUnitId = casterId;
            self.timeScale = 1.00f;
            self.selectHandle = selectHandle;
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
                    if (timelineNode.ActionCallParam is ActionCallSelectLast)
                    {
                        ActionHandlerHelper.CreateAction(self.GetUnit(), timelineNode.ActionId, self.selectHandle);
                    }
                    else
                    {
                        SelectHandle selectHandle = SelectHandleHelper.GetSelectHandle(self.GetUnit(), timelineNode.ActionCallParam);
                        ActionHandlerHelper.CreateAction(self.GetUnit(), timelineNode.ActionId, selectHandle);
                    }
                }
            }
        }
        
        
    }
}