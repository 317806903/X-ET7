using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET
{
    [FriendOf(typeof(PlayerBackPackComponent))]
    public static class PlayerBackPackComponentSystem
    {
        [ObjectSystem]
        public class PlayerBackPackComponentAwakeSystem : AwakeSystem<PlayerBackPackComponent>
        {
            protected override void Awake(PlayerBackPackComponent self)
            {

            }
        }

        public static long GetPlayerId(this PlayerBackPackComponent self)
        {
            return self.GetParent<PlayerDataComponent>().playerId;
        }

    }
}