
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgCommonLoading))]
	[EnableMethod]
	public class DlgCommonLoadingViewComponent : Entity, IAwake, IDestroy
	{
		public UnityEngine.UI.Button E_BG_ClickButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BG_ClickButton == null )
				{
					this.m_E_BG_ClickButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_BG_Click");
				}
				return this.m_E_BG_ClickButton;
			}
		}

		public UnityEngine.UI.Image E_BG_ClickImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BG_ClickImage == null )
				{
					this.m_E_BG_ClickImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_BG_Click");
				}
				return this.m_E_BG_ClickImage;
			}
		}

		public UnityEngine.RectTransform EG_ShowRootRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_ShowRootRectTransform == null )
				{
					this.m_EG_ShowRootRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_ShowRoot");
				}
				return this.m_EG_ShowRootRectTransform;
			}
		}

		public void DestroyWidget()
		{
			this.m_E_BG_ClickButton = null;
			this.m_E_BG_ClickImage = null;
			this.m_EG_ShowRootRectTransform = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_BG_ClickButton = null;
		private UnityEngine.UI.Image m_E_BG_ClickImage = null;
		private UnityEngine.RectTransform m_EG_ShowRootRectTransform = null;
		public Transform uiTransform = null;
	}
}
