using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgBattleTowerBegin))]
	public static class DlgBattleTowerBeginSystem
	{

		public static void RegisterUIEvent(this DlgBattleTowerBegin self)
		{

		}

		public static void ShowWindow(this DlgBattleTowerBegin self, ShowWindowData contextData = null)
		{
			string resAudioCfgId = "ResAudio_UI_ready_go";
			ET.Ability.Client.UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), resAudioCfgId);
		}
	}
}
