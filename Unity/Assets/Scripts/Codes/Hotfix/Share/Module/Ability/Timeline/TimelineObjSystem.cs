using System;
using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
    [FriendOf(typeof(TimelineObj))]
    public static class TimelineObjSystem
    {
        [ObjectSystem]
        public class TimelineObjAwakeSystem: AwakeSystem<TimelineObj, TimelineCfg, Unit>
        {
            protected override void Awake(TimelineObj self, TimelineCfg model, Unit caster)
            {
                self.model = model;
                self.casterUnitId = caster.Id;
                self.values = new Dictionary<string, object>();
                self.timeScale = 1.00f;
                // TODO zpb
                // if (casterUnit != null)
                // {
                //     ChaState cs = caster.GetComponent<ChaState>();
                //     if (cs)
                //     {
                //         this.values.Add("faceDegree", cs.faceDegree);
                //         this.values.Add("moveDegree", cs.moveDegree);
                //     }
                //
                //     this._timeScale = cs.actionSpeed;
                // }
            }
        }

        [ObjectSystem]
        public class TimelineObjDestroySystem: DestroySystem<TimelineObj>
        {
            protected override void Destroy(TimelineObj self)
            {
            }
        }

        public static Unit GetCasterUnit(this TimelineObj self)
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
                    ActionHandlerHelper.CreateAction(self.GetCasterUnit(), timelineNode.ActionId, null);
                }
            }
        }
        
        
    }
}