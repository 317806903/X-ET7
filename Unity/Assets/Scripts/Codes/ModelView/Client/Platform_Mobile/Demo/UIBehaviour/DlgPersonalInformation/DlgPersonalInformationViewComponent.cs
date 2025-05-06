
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgPersonalInformation))]
	[EnableMethod]
	public class DlgPersonalInformationViewComponent : Entity, IAwake, IDestroy
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

		public TMPro.TextMeshProUGUI ELabel_IDTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_IDTextMeshProUGUI == null )
				{
					this.m_ELabel_IDTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/Root/ELabel_ID");
				}
				return this.m_ELabel_IDTextMeshProUGUI;
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

		public UnityEngine.UI.Button E_LogoutButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_LogoutButton == null )
				{
					this.m_E_LogoutButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Confirm/E_Logout");
				}
				return this.m_E_LogoutButton;
			}
		}

		public UnityEngine.UI.Image E_LogoutImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_LogoutImage == null )
				{
					this.m_E_LogoutImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Confirm/E_Logout");
				}
				return this.m_E_LogoutImage;
			}
		}

		public UnityEngine.RectTransform EG_AccountRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_AccountRectTransform == null )
				{
					this.m_EG_AccountRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Account");
				}
				return this.m_EG_AccountRectTransform;
			}
		}

		public UnityEngine.UI.Button E_Logout_SdkButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Logout_SdkButton == null )
				{
					this.m_E_Logout_SdkButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Account/E_Logout_Sdk");
				}
				return this.m_E_Logout_SdkButton;
			}
		}

		public UnityEngine.UI.Image E_Logout_SdkImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Logout_SdkImage == null )
				{
					this.m_E_Logout_SdkImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Account/E_Logout_Sdk");
				}
				return this.m_E_Logout_SdkImage;
			}
		}

		public UnityEngine.UI.Button E_AccountButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_AccountButton == null )
				{
					this.m_E_AccountButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Account/E_Account");
				}
				return this.m_E_AccountButton;
			}
		}

		public UnityEngine.UI.Image E_AccountImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_AccountImage == null )
				{
					this.m_E_AccountImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Account/E_Account");
				}
				return this.m_E_AccountImage;
			}
		}

		public TMPro.TextMeshProUGUI E_Account_TextTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Account_TextTextMeshProUGUI == null )
				{
					this.m_E_Account_TextTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Account/E_Account/E_Account_Text");
				}
				return this.m_E_Account_TextTextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI E_Account_TitleTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Account_TitleTextMeshProUGUI == null )
				{
					this.m_E_Account_TitleTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Account/E_Account/E_Account_Title");
				}
				return this.m_E_Account_TitleTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView E_Account_TitleUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Account_TitleUITextLocalizeMonoView == null )
				{
					this.m_E_Account_TitleUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Account/E_Account/E_Account_Title");
				}
				return this.m_E_Account_TitleUITextLocalizeMonoView;
			}
		}

		public UnityEngine.UI.Button E_GoogleLoginButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_GoogleLoginButton == null )
				{
					this.m_E_GoogleLoginButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Account/E_GoogleLogin");
				}
				return this.m_E_GoogleLoginButton;
			}
		}

		public UnityEngine.UI.Image E_GoogleLoginImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_GoogleLoginImage == null )
				{
					this.m_E_GoogleLoginImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Account/E_GoogleLogin");
				}
				return this.m_E_GoogleLoginImage;
			}
		}

		public UnityEngine.UI.Button E_IphoneLoginButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_IphoneLoginButton == null )
				{
					this.m_E_IphoneLoginButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Account/E_IphoneLogin");
				}
				return this.m_E_IphoneLoginButton;
			}
		}

		public UnityEngine.UI.Image E_IphoneLoginImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_IphoneLoginImage == null )
				{
					this.m_E_IphoneLoginImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/EG_Account/E_IphoneLogin");
				}
				return this.m_E_IphoneLoginImage;
			}
		}

		public UnityEngine.UI.Button EBtn_ChgNameButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EBtn_ChgNameButton == null )
				{
					this.m_EBtn_ChgNameButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/Root/E_info/name/EBtn_ChgName");
				}
				return this.m_EBtn_ChgNameButton;
			}
		}

		public UnityEngine.UI.Image EBtn_ChgNameImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EBtn_ChgNameImage == null )
				{
					this.m_EBtn_ChgNameImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/E_info/name/EBtn_ChgName");
				}
				return this.m_EBtn_ChgNameImage;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_NameTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_NameTextMeshProUGUI == null )
				{
					this.m_ELabel_NameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/Root/E_info/name/ELabel_Name");
				}
				return this.m_ELabel_NameTextMeshProUGUI;
			}
		}

		public ES_AvatarShow ES_AvatarShow
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_es_avatarshow == null )
				{
					Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject, "EGBackGround/Root/E_info/Avatar/ES_AvatarShow");
					this.m_es_avatarshow = this.AddChild<ES_AvatarShow, Transform>(subTrans);
				}
				return this.m_es_avatarshow;
			}
		}

		public UnityEngine.UI.Button EBtn_ChgAvatarButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EBtn_ChgAvatarButton == null )
				{
					this.m_EBtn_ChgAvatarButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/Root/E_info/Avatar/EBtn_ChgAvatar");
				}
				return this.m_EBtn_ChgAvatarButton;
			}
		}

		public UnityEngine.UI.Image EBtn_ChgAvatarImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EBtn_ChgAvatarImage == null )
				{
					this.m_EBtn_ChgAvatarImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/Root/E_info/Avatar/EBtn_ChgAvatar");
				}
				return this.m_EBtn_ChgAvatarImage;
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
					this.m_E_BtnCloseButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/E_BtnClose");
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
					this.m_E_BtnCloseImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/E_BtnClose");
				}
				return this.m_E_BtnCloseImage;
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
			this.m_ELabel_IDTextMeshProUGUI = null;
			this.m_EG_ConfirmRectTransform = null;
			this.m_E_LogoutButton = null;
			this.m_E_LogoutImage = null;
			this.m_EG_AccountRectTransform = null;
			this.m_E_Logout_SdkButton = null;
			this.m_E_Logout_SdkImage = null;
			this.m_E_AccountButton = null;
			this.m_E_AccountImage = null;
			this.m_E_Account_TextTextMeshProUGUI = null;
			this.m_E_Account_TitleTextMeshProUGUI = null;
			this.m_E_Account_TitleUITextLocalizeMonoView = null;
			this.m_E_GoogleLoginButton = null;
			this.m_E_GoogleLoginImage = null;
			this.m_E_IphoneLoginButton = null;
			this.m_E_IphoneLoginImage = null;
			this.m_EBtn_ChgNameButton = null;
			this.m_EBtn_ChgNameImage = null;
			this.m_ELabel_NameTextMeshProUGUI = null;
			this.m_es_avatarshow?.Dispose();
			this.m_es_avatarshow = null;
			this.m_EBtn_ChgAvatarButton = null;
			this.m_EBtn_ChgAvatarImage = null;
			this.m_EG_titleRectTransform = null;
			this.m_EG_titleImage = null;
			this.m_E_BtnCloseButton = null;
			this.m_E_BtnCloseImage = null;
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
		private TMPro.TextMeshProUGUI m_ELabel_IDTextMeshProUGUI = null;
		private UnityEngine.RectTransform m_EG_ConfirmRectTransform = null;
		private UnityEngine.UI.Button m_E_LogoutButton = null;
		private UnityEngine.UI.Image m_E_LogoutImage = null;
		private UnityEngine.RectTransform m_EG_AccountRectTransform = null;
		private UnityEngine.UI.Button m_E_Logout_SdkButton = null;
		private UnityEngine.UI.Image m_E_Logout_SdkImage = null;
		private UnityEngine.UI.Button m_E_AccountButton = null;
		private UnityEngine.UI.Image m_E_AccountImage = null;
		private TMPro.TextMeshProUGUI m_E_Account_TextTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_E_Account_TitleTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_E_Account_TitleUITextLocalizeMonoView = null;
		private UnityEngine.UI.Button m_E_GoogleLoginButton = null;
		private UnityEngine.UI.Image m_E_GoogleLoginImage = null;
		private UnityEngine.UI.Button m_E_IphoneLoginButton = null;
		private UnityEngine.UI.Image m_E_IphoneLoginImage = null;
		private UnityEngine.UI.Button m_EBtn_ChgNameButton = null;
		private UnityEngine.UI.Image m_EBtn_ChgNameImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_NameTextMeshProUGUI = null;
		private ES_AvatarShow m_es_avatarshow = null;
		private UnityEngine.UI.Button m_EBtn_ChgAvatarButton = null;
		private UnityEngine.UI.Image m_EBtn_ChgAvatarImage = null;
		private UnityEngine.RectTransform m_EG_titleRectTransform = null;
		private UnityEngine.UI.Image m_EG_titleImage = null;
		private UnityEngine.UI.Button m_E_BtnCloseButton = null;
		private UnityEngine.UI.Image m_E_BtnCloseImage = null;
		private UnityEngine.RectTransform m_EG_OpenAnimationRectTransform = null;
		private UnityEngine.RectTransform m_EG_CloseAnimationRectTransform = null;
		public Transform uiTransform = null;
	}
}
