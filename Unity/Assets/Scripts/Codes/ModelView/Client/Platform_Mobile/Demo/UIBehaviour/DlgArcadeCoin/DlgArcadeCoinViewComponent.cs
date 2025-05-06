
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgArcadeCoin))]
	[EnableMethod]
	public class DlgArcadeCoinViewComponent : Entity, IAwake, IDestroy
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

		public TMPro.TextMeshProUGUI E_CoinTitleTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_CoinTitleTextMeshProUGUI == null )
				{
					this.m_E_CoinTitleTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/Root/titleBK/E_CoinTitle");
				}
				return this.m_E_CoinTitleTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Button EButton_SubButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_SubButton == null )
				{
					this.m_EButton_SubButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/Root/Operator/EButton_Sub");
				}
				return this.m_EButton_SubButton;
			}
		}

		public UnityEngine.UI.Image EButton_SubImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_SubImage == null )
				{
					this.m_EButton_SubImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/Operator/EButton_Sub");
				}
				return this.m_EButton_SubImage;
			}
		}

		public UnityEngine.UI.Button EButton_AddButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_AddButton == null )
				{
					this.m_EButton_AddButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/Root/Operator/EButton_Add");
				}
				return this.m_EButton_AddButton;
			}
		}

		public UnityEngine.UI.Image EButton_AddImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_AddImage == null )
				{
					this.m_EButton_AddImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/Operator/EButton_Add");
				}
				return this.m_EButton_AddImage;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_CoinNumTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_CoinNumTextMeshProUGUI == null )
				{
					this.m_ELabel_CoinNumTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/Root/Operator/ELabel_CoinNum");
				}
				return this.m_ELabel_CoinNumTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Button EButton_OKButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_OKButton == null )
				{
					this.m_EButton_OKButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/Root/EButton_OK");
				}
				return this.m_EButton_OKButton;
			}
		}

		public UnityEngine.UI.Image EButton_OKImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_OKImage == null )
				{
					this.m_EButton_OKImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/EButton_OK");
				}
				return this.m_EButton_OKImage;
			}
		}

		public TMPro.TextMeshProUGUI Elabel_MsgTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_Elabel_MsgTextMeshProUGUI == null )
				{
					this.m_Elabel_MsgTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/Root/Elabel_Msg");
				}
				return this.m_Elabel_MsgTextMeshProUGUI;
			}
		}

		public UnityEngine.RectTransform EG_PayQRCodeRootRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_PayQRCodeRootRectTransform == null )
				{
					this.m_EG_PayQRCodeRootRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_PayQRCodeRoot");
				}
				return this.m_EG_PayQRCodeRootRectTransform;
			}
		}

		public UnityEngine.UI.Button E_BG_PayQRCodeClickButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BG_PayQRCodeClickButton == null )
				{
					this.m_E_BG_PayQRCodeClickButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG_PayQRCodeRoot/E_BG_PayQRCodeClick");
				}
				return this.m_E_BG_PayQRCodeClickButton;
			}
		}

		public UnityEngine.UI.Image E_BG_PayQRCodeClickImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BG_PayQRCodeClickImage == null )
				{
					this.m_E_BG_PayQRCodeClickImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_PayQRCodeRoot/E_BG_PayQRCodeClick");
				}
				return this.m_E_BG_PayQRCodeClickImage;
			}
		}

		public UnityEngine.UI.RawImage E_PayQRCodeImgRawImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_PayQRCodeImgRawImage == null )
				{
					this.m_E_PayQRCodeImgRawImage = UIFindHelper.FindDeepChild<UnityEngine.UI.RawImage>(this.uiTransform.gameObject, "EG_PayQRCodeRoot/go/E_PayQRCodeImg");
				}
				return this.m_E_PayQRCodeImgRawImage;
			}
		}

		public TMPro.TextMeshProUGUI E_QRCodeTitleTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_QRCodeTitleTextMeshProUGUI == null )
				{
					this.m_E_QRCodeTitleTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_PayQRCodeRoot/go/E_QRCodeTitle");
				}
				return this.m_E_QRCodeTitleTextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI E_QRCodeTextBottomTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_QRCodeTextBottomTextMeshProUGUI == null )
				{
					this.m_E_QRCodeTextBottomTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_PayQRCodeRoot/go/E_QRCodeTextBottom");
				}
				return this.m_E_QRCodeTextBottomTextMeshProUGUI;
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
			this.m_E_CoinTitleTextMeshProUGUI = null;
			this.m_EButton_SubButton = null;
			this.m_EButton_SubImage = null;
			this.m_EButton_AddButton = null;
			this.m_EButton_AddImage = null;
			this.m_ELabel_CoinNumTextMeshProUGUI = null;
			this.m_EButton_OKButton = null;
			this.m_EButton_OKImage = null;
			this.m_Elabel_MsgTextMeshProUGUI = null;
			this.m_EG_PayQRCodeRootRectTransform = null;
			this.m_E_BG_PayQRCodeClickButton = null;
			this.m_E_BG_PayQRCodeClickImage = null;
			this.m_E_PayQRCodeImgRawImage = null;
			this.m_E_QRCodeTitleTextMeshProUGUI = null;
			this.m_E_QRCodeTextBottomTextMeshProUGUI = null;
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
		private TMPro.TextMeshProUGUI m_E_CoinTitleTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_EButton_SubButton = null;
		private UnityEngine.UI.Image m_EButton_SubImage = null;
		private UnityEngine.UI.Button m_EButton_AddButton = null;
		private UnityEngine.UI.Image m_EButton_AddImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_CoinNumTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_EButton_OKButton = null;
		private UnityEngine.UI.Image m_EButton_OKImage = null;
		private TMPro.TextMeshProUGUI m_Elabel_MsgTextMeshProUGUI = null;
		private UnityEngine.RectTransform m_EG_PayQRCodeRootRectTransform = null;
		private UnityEngine.UI.Button m_E_BG_PayQRCodeClickButton = null;
		private UnityEngine.UI.Image m_E_BG_PayQRCodeClickImage = null;
		private UnityEngine.UI.RawImage m_E_PayQRCodeImgRawImage = null;
		private TMPro.TextMeshProUGUI m_E_QRCodeTitleTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_E_QRCodeTextBottomTextMeshProUGUI = null;
		private UnityEngine.RectTransform m_EG_OpenAnimationRectTransform = null;
		private UnityEngine.RectTransform m_EG_CloseAnimationRectTransform = null;
		public Transform uiTransform = null;
	}
}
