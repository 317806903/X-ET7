using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

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
        public static void Init(this MailInfoComponent self, string mailType, string mailTitle, string mailContent, Dictionary<string, int> itemCfgList, long receiveTime, long limitTime)
        {
            self.mailType = mailType;
            self.mailTitle = mailTitle;
            self.mailContent = mailContent;
            if (itemCfgList != null)
            {
                self.itemCfgList = new();
                foreach (var item in itemCfgList)
                {
                    self.itemCfgList.Add(item.Key, item.Value);
                }
            }
            self.receiveTime = receiveTime;
            self.limitTime = limitTime;
        }

        public static string GetMailTypeIcon(this MailInfoComponent self)
        {
            MailTypeCfg mailTypeCfg = MailTypeCfgCategory.Instance.Get(self.mailType);
            ResIconCfg resIconCfg = ResIconCfgCategory.Instance.Get(mailTypeCfg.MaiTypeIcon);
            return resIconCfg.ResName;
        }

        public static string GetMailTypeName(this MailInfoComponent self)
        {
            MailTypeCfg mailTypeCfg = MailTypeCfgCategory.Instance.Get(self.mailType);
            return mailTypeCfg.MaiTypeName;
        }

    }
}