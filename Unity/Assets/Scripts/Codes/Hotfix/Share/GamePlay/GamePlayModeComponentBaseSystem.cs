using ET.Ability;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DotRecast.Detour;
using DotRecast.Recast.Toolset;
using DotRecast.Recast.Toolset.Builder;
using DotRecast.Recast.Toolset.Geom;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof (GamePlayModeComponentBase))]
    [FriendOf(typeof (Unit))]
    public static class GamePlayModeComponentBaseSystem
    {
        [ObjectSystem]
        public class GamePlayModeComponentAwakeSystem: AwakeSystem<GamePlayModeComponentBase>
        {
            protected override void Awake(GamePlayModeComponentBase self)
            {
            }
        }

        [ObjectSystem]
        public class GamePlayModeComponentDestroySystem: DestroySystem<GamePlayModeComponentBase>
        {
            protected override void Destroy(GamePlayModeComponentBase self)
            {
            }
        }

        [ObjectSystem]
        public class GamePlayModeComponentFixedUpdateSystem: FixedUpdateSystem<GamePlayModeComponentBase>
        {
            protected override void FixedUpdate(GamePlayModeComponentBase self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static void FixedUpdate(this GamePlayModeComponentBase self, float fixedDeltaTime)
        {
        }

        public static GamePlayComponent GetGamePlay(this GamePlayModeComponentBase self)
        {
            GamePlayComponent gamePlayComponent = self.GetParent<GamePlayComponent>();
            return gamePlayComponent;
        }

        public static List<long> GetPlayerList(this GamePlayModeComponentBase self)
        {
            GamePlayComponent gamePlayComponent = self.GetGamePlay();
            return gamePlayComponent.GetPlayerList();
        }

        public static bool ChkPlayerIsQuit(this GamePlayModeComponentBase self, long playerId)
        {
            GamePlayComponent gamePlayComponent = self.GetGamePlay();
            return gamePlayComponent.ChkPlayerIsQuit(playerId);
        }

        public static void NoticeToClientAll(this GamePlayModeComponentBase self)
        {
            List<long> playerList = self.GetPlayerList();
            for (int i = 0; i < playerList.Count; i++)
            {
                self.NoticeToClient(playerList[i]);
            }
        }

        public static void NoticeToClient(this GamePlayModeComponentBase self, long playerId)
		{
			EventType.WaitNoticeGamePlayModeToClient _WaitNoticeGamePlayModeChgToClient = new ()
			{
				playerId = playerId,
				gamePlayComponent = self.GetGamePlay(),
			};
			EventSystem.Instance.Publish(self.DomainScene(), _WaitNoticeGamePlayModeChgToClient);
        }
    }
}