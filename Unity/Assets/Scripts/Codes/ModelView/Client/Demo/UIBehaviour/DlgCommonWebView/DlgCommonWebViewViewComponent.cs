
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgCommonWebView))]
	[EnableMethod]
	public class DlgCommonWebViewViewComponent : Entity, IAwake, IDestroy
	{
		public UnityEngine.RectTransform EGBackGroundRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EGBackGroundRectTransform == null )
				{
					this.m_EGBackGroundRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround");
				}
				return this.m_EGBackGroundRectTransform;
			}
		}

		public UnityEngine.RectTransform EG_WebViewShowRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_WebViewShowRectTransform == null )
				{
					this.m_EG_WebViewShowRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/EG_WebViewShow");
				}
				return this.m_EG_WebViewShowRectTransform;
			}
		}

		public void DestroyWidget()
		{
			this.m_EGBackGroundRectTransform = null;
			this.m_EG_WebViewShowRectTransform = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EGBackGroundRectTransform = null;
		private UnityEngine.RectTransform m_EG_WebViewShowRectTransform = null;
		public Transform uiTransform = null;
	}
}
