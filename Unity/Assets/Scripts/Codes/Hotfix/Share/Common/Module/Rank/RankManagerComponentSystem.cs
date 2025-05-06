using System.Collections.Generic;
using System.Linq;

namespace ET
{
    [FriendOf(typeof(RankManagerComponent))]
    public static class RankManagerComponentSystem
    {
        [ObjectSystem]
        public class RankManagerComponentAwakeSystem : AwakeSystem<RankManagerComponent>
        {
            protected override void Awake(RankManagerComponent self)
            {
            }
        }

        public static RankComponent GetRank(this RankManagerComponent self, RankType rankType)
        {
            switch (rankType)
            {
                case RankType.PVE:
                    RankPVEComponent rankPVEComponent = self.GetComponent<RankPVEComponent>();
                    return rankPVEComponent;
                case RankType.EndlessChallenge:
                    RankEndlessChallengeComponent rankEndlessChallengeComponent = self.GetComponent<RankEndlessChallengeComponent>();
                    return rankEndlessChallengeComponent;
                default:
                    break;
            }

            return null;
        }

        public static bool ChkRankExist(this RankManagerComponent self, RankType rankType)
        {
            if (self.GetRank(rankType) != null)
            {
                return true;
            }

            return false;
        }

        public static SortedDictionary<int, RankItemComponent> GetRankShow(this RankManagerComponent self, long playerId, RankType rankType)
        {
            RankComponent rankComponent = self.GetRank(rankType);
            return rankComponent.GetRankShow(playerId);
        }

        public static (int, RankItemComponent) GetMyRankShow(this RankManagerComponent self, long playerId, RankType rankType)
        {
            RankComponent rankComponent = self.GetRank(rankType);
            return rankComponent.GetMyRankShow(playerId);
        }

        public static (ulong, int) GetRankedMoreThan(this RankManagerComponent self, RankType rankType, long score, long playerId)
        {
            RankComponent rankComponent = self.GetRank(rankType);
            return rankComponent.GetRankedMoreThan(score, playerId);
        }

    }
}