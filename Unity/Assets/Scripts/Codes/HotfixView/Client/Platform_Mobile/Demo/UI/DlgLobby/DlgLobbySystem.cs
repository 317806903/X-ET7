using System.Collections;
using System.Collections.Generic;
using System;
using ET.AbilityConfig;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgLobby))]
	public static class DlgLobbySystem
	{

		public static void RegisterUIEvent(this DlgLobby self)
		{
			self.View.E_EnterMapButton.AddListenerAsync(self.EnterMap);
			self.View.E_ReturnLoginButton.AddListenerAsync(self.ReturnBack);
			self.View.EButton_ChooseBattleCfgButton.AddListenerAsync(self.OnChooseBattleCfg);

			self.View.EButton_ChgBattleDeckButton.AddListenerAsync(self.OnChgBattleDeck);
		}

		public static async ETTask ShowWindow(this DlgLobby self, ShowWindowData contextData = null)
		{
			self.ShowCurBattleCfgId(null, false);
		}

		public static void HideWindow(this DlgLobby self)
		{

		}

		public static void ShowCurBattleCfgId(this DlgLobby self, string gamePlayBattleLevelCfgId, bool isForceSet)
		{
			if (isForceSet)
			{
				GamePlayBattleLevelCfg gamePlayBattleLevelCfg = GamePlayBattleLevelCfgCategory.Instance.Get(gamePlayBattleLevelCfgId);
				self.gamePlayBattleLevelCfgId = gamePlayBattleLevelCfgId;
				self.View.ELabel_BattleCfgIdTextMeshProUGUI.text = gamePlayBattleLevelCfg.Name;
				self.SetLastBattleCfgIdChoose(gamePlayBattleLevelCfgId);
			}
			else
			{
				gamePlayBattleLevelCfgId = self.GetLastBattleCfgIdChoose();
				GamePlayBattleLevelCfg gamePlayBattleLevelCfg = GamePlayBattleLevelCfgCategory.Instance.Get(gamePlayBattleLevelCfgId);
				self.gamePlayBattleLevelCfgId = gamePlayBattleLevelCfgId;
				self.View.ELabel_BattleCfgIdTextMeshProUGUI.text = gamePlayBattleLevelCfg.Name;
			}
		}

		public static string GetLastBattleCfgIdChoose(this DlgLobby self)
		{
			return PlayerPrefs.GetString("DlgLobbyBattleCfgId", "GamePlayBattleLevel_Global1");
		}

		public static void SetLastBattleCfgIdChoose(this DlgLobby self, string gamePlayBattleLevelCfgId)
		{
			PlayerPrefs.SetString("DlgLobbyBattleCfgId", gamePlayBattleLevelCfgId);
		}

		public static async ETTask RefreshBattleCfgIdChoose(this DlgLobby self, string gamePlayBattleLevelCfgId)
		{
			self.ShowCurBattleCfgId(gamePlayBattleLevelCfgId, true);
		}

		public static async ETTask EnterMap(this DlgLobby self)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

			string gamePlayBattleLevelCfgId = self.gamePlayBattleLevelCfgId;
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

		public static async ETTask ReturnBack(this DlgLobby self)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(),SoundEffectType.Back);

			await ET.Client.UIManagerHelper.ExitRoomUI(self.DomainScene());
		}

		public static async ETTask OnChooseBattleCfg(this DlgLobby self)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

			await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBattleCfgChoose>(new DlgBattleCfgChoose_ShowWindowData()
			{
				isGlobalMode = true,
				isAR = false,
			});
		}

		public static async ETTask OnChgBattleDeck(this DlgLobby self)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

			await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBattleDeck>();
		}

	}
}
