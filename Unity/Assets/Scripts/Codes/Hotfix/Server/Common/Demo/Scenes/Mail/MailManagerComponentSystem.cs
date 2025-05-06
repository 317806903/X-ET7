using System.Collections.Generic;
using System.Linq;

namespace ET.Server
{
    [FriendOf(typeof(MailManagerComponent))]
    public static class MailManagerComponentSystem
    {
        [ObjectSystem]
        public class MailManagerComponentAwakeSystem : AwakeSystem<MailManagerComponent>
        {
            protected override void Awake(MailManagerComponent self)
            {
                self.InitByDB().Coroutine();
            }
        }

        [ObjectSystem]
        public class MailManagerComponentFixedUpdateSystem: FixedUpdateSystem<MailManagerComponent>
        {
            protected override void FixedUpdate(MailManagerComponent self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Mail)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static void FixedUpdate(this MailManagerComponent self, float fixedDeltaTime)
        {
            if (++self.curFrameChk >= self.waitFrameChk)
            {
                self.curFrameChk = 0;

                self.DealLimitTime();
            }
        }

        public static void DealLimitTime(this MailManagerComponent self)
        {
            using HashSetComponent<MailToPlayersComponent> tmp = HashSetComponent<MailToPlayersComponent>.Create();
            foreach (MailToPlayersComponent mailToPlayersComponent in self.Children.Values)
            {
                MailInfoComponent mailInfoComponent = mailToPlayersComponent.GetMailInfo();
                if (mailInfoComponent.limitTime < TimeHelper.ServerNow())
                {
                    tmp.Add(mailToPlayersComponent);
                    continue;
                }
                else
                {
                    if (mailToPlayersComponent.mailToPlayerType == MailToPlayerType.PlayerList)
                    {
                        bool allSend = true;
                        foreach (var playerId in mailToPlayersComponent.waitSendPlayerList)
                        {
                            if (mailToPlayersComponent.deliveredPlayerList.Contains(playerId) == false)
                            {
                                allSend = false;
                                break;
                            }
                        }
                        if (allSend)
                        {
                            tmp.Add(mailToPlayersComponent);
                            continue;
                        }
                    }
                }
            }

            MailHistoryManagerComponent mailHistoryManagerComponent = ET.Server.MailHelper.GetMailHistoryManager(self.DomainScene());
            foreach (MailToPlayersComponent mailToPlayersComponent in tmp)
            {
                MailToPlayersHistoryComponent mailToPlayersHistoryComponent = mailHistoryManagerComponent.AddChild<MailToPlayersHistoryComponent>();
                MailInfoComponent mailInfoComponent = mailToPlayersComponent.GetMailInfo();
                mailToPlayersHistoryComponent.InitMailInfo(mailInfoComponent.mailType, mailInfoComponent.mailTitle, mailInfoComponent.mailContent, mailInfoComponent.itemCfgList, mailInfoComponent.receiveTime, mailInfoComponent.limitTime);
                mailToPlayersHistoryComponent.SetMailToPlayerType(mailToPlayersComponent.mailToPlayerType, mailToPlayersComponent.waitSendPlayerList, mailToPlayersComponent.deliveredPlayerList, mailToPlayersComponent.createTime);
                mailToPlayersHistoryComponent.SetDataCacheAutoWrite();

                ET.Server.DBHelper.RemoveDB(mailToPlayersComponent).Coroutine();
                self.RemoveChild(mailToPlayersComponent.Id);
            }
        }

        /// <summary>
        /// 从数据库中得到MailToPlayersComponent列表
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static async ETTask InitByDB(this MailManagerComponent self)
        {
            List<MailToPlayersComponent> list = await ET.Server.DBHelper.LoadDBListWithParent2Child<MailToPlayersComponent>(self);
            if (list == null || list.Count == 0)
            {
            }
            else
            {
            }
        }

    }
}