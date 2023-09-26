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
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            GamePlayTowerDefenseStatus gamePlayTowerDefenseStatus = gamePlayTowerDefenseComponent.gamePlayTowerDefenseStatus;
            self.View.E_RootImage.gameObject.SetActive(false);
            await self.ShowEffect(gamePlayTowerDefenseStatus);
            self.View.E_RootImage.gameObject.SetActive(true);
		}

		public static async ETTask ShowEffect(this DlgBattleTowerEnd self, GamePlayTowerDefenseStatus gamePlayTowerDefenseStatus)
		{
			Transform loseTrans = self.View.E_EffectImage.transform.Find("Effect_GameEnd/EButton_GameEnd_lose");
			Transform victoryTrans = self.View.E_EffectImage.transform.Find("Effect_GameEnd/EButton_GameEnd_victory");
            if (gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.GameSuccess)
            {
	            loseTrans.gameObject.SetActive(false);
	            victoryTrans.gameObject.SetActive(true);

	            string resAudioCfgId = "ResAudio_UI_victory";
	            ET.Ability.Client.UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), resAudioCfgId);
            }
            else
            {
	            loseTrans.gameObject.SetActive(true);
	            victoryTrans.gameObject.SetActive(false);

	            string resAudioCfgId = "ResAudio_UI_failed";
	            ET.Ability.Client.UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), resAudioCfgId);
            }

            await TimerComponent.Instance.WaitAsync(2000);
		}

		public static async ETTask OnReturnRoom(this DlgBattleTowerEnd self)
		{
			ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioClick(self.DomainScene());

			await RoomHelper.MemberReturnRoomFromBattleAsync(self.ClientScene());
			await SceneHelper.EnterHall(self.ClientScene());
		}
	}
}
