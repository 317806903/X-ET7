using System.Collections.Generic;
using System.Linq;

namespace ET
{
    [FriendOf(typeof(MailInfoComponent))]
    public static class MailInfoComponentSystem
    {
        [ObjectSystem]
        public class MailInfoComponentAwakeSystem : AwakeSystem<MailInfoComponent>
        {
            protected override void Awake(MailInfoComponent self)
            {
            }
        }

        /// <summary>
        /// 邮件初始化
        /// </summary>
        /// <param name="self"></param>
        /// <param name="mailType"></param>
        /// <param name="mailTitle"></param>
        /// <param name="mailContent"></param>
        /// <param name="itemCfgList"></param>
        /// <param name="receiveTime"></param>
        /// <param name="limitTime"></param>
        public static void Init(this MailInfoComponent self, MailType mailType, string mailTitle, string mailContent, Dictionary<string, int> itemCfgList, long receiveTime, long limitTime)
        {
            self.mailType = mailType;
            self.mailTitle = mailTitle;
            self.mailContent = mailContent;
            self.itemCfgList = itemCfgList;
            self.receiveTime = receiveTime;
            self.limitTime = limitTime;
        }

    }
}