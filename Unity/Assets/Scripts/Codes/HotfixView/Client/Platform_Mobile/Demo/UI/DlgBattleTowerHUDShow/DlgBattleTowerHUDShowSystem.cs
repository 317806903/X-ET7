using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[Invoke(TimerInvokeType.BattleTowerHUDShowFrameTimer)]
	public class DlgBattleTowerHUDShowTimer: ATimer<DlgBattleTowerHUDShow>
	{
		protected override void Run(DlgBattleTowerHUDShow self)
		{
			try
			{
				self.Update();
			}
			catch (Exception e)
			{
				Log.Error($"move timer error: {self.Id}\n{e}");
			}
		}
	}

	[FriendOf(typeof(DlgBattleTowerHUDShow))]
	public static class DlgBattleTowerHUDShowSystem
	{
		public static void RegisterUIEvent(this DlgBattleTowerHUDShow self)
		{
		}

		public static async ETTask ShowWindow(this DlgBattleTowerHUDShow self, ShowWindowData contextData = null)
		{
			self.HomeHealthBarDictionary.Clear();
			self.Timer = TimerComponent.Instance.NewFrameTimer(TimerInvokeType.BattleTowerHUDShowFrameTimer, self);
		}

		public static void HideWindow(this DlgBattleTowerHUDShow self)
		{
			TimerComponent.Instance?.Remove(ref self.Timer);
		}

		public static void Update(this DlgBattleTowerHUDShow self)
		{
			if (++self.curFrame >= self.waitFrame)
			{
				self.curFrame = 0;

				self.UpdateSiblingIndex();
			}
		}

		public static void UpdateSiblingIndex(this DlgBattleTowerHUDShow self)
		{
			var sortedList = self.HomeHealthBarDictionary.OrderByDescending(x => x.Value).ToList();
			for (int i = 0; i < sortedList.Count; i++)
			{
				sortedList[i].Key.SetSiblingIndex(i);
			}
		}

		public static void UpdateDistance(this DlgBattleTowerHUDShow self, RectTransform rectTrans, float distance)
		{
			self.HomeHealthBarDictionary[rectTrans] = distance;
		}

		public static void RemoveDic(this DlgBattleTowerHUDShow self, RectTransform rectTrans)
		{
			if (self.HomeHealthBarDictionary.ContainsKey(rectTrans))
			{
				self.HomeHealthBarDictionary.Remove(rectTrans);
			}
			if (self.HomeHealthBarDictionary.Count == 0)
			{
				self.HideWindow();
			}
		}

	}
}
