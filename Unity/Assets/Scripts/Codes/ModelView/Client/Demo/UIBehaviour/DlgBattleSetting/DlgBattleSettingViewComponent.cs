
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgBattleSetting))]
	[EnableMethod]
	public class DlgBattleSettingViewComponent : Entity, IAwake, IDestroy
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

		public UnityEngine.UI.Button E_BtnCloseButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BtnCloseButton == null )
				{
					this.m_E_BtnCloseButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/Root/E_BtnClose");
				}
				return this.m_E_BtnCloseButton;
			}
		}

		public UnityEngine.UI.Image E_BtnCloseImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BtnCloseImage == null )
				{
					this.m_E_BtnCloseImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/E_BtnClose");
				}
				return this.m_E_BtnCloseImage;
			}
		}

		public TMPro.TextMeshProUGUI E_TitleTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_TitleTextMeshProUGUI == null )
				{
					this.m_E_TitleTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/Root/E_Title");
				}
				return this.m_E_TitleTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView E_TitleUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_TitleUITextLocalizeMonoView == null )
				{
					this.m_E_TitleUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EGBackGround/Root/E_Title");
				}
				return this.m_E_TitleUITextLocalizeMonoView;
			}
		}

		public UnityEngine.UI.Image E_OtherImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_OtherImage == null )
				{
					this.m_E_OtherImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/E_Other");
				}
				return this.m_E_OtherImage;
			}
		}

		public UnityEngine.UI.Button E_ReScanButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ReScanButton == null )
				{
					this.m_E_ReScanButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/Root/E_Other/E_ReScan");
				}
				return this.m_E_ReScanButton;
			}
		}

		public UnityEngine.UI.Image E_ReScanImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ReScanImage == null )
				{
					this.m_E_ReScanImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/E_Other/E_ReScan");
				}
				return this.m_E_ReScanImage;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_ReScanTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_ReScanTextMeshProUGUI == null )
				{
					this.m_ELabel_ReScanTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/Root/E_Other/E_ReScan/ELabel_ReScan");
				}
				return this.m_ELabel_ReScanTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Button E_QuitBattleButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_QuitBattleButton == null )
				{
					this.m_E_QuitBattleButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/Root/E_Other/E_QuitBattle");
				}
				return this.m_E_QuitBattleButton;
			}
		}

		public UnityEngine.UI.Image E_QuitBattleImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_QuitBattleImage == null )
				{
					this.m_E_QuitBattleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/E_Other/E_QuitBattle");
				}
				return this.m_E_QuitBattleImage;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_QuitBattleTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_QuitBattleTextMeshProUGUI == null )
				{
					this.m_ELabel_QuitBattleTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/Root/E_Other/E_QuitBattle/ELabel_QuitBattle");
				}
				return this.m_ELabel_QuitBattleTextMeshProUGUI;
			}
		}

		public UnityEngine.RectTransform EG_Toggle_AudioRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_Toggle_AudioRectTransform == null )
				{
					this.m_EG_Toggle_AudioRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/E_Operator/EG_Toggle_Audio");
				}
				return this.m_EG_Toggle_AudioRectTransform;
			}
		}

		public UnityEngine.UI.Image EG_Toggle_AudioImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_Toggle_AudioImage == null )
				{
					this.m_EG_Toggle_AudioImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/E_Operator/EG_Toggle_Audio");
				}
				return this.m_EG_Toggle_AudioImage;
			}
		}

		public UnityEngine.RectTransform EG_Audio_OnRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_Audio_OnRectTransform == null )
				{
					this.m_EG_Audio_OnRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/E_Operator/EG_Toggle_Audio/EG_Audio_On");
				}
				return this.m_EG_Audio_OnRectTransform;
			}
		}

		public UnityEngine.UI.Image EG_Audio_OnImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_Audio_OnImage == null )
				{
					this.m_EG_Audio_OnImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/E_Operator/EG_Toggle_Audio/EG_Audio_On");
				}
				return this.m_EG_Audio_OnImage;
			}
		}

		public UnityEngine.RectTransform EG_Audio_OffRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_Audio_OffRectTransform == null )
				{
					this.m_EG_Audio_OffRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/E_Operator/EG_Toggle_Audio/EG_Audio_Off");
				}
				return this.m_EG_Audio_OffRectTransform;
			}
		}

		public UnityEngine.UI.Image EG_Audio_OffImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_Audio_OffImage == null )
				{
					this.m_EG_Audio_OffImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/E_Operator/EG_Toggle_Audio/EG_Audio_Off");
				}
				return this.m_EG_Audio_OffImage;
			}
		}

		public UnityEngine.RectTransform EG_Button_AudioRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_Button_AudioRectTransform == null )
				{
					this.m_EG_Button_AudioRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/E_Operator/EG_Toggle_Audio/EG_Button_Audio");
				}
				return this.m_EG_Button_AudioRectTransform;
			}
		}

		public UnityEngine.UI.Image EG_Button_AudioImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_Button_AudioImage == null )
				{
					this.m_EG_Button_AudioImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/E_Operator/EG_Toggle_Audio/EG_Button_Audio");
				}
				return this.m_EG_Button_AudioImage;
			}
		}

		public UnityEngine.RectTransform EG_Toggle_DamageShowRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_Toggle_DamageShowRectTransform == null )
				{
					this.m_EG_Toggle_DamageShowRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/E_Operator/EG_Toggle_DamageShow");
				}
				return this.m_EG_Toggle_DamageShowRectTransform;
			}
		}

		public UnityEngine.UI.Image EG_Toggle_DamageShowImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_Toggle_DamageShowImage == null )
				{
					this.m_EG_Toggle_DamageShowImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/E_Operator/EG_Toggle_DamageShow");
				}
				return this.m_EG_Toggle_DamageShowImage;
			}
		}

		public UnityEngine.RectTransform EG_DamageShow_OnRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_DamageShow_OnRectTransform == null )
				{
					this.m_EG_DamageShow_OnRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/E_Operator/EG_Toggle_DamageShow/EG_DamageShow_On");
				}
				return this.m_EG_DamageShow_OnRectTransform;
			}
		}

		public UnityEngine.UI.Image EG_DamageShow_OnImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_DamageShow_OnImage == null )
				{
					this.m_EG_DamageShow_OnImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/E_Operator/EG_Toggle_DamageShow/EG_DamageShow_On");
				}
				return this.m_EG_DamageShow_OnImage;
			}
		}

		public UnityEngine.RectTransform EG_DamageShow_OffRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_DamageShow_OffRectTransform == null )
				{
					this.m_EG_DamageShow_OffRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/E_Operator/EG_Toggle_DamageShow/EG_DamageShow_Off");
				}
				return this.m_EG_DamageShow_OffRectTransform;
			}
		}

		public UnityEngine.UI.Image EG_DamageShow_OffImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_DamageShow_OffImage == null )
				{
					this.m_EG_DamageShow_OffImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/E_Operator/EG_Toggle_DamageShow/EG_DamageShow_Off");
				}
				return this.m_EG_DamageShow_OffImage;
			}
		}

		public UnityEngine.RectTransform EG_Button_DamagerShowRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_Button_DamagerShowRectTransform == null )
				{
					this.m_EG_Button_DamagerShowRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/E_Operator/EG_Toggle_DamageShow/EG_Button_DamagerShow");
				}
				return this.m_EG_Button_DamagerShowRectTransform;
			}
		}

		public UnityEngine.UI.Image EG_Button_DamagerShowImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_Button_DamagerShowImage == null )
				{
					this.m_EG_Button_DamagerShowImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/E_Operator/EG_Toggle_DamageShow/EG_Button_DamagerShow");
				}
				return this.m_EG_Button_DamagerShowImage;
			}
		}

		public UnityEngine.RectTransform EG_Toggle_MusicRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_Toggle_MusicRectTransform == null )
				{
					this.m_EG_Toggle_MusicRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/E_Operator/EG_Toggle_Music");
				}
				return this.m_EG_Toggle_MusicRectTransform;
			}
		}

		public UnityEngine.UI.Image EG_Toggle_MusicImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_Toggle_MusicImage == null )
				{
					this.m_EG_Toggle_MusicImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/E_Operator/EG_Toggle_Music");
				}
				return this.m_EG_Toggle_MusicImage;
			}
		}

		public UnityEngine.RectTransform EG_Music_OnRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_Music_OnRectTransform == null )
				{
					this.m_EG_Music_OnRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/E_Operator/EG_Toggle_Music/EG_Music_On");
				}
				return this.m_EG_Music_OnRectTransform;
			}
		}

		public UnityEngine.UI.Image EG_Music_OnImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_Music_OnImage == null )
				{
					this.m_EG_Music_OnImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/E_Operator/EG_Toggle_Music/EG_Music_On");
				}
				return this.m_EG_Music_OnImage;
			}
		}

		public UnityEngine.RectTransform EG_Music_OffRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_Music_OffRectTransform == null )
				{
					this.m_EG_Music_OffRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/E_Operator/EG_Toggle_Music/EG_Music_Off");
				}
				return this.m_EG_Music_OffRectTransform;
			}
		}

		public UnityEngine.UI.Image EG_Music_OffImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_Music_OffImage == null )
				{
					this.m_EG_Music_OffImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/E_Operator/EG_Toggle_Music/EG_Music_Off");
				}
				return this.m_EG_Music_OffImage;
			}
		}

		public UnityEngine.RectTransform EG_Button_MusicRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_Button_MusicRectTransform == null )
				{
					this.m_EG_Button_MusicRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/E_Operator/EG_Toggle_Music/EG_Button_Music");
				}
				return this.m_EG_Button_MusicRectTransform;
			}
		}

		public UnityEngine.UI.Image EG_Button_MusicImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_Button_MusicImage == null )
				{
					this.m_EG_Button_MusicImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/E_Operator/EG_Toggle_Music/EG_Button_Music");
				}
				return this.m_EG_Button_MusicImage;
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
			this.m_E_BtnCloseButton = null;
			this.m_E_BtnCloseImage = null;
			this.m_E_TitleTextMeshProUGUI = null;
			this.m_E_TitleUITextLocalizeMonoView = null;
			this.m_E_OtherImage = null;
			this.m_E_ReScanButton = null;
			this.m_E_ReScanImage = null;
			this.m_ELabel_ReScanTextMeshProUGUI = null;
			this.m_E_QuitBattleButton = null;
			this.m_E_QuitBattleImage = null;
			this.m_ELabel_QuitBattleTextMeshProUGUI = null;
			this.m_EG_Toggle_AudioRectTransform = null;
			this.m_EG_Toggle_AudioImage = null;
			this.m_EG_Audio_OnRectTransform = null;
			this.m_EG_Audio_OnImage = null;
			this.m_EG_Audio_OffRectTransform = null;
			this.m_EG_Audio_OffImage = null;
			this.m_EG_Button_AudioRectTransform = null;
			this.m_EG_Button_AudioImage = null;
			this.m_EG_Toggle_DamageShowRectTransform = null;
			this.m_EG_Toggle_DamageShowImage = null;
			this.m_EG_DamageShow_OnRectTransform = null;
			this.m_EG_DamageShow_OnImage = null;
			this.m_EG_DamageShow_OffRectTransform = null;
			this.m_EG_DamageShow_OffImage = null;
			this.m_EG_Button_DamagerShowRectTransform = null;
			this.m_EG_Button_DamagerShowImage = null;
			this.m_EG_Toggle_MusicRectTransform = null;
			this.m_EG_Toggle_MusicImage = null;
			this.m_EG_Music_OnRectTransform = null;
			this.m_EG_Music_OnImage = null;
			this.m_EG_Music_OffRectTransform = null;
			this.m_EG_Music_OffImage = null;
			this.m_EG_Button_MusicRectTransform = null;
			this.m_EG_Button_MusicImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EGBackGroundRectTransform = null;
		private UnityEngine.UI.Button m_E_BG_ClickButton = null;
		private UnityEngine.UI.Image m_E_BG_ClickImage = null;
		private UnityEngine.RectTransform m_EG_bgARRectTransform = null;
		private BlurBackground.TranslucentImage m_EG_bgARTranslucentImage = null;
		private UnityEngine.RectTransform m_EG_bgRectTransform = null;
		private UnityEngine.UI.Image m_EG_bgImage = null;
		private UnityEngine.UI.Button m_E_BtnCloseButton = null;
		private UnityEngine.UI.Image m_E_BtnCloseImage = null;
		private TMPro.TextMeshProUGUI m_E_TitleTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_E_TitleUITextLocalizeMonoView = null;
		private UnityEngine.UI.Image m_E_OtherImage = null;
		private UnityEngine.UI.Button m_E_ReScanButton = null;
		private UnityEngine.UI.Image m_E_ReScanImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_ReScanTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_E_QuitBattleButton = null;
		private UnityEngine.UI.Image m_E_QuitBattleImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_QuitBattleTextMeshProUGUI = null;
		private UnityEngine.RectTransform m_EG_Toggle_AudioRectTransform = null;
		private UnityEngine.UI.Image m_EG_Toggle_AudioImage = null;
		private UnityEngine.RectTransform m_EG_Audio_OnRectTransform = null;
		private UnityEngine.UI.Image m_EG_Audio_OnImage = null;
		private UnityEngine.RectTransform m_EG_Audio_OffRectTransform = null;
		private UnityEngine.UI.Image m_EG_Audio_OffImage = null;
		private UnityEngine.RectTransform m_EG_Button_AudioRectTransform = null;
		private UnityEngine.UI.Image m_EG_Button_AudioImage = null;
		private UnityEngine.RectTransform m_EG_Toggle_DamageShowRectTransform = null;
		private UnityEngine.UI.Image m_EG_Toggle_DamageShowImage = null;
		private UnityEngine.RectTransform m_EG_DamageShow_OnRectTransform = null;
		private UnityEngine.UI.Image m_EG_DamageShow_OnImage = null;
		private UnityEngine.RectTransform m_EG_DamageShow_OffRectTransform = null;
		private UnityEngine.UI.Image m_EG_DamageShow_OffImage = null;
		private UnityEngine.RectTransform m_EG_Button_DamagerShowRectTransform = null;
		private UnityEngine.UI.Image m_EG_Button_DamagerShowImage = null;
		private UnityEngine.RectTransform m_EG_Toggle_MusicRectTransform = null;
		private UnityEngine.UI.Image m_EG_Toggle_MusicImage = null;
		private UnityEngine.RectTransform m_EG_Music_OnRectTransform = null;
		private UnityEngine.UI.Image m_EG_Music_OnImage = null;
		private UnityEngine.RectTransform m_EG_Music_OffRectTransform = null;
		private UnityEngine.UI.Image m_EG_Music_OffImage = null;
		private UnityEngine.RectTransform m_EG_Button_MusicRectTransform = null;
		private UnityEngine.UI.Image m_EG_Button_MusicImage = null;
		public Transform uiTransform = null;
	}
}
