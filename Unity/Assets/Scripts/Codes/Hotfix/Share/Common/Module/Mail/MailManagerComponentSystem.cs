using System.Collections.Generic;
using System.Linq;

namespace ET
{
    [FriendOf(typeof(MailManagerComponent))]
    public static class MailManagerComponentSystem
    {
        /// <summary>
        /// 遍历子实体MailToPlayersComponent及得到MailInfoComponent字节数组
        /// </summary>
        /// <param name="self"></param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public static List<byte[]> GetPlayerMailFromCenter(this MailManagerComponent self, long playerId)
        {
            ListComponent<byte[]> componentBytes = ListComponent<byte[]>.Create();
            foreach (MailToPlayersComponent mailToPlayersComponent in self.Children.Values)
            {
                //是否需要写入
                bool bRet = false;

                //若是发送给全部玩家
                if (mailToPlayersComponent.mailToPlayerType == MailToPlayerType.AllPlayer)
                {
                    if (mailToPlayersComponent.deliveredPlayerList.Contains(playerId) == false)
                    {
                        MailInfoComponent mailInfoComponent = mailToPlayersComponent.GetMailInfo();
                        if (mailInfoComponent.limitTime < TimeHelper.ServerNow())
                        {
                            continue;
                        }

                        string mailTitleOrg = mailInfoComponent.mailTitle;
                        string mailContentOrg = mailInfoComponent.mailContent;
                        if (mailToPlayersComponent.playerParam != null &&
                            mailToPlayersComponent.playerParam.ContainsKey(playerId))
                        {
                            mailInfoComponent.mailTitle = mailInfoComponent.mailTitle.Replace("{Rank}", mailToPlayersComponent.playerParam[playerId]);
                            mailInfoComponent.mailContent = mailInfoComponent.mailContent.Replace("{Rank}", mailToPlayersComponent.playerParam[playerId]);
                        }
                        componentBytes.Add(mailInfoComponent.ToBson());
                        mailInfoComponent.mailTitle = mailTitleOrg;
                        mailInfoComponent.mailContent = mailContentOrg;

                        bRet = true;
                        //加入已经发送的list
                        mailToPlayersComponent.deliveredPlayerList.Add(playerId);
                    }
                }
                //发送给部分玩家
                else if (mailToPlayersComponent.mailToPlayerType == MailToPlayerType.PlayerList)
                {
                    if (mailToPlayersComponent.waitSendPlayerList.Contains(playerId) && mailToPlayersComponent.deliveredPlayerList.Contains(playerId) == false)
                    {
                        MailInfoComponent mailInfoComponent = mailToPlayersComponent.GetMailInfo();
                        if (mailInfoComponent.limitTime < TimeHelper.ServerNow())
                        {
                            continue;
                        }
                        string mailTitleOrg = mailInfoComponent.mailTitle;
                        string mailContentOrg = mailInfoComponent.mailContent;
                        if (mailToPlayersComponent.playerParam != null &&
                            mailToPlayersComponent.playerParam.ContainsKey(playerId))
                        {
                            mailInfoComponent.mailTitle = mailInfoComponent.mailTitle.Replace("{Rank}", mailToPlayersComponent.playerParam[playerId]);
                            mailInfoComponent.mailContent = mailInfoComponent.mailContent.Replace("{Rank}", mailToPlayersComponent.playerParam[playerId]);
                        }
                        componentBytes.Add(mailInfoComponent.ToBson());

                        mailInfoComponent.mailTitle = mailTitleOrg;
                        mailInfoComponent.mailContent = mailContentOrg;

                        bRet = true;
                        //Playerid加入已经发送的list
                        mailToPlayersComponent.deliveredPlayerList.Add(playerId);
                    }
                }

                if (bRet)
                {
                    mailToPlayersComponent.SetDataCacheAutoWrite();
                }

            }

            return componentBytes;
        }

        public static bool ChkIsNewPlayerMailFromCenter(this MailManagerComponent self, long playerId)
        {
            foreach (MailToPlayersComponent mailToPlayersComponent in self.Children.Values)
            {
                //若是发送给全部玩家
                if (mailToPlayersComponent.mailToPlayerType == MailToPlayerType.AllPlayer)
                {
                    if (mailToPlayersComponent.deliveredPlayerList.Contains(playerId) == false)
                    {
                        MailInfoComponent mailInfoComponent = mailToPlayersComponent.GetMailInfo();
                        if (mailInfoComponent.limitTime < TimeHelper.ServerNow())
                        {
                            continue;
                        }

                        return true;
                    }
                }
                //发送给部分玩家
                else if (mailToPlayersComponent.mailToPlayerType == MailToPlayerType.PlayerList)
                {
                    if (mailToPlayersComponent.waitSendPlayerList.Contains(playerId) && mailToPlayersComponent.deliveredPlayerList.Contains(playerId) == false)
                    {
                        MailInfoComponent mailInfoComponent = mailToPlayersComponent.GetMailInfo();
                        if (mailInfoComponent.limitTime < TimeHelper.ServerNow())
                        {
                            continue;
                        }
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 写入mailToPlayersComponent组件到MailManagerComponent
        /// </summary>
        /// <param name="self"></param>
        /// <param name="bytes">mailToPlayersComponent字节数组</param>
        public static void InsertMailToCenter(this MailManagerComponent self, byte[] bytes)
        {
            MailToPlayersComponent mailToPlayersComponent = MongoHelper.Deserialize<MailToPlayersComponent>(bytes);
            self.AddChild(mailToPlayersComponent);
            mailToPlayersComponent.SetDataCacheAutoWrite();
        }

    }
}