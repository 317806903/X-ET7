using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
	public class ActionGame_RewardGamePlayTowerDefenseCards: IActionGameHandler
	{
		public override async ETTask Run(Scene scene, string actionId, float delayTime, ActionGameContext actionGameContext)
		{
			if (delayTime > 0)
			{
				await TimerComponent.Instance.WaitTillAsync(TimeHelper.ServerFrameTime() + (long)(1000 * delayTime));
				if (scene.IsDisposed)
				{
					return;
				}
			}

			long playerId = actionGameContext.playerId;
			if (playerId == (long)ET.PlayerId.PlayerNone)
			{
				return;
			}
			GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(scene);
			if (gamePlayComponent == null)
			{
				return;
			}
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(scene);
			if (gamePlayTowerDefenseComponent == null)
			{
				return;
			}
			ActionGameCfg_RewardGamePlayTowerDefenseCards actionGameCfgRewardGamePlayTowerDefenseCards = ActionGameCfg_RewardGamePlayTowerDefenseCardsCategory.Instance.Get(actionId);

			PlayerOwnerTowersComponent playerOwnerTowersComponent = gamePlayTowerDefenseComponent.GetComponent<PlayerOwnerTowersComponent>();
			if (playerOwnerTowersComponent == null || playerOwnerTowersComponent.playerOwnerTowerCardIds == null)
			{
				return;
			}
			if (playerOwnerTowersComponent.playerOwnerTowerCardIds.TryGetDic(playerId, out var dic) == false)
			{
				return;
			}
			ListComponent<string> list = ListComponent<string>.Create();
			if (actionGameCfgRewardGamePlayTowerDefenseCards.IsNeedBattleDeckTower)
			{
				foreach (var item in dic)
				{
					string itemCfgId = item.Key;
					if (ItemHelper.ChkIsBattleDeckTower(itemCfgId))
					{
						list.Add(itemCfgId);
					}
				}
			}
			if (actionGameCfgRewardGamePlayTowerDefenseCards.IsNeedBattleDeckSkill)
			{
				foreach (var item in dic)
				{
					string itemCfgId = item.Key;
					if (ItemHelper.ChkIsSkill(itemCfgId))
					{
						list.Add(itemCfgId);
					}
				}
			}
			if (actionGameCfgRewardGamePlayTowerDefenseCards.IsNeedBattleDeckMonsterCall)
			{
				foreach (var item in dic)
				{
					string itemCfgId = item.Key;
					if (ItemHelper.ChkIsCallMonster(itemCfgId))
					{
						list.Add(itemCfgId);
					}
				}
			}

			if (actionGameCfgRewardGamePlayTowerDefenseCards.ExtendTowerList.Count > 0)
			{
				list.AddRange(actionGameCfgRewardGamePlayTowerDefenseCards.ExtendTowerList);
			}

			List<(string towerCfgId, int towerNum)> towerList = ListComponent<(string towerCfgId, int towerNum)>.Create();
			for (int i = 0; i < actionGameCfgRewardGamePlayTowerDefenseCards.Num; i++)
			{
				int index = RandomGenerator.RandomNumber(0, list.Count);
				towerList.Add((list[index], 1));
			}
			gamePlayTowerDefenseComponent.RewardExtendCards(playerId, towerList);
			await ETTask.CompletedTask;
		}
	}
}
