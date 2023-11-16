using System.Collections.Generic;
using System.Linq;

namespace ET
{
    [FriendOf(typeof(RankShowPlayerComponent))]
    public static class RankShowPlayerComponentSystem
    {
        [ObjectSystem]
        public class RankShowPlayerComponentAwakeSystem : AwakeSystem<RankShowPlayerComponent>
        {
            protected override void Awake(RankShowPlayerComponent self)
            {
            }
        }

        public static RankShowComponent GetRankShow(this RankShowPlayerComponent self, RankType rankType)
        {
            if (self.RankList.TryGetValue(rankType, out long rankShowComponentId))
            {
                RankShowComponent rankShowComponent = self.GetChild<RankShowComponent>(rankShowComponentId);
                if (rankShowComponent == null)
                {
                    self.RankList.Remove(rankType);
                }
                return rankShowComponent;
            }
            else
            {
                return null;
            }
        }

        public static RankShowComponent SetRankShow(this RankShowPlayerComponent self, RankType rankType, SortedDictionary<int, RankItemComponent> rankIndex2PlayerId)
        {
            RankShowComponent rankShowComponent = self.AddChild<RankShowComponent>();
            rankShowComponent.SetRankShow(rankIndex2PlayerId);
            DataCacheClearComponent dataCacheClearComponent = rankShowComponent.AddComponent<DataCacheClearComponent>();
            dataCacheClearComponent.ResetChkTimeInterval(10);
            long rankShowComponentId = rankShowComponent.Id;
            self.RankList.Add(rankType, rankShowComponentId);
            return rankShowComponent;
        }

        public static RankShowComponent SetRankShow(this RankShowPlayerComponent self, RankType rankType, RankShowComponent rankShowComponent)
        {
            rankShowComponent = (RankShowComponent)self.AddChild(rankShowComponent);

            DataCacheClearComponent dataCacheClearComponent = rankShowComponent.AddComponent<DataCacheClearComponent>();
            dataCacheClearComponent.ResetChkTimeInterval(10);
            long rankShowComponentId = rankShowComponent.Id;
            self.RankList.Add(rankType, rankShowComponentId);
            return rankShowComponent;
        }

    }
}