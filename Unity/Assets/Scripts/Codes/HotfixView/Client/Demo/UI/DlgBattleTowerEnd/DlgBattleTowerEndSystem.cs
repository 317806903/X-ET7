using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using ET.AbilityConfig;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgBattleTowerEnd))]
	public static class DlgBattleTowerEndSystem
	{

		public static void RegisterUIEvent(this DlgBattleTowerEnd self)
		{
			self.View.E_ReturnRoomButton.AddListenerAsync(self.OnReturnRoom);
		}

		public static void ShowWindow(this DlgBattleTowerEnd self, ShowWindowData contextData = null)
		{
			self.Show().Coroutine();
			self.ChkNeedShowGuide().Coroutine();
		}

		public static async ETTask Show(this DlgBattleTowerEnd self)
		{
            self.View.E_RootImage.gameObject.SetActive(false);
            await self.ShowEffect();
            self.View.E_RootImage.gameObject.SetActive(true);
		}

		public static async ETTask ChkNeedShowGuide(this DlgBattleTowerEnd self)
		{
			PlayerFunctionMenuComponent playerFunctionMenuComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerFunctionMenu(self.DomainScene());
			List<string> openningList = playerFunctionMenuComponent.GetOpenningFunctionMenuList();
			if (openningList.Count > 0)
			{
				string functionMenuCfgId = openningList[0];
				await ET.Client.UIGuideHelper.DoUIGuide(self.DomainScene(), "BattleTowerEndGuide", () =>
				{

				});
			}
		}

		public static async ETTask ShowEffect(this DlgBattleTowerEnd self)
		{
			PlayerStatusComponent playerStatusComponent = ET.Client.PlayerStatusHelper.GetMyPlayerStatusComponent(self.DomainScene());
			Log.Debug($"--ShowEffect playerStatusComponent[{playerStatusComponent}]");

			Transform transEndlessChallenge = self.View.uiTransform.transform.Find("E_Effect_EndlessChallenge");
			transEndlessChallenge.gameObject.SetActive(false);
			Transform transPVP = self.View.uiTransform.transform.Find("E_Effect_PVP");
			transPVP.gameObject.SetActive(false);
			Transform transNormal = self.View.uiTransform.transform.Find("E_Effect_Normal");
			transNormal.gameObject.SetActive(false);
			Transform transChallenge = self.View.uiTransform.transform.Find("E_Effect_EndChallengeMode");
			transChallenge.gameObject.SetActive(false);

			self.View.E_Return_TextTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_End_Next");

			await TimerComponent.Instance.WaitAsync(500);


			self.View.EG_ItemListRectTransform.SetVisible(false);
			self.View.EG_GoldCoinsRectTransform.SetVisible(false);
			await self.ShowDropItemList();
			self.ShowDropGold();

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
			GamePlayTowerDefenseStatus gamePlayTowerDefenseStatus = gamePlayTowerDefenseComponent.gamePlayTowerDefenseStatus;
			bool success = false;
			if (gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.GameEnd)
			{
				long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
				if (gamePlayTowerDefenseComponent.ChkHomeWin(myPlayerId))
				{
					success = true;
				}
			}
			if (gamePlayTowerDefenseComponent.gamePlayTowerDefenseMode == GamePlayTowerDefenseMode.TowerDefense_PVE)
			{
				await self.ShowEffectPVE(success);
				if (success)
				{
					await ET.Client.GameJudgeChooseHelper.ShowGameJudgeChoose(self.DomainScene());
				}
			}
			else if (gamePlayTowerDefenseComponent.gamePlayTowerDefenseMode == GamePlayTowerDefenseMode.TowerDefense_EndlessChallenge)
			{
				MonsterWaveCallComponent monsterWaveCallComponent = gamePlayTowerDefenseComponent.GetComponent<MonsterWaveCallComponent>();
				int aliveNum = monsterWaveCallComponent.GetWaveIndex() - 1;
				await self.ShowEffectEndlessChallenge(aliveNum);

				if (aliveNum > 5)
				{
					await ET.Client.GameJudgeChooseHelper.ShowGameJudgeChoose(self.DomainScene());
				}
			}
			else if (gamePlayTowerDefenseComponent.gamePlayTowerDefenseMode == GamePlayTowerDefenseMode.TowerDefense_PVP)
			{
				await self.ShowEffectPVP(success);
			}
			else
			{
				await self.ShowEffectNormal(success);
			}

			EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
			{
				eventName = "LevelEnded",
				properties = new()
				{
					{"finished", true},
					{"max_wave_num", self.GetCurMonsterWave()},
					{"tower_num", self.GetMyTowerList().Count},
					{"coin_num", self.GetMyGold()},
				}
			});

		}

		public static async ETTask ShowEffectNormal(this DlgBattleTowerEnd self, bool success)
		{
			Transform transPVP = self.View.uiTransform.transform.Find("E_Effect_Normal");
			transPVP.gameObject.SetActive(true);

			Transform loseTrans = transPVP.Find("Effect_GameEnd2/EButton_GameEnd_lose");
			Transform victoryTrans = transPVP.Find("Effect_GameEnd2/EButton_GameEnd_victory");
            if (success)
            {
	            loseTrans.gameObject.SetActive(false);
	            victoryTrans.gameObject.SetActive(true);

	            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.GameEndWin);
            }
            else
            {
	            loseTrans.gameObject.SetActive(true);
	            victoryTrans.gameObject.SetActive(false);

	            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.GameEndFail);
            }

            await TimerComponent.Instance.WaitAsync(2000);
		}

		public static async ETTask ShowEffectPVP(this DlgBattleTowerEnd self, bool success)
		{
			Transform transPVP = self.View.uiTransform.transform.Find("E_Effect_PVP");
			transPVP.gameObject.SetActive(true);

			Transform loseTrans = transPVP.Find("Effect_GameEnd/EButton_GameEnd_lose");
			Transform victoryTrans = transPVP.Find("Effect_GameEnd/EButton_GameEnd_victory");
            if (success)
            {
	            loseTrans.gameObject.SetActive(false);
	            victoryTrans.gameObject.SetActive(true);

	            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.GameEndWin);
            }
            else
            {
	            loseTrans.gameObject.SetActive(true);
	            victoryTrans.gameObject.SetActive(false);

	            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.GameEndFail);
            }

            await TimerComponent.Instance.WaitAsync(2000);
		}

		public static async ETTask ShowEffectPVE(this DlgBattleTowerEnd self, bool success)
		{
			Transform transPVE = self.View.uiTransform.transform.Find("E_Effect_EndChallengeMode");
			transPVE.gameObject.SetActive(true);
			self.View.E_victoryImage.gameObject.SetActive(true);
			Transform loseTrans = transPVE.Find("Effect_GameEnd_ChallengeModeEnds/E_lose");
			Transform victoryTrans = transPVE.Find("Effect_GameEnd_ChallengeModeEnds/E_victory");

			loseTrans.gameObject.SetActive(false);
			victoryTrans.gameObject.SetActive(false);

			string cfgId = GamePlayHelper.GetGamePlay(self.DomainScene()).GetGamePlayBattleConfig().Id;
			int level = TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallengeIndex(cfgId);
			self.View.ELabel_LvTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_BattleEnd_ChallengeLevel", level);
            if (success)
            {
	            if (TowerDefense_ChallengeLevelCfgCategory.Instance.GetNextChallenge(cfgId) == null)
	            {
		            self.View.E_Return_TextTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_End_Retry");
	            }
	            else
	            {
		            self.View.E_Return_TextTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_End_Next");
	            }

				loseTrans.gameObject.SetActive(false);
	            victoryTrans.gameObject.SetActive(true);
	            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.GameEndWin);

	            int count = self.GetMyDropItemList().Count;
	            if (count > 0)
	            {
		            await TimerComponent.Instance.WaitAsync(2000);
		            self.View.E_victoryImage.gameObject.SetActive(false);
		            self.View.EG_ItemListRectTransform.SetVisible(true);
		            return;
	            }
            }
            else
            {
	            self.View.E_Return_TextTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_End_Retry");
				loseTrans.gameObject.SetActive(true);
	            victoryTrans.gameObject.SetActive(false);
	            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.GameEndFail);
            }

            await TimerComponent.Instance.WaitAsync(2000);
		}

		public static async ETTask ShowEffectEndlessChallenge(this DlgBattleTowerEnd self, int waveIndex)
		{
			Transform transEndlessChallenge = self.View.uiTransform.transform.Find("E_Effect_EndlessChallenge");
			transEndlessChallenge.gameObject.SetActive(true);

			//string text1 = $"存活<color=#ffffff><size=150>{waveIndex}</size></color>波";
			//string text1 = LocalizeComponent.Instance.GetTextValue("TextCode_Key_BattleEnd_ChallengeEnds1", waveIndex);
			self.View.ELabel_ChanllengeNumText.text = waveIndex.ToString();
			// LayoutRebuilder.ForceRebuildLayoutImmediate(self.View.E_ChanllengeLevel_TextTextMeshProUGUI.GetComponent<RectTransform>());

			//string text2 = $"战胜N%的玩家";
			// int rankedMoreThan = await ET.Client.RankHelper.GetRankedMoreThan(self.DomainScene(), RankType.EndlessChallenge, waveIndex);
			// string text2 = LocalizeComponent.Instance.GetTextValue("TextCode_Key_BattleEnd_ChallengeEnds2", 100-rankedMoreThan);
			// self.View.E_ChanllengeText_2TextMeshProUGUI.text = text2;

			(ulong rankIndex, int rankedMoreThan) = await ET.Client.RankHelper.GetRankedMoreThan(self.DomainScene(), RankType.EndlessChallenge, waveIndex);
			//Log.Debug($"rankIndex={rankIndex} rankedMoreThan={rankedMoreThan}");
			if (rankIndex == 0 || rankIndex == 99999)
			{
				string noRank = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Rank_NoRank");
				self.View.ELabel_RankTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_BattleEnd_Rank", noRank);
			}
			else
			{
				self.View.ELabel_RankTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_BattleEnd_Rank", rankIndex);
			}

			long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());

			int killNum = GamePlayHelper.GetGamePlay(self.DomainScene()).GetComponent<GamePlayStatisticalDataManagerComponent>().GetPlayerKillNum(myPlayerId);
			self.View.ELabel_KillNumTextMeshProUGUI.text =
					LocalizeComponent.Instance.GetTextValue("TextCode_Key_BattleEnd_KillNum", killNum);

			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.GameEndFinish);

            await TimerComponent.Instance.WaitAsync(2000);
		}

		public static async ETTask OnReturnRoom(this DlgBattleTowerEnd self)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

			await RoomHelper.MemberReturnRoomFromBattleAsync(self.ClientScene());
			await SceneHelper.EnterHall(self.ClientScene(), false, false);
		}

		public static int GetCurMonsterWave(this DlgBattleTowerEnd self)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
			MonsterWaveCallComponent monsterWaveCallComponent = gamePlayTowerDefenseComponent.GetComponent<MonsterWaveCallComponent>();
			return monsterWaveCallComponent.GetWaveIndex();
		}

		public static int GetMyGold(this DlgBattleTowerEnd self)
		{
			GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(self.DomainScene());
			long playerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
			int curGoldValue = (int)gamePlayComponent.GetPlayerCoin(playerId, CoinType.Gold);
			return curGoldValue;
		}

		public static List<long> GetMyTowerList(this DlgBattleTowerEnd self)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
			long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
			List<long> towerList = gamePlayTowerDefenseComponent.GetPutTowers(myPlayerId);
			return towerList;
		}

		public static List<(string itemCfgId, int num)> GetMyDropItemList(this DlgBattleTowerEnd self)
		{
			long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());

			Dictionary<string, int> dropItemDic = GamePlayHelper.GetGamePlay(self.DomainScene()).GetComponent<GamePlayDropItemComponent>().GetPlayerDropItemList(myPlayerId);
			if (dropItemDic == null || dropItemDic.Count == 0)
			{
				return new();
			}
			List<(string, int)> dropItemList = dropItemDic.Select(dropItem=>(dropItem.Key, dropItem.Value)).ToList();
			return dropItemList;
		}

		public static int GetMyDropGold(this DlgBattleTowerEnd self)
		{
			long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());

			int dropGold = GamePlayHelper.GetGamePlay(self.DomainScene()).GetComponent<GamePlayDropItemComponent>().GetPlayerDropGold(myPlayerId);
			return dropGold;
		}

		public static void ShowDropGold(this DlgBattleTowerEnd self)
		{
			int gold = self.GetMyDropGold();
			self.View.E_GoldTextMeshProUGUI.SetText($"+{gold}");
		}

		public static async ETTask<bool> ShowDropItemList(this DlgBattleTowerEnd self)
		{
			int count = self.GetMyDropItemList().Count;

			if (count > 0)
			{
				self.View.ELoopScrollList_ItemLoopHorizontalScrollRect.prefabSource.prefabName = "Item_TowerBuy";
				self.View.ELoopScrollList_ItemLoopHorizontalScrollRect.prefabSource.poolSize = 7;
				self.View.ELoopScrollList_ItemLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
					self.AddItemRefreshListener(transform, i));

				self.AddUIScrollItems(ref self.ScrollItemReward, count);
				self.View.ELoopScrollList_ItemLoopHorizontalScrollRect.SetVisible(true, count);

				return true;
			}
			else
			{
				return false;
			}
		}

        public static async ETTask AddItemRefreshListener(this DlgBattleTowerEnd self, Transform transform, int index)
        {
	        self.View.ELoopScrollList_ItemLoopHorizontalScrollRect.SetSrcollMiddle(index);

            Scroll_Item_TowerBuy itemTower = self.ScrollItemReward[index].BindTrans(transform);

            var dropItems = self.GetMyDropItemList();
            string itemCfgId = dropItems[index].itemCfgId;
            itemTower.ShowBagItem(itemCfgId, true);
        }
	}
}
