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

        public static void Init(this RankShowComponent self, RankType rankType)
        {
            self.rankType = rankType;
        }

        public static RankShowItemComponent AddRankShowItemComponent(this RankShowComponent self, long playerId)
        {
            RankShowItemComponent rankShowItemComponent = null;
            switch (self.rankType)
            {
                case RankType.PVE:
                {
                    rankShowItemComponent = self.AddChildWithId<RankShowItemComponent>(playerId);
                    break;
                }
                case RankType.EndlessChallenge:
                {
                    rankShowItemComponent = self.AddChildWithId<RankEndlessChallengeShowItemComponent>(playerId);
                    break;
                }
                default:
                    break;
            }

            return rankShowItemComponent;
        }

        public static RankShowItemComponent GetRankShowItemComponent(this RankShowComponent self, long playerId)
        {
            RankShowItemComponent rankShowItemComponent = null;
            switch (self.rankType)
            {
                case RankType.PVE:
                {
                    rankShowItemComponent = self.GetChild<RankShowItemComponent>(playerId);
                    break;
                }
                case RankType.EndlessChallenge:
                {
                    rankShowItemComponent = self.GetChild<RankEndlessChallengeShowItemComponent>(playerId);
                    break;
                }
                default:
                    break;
            }

            return rankShowItemComponent;
        }

        public static void SetRankShow(this RankShowComponent self, long playerId, int myRank, RankItemComponent myRankItemComponent, SortedDictionary<int, RankItemComponent> rankIndex2PlayerId)
        {
            RankShowItemComponent myRankShowItemComponent = self.AddRankShowItemComponent(playerId);
            myRankShowItemComponent.rank = myRank;
            if (myRankItemComponent != null)
            {
                myRankShowItemComponent.playerId = playerId;
                myRankShowItemComponent.score = myRankItemComponent.score;
                myRankShowItemComponent.recordTime = myRankItemComponent.recordTime;
                if (myRankItemComponent is RankEndlessChallengeItemComponent rankEndlessChallengeItemComponent && myRankShowItemComponent is RankEndlessChallengeShowItemComponent rankEndlessChallengeShowItemComponent)
                {
                    rankEndlessChallengeShowItemComponent.killNum = rankEndlessChallengeItemComponent.killNum;
                }
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
                    RankShowItemComponent rankShowItemComponent = self.GetRankShowItemComponent(rankPlayerId);
                    if (rankShowItemComponent != null)
                    {
                        continue;
                    }
                    rankShowItemComponent = self.AddRankShowItemComponent(rankPlayerId);
                    rankShowItemComponent.rank = rank;
                    rankShowItemComponent.playerId = rankItemComponent.playerId;
                    rankShowItemComponent.score = rankItemComponent.score;
                    rankShowItemComponent.recordTime = rankItemComponent.recordTime;
                    if (rankItemComponent is RankEndlessChallengeItemComponent rankEndlessChallengeItemComponent && rankShowItemComponent is RankEndlessChallengeShowItemComponent rankEndlessChallengeShowItemComponent)
                    {
                        rankEndlessChallengeShowItemComponent.killNum = rankEndlessChallengeItemComponent.killNum;
                    }
                    self.rankList.Add(rankShowItemComponent.Id);
                }
            }
        }

        public static List<EntityRef<RankShowItemComponent>> GetRankList(this RankShowComponent self)
        {
            self.rankListTmp.Clear();
            for (int i = 0; i < self.rankList.Count; i++)
            {
                RankShowItemComponent rankShowItemComponent = self.GetRankShowItemComponent(self.rankList[i]);
                self.rankListTmp.Add(rankShowItemComponent);
            }
            return self.rankListTmp;
        }

        public static (int myRank, long score) GetMyRank(this RankShowComponent self)
        {
            RankShowItemComponent rankShowItemComponent = self.GetMyRankShowItemComponent();
            if (rankShowItemComponent != null)
            {
                return (rankShowItemComponent.rank, rankShowItemComponent.score);
            }

            return (-1, 0);
        }

        public static RankShowItemComponent GetMyRankShowItemComponent(this RankShowComponent self)
        {
            RankShowItemComponent rankShowItemComponent = self.GetRankShowItemComponent(self.myRankShowItemComponentId);
            return rankShowItemComponent;
        }

    }
}