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