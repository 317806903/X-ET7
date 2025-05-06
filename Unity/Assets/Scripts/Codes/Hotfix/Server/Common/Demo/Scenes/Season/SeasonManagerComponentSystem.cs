using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET.Server
{
    [FriendOf(typeof(SeasonManagerComponent))]
    public static class SeasonManagerComponentSystem
    {
        [ObjectSystem]
        public class SeasonManagerComponentAwakeSystem : AwakeSystem<SeasonManagerComponent>
        {
            protected override void Awake(SeasonManagerComponent self)
            {
            }
        }

        public static void Init(this SeasonManagerComponent self)
        {
            self.InitByDBOne().Coroutine();
        }

        public static async ETTask<SeasonComponent> InitByDBOne(this SeasonManagerComponent self)
        {
            SeasonComponent seasonComponent;
            List<SeasonComponent> list = await ET.Server.DBHelper.LoadDBListWithParent2Child<SeasonComponent>(self);
            if (list == null || list.Count == 0)
            {
                seasonComponent = self.AddChild<SeasonComponent>();
                ET.Server.SeasonHelper.SetCurSeasonByTime(seasonComponent);
                seasonComponent.initTime = TimeHelper.ServerNow();
                seasonComponent.AddComponent<SeasonComponentStatusInSeason>();
                seasonComponent.SetDataCacheAutoWrite();
            }
            else
            {
                seasonComponent = list[0];

                SeasonComponentStatusSettlement seasonComponentStatusSettlement = seasonComponent.GetComponent<SeasonComponentStatusSettlement>();
                if (seasonComponentStatusSettlement != null)
                {
                    seasonComponentStatusSettlement.Init();
                }
                else
                {
                    SeasonComponentStatusInSeason seasonComponentStatusInSeason = seasonComponent.GetComponent<SeasonComponentStatusInSeason>();
                    if (seasonComponentStatusInSeason == null)
                    {
                        seasonComponent.AddComponent<SeasonComponentStatusInSeason>();
                    }
                }
            }
            self.SeasonComponent = seasonComponent;
            return seasonComponent;
        }

    }
}