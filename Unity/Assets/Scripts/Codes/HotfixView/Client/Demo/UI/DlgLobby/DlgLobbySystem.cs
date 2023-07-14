using System.Collections;
using System.Collections.Generic;
using System;
using ET.AbilityConfig;
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
			self.View.E_InputFieldInputField.onEndEdit.RemoveAllListeners();
			self.View.E_InputFieldInputField.onEndEdit.AddListener(
				(txt)=>
				{
					self.ChgGlobalBattleLevelCfg().Coroutine();
				});
		}

		public static void ShowWindow(this DlgLobby self, Entity contextData = null)
		{
		}

		public static async ETTask ChgGlobalBattleLevelCfg(this DlgLobby self)
		{
			string gamePlayBattleLevelCfgId = self.View.E_InputFieldInputField.text;
			if (GamePlayBattleLevelCfgCategory.Instance.Contain(gamePlayBattleLevelCfgId) == false)
			{
				Log.Error($"GamePlayBattleLevelCfg not exist when gamePlayBattleLevelCfgId[{gamePlayBattleLevelCfgId}]");
				return;
			}
			GamePlayBattleLevelCfg gamePlayBattleLevelCfg = GamePlayBattleLevelCfgCategory.Instance.Get(gamePlayBattleLevelCfgId);
			if (gamePlayBattleLevelCfg.IsGlobalMode == false)
			{
				Log.Error($"gamePlayBattleLevelCfg.IsGlobalMode == false when gamePlayBattleLevelCfgId[{gamePlayBattleLevelCfgId}]");
				return;
			}

		}
		
		public static async ETTask EnterMap(this DlgLobby self)
		{
			string gamePlayBattleLevelCfgId = self.View.E_InputFieldInputField.text;
			if (GamePlayBattleLevelCfgCategory.Instance.Contain(gamePlayBattleLevelCfgId) == false)
			{
				Log.Error($"GamePlayBattleLevelCfg not exist when gamePlayBattleLevelCfgId[{gamePlayBattleLevelCfgId}]");
				return;
			}

			GamePlayBattleLevelCfg gamePlayBattleLevelCfg = GamePlayBattleLevelCfgCategory.Instance.Get(gamePlayBattleLevelCfgId);
			if (gamePlayBattleLevelCfg.IsGlobalMode == false)
			{
				Log.Error($"gamePlayBattleLevelCfg.IsGlobalMode == false when gamePlayBattleLevelCfgId[{gamePlayBattleLevelCfgId}]");
				return;
			}
			
			await EnterMapHelper.EnterMapAsync(self.ClientScene(), gamePlayBattleLevelCfgId);
		}
		
		public static async ETTask ReturnLogin(this DlgLobby self)
		{
			await LoginHelper.LoginOut(self.ClientScene());
			self.ClientScene().GetComponent<UIComponent>().HideAllShownWindow();
			await self.ClientScene().GetComponent<UIComponent>().ShowWindowAsync(WindowID.WindowID_Login);
		}

	}
}
