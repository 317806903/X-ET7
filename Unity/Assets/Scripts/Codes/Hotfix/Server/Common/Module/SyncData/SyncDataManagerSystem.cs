using System;
using System.Collections.Generic;
using System.Diagnostics;
using ET.AbilityConfig;

namespace ET.Server
{
    [ComponentOf(typeof (SyncDataManager))]
    public static class SyncDataManagerSystem
    {
        [ObjectSystem]
        public class SyncDataManagerFixedUpdateSystem: LateUpdateSystem<SyncDataManager>
        {
            protected override void LateUpdate(SyncDataManager self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static void FixedUpdate(this SyncDataManager self, float fixedDeltaTime)
        {
            if (++self.curFrameSync >= self.waitFrameSync)
            {
                self.curFrameSync = 0;

                self.GetPlayerSessionInfo().Coroutine();
            }
        }

        public static async ETTask GetPlayerSessionInfo(this SyncDataManager self)
        {
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(self.DomainScene());
            if (gamePlayComponent == null)
            {
                return;
            }
            foreach (long playerId in gamePlayComponent.GetPlayerList())
            {
                if (self.IsDisposed)
                {
                    return;
                }
                try
                {
                    ActorLocationSenderOneType actorLocationSenderOneType = ActorLocationSenderComponent.Instance?.Get(LocationType.Player);
                    G2M_GetPlayerSessionInfo _G2M_GetPlayerSessionInfo = await actorLocationSenderOneType?.Call(playerId, new M2G_GetPlayerSessionInfo(), self.DomainScene().InstanceId) as G2M_GetPlayerSessionInfo;
                    if (self.IsDisposed)
                    {
                        return;
                    }

                    if (_G2M_GetPlayerSessionInfo.Error == ET.ErrorCode.ERR_Success)
                    {
                        self.playerSessionInfoList[playerId] = self.GetPlayerSessionInfo(_G2M_GetPlayerSessionInfo.Fps, _G2M_GetPlayerSessionInfo.PingTime);
                    }
                }
                catch (Exception e)
                {
                    Log.Error($"playerId[{playerId}] {e}");
                }

            }


        }

        public static int GetPlayerSessionInfo(this SyncDataManager self, int fps, long pingTime)
        {
            int synFrame = 0;
            if (pingTime > 0)
            {
                synFrame = (int)(((float)pingTime) * ET.TimeHelper.LogicFrame * 0.001f);
            }
            if (pingTime > 500 && pingTime <= 1000)
            {
                synFrame = (int)(synFrame*1.2f);
            }
            else if (pingTime > 1000 && pingTime <= 3000)
            {
                synFrame = (int)(synFrame*2f);
            }
            else if (pingTime > 2000)
            {
                synFrame = (int)(synFrame*3f);
            }
            if (fps < 20)
            {
                synFrame += 10;
            }
            if (synFrame < 3)
            {
                synFrame = 3;
            }
            //Log.Debug($"zpb synFrame[{synFrame}] fps[{fps}] pingTime[{pingTime}]");

            //synFrame = RandomGenerator.RandomNumber(3, 20);
            //synFrame = 100;
            return synFrame;
        }
    }
}