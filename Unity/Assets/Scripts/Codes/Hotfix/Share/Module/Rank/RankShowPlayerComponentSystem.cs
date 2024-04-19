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

        public static void RemoveRankShow(this RankShowPlayerComponent self, RankType rankType)
        {
            if (self.RankList.TryGetValue(rankType, out long rankShowComponentId))
            {
                self.RankList.Remove(rankType);
                self.RemoveChild(rankShowComponentId);
            }
            else
            {
            }
        }

        public static RankShowComponent SetRankShow(this RankShowPlayerComponent self, long playerId, RankType rankType, int myRank, RankItemComponent myRankItemComponent, SortedDictionary<int, RankItemComponent> rankIndex2PlayerId)
        {
            RankShowComponent rankShowComponent = self.GetRankShow(rankType);
            if (rankShowComponent != null)
            {
                rankShowComponent.Dispose();
                self.RankList.Remove(rankType);
            }
            rankShowComponent = self.AddChild<RankShowComponent>();
            rankShowComponent.Init(rankType);

            rankShowComponent.SetRankShow(playerId, myRank, myRankItemComponent, rankIndex2PlayerId);
            rankShowComponent.SetDataCacheAutoClear(10);
            long rankShowComponentId = rankShowComponent.Id;
            self.RankList.Add(rankType, rankShowComponentId);
            return rankShowComponent;
        }

        public static RankShowComponent SetRankShow(this RankShowPlayerComponent self, long playerId, RankType rankType, RankShowComponent rankShowComponent)
        {
            rankShowComponent = (RankShowComponent)self.AddChild(rankShowComponent);

            rankShowComponent.SetDataCacheAutoClear(10);

            long rankShowComponentId = rankShowComponent.Id;
            self.RankList.Add(rankType, rankShowComponentId);
            return rankShowComponent;
        }

    }
}