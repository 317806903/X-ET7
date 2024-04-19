using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Server
{
    [FriendOf(typeof (GamePlayPKComponent))]
    [FriendOf(typeof (Unit))]
    public static class GamePlayPKComponentSystem
    {
        public static async ETTask GameBeginWhenServer(this GamePlayPKComponent self)
        {

            await ETTask.CompletedTask;
        }

        public static async ETTask GameEndWhenServer(this GamePlayPKComponent self)
        {

            await ETTask.CompletedTask;
        }

    }
}