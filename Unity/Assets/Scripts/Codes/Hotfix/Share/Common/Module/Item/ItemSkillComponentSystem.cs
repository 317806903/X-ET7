using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET
{
    [FriendOf(typeof(ItemSkillComponent))]
    public static class ItemSkillComponentSystem
    {
        [ObjectSystem]
        public class ItemSkillComponentAwakeSystem : AwakeSystem<ItemSkillComponent>
        {
            protected override void Awake(ItemSkillComponent self)
            {
                self.CfgId = self.GetItem().CfgId;
                self.level = GlobalSettingCfgCategory.Instance.SkillLevelWhenGot;
            }
        }

        public static ItemComponent GetItem(this ItemSkillComponent self)
        {
            return self.GetParent<ItemComponent>();
        }
    }
}