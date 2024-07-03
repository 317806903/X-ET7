using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Server
{
    [FriendOf(typeof (GamePlayPkComponentBase))]
    [FriendOf(typeof (Unit))]
    public static class GamePlayPKComponentSystem
    {
        public static async ETTask GameBeginWhenServer(this GamePlayPkComponentBase self)
        {

            await ETTask.CompletedTask;
        }

        public static async ETTask GameEndWhenServer(this GamePlayPkComponentBase self)
        {

            await ETTask.CompletedTask;
        }

        public static async ETTask GameRecoverWhenServer(this GamePlayPkComponentBase self, long playerId)
        {

            await ETTask.CompletedTask;
        }

    }
}