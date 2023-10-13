using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgGameModeAR))]
	public static class DlgGameModeARSystem
	{
		public static void RegisterUIEvent(this DlgGameModeAR self)
		{
			self.View.E_PVEButton.AddListenerAsync(self.EnterAREndlessChallenge);
			//self.View.E_PVEButton.AddListenerAsync(self.EnterARPVE);
			self.View.E_PVPButton.AddListenerAsync(self.EnterARPVP);
			self.View.E_ReturnLoginButton.AddListenerAsync(self.ReturnLogin);
		}

		public static void ShowWindow(this DlgGameModeAR self, ShowWindowData contextData = null)
		{
		}

		public static async ETTask EnterAREndlessChallenge(this DlgGameModeAR self)
		{
			ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioConfirm(self.DomainScene());

			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameModeAR>();

			DlgARHall_ShowWindowData _DlgARHall_ShowWindowData = new()
			{
				playerStatus = PlayerStatus.Hall,
				_ARRoomType = ARRoomType.EndlessChallenge,
				arRoomId = 0,
			};
			await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData);
		}

		public static async ETTask EnterARPVE(this DlgGameModeAR self)
		{
			ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioConfirm(self.DomainScene());

			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameModeAR>();

			DlgARHall_ShowWindowData _DlgARHall_ShowWindowData = new()
			{
				playerStatus = PlayerStatus.Hall,
				_ARRoomType = ARRoomType.PVE,
				arRoomId = 0,
			};
			await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData);
		}

		public static async ETTask EnterARPVP(this DlgGameModeAR self)
		{
			ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioConfirm(self.DomainScene());

			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameModeAR>();

			DlgARHall_ShowWindowData _DlgARHall_ShowWindowData = new()
			{
				playerStatus = PlayerStatus.Hall,
				_ARRoomType = ARRoomType.PVP,
				arRoomId = 0,
			};
			await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData);
		}

		public static async ETTask ReturnLogin(this DlgGameModeAR self)
		{
			ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioBack(self.DomainScene());

			await LoginHelper.LoginOut(self.ClientScene());
		}
	}
}
