using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET
{
    [FriendOf(typeof(ItemComponent))]
    public static class ItemComponentSystem
    {
        [ObjectSystem]
        public class ItemComponentAwakeSystem : AwakeSystem<ItemComponent>
        {
            protected override void Awake(ItemComponent self)
            {

            }
        }

        public static void Init(this ItemComponent self, string itemCfgId, int count)
        {
            self.CfgId = itemCfgId;
            self.count = count;

            self._InitComponent();
        }

        public static void _InitComponent(this ItemComponent self)
        {
            if (self.model.ItemType == ItemType.Tower)
            {
                self.AddComponent<ItemTowerComponent>();
            }
        }

        public static string GetItemName(this ItemComponent self)
        {
            return self.model.Name;
        }

        public static string GetItemDesc(this ItemComponent self)
        {
            return self.model.Desc;
        }

        public static string GetItemIcon(this ItemComponent self)
        {
            ResIconCfg resIconCfg = ResIconCfgCategory.Instance.Get(self.model.Icon);
            return resIconCfg.ResName;
        }

        public static QualityType GetItemQualityType(this ItemComponent self)
        {
            return self.model.QualityType;
        }

    }
}