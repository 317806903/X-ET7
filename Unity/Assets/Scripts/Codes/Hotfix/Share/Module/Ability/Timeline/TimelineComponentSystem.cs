using System;
using System.Collections.Generic;

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
                TimelineComponent.Instance = self;
            }
        }

        [ObjectSystem]
        public class TimelineComponentDestroySystem: DestroySystem<TimelineComponent>
        {
            protected override void Destroy(TimelineComponent self)
            {
                TimelineComponent.Instance = null;
            }
        }
        
        [ObjectSystem]
        public class TimelineComponentFixedUpdateSystem: FixedUpdateSystem<TimelineComponent>
        {
            protected override void FixedUpdate(TimelineComponent self)
            {
                self.FixedUpdate();
            }
        }

        public static TimelineObj CreateTimeline(this TimelineComponent self, Unit castUnit, int timelineId)
        {
            TimelineModel timelineModel = new TimelineModel();//timelineId
            
            TimelineObj timelineObj = self.AddChild<TimelineObj, TimelineModel, Unit>(timelineModel, castUnit);
            return timelineObj;
        }

        public static void FixedUpdate(this TimelineComponent self)
        {
            if (self.Children.Count <= 0)
            {
                return;
            }

            float fixedDeltaTime = TimeHelper.FixedDetalTime;

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
                if (timelineObj.model.duration <= timelineObj.timeElapsed)
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