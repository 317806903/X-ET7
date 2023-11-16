
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgGameModeAR))]
	[EnableMethod]
	public class DlgGameModeARViewComponent : Entity, IAwake, IDestroy
	{
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
					this.m_EG_bgARRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "E_HomePage/BG/EG_bgAR");
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
					this.m_EG_bgARTranslucentImage = UIFindHelper.FindDeepChild<BlurBackground.TranslucentImage>(this.uiTransform.gameObject, "E_HomePage/BG/EG_bgAR");
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
					this.m_EG_bgRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "E_HomePage/BG/EG_bg");
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
					this.m_EG_bgImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/BG/EG_bg");
				}
				return this.m_EG_bgImage;
			}
		}

		public UnityEngine.UI.Button E_PVEButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_PVEButton == null )
				{
					this.m_E_PVEButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/E_PVE");
				}
				return this.m_E_PVEButton;
			}
		}

		public UnityEngine.UI.Image E_PVEImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_PVEImage == null )
				{
					this.m_E_PVEImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/E_PVE");
				}
				return this.m_E_PVEImage;
			}
		}

		public TMPro.TextMeshProUGUI E_PVENameTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_PVENameTextMeshProUGUI == null )
				{
					this.m_E_PVENameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/E_PVE/E_PVEName");
				}
				return this.m_E_PVENameTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView E_PVENameUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_PVENameUITextLocalizeMonoView == null )
				{
					this.m_E_PVENameUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/E_PVE/E_PVEName");
				}
				return this.m_E_PVENameUITextLocalizeMonoView;
			}
		}

		public UnityEngine.UI.Button E_PVPButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_PVPButton == null )
				{
					this.m_E_PVPButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/E_PVP");
				}
				return this.m_E_PVPButton;
			}
		}

		public UnityEngine.UI.Image E_PVPImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_PVPImage == null )
				{
					this.m_E_PVPImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/E_PVP");
				}
				return this.m_E_PVPImage;
			}
		}

		public UnityEngine.UI.Button E_ScanCodeButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ScanCodeButton == null )
				{
					this.m_E_ScanCodeButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_play/E_join/E_ScanCode");
				}
				return this.m_E_ScanCodeButton;
			}
		}

		public UnityEngine.UI.Image E_ScanCodeImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ScanCodeImage == null )
				{
					this.m_E_ScanCodeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_play/E_join/E_ScanCode");
				}
				return this.m_E_ScanCodeImage;
			}
		}

		public UnityEngine.UI.Button E_AvatarButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_AvatarButton == null )
				{
					this.m_E_AvatarButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_Avatar");
				}
				return this.m_E_AvatarButton;
			}
		}

		public UnityEngine.UI.Image E_AvatarImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_AvatarImage == null )
				{
					this.m_E_AvatarImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_Avatar");
				}
				return this.m_E_AvatarImage;
			}
		}

		public UnityEngine.UI.Image E_PlayerIcoImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_PlayerIcoImage == null )
				{
					this.m_E_PlayerIcoImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_Avatar/Avatar/E_PlayerIco");
				}
				return this.m_E_PlayerIcoImage;
			}
		}

		public TMPro.TextMeshProUGUI E_PlayerNameTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_PlayerNameTextMeshProUGUI == null )
				{
					this.m_E_PlayerNameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_HomePage/E_Avatar/E_PlayerName");
				}
				return this.m_E_PlayerNameTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Button E_BackpackButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BackpackButton == null )
				{
					this.m_E_BackpackButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_Function/E_Backpack");
				}
				return this.m_E_BackpackButton;
			}
		}

		public UnityEngine.UI.Image E_BackpackImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BackpackImage == null )
				{
					this.m_E_BackpackImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_Function/E_Backpack");
				}
				return this.m_E_BackpackImage;
			}
		}

		public UnityEngine.UI.Button E_SetUpButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_SetUpButton == null )
				{
					this.m_E_SetUpButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_Function/E_SetUp");
				}
				return this.m_E_SetUpButton;
			}
		}

		public UnityEngine.UI.Image E_SetUpImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_SetUpImage == null )
				{
					this.m_E_SetUpImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_Function/E_SetUp");
				}
				return this.m_E_SetUpImage;
			}
		}

		public UnityEngine.UI.Button E_tutorialButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_tutorialButton == null )
				{
					this.m_E_tutorialButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_Function/E_tutorial");
				}
				return this.m_E_tutorialButton;
			}
		}

		public BlurBackground.TranslucentImage E_tutorialTranslucentImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_tutorialTranslucentImage == null )
				{
					this.m_E_tutorialTranslucentImage = UIFindHelper.FindDeepChild<BlurBackground.TranslucentImage>(this.uiTransform.gameObject, "E_HomePage/E_Function/E_tutorial");
				}
				return this.m_E_tutorialTranslucentImage;
			}
		}

		public UnityEngine.UI.Button E_RankButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_RankButton == null )
				{
					this.m_E_RankButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_Function/E_Rank");
				}
				return this.m_E_RankButton;
			}
		}

		public BlurBackground.TranslucentImage E_RankTranslucentImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_RankTranslucentImage == null )
				{
					this.m_E_RankTranslucentImage = UIFindHelper.FindDeepChild<BlurBackground.TranslucentImage>(this.uiTransform.gameObject, "E_HomePage/E_Function/E_Rank");
				}
				return this.m_E_RankTranslucentImage;
			}
		}

		public UnityEngine.UI.Button E_ReturnLoginButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ReturnLoginButton == null )
				{
					this.m_E_ReturnLoginButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_ReturnLogin");
				}
				return this.m_E_ReturnLoginButton;
			}
		}

		public UnityEngine.UI.Image E_ReturnLoginImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ReturnLoginImage == null )
				{
					this.m_E_ReturnLoginImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_ReturnLogin");
				}
				return this.m_E_ReturnLoginImage;
			}
		}

		public UnityEngine.UI.Text E_ReturnTextText
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ReturnTextText == null )
				{
					this.m_E_ReturnTextText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "E_HomePage/E_ReturnLogin/E_ReturnText");
				}
				return this.m_E_ReturnTextText;
			}
		}

		public UITextLocalizeMonoView E_ReturnTextUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ReturnTextUITextLocalizeMonoView == null )
				{
					this.m_E_ReturnTextUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "E_HomePage/E_ReturnLogin/E_ReturnText");
				}
				return this.m_E_ReturnTextUITextLocalizeMonoView;
			}
		}

		public void DestroyWidget()
		{
			this.m_EG_bgARRectTransform = null;
			this.m_EG_bgARTranslucentImage = null;
			this.m_EG_bgRectTransform = null;
			this.m_EG_bgImage = null;
			this.m_E_PVEButton = null;
			this.m_E_PVEImage = null;
			this.m_E_PVENameTextMeshProUGUI = null;
			this.m_E_PVENameUITextLocalizeMonoView = null;
			this.m_E_PVPButton = null;
			this.m_E_PVPImage = null;
			this.m_E_ScanCodeButton = null;
			this.m_E_ScanCodeImage = null;
			this.m_E_AvatarButton = null;
			this.m_E_AvatarImage = null;
			this.m_E_PlayerIcoImage = null;
			this.m_E_PlayerNameTextMeshProUGUI = null;
			this.m_E_BackpackButton = null;
			this.m_E_BackpackImage = null;
			this.m_E_SetUpButton = null;
			this.m_E_SetUpImage = null;
			this.m_E_tutorialButton = null;
			this.m_E_tutorialTranslucentImage = null;
			this.m_E_RankButton = null;
			this.m_E_RankTranslucentImage = null;
			this.m_E_ReturnLoginButton = null;
			this.m_E_ReturnLoginImage = null;
			this.m_E_ReturnTextText = null;
			this.m_E_ReturnTextUITextLocalizeMonoView = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EG_bgARRectTransform = null;
		private BlurBackground.TranslucentImage m_EG_bgARTranslucentImage = null;
		private UnityEngine.RectTransform m_EG_bgRectTransform = null;
		private UnityEngine.UI.Image m_EG_bgImage = null;
		private UnityEngine.UI.Button m_E_PVEButton = null;
		private UnityEngine.UI.Image m_E_PVEImage = null;
		private TMPro.TextMeshProUGUI m_E_PVENameTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_E_PVENameUITextLocalizeMonoView = null;
		private UnityEngine.UI.Button m_E_PVPButton = null;
		private UnityEngine.UI.Image m_E_PVPImage = null;
		private UnityEngine.UI.Button m_E_ScanCodeButton = null;
		private UnityEngine.UI.Image m_E_ScanCodeImage = null;
		private UnityEngine.UI.Button m_E_AvatarButton = null;
		private UnityEngine.UI.Image m_E_AvatarImage = null;
		private UnityEngine.UI.Image m_E_PlayerIcoImage = null;
		private TMPro.TextMeshProUGUI m_E_PlayerNameTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_E_BackpackButton = null;
		private UnityEngine.UI.Image m_E_BackpackImage = null;
		private UnityEngine.UI.Button m_E_SetUpButton = null;
		private UnityEngine.UI.Image m_E_SetUpImage = null;
		private UnityEngine.UI.Button m_E_tutorialButton = null;
		private BlurBackground.TranslucentImage m_E_tutorialTranslucentImage = null;
		private UnityEngine.UI.Button m_E_RankButton = null;
		private BlurBackground.TranslucentImage m_E_RankTranslucentImage = null;
		private UnityEngine.UI.Button m_E_ReturnLoginButton = null;
		private UnityEngine.UI.Image m_E_ReturnLoginImage = null;
		private UnityEngine.UI.Text m_E_ReturnTextText = null;
		private UITextLocalizeMonoView m_E_ReturnTextUITextLocalizeMonoView = null;
		public Transform uiTransform = null;
	}
}
