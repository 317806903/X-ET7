using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

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

        public static async ETTask RecordWhenSeasonFinished(this RankComponent self, Type rankItemType, int seasonIndex, int seasonCfgId)
        {
            if (self.seasonIndex > seasonIndex)
            {
                Log.Error($"ET.Server.RankComponentSystem.RecordWhenSeasonFinished self.seasonIndex[{self.seasonIndex}] > seasonIndex[{seasonIndex}]");
                return;
            }
            SeasonInfoCfg seasonInfoCfg = SeasonInfoCfgCategory.Instance.Get(seasonCfgId);
            string seasonName = seasonInfoCfg.Name;

            Dictionary<int, (List<long> playerList, string mailCfgId)> rank2Mail = new();
            foreach (var item in seasonInfoCfg.RewardMail)
            {
                rank2Mail.Add(item.Key, (new(), item.Value));
            }

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
            await self.RenameCollection(rankItemType, seasonIndex);

            Dictionary<long, string> playerParam = new();
            for (int i = 0; i < self.recordWhenFinished.Count; i++)
            {
                long playerId = self.recordWhenFinished[i].playerId;
                playerParam[playerId] = $"{i + 1}";
                foreach (var item in rank2Mail)
                {
                    if (i <= item.Key - 1)
                    {
                        item.Value.playerList.Add(playerId);
                        break;
                    }
                }
            }

            long receiveTime = TimeHelper.ServerNow();
            foreach (var item in rank2Mail)
            {
                MailToPlayerType mailToPlayerType = MailToPlayerType.PlayerList;
                List<long> waitSendPlayerList = item.Value.playerList;
                if (waitSendPlayerList.Count == 0)
                {
                    continue;
                }
                MailCfg mailCfg = MailCfgCategory.Instance.Get(item.Value.mailCfgId);
                DateTime limitTimeTmp = TimeHelper.DateTimeNow().AddDays(mailCfg.EffectiveTime);
                long limitTime = TimeHelper.ToTimeStamp(limitTimeTmp);
                Dictionary<string, int> itemCfgList = ET.DropItemRuleHelper.Drop(mailCfg.DropRuleId);

                Dictionary<long, string> playerParamTmp = new();
                foreach (long playerId in waitSendPlayerList)
                {
                    playerParamTmp[playerId] = playerParam[playerId];
                }

                // 赛季名称 {SeasonName}
                // 玩家名称	{Name}
                // 玩家排名	{Rank}
                string mailTitle = mailCfg.MailTitle;
                mailTitle = mailTitle.Replace("{SeasonName}", seasonName);
                string mailContent = mailCfg.MailContent;
                mailContent = mailContent.Replace("{SeasonName}", seasonName);
                await ET.Server.MailHelper.InsertMailToCenter(self.DomainScene(), -1, mailCfg.MailType, mailTitle, mailContent, itemCfgList, receiveTime, limitTime, mailToPlayerType, waitSendPlayerList, playerParamTmp);
            }

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

        public static async ETTask RenameCollection(this RankComponent self, Type rankItemType, int seasonCfgId)
        {
            await ET.Server.DBHelper.RenameCollection(self.DomainScene(), self.GetType(), seasonCfgId);
            await ET.Server.DBHelper.RenameCollection(self.DomainScene(), rankItemType, seasonCfgId);
        }
    }
}