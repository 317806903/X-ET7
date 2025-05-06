using System.Collections;
using System.Collections.Generic;
using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgZpbTest))]
	public static class DlgZpbTestSystem
	{
		public static void RegisterUIEvent(this DlgZpbTest self)
		{
		}

		public static async ETTask PreLoadWindow(this DlgZpbTest self, ShowWindowData contextData, Action finished)
		{
			if (self.IsDisposed)
			{
				return;
			}
			finished?.Invoke();
		}

		public static async ETTask ShowWindow(this DlgZpbTest self, ShowWindowData contextData = null)
		{
			self.dlgShowTime = TimeHelper.ClientNow();

			DlgZpbTest_ShowWindowData _DlgZpbTest_ShowWindowData = (DlgZpbTest_ShowWindowData)contextData;
		}

		public static bool ChkCanClickBg(this DlgZpbTest self)
		{
			if (self.dlgShowTime < TimeHelper.ClientNow() - (long)(1000 * 1f))
			{
				return true;
			}
			return false;
		}

		public static void HideWindow(this DlgZpbTest self)
		{
		}

	}
}
