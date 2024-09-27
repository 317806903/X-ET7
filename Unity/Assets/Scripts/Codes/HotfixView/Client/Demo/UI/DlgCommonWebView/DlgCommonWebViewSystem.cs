using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgCommonWebView))]
	public static class DlgCommonWebViewSystem
	{
		public static void RegisterUIEvent(this DlgCommonWebView self)
		{
		}

		public static async ETTask ShowWindow(this DlgCommonWebView self, ShowWindowData contextData = null)
		{
		}

		public static void HideWindow(this DlgCommonWebView self)
		{
		}

		public static void OnClose(this DlgCommonWebView self)
		{
			ET.Client.UIRootManagerComponent.Instance.SetDefaultRotation().Coroutine();
			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgCommonWebView>();
		}

		public static void ShowWebView(this DlgCommonWebView self, string url)
		{
			ET.Client.UIRootManagerComponent.Instance.SetAutoRotation().Coroutine();

			UniWebView uniWebView = self.View.EG_WebViewShowRectTransform.gameObject.AddComponent<UniWebView>();
			uniWebView.ReferenceRectTransform = self.View.EG_WebViewShowRectTransform;
			uniWebView.EmbeddedToolbar.SetPosition(UniWebViewToolbarPosition.Top);
			uniWebView.EmbeddedToolbar.Show();
			uniWebView.OnShouldClose += (view) => {
				GameObject.Destroy(uniWebView);
				uniWebView = null;
				self.OnClose();
				return true;
			};
			uniWebView.Load(url);
			uniWebView.Show();
		}

	}
}
