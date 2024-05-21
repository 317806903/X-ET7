
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
					this.m_E_AvatarButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_OperationPanel/E_function/E_Avatar");
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
					this.m_E_AvatarImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_OperationPanel/E_function/E_Avatar");
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
					this.m_E_PlayerIcoImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_OperationPanel/E_function/E_Avatar/Avatar/E_PlayerIco");
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
					this.m_E_PlayerNameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_HomePage/E_OperationPanel/E_function/E_Avatar/E_PlayerName");
				}
				return this.m_E_PlayerNameTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Image E_RedDotImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_RedDotImage == null )
				{
					this.m_E_RedDotImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_OperationPanel/E_function/E_Avatar/E_RedDot");
				}
				return this.m_E_RedDotImage;
			}
		}

		public UnityEngine.UI.Button E_BagsButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BagsButton == null )
				{
					this.m_E_BagsButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_OperationPanel/E_function/E_Function/E_Bags");
				}
				return this.m_E_BagsButton;
			}
		}

		public UnityEngine.UI.Button E_BattleDeckButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BattleDeckButton == null )
				{
					this.m_E_BattleDeckButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_OperationPanel/E_function/E_Function/E_BattleDeck");
				}
				return this.m_E_BattleDeckButton;
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
					this.m_E_RankButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_OperationPanel/E_function/E_Function/E_Rank");
				}
				return this.m_E_RankButton;
			}
		}

		public UnityEngine.UI.Button E_SettingButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_SettingButton == null )
				{
					this.m_E_SettingButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_OperationPanel/E_function/E_Function/E_Setting");
				}
				return this.m_E_SettingButton;
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
					this.m_E_ScanCodeButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_OperationPanel/E_play/E_GameMode/title/E_ScanCode");
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
					this.m_E_ScanCodeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_OperationPanel/E_play/E_GameMode/title/E_ScanCode");
				}
				return this.m_E_ScanCodeImage;
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
					this.m_E_PVEButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_OperationPanel/E_play/E_GameMode/E_PVE");
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
					this.m_E_PVEImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_OperationPanel/E_play/E_GameMode/E_PVE");
				}
				return this.m_E_PVEImage;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_PVEPhysicalStrengthTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_PVEPhysicalStrengthTextMeshProUGUI == null )
				{
					this.m_ELabel_PVEPhysicalStrengthTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_HomePage/E_OperationPanel/E_play/E_GameMode/E_PVE/PhysicalStrength/ELabel_PVEPhysicalStrength");
				}
				return this.m_ELabel_PVEPhysicalStrengthTextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI E_PVPNameTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_PVPNameTextMeshProUGUI == null )
				{
					this.m_E_PVPNameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_HomePage/E_OperationPanel/E_play/E_GameMode/E_PVE/E_PVPName");
				}
				return this.m_E_PVPNameTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView E_PVPNameUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_PVPNameUITextLocalizeMonoView == null )
				{
					this.m_E_PVPNameUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "E_HomePage/E_OperationPanel/E_play/E_GameMode/E_PVE/E_PVPName");
				}
				return this.m_E_PVPNameUITextLocalizeMonoView;
			}
		}

		public UnityEngine.UI.Button E_EndlessChallengeButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_EndlessChallengeButton == null )
				{
					this.m_E_EndlessChallengeButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_OperationPanel/E_play/E_GameMode/E_EndlessChallenge");
				}
				return this.m_E_EndlessChallengeButton;
			}
		}

		public UnityEngine.UI.Image E_EndlessChallengeImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_EndlessChallengeImage == null )
				{
					this.m_E_EndlessChallengeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_OperationPanel/E_play/E_GameMode/E_EndlessChallenge");
				}
				return this.m_E_EndlessChallengeImage;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_WavesTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_WavesTextMeshProUGUI == null )
				{
					this.m_ELabel_WavesTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_HomePage/E_OperationPanel/E_play/E_GameMode/E_EndlessChallenge/ELabel_Waves");
				}
				return this.m_ELabel_WavesTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView ELabel_WavesUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_WavesUITextLocalizeMonoView == null )
				{
					this.m_ELabel_WavesUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "E_HomePage/E_OperationPanel/E_play/E_GameMode/E_EndlessChallenge/ELabel_Waves");
				}
				return this.m_ELabel_WavesUITextLocalizeMonoView;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_RankTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_RankTextMeshProUGUI == null )
				{
					this.m_ELabel_RankTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_HomePage/E_OperationPanel/E_play/E_GameMode/E_EndlessChallenge/ELabel_Rank");
				}
				return this.m_ELabel_RankTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView ELabel_RankUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_RankUITextLocalizeMonoView == null )
				{
					this.m_ELabel_RankUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "E_HomePage/E_OperationPanel/E_play/E_GameMode/E_EndlessChallenge/ELabel_Rank");
				}
				return this.m_ELabel_RankUITextLocalizeMonoView;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_EndlessPhysicalStrengthTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_EndlessPhysicalStrengthTextMeshProUGUI == null )
				{
					this.m_ELabel_EndlessPhysicalStrengthTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_HomePage/E_OperationPanel/E_play/E_GameMode/E_EndlessChallenge/PhysicalStrength/ELabel_EndlessPhysicalStrength");
				}
				return this.m_ELabel_EndlessPhysicalStrengthTextMeshProUGUI;
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
					this.m_E_PVPButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_OperationPanel/E_play/E_GameMode/E_PVP");
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
					this.m_E_PVPImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_OperationPanel/E_play/E_GameMode/E_PVP");
				}
				return this.m_E_PVPImage;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_PVPPhysicalStrengthTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_PVPPhysicalStrengthTextMeshProUGUI == null )
				{
					this.m_ELabel_PVPPhysicalStrengthTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_HomePage/E_OperationPanel/E_play/E_GameMode/E_PVP/PhysicalStrength/ELabel_PVPPhysicalStrength");
				}
				return this.m_ELabel_PVPPhysicalStrengthTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Button E_GameSettingButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_GameSettingButton == null )
				{
					this.m_E_GameSettingButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_OperationPanel/E_play/E_GameMode/E_GameSetting");
				}
				return this.m_E_GameSettingButton;
			}
		}

		public UnityEngine.UI.Image E_GameSettingImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_GameSettingImage == null )
				{
					this.m_E_GameSettingImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_OperationPanel/E_play/E_GameMode/E_GameSetting");
				}
				return this.m_E_GameSettingImage;
			}
		}

		public void DestroyWidget()
		{
			this.m_EG_bgARRectTransform = null;
			this.m_EG_bgARTranslucentImage = null;
			this.m_EG_bgRectTransform = null;
			this.m_EG_bgImage = null;
			this.m_E_AvatarButton = null;
			this.m_E_AvatarImage = null;
			this.m_E_PlayerIcoImage = null;
			this.m_E_PlayerNameTextMeshProUGUI = null;
			this.m_E_RedDotImage = null;
			this.m_E_BagsButton = null;
			this.m_E_BattleDeckButton = null;
			this.m_E_RankButton = null;
			this.m_E_SettingButton = null;
			this.m_E_ScanCodeButton = null;
			this.m_E_ScanCodeImage = null;
			this.m_E_PVEButton = null;
			this.m_E_PVEImage = null;
			this.m_ELabel_PVEPhysicalStrengthTextMeshProUGUI = null;
			this.m_E_PVPNameTextMeshProUGUI = null;
			this.m_E_PVPNameUITextLocalizeMonoView = null;
			this.m_E_EndlessChallengeButton = null;
			this.m_E_EndlessChallengeImage = null;
			this.m_ELabel_WavesTextMeshProUGUI = null;
			this.m_ELabel_WavesUITextLocalizeMonoView = null;
			this.m_ELabel_RankTextMeshProUGUI = null;
			this.m_ELabel_RankUITextLocalizeMonoView = null;
			this.m_ELabel_EndlessPhysicalStrengthTextMeshProUGUI = null;
			this.m_E_PVPButton = null;
			this.m_E_PVPImage = null;
			this.m_ELabel_PVPPhysicalStrengthTextMeshProUGUI = null;
			this.m_E_GameSettingButton = null;
			this.m_E_GameSettingImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EG_bgARRectTransform = null;
		private BlurBackground.TranslucentImage m_EG_bgARTranslucentImage = null;
		private UnityEngine.RectTransform m_EG_bgRectTransform = null;
		private UnityEngine.UI.Image m_EG_bgImage = null;
		private UnityEngine.UI.Button m_E_AvatarButton = null;
		private UnityEngine.UI.Image m_E_AvatarImage = null;
		private UnityEngine.UI.Image m_E_PlayerIcoImage = null;
		private TMPro.TextMeshProUGUI m_E_PlayerNameTextMeshProUGUI = null;
		private UnityEngine.UI.Image m_E_RedDotImage = null;
		private UnityEngine.UI.Button m_E_BagsButton = null;
		private UnityEngine.UI.Button m_E_BattleDeckButton = null;
		private UnityEngine.UI.Button m_E_RankButton = null;
		private UnityEngine.UI.Button m_E_SettingButton = null;
		private UnityEngine.UI.Button m_E_ScanCodeButton = null;
		private UnityEngine.UI.Image m_E_ScanCodeImage = null;
		private UnityEngine.UI.Button m_E_PVEButton = null;
		private UnityEngine.UI.Image m_E_PVEImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_PVEPhysicalStrengthTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_E_PVPNameTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_E_PVPNameUITextLocalizeMonoView = null;
		private UnityEngine.UI.Button m_E_EndlessChallengeButton = null;
		private UnityEngine.UI.Image m_E_EndlessChallengeImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_WavesTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ELabel_WavesUITextLocalizeMonoView = null;
		private TMPro.TextMeshProUGUI m_ELabel_RankTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ELabel_RankUITextLocalizeMonoView = null;
		private TMPro.TextMeshProUGUI m_ELabel_EndlessPhysicalStrengthTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_E_PVPButton = null;
		private UnityEngine.UI.Image m_E_PVPImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_PVPPhysicalStrengthTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_E_GameSettingButton = null;
		private UnityEngine.UI.Image m_E_GameSettingImage = null;
		public Transform uiTransform = null;
	}
}
