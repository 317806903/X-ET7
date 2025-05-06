
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

		public UnityEngine.UI.Button E_CancelButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_CancelButton == null )
				{
					this.m_E_CancelButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGRoot/E_Cancel");
				}
				return this.m_E_CancelButton;
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

		public UnityEngine.RectTransform EG_ConfirmRootRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_ConfirmRootRectTransform == null )
				{
					this.m_EG_ConfirmRootRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGRoot/EG_ConfirmRoot");
				}
				return this.m_EG_ConfirmRootRectTransform;
			}
		}

		public UnityEngine.UI.Button E_ConfirmButtonButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ConfirmButtonButton == null )
				{
					this.m_E_ConfirmButtonButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGRoot/EG_ConfirmRoot/confirm/E_ConfirmButton");
				}
				return this.m_E_ConfirmButtonButton;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_ConfirmTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_ConfirmTextMeshProUGUI == null )
				{
					this.m_ELabel_ConfirmTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGRoot/EG_ConfirmRoot/confirm/E_ConfirmButton/ELabel_Confirm");
				}
				return this.m_ELabel_ConfirmTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView ELabel_ConfirmUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_ConfirmUITextLocalizeMonoView == null )
				{
					this.m_ELabel_ConfirmUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EGRoot/EG_ConfirmRoot/confirm/E_ConfirmButton/ELabel_Confirm");
				}
				return this.m_ELabel_ConfirmUITextLocalizeMonoView;
			}
		}

		public UnityEngine.UI.Button E_CancelButtonButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_CancelButtonButton == null )
				{
					this.m_E_CancelButtonButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGRoot/EG_ConfirmRoot/cancel/E_CancelButton");
				}
				return this.m_E_CancelButtonButton;
			}
		}

		public UnityEngine.UI.Image E_CancelButtonImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_CancelButtonImage == null )
				{
					this.m_E_CancelButtonImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRoot/EG_ConfirmRoot/cancel/E_CancelButton");
				}
				return this.m_E_CancelButtonImage;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_CancelTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_CancelTextMeshProUGUI == null )
				{
					this.m_ELabel_CancelTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGRoot/EG_ConfirmRoot/cancel/E_CancelButton/ELabel_Cancel");
				}
				return this.m_ELabel_CancelTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView ELabel_CancelUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_CancelUITextLocalizeMonoView == null )
				{
					this.m_ELabel_CancelUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EGRoot/EG_ConfirmRoot/cancel/E_CancelButton/ELabel_Cancel");
				}
				return this.m_ELabel_CancelUITextLocalizeMonoView;
			}
		}

		public UnityEngine.RectTransform EG_OpenAnimationRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_OpenAnimationRectTransform == null )
				{
					this.m_EG_OpenAnimationRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_OpenAnimation");
				}
				return this.m_EG_OpenAnimationRectTransform;
			}
		}

		public UnityEngine.RectTransform EG_CloseAnimationRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_CloseAnimationRectTransform == null )
				{
					this.m_EG_CloseAnimationRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_CloseAnimation");
				}
				return this.m_EG_CloseAnimationRectTransform;
			}
		}

		public void DestroyWidget()
		{
			this.m_EGRootRectTransform = null;
			this.m_E_CancelButton = null;
			this.m_E_CancelImage = null;
			this.m_ELabel_TextMeshProUGUI = null;
			this.m_E_TipNodeImage = null;
			this.m_E_TipTextTextMeshProUGUI = null;
			this.m_EG_ConfirmRootRectTransform = null;
			this.m_E_ConfirmButtonButton = null;
			this.m_ELabel_ConfirmTextMeshProUGUI = null;
			this.m_ELabel_ConfirmUITextLocalizeMonoView = null;
			this.m_E_CancelButtonButton = null;
			this.m_E_CancelButtonImage = null;
			this.m_ELabel_CancelTextMeshProUGUI = null;
			this.m_ELabel_CancelUITextLocalizeMonoView = null;
			this.m_EG_OpenAnimationRectTransform = null;
			this.m_EG_CloseAnimationRectTransform = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EGRootRectTransform = null;
		private UnityEngine.UI.Button m_E_CancelButton = null;
		private UnityEngine.UI.Image m_E_CancelImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_TextMeshProUGUI = null;
		private UnityEngine.UI.Image m_E_TipNodeImage = null;
		private TMPro.TextMeshProUGUI m_E_TipTextTextMeshProUGUI = null;
		private UnityEngine.RectTransform m_EG_ConfirmRootRectTransform = null;
		private UnityEngine.UI.Button m_E_ConfirmButtonButton = null;
		private TMPro.TextMeshProUGUI m_ELabel_ConfirmTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ELabel_ConfirmUITextLocalizeMonoView = null;
		private UnityEngine.UI.Button m_E_CancelButtonButton = null;
		private UnityEngine.UI.Image m_E_CancelButtonImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_CancelTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ELabel_CancelUITextLocalizeMonoView = null;
		private UnityEngine.RectTransform m_EG_OpenAnimationRectTransform = null;
		private UnityEngine.RectTransform m_EG_CloseAnimationRectTransform = null;
		public Transform uiTransform = null;
	}
}
