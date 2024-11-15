﻿using System;
using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_DealMyMailHandler : AMRpcHandler<C2G_DealMyMail, G2C_DealMyMail>
	{
		protected override async ETTask Run(Session session, C2G_DealMyMail request, G2C_DealMyMail response)
		{
			Scene scene = session.DomainScene();
			Player player = session.GetComponent<SessionPlayerComponent>().Player;
			long playerId = player.Id;
			DealMailType dealMailType = (DealMailType)request.DealMailType;
			long mailId = request.MailId;
			PlayerMailComponent playerMailComponent = await ET.Server.PlayerCacheHelper.GetPlayerMailByPlayerId(scene, playerId);

			await playerMailComponent.DealPlayerMail(dealMailType, mailId);
			await PlayerCacheHelper.SavePlayerModel(scene, playerId, PlayerModelType.Mails, new() { "mailStatus" }, PlayerModelChgType.PlayerMails_GetItemGifts);

			await PlayerCacheHelper.DealPlayerUIRedDotType(scene, playerId, PlayerModelType.Mails);

		}
	}
}