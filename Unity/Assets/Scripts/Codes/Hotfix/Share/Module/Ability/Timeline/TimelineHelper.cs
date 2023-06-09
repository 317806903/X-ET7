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
        
        public static TimelineObj CreateTimeline(Unit unit, string timelineCfgId, SelectHandle selectHandle)
        {
            TimelineObj timelineObj = GetTimelineComponent(unit).CreateTimeline(timelineCfgId, unit.Id, selectHandle);
            return timelineObj;
        }
    }
}