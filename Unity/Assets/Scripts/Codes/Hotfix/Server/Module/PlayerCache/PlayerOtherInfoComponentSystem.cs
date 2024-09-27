using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using ET.Server;

namespace ET.Server
{
    [FriendOf(typeof(PlayerOtherInfoComponent))]
    public static class PlayerOtherInfoComponentSystem
    {
        [ObjectSystem]
        public class PlayerOtherInfoComponentFixedUpdateSystem : FixedUpdateSystem<PlayerOtherInfoComponent>
        {
            protected override void FixedUpdate(PlayerOtherInfoComponent self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Gate)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static void FixedUpdate(this PlayerOtherInfoComponent self, float fixedDeltaTime)
        {
            if (++self.curFrameChk >= self.waitFrameChk)
            {
                self.curFrameChk = 0;


                ET.Server.PlayerCacheHelper.ChkIsNewMailRedDot(self.DomainScene(), self.GetPlayerId()).Coroutine();
            }
        }
    }
}