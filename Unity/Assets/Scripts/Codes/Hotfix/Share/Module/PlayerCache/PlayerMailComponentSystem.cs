using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET
{
    [FriendOf(typeof(PlayerMailComponent))]
    public static class PlayerMailComponentSystem
    {
        [ObjectSystem]
        public class PlayerMailComponentAwakeSystem : AwakeSystem<PlayerMailComponent>
        {
            protected override void Awake(PlayerMailComponent self)
            {
                self.mailStatus = new();
            }
        }

        public static void Init(this PlayerMailComponent self)
        {
            self.DealOvertimeMail();
        }

        public static void DealOvertimeMail(this PlayerMailComponent self)
        {
            using ListComponent<long> removeList = ListComponent<long>.Create();
            foreach (KeyValuePair<long, Entity> child in self.Children)
            {
                MailInfoComponent mailInfoComponent = child.Value as MailInfoComponent;
                if (mailInfoComponent.limitTime < TimeHelper.ServerNow())
                {
                    removeList.Add(mailInfoComponent.Id);
                }
            }

            if (removeList.Count > 0)
            {
                foreach (long mailId in removeList)
                {
                    self.RemoveChild(mailId);
                }
                self.SetDataCacheAutoWrite();
            }
        }

        /// <summary>
        /// 获取玩家ID
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static long GetPlayerId(this PlayerMailComponent self)
        {
            return self.GetParent<PlayerDataComponent>().playerId;
        }

        /// <summary>
        /// 遍历子实体获取玩家的邮件MailInfoComponent数据及邮件状态
        /// </summary>
        /// <param name="self"></param>
        /// <param name="isSortByReceiveTime"></param>
        /// <returns></returns>
        public static List<(MailInfoComponent, MailStatus)> GetPlayerMailList(this PlayerMailComponent self, bool isSortByReceiveTime)
        {
            ListComponent<(MailInfoComponent mailInfoComponent, MailStatus mailStatus)> list = ListComponent<(MailInfoComponent, MailStatus)>.Create();
            foreach (KeyValuePair<long, Entity> child in self.Children)
            {
                MailInfoComponent mailInfoComponent = child.Value as MailInfoComponent;
                if (mailInfoComponent.limitTime < TimeHelper.ServerNow())
                {
                    continue;
                }
                list.Add((mailInfoComponent, self.mailStatus[mailInfoComponent.Id]));
            }

            //对邮件进行排序
            list.Sort(
                (x, y) =>
                {
                    //邮件状态枚举越大越排在后
                    if (x.mailStatus > y.mailStatus)
                    {
                        return 1;
                    }

                    if (isSortByReceiveTime)
                    {
                        //比较接受时间
                        if (x.mailInfoComponent.receiveTime > y.mailInfoComponent.receiveTime)
                        {
                            return 1;
                        }
                        if (x.mailInfoComponent.limitTime > y.mailInfoComponent.limitTime)
                        {
                            return 1;
                        }

                        return -1;
                    }
                    else
                    {
                        //比较邮件销毁时间
                        if (x.mailInfoComponent.limitTime > y.mailInfoComponent.limitTime)
                        {
                            return 1;
                        }

                        if (x.mailInfoComponent.receiveTime > y.mailInfoComponent.receiveTime)
                        {
                            return 1;
                        }
                        return -1;
                    }
                });
            return list;
        }

        /// <summary>
        /// 指定排序方式，遍历子实体获取玩家的邮件MailInfoComponent数据及邮件状态
        /// </summary>
        /// <param name="self"></param>
        /// <param name="mailSortRule">排序规则</param>
        /// <param name="isDescendingOrder">是否是降序</param>
        /// <returns></returns>
        public static List<(MailInfoComponent, MailStatus)> GetPlayerMailListBySort(this PlayerMailComponent self, MailSortRule mailSortRule, bool isDescendingOrder)
        {
            ListComponent<(MailInfoComponent mailInfoComponent, MailStatus mailStatus)> unreadOrNotReceivedList = ListComponent<(MailInfoComponent mailInfoComponent, MailStatus mailStatus)>.Create();
            using ListComponent<(MailInfoComponent mailInfoComponent, MailStatus mailStatus)> readList = ListComponent<(MailInfoComponent mailInfoComponent, MailStatus mailStatus)>.Create();

            foreach (KeyValuePair<long, Entity> child in self.Children)
            {
                MailInfoComponent mailInfoComponent = child.Value as MailInfoComponent;
                if (mailInfoComponent.limitTime < TimeHelper.ServerNow())
                {
                    continue;
                }
                MailStatus mailStatus = self.mailStatus[mailInfoComponent.Id];

                if (mailStatus == MailStatus.UnRead || mailStatus == MailStatus.ReadAndNotGetItem)
                {
                    unreadOrNotReceivedList.Add((mailInfoComponent, mailStatus));
                }
                else
                {
                    readList.Add((mailInfoComponent, mailStatus));
                }
            }

            Comparison<(MailInfoComponent, MailStatus)> comparison = (x, y) =>
            {
                (MailInfoComponent mailInfoX, MailStatus statusX) = x;
                (MailInfoComponent mailInfoY, MailStatus statusY) = y;

                long result = 0;

                switch (mailSortRule)
                {
                    case MailSortRule.ReceivedTime:
                        result = mailInfoX.receiveTime - mailInfoY.receiveTime;
                        break;
                    case MailSortRule.LimitTime:
                        result = mailInfoX.limitTime - mailInfoY.limitTime;
                        break;
                }

                return isDescendingOrder ? -result.CompareTo(0) : result.CompareTo(0);
            };

            unreadOrNotReceivedList.Sort(comparison);
            readList.Sort(comparison);

            unreadOrNotReceivedList.AddRange(readList);
            return unreadOrNotReceivedList;
        }
    }
}