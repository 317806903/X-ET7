using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgCommonTipTopShow))]
	public static class DlgCommonTipTopShowSystem
	{
		public static void RegisterUIEvent(this DlgCommonTipTopShow self)
		{
			self.transTipNode = self.View.E_TipNodeImage.transform;
			self.transTipNode.gameObject.SetActive(false);
			self.isDoing = false;
		}

		public static void ShowWindow(this DlgCommonTipTopShow self, ShowWindowData contextData = null)
		{
			self.isDoing = false;
			self.tips.Clear();
		}

		public static void ShowTip(this DlgCommonTipTopShow self, string tipMsg)
		{
			self.tips.Push(tipMsg);

			self._DoShowTip().Coroutine();
		}

		public static async ETTask _DoShowTip(this DlgCommonTipTopShow self)
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

			self.ChkNeedClose(tipNode);

			self.isDoing = false;

			await self._DoShowTip();
		}

		public static void ChkNeedClose(this DlgCommonTipTopShow self, GameObject go)
		{
			GameObject.Destroy(go);

			if (self.tips.Count == 0)
			{
				UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgCommonTipTopShow>();
			}
		}
	}
}
