
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

		public UnityEngine.UI.Button EButton_GoldCoinButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_GoldCoinButton == null )
				{
					this.m_EButton_GoldCoinButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_Info/EButton_GoldCoin");
				}
				return this.m_EButton_GoldCoinButton;
			}
		}

		public UnityEngine.UI.Image EButton_GoldCoinImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_GoldCoinImage == null )
				{
					this.m_EButton_GoldCoinImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_Info/EButton_GoldCoin");
				}
				return this.m_EButton_GoldCoinImage;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_GoldCoinNumTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_GoldCoinNumTextMeshProUGUI == null )
				{
					this.m_ELabel_GoldCoinNumTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_HomePage/E_Info/EButton_GoldCoin/ELabel_GoldCoinNum");
				}
				return this.m_ELabel_GoldCoinNumTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Button EButton_PhysicalStrengthButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_PhysicalStrengthButton == null )
				{
					this.m_EButton_PhysicalStrengthButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_Info/EButton_PhysicalStrength");
				}
				return this.m_EButton_PhysicalStrengthButton;
			}
		}

		public UnityEngine.UI.Image EButton_PhysicalStrengthImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_PhysicalStrengthImage == null )
				{
					this.m_EButton_PhysicalStrengthImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_Info/EButton_PhysicalStrength");
				}
				return this.m_EButton_PhysicalStrengthImage;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_PhysicalStrengthNumTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_PhysicalStrengthNumTextMeshProUGUI == null )
				{
					this.m_ELabel_PhysicalStrengthNumTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_HomePage/E_Info/EButton_PhysicalStrength/ELabel_PhysicalStrengthNum");
				}
				return this.m_ELabel_PhysicalStrengthNumTextMeshProUGUI;
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
					this.m_E_ScanCodeButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/title/E_ScanCode");
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
					this.m_E_ScanCodeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/title/E_ScanCode");
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
					this.m_ELabel_PVEPhysicalStrengthTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/E_PVE/PhysicalStrength/ELabel_PVEPhysicalStrength");
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
					this.m_E_PVPNameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/E_PVE/E_PVPName");
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
					this.m_E_PVPNameUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/E_PVE/E_PVPName");
				}
				return this.m_E_PVPNameUITextLocalizeMonoView;
			}
		}

		public UnityEngine.UI.Image E_PVELockImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_PVELockImage == null )
				{
					this.m_E_PVELockImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/E_PVE/E_PVELock");
				}
				return this.m_E_PVELockImage;
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
					this.m_E_EndlessChallengeButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/E_EndlessChallenge");
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
					this.m_E_EndlessChallengeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/E_EndlessChallenge");
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
					this.m_ELabel_WavesTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/E_EndlessChallenge/ELabel_Waves");
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
					this.m_ELabel_WavesUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/E_EndlessChallenge/ELabel_Waves");
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
					this.m_ELabel_RankTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/E_EndlessChallenge/ELabel_Rank");
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
					this.m_ELabel_RankUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/E_EndlessChallenge/ELabel_Rank");
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
					this.m_ELabel_EndlessPhysicalStrengthTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/E_EndlessChallenge/PhysicalStrength/ELabel_EndlessPhysicalStrength");
				}
				return this.m_ELabel_EndlessPhysicalStrengthTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Button EButton_PVPButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_PVPButton == null )
				{
					this.m_EButton_PVPButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/E_PVP/EButton_PVP");
				}
				return this.m_EButton_PVPButton;
			}
		}

		public UnityEngine.UI.Image EButton_PVPImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_PVPImage == null )
				{
					this.m_EButton_PVPImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/E_PVP/EButton_PVP");
				}
				return this.m_EButton_PVPImage;
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
					this.m_ELabel_PVPPhysicalStrengthTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/E_PVP/EButton_PVP/PhysicalStrength/ELabel_PVPPhysicalStrength");
				}
				return this.m_ELabel_PVPPhysicalStrengthTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Image E_PVPLockImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_PVPLockImage == null )
				{
					this.m_E_PVPLockImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/E_PVP/E_PVPLock");
				}
				return this.m_E_PVPLockImage;
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
					this.m_E_RedDotImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_Avatar/E_RedDot");
				}
				return this.m_E_RedDotImage;
			}
		}

		public UnityEngine.UI.Button E_TutorialButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_TutorialButton == null )
				{
					this.m_E_TutorialButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_Function/E_Tutorial");
				}
				return this.m_E_TutorialButton;
			}
		}

		public UnityEngine.UI.Button E_TutorialLockButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_TutorialLockButton == null )
				{
					this.m_E_TutorialLockButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_Function/E_TutorialLock");
				}
				return this.m_E_TutorialLockButton;
			}
		}

		public UnityEngine.UI.Button E_CardsButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_CardsButton == null )
				{
					this.m_E_CardsButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_Function/E_Cards");
				}
				return this.m_E_CardsButton;
			}
		}

		public UnityEngine.UI.Button E_CardsLockButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_CardsLockButton == null )
				{
					this.m_E_CardsLockButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_Function/E_CardsLock");
				}
				return this.m_E_CardsLockButton;
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
			this.m_EButton_GoldCoinButton = null;
			this.m_EButton_GoldCoinImage = null;
			this.m_ELabel_GoldCoinNumTextMeshProUGUI = null;
			this.m_EButton_PhysicalStrengthButton = null;
			this.m_EButton_PhysicalStrengthImage = null;
			this.m_ELabel_PhysicalStrengthNumTextMeshProUGUI = null;
			this.m_E_ScanCodeButton = null;
			this.m_E_ScanCodeImage = null;
			this.m_E_PVEButton = null;
			this.m_E_PVEImage = null;
			this.m_ELabel_PVEPhysicalStrengthTextMeshProUGUI = null;
			this.m_E_PVPNameTextMeshProUGUI = null;
			this.m_E_PVPNameUITextLocalizeMonoView = null;
			this.m_E_PVELockImage = null;
			this.m_E_EndlessChallengeButton = null;
			this.m_E_EndlessChallengeImage = null;
			this.m_ELabel_WavesTextMeshProUGUI = null;
			this.m_ELabel_WavesUITextLocalizeMonoView = null;
			this.m_ELabel_RankTextMeshProUGUI = null;
			this.m_ELabel_RankUITextLocalizeMonoView = null;
			this.m_ELabel_EndlessPhysicalStrengthTextMeshProUGUI = null;
			this.m_EButton_PVPButton = null;
			this.m_EButton_PVPImage = null;
			this.m_ELabel_PVPPhysicalStrengthTextMeshProUGUI = null;
			this.m_E_PVPLockImage = null;
			this.m_E_AvatarButton = null;
			this.m_E_AvatarImage = null;
			this.m_E_PlayerIcoImage = null;
			this.m_E_PlayerNameTextMeshProUGUI = null;
			this.m_E_RedDotImage = null;
			this.m_E_TutorialButton = null;
			this.m_E_TutorialLockButton = null;
			this.m_E_CardsButton = null;
			this.m_E_CardsLockButton = null;
			this.m_E_RankButton = null;
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
		private UnityEngine.UI.Button m_EButton_GoldCoinButton = null;
		private UnityEngine.UI.Image m_EButton_GoldCoinImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_GoldCoinNumTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_EButton_PhysicalStrengthButton = null;
		private UnityEngine.UI.Image m_EButton_PhysicalStrengthImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_PhysicalStrengthNumTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_E_ScanCodeButton = null;
		private UnityEngine.UI.Image m_E_ScanCodeImage = null;
		private UnityEngine.UI.Button m_E_PVEButton = null;
		private UnityEngine.UI.Image m_E_PVEImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_PVEPhysicalStrengthTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_E_PVPNameTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_E_PVPNameUITextLocalizeMonoView = null;
		private UnityEngine.UI.Image m_E_PVELockImage = null;
		private UnityEngine.UI.Button m_E_EndlessChallengeButton = null;
		private UnityEngine.UI.Image m_E_EndlessChallengeImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_WavesTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ELabel_WavesUITextLocalizeMonoView = null;
		private TMPro.TextMeshProUGUI m_ELabel_RankTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ELabel_RankUITextLocalizeMonoView = null;
		private TMPro.TextMeshProUGUI m_ELabel_EndlessPhysicalStrengthTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_EButton_PVPButton = null;
		private UnityEngine.UI.Image m_EButton_PVPImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_PVPPhysicalStrengthTextMeshProUGUI = null;
		private UnityEngine.UI.Image m_E_PVPLockImage = null;
		private UnityEngine.UI.Button m_E_AvatarButton = null;
		private UnityEngine.UI.Image m_E_AvatarImage = null;
		private UnityEngine.UI.Image m_E_PlayerIcoImage = null;
		private TMPro.TextMeshProUGUI m_E_PlayerNameTextMeshProUGUI = null;
		private UnityEngine.UI.Image m_E_RedDotImage = null;
		private UnityEngine.UI.Button m_E_TutorialButton = null;
		private UnityEngine.UI.Button m_E_TutorialLockButton = null;
		private UnityEngine.UI.Button m_E_CardsButton = null;
		private UnityEngine.UI.Button m_E_CardsLockButton = null;
		private UnityEngine.UI.Button m_E_RankButton = null;
		private UnityEngine.UI.Button m_E_ReturnLoginButton = null;
		private UnityEngine.UI.Image m_E_ReturnLoginImage = null;
		private UnityEngine.UI.Text m_E_ReturnTextText = null;
		private UITextLocalizeMonoView m_E_ReturnTextUITextLocalizeMonoView = null;
		public Transform uiTransform = null;
	}
}
