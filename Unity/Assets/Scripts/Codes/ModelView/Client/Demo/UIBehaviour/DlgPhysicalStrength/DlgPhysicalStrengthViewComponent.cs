
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgPhysicalStrength))]
	[EnableMethod]
	public class DlgPhysicalStrengthViewComponent : Entity, IAwake, IDestroy
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

		public UnityEngine.UI.Slider E_PhysicalStrengthSlider
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_PhysicalStrengthSlider == null )
				{
					this.m_E_PhysicalStrengthSlider = UIFindHelper.FindDeepChild<UnityEngine.UI.Slider>(this.uiTransform.gameObject, "EGBackGround/PhysicalStrength/Processing/E_PhysicalStrength");
				}
				return this.m_E_PhysicalStrengthSlider;
			}
		}

		public UnityEngine.UI.Image E_PhysicalStrengthImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_PhysicalStrengthImage == null )
				{
					this.m_E_PhysicalStrengthImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/PhysicalStrength/Processing/E_PhysicalStrength");
				}
				return this.m_E_PhysicalStrengthImage;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_PercentageTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_PercentageTextMeshProUGUI == null )
				{
					this.m_ELabel_PercentageTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/PhysicalStrength/Processing/ELabel_Percentage");
				}
				return this.m_ELabel_PercentageTextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_RcoverNumTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_RcoverNumTextMeshProUGUI == null )
				{
					this.m_ELabel_RcoverNumTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/PhysicalStrength/E_info01/ELabel_RcoverNum");
				}
				return this.m_ELabel_RcoverNumTextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_RecoverLeftTImeTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_RecoverLeftTImeTextMeshProUGUI == null )
				{
					this.m_ELabel_RecoverLeftTImeTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/PhysicalStrength/E_info01/ELabel_RecoverLeftTIme");
				}
				return this.m_ELabel_RecoverLeftTImeTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView ELabel_RecoverLeftTImeUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_RecoverLeftTImeUITextLocalizeMonoView == null )
				{
					this.m_ELabel_RecoverLeftTImeUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EGBackGround/PhysicalStrength/E_info01/ELabel_RecoverLeftTIme");
				}
				return this.m_ELabel_RecoverLeftTImeUITextLocalizeMonoView;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_GetPhysicalStrengthNumTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_GetPhysicalStrengthNumTextMeshProUGUI == null )
				{
					this.m_ELabel_GetPhysicalStrengthNumTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/PhysicalStrength/E_info02/ELabel_GetPhysicalStrengthNum");
				}
				return this.m_ELabel_GetPhysicalStrengthNumTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Button EButton_WatchADButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_WatchADButton == null )
				{
					this.m_EButton_WatchADButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/PhysicalStrength/EButton_WatchAD");
				}
				return this.m_EButton_WatchADButton;
			}
		}

		public UnityEngine.UI.Image EButton_WatchADImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_WatchADImage == null )
				{
					this.m_EButton_WatchADImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/PhysicalStrength/EButton_WatchAD");
				}
				return this.m_EButton_WatchADImage;
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
					this.m_E_CancelTextTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/PhysicalStrength/EButton_WatchAD/E_CancelText");
				}
				return this.m_E_CancelTextTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView E_CancelTextUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_CancelTextUITextLocalizeMonoView == null )
				{
					this.m_E_CancelTextUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EGBackGround/PhysicalStrength/EButton_WatchAD/E_CancelText");
				}
				return this.m_E_CancelTextUITextLocalizeMonoView;
			}
		}

		public UnityEngine.UI.Button EButton_CoinButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_CoinButton == null )
				{
					this.m_EButton_CoinButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/PhysicalStrength/EButton_Coin");
				}
				return this.m_EButton_CoinButton;
			}
		}

		public UnityEngine.UI.Image EButton_CoinImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_CoinImage == null )
				{
					this.m_EButton_CoinImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/PhysicalStrength/EButton_Coin");
				}
				return this.m_EButton_CoinImage;
			}
		}

		public TMPro.TextMeshProUGUI E_numberTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_numberTextMeshProUGUI == null )
				{
					this.m_E_numberTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/PhysicalStrength/EButton_Coin/E_number");
				}
				return this.m_E_numberTextMeshProUGUI;
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
					this.m_EG_titleRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/PhysicalStrength/EG_title");
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
					this.m_EG_titleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/PhysicalStrength/EG_title");
				}
				return this.m_EG_titleImage;
			}
		}

		public UnityEngine.UI.Button EButton_CloseButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_CloseButton == null )
				{
					this.m_EButton_CloseButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/PhysicalStrength/EG_title/EButton_Close");
				}
				return this.m_EButton_CloseButton;
			}
		}

		public UnityEngine.UI.Image EButton_CloseImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_CloseImage == null )
				{
					this.m_EButton_CloseImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/PhysicalStrength/EG_title/EButton_Close");
				}
				return this.m_EButton_CloseImage;
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
					this.m_E_TitleTextTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/PhysicalStrength/EG_title/E_TitleText");
				}
				return this.m_E_TitleTextTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView E_TitleTextUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_TitleTextUITextLocalizeMonoView == null )
				{
					this.m_E_TitleTextUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EGBackGround/PhysicalStrength/EG_title/E_TitleText");
				}
				return this.m_E_TitleTextUITextLocalizeMonoView;
			}
		}

		public UnityEngine.UI.Slider E_PhysicalStrength2Slider
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_PhysicalStrength2Slider == null )
				{
					this.m_E_PhysicalStrength2Slider = UIFindHelper.FindDeepChild<UnityEngine.UI.Slider>(this.uiTransform.gameObject, "EGBackGround/PhysicalStrength02/Processing/E_PhysicalStrength2");
				}
				return this.m_E_PhysicalStrength2Slider;
			}
		}

		public UnityEngine.UI.Image E_PhysicalStrength2Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_PhysicalStrength2Image == null )
				{
					this.m_E_PhysicalStrength2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/PhysicalStrength02/Processing/E_PhysicalStrength2");
				}
				return this.m_E_PhysicalStrength2Image;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_Percentage2TextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_Percentage2TextMeshProUGUI == null )
				{
					this.m_ELabel_Percentage2TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/PhysicalStrength02/Processing/ELabel_Percentage2");
				}
				return this.m_ELabel_Percentage2TextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_RcoverNum2TextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_RcoverNum2TextMeshProUGUI == null )
				{
					this.m_ELabel_RcoverNum2TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/PhysicalStrength02/E_info012/ELabel_RcoverNum2");
				}
				return this.m_ELabel_RcoverNum2TextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_RecoverLeftTIme2TextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_RecoverLeftTIme2TextMeshProUGUI == null )
				{
					this.m_ELabel_RecoverLeftTIme2TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/PhysicalStrength02/E_info012/ELabel_RecoverLeftTIme2");
				}
				return this.m_ELabel_RecoverLeftTIme2TextMeshProUGUI;
			}
		}

		public UnityEngine.RectTransform EG_title2RectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_title2RectTransform == null )
				{
					this.m_EG_title2RectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/PhysicalStrength02/EG_title2");
				}
				return this.m_EG_title2RectTransform;
			}
		}

		public UnityEngine.UI.Image EG_title2Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_title2Image == null )
				{
					this.m_EG_title2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/PhysicalStrength02/EG_title2");
				}
				return this.m_EG_title2Image;
			}
		}

		public UnityEngine.UI.Button EButton_Close2Button
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_Close2Button == null )
				{
					this.m_EButton_Close2Button = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/PhysicalStrength02/EG_title2/EButton_Close2");
				}
				return this.m_EButton_Close2Button;
			}
		}

		public UnityEngine.UI.Image EButton_Close2Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_Close2Image == null )
				{
					this.m_EButton_Close2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/PhysicalStrength02/EG_title2/EButton_Close2");
				}
				return this.m_EButton_Close2Image;
			}
		}

		public TMPro.TextMeshProUGUI E_TitleText2TextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_TitleText2TextMeshProUGUI == null )
				{
					this.m_E_TitleText2TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/PhysicalStrength02/EG_title2/E_TitleText2");
				}
				return this.m_E_TitleText2TextMeshProUGUI;
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
			this.m_E_PhysicalStrengthSlider = null;
			this.m_E_PhysicalStrengthImage = null;
			this.m_ELabel_PercentageTextMeshProUGUI = null;
			this.m_ELabel_RcoverNumTextMeshProUGUI = null;
			this.m_ELabel_RecoverLeftTImeTextMeshProUGUI = null;
			this.m_ELabel_RecoverLeftTImeUITextLocalizeMonoView = null;
			this.m_ELabel_GetPhysicalStrengthNumTextMeshProUGUI = null;
			this.m_EButton_WatchADButton = null;
			this.m_EButton_WatchADImage = null;
			this.m_E_CancelTextTextMeshProUGUI = null;
			this.m_E_CancelTextUITextLocalizeMonoView = null;
			this.m_EButton_CoinButton = null;
			this.m_EButton_CoinImage = null;
			this.m_E_numberTextMeshProUGUI = null;
			this.m_EG_titleRectTransform = null;
			this.m_EG_titleImage = null;
			this.m_EButton_CloseButton = null;
			this.m_EButton_CloseImage = null;
			this.m_E_TitleTextTextMeshProUGUI = null;
			this.m_E_TitleTextUITextLocalizeMonoView = null;
			this.m_E_PhysicalStrength2Slider = null;
			this.m_E_PhysicalStrength2Image = null;
			this.m_ELabel_Percentage2TextMeshProUGUI = null;
			this.m_ELabel_RcoverNum2TextMeshProUGUI = null;
			this.m_ELabel_RecoverLeftTIme2TextMeshProUGUI = null;
			this.m_EG_title2RectTransform = null;
			this.m_EG_title2Image = null;
			this.m_EButton_Close2Button = null;
			this.m_EButton_Close2Image = null;
			this.m_E_TitleText2TextMeshProUGUI = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EGBackGroundRectTransform = null;
		private UnityEngine.UI.Button m_E_BG_ClickButton = null;
		private UnityEngine.UI.Image m_E_BG_ClickImage = null;
		private UnityEngine.RectTransform m_EG_bgARRectTransform = null;
		private BlurBackground.TranslucentImage m_EG_bgARTranslucentImage = null;
		private UnityEngine.RectTransform m_EG_bgRectTransform = null;
		private UnityEngine.UI.Image m_EG_bgImage = null;
		private UnityEngine.UI.Slider m_E_PhysicalStrengthSlider = null;
		private UnityEngine.UI.Image m_E_PhysicalStrengthImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_PercentageTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ELabel_RcoverNumTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ELabel_RecoverLeftTImeTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ELabel_RecoverLeftTImeUITextLocalizeMonoView = null;
		private TMPro.TextMeshProUGUI m_ELabel_GetPhysicalStrengthNumTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_EButton_WatchADButton = null;
		private UnityEngine.UI.Image m_EButton_WatchADImage = null;
		private TMPro.TextMeshProUGUI m_E_CancelTextTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_E_CancelTextUITextLocalizeMonoView = null;
		private UnityEngine.UI.Button m_EButton_CoinButton = null;
		private UnityEngine.UI.Image m_EButton_CoinImage = null;
		private TMPro.TextMeshProUGUI m_E_numberTextMeshProUGUI = null;
		private UnityEngine.RectTransform m_EG_titleRectTransform = null;
		private UnityEngine.UI.Image m_EG_titleImage = null;
		private UnityEngine.UI.Button m_EButton_CloseButton = null;
		private UnityEngine.UI.Image m_EButton_CloseImage = null;
		private TMPro.TextMeshProUGUI m_E_TitleTextTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_E_TitleTextUITextLocalizeMonoView = null;
		private UnityEngine.UI.Slider m_E_PhysicalStrength2Slider = null;
		private UnityEngine.UI.Image m_E_PhysicalStrength2Image = null;
		private TMPro.TextMeshProUGUI m_ELabel_Percentage2TextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ELabel_RcoverNum2TextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ELabel_RecoverLeftTIme2TextMeshProUGUI = null;
		private UnityEngine.RectTransform m_EG_title2RectTransform = null;
		private UnityEngine.UI.Image m_EG_title2Image = null;
		private UnityEngine.UI.Button m_EButton_Close2Button = null;
		private UnityEngine.UI.Image m_EButton_Close2Image = null;
		private TMPro.TextMeshProUGUI m_E_TitleText2TextMeshProUGUI = null;
		public Transform uiTransform = null;
	}
}
