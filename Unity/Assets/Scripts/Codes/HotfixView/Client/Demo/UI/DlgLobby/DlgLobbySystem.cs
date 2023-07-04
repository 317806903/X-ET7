using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgLobby))]
	public static  class DlgLobbySystem
	{

		public static void RegisterUIEvent(this DlgLobby self)
		{
			self.View.E_EnterMapButton.AddListenerAsync(self.EnterMap);
			self.View.E_ReturnLoginButton.AddListenerAsync(self.ReturnLogin);
		}

		public static void ShowWindow(this DlgLobby self, Entity contextData = null)
		{
		}

		public static async ETTask EnterMap(this DlgLobby self)
		{
			string mapName = self.View.E_InputFieldInputField.text;
			await EnterMapHelper.EnterMapAsync(self.ClientScene(), mapName);
		}
		
		public static async ETTask ReturnLogin(this DlgLobby self)
		{
			await LoginHelper.LoginOut(self.ClientScene());
			self.ClientScene().GetComponent<UIComponent>().HideAllShownWindow();
			await self.ClientScene().GetComponent<UIComponent>().ShowWindowAsync(WindowID.WindowID_Login);
		}

	}
}
