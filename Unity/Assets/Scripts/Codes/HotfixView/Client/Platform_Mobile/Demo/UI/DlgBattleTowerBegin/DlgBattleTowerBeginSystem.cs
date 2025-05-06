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

		public static async ETTask ShowWindow(this DlgBattleTowerBegin self, ShowWindowData contextData = null)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.ReadyGo);

			await TimerComponent.Instance.WaitAsync(2000);
			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgBattleTowerBegin>();
		}

		public static void HideWindow(this DlgBattleTowerBegin self)
		{
		}

	}
}
