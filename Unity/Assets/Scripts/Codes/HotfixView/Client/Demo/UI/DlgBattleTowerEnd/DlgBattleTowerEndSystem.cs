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
			Transform transPVE = self.View.uiTransform.transform.Find("E_Effect_PVE");
			transPVE.gameObject.SetActive(false);
			Transform transPVP = self.View.uiTransform.transform.Find("E_Effect_PVP");
			transPVP.gameObject.SetActive(false);
			Transform transNormal = self.View.uiTransform.transform.Find("E_Effect_Normal");
			transNormal.gameObject.SetActive(false);

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
			}
			else if (gamePlayTowerDefenseComponent.gamePlayTowerDefenseMode == GamePlayTowerDefenseMode.TowerDefense_EndlessChallenge)
			{
				MonsterWaveCallComponent monsterWaveCallComponent = gamePlayTowerDefenseComponent.GetComponent<MonsterWaveCallComponent>();
				await self.ShowEffectEndlessChallenge(monsterWaveCallComponent.curIndex);

				EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
				{
					eventName = "InfinityEnded",
					properties = new()
					{
						{"finished", true},
						{"max_wave_num", self.GetCurMonsterWave()},
						{"tower_num", self.GetMyTowerList().Count},
						{"coin_num", self.GetMyGold()},
					}
				});

			}
			else if (gamePlayTowerDefenseComponent.gamePlayTowerDefenseMode == GamePlayTowerDefenseMode.TowerDefense_PVP)
			{
				await self.ShowEffectPVP(success);
			}
			else
			{
				await self.ShowEffectNormal(success);
			}
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

	            string resAudioCfgId = "ResAudio_UI_victory";
	            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), resAudioCfgId);
            }
            else
            {
	            loseTrans.gameObject.SetActive(true);
	            victoryTrans.gameObject.SetActive(false);

	            string resAudioCfgId = "ResAudio_UI_failed";
	            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), resAudioCfgId);
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

	            string resAudioCfgId = "ResAudio_UI_victory";
	            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), resAudioCfgId);
            }
            else
            {
	            loseTrans.gameObject.SetActive(true);
	            victoryTrans.gameObject.SetActive(false);

	            string resAudioCfgId = "ResAudio_UI_failed";
	            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), resAudioCfgId);
            }

            await TimerComponent.Instance.WaitAsync(2000);
		}

		public static async ETTask ShowEffectPVE(this DlgBattleTowerEnd self, bool success)
		{
			Transform transPVE = self.View.uiTransform.transform.Find("E_Effect_PVE");
			transPVE.gameObject.SetActive(true);
			transPVE.Find("Effect_GameEnd_Model/EButton_GameEnd_lose").gameObject.SetActive(false);
			transPVE.Find("Effect_GameEnd_Model/EButton_GameEnd_victory").gameObject.SetActive(false);

			Transform loseTrans = transPVE.Find("Effect_GameEnd_Model/EButton_ChallengeMode_lose");
			Transform victoryTrans = transPVE.Find("Effect_GameEnd_Model/EButton_ChallengeMode_victory");

			string cfgId = GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene()).GetGamePlay().GetGamePlayBattleConfig().Id;
			string[] sArray = cfgId.Split('_');
			string challengeLevel = sArray[2];
			int level = int.Parse(challengeLevel.Replace("Level", ""));
			
            if (success)
            {
	            loseTrans.gameObject.SetActive(false);
	            victoryTrans.gameObject.SetActive(true);
				self.View.E_ChanllengeLevel_Text_2TextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_BattleEnd_ChallengeLevel", level);
	            string resAudioCfgId = "ResAudio_UI_victory";
	            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), resAudioCfgId);
            }
            else
            {
	            loseTrans.gameObject.SetActive(true);
	            victoryTrans.gameObject.SetActive(false);
				self.View.E_ChanllengeLevel_TextTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_BattleEnd_ChallengeLevel", level);
				string resAudioCfgId = "ResAudio_UI_failed";
	            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), resAudioCfgId);
            }

            await TimerComponent.Instance.WaitAsync(2000);
		}

		public static async ETTask ShowEffectEndlessChallenge(this DlgBattleTowerEnd self, int waveIndex)
		{
			Transform transEndlessChallenge = self.View.uiTransform.transform.Find("E_Effect_EndlessChallenge");
			transEndlessChallenge.gameObject.SetActive(true);



			//string text1 = $"存活<color=#ffffff><size=150>{waveIndex}</size></color>波";
			//string text1 = LocalizeComponent.Instance.GetTextValue("TextCode_Key_BattleEnd_ChallengeEnds1", waveIndex);
			self.View.ELabel_ChanllengeNumText.text = waveIndex + "\n";
			//string text2 = $"战胜N%的玩家";
			int rankedMoreThan = await ET.Client.RankHelper.GetRankedMoreThan(self.DomainScene(), RankType.EndlessChallenge, waveIndex);
			string text2 = LocalizeComponent.Instance.GetTextValue("TextCode_Key_BattleEnd_ChallengeEnds2", 100-rankedMoreThan);
			self.View.E_ChanllengeText_2TextMeshProUGUI.text = text2;

			string resAudioCfgId = "ResAudio_UI_victory";
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), resAudioCfgId);

            await TimerComponent.Instance.WaitAsync(2000);
		}

		public static async ETTask OnReturnRoom(this DlgBattleTowerEnd self)
		{
			UIAudioManagerHelper.PlayUIAudioClick(self.DomainScene());

			await RoomHelper.MemberReturnRoomFromBattleAsync(self.ClientScene());
			await SceneHelper.EnterHall(self.ClientScene());
		}

		public static int GetCurMonsterWave(this DlgBattleTowerEnd self)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
			MonsterWaveCallComponent monsterWaveCallComponent = gamePlayTowerDefenseComponent.GetComponent<MonsterWaveCallComponent>();
			return monsterWaveCallComponent.curIndex;
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
	}
}
