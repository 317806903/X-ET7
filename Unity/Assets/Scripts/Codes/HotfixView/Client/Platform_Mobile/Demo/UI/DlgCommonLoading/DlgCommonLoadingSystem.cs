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

		public static async ETTask ShowWindow(this DlgCommonLoading self, ShowWindowData contextData = null)
		{
			self.showNum = 0;
			self.isForceShow = false;
		}

		public static void HideWindow(this DlgCommonLoading self)
		{
			self.showNum = 0;
			self.isForceShow = false;
		}

		public static void Show(this DlgCommonLoading self, bool bForceShow)
		{
			self.showNum++;

			CanvasGroup canvasGroup = self.View.EG_ShowRootRectTransform.GetComponent<CanvasGroup>();
			if (bForceShow)
			{
				self.isForceShow = true;
				if (self.quence != null)
				{
					self.quence.Kill();
					self.quence.Complete();
					self.quence = null;
				}
				self.quence = DOTween.Sequence();
				canvasGroup.alpha = 0;
				self.quence.Append(canvasGroup.DOFade(1, 2).From(0));
				self.quence.AppendInterval(10);
				self.quence.OnComplete(() =>
				{
					self.Hide(true);
				});
			}
			else if(self.isForceShow)
			{
				if (self.quence != null)
				{
					self.quence.Kill();
					self.quence.Complete();
					self.quence = null;
				}
				self.quence = DOTween.Sequence();
				self.quence.Append(canvasGroup.DOFade(1, 2).From(0));
				self.quence.AppendInterval(10);
				self.quence.OnComplete(() =>
				{
					self.Hide(true);
				});
			}
			else
			{
				if (self.quence != null)
				{
					self.quence.Kill();
					self.quence.Complete();
					self.quence = null;
				}
				self.quence = DOTween.Sequence();
				canvasGroup.alpha = 0;
				self.quence.AppendInterval(2);
				self.quence.Append(canvasGroup.DOFade(1, 2).From(0));
				self.quence.AppendInterval(10);
				self.quence.OnComplete(() =>
				{
					self.Hide(true);
				});
			}
		}

		public static void Hide(this DlgCommonLoading self, bool bForceHide)
		{
			if (self.IsDisposed)
			{
				return;
			}
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
				self.isForceShow = false;
				if (self.quence != null)
				{
					self.quence.Kill();
					self.quence.Complete();
					self.quence = null;
				}
				UIComponent _UIComponent = UIManagerHelper.GetUIComponent(self.DomainScene());
				_UIComponent.HideWindow<DlgCommonLoading>();
			}
		}
	}
}
