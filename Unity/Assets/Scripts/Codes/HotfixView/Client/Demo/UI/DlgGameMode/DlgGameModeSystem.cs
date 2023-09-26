using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgGameMode))]
	public static class DlgGameModeSystem
	{

		public static void RegisterUIEvent(this DlgGameMode self)
		{
			self.View.E_SingleMapModeButton.AddListenerAsync(self.EnterSingleMapMode);
			self.View.E_RoomModeButton.AddListenerAsync(self.EnterRoomMode);
			self.View.E_ARRoomModeButton.AddListenerAsync(self.EnterARRoomMode);
			self.View.E_ReturnLoginButton.AddListenerAsync(self.ReturnLogin);
		}

		public static void ShowWindow(this DlgGameMode self, ShowWindowData contextData = null)
		{
		}

		public static async ETTask EnterSingleMapMode(this DlgGameMode self)
		{
			ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioConfirm(self.DomainScene());

			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameMode>();
			await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgLobby>();
		}

		public static async ETTask EnterRoomMode(this DlgGameMode self)
		{
			ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioConfirm(self.DomainScene());

			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameMode>();
			await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgHall>();
		}

		public static async ETTask EnterARRoomMode(this DlgGameMode self)
		{
			ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioConfirm(self.DomainScene());

			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameMode>();
			await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgARHall>();
		}

		public static async ETTask ReturnLogin(this DlgGameMode self)
		{
			ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioBack(self.DomainScene());

			await LoginHelper.LoginOut(self.ClientScene());
		}



	}
}
