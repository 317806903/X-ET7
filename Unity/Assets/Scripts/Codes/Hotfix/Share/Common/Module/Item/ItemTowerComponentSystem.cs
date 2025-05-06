using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET
{
    [FriendOf(typeof(ItemTowerComponent))]
    public static class ItemTowerComponentSystem
    {
        [ObjectSystem]
        public class ItemTowerComponentAwakeSystem : AwakeSystem<ItemTowerComponent>
        {
            protected override void Awake(ItemTowerComponent self)
            {
                self.CfgId = self.GetItem().CfgId;
                self.level = GlobalSettingCfgCategory.Instance.TowerLevelWhenGot;
            }
        }

        public static ItemComponent GetItem(this ItemTowerComponent self)
        {
            return self.GetParent<ItemComponent>();
        }
    }
}