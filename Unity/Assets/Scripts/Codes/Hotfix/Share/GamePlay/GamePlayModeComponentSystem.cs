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
    [FriendOf(typeof (GamePlayModeComponent))]
    [FriendOf(typeof (Unit))]
    public static class GamePlayModeComponentSystem
    {
        [ObjectSystem]
        public class GamePlayModeComponentAwakeSystem: AwakeSystem<GamePlayModeComponent>
        {
            protected override void Awake(GamePlayModeComponent self)
            {
            }
        }

        [ObjectSystem]
        public class GamePlayModeComponentDestroySystem: DestroySystem<GamePlayModeComponent>
        {
            protected override void Destroy(GamePlayModeComponent self)
            {
            }
        }

        [ObjectSystem]
        public class GamePlayModeComponentFixedUpdateSystem: FixedUpdateSystem<GamePlayModeComponent>
        {
            protected override void FixedUpdate(GamePlayModeComponent self)
            {
                if (self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static void FixedUpdate(this GamePlayModeComponent self, float fixedDeltaTime)
        {
        }

        public static GamePlayComponent GetGamePlay(this GamePlayModeComponent self)
        {
            GamePlayComponent gamePlayComponent = self.GetParent<GamePlayComponent>();
            return gamePlayComponent;
        }

        public static List<long> GetPlayerList(this GamePlayModeComponent self)
        {
            GamePlayComponent gamePlayComponent = self.GetGamePlay();
            return gamePlayComponent.GetPlayerList();
        }

        public static void NoticeToClientAll(this GamePlayModeComponent self)
        {
            List<long> playerList = self.GetPlayerList();
            for (int i = 0; i < playerList.Count; i++)
            {
                self.NoticeToClient(playerList[i]);
            }
        }

        public static void NoticeToClient(this GamePlayModeComponent self, long playerId)
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