
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgARRoomPVE))]
	[EnableMethod]
	public class DlgARRoomPVEViewComponent : Entity, IAwake, IDestroy
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

		public UnityEngine.RectTransform EGRootRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EGRootRectTransform == null )
				{
					this.m_EGRootRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/EGRoot");
				}
				return this.m_EGRootRectTransform;
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
					this.m_E_QuitRoomButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/EGRoot/E_QuitRoom");
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
					this.m_E_QuitRoomImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/EGRoot/E_QuitRoom");
				}
				return this.m_E_QuitRoomImage;
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
					this.m_E_ReScanButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/EGRoot/Info/E_ReScan");
				}
				return this.m_E_ReScanButton;
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
					this.m_E_ShowQrCodeButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/EGRoot/Info/E_ShowQrCode");
				}
				return this.m_E_ShowQrCodeButton;
			}
		}

		public UnityEngine.UI.Button E_StartButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_StartButton == null )
				{
					this.m_E_StartButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/EGRoot/Info/E_Start");
				}
				return this.m_E_StartButton;
			}
		}

		public UnityEngine.UI.Image E_StartImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_StartImage == null )
				{
					this.m_E_StartImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/EGRoot/Info/E_Start");
				}
				return this.m_E_StartImage;
			}
		}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollList_RoomMemberLoopHorizontalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopScrollList_RoomMemberLoopHorizontalScrollRect == null )
				{
					this.m_ELoopScrollList_RoomMemberLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "EGBackGround/EGRoot/Info/info/list/ELoopScrollList_RoomMember");
				}
				return this.m_ELoopScrollList_RoomMemberLoopHorizontalScrollRect;
			}
		}

		public UnityEngine.UI.Image EButton_boxImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_boxImage == null )
				{
					this.m_EButton_boxImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/EGRoot/Info/info/list/ELoopScrollList_RoomMember/Content/Item_RoomMember/EButton_box");
				}
				return this.m_EButton_boxImage;
			}
		}

		public UnityEngine.UI.Image EButton_shadowImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_shadowImage == null )
				{
					this.m_EButton_shadowImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/EGRoot/Info/info/list/ELoopScrollList_RoomMember/Content/Item_RoomMember/EButton_shadow");
				}
				return this.m_EButton_shadowImage;
			}
		}

		public UnityEngine.UI.Image EImage_TeamImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EImage_TeamImage == null )
				{
					this.m_EImage_TeamImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/EGRoot/Info/info/list/ELoopScrollList_RoomMember/Content/Item_RoomMember/EImage_Team");
				}
				return this.m_EImage_TeamImage;
			}
		}

		public UnityEngine.UI.Button EButton_IconButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_IconButton == null )
				{
					this.m_EButton_IconButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/EGRoot/Info/info/list/ELoopScrollList_RoomMember/Content/Item_RoomMember/EButton_Icon");
				}
				return this.m_EButton_IconButton;
			}
		}

		public UnityEngine.UI.Image EButton_IconImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_IconImage == null )
				{
					this.m_EButton_IconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/EGRoot/Info/info/list/ELoopScrollList_RoomMember/Content/Item_RoomMember/EButton_Icon");
				}
				return this.m_EButton_IconImage;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_Content_NameTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_Content_NameTextMeshProUGUI == null )
				{
					this.m_ELabel_Content_NameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/EGRoot/Info/info/list/ELoopScrollList_RoomMember/Content/Item_RoomMember/ELabel_Content_Name");
				}
				return this.m_ELabel_Content_NameTextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_Content_LvTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_Content_LvTextMeshProUGUI == null )
				{
					this.m_ELabel_Content_LvTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/EGRoot/Info/info/list/ELoopScrollList_RoomMember/Content/Item_RoomMember/ELabel_Content_Lv");
				}
				return this.m_ELabel_Content_LvTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Image ELabel_Content_LeaderImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_Content_LeaderImage == null )
				{
					this.m_ELabel_Content_LeaderImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/EGRoot/Info/info/list/ELoopScrollList_RoomMember/Content/Item_RoomMember/ELabel_Content_Leader");
				}
				return this.m_ELabel_Content_LeaderImage;
			}
		}

		public UnityEngine.RectTransform EG_ReadyRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_ReadyRectTransform == null )
				{
					this.m_EG_ReadyRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/EGRoot/Info/info/list/ELoopScrollList_RoomMember/Content/Item_RoomMember/EG_Ready");
				}
				return this.m_EG_ReadyRectTransform;
			}
		}

		public UnityEngine.UI.Image EG_ReadyImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_ReadyImage == null )
				{
					this.m_EG_ReadyImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/EGRoot/Info/info/list/ELoopScrollList_RoomMember/Content/Item_RoomMember/EG_Ready");
				}
				return this.m_EG_ReadyImage;
			}
		}

		public UnityEngine.UI.Button EButton_OperatorButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_OperatorButton == null )
				{
					this.m_EButton_OperatorButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/EGRoot/Info/info/list/ELoopScrollList_RoomMember/Content/Item_RoomMember/EButton_Operator");
				}
				return this.m_EButton_OperatorButton;
			}
		}

		public UnityEngine.UI.Image EButton_OperatorImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_OperatorImage == null )
				{
					this.m_EButton_OperatorImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/EGRoot/Info/info/list/ELoopScrollList_RoomMember/Content/Item_RoomMember/EButton_Operator");
				}
				return this.m_EButton_OperatorImage;
			}
		}

		public UnityEngine.UI.Text ELabel_OperatorText
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_OperatorText == null )
				{
					this.m_ELabel_OperatorText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "EGBackGround/EGRoot/Info/info/list/ELoopScrollList_RoomMember/Content/Item_RoomMember/EButton_Operator/ELabel_Operator");
				}
				return this.m_ELabel_OperatorText;
			}
		}

		public UnityEngine.RectTransform EG_NoneRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_NoneRectTransform == null )
				{
					this.m_EG_NoneRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/EGRoot/Info/info/list/ELoopScrollList_RoomMember/Content/Item_RoomMember/EG_None");
				}
				return this.m_EG_NoneRectTransform;
			}
		}

		public TMPro.TextMeshProUGUI EG_NoneTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_NoneTextMeshProUGUI == null )
				{
					this.m_EG_NoneTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/EGRoot/Info/info/list/ELoopScrollList_RoomMember/Content/Item_RoomMember/EG_None");
				}
				return this.m_EG_NoneTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView EG_NoneUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_NoneUITextLocalizeMonoView == null )
				{
					this.m_EG_NoneUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EGBackGround/EGRoot/Info/info/list/ELoopScrollList_RoomMember/Content/Item_RoomMember/EG_None");
				}
				return this.m_EG_NoneUITextLocalizeMonoView;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_LevelTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_LevelTextMeshProUGUI == null )
				{
					this.m_ELabel_LevelTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/EGRoot/Level/title/ELabel_Level");
				}
				return this.m_ELabel_LevelTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView ELabel_LevelUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_LevelUITextLocalizeMonoView == null )
				{
					this.m_ELabel_LevelUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EGBackGround/EGRoot/Level/title/ELabel_Level");
				}
				return this.m_ELabel_LevelUITextLocalizeMonoView;
			}
		}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollList_RewardLoopHorizontalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopScrollList_RewardLoopHorizontalScrollRect == null )
				{
					this.m_ELoopScrollList_RewardLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "EGBackGround/EGRoot/Level/Reward/list/ELoopScrollList_Reward");
				}
				return this.m_ELoopScrollList_RewardLoopHorizontalScrollRect;
			}
		}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollList_MonsterLoopHorizontalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopScrollList_MonsterLoopHorizontalScrollRect == null )
				{
					this.m_ELoopScrollList_MonsterLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "EGBackGround/EGRoot/Level/monster/list/ELoopScrollList_Monster");
				}
				return this.m_ELoopScrollList_MonsterLoopHorizontalScrollRect;
			}
		}

		public UnityEngine.UI.Image EImage_Label1Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EImage_Label1Image == null )
				{
					this.m_EImage_Label1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/EGRoot/Level/monster/tips/EImage_Label1");
				}
				return this.m_EImage_Label1Image;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_Label1TextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_Label1TextMeshProUGUI == null )
				{
					this.m_ELabel_Label1TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/EGRoot/Level/monster/tips/EImage_Label1/ELabel_Label1");
				}
				return this.m_ELabel_Label1TextMeshProUGUI;
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
			this.m_E_BGARImage = null;
			this.m_EGRootRectTransform = null;
			this.m_E_QuitRoomButton = null;
			this.m_E_QuitRoomImage = null;
			this.m_E_ReScanButton = null;
			this.m_E_ShowQrCodeButton = null;
			this.m_E_StartButton = null;
			this.m_E_StartImage = null;
			this.m_ELoopScrollList_RoomMemberLoopHorizontalScrollRect = null;
			this.m_EButton_boxImage = null;
			this.m_EButton_shadowImage = null;
			this.m_EImage_TeamImage = null;
			this.m_EButton_IconButton = null;
			this.m_EButton_IconImage = null;
			this.m_ELabel_Content_NameTextMeshProUGUI = null;
			this.m_ELabel_Content_LvTextMeshProUGUI = null;
			this.m_ELabel_Content_LeaderImage = null;
			this.m_EG_ReadyRectTransform = null;
			this.m_EG_ReadyImage = null;
			this.m_EButton_OperatorButton = null;
			this.m_EButton_OperatorImage = null;
			this.m_ELabel_OperatorText = null;
			this.m_EG_NoneRectTransform = null;
			this.m_EG_NoneTextMeshProUGUI = null;
			this.m_EG_NoneUITextLocalizeMonoView = null;
			this.m_ELabel_LevelTextMeshProUGUI = null;
			this.m_ELabel_LevelUITextLocalizeMonoView = null;
			this.m_ELoopScrollList_RewardLoopHorizontalScrollRect = null;
			this.m_ELoopScrollList_MonsterLoopHorizontalScrollRect = null;
			this.m_EImage_Label1Image = null;
			this.m_ELabel_Label1TextMeshProUGUI = null;
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
		private UnityEngine.UI.Image m_E_BGARImage = null;
		private UnityEngine.RectTransform m_EGRootRectTransform = null;
		private UnityEngine.UI.Button m_E_QuitRoomButton = null;
		private UnityEngine.UI.Image m_E_QuitRoomImage = null;
		private UnityEngine.UI.Button m_E_ReScanButton = null;
		private UnityEngine.UI.Button m_E_ShowQrCodeButton = null;
		private UnityEngine.UI.Button m_E_StartButton = null;
		private UnityEngine.UI.Image m_E_StartImage = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_RoomMemberLoopHorizontalScrollRect = null;
		private UnityEngine.UI.Image m_EButton_boxImage = null;
		private UnityEngine.UI.Image m_EButton_shadowImage = null;
		private UnityEngine.UI.Image m_EImage_TeamImage = null;
		private UnityEngine.UI.Button m_EButton_IconButton = null;
		private UnityEngine.UI.Image m_EButton_IconImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_Content_NameTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ELabel_Content_LvTextMeshProUGUI = null;
		private UnityEngine.UI.Image m_ELabel_Content_LeaderImage = null;
		private UnityEngine.RectTransform m_EG_ReadyRectTransform = null;
		private UnityEngine.UI.Image m_EG_ReadyImage = null;
		private UnityEngine.UI.Button m_EButton_OperatorButton = null;
		private UnityEngine.UI.Image m_EButton_OperatorImage = null;
		private UnityEngine.UI.Text m_ELabel_OperatorText = null;
		private UnityEngine.RectTransform m_EG_NoneRectTransform = null;
		private TMPro.TextMeshProUGUI m_EG_NoneTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_EG_NoneUITextLocalizeMonoView = null;
		private TMPro.TextMeshProUGUI m_ELabel_LevelTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ELabel_LevelUITextLocalizeMonoView = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_RewardLoopHorizontalScrollRect = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_MonsterLoopHorizontalScrollRect = null;
		private UnityEngine.UI.Image m_EImage_Label1Image = null;
		private TMPro.TextMeshProUGUI m_ELabel_Label1TextMeshProUGUI = null;
		private UnityEngine.UI.Button m_E_EnergyButton = null;
		private UnityEngine.UI.Image m_E_EnergyImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_PhysicalStrengthNumTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_E_GoldCoinButton = null;
		private UnityEngine.UI.Image m_E_GoldCoinImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_CoinNumTextMeshProUGUI = null;
		public Transform uiTransform = null;
	}
}
