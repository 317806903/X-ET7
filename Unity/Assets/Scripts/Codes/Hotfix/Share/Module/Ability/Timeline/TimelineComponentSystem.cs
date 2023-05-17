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

        public static Unit GetUnit(this TimelineComponent self)
        {
            return self.GetParent<Unit>();
        }

        public static TimelineObj CreateTimeline(this TimelineComponent self, string timelineCfgId, long casterId, SelectHandle selectHandle)
        {
            TimelineObj timelineObj = self.AddChild<TimelineObj>();
            timelineObj.Init(timelineCfgId, casterId, selectHandle);
            return timelineObj;
        }
        
        public static TimelineObj ReplaceTimeline(this TimelineComponent self, long oldTimeLineId, string timelineCfgId)
        {
            TimelineObj timelineObj = self.GetChild<TimelineObj>(oldTimeLineId);
            if (timelineObj == null)
            {
                return null;
            }

            long casterUnitId = timelineObj.casterUnitId;
            SelectHandle selectHandle = timelineObj.selectHandle;
            self.RemoveChild(oldTimeLineId);
            return self.CreateTimeline(timelineCfgId, casterUnitId, selectHandle);
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