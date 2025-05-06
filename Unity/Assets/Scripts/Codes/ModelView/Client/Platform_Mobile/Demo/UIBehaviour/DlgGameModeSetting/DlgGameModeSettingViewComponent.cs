
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgGameModeSetting))]
	[EnableMethod]
	public class DlgGameModeSettingViewComponent : Entity, IAwake, IDestroy
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
					this.m_E_TitleTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Title");
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
					this.m_E_TitleUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Title");
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
					this.m_E_OtherImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Other");
				}
				return this.m_E_OtherImage;
			}
		}

		public UnityEngine.UI.Button E_ButtonTutorialsButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ButtonTutorialsButton == null )
				{
					this.m_E_ButtonTutorialsButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Other/E_ButtonTutorials");
				}
				return this.m_E_ButtonTutorialsButton;
			}
		}

		public UnityEngine.UI.Image E_ButtonTutorialsImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ButtonTutorialsImage == null )
				{
					this.m_E_ButtonTutorialsImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Other/E_ButtonTutorials");
				}
				return this.m_E_ButtonTutorialsImage;
			}
		}

		public UnityEngine.UI.Button E_PrivacyPolicyButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_PrivacyPolicyButton == null )
				{
					this.m_E_PrivacyPolicyButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Other/E_PrivacyPolicy");
				}
				return this.m_E_PrivacyPolicyButton;
			}
		}

		public UnityEngine.UI.Image E_PrivacyPolicyImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_PrivacyPolicyImage == null )
				{
					this.m_E_PrivacyPolicyImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Other/E_PrivacyPolicy");
				}
				return this.m_E_PrivacyPolicyImage;
			}
		}

		public UnityEngine.UI.Button E_DiscordButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_DiscordButton == null )
				{
					this.m_E_DiscordButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Other/E_Discord");
				}
				return this.m_E_DiscordButton;
			}
		}

		public UnityEngine.UI.Image E_DiscordImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_DiscordImage == null )
				{
					this.m_E_DiscordImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Other/E_Discord");
				}
				return this.m_E_DiscordImage;
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
					this.m_EG_Toggle_AudioRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Operator/EG_Toggle_Audio");
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
					this.m_EG_Toggle_AudioImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Operator/EG_Toggle_Audio");
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
					this.m_EG_Audio_OnRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Operator/EG_Toggle_Audio/EG_Audio_On");
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
					this.m_EG_Audio_OnImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Operator/EG_Toggle_Audio/EG_Audio_On");
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
					this.m_EG_Audio_OffRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Operator/EG_Toggle_Audio/EG_Audio_Off");
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
					this.m_EG_Audio_OffImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Operator/EG_Toggle_Audio/EG_Audio_Off");
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
					this.m_EG_Button_AudioRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Operator/EG_Toggle_Audio/EG_Button_Audio");
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
					this.m_EG_Button_AudioImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Operator/EG_Toggle_Audio/EG_Button_Audio");
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
					this.m_EG_Toggle_DamageShowRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Operator/EG_Toggle_DamageShow");
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
					this.m_EG_Toggle_DamageShowImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Operator/EG_Toggle_DamageShow");
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
					this.m_EG_DamageShow_OnRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Operator/EG_Toggle_DamageShow/EG_DamageShow_On");
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
					this.m_EG_DamageShow_OnImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Operator/EG_Toggle_DamageShow/EG_DamageShow_On");
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
					this.m_EG_DamageShow_OffRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Operator/EG_Toggle_DamageShow/EG_DamageShow_Off");
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
					this.m_EG_DamageShow_OffImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Operator/EG_Toggle_DamageShow/EG_DamageShow_Off");
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
					this.m_EG_Button_DamagerShowRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Operator/EG_Toggle_DamageShow/EG_Button_DamagerShow");
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
					this.m_EG_Button_DamagerShowImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Operator/EG_Toggle_DamageShow/EG_Button_DamagerShow");
				}
				return this.m_EG_Button_DamagerShowImage;
			}
		}

		public UnityEngine.RectTransform EG_LanguageRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_LanguageRectTransform == null )
				{
					this.m_EG_LanguageRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Operator/EG_Language");
				}
				return this.m_EG_LanguageRectTransform;
			}
		}

		public UnityEngine.UI.Image EG_LanguageImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_LanguageImage == null )
				{
					this.m_EG_LanguageImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Operator/EG_Language");
				}
				return this.m_EG_LanguageImage;
			}
		}

		public UnityEngine.RectTransform EG_DamageShow_On1RectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_DamageShow_On1RectTransform == null )
				{
					this.m_EG_DamageShow_On1RectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Operator/EG_Language/EG_DamageShow_On1");
				}
				return this.m_EG_DamageShow_On1RectTransform;
			}
		}

		public UnityEngine.UI.Image EG_DamageShow_On1Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_DamageShow_On1Image == null )
				{
					this.m_EG_DamageShow_On1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Operator/EG_Language/EG_DamageShow_On1");
				}
				return this.m_EG_DamageShow_On1Image;
			}
		}

		public UnityEngine.RectTransform EG_DamageShow_Off1RectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_DamageShow_Off1RectTransform == null )
				{
					this.m_EG_DamageShow_Off1RectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Operator/EG_Language/EG_DamageShow_Off1");
				}
				return this.m_EG_DamageShow_Off1RectTransform;
			}
		}

		public UnityEngine.UI.Image EG_DamageShow_Off1Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_DamageShow_Off1Image == null )
				{
					this.m_EG_DamageShow_Off1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Operator/EG_Language/EG_DamageShow_Off1");
				}
				return this.m_EG_DamageShow_Off1Image;
			}
		}

		public UnityEngine.RectTransform EG_Button_DamagerShow1RectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_Button_DamagerShow1RectTransform == null )
				{
					this.m_EG_Button_DamagerShow1RectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Operator/EG_Language/EG_Button_DamagerShow1");
				}
				return this.m_EG_Button_DamagerShow1RectTransform;
			}
		}

		public UnityEngine.UI.Image EG_Button_DamagerShow1Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_Button_DamagerShow1Image == null )
				{
					this.m_EG_Button_DamagerShow1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Operator/EG_Language/EG_Button_DamagerShow1");
				}
				return this.m_EG_Button_DamagerShow1Image;
			}
		}

		public UnityEngine.UI.Button E_btn_LanguageButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_btn_LanguageButton == null )
				{
					this.m_E_btn_LanguageButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Operator/EG_Language/E_btn_Language");
				}
				return this.m_E_btn_LanguageButton;
			}
		}

		public TMPro.TextMeshProUGUI E_LanguageTextTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_LanguageTextTextMeshProUGUI == null )
				{
					this.m_E_LanguageTextTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Operator/EG_Language/E_btn_Language/E_LanguageText");
				}
				return this.m_E_LanguageTextTextMeshProUGUI;
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
					this.m_EG_Toggle_MusicRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Operator/EG_Toggle_Music");
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
					this.m_EG_Toggle_MusicImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Operator/EG_Toggle_Music");
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
					this.m_EG_Music_OnRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Operator/EG_Toggle_Music/EG_Music_On");
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
					this.m_EG_Music_OnImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Operator/EG_Toggle_Music/EG_Music_On");
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
					this.m_EG_Music_OffRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Operator/EG_Toggle_Music/EG_Music_Off");
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
					this.m_EG_Music_OffImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Operator/EG_Toggle_Music/EG_Music_Off");
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
					this.m_EG_Button_MusicRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Operator/EG_Toggle_Music/EG_Button_Music");
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
					this.m_EG_Button_MusicImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/Content/E_Operator/EG_Toggle_Music/EG_Button_Music");
				}
				return this.m_EG_Button_MusicImage;
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
			this.m_E_BtnCloseButton = null;
			this.m_E_BtnCloseImage = null;
			this.m_E_TitleTextMeshProUGUI = null;
			this.m_E_TitleUITextLocalizeMonoView = null;
			this.m_E_OtherImage = null;
			this.m_E_ButtonTutorialsButton = null;
			this.m_E_ButtonTutorialsImage = null;
			this.m_E_PrivacyPolicyButton = null;
			this.m_E_PrivacyPolicyImage = null;
			this.m_E_DiscordButton = null;
			this.m_E_DiscordImage = null;
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
			this.m_EG_LanguageRectTransform = null;
			this.m_EG_LanguageImage = null;
			this.m_EG_DamageShow_On1RectTransform = null;
			this.m_EG_DamageShow_On1Image = null;
			this.m_EG_DamageShow_Off1RectTransform = null;
			this.m_EG_DamageShow_Off1Image = null;
			this.m_EG_Button_DamagerShow1RectTransform = null;
			this.m_EG_Button_DamagerShow1Image = null;
			this.m_E_btn_LanguageButton = null;
			this.m_E_LanguageTextTextMeshProUGUI = null;
			this.m_EG_Toggle_MusicRectTransform = null;
			this.m_EG_Toggle_MusicImage = null;
			this.m_EG_Music_OnRectTransform = null;
			this.m_EG_Music_OnImage = null;
			this.m_EG_Music_OffRectTransform = null;
			this.m_EG_Music_OffImage = null;
			this.m_EG_Button_MusicRectTransform = null;
			this.m_EG_Button_MusicImage = null;
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
		private UnityEngine.UI.Button m_E_BtnCloseButton = null;
		private UnityEngine.UI.Image m_E_BtnCloseImage = null;
		private TMPro.TextMeshProUGUI m_E_TitleTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_E_TitleUITextLocalizeMonoView = null;
		private UnityEngine.UI.Image m_E_OtherImage = null;
		private UnityEngine.UI.Button m_E_ButtonTutorialsButton = null;
		private UnityEngine.UI.Image m_E_ButtonTutorialsImage = null;
		private UnityEngine.UI.Button m_E_PrivacyPolicyButton = null;
		private UnityEngine.UI.Image m_E_PrivacyPolicyImage = null;
		private UnityEngine.UI.Button m_E_DiscordButton = null;
		private UnityEngine.UI.Image m_E_DiscordImage = null;
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
		private UnityEngine.RectTransform m_EG_LanguageRectTransform = null;
		private UnityEngine.UI.Image m_EG_LanguageImage = null;
		private UnityEngine.RectTransform m_EG_DamageShow_On1RectTransform = null;
		private UnityEngine.UI.Image m_EG_DamageShow_On1Image = null;
		private UnityEngine.RectTransform m_EG_DamageShow_Off1RectTransform = null;
		private UnityEngine.UI.Image m_EG_DamageShow_Off1Image = null;
		private UnityEngine.RectTransform m_EG_Button_DamagerShow1RectTransform = null;
		private UnityEngine.UI.Image m_EG_Button_DamagerShow1Image = null;
		private UnityEngine.UI.Button m_E_btn_LanguageButton = null;
		private TMPro.TextMeshProUGUI m_E_LanguageTextTextMeshProUGUI = null;
		private UnityEngine.RectTransform m_EG_Toggle_MusicRectTransform = null;
		private UnityEngine.UI.Image m_EG_Toggle_MusicImage = null;
		private UnityEngine.RectTransform m_EG_Music_OnRectTransform = null;
		private UnityEngine.UI.Image m_EG_Music_OnImage = null;
		private UnityEngine.RectTransform m_EG_Music_OffRectTransform = null;
		private UnityEngine.UI.Image m_EG_Music_OffImage = null;
		private UnityEngine.RectTransform m_EG_Button_MusicRectTransform = null;
		private UnityEngine.UI.Image m_EG_Button_MusicImage = null;
		private UnityEngine.RectTransform m_EG_OpenAnimationRectTransform = null;
		private UnityEngine.RectTransform m_EG_CloseAnimationRectTransform = null;
		public Transform uiTransform = null;
	}
}
