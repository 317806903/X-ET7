using System;
using System.Collections.Generic;
using System.Xml.Schema;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_InsertMailWhenDebugHandler : AMHandler<C2G_InsertMailWhenDebug>
	{
		protected override async ETTask Run(Session session, C2G_InsertMailWhenDebug message)
		{
			Player player = session.GetComponent<SessionPlayerComponent>().Player;
			long playerId = player.Id;
			Scene scene = session.DomainScene();

			string mailType = "MailType_1";
			string mailTitle = $"test11 mailTitle_{TimeHelper.ServerNow()}";
			string mailContent = $"test11 mailContent_{TimeHelper.ServerNow()}";

            System.Random random = new System.Random();
            List<string> keys = new List<string>
            {
                //用于测试的邮件奖励配置Key
                 "Token_Diamond",
                 "Token_ArcadeCoin",
                 "Tow21_3",
                 "Tow4_1",
                 "Tow25_2",
                 "AvatarFrame_Season1_1","AvatarFrame_Season1_2","AvatarFrame_Season1_3","AvatarFrame_Season1_4","AvatarFrame_Season1_5",
            };
            Dictionary<string, int> itemCfgdict = new Dictionary<string, int>();
            foreach (string key in keys)
            {
                if (random.NextDouble() < 0.3) // 15%的概率添加键
                {
                    itemCfgdict[key] = random.Next(1, 5); // 生成1到5之间的随机值
                }
            }
            if(itemCfgdict.Count<= 0)
            {
                itemCfgdict = null;
            }

            long receiveTime = TimeHelper.ServerNow();
			long limitTime = TimeHelper.ServerNow() + 10000 * 1000;
			MailToPlayerType mailToPlayerType = MailToPlayerType.AllPlayer;
			List<long> waitSendPlayerList = null;

			await ET.Server.MailHelper.InsertMailToCenter(scene, playerId, mailType, mailTitle, mailContent, itemCfgdict, receiveTime, limitTime, mailToPlayerType, waitSendPlayerList, null);

            await ETTask.CompletedTask;
		}
	}
}