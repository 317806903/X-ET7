using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class TimelineHelper
    {
        public static TimelineComponent GetTimelineComponent(Unit unit)
        {
            TimelineComponent timelineComponent = unit.GetComponent<TimelineComponent>();
            return timelineComponent;
        }
        
        public static TimelineObj CreateTimeline(Unit unit, int timelineId)
        {
            TimelineObj timelineObj = GetTimelineComponent(unit).CreateTimeline(unit, timelineId);
            return timelineObj;
        }
    }
}