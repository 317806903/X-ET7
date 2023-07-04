using System;
using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
    [FriendOf(typeof(TimelineComponent))]
    [FriendOf(typeof(TimelineObj))]
    public static class TimelineComponentSystem
    {
        [ObjectSystem]
        public class TimelineComponentAwakeSystem: AwakeSystem<TimelineComponent>
        {
            protected override void Awake(TimelineComponent self)
            {
            }
        }

        [ObjectSystem]
        public class TimelineComponentDestroySystem: DestroySystem<TimelineComponent>
        {
            protected override void Destroy(TimelineComponent self)
            {
            }
        }
        
        [ObjectSystem]
        public class TimelineComponentFixedUpdateSystem: FixedUpdateSystem<TimelineComponent>
        {
            protected override void FixedUpdate(TimelineComponent self)
            {
                if (self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static Unit GetUnit(this TimelineComponent self)
        {
            return self.GetParent<Unit>();
        }

        public static TimelineObj CreateTimeline(this TimelineComponent self, string timelineCfgId, long casterId)
        {
            TimelineObj timelineObj = self.AddChild<TimelineObj>();
            timelineObj.Init(timelineCfgId, casterId);
            return timelineObj;
        }
        
        public static TimelineObj ReplaceTimeline(this TimelineComponent self, long oldTimeLineId, string timelineCfgId)
        {
            TimelineObj timelineObjOld = self.GetChild<TimelineObj>(oldTimeLineId);
            if (timelineObjOld == null)
            {
                return null;
            }

            long casterUnitId = timelineObjOld.casterUnitId;
            ActionContext actionContext = timelineObjOld.actionContext;
            self.RemoveChild(oldTimeLineId);
            TimelineObj timelineObj = self.CreateTimeline(timelineCfgId, casterUnitId);
            timelineObj.InitActionContext(actionContext);
            return timelineObj;
        }

        public static void FixedUpdate(this TimelineComponent self, float fixedDeltaTime)
        {
            if (self.Children.Count <= 0)
            {
                return;
            }

            if(self.removeList ==  null)
            {
                self.removeList = new();
            }
            else
            {
                self.removeList.Clear();
            }
            foreach (var timelineObjs in self.Children)
            {
                TimelineObj timelineObj = timelineObjs.Value as TimelineObj;
                timelineObj.FixedUpdate(fixedDeltaTime);

                //判断timeline是否终结
                if (timelineObj.model.Duration <= timelineObj.timeElapsed)
                {
                    self.removeList.Add(timelineObjs.Key);
                }
            }

            int count = self.removeList.Count;
            for (int i = 0; i < count; i++)
            {
                self.RemoveChild(self.removeList[i]);
            }

            self.removeList.Clear();
        }
    }
}