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

        public static async ETTask<TimelineObj> CreateTimeline(this TimelineComponent self, string timelineCfgId, long casterId)
        {
            if (self.isForeaching)
            {
                await TimerComponent.Instance.WaitFrameAsync();
            }
            TimelineObj timelineObj = self.AddChild<TimelineObj>();
            timelineObj.Init(timelineCfgId, casterId);
            return timelineObj;
        }

        public static async ETTask<TimelineObj> ReplaceTimeline(this TimelineComponent self, long oldTimeLineId, string newTimelineCfgId)
        {
            TimelineObj timelineObjOld = self.GetChild<TimelineObj>(oldTimeLineId);
            if (timelineObjOld == null)
            {
                return null;
            }

            long casterUnitId = timelineObjOld.casterUnitId;
            ActionContext actionContext = timelineObjOld.actionContext;
            self.RemoveChild(oldTimeLineId);
            TimelineObj timelineObj = await self.CreateTimeline(newTimelineCfgId, casterUnitId);
            timelineObj.InitActionContext(actionContext);
            return timelineObj;
        }

        public static async ETTask<TimelineObj> PlayTimeline(this TimelineComponent self, long casterUnitId, string timelineCfgId, ActionContext actionContext)
        {
            TimelineObj timelineObj = await self.CreateTimeline(timelineCfgId, casterUnitId);
            timelineObj.InitActionContext(actionContext);
            return timelineObj;
        }

        public static void JumpTimeline(this TimelineComponent self, long timeLineId, float newTimeElapsed)
        {
            TimelineObj timelineObj = self.GetChild<TimelineObj>(timeLineId);
            if (timelineObj == null)
            {
                return;
            }

            if (newTimeElapsed == -1)
            {
                timelineObj.duration = 0;
                return;
            }

            float curTimeElapsed = timelineObj.timeElapsed;
            timelineObj.timeElapsed = newTimeElapsed;
            if (newTimeElapsed > curTimeElapsed)
            {
                timelineObj.duration = timelineObj.duration + (newTimeElapsed - curTimeElapsed);
            }
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
            self.isForeaching = true;
            foreach (var timelineObjs in self.Children)
            {
                TimelineObj timelineObj = timelineObjs.Value as TimelineObj;
                timelineObj.FixedUpdate(fixedDeltaTime);

                //判断timeline是否终结
                if (timelineObj.duration <= timelineObj.timeElapsed)
                {
                    self.removeList.Add(timelineObjs.Key);
                }
            }
            self.isForeaching = false;

            int count = self.removeList.Count;
            for (int i = 0; i < count; i++)
            {
                self.RemoveChild(self.removeList[i]);
            }

            self.removeList.Clear();
        }
    }
}