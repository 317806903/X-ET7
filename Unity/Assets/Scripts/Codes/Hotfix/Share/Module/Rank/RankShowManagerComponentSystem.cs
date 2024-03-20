using System.Collections.Generic;
using System.Linq;

namespace ET
{
    [FriendOf(typeof(RankShowManagerComponent))]
    public static class RankShowManagerComponentSystem
    {
        [ObjectSystem]
        public class RankShowManagerComponentAwakeSystem : AwakeSystem<RankShowManagerComponent>
        {
            protected override void Awake(RankShowManagerComponent self)
            {
            }
        }

        public static RankShowComponent GetRankShow(this RankShowManagerComponent self, long playerId, RankType rankType)
        {
            RankShowPlayerComponent rankShowPlayerComponent = self.GetChild<RankShowPlayerComponent>(playerId);
            if (rankShowPlayerComponent != null)
            {
                return rankShowPlayerComponent.GetRankShow(rankType);
            }
            else
            {
                return null;
            }
        }

        public static RankShowComponent SetRankShow(this RankShowManagerComponent self, long playerId, RankType rankType, int myRank, RankItemComponent myRankItemComponent, SortedDictionary<int, RankItemComponent> rankIndex2PlayerId)
        {
            RankShowPlayerComponent rankShowPlayerComponent = self.GetChild<RankShowPlayerComponent>(playerId);
            if (rankShowPlayerComponent == null)
            {
                rankShowPlayerComponent = self.AddChildWithId<RankShowPlayerComponent>(playerId);
            }
            RankShowComponent rankShowComponent = rankShowPlayerComponent.SetRankShow(playerId, rankType, myRank, myRankItemComponent, rankIndex2PlayerId);
            return rankShowComponent;
        }

        public static RankShowComponent SetRankShow(this RankShowManagerComponent self, long playerId, RankType rankType, RankShowComponent rankShowComponent)
        {
            RankShowPlayerComponent rankShowPlayerComponent = self.GetChild<RankShowPlayerComponent>(playerId);
            if (rankShowPlayerComponent == null)
            {
                rankShowPlayerComponent = self.AddChildWithId<RankShowPlayerComponent>(playerId);
            }
            rankShowComponent = rankShowPlayerComponent.SetRankShow(playerId, rankType, rankShowComponent);
            return rankShowComponent;
        }

    }
}