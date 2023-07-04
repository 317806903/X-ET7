using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgGameMode))]
	public static  class DlgGameModeSystem
	{

		public static void RegisterUIEvent(this DlgGameMode self)
		{
			self.View.E_SingleMapModeButton.AddListenerAsync(self.EnterSingleMapMode);
			self.View.E_RoomModeButton.AddListenerAsync(self.EnterRoomMode);
			self.View.E_ARRoomModeButton.AddListenerAsync(self.EnterARRoomMode);
			self.View.E_ReturnLoginButton.AddListenerAsync(self.ReturnLogin);
		}

		public static void ShowWindow(this DlgGameMode self, Entity contextData = null)
		{
		}

		public static async ETTask EnterSingleMapMode(this DlgGameMode self)
		{
			self.ClientScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_GameMode);
			await self.ClientScene().GetComponent<UIComponent>().ShowWindowAsync(WindowID.WindowID_Lobby);
		}

		public static async ETTask EnterRoomMode(this DlgGameMode self)
		{
			self.ClientScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_GameMode);
			await self.ClientScene().GetComponent<UIComponent>().ShowWindowAsync(WindowID.WindowID_Hall);
		}

		public static async ETTask EnterARRoomMode(this DlgGameMode self)
		{
			self.ClientScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_GameMode);
			await self.ClientScene().GetComponent<UIComponent>().ShowWindowAsync(WindowID.WindowID_ARRoom);
		}

		public static async ETTask ReturnLogin(this DlgGameMode self)
		{
			await LoginHelper.LoginOut(self.ClientScene());
			self.ClientScene().GetComponent<UIComponent>().HideAllShownWindow();
			await self.ClientScene().GetComponent<UIComponent>().ShowWindowAsync(WindowID.WindowID_Login);
		}

		 

	}
}
