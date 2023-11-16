using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof(RankComponent))]
    public static class RankComponentSystem
    {
        public static SortedDictionary<int, RankItemComponent> GetRankShow(this RankComponent self, long playerId)
        {
            SortedDictionary<int, RankItemComponent> rankIndex2PlayerId = new();
            int myRank = -1;
            if (self.playerId2Score.TryGetValue(playerId, out long score))
            {
                myRank = (int)self.SkipList.GetRank(score, playerId);
            }

            int showTotalCount = 30;
            int showMyCount = 2;
            List<SkipListNode> topList = self.SkipList.GetNodeListByRank(0, (uint)showTotalCount);
            for (int i = 0; i < topList.Count; i++)
            {
                RankItemComponent rankItemComponent = self.GetChild<RankItemComponent>((long)topList[i].obj);
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
                RankItemComponent rankItemComponent = self.GetChild<RankItemComponent>((long)nearMyList[i].obj);
                rankIndex2PlayerId[playerIndexBegin + i + 1] = rankItemComponent;
            }
            return rankIndex2PlayerId;
        }

    }
}