using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET.Server
{
    [FriendOf(typeof(SeasonComponentStatusSettlement))]
    public static class SeasonComponentStatusSettlementSystem
    {
        [ObjectSystem]
        public class SeasonComponentStatusSettlementAwakeSystem : AwakeSystem<SeasonComponentStatusSettlement>
        {
            protected override void Awake(SeasonComponentStatusSettlement self)
            {
            }
        }

        public static void Init(this SeasonComponentStatusSettlement self)
        {
            self._Init().Coroutine();
        }

        public static SeasonComponent GetSeasonComponent(this SeasonComponentStatusSettlement self)
        {
            return self.GetParent<SeasonComponent>();
        }

        public static async ETTask _Init(this SeasonComponentStatusSettlement self)
        {
            SeasonComponent seasonComponent = self.GetSeasonComponent();
            Log.Error($"ET.Server.SeasonComponentStatusSettlementSystem._Init[{seasonComponent.seasonIndex}][{seasonComponent.seasonCfgId}] 111");
            bool bRetChgSeason2PlayerCache = await self.NoticeChgSeason2PlayerCache();
            bool bRetChgSeason2Rank = await self.NoticeChgSeason2Rank();

            Log.Error($"ET.Server.SeasonComponentStatusSettlementSystem._Init[{seasonComponent.seasonIndex}][{seasonComponent.seasonCfgId}] 222");

            SeasonHistoryComponent seasonHistoryComponent = self.AddChild<SeasonHistoryComponent>();
            seasonHistoryComponent.Init(seasonComponent.seasonIndex, seasonComponent.seasonCfgId, seasonComponent.startTime, seasonComponent.endTime, seasonComponent.initTime, TimeHelper.ServerNow());
            seasonHistoryComponent.SetDataCacheAutoWrite(true);
            await seasonHistoryComponent.ChkDataCacheAutoWriteFinished();

            seasonComponent.SetDataCacheAutoWrite(true);
            await seasonComponent.ChkDataCacheAutoWriteFinished();

            self.Dispose();
            await seasonComponent.GoToNextSeason();

        }

        public static async ETTask<bool> NoticeChgSeason2PlayerCache(this SeasonComponentStatusSettlement self)
        {
            SeasonComponent seasonComponent = self.GetSeasonComponent();
            StartSceneConfig playerCacheSceneConfig = StartSceneConfigCategory.Instance.GetPlayerCacheManager(self.DomainZone());

            P2S_ChgSeason _P2S_ChgSeason = (P2S_ChgSeason) await ActorMessageSenderComponent.Instance.Call(playerCacheSceneConfig.InstanceId, new S2P_ChgSeason()
            {
                SeasonIndex = seasonComponent.seasonIndex,
                SeasonCfgId = seasonComponent.seasonCfgId,
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

        public static async ETTask<bool> NoticeChgSeason2Rank(this SeasonComponentStatusSettlement self)
        {
            SeasonComponent seasonComponent = self.GetSeasonComponent();
            StartSceneConfig rankSceneConfig = StartSceneConfigCategory.Instance.GetRankManager(self.DomainZone());

            R2S_ChgSeason _R2S_ChgSeason = (R2S_ChgSeason) await ActorMessageSenderComponent.Instance.Call(rankSceneConfig.InstanceId, new S2R_ChgSeason()
            {
                SeasonIndex = seasonComponent.seasonIndex,
                SeasonCfgId = seasonComponent.seasonCfgId,
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