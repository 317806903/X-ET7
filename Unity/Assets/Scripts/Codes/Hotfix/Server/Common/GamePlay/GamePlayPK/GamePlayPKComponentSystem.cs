using System.Collections.Generic;
using ET.Ability;
using Unity.Mathematics;

namespace ET.Server
{
    [FriendOf(typeof (GamePlayPkComponent))]
    [FriendOf(typeof (Unit))]
    public static class GamePlayPKComponentSystem
    {
        public static async ETTask GameBeginWhenServer(this GamePlayPkComponent self)
        {

            await ETTask.CompletedTask;
        }

        public static async ETTask GameEndWhenServer(this GamePlayPkComponent self)
        {

            await ETTask.CompletedTask;
        }

        public static async ETTask GameRecoverWhenServer(this GamePlayPkComponent self, long playerId)
        {

            await ETTask.CompletedTask;
        }

    }
}