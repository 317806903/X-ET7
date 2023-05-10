using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class TagHelper
    {
        public static void AddTag(Unit unit, int tagCfgId)
        {
            unit.GetComponent<TagComponent>().AddTag(tagCfgId);
        }
        
    }
}