using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgCommonTip))]
	public static class DlgCommonTipSystem
	{

		public static void RegisterUIEvent(this DlgCommonTip self)
		{
			self.transTipNode = self.View.E_TipNodeImage.transform;
			self.transTipNode.gameObject.SetActive(false);
			self.isDoing = false;
		}

		public static async ETTask ShowWindow(this DlgCommonTip self, ShowWindowData contextData = null)
		{
			self.isDoing = false;
			self.tips.Clear();
		}

		public static void HideWindow(this DlgCommonTip self)
		{

		}

		public static void ShowTip(this DlgCommonTip self, string tipMsg)
		{
			self.tips.Enqueue(tipMsg);

			self._DoShowTip().Coroutine();
		}

		public static async ETTask _DoShowTip(this DlgCommonTip self)
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
			string tipMsg = self.tips.Dequeue();
			self.View.E_TipTextTextMeshProUGUI.text = tipMsg;
			GameObject tipNode = GameObject.Instantiate(self.transTipNode.gameObject);
			self.tipShowGoList.Add(tipNode);
			tipNode.SetActive(true);
			Transform parent = self.View.EGBackGroundRectTransform.transform;
			tipNode.transform.SetParent(parent);
			var uiRect = tipNode.GetComponent<RectTransform>();
			uiRect.localScale = Vector3.one;
			uiRect.localPosition = Vector3.zero; // 注意PosZ也要设置，否则有可能会不显示
			uiRect.sizeDelta = parent.GetComponent<RectTransform>().sizeDelta;

			self._TipMove(uiRect).Coroutine();

			await TimerComponent.Instance.WaitAsync(200);

			self.isDoing = false;

			await self._DoShowTip();
		}

		public static async ETTask _TipMove(this DlgCommonTip self, RectTransform uiRect)
		{
			Sequence mySequence = DOTween.Sequence();
			Tweener twe = uiRect.transform.DOLocalMoveY(300, 1);
			twe.SetEase(Ease.OutCubic);
			mySequence.Append(twe);
			mySequence.AppendInterval(0.8f);
			mySequence.OnComplete(() =>
			{
				self.ChkNeedClose(uiRect.gameObject);
			});
			await ETTask.CompletedTask;
		}

		public static void ChkNeedClose(this DlgCommonTip self, GameObject go)
		{
			self.tipShowGoList.Remove(go);
			GameObject.Destroy(go);
			if (self.tipShowGoList.Count == 0 && self.tips.Count == 0)
			{
				if (self.IsDisposed == false)
				{
					UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgCommonTip>();
				}
			}
		}
	}
}
