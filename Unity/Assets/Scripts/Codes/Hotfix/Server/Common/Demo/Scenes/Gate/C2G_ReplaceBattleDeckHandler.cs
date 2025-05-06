using System;


namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_ReplaceBattleDeckHandler : AMRpcHandler<C2G_ReplaceBattleDeck, G2C_ReplaceBattleDeck>
	{
		protected override async ETTask Run(Session session, C2G_ReplaceBattleDeck request, G2C_ReplaceBattleDeck response)
		{
			Scene scene = session.DomainScene();
			Player player = session.GetComponent<SessionPlayerComponent>().Player;
			long playerId = player.Id;
			int replaceIndex = request.ReplaceIndex;
			string itemCfgId = request.ItemCfgId;

			if (replaceIndex == -1 && string.IsNullOrEmpty(itemCfgId))
			{
				await ET.Server.PlayerCacheHelper.SetDefaultBattleDeckItemList(scene, playerId, true);
				return;
			}

			if (ET.ItemHelper.ChkIsCallMonster(itemCfgId))
			{
				PlayerBattleCardComponent playerBattleCardComponent = await PlayerCacheHelper.GetPlayerBattleCardByPlayerId(scene, playerId, true);
				playerBattleCardComponent.ReplaceBattleMonsterCallItemCfgId(replaceIndex, itemCfgId);
				await PlayerCacheHelper.SavePlayerModel(scene, playerId, PlayerModelType.BattleCard, null, PlayerModelChgType.PlayerBattleCard_ReplaceBattleDeckMonsterCall);
			}
			else if (ET.ItemHelper.ChkIsBattleDeckTower(itemCfgId))
			{
				PlayerBattleCardComponent playerBattleCardComponent = await PlayerCacheHelper.GetPlayerBattleCardByPlayerId(scene, playerId, true);
				playerBattleCardComponent.ReplaceBattleCardItemCfgId(replaceIndex, itemCfgId);
				await PlayerCacheHelper.SavePlayerModel(scene, playerId, PlayerModelType.BattleCard, null, PlayerModelChgType.PlayerBattleCard_ReplaceBattleDeckTower);
			}
			else if (ET.ItemHelper.ChkIsSkill(itemCfgId))
			{
				PlayerBattleSkillComponent playerBattleSkillComponent = await PlayerCacheHelper.GetPlayerBattleSkillByPlayerId(scene, playerId, true);
				playerBattleSkillComponent.ReplaceBattleSkillItemCfgId(replaceIndex, itemCfgId);
				await PlayerCacheHelper.SavePlayerModel(scene, playerId, PlayerModelType.BattleSkill, null, PlayerModelChgType.PlayerBattleSkill_ReplaceBattleDeck);

			}
			else
			{
				response.Error = ErrorCode.ERR_LogicError;
				response.Message = $"itemCfgId[{itemCfgId}] type not support";
			}

			await ETTask.CompletedTask;
		}
	}
}