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
			self.View.E_ARRoomModeCreateButton.AddListenerAsync(self.EnterARRoomCreateMode);
			self.View.E_ARRoomModeJoinButton.AddListenerAsync(self.EnterARRoomJoinMode);
			self.View.E_ReturnLoginButton.AddListenerAsync(self.ReturnLogin);
		}

		public static void ShowWindow(this DlgGameMode self, ShowWindowData contextData = null)
		{
		}

		public static async ETTask EnterSingleMapMode(this DlgGameMode self)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameMode>();
			await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgLobby>();
		}

		public static async ETTask EnterRoomMode(this DlgGameMode self)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameMode>();
			await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgHall>();
		}

		public static async ETTask EnterARRoomCreateMode(this DlgGameMode self)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

#if UNITY_EDITOR
			Log.Error($" UNITY_EDITOR ");
			return;
#endif

			DlgARHall_ShowWindowData _DlgARHall_ShowWindowData = new()
			{
				playerStatus = PlayerStatus.Room,
				RoomType = RoomType.Normal,
				SubRoomType = SubRoomType.NormalARCreate,
				arRoomId = 0,
			};
			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameMode>();
			await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData);
		}

		public static async ETTask EnterARRoomJoinMode(this DlgGameMode self)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

#if UNITY_EDITOR
			Log.Error($" UNITY_EDITOR ");
			return;
#endif

			DlgARHall_ShowWindowData _DlgARHall_ShowWindowData = new()
			{
				playerStatus = PlayerStatus.Room,
				RoomType = RoomType.Normal,
				SubRoomType = SubRoomType.NormalARScanCode,
				arRoomId = 0,
			};
			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgGameMode>();
			await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData);
		}

		public static async ETTask ReturnLogin(this DlgGameMode self)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(),SoundEffectType.Back);

			await LoginHelper.LoginOut(self.ClientScene());
		}
	}
}
