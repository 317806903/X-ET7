using System.Collections;
using System.Collections.Generic;
using System;
using ET.AbilityConfig;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgFunctionMenuOpenShow))]
	public static class DlgFunctionMenuOpenShowSystem
	{
		public static void RegisterUIEvent(this DlgFunctionMenuOpenShow self)
		{
			self.View.E_BGButton.AddListenerAsync(self.OnCloseButton);
		}

		public static async ETTask ShowWindow(this DlgFunctionMenuOpenShow self, ShowWindowData contextData = null)
		{
			if (contextData == null)
			{
				Log.Error("contextData == null");
				return;
			}
			else
			{
				DlgFunctionMenuOpenShow_ShowWindowData _DlgFunctionMenuOpenShow_ShowWindowData = contextData as DlgFunctionMenuOpenShow_ShowWindowData;
				self.functionMenuCfgId = _DlgFunctionMenuOpenShow_ShowWindowData.functionMenuCfgId;
				self.finished = _DlgFunctionMenuOpenShow_ShowWindowData.finished;
			}

			self._ShowWindow().Coroutine();
		}

		public static async ETTask _ShowWindow(this DlgFunctionMenuOpenShow self)
		{
			FunctionMenuCfg functionMenuCfg = FunctionMenuCfgCategory.Instance.Get(self.functionMenuCfgId);
			self.View.ELabel_Label1TextMeshProUGUI.text = functionMenuCfg.Name;
			self.View.ELabel_Label2TextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_FunctionMenu_Unlocked");
			await self.View.E_ImageImage.SetImageByPath(self, functionMenuCfg.Icon_Ref.ResName, true);
		}

		public static void HideWindow(this DlgFunctionMenuOpenShow self)
		{
		}

		public static async ETTask OnCloseButton(this DlgFunctionMenuOpenShow self)
		{
			self.finished?.Invoke(self.DomainScene());
			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgFunctionMenuOpenShow>();
		}
	}
}
