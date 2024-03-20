using System.Collections;
using System.Collections.Generic;
using System;
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
		}

		public static async ETTask Show(this DlgBattleTowerEnd self)
		{
            self.View.E_RootImage.gameObject.SetActive(false);
            await self.ShowEffect();
            self.View.E_RootImage.gameObject.SetActive(true);
		}

		public static async ETTask ShowEffect(this DlgBattleTowerEnd self)
		{
			PlayerStatusComponent playerStatusComponent = ET.Client.PlayerHelper.GetMyPlayerStatusComponent(self.DomainScene());
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

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
			GamePlayTowerDefenseStatus gamePlayTowerDefenseStatus = gamePlayTowerDefenseComponent.gamePlayTowerDefenseStatus;
			bool success = false;
			if (gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.GameEnd)
			{
				long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
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
			Transform rewardCardTrans = transPVE.Find("Effect_GameEnd_ChallengeModeEnds/E_victory/E_Reward");
			Transform rewardGoldTrans = transPVE.Find("Effect_GameEnd_ChallengeModeEnds/E_GoldCoins");

			loseTrans.gameObject.SetActive(false);
			victoryTrans.gameObject.SetActive(false);
			rewardCardTrans.gameObject.SetActive(false);
			rewardGoldTrans.gameObject.SetActive(false);
			self.View.E_NewcardImage.gameObject.SetActive(false);

			string cfgId = GamePlayHelper.GetGamePlay(self.DomainScene()).GetGamePlayBattleConfig().Id;
			int level = TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallengeIndex(cfgId);
			self.View.ELabel_LvTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_BattleEnd_ChallengeLevel", level);
            if (success)
            {
	            int count = TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallenges(true).Count;
				if(level == count){
					self.View.E_Return_TextTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_End_Retry");
				}else{
					self.View.E_Return_TextTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_End_Next");
				}
				loseTrans.gameObject.SetActive(false);
	            victoryTrans.gameObject.SetActive(true);
	            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.GameEndWin);

				long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
				Dictionary<string, int> dropItems = GamePlayHelper.GetGamePlay(self.DomainScene()).GetComponent<GamePlayStatisticalDataManagerComponent>().GetPlayerDropItemsInfo(myPlayerId);
				foreach((string itemCfgId, int itemCnt) in dropItems)
				{
					if (ItemHelper.ChkIsTower(itemCfgId))
					{
						self.View.EButton_nameTextMeshProUGUI.text = ItemHelper.GetItemName(itemCfgId);
						await self.View.EButton_IconImage.SetImageByPath(ItemHelper.GetItemIcon(itemCfgId));

						List<string> labels = ItemHelper.GetTowerItemLabels(itemCfgId);
						int labelCount = labels.Count;
						self.View.EImage_Label1Image.gameObject.SetActive(labelCount >= 1);
						self.View.EImage_Label2Image.gameObject.SetActive(labelCount >= 2);
						if (labelCount >= 1)
						{
							self.View.ELabel_Label1TextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue(labels[0]);
						}

						if (labelCount >= 2)
						{
							self.View.ELabel_Label2TextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue(labels[1]);
						}

						int towerQuality = (int)ItemHelper.GetItemQualityType(itemCfgId);
						self.View.EImage_LowImage.SetVisible(towerQuality == 0);
						self.View.EImage_MiddleImage.SetVisible(towerQuality == 1);
						self.View.EImage_HighImage.SetVisible(towerQuality == 2);

						self.View.EButton_BuyButton.AddListener(()=>
						{
							self.ShowDetails(itemCfgId);
						});

						await TimerComponent.Instance.WaitAsync(2000);
						self.View.E_victoryImage.gameObject.SetActive(false);
						rewardCardTrans.gameObject.SetActive(true);
						self.View.E_NewcardImage.gameObject.SetActive(true);
						break;
					}
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
			LayoutRebuilder.ForceRebuildLayoutImmediate(self.View.E_ChanllengeLevel_TextTextMeshProUGUI.GetComponent<RectTransform>());

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

			long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());

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
			long playerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
			int curGoldValue = (int)gamePlayComponent.GetPlayerCoin(playerId, CoinType.Gold);
			return curGoldValue;
		}

		public static List<long> GetMyTowerList(this DlgBattleTowerEnd self)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
			long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
			List<long> towerList = gamePlayTowerDefenseComponent.GetPutTowers(myPlayerId);
			return towerList;
		}

		public static void ShowDetails(this DlgBattleTowerEnd self, string itemCfgId)
		{
			UIComponent _UIComponent = UIManagerHelper.GetUIComponent(self.DomainScene());
			_UIComponent.ShowWindow<DlgDetails>();
			DlgDetails _DlgDetails = _UIComponent.GetDlgLogic<DlgDetails>(true);
			if (_DlgDetails != null)
			{
				_DlgDetails.SetCurItemCfgId(itemCfgId);
			}
		}
	}
}
