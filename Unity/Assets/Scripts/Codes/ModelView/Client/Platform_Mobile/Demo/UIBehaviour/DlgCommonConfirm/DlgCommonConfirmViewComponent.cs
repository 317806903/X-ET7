
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgCommonConfirm))]
	[EnableMethod]
	public class DlgCommonConfirmViewComponent : Entity, IAwake, IDestroy
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
					this.m_E_BG_ClickButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/E_BG_Click");
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
					this.m_E_BG_ClickImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/E_BG_Click");
				}
				return this.m_E_BG_ClickImage;
			}
		}

		public UnityEngine.RectTransform EG_bgARRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_bgARRectTransform == null )
				{
					this.m_EG_bgARRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/E_BG_Click/EG_bgAR");
				}
				return this.m_EG_bgARRectTransform;
			}
		}

		public UnityEngine.UI.Image EG_bgARImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_bgARImage == null )
				{
					this.m_EG_bgARImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/E_BG_Click/EG_bgAR");
				}
				return this.m_EG_bgARImage;
			}
		}

		public UnityEngine.RectTransform EG_bgRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_bgRectTransform == null )
				{
					this.m_EG_bgRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/E_BG_Click/EG_bg");
				}
				return this.m_EG_bgRectTransform;
			}
		}

		public UnityEngine.UI.Image EG_bgImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_bgImage == null )
				{
					this.m_EG_bgImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/E_BG_Click/EG_bg");
				}
				return this.m_EG_bgImage;
			}
		}

		public UnityEngine.RectTransform EGDetailRootRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EGDetailRootRectTransform == null )
				{
					this.m_EGDetailRootRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/EGDetailRoot");
				}
				return this.m_EGDetailRootRectTransform;
			}
		}

		public UnityEngine.RectTransform EGDetailScrollRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EGDetailScrollRectTransform == null )
				{
					this.m_EGDetailScrollRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/EGDetailRoot/EGDetailScroll");
				}
				return this.m_EGDetailScrollRectTransform;
			}
		}

		public UnityEngine.UI.ScrollRect EGDetailScrollScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EGDetailScrollScrollRect == null )
				{
					this.m_EGDetailScrollScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.ScrollRect>(this.uiTransform.gameObject, "EGBackGround/Root/EGDetailRoot/EGDetailScroll");
				}
				return this.m_EGDetailScrollScrollRect;
			}
		}

		public UnityEngine.UI.Image EGDetailScrollImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EGDetailScrollImage == null )
				{
					this.m_EGDetailScrollImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/EGDetailRoot/EGDetailScroll");
				}
				return this.m_EGDetailScrollImage;
			}
		}

		public TMPro.TextMeshProUGUI E_TextTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_TextTextMeshProUGUI == null )
				{
					this.m_E_TextTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/Root/EGDetailRoot/EGDetailScroll/Viewport/Content/E_Text");
				}
				return this.m_E_TextTextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI E_TextSimpleTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_TextSimpleTextMeshProUGUI == null )
				{
					this.m_E_TextSimpleTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/Root/EGDetailRoot/E_TextSimple");
				}
				return this.m_E_TextSimpleTextMeshProUGUI;
			}
		}

		public UnityEngine.RectTransform EG_ConfirmRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_ConfirmRectTransform == null )
				{
					this.m_EG_ConfirmRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Confirm");
				}
				return this.m_EG_ConfirmRectTransform;
			}
		}

		public UnityEngine.UI.Button E_ConfirmCancelButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ConfirmCancelButton == null )
				{
					this.m_E_ConfirmCancelButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Confirm/E_ConfirmCancel");
				}
				return this.m_E_ConfirmCancelButton;
			}
		}

		public UnityEngine.UI.Image E_ConfirmCancelImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ConfirmCancelImage == null )
				{
					this.m_E_ConfirmCancelImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Confirm/E_ConfirmCancel");
				}
				return this.m_E_ConfirmCancelImage;
			}
		}

		public TMPro.TextMeshProUGUI E_CancelTextTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_CancelTextTextMeshProUGUI == null )
				{
					this.m_E_CancelTextTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Confirm/E_ConfirmCancel/E_CancelText");
				}
				return this.m_E_CancelTextTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Button E_ConfirmSureButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ConfirmSureButton == null )
				{
					this.m_E_ConfirmSureButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Confirm/E_ConfirmSure");
				}
				return this.m_E_ConfirmSureButton;
			}
		}

		public UnityEngine.UI.Image E_ConfirmSureImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ConfirmSureImage == null )
				{
					this.m_E_ConfirmSureImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Confirm/E_ConfirmSure");
				}
				return this.m_E_ConfirmSureImage;
			}
		}

		public TMPro.TextMeshProUGUI E_SureTextTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_SureTextTextMeshProUGUI == null )
				{
					this.m_E_SureTextTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Confirm/E_ConfirmSure/E_SureText");
				}
				return this.m_E_SureTextTextMeshProUGUI;
			}
		}

		public UnityEngine.RectTransform EG_SureRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_SureRectTransform == null )
				{
					this.m_EG_SureRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Sure");
				}
				return this.m_EG_SureRectTransform;
			}
		}

		public UnityEngine.UI.Button E_SureButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_SureButton == null )
				{
					this.m_E_SureButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Sure/E_Sure");
				}
				return this.m_E_SureButton;
			}
		}

		public UnityEngine.UI.Image E_SureImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_SureImage == null )
				{
					this.m_E_SureImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Sure/E_Sure");
				}
				return this.m_E_SureImage;
			}
		}

		public TMPro.TextMeshProUGUI E_OnlySureTextTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_OnlySureTextTextMeshProUGUI == null )
				{
					this.m_E_OnlySureTextTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Sure/E_Sure/E_OnlySureText");
				}
				return this.m_E_OnlySureTextTextMeshProUGUI;
			}
		}

		public UnityEngine.RectTransform EG_titleRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_titleRectTransform == null )
				{
					this.m_EG_titleRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/EG_title");
				}
				return this.m_EG_titleRectTransform;
			}
		}

		public UnityEngine.UI.Image EG_titleImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_titleImage == null )
				{
					this.m_EG_titleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/EG_title");
				}
				return this.m_EG_titleImage;
			}
		}

		public TMPro.TextMeshProUGUI E_TitleTextTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_TitleTextTextMeshProUGUI == null )
				{
					this.m_E_TitleTextTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/Root/EG_title/E_TitleText");
				}
				return this.m_E_TitleTextTextMeshProUGUI;
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
			this.m_EGBackGroundRectTransform = null;
			this.m_E_BG_ClickButton = null;
			this.m_E_BG_ClickImage = null;
			this.m_EG_bgARRectTransform = null;
			this.m_EG_bgARImage = null;
			this.m_EG_bgRectTransform = null;
			this.m_EG_bgImage = null;
			this.m_EGDetailRootRectTransform = null;
			this.m_EGDetailScrollRectTransform = null;
			this.m_EGDetailScrollScrollRect = null;
			this.m_EGDetailScrollImage = null;
			this.m_E_TextTextMeshProUGUI = null;
			this.m_E_TextSimpleTextMeshProUGUI = null;
			this.m_EG_ConfirmRectTransform = null;
			this.m_E_ConfirmCancelButton = null;
			this.m_E_ConfirmCancelImage = null;
			this.m_E_CancelTextTextMeshProUGUI = null;
			this.m_E_ConfirmSureButton = null;
			this.m_E_ConfirmSureImage = null;
			this.m_E_SureTextTextMeshProUGUI = null;
			this.m_EG_SureRectTransform = null;
			this.m_E_SureButton = null;
			this.m_E_SureImage = null;
			this.m_E_OnlySureTextTextMeshProUGUI = null;
			this.m_EG_titleRectTransform = null;
			this.m_EG_titleImage = null;
			this.m_E_TitleTextTextMeshProUGUI = null;
			this.m_EG_OpenAnimationRectTransform = null;
			this.m_EG_CloseAnimationRectTransform = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EGBackGroundRectTransform = null;
		private UnityEngine.UI.Button m_E_BG_ClickButton = null;
		private UnityEngine.UI.Image m_E_BG_ClickImage = null;
		private UnityEngine.RectTransform m_EG_bgARRectTransform = null;
		private UnityEngine.UI.Image m_EG_bgARImage = null;
		private UnityEngine.RectTransform m_EG_bgRectTransform = null;
		private UnityEngine.UI.Image m_EG_bgImage = null;
		private UnityEngine.RectTransform m_EGDetailRootRectTransform = null;
		private UnityEngine.RectTransform m_EGDetailScrollRectTransform = null;
		private UnityEngine.UI.ScrollRect m_EGDetailScrollScrollRect = null;
		private UnityEngine.UI.Image m_EGDetailScrollImage = null;
		private TMPro.TextMeshProUGUI m_E_TextTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_E_TextSimpleTextMeshProUGUI = null;
		private UnityEngine.RectTransform m_EG_ConfirmRectTransform = null;
		private UnityEngine.UI.Button m_E_ConfirmCancelButton = null;
		private UnityEngine.UI.Image m_E_ConfirmCancelImage = null;
		private TMPro.TextMeshProUGUI m_E_CancelTextTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_E_ConfirmSureButton = null;
		private UnityEngine.UI.Image m_E_ConfirmSureImage = null;
		private TMPro.TextMeshProUGUI m_E_SureTextTextMeshProUGUI = null;
		private UnityEngine.RectTransform m_EG_SureRectTransform = null;
		private UnityEngine.UI.Button m_E_SureButton = null;
		private UnityEngine.UI.Image m_E_SureImage = null;
		private TMPro.TextMeshProUGUI m_E_OnlySureTextTextMeshProUGUI = null;
		private UnityEngine.RectTransform m_EG_titleRectTransform = null;
		private UnityEngine.UI.Image m_EG_titleImage = null;
		private TMPro.TextMeshProUGUI m_E_TitleTextTextMeshProUGUI = null;
		private UnityEngine.RectTransform m_EG_OpenAnimationRectTransform = null;
		private UnityEngine.RectTransform m_EG_CloseAnimationRectTransform = null;
		public Transform uiTransform = null;
	}
}
