using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[Invoke(TimerInvokeType.DlgFixedMenuHighestFrameTimer)]
	public class DlgFixedMenuHighestTimer: ATimer<DlgFixedMenuHighest>
	{
		protected override void Run(DlgFixedMenuHighest self)
		{
			try
			{
				if (self.IsDisposed)
				{
					TimerComponent.Instance?.Remove(ref self.Timer);
					return;
				}
				self.Update();
			}
			catch (Exception e)
			{
				Log.Error($"DlgFixedMenuHighest timer error: {self.Id}\n{e}");
			}
		}
	}

	[FriendOf(typeof(DlgFixedMenuHighest))]
	public static class DlgFixedMenuHighestSystem
	{
		public static void RegisterUIEvent(this DlgFixedMenuHighest self)
		{
		}

		public static void ShowWindow(this DlgFixedMenuHighest self, ShowWindowData contextData = null)
		{
			self._ShowWindow().Coroutine();
		}

		public static async ETTask _ShowWindow(this DlgFixedMenuHighest self)
		{
			self.isMoving = false;
			self.transPos.Clear();

			await self.ShowReportButton();
			self.Timer = TimerComponent.Instance.NewFrameTimer(TimerInvokeType.DlgFixedMenuHighestFrameTimer, self);

		}

		public static async ETTask ShowReportButton(this DlgFixedMenuHighest self)
		{
			self.View.EG_ReportRectTransform.SetVisible(true);

			EventTriggerListener.Get(self.View.EG_ReportRectTransform.gameObject).onPress.AddListener((go, data) =>
			{
			});

			EventTriggerListener.Get(self.View.EG_ReportRectTransform.gameObject).onClick.AddListener((go, data) =>
			{
				UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

				UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgGameReport>().Coroutine();
			});

			EventTriggerListener.Get(self.View.EG_ReportRectTransform.gameObject).onBeginDrag.AddListener((go, data) =>
			{
				Vector2 screenPos = data.position;

				self.View.EG_ReportRectTransform.SetRtAnchorSafe(new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f));

				self.SetTargetPos(screenPos, false);
			});

			EventTriggerListener.Get(self.View.EG_ReportRectTransform.gameObject).onDrag.AddListener((go, data) =>
			{
				Vector2 screenPos = data.position;

				self.SetTargetPos(screenPos, false);
			});

			EventTriggerListener.Get(self.View.EG_ReportRectTransform.gameObject).onEndDrag.AddListener((go, data) =>
			{
				Vector2 screenPos = data.position;
				self.SetTargetPos(screenPos, true);
			});
		}

		public static void SetTargetPos(this DlgFixedMenuHighest self, Vector2 screenPos, bool isNeedAdsorb)
		{
			self.isMoving = true;

			bool isLeft = false;
			bool isTop = false;

			Vector2 halfSize = self.View.EG_ReportRectTransform.sizeDelta * 0.5f * self.View.EG_ReportRectTransform.root.localScale.x;
			if (isNeedAdsorb)
			{
				float screenWidth = Screen.width;
				float screenHeight = Screen.height;

				float distToLeft = screenPos.x;
				float distToRight = Mathf.Abs(screenPos.x - screenWidth);

				float distToBottom = Mathf.Abs(screenPos.y);
				float distToTop = Mathf.Abs(screenPos.y - screenHeight);

				float horDistance = Mathf.Min(distToLeft, distToRight);
				float vertDistance = Mathf.Min(distToBottom, distToTop);

				if (horDistance < vertDistance)
				{
					if (distToLeft < distToRight)
					{
						screenPos = new Vector2(0, screenPos.y);
						isLeft = true;
					}
					else
						screenPos = new Vector2(screenWidth, screenPos.y);

					screenPos.y = Mathf.Clamp(screenPos.y, halfSize.y, screenHeight - halfSize.y);
				}
				else
				{
					if (distToBottom < distToTop)
						screenPos = new Vector2(screenPos.x, 0);
					else
					{
						screenPos = new Vector2(screenPos.x, screenHeight);
						isTop = true;
					}

					screenPos.x = Mathf.Clamp(screenPos.x, halfSize.x, screenWidth - halfSize.x);
				}
			}

			var canvasRT = self.View.EG_RootRectTransform;
			// 将屏幕坐标转换为UI坐标
			Vector2 canvasPosition;
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRT, screenPos, UIManagerComponent.Instance.UICamera, out canvasPosition))
			{
				if (isNeedAdsorb)
				{
					if (isLeft)
					{
						canvasPosition.x += halfSize.x;
					}
					else
					{
						canvasPosition.x -= halfSize.x;
					}
					if (isTop)
					{
						canvasPosition.y -= halfSize.y;
					}
					else
					{
						canvasPosition.y += halfSize.y;
					}
				}
				self.transPos[self.View.EG_ReportRectTransform] = canvasPosition;
			}
		}

		public static void HideWindow(this DlgFixedMenuHighest self)
		{
			TimerComponent.Instance?.Remove(ref self.Timer);
		}

		public static void Update(this DlgFixedMenuHighest self)
		{
			self.UpdatePos();
		}

		public static void UpdatePos(this DlgFixedMenuHighest self)
		{
			if (self.ChkIsMoving() == false)
			{
				return;
			}

			self.MoveingButton();
		}

		public static bool ChkIsMoving(this DlgFixedMenuHighest self)
		{
			if (self.isMoving == false)
			{
				return false;
			}
			foreach (var item in self.transPos)
			{
				RectTransform rectTrans = item.Key;
				Vector2 targetPos = item.Value;
				if (rectTrans.anchoredPosition.Equals(targetPos) == false)
				{
					self.isMoving = true;
					return self.isMoving;
				}
			}
			self.isMoving = false;
			return self.isMoving;
		}

		public static void MoveingButton(this DlgFixedMenuHighest self)
		{
			foreach (var item in self.transPos)
			{
				RectTransform rectTrans = item.Key;
				Vector2 targetPos = item.Value;
				if (rectTrans.anchoredPosition.Equals(targetPos) == false)
				{
					self.MoveingButtonOne(rectTrans, targetPos);
				}
			}
		}

		public static void MoveingButtonOne(this DlgFixedMenuHighest self, RectTransform rectTrans, Vector2 targetPos)
		{
			if (Math.Abs(rectTrans.anchoredPosition.x - targetPos.x) <= 1 && Math.Abs(rectTrans.anchoredPosition.y - targetPos.y) <= 1)
			{
				rectTrans.anchoredPosition = targetPos;
				return;
			}
			rectTrans.anchoredPosition = Vector2.Lerp(rectTrans.anchoredPosition, targetPos, 0.5f);
		}
	}
}
