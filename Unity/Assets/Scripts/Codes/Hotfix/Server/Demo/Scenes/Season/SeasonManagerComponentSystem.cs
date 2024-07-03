using System.Collections.Generic;
using System.Linq;

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
                self.InitByDBOne().Coroutine();
            }
        }

        public static async ETTask RecordWhenSeasonFinished(this SeasonManagerComponent self)
        {
            SeasonComponent seasonComponent = self.SeasonComponent;
            if (seasonComponent == null)
            {
                return;
            }

            await seasonComponent.RecordWhenSeasonFinished();
        }

        public static async ETTask<SeasonComponent> InitByDBOne(this SeasonManagerComponent self)
        {
            SeasonComponent seasonComponent;
            List<SeasonComponent> list = await ET.Server.DBHelper.LoadDBListWithParent2Child<SeasonComponent>(self);
            if (list == null || list.Count == 0)
            {
                seasonComponent = self.AddChild<SeasonComponent>();
                seasonComponent.seasonId = 1;
                seasonComponent.startTime = TimeHelper.ServerNow();
                seasonComponent.SetDataCacheAutoWrite();
            }
            else
            {
                seasonComponent = list[0];
            }
            self.SeasonComponent = seasonComponent;
            return seasonComponent;
        }

    }
}