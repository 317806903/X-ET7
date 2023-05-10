using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class TimelineHelper
    {
        public static TimelineObj CreateTimeline(Unit castUnit, int timelineId)
        {
            TimelineObj timelineObj = TimelineComponent.Instance.CreateTimeline(castUnit, timelineId);
            return timelineObj;
        }
    }
}