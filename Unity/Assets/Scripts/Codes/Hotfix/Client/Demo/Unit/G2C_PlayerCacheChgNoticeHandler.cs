using System;
using System.Collections.Generic;
using ET.Ability;

namespace ET.Client
{
	[MessageHandler(SceneType.Client)]
	public class G2C_PlayerCacheChgNoticeHandler : AMHandler<G2C_PlayerCacheChgNotice>
	{
		protected override async ETTask Run(Session session, G2C_PlayerCacheChgNotice message)
		{
			Scene clientScene = session.DomainScene();
			PlayerModelType playerModelType = (PlayerModelType)message.PlayerModelType;

			switch (playerModelType)
			{
				case PlayerModelType.BaseInfo:
				case PlayerModelType.BackPack:
				case PlayerModelType.BattleCard:
				case PlayerModelType.FunctionMenu:
					Player player = PlayerStatusHelper.GetMyPlayer(clientScene);
					if (player == null)
					{
						return;
					}
					long myPlayerId = ET.Client.PlayerStatusHelper.GetMyPlayerId(clientScene);
					ET.Client.PlayerCacheHelper.ClearPlayerModel(clientScene, myPlayerId, playerModelType);
					break;
				case PlayerModelType.RankPVE:
					ET.Client.RankHelper.ClearRankShow(clientScene, RankType.PVE);
					break;
				case PlayerModelType.RankEndlessChallenge:
					ET.Client.RankHelper.ClearRankShow(clientScene, RankType.EndlessChallenge);
					break;
				default:
					break;
			}

			EventSystem.Instance.Publish(clientScene, new EventType.NoticePlayerCacheChg()
			{
				playerModelType = playerModelType,
			});

			await ETTask.CompletedTask;
		}
	}
}
