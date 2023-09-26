using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[Invoke(TimerInvokeType.LoadingTimer)]
	public class DlgLoadingTimer: ATimer<DlgLoading>
	{
		protected override void Run(DlgLoading self)
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

	[FriendOf(typeof(DlgLoading))]
	public static class DlgLoadingSystem
	{

		public static void RegisterUIEvent(this DlgLoading self)
		{

		}

		public static void ShowWindow(this DlgLoading self, ShowWindowData contextData = null)
		{
			self.targetProcess = 0.5f;
			self.curProcess = 0.2f;

			self.Timer = TimerComponent.Instance.NewRepeatedTimer(100, TimerInvokeType.LoadingTimer, self);
		}

		public static void HideWindow(this DlgLoading self)
		{
			TimerComponent.Instance?.Remove(ref self.Timer);
		}

		public static void UpdateProcess(this DlgLoading self, float process)
		{
			Log.Debug($"DlgLoadingSystem.UpdateProcess process={process} self.targetProcess={self.targetProcess}");
			if (self.targetProcess < process)
			{
				self.targetProcess = process;
			}
		}

		public static void Update(this DlgLoading self)
		{
			if (self.targetProcess >= 1)
			{
				UIManagerHelper.GetUIComponent(self.DomainScene()).CloseWindow<DlgLoading>();
				return;
			}
			if (self.curProcess >= self.targetProcess)
			{
				return;
			}

			self.curProcess += 0.05f;
			self.View.E_SliderSlider.value = self.curProcess;
		}
	}
}
