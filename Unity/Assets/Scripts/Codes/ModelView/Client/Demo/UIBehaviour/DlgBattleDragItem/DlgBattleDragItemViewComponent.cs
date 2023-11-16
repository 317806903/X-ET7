
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgBattleDragItem))]
	[EnableMethod]
	public class DlgBattleDragItemViewComponent : Entity, IAwake, IDestroy
	{
		public UnityEngine.RectTransform EGRootRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EGRootRectTransform == null )
				{
					this.m_EGRootRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGRoot");
				}
				return this.m_EGRootRectTransform;
			}
		}

		public UnityEngine.UI.Image E_CancelImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_CancelImage == null )
				{
					this.m_E_CancelImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRoot/E_Cancel");
				}
				return this.m_E_CancelImage;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_TextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_TextMeshProUGUI == null )
				{
					this.m_ELabel_TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGRoot/E_Cancel/ELabel_");
				}
				return this.m_ELabel_TextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Image E_TipNodeImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_TipNodeImage == null )
				{
					this.m_E_TipNodeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRoot/TipNode/E_TipNode");
				}
				return this.m_E_TipNodeImage;
			}
		}

		public TMPro.TextMeshProUGUI E_TipTextTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_TipTextTextMeshProUGUI == null )
				{
					this.m_E_TipTextTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGRoot/TipNode/E_TipNode/E_TipText");
				}
				return this.m_E_TipTextTextMeshProUGUI;
			}
		}

		public void DestroyWidget()
		{
			this.m_EGRootRectTransform = null;
			this.m_E_CancelImage = null;
			this.m_ELabel_TextMeshProUGUI = null;
			this.m_E_TipNodeImage = null;
			this.m_E_TipTextTextMeshProUGUI = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EGRootRectTransform = null;
		private UnityEngine.UI.Image m_E_CancelImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_TextMeshProUGUI = null;
		private UnityEngine.UI.Image m_E_TipNodeImage = null;
		private TMPro.TextMeshProUGUI m_E_TipTextTextMeshProUGUI = null;
		public Transform uiTransform = null;
	}
}
