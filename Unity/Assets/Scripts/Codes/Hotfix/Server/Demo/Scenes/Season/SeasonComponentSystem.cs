using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET.Server
{
    [FriendOf(typeof(SeasonComponent))]
    public static class SeasonComponentSystem
    {
        [ObjectSystem]
        public class SeasonComponentAwakeSystem : AwakeSystem<SeasonComponent>
        {
            protected override void Awake(SeasonComponent self)
            {
            }
        }

        public static async ETTask RecordWhenSeasonFinished(this SeasonComponent self)
        {
            self.seasonStatus = SeasonStatus.SettlementSeason;
            bool bRetChgSeason2PlayerCache = await self.NoticeChgSeason2PlayerCache();
            bool bRetChgSeason2Rank = await self.NoticeChgSeason2Rank();

            if (self.seasonList == null)
            {
                self.seasonList = new();
            }
            self.seasonList.Add((self.seasonId, self.startTime, self.endTime));
            self.SetDataCacheAutoWrite(true);
            await self.ChkDataCacheAutoWriteFinished();
            self.seasonStatus = SeasonStatus.WaitingNewSeason;

            SeasonInfoCfg seasonInfoCfg = SeasonInfoCfgCategory.Instance.Get(self.seasonId + 1);
            if (seasonInfoCfg == null)
            {
                return;
            }

            self.seasonId = seasonInfoCfg.Id;
            self.startTime = TimeHelper.ServerNow();
            self.seasonStatus = SeasonStatus.InSeason;
            self.SetDataCacheAutoWrite(true);
            await self.ChkDataCacheAutoWriteFinished();
        }

        public static async ETTask<bool> NoticeChgSeason2PlayerCache(this SeasonComponent self)
        {
            StartSceneConfig playerCacheSceneConfig = StartSceneConfigCategory.Instance.GetPlayerCacheManager(self.DomainZone());

            P2S_ChgSeason _P2S_ChgSeason = (P2S_ChgSeason) await ActorMessageSenderComponent.Instance.Call(playerCacheSceneConfig.InstanceId, new S2P_ChgSeason()
            {
                SeasonId = self.seasonId,
            });

            if (_P2S_ChgSeason.Error != ET.ErrorCode.ERR_Success)
            {
                Log.Error($"NoticeChgSeason2PlayerCache Error==1 msg={_P2S_ChgSeason.Message}");
                return false;
            }
            else
            {
                return true;
            }
        }

        public static async ETTask<bool> NoticeChgSeason2Rank(this SeasonComponent self)
        {
            StartSceneConfig rankSceneConfig = StartSceneConfigCategory.Instance.GetRankManager(self.DomainZone());

            R2S_ChgSeason _R2S_ChgSeason = (R2S_ChgSeason) await ActorMessageSenderComponent.Instance.Call(rankSceneConfig.InstanceId, new S2R_ChgSeason()
            {
                SeasonId = self.seasonId,
            });

            if (_R2S_ChgSeason.Error != ET.ErrorCode.ERR_Success)
            {
                Log.Error($"NoticeChgSeason2Rank Error==1 msg={_R2S_ChgSeason.Message}");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}