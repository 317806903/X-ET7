
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgARRoom))]
	[EnableMethod]
	public class DlgARRoomViewComponent : Entity, IAwake, IDestroy
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

		public BlurBackground.TranslucentImage E_BGTranslucentImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BGTranslucentImage == null )
				{
					this.m_E_BGTranslucentImage = UIFindHelper.FindDeepChild<BlurBackground.TranslucentImage>(this.uiTransform.gameObject, "EGBackGround/E_BG_Click/E_BG");
				}
				return this.m_E_BGTranslucentImage;
			}
		}

		public UnityEngine.UI.Image E_BGImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BGImage == null )
				{
					this.m_E_BGImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/BG/E_BG");
				}
				return this.m_E_BGImage;
			}
		}

		public UnityEngine.UI.Image E_BGARImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BGARImage == null )
				{
					this.m_E_BGARImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/E_BG_Click/E_BGAR");
				}
				return this.m_E_BGARImage;
			}
		}

		public BlurBackground.TranslucentImage E_BGARTranslucentImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BGARTranslucentImage == null )
				{
					this.m_E_BGARTranslucentImage = UIFindHelper.FindDeepChild<BlurBackground.TranslucentImage>(this.uiTransform.gameObject, "EGBackGround/BG/E_BGAR");
				}
				return this.m_E_BGARTranslucentImage;
			}
		}

		public UnityEngine.UI.Text ELabel_RoomIdText
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_RoomIdText == null )
				{
					this.m_ELabel_RoomIdText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "EGBackGround/ELabel_RoomId");
				}
				return this.m_ELabel_RoomIdText;
			}
		}

		public UnityEngine.UI.Button E_RoomMemberChgTeamButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_RoomMemberChgTeamButton == null )
				{
					this.m_E_RoomMemberChgTeamButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/E_RoomMemberChgTeam");
				}
				return this.m_E_RoomMemberChgTeamButton;
			}
		}

		public UnityEngine.UI.Image E_RoomMemberChgTeamImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_RoomMemberChgTeamImage == null )
				{
					this.m_E_RoomMemberChgTeamImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/E_RoomMemberChgTeam");
				}
				return this.m_E_RoomMemberChgTeamImage;
			}
		}

		public TMPro.TextMeshProUGUI ELable_RoomMemberChgTeamTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELable_RoomMemberChgTeamTextMeshProUGUI == null )
				{
					this.m_ELable_RoomMemberChgTeamTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/E_RoomMemberChgTeam/ELable_RoomMemberChgTeam");
				}
				return this.m_ELable_RoomMemberChgTeamTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView ELable_RoomMemberChgTeamUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELable_RoomMemberChgTeamUITextLocalizeMonoView == null )
				{
					this.m_ELable_RoomMemberChgTeamUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EGBackGround/E_RoomMemberChgTeam/ELable_RoomMemberChgTeam");
				}
				return this.m_ELable_RoomMemberChgTeamUITextLocalizeMonoView;
			}
		}

		public UnityEngine.UI.Button E_QuitRoomButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_QuitRoomButton == null )
				{
					this.m_E_QuitRoomButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/E_QuitRoom");
				}
				return this.m_E_QuitRoomButton;
			}
		}

		public UnityEngine.UI.Image E_QuitRoomImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_QuitRoomImage == null )
				{
					this.m_E_QuitRoomImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/E_QuitRoom");
				}
				return this.m_E_QuitRoomImage;
			}
		}

		public UnityEngine.UI.Image E_RoomMemberList_titleImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_RoomMemberList_titleImage == null )
				{
					this.m_E_RoomMemberList_titleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/E_RoomMemberList_title");
				}
				return this.m_E_RoomMemberList_titleImage;
			}
		}

		public UnityEngine.UI.Image E_RoomMemberList_title_ChallengeImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_RoomMemberList_title_ChallengeImage == null )
				{
					this.m_E_RoomMemberList_title_ChallengeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/E_RoomMemberList_title_Challenge");
				}
				return this.m_E_RoomMemberList_title_ChallengeImage;
			}
		}

		public TMPro.TextMeshProUGUI E_playerCountTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_playerCountTextMeshProUGUI == null )
				{
					this.m_E_playerCountTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/RoomMemberList/E_playerCount");
				}
				return this.m_E_playerCountTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollList_MemberLoopHorizontalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopScrollList_MemberLoopHorizontalScrollRect == null )
				{
					this.m_ELoopScrollList_MemberLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "EGBackGround/RoomMemberList/ELoopScrollList_Member");
				}
				return this.m_ELoopScrollList_MemberLoopHorizontalScrollRect;
			}
		}

		public UnityEngine.UI.Button E_RoomMemberStatusButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_RoomMemberStatusButton == null )
				{
					this.m_E_RoomMemberStatusButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/E_RoomMemberStatus");
				}
				return this.m_E_RoomMemberStatusButton;
			}
		}

		public UnityEngine.UI.Image E_RoomMemberStatusImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_RoomMemberStatusImage == null )
				{
					this.m_E_RoomMemberStatusImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/E_RoomMemberStatus");
				}
				return this.m_E_RoomMemberStatusImage;
			}
		}

		public TMPro.TextMeshProUGUI ELable_RoomMemberStatusTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELable_RoomMemberStatusTextMeshProUGUI == null )
				{
					this.m_ELable_RoomMemberStatusTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/E_RoomMemberStatus/ELable_RoomMemberStatus");
				}
				return this.m_ELable_RoomMemberStatusTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView ELable_RoomMemberStatusUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELable_RoomMemberStatusUITextLocalizeMonoView == null )
				{
					this.m_ELable_RoomMemberStatusUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EGBackGround/E_RoomMemberStatus/ELable_RoomMemberStatus");
				}
				return this.m_ELable_RoomMemberStatusUITextLocalizeMonoView;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_TakePhysicalStrengthNumTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_TakePhysicalStrengthNumTextMeshProUGUI == null )
				{
					this.m_ELabel_TakePhysicalStrengthNumTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/E_RoomMemberStatus/ELable_RoomMemberStatus/ELabel_TakePhysicalStrengthNum");
				}
				return this.m_ELabel_TakePhysicalStrengthNumTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Button E_ShowQrCodeButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ShowQrCodeButton == null )
				{
					this.m_E_ShowQrCodeButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/E_ShowQrCode");
				}
				return this.m_E_ShowQrCodeButton;
			}
		}

		public UnityEngine.UI.Image E_ShowQrCodeImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ShowQrCodeImage == null )
				{
					this.m_E_ShowQrCodeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/E_ShowQrCode");
				}
				return this.m_E_ShowQrCodeImage;
			}
		}

		public UnityEngine.UI.Image E_ShowQrCode2Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ShowQrCode2Image == null )
				{
					this.m_E_ShowQrCode2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/E_ShowQrCode/E_ShowQrCode2");
				}
				return this.m_E_ShowQrCode2Image;
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
					this.m_E_ReScanButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/E_ReScan");
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
					this.m_E_ReScanImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/E_ReScan");
				}
				return this.m_E_ReScanImage;
			}
		}

		public UnityEngine.RectTransform EG_ChooseBattleCfgRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_ChooseBattleCfgRectTransform == null )
				{
					this.m_EG_ChooseBattleCfgRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/EG_ChooseBattleCfg");
				}
				return this.m_EG_ChooseBattleCfgRectTransform;
			}
		}

		public UnityEngine.UI.Button EButton_ChooseBattleCfgButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_ChooseBattleCfgButton == null )
				{
					this.m_EButton_ChooseBattleCfgButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/EG_ChooseBattleCfg/EButton_ChooseBattleCfg");
				}
				return this.m_EButton_ChooseBattleCfgButton;
			}
		}

		public UnityEngine.UI.Image EButton_ChooseBattleCfgImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_ChooseBattleCfgImage == null )
				{
					this.m_EButton_ChooseBattleCfgImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/EG_ChooseBattleCfg/EButton_ChooseBattleCfg");
				}
				return this.m_EButton_ChooseBattleCfgImage;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_BattleCfgIdTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_BattleCfgIdTextMeshProUGUI == null )
				{
					this.m_ELabel_BattleCfgIdTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/EG_ChooseBattleCfg/ELabel_BattleCfgId");
				}
				return this.m_ELabel_BattleCfgIdTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Button E_EnergyButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_EnergyButton == null )
				{
					this.m_E_EnergyButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/value/E_Energy");
				}
				return this.m_E_EnergyButton;
			}
		}

		public UnityEngine.UI.Image E_EnergyImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_EnergyImage == null )
				{
					this.m_E_EnergyImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/value/E_Energy");
				}
				return this.m_E_EnergyImage;
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
					this.m_ELabel_PhysicalStrengthNumTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/value/E_Energy/ELabel_PhysicalStrengthNum");
				}
				return this.m_ELabel_PhysicalStrengthNumTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Button E_GoldCoinButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_GoldCoinButton == null )
				{
					this.m_E_GoldCoinButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/value/E_GoldCoin");
				}
				return this.m_E_GoldCoinButton;
			}
		}

		public UnityEngine.UI.Image E_GoldCoinImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_GoldCoinImage == null )
				{
					this.m_E_GoldCoinImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/value/E_GoldCoin");
				}
				return this.m_E_GoldCoinImage;
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
					this.m_ELabel_CoinNumTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/value/E_GoldCoin/ELabel_CoinNum");
				}
				return this.m_ELabel_CoinNumTextMeshProUGUI;
			}
		}

		public void DestroyWidget()
		{
			this.m_EGBackGroundRectTransform = null;
			this.m_E_BG_ClickButton = null;
			this.m_E_BG_ClickImage = null;
			this.m_E_BGTranslucentImage = null;
			this.m_E_BGImage = null;
			this.m_E_BGARImage = null;
			this.m_E_BGARTranslucentImage = null;
			this.m_ELabel_RoomIdText = null;
			this.m_E_RoomMemberChgTeamButton = null;
			this.m_E_RoomMemberChgTeamImage = null;
			this.m_ELable_RoomMemberChgTeamTextMeshProUGUI = null;
			this.m_ELable_RoomMemberChgTeamUITextLocalizeMonoView = null;
			this.m_E_QuitRoomButton = null;
			this.m_E_QuitRoomImage = null;
			this.m_E_RoomMemberList_titleImage = null;
			this.m_E_RoomMemberList_title_ChallengeImage = null;
			this.m_E_playerCountTextMeshProUGUI = null;
			this.m_ELoopScrollList_MemberLoopHorizontalScrollRect = null;
			this.m_E_RoomMemberStatusButton = null;
			this.m_E_RoomMemberStatusImage = null;
			this.m_ELable_RoomMemberStatusTextMeshProUGUI = null;
			this.m_ELable_RoomMemberStatusUITextLocalizeMonoView = null;
			this.m_ELabel_TakePhysicalStrengthNumTextMeshProUGUI = null;
			this.m_E_ShowQrCodeButton = null;
			this.m_E_ShowQrCodeImage = null;
			this.m_E_ShowQrCode2Image = null;
			this.m_E_ReScanButton = null;
			this.m_E_ReScanImage = null;
			this.m_EG_ChooseBattleCfgRectTransform = null;
			this.m_EButton_ChooseBattleCfgButton = null;
			this.m_EButton_ChooseBattleCfgImage = null;
			this.m_ELabel_BattleCfgIdTextMeshProUGUI = null;
			this.m_E_EnergyButton = null;
			this.m_E_EnergyImage = null;
			this.m_ELabel_PhysicalStrengthNumTextMeshProUGUI = null;
			this.m_E_GoldCoinButton = null;
			this.m_E_GoldCoinImage = null;
			this.m_ELabel_CoinNumTextMeshProUGUI = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EGBackGroundRectTransform = null;
		private UnityEngine.UI.Button m_E_BG_ClickButton = null;
		private UnityEngine.UI.Image m_E_BG_ClickImage = null;
		private BlurBackground.TranslucentImage m_E_BGTranslucentImage = null;
		private UnityEngine.UI.Image m_E_BGImage = null;
		private UnityEngine.UI.Image m_E_BGARImage = null;
		private BlurBackground.TranslucentImage m_E_BGARTranslucentImage = null;
		private UnityEngine.UI.Text m_ELabel_RoomIdText = null;
		private UnityEngine.UI.Button m_E_RoomMemberChgTeamButton = null;
		private UnityEngine.UI.Image m_E_RoomMemberChgTeamImage = null;
		private TMPro.TextMeshProUGUI m_ELable_RoomMemberChgTeamTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ELable_RoomMemberChgTeamUITextLocalizeMonoView = null;
		private UnityEngine.UI.Button m_E_QuitRoomButton = null;
		private UnityEngine.UI.Image m_E_QuitRoomImage = null;
		private UnityEngine.UI.Image m_E_RoomMemberList_titleImage = null;
		private UnityEngine.UI.Image m_E_RoomMemberList_title_ChallengeImage = null;
		private TMPro.TextMeshProUGUI m_E_playerCountTextMeshProUGUI = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_MemberLoopHorizontalScrollRect = null;
		private UnityEngine.UI.Button m_E_RoomMemberStatusButton = null;
		private UnityEngine.UI.Image m_E_RoomMemberStatusImage = null;
		private TMPro.TextMeshProUGUI m_ELable_RoomMemberStatusTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ELable_RoomMemberStatusUITextLocalizeMonoView = null;
		private TMPro.TextMeshProUGUI m_ELabel_TakePhysicalStrengthNumTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_E_ShowQrCodeButton = null;
		private UnityEngine.UI.Image m_E_ShowQrCodeImage = null;
		private UnityEngine.UI.Image m_E_ShowQrCode2Image = null;
		private UnityEngine.UI.Button m_E_ReScanButton = null;
		private UnityEngine.UI.Image m_E_ReScanImage = null;
		private UnityEngine.RectTransform m_EG_ChooseBattleCfgRectTransform = null;
		private UnityEngine.UI.Button m_EButton_ChooseBattleCfgButton = null;
		private UnityEngine.UI.Image m_EButton_ChooseBattleCfgImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_BattleCfgIdTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_E_EnergyButton = null;
		private UnityEngine.UI.Image m_E_EnergyImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_PhysicalStrengthNumTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_E_GoldCoinButton = null;
		private UnityEngine.UI.Image m_E_GoldCoinImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_CoinNumTextMeshProUGUI = null;
		public Transform uiTransform = null;
	}
}
