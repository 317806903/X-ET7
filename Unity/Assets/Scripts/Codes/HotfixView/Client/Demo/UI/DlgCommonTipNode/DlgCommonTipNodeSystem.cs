using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgCommonTipNode))]
	public static class DlgCommonTipNodeSystem
	{
		public static void RegisterUIEvent(this DlgCommonTipNode self)
		{
			self.transTipNode = self.View.E_TipNodeImage.transform;
			self.transTipNode.gameObject.SetActive(false);
			self.isDoing = false;
		}

		public static void ShowWindow(this DlgCommonTipNode self, ShowWindowData contextData = null)
		{
		}
		
		public static void ShowTip(this DlgCommonTipNode self, string tipMsg)
		{
			self.tips.Push(tipMsg);

			self._DoShowTip().Coroutine();
		}

		public static async ETTask _DoShowTip(this DlgCommonTipNode self)
		{
			if (self.tips.Count == 0)
			{
				return;
			}

			if (self.isDoing)
			{
				return;
			}

			self.isDoing = true;
			string tipMsg = self.tips.Pop();
			self.View.E_TipTextTextMeshProUGUI.text = tipMsg;
			GameObject tipNode = GameObject.Instantiate(self.transTipNode.gameObject);
			tipNode.SetActive(true);
			Transform parent = self.View.EGBackGroundRectTransform.transform;
			tipNode.transform.SetParent(parent);
			var uiRect = tipNode.GetComponent<RectTransform>();
			uiRect.localScale = Vector3.one;
			uiRect.localPosition = new Vector3(0, parent.GetComponent<RectTransform>().rect.height / 2 - 80, 0); // 注意PosZ也要设置，否则有可能会不显示
			uiRect.sizeDelta = parent.GetComponent<RectTransform>().sizeDelta;

			await TimerComponent.Instance.WaitAsync(5000);
			
			GameObject.Destroy(tipNode);

			self.isDoing = false;

			await self._DoShowTip();
		}
	}
}
