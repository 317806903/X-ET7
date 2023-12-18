using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof(RankShowComponent))]
    public static class RankShowComponentSystem
    {
        [ObjectSystem]
        public class RankShowComponentAwakeSystem : AwakeSystem<RankShowComponent>
        {
            protected override void Awake(RankShowComponent self)
            {
            }
        }

        public static void SetRankShow(this RankShowComponent self, long playerId, int myRank, RankItemComponent myRankItemComponent, SortedDictionary<int, RankItemComponent> rankIndex2PlayerId)
        {
            RankShowItemComponent myRankShowItemComponent = self.AddChildWithId<RankShowItemComponent>(playerId);
            myRankShowItemComponent.rank = myRank;
            if (myRankItemComponent != null)
            {
                myRankShowItemComponent.playerId = playerId;
                myRankShowItemComponent.score = myRankItemComponent.score;
                myRankShowItemComponent.recordTime = myRankItemComponent.recordTime;
            }
            else
            {
                myRankShowItemComponent.playerId = playerId;
                myRankShowItemComponent.score = -1;
            }

            self.myRankShowItemComponentId = myRankShowItemComponent.Id;

            self.rankList.Clear();
            foreach (var item in rankIndex2PlayerId)
            {
                int rank = item.Key;
                RankItemComponent rankItemComponent = item.Value;
                long rankPlayerId = rankItemComponent.playerId;
                if (rankPlayerId == playerId)
                {
                    self.rankList.Add(myRankShowItemComponent.Id);
                }
                else
                {
                    RankShowItemComponent rankShowItemComponent = self.GetChild<RankShowItemComponent>(rankPlayerId);
                    if (rankShowItemComponent != null)
                    {
                        continue;
                    }
                    rankShowItemComponent = self.AddChildWithId<RankShowItemComponent>(rankPlayerId);
                    rankShowItemComponent.rank = rank;
                    rankShowItemComponent.playerId = rankItemComponent.playerId;
                    rankShowItemComponent.score = rankItemComponent.score;
                    rankShowItemComponent.recordTime = rankItemComponent.recordTime;
                    self.rankList.Add(rankShowItemComponent.Id);
                }
            }
        }

        public static List<EntityRef<RankShowItemComponent>> GetRankList(this RankShowComponent self)
        {
            self.rankListTmp.Clear();
            for (int i = 0; i < self.rankList.Count; i++)
            {
                RankShowItemComponent rankShowItemComponent = self.GetChild<RankShowItemComponent>(self.rankList[i]);
                self.rankListTmp.Add(rankShowItemComponent);
            }
            return self.rankListTmp;
        }

        public static int GetMyRank(this RankShowComponent self)
        {
            RankShowItemComponent rankShowItemComponent = self.GetMyRankShowItemComponent();
            if (rankShowItemComponent != null)
            {
                return rankShowItemComponent.rank;
            }

            return -1;
        }

        public static RankShowItemComponent GetMyRankShowItemComponent(this RankShowComponent self)
        {
            RankShowItemComponent rankShowItemComponent = self.GetChild<RankShowItemComponent>(self.myRankShowItemComponentId);
            return rankShowItemComponent;
        }

    }
}