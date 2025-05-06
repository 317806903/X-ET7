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

			Player player = PlayerStatusHelper.GetMyPlayer(clientScene);
			if (player == null)
			{
				return;
			}
			long myPlayerId = ET.Client.PlayerStatusHelper.GetMyPlayerId(clientScene);

			PlayerModelType playerModelType = (PlayerModelType)message.PlayerModelType;

			bool isNeedClearPlayerModel = false;
			switch (playerModelType)
			{
				case PlayerModelType.BaseInfo:
					isNeedClearPlayerModel = true;
					break;
				case PlayerModelType.BackPack:
					isNeedClearPlayerModel = true;
					ET.Client.PlayerCacheHelper.ClearPlayerModel(clientScene, myPlayerId, PlayerModelType.BattleCard);
					ET.Client.PlayerCacheHelper.ClearPlayerModel(clientScene, myPlayerId, PlayerModelType.BattleSkill);
					break;
				case PlayerModelType.BattleCard:
					isNeedClearPlayerModel = true;
					break;
				case PlayerModelType.BattleSkill:
					isNeedClearPlayerModel = true;
					break;
				case PlayerModelType.FunctionMenu:
					isNeedClearPlayerModel = true;
					break;
				case PlayerModelType.OtherInfo:
					isNeedClearPlayerModel = true;
					break;
				case PlayerModelType.Mails:
					isNeedClearPlayerModel = true;
					break;
				case PlayerModelType.SeasonInfo:
					isNeedClearPlayerModel = true;
					break;
				case PlayerModelType.TokenArcadeCoinAdd:
				case PlayerModelType.TokenArcadeCoinReduce:
				case PlayerModelType.TokenDiamondAdd:
				case PlayerModelType.TokenDiamondReduce:
				{
					ET.Client.PlayerCacheHelper.ClearPlayerModel(clientScene, myPlayerId, PlayerModelType.BackPack);
					break;
				}
				case PlayerModelType.RankPVE:
					ET.Client.RankHelper.ClearRankShow(clientScene, RankType.PVE);
					break;
				case PlayerModelType.RankEndlessChallenge:
					ET.Client.RankHelper.ClearRankShow(clientScene, RankType.EndlessChallenge);
					break;
				default:
					break;
			}

			if(isNeedClearPlayerModel)
			{
				ET.Client.PlayerCacheHelper.ClearPlayerModel(clientScene, myPlayerId, playerModelType);
			}

			EventSystem.Instance.Publish(clientScene, new ClientEventType.NoticePlayerCacheChg()
			{
				playerModelType = playerModelType,
			});

			await ETTask.CompletedTask;
		}
	}
}
