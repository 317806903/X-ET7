using System.Collections.Generic;
using System.Linq;

namespace ET
{
    [FriendOf(typeof(MailToPlayersComponent))]
    public static class MailToPlayersComponentSystem
    {
        [ObjectSystem]
        public class MailToPlayersComponentAwakeSystem : AwakeSystem<MailToPlayersComponent>
        {
            protected override void Awake(MailToPlayersComponent self)
            {
                self.waitSendPlayerList = new();
                self.deliveredPlayerList = new();
            }
        }

        /// <summary>
        /// 获取成员变量MailInfoComponent
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static MailInfoComponent GetMailInfo(this MailToPlayersComponent self)
        {
            foreach (var item in self.Children)
            {
                return item.Value as MailInfoComponent;
            }
            return null;
        }

        /// <summary>
        /// 初始化MailInfoComponent添加为子物体并绑定为成员变量
        /// </summary>
        /// <param name="self"></param>
        /// <param name="mailType"></param>
        /// <param name="mailTitle"></param>
        /// <param name="mailContent"></param>
        /// <param name="itemCfgList"></param>
        /// <param name="receiveTime"></param>
        /// <param name="limitTime"></param>
        public static void InitMailInfo(this MailToPlayersComponent self, string mailType, string mailTitle, string mailContent, Dictionary<string, int> itemCfgList, long receiveTime, long limitTime)
        {
            MailInfoComponent mailInfoComponent = self.AddChild<MailInfoComponent>();
            mailInfoComponent.Init(mailType, mailTitle, mailContent, itemCfgList, receiveTime, limitTime);
        }

        /// <summary>
        /// 设置玩家成员变量mailToPlayerType，waitSendPlayerList
        /// </summary>
        /// <param name="self"></param>
        /// <param name="mailToPlayerType"></param>
        /// <param name="waitSendPlayerList"></param>
        public static void SetMailToPlayerType(this MailToPlayersComponent self, MailToPlayerType mailToPlayerType, List<long> waitSendPlayerList, Dictionary<long, string> playerParam)
        {
            self.createTime = TimeHelper.ServerNow();
            self.mailToPlayerType = mailToPlayerType;
            if (self.mailToPlayerType == MailToPlayerType.PlayerList)
            {
                self.waitSendPlayerList.Clear();
                foreach (long playerId in waitSendPlayerList)
                {
                    self.waitSendPlayerList.Add(playerId);
                }
            }
            self.playerParam = playerParam;
        }

    }
}