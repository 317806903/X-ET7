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

        public static void SetRankShow(this RankShowComponent self, SortedDictionary<int, RankItemComponent> rankIndex2PlayerId)
        {
            self.rankIndex2PlayerId.Clear();
            foreach (var item in rankIndex2PlayerId)
            {
                int rank = item.Key;
                RankItemComponent rankItemComponent = item.Value;
                long playerId = rankItemComponent.playerId;
                self.rankIndex2PlayerId[rank] = rankItemComponent.playerId;
                RankShowItemComponent rankShowItemComponent = self.AddChildWithId<RankShowItemComponent>(playerId);
                rankShowItemComponent.playerId = rankItemComponent.playerId;
                rankShowItemComponent.score = rankItemComponent.score;
                rankShowItemComponent.recordTime = rankItemComponent.recordTime;
            }
        }

        public static SortedDictionary<int, RankShowItemComponent> GetRankList(this RankShowComponent self)
        {
            SortedDictionary<int, RankShowItemComponent> rankIndex2PlayerId = new();
            foreach (var item in self.rankIndex2PlayerId)
            {
                rankIndex2PlayerId.Add(item.Key, self.GetChild<RankShowItemComponent>(item.Value));
            }

            return rankIndex2PlayerId;
        }

    }
}