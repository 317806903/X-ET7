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

		public static void ShowWindow(this DlgCommonTip self, ShowWindowData contextData = null)
		{
		}

		public static void ShowTip(this DlgCommonTip self, string tipMsg)
		{
			self.tips.Push(tipMsg);

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
			string tipMsg = self.tips.Pop();
			self.View.E_TipTextTextMeshProUGUI.text = tipMsg;
			GameObject tipNode = GameObject.Instantiate(self.transTipNode.gameObject);
			tipNode.SetActive(true);
			Transform parent = self.View.EGBackGroundRectTransform.transform;
			tipNode.transform.SetParent(parent);
			var uiRect = tipNode.GetComponent<RectTransform>();
			uiRect.localScale = Vector3.one;
			uiRect.localPosition = Vector3.zero; // 注意PosZ也要设置，否则有可能会不显示
			uiRect.sizeDelta = parent.GetComponent<RectTransform>().sizeDelta;

			self._TipMove(uiRect).Coroutine();

			await TimerComponent.Instance.WaitAsync(700);

			self.isDoing = false;

			await self._DoShowTip();
		}

		public static async ETTask _TipMove(this DlgCommonTip self, RectTransform uiRect)
		{
			Tweener twe = uiRect.transform.DOLocalMoveY(300, 2); //3秒时间在世界坐标中,让X轴移动到5的位置
			twe.OnComplete(() =>
			{
				GameObject.Destroy(uiRect.gameObject);
			});
			twe.SetEase(Ease.OutCubic);
			await Task.CompletedTask;
		}
	}
}
