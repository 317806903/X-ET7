using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET.Server
{
    [FriendOf(typeof(SeasonComponentStatusInSeason))]
    public static class SeasonComponentStatusInSeasonSystem
    {
        [ObjectSystem]
        public class SeasonComponentStatusInSeasonAwakeSystem : AwakeSystem<SeasonComponentStatusInSeason>
        {
            protected override void Awake(SeasonComponentStatusInSeason self)
            {
            }
        }

        [ObjectSystem]
        public class SeasonComponentStatusInSeasonFixedUpdateSystem: FixedUpdateSystem<SeasonComponentStatusInSeason>
        {
            protected override void FixedUpdate(SeasonComponentStatusInSeason self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Season)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static void FixedUpdate(this SeasonComponentStatusInSeason self, float fixedDeltaTime)
        {
            SeasonComponent seasonComponent = self.GetSeasonComponent();
            if (seasonComponent == null)
            {
                return;
            }

            if (++self.curFrameChk >= self.waitFrameChk)
            {
                self.curFrameChk = 0;

                bool isSeasonEnd = self.ChkIsSeasonEnd();
                if (isSeasonEnd)
                {
                    self.DealSeasonFinish();
                }
            }
        }

        public static SeasonComponent GetSeasonComponent(this SeasonComponentStatusInSeason self)
        {
            return self.GetParent<SeasonComponent>();
        }

        public static bool ChkIsSeasonEnd(this SeasonComponentStatusInSeason self)
        {
            SeasonComponent seasonComponent = self.GetSeasonComponent();

            if (ET.TimeHelper.ChkIsAfter(seasonComponent.endTime, TimeHelper.ServerNow()))
            {
                return false;
            }

            return true;
        }

        public static void DealSeasonFinish(this SeasonComponentStatusInSeason self)
        {
            SeasonComponent seasonComponent = self.GetSeasonComponent();

            SeasonComponentStatusSettlement seasonComponentStatusSettlement = seasonComponent.AddComponent<SeasonComponentStatusSettlement>();
            seasonComponentStatusSettlement.Init();

            self.Dispose();
        }

    }
}