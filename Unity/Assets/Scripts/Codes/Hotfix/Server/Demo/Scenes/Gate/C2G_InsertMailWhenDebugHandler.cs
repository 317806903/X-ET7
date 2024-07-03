using System;
using System.Collections.Generic;
using System.Xml.Schema;
using ET.AbilityConfig;

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

			MailType mailType = MailType.None;
			string mailTitle = $"test11 mailTitle_{TimeHelper.ServerNow()}";
			string mailContent = $"test11 mailContent_{TimeHelper.ServerNow()}";
			//WJTODO
			Dictionary<string, int> itemCfgList;
            Random rand = new Random();
            int randomValue = rand.Next(0, 2); // 0或1
            if (randomValue == 0)
            {
                itemCfgList = null; // 50% 概率为空
				//int randomValue2 = rand.Next(3, 10);
				//itemCfgList = new Dictionary<string, int>
				//	{
				//	    //{"AvatarFrame_01", randomValue2},
				//	    //{"AvatarFrame_02", randomValue2-1},
				//	    //{"AvatarFrame_03", randomValue2+1}
				//	};
            }
            else
            {
                int randomValue2 = rand.Next(3, 10);
                itemCfgList = new Dictionary<string, int>
					{
					    //{"AvatarFrame_01", randomValue2},
					    {"AvatarFrame_02", randomValue2-1},
					    {"AvatarFrame_03", randomValue2+1}
					};
            }

            long receiveTime = TimeHelper.ServerNow();
			long limitTime = TimeHelper.ServerNow() + 10000 * 1000;
			MailToPlayerType mailToPlayerType = MailToPlayerType.AllPlayer;
			List<long> waitSendPlayerList = null;

			await ET.Server.MailHelper.InsertMailToCenter(scene, playerId, mailType, mailTitle, mailContent, itemCfgList, receiveTime, limitTime, mailToPlayerType, waitSendPlayerList);

            await ETTask.CompletedTask;
		}
	}
}