using System.Collections;
using System.Collections.Generic;
using System;
using ET.AbilityConfig;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgBattleHomeHUD))]
	public static class DlgBattleHomeHUDSystem
	{
		public static void RegisterUIEvent(this DlgBattleHomeHUD self)
		{
			self.View.E_Sprite_BGButton.AddListener(self.OnClickBG);
		}

		public static async ETTask ShowWindow(this DlgBattleHomeHUD self, ShowWindowData contextData = null)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

			self.homeUnitId = ((DlgBattleHomeHUD_ShowWindowData)contextData).homeUnitId;
			self.homeCfgId = ((DlgBattleHomeHUD_ShowWindowData)contextData).homeCfgId;
			self.isSelf = false;
			self._ShowWindow();
			self.ShowHomeInfo();

		}

		public static void HideWindow(this DlgBattleHomeHUD self)
		{

		}

		public static void _ShowWindow(this DlgBattleHomeHUD self)
		{

		}

		public static void ShowHomeInfo(this DlgBattleHomeHUD self)
		{
			self.ShowHomeInfo_Base();
			self.ShowHomeInfo_Attr();
		}

		public static void ShowHomeInfo_Base(this DlgBattleHomeHUD self)
		{
		}

		public static void ShowHomeInfo_Attr(this DlgBattleHomeHUD self)
		{
		}

		public static void OnClickBG(this DlgBattleHomeHUD self)
		{
			//UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

			self.OnClose();
		}

		public static void OnClose(this DlgBattleHomeHUD self)
		{
			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgBattleHomeHUD>();
		}

	}
}
