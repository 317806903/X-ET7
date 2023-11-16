using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
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
			self.transBackground = self.View.uiTransform.Find("Sprite_BackGround/ProgressPrarent/Processing/Background");
			self.transPercentage = self.View.uiTransform.Find("Sprite_BackGround/ProgressPrarent/Processing/Percentage");
		}

		public static void ShowWindow(this DlgLoading self, ShowWindowData contextData = null)
		{
			self.ShowProcess(0);
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

		public static void ShowProcess(this DlgLoading self, float per)
		{
			self.transBackground.localScale = new Vector3(per, 1, 1);
			self.transPercentage.gameObject.GetComponent<TextMeshProUGUI>().text = $"{(int)(per * 100)}%";
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
			self.ShowProcess(self.curProcess);
		}
	}
}
