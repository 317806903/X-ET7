using System.Collections;
using System.Collections.Generic;
using System;
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
				PutHomeComponent putHomeComponent = gamePlayTowerDefenseComponent.GetComponent<PutHomeComponent>();
				long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());

				if (putHomeComponent.ChkHomeWin(myPlayerId))
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
					eventName = "Infinity_finish",
					properties = new()
					{
						{"is_finish", true},
						{"max_wave", monsterWaveCallComponent.curIndex},
						{"tower_num", 1},
						{"coin_left", 1},
						{"last_time", 0},
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

			Transform loseTrans = transPVE.Find("Effect_GameEnd_Model/EButton_GameEnd_lose");
			Transform victoryTrans = transPVE.Find("Effect_GameEnd_Model/EButton_GameEnd_victory");
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

		public static async ETTask ShowEffectEndlessChallenge(this DlgBattleTowerEnd self, int waveIndex)
		{
			Transform transEndlessChallenge = self.View.uiTransform.transform.Find("E_Effect_EndlessChallenge");
			transEndlessChallenge.gameObject.SetActive(true);



			//string text1 = $"存活<color=#ffffff><size=150>{waveIndex}</size></color>波";
			string text1 = LocalizeComponent.Instance.GetTextValue("TextCode_Key_BattleEnd_ChallengeEnds1", waveIndex);
			self.View.E_ChanllengeText_1TextMeshProUGUI.text = text1;
			//string text2 = $"战胜N%的玩家";
			string text2 = LocalizeComponent.Instance.GetTextValue("TextCode_Key_BattleEnd_ChallengeEnds2", "N");
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
	}
}
