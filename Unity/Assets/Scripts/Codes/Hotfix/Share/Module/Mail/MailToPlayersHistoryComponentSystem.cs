using System.Collections.Generic;
using System.Linq;

namespace ET
{
    [FriendOf(typeof(MailToPlayersHistoryComponent))]
    public static class MailToPlayersHistoryComponentSystem
    {
        [ObjectSystem]
        public class MailToPlayersHistoryComponentAwakeSystem : AwakeSystem<MailToPlayersHistoryComponent>
        {
            protected override void Awake(MailToPlayersHistoryComponent self)
            {
                self.waitSendPlayerList = new();
                self.deliveredPlayerList = new();
            }
        }

        public static void InitMailInfo(this MailToPlayersHistoryComponent self, string mailType, string mailTitle, string mailContent, Dictionary<string, int> itemCfgList, long receiveTime, long limitTime)
        {
            MailInfoComponent mailInfoComponent = self.AddChild<MailInfoComponent>();
            mailInfoComponent.Init(mailType, mailTitle, mailContent, itemCfgList, receiveTime, limitTime);
        }

        public static void SetMailToPlayerType(this MailToPlayersHistoryComponent self, MailToPlayerType mailToPlayerType, HashSet<long> waitSendPlayerList, HashSet<long> deliveredPlayerList, long createTime)
        {
            self.createTime = createTime;
            self.historyTime = TimeHelper.ServerNow();
            self.mailToPlayerType = mailToPlayerType;

            self.waitSendPlayerList.Clear();
            foreach (long playerId in waitSendPlayerList)
            {
                self.waitSendPlayerList.Add(playerId);
            }
            self.deliveredPlayerList.Clear();
            foreach (long playerId in deliveredPlayerList)
            {
                self.deliveredPlayerList.Add(playerId);
            }
        }

    }
}