﻿
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgCommonChoose))]
	[EnableMethod]
	public class DlgCommonChooseViewComponent : Entity, IAwake, IDestroy
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

		public BlurBackground.TranslucentImage EG_bgARTranslucentImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_bgARTranslucentImage == null )
				{
					this.m_EG_bgARTranslucentImage = UIFindHelper.FindDeepChild<BlurBackground.TranslucentImage>(this.uiTransform.gameObject, "EGBackGround/E_BG_Click/EG_bgAR");
				}
				return this.m_EG_bgARTranslucentImage;
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
					this.m_E_TextTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/Root/Scroll View/Viewport/Content/E_Text");
				}
				return this.m_E_TextTextMeshProUGUI;
			}
		}

		public UnityEngine.RectTransform EG_ChooseRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_ChooseRectTransform == null )
				{
					this.m_EG_ChooseRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Choose");
				}
				return this.m_EG_ChooseRectTransform;
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
					this.m_E_ConfirmCancelButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Choose/E_ConfirmCancel");
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
					this.m_E_ConfirmCancelImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Choose/E_ConfirmCancel");
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
					this.m_E_CancelTextTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Choose/E_ConfirmCancel/E_CancelText");
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
					this.m_E_ConfirmSureButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Choose/E_ConfirmSure");
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
					this.m_E_ConfirmSureImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Choose/E_ConfirmSure");
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
					this.m_E_SureTextTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Choose/E_ConfirmSure/E_SureText");
				}
				return this.m_E_SureTextTextMeshProUGUI;
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

		public TMPro.TextMeshProUGUI E_LimitTimeTextTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_LimitTimeTextTextMeshProUGUI == null )
				{
					this.m_E_LimitTimeTextTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/Root/E_LimitTimeText");
				}
				return this.m_E_LimitTimeTextTextMeshProUGUI;
			}
		}

		public void DestroyWidget()
		{
			this.m_EGBackGroundRectTransform = null;
			this.m_E_BG_ClickButton = null;
			this.m_E_BG_ClickImage = null;
			this.m_EG_bgARRectTransform = null;
			this.m_EG_bgARTranslucentImage = null;
			this.m_EG_bgRectTransform = null;
			this.m_EG_bgImage = null;
			this.m_E_TextTextMeshProUGUI = null;
			this.m_EG_ChooseRectTransform = null;
			this.m_E_ConfirmCancelButton = null;
			this.m_E_ConfirmCancelImage = null;
			this.m_E_CancelTextTextMeshProUGUI = null;
			this.m_E_ConfirmSureButton = null;
			this.m_E_ConfirmSureImage = null;
			this.m_E_SureTextTextMeshProUGUI = null;
			this.m_EG_titleRectTransform = null;
			this.m_EG_titleImage = null;
			this.m_E_TitleTextTextMeshProUGUI = null;
			this.m_E_LimitTimeTextTextMeshProUGUI = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EGBackGroundRectTransform = null;
		private UnityEngine.UI.Button m_E_BG_ClickButton = null;
		private UnityEngine.UI.Image m_E_BG_ClickImage = null;
		private UnityEngine.RectTransform m_EG_bgARRectTransform = null;
		private BlurBackground.TranslucentImage m_EG_bgARTranslucentImage = null;
		private UnityEngine.RectTransform m_EG_bgRectTransform = null;
		private UnityEngine.UI.Image m_EG_bgImage = null;
		private TMPro.TextMeshProUGUI m_E_TextTextMeshProUGUI = null;
		private UnityEngine.RectTransform m_EG_ChooseRectTransform = null;
		private UnityEngine.UI.Button m_E_ConfirmCancelButton = null;
		private UnityEngine.UI.Image m_E_ConfirmCancelImage = null;
		private TMPro.TextMeshProUGUI m_E_CancelTextTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_E_ConfirmSureButton = null;
		private UnityEngine.UI.Image m_E_ConfirmSureImage = null;
		private TMPro.TextMeshProUGUI m_E_SureTextTextMeshProUGUI = null;
		private UnityEngine.RectTransform m_EG_titleRectTransform = null;
		private UnityEngine.UI.Image m_EG_titleImage = null;
		private TMPro.TextMeshProUGUI m_E_TitleTextTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_E_LimitTimeTextTextMeshProUGUI = null;
		public Transform uiTransform = null;
	}
}