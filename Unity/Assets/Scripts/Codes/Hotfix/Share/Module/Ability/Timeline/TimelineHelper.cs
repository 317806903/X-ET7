using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class TimelineHelper
    {
        public static TimelineComponent GetTimelineComponent(Unit unit)
        {
            TimelineComponent timelineComponent = unit.GetComponent<TimelineComponent>();
            if (timelineComponent == null)
            {
                timelineComponent = unit.AddComponent<TimelineComponent>();
            }
            return timelineComponent;
        }

        public static async ETTask<TimelineObj> CreateTimeline(Unit unit, string timelineCfgId)
        {
            TimelineObj timelineObj = await GetTimelineComponent(unit).CreateTimeline(timelineCfgId, unit.Id);
            return timelineObj;
        }

        public static async ETTask<TimelineObj> ReplaceTimeline(Unit unit, long oldTimeLineId, string newTimelineCfgId)
        {
            TimelineObj timelineObj = await GetTimelineComponent(unit).ReplaceTimeline(oldTimeLineId, newTimelineCfgId);
            return timelineObj;
        }

        public static async ETTask<TimelineObj> PlayTimeline(Unit unit, long casterUnitId, string timelineCfgId, ActionContext actionContext)
        {
            ActionContext newActionContext = new();
            newActionContext.unitId = unit.Id;
            newActionContext.skillCfgId = actionContext.skillCfgId;
            newActionContext.skillSlotType = actionContext.skillSlotType;
            newActionContext.skillLevel = actionContext.skillLevel;
            TimelineObj timelineObj = await GetTimelineComponent(unit).PlayTimeline(casterUnitId, timelineCfgId, newActionContext);
            return timelineObj;
        }

        public static void JumpTimeline(Unit unit, float newTimeElapsed, ref ActionContext actionContext)
        {
            long timeLineId = actionContext.timelineId;
            GetTimelineComponent(unit).JumpTimeline(timeLineId, newTimeElapsed);
        }
    }
}