using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof(RankComponent))]
    public static class RankComponentSystem
    {
        public static (int, RankItemComponent) GetMyRankShow(this RankComponent self, long playerId)
        {
            RankItemComponent myRankItemComponent = self.GetChild<RankItemComponent>(playerId);

            int myRank = -1;
            if (self.playerId2Score.TryGetValue(playerId, out long score))
            {
                myRank = (int)self.SkipList.GetRank(score, myRankItemComponent);

                if ((ulong)myRank > self.topRankPlayerCount)
                {
                    myRank = -1;
                }
            }

            return (myRank, myRankItemComponent);
        }

        public static ulong GetRankByScore(this RankComponent self, long score, long playerId)
        {
            ulong rank = self.SkipList.GetRank(score, null);

            if (self.playerId2Score.TryGetValue(playerId, out long scoreRecord))
            {
                if (scoreRecord == score)
                {
                    (int myRank, _) = self.GetMyRankShow(playerId);
                    rank = (ulong)myRank;
                }
            }

            if ((ulong)rank > self.topRankPlayerCount)
            {
                rank = 99999;
            }
            return rank;
        }

        public static (ulong, int) GetRankedMoreThan(this RankComponent self, long score, long playerId)
        {
            ulong rank = self.GetRankByScore(score, playerId);
            int rankedMoreThan = 0;
            if (self.rankTotalNum == 0)
            {
                return (0, 0);
            }

            if (rank > self.rankTotalNum)
            {
                rank = self.rankTotalNum;
            }

            if (self.rankTotalNum < self.topRankPlayerCount)
            {
                rankedMoreThan = (int)((float)rank / self.rankTotalNum * 100);
            }
            else
            {
                rankedMoreThan = (int)((float)rank / self.rankTotalNum * 100);
            }

            return (rank, rankedMoreThan);
        }

        public static SortedDictionary<int, RankItemComponent> GetRankShow(this RankComponent self, long playerId)
        {

            (int myRank, _) = self.GetMyRankShow(playerId);
            SortedDictionary<int, RankItemComponent> rankIndex2PlayerId = new();

            int showTotalCount = 30;
            int showMyCount = 2;
            List<SkipListNode> topList = self.SkipList.GetNodeListByRank(0, (uint)showTotalCount);
            for (int i = 0; i < topList.Count; i++)
            {
                RankItemComponent tmp = (RankItemComponent)topList[i].obj;
                RankItemComponent rankItemComponent = self.GetChild<RankItemComponent>(tmp.playerId);
                rankIndex2PlayerId[i+1] = rankItemComponent;
            }

            if (myRank == -1 || myRank <= showTotalCount - showMyCount)
            {
                return rankIndex2PlayerId;
            }

            int playerIndexBegin = math.max(showTotalCount, myRank - showMyCount);
            int playerIndexEnd = math.min((int)self.SkipList.Length, myRank + showMyCount);

            List<SkipListNode> nearMyList = self.SkipList.GetNodeListByRank((uint)playerIndexBegin, (uint)playerIndexEnd);
            for (int i = 0; i < nearMyList.Count; i++)
            {
                RankItemComponent tmp = (RankItemComponent)nearMyList[i].obj;
                RankItemComponent rankItemComponent = self.GetChild<RankItemComponent>(tmp.playerId);
                rankIndex2PlayerId[playerIndexBegin + i] = rankItemComponent;
            }
            return rankIndex2PlayerId;
        }

    }
}