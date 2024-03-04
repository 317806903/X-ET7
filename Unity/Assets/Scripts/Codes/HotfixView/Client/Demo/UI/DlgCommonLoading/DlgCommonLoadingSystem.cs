using System.Collections;
using System.Collections.Generic;
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgCommonLoading))]
	public static class DlgCommonLoadingSystem
	{
		public static void RegisterUIEvent(this DlgCommonLoading self)
		{
		}

		public static void ShowWindow(this DlgCommonLoading self, ShowWindowData contextData = null)
		{
			self.showNum = 0;
		}

		public static void Show(this DlgCommonLoading self)
		{
			self.showNum++;

			CanvasGroup canvasGroup = self.View.EG_ShowRootRectTransform.GetComponent<CanvasGroup>();
			if (self.showNum == 1)
			{
				canvasGroup.alpha = 0;
				canvasGroup.DOKill();
				Tweener twe = canvasGroup.DOFade(0, 0).From(0).SetDelay(2);
				twe.OnComplete(() =>
				{
					Tweener twe2 = canvasGroup.DOFade(1, 2).From(0).SetDelay(10);
					twe2.OnComplete(() =>
					{
						self.Hide(true);
					});
				});
			}
		}

		public static void Hide(this DlgCommonLoading self, bool bForceHide)
		{
			if (bForceHide)
			{
				self.showNum = 0;
			}
			else
			{
				self.showNum--;
			}
			if (self.showNum <= 0)
			{
				UIComponent _UIComponent = UIManagerHelper.GetUIComponent(self.DomainScene());
				_UIComponent.HideWindow<DlgCommonLoading>();
				CanvasGroup canvasGroup = self.View.EG_ShowRootRectTransform.GetComponent<CanvasGroup>();
				canvasGroup.DOKill();
			}
		}
	}
}
