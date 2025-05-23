﻿using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgDescTips))]
	public static class DlgDescTipsSystem
	{
		public static void RegisterUIEvent(this DlgDescTips self)
		{
			self.View.EButton_CloseButton.AddListenerAsync(self.OnCloseButton);
			self.View.E_BGButton.AddListenerAsync(self.OnCloseButton);
		}

		public static async ETTask ShowWindow(this DlgDescTips self, ShowWindowData contextData = null)
		{
			if (contextData == null)
			{
				Log.Error("contextData == null");
				return;
			}
			else
			{
				DlgDescTips_ShowWindowData _DlgDescTips_ShowWindowData = contextData as DlgDescTips_ShowWindowData;
				self.ShowTip(_DlgDescTips_ShowWindowData.Desc, _DlgDescTips_ShowWindowData.Pos, _DlgDescTips_ShowWindowData.tipTextAlignmentMid, _DlgDescTips_ShowWindowData.notNeedClickBg).Coroutine();
			}
		}

		public static void HideWindow(this DlgDescTips self)
		{
		}

		public static async ETTask ShowTip(this DlgDescTips self, string tipDesc, Vector3 pos, bool tipTextAlignmentMid, bool notNeedClickBg)
		{
			if (tipTextAlignmentMid)
			{
				self.View.ELabel_Label1TextMeshProUGUI.horizontalAlignment = HorizontalAlignmentOptions.Center;
			}
			else
			{
				self.View.ELabel_Label1TextMeshProUGUI.horizontalAlignment = HorizontalAlignmentOptions.Left;
			}

			if (notNeedClickBg)
			{
				self.View.E_BGButton.SetVisible(false);
			}
			else
			{
				self.View.E_BGButton.SetVisible(true);
			}
			RectTransform tipTextNodeRectTransform = self.View.EImage_Label1Image.rectTransform;
			self.View.EG_TipsRectTransform.anchoredPosition = new Vector2(-5000, -5000);
			tipTextNodeRectTransform.anchoredPosition = new Vector2(0, tipTextNodeRectTransform.anchoredPosition.y);
			tipTextNodeRectTransform.sizeDelta = new Vector2(self.View.EG_TipsRectTransform.rect.width * 0.5f, tipTextNodeRectTransform.sizeDelta.y);
			self.View.ELabel_Label1TextMeshProUGUI.text = tipDesc;

			VerticalLayoutGroup verticalLayoutGroup = self.View.EImage_Label1Image.gameObject.GetComponent<VerticalLayoutGroup>();
			verticalLayoutGroup.childControlWidth = true;
			LayoutRebuilder.ForceRebuildLayoutImmediate(tipTextNodeRectTransform);
			if (self.View.ELabel_Label1TextMeshProUGUI.rectTransform.sizeDelta.x < 1)
			{
				verticalLayoutGroup.childControlWidth = false;
				self.View.ELabel_Label1TextMeshProUGUI.rectTransform.sizeDelta = new Vector2(tipTextNodeRectTransform.sizeDelta.x, tipTextNodeRectTransform.sizeDelta.y/3);
			}
			else
			{
				tipTextNodeRectTransform.sizeDelta = new Vector2(self.View.ELabel_Label1TextMeshProUGUI.rectTransform.sizeDelta.x + 100, tipTextNodeRectTransform.sizeDelta.y);
			}

			self.View.EG_TipsRectTransform.position = pos;

			await self.MoveTipWhenOutScreen();
		}

		public static async ETTask MoveTipWhenOutScreen(this DlgDescTips self)
		{
			RectTransform tipTextNodeRectTransform = self.View.EImage_Label1Image.rectTransform;
			Vector2 lastTipAnchoredPos = tipTextNodeRectTransform.anchoredPosition;
			float tipWidth = tipTextNodeRectTransform.sizeDelta.x;
			Vector2 lastTipPos = lastTipAnchoredPos + self.View.EG_TipsRectTransform.anchoredPosition;

			float borderlineDis = 25;
			float screenWidth = self.View.EG_TipsRectTransform.rect.width;
			if (lastTipPos.x > 0)
			{
				float maxX = lastTipPos.x + tipWidth * 0.5f;
				if (maxX <= screenWidth * 0.5f - borderlineDis)
				{
					return;
				}

				tipTextNodeRectTransform.anchoredPosition = new Vector2(lastTipAnchoredPos.x - (maxX - (screenWidth * 0.5f - borderlineDis)), lastTipAnchoredPos.y);
			}
			else
			{
				float minX = lastTipPos.x - tipWidth * 0.5f;
				if (minX >= -screenWidth * 0.5f + borderlineDis)
				{
					return;
				}

				tipTextNodeRectTransform.anchoredPosition = new Vector2(lastTipAnchoredPos.x - (minX + (screenWidth * 0.5f - borderlineDis)), lastTipAnchoredPos.y);
			}
		}

		public static async ETTask OnCloseButton(this DlgDescTips self)
		{
			await TimerComponent.Instance.WaitFrameAsync();
			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgDescTips>();
		}
	}
}
