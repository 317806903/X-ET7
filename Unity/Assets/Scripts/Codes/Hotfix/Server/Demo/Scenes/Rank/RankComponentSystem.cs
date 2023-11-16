using System.Collections.Generic;
using System.Linq;

namespace ET.Server
{
    [FriendOf(typeof(RankComponent))]
    public static class RankComponentSystem
    {
        public static async ETTask Init<T>(this RankComponent self)where T :RankItemComponent, new()
        {
            self.topRankPlayerCount = 5000;
            if (self.topRankPlayerList == null)
            {
                self.topRankPlayerList = new();
            }
            self.SkipList = SkipList.CreateList(true);
            self.playerId2Score = new();

            foreach (long playerId in self.topRankPlayerList)
            {
                T rankItemComponent = await self.InitByDBOne<T>(playerId);

                self.SkipList.Insert(rankItemComponent.score, playerId);
                self.playerId2Score.Add(playerId, rankItemComponent.score);
            }
        }

        public static async ETTask ResetRankItem<T>(this RankComponent self, long playerId, long scoreNew)where T :RankItemComponent, new()
        {
            if (self.playerId2Score.TryGetValue(playerId, out long score))
            {
                self.SkipList.DeleteNode(score, playerId);
            }
            self.SkipList.Insert(scoreNew, playerId);
            self.playerId2Score[playerId] = scoreNew;

            RankItemComponent rankItemComponent = self.GetChild<RankItemComponent>(playerId);
            if (rankItemComponent == null)
            {
                rankItemComponent = await self.InitByDBOne<T>(playerId) as RankItemComponent;
                rankItemComponent.playerId = playerId;
            }
            rankItemComponent.score = scoreNew;
            rankItemComponent.recordTime = TimeHelper.ServerNow();
            rankItemComponent.GetComponent<DataCacheWriteComponent>().SetNeedSave();

            if (self.SkipList.GetRank(scoreNew, playerId) < self.topRankPlayerCount)
            {
                if (self.topRankPlayerList.Contains(playerId) == false)
                {
                    self.topRankPlayerList.Add(playerId);
                }
                self.GetComponent<DataCacheWriteComponent>().SetNeedSave();
            }
        }

        public static async ETTask<T> InitByDBOne<T>(this RankComponent self, long playerId) where T :Entity, IAwake, new()
        {
            return await ET.Server.DBHelper.LoadDBWithParent2Child<T>(self, playerId);
        }

        public static async ETTask SaveDB<T>(this RankComponent self) where T :Entity
        {
            await ET.Server.DBHelper.SaveDB(self);
        }
    }
}