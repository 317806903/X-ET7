using System;
using System.Collections.Generic;
using System.Xml.Schema;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_AddDiamondWhenDebugHandler : AMHandler<C2G_AddDiamondWhenDebug>
	{
		protected override async ETTask Run(Session session, C2G_AddDiamondWhenDebug message)
		{
			Player player = session.GetComponent<SessionPlayerComponent>().Player;
			long playerId = player.Id;
			Scene scene = session.DomainScene();

			await ET.Server.PlayerCacheHelper.AddTokenDiamond(scene, playerId, 50);

            await ETTask.CompletedTask;
		}
	}
}