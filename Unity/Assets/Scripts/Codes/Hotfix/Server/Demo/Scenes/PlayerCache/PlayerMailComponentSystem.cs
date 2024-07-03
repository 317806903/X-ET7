using System.Collections.Generic;
using System.Linq;
using ET.Server;

namespace ET.Server
{
    [FriendOf(typeof(PlayerMailComponent))]
    public static class PlayerMailComponentSystem
    {
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static async ETTask Refresh(this PlayerMailComponent self)
        {
            (bool bRet, List<byte[]> componentBytes) = await Server.MailHelper.GetPlayerMailFromCenter(self.DomainScene(), self.GetPlayerId());
            if (bRet)
            {
                foreach (byte[] bytes in componentBytes)
                {
                    MailInfoComponent mailInfoComponent = MongoHelper.Deserialize<MailInfoComponent>(bytes);
                    self.AddChild(mailInfoComponent);
                    self.mailStatus.Add(mailInfoComponent.Id, MailStatus.UnRead);
                }
                self.SetDataCacheAutoWrite();
            }
        }

        /// <summary>
        /// 处理玩家点击邮件
        /// </summary>
        /// <param name="self"></param>
        /// <param name="dealMailType"></param>
        /// <param name="mailId"></param>
        /// <returns></returns>
        public static async ETTask<bool> DealPlayerMail(this PlayerMailComponent self, DealMailType dealMailType, long mailId)
        {
            //若邮件ID为-1
            if (mailId == -1)
            {
                foreach (KeyValuePair<long, Entity> child in self.Children)
                {
                    MailInfoComponent mailInfoComponent = child.Value as MailInfoComponent;
                    bool bRet = await self.DealPlayerMail(dealMailType, mailInfoComponent);
                    if (bRet == false)
                    {
                        return false;
                    }
                }
            }
            //若邮件id为正常时
            else
            {
                MailInfoComponent mailInfoComponent = self.GetChild<MailInfoComponent>(mailId);
                if (mailInfoComponent == null)
                {
                    return false;
                }
                await self.DealPlayerMail(dealMailType, mailInfoComponent);
            }

            return true;
        }

        /// <summary>
        /// 真正的处理邮件的逻辑
        /// </summary>
        /// <param name="self"></param>
        /// <param name="dealMailType"></param>
        /// <param name="mailInfoComponent"></param>
        /// <returns></returns>
        public static async ETTask<bool> DealPlayerMail(this PlayerMailComponent self, DealMailType dealMailType, MailInfoComponent mailInfoComponent)
        {
            //是否有奖
            bool existItem = true;
            if (mailInfoComponent.itemCfgList == null || mailInfoComponent.itemCfgList.Count == 0)
            {
                existItem = false;
            }

            //若邮件是 有奖已读 或 没奖已读 则返回true
            MailStatus mailStatus = self.mailStatus[mailInfoComponent.Id];
            if (mailStatus == MailStatus.ReadAndGetItem || mailStatus == MailStatus.ReadAndNoItem)
            {
                return true;
            }

            //有奖
            if (existItem)
            {
                //有奖但只读过
                if (dealMailType == DealMailType.ReadOnly)
                {
                    self.mailStatus[mailInfoComponent.Id] = MailStatus.ReadAndNotGetItem;
                }
                //读了并且领奖
                else if (dealMailType == DealMailType.ReadAndGetItem)
                {
                    self.mailStatus[mailInfoComponent.Id] = MailStatus.ReadAndGetItem;
                    await PlayerCacheHelper.AddItems(self.DomainScene(), self.GetPlayerId(), mailInfoComponent.itemCfgList);
                }
            }
            //无奖
            else
            {
                self.mailStatus[mailInfoComponent.Id] = MailStatus.ReadAndNoItem;
            }
            return true;
        }

    }
}