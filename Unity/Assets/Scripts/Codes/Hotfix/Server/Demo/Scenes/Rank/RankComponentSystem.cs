using System;
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

                self.SkipList.Insert(rankItemComponent.score, rankItemComponent);
                self.playerId2Score.Add(playerId, rankItemComponent.score);
            }
        }

        public static async ETTask ResetRankItem<T>(this RankComponent self, long playerId, long scoreNew, int killNum)where T :RankItemComponent, new()
        {
            RankItemComponent rankItemComponent = self.GetChild<RankItemComponent>(playerId);
            if (rankItemComponent == null)
            {
                rankItemComponent = await self.InitByDBOne<T>(playerId) as RankItemComponent;
                rankItemComponent.playerId = playerId;
            }

            if (self.playerId2Score.TryGetValue(playerId, out long score))
            {
                self.SkipList.DeleteNode(score, rankItemComponent);
                self.rankTotalNum--;
            }

            rankItemComponent.score = scoreNew;
            if (rankItemComponent is RankEndlessChallengeItemComponent)
            {
                RankEndlessChallengeItemComponent rankEndlessChallengeItemComponent = (RankEndlessChallengeItemComponent)rankItemComponent;
                rankEndlessChallengeItemComponent.killNum = killNum;
            }
            rankItemComponent.recordTime = TimeHelper.ServerNow();
            rankItemComponent.SetDataCacheAutoWrite();

            self.playerId2Score[playerId] = scoreNew;
            self.SkipList.Insert(scoreNew, rankItemComponent);
            self.rankTotalNum++;

            if (self.SkipList.GetRank(scoreNew, rankItemComponent) < self.topRankPlayerCount)
            {
                self.topRankPlayerList.Add(playerId);
                self.SetDataCacheAutoWrite();
            }
        }

        public static async ETTask RecordWhenSeasonFinished(this RankComponent self, Type rankItemType, int seasonId)
        {
            self.playerId2Score.Clear();
            ulong totalCount = self.topRankPlayerCount > self.SkipList.Length? self.SkipList.Length : self.topRankPlayerCount;
            List<SkipListNode> topList = self.SkipList.GetNodeListByRank(0, (uint)totalCount);
            self.recordWhenFinished = new();
            for (int i = 0; i < topList.Count; i++)
            {
                SkipListNode skipListNode = topList[i];
                RankItemComponent rankItemComponentTmp = skipListNode.obj as RankItemComponent;
                long playerId = rankItemComponentTmp.playerId;
                RankItemComponent rankItemComponent = self.GetChild<RankItemComponent>(playerId);
                if (rankItemComponent != null)
                {
                    self.recordWhenFinished.Add(rankItemComponent);
                }
            }
            self.topRankPlayerList.Clear();
            self.SkipList = null;

            self.SetDataCacheAutoWrite(true);
            bool bFinished = await self.ChkDataCacheAutoWriteFinished();
            if (bFinished == false)
            {
                return;
            }

            await self.RenameCollection(rankItemType, seasonId);
            self.Dispose();
        }

        public static async ETTask<T> InitByDBOne<T>(this RankComponent self, long playerId) where T :Entity, IAwake, new()
        {
            return await ET.Server.DBHelper.LoadDBWithParent2Child<T>(self, playerId, true);
        }

        public static async ETTask<long> GetDBCount<T>(this RankComponent self) where T :Entity, IAwake, new()
        {
            return await ET.Server.DBHelper.GetDBCount<T>(self.DomainScene());
        }

        public static async ETTask RenameCollection(this RankComponent self, Type rankItemType, int seasonId)
        {
            await ET.Server.DBHelper.RenameCollection(self.DomainScene(), self.GetType(), seasonId);
            await ET.Server.DBHelper.RenameCollection(self.DomainScene(), rankItemType, seasonId);
        }
    }
}