using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgBattleTowerEnd))]
	public static  class DlgBattleTowerEndSystem
	{

		public static void RegisterUIEvent(this DlgBattleTowerEnd self)
		{
			self.View.E_ReturnRoomButton.AddListenerAsync(self.OnReturnRoom);
		}

		public static void ShowWindow(this DlgBattleTowerEnd self, Entity contextData = null)
		{
			self.ShowGameResult();
		}

		public static void ShowGameResult(this DlgBattleTowerEnd self)
		{
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlayer(self.DomainScene());
            GamePlayStatus gamePlayStatus = gamePlayComponent.GamePlayStatus;
            if (gamePlayStatus == GamePlayStatus.GameSuccess)
            {
	            self.View.ELabel_GameEndText.text = "战斗胜利";
            }
            else
            {
	            self.View.ELabel_GameEndText.text = "战斗失败";
            }
		}

		public static async ETTask OnReturnRoom(this DlgBattleTowerEnd self)
		{
			await RoomHelper.MemberReturnRoomFromBattleAsync(self.ClientScene());
			await SceneHelper.EnterHall(self.ClientScene());
		}
	}
}
