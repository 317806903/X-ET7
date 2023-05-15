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
        //
        // [ObjectSystem]
        // public class TimelineComponentFixedUpdateSystem: FixedUpdateSystem<TimelineComponent>
        // {
        //     protected override void FixedUpdate(TimelineComponent self)
        //     {
        //         self.FixedUpdate();
        //     }
        // }

        public static TimelineObj CreateTimeline(this TimelineComponent self, Unit castUnit, string timelineId)
        {
            TimelineCfg timelineCfg = TimelineCfgCategory.Instance.Get(timelineId);
            TimelineObj timelineObj = self.AddChild<TimelineObj, TimelineCfg, Unit>(timelineCfg, castUnit);
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