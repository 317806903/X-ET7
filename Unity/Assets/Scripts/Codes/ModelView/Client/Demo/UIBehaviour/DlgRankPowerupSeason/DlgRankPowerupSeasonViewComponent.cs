
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgRankPowerupSeason))]
	[EnableMethod]
	public class DlgRankPowerupSeasonViewComponent : Entity, IAwake, IDestroy
	{
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
					this.m_E_BG_ClickButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_BG_Click");
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
					this.m_E_BG_ClickImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_BG_Click");
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
					this.m_EG_bgARRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "E_BG_Click/EG_bgAR");
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
					this.m_EG_bgARTranslucentImage = UIFindHelper.FindDeepChild<BlurBackground.TranslucentImage>(this.uiTransform.gameObject, "E_BG_Click/EG_bgAR");
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
					this.m_EG_bgRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "E_BG_Click/EG_bg");
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
					this.m_EG_bgImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_BG_Click/EG_bg");
				}
				return this.m_EG_bgImage;
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
					this.m_EGRootRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGRoot");
				}
				return this.m_EGRootRectTransform;
			}
		}

		public UnityEngine.RectTransform EG_CoinListRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_CoinListRectTransform == null )
				{
					this.m_EG_CoinListRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGRoot/EG_CoinList");
				}
				return this.m_EG_CoinListRectTransform;
			}
		}

		public UnityEngine.UI.Button EButton_ArcadeCoinButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_ArcadeCoinButton == null )
				{
					this.m_EButton_ArcadeCoinButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGRoot/EG_CoinList/EButton_ArcadeCoin");
				}
				return this.m_EButton_ArcadeCoinButton;
			}
		}

		public UnityEngine.UI.Image EButton_ArcadeCoinImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_ArcadeCoinImage == null )
				{
					this.m_EButton_ArcadeCoinImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRoot/EG_CoinList/EButton_ArcadeCoin");
				}
				return this.m_EButton_ArcadeCoinImage;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_ArcadeCoinNumTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_ArcadeCoinNumTextMeshProUGUI == null )
				{
					this.m_ELabel_ArcadeCoinNumTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGRoot/EG_CoinList/EButton_ArcadeCoin/ELabel_ArcadeCoinNum");
				}
				return this.m_ELabel_ArcadeCoinNumTextMeshProUGUI;
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
					this.m_EButton_PhysicalStrengthButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGRoot/EG_CoinList/EButton_PhysicalStrength");
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
					this.m_EButton_PhysicalStrengthImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRoot/EG_CoinList/EButton_PhysicalStrength");
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
					this.m_ELabel_PhysicalStrengthNumTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGRoot/EG_CoinList/EButton_PhysicalStrength/ELabel_PhysicalStrengthNum");
				}
				return this.m_ELabel_PhysicalStrengthNumTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Button E_QuitRankButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_QuitRankButton == null )
				{
					this.m_E_QuitRankButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGRoot/E_QuitRank");
				}
				return this.m_E_QuitRankButton;
			}
		}

		public UnityEngine.UI.Image E_QuitRankImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_QuitRankImage == null )
				{
					this.m_E_QuitRankImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRoot/E_QuitRank");
				}
				return this.m_E_QuitRankImage;
			}
		}

		public TMPro.TextMeshProUGUI ETxtTitleTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtTitleTextMeshProUGUI == null )
				{
					this.m_ETxtTitleTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGRoot/ETxtTitle");
				}
				return this.m_ETxtTitleTextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI ETxtTimeTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtTimeTextMeshProUGUI == null )
				{
					this.m_ETxtTimeTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGRoot/ETxtTime");
				}
				return this.m_ETxtTimeTextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI ETxtMonstersTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtMonstersTextMeshProUGUI == null )
				{
					this.m_ETxtMonstersTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGRoot/LeftSeasonInfoPanel/ETxtMonsters");
				}
				return this.m_ETxtMonstersTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView ETxtMonstersUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtMonstersUITextLocalizeMonoView == null )
				{
					this.m_ETxtMonstersUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EGRoot/LeftSeasonInfoPanel/ETxtMonsters");
				}
				return this.m_ETxtMonstersUITextLocalizeMonoView;
			}
		}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopListView_MonsersLoopHorizontalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopListView_MonsersLoopHorizontalScrollRect == null )
				{
					this.m_ELoopListView_MonsersLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "EGRoot/LeftSeasonInfoPanel/ELoopListView_Monsers");
				}
				return this.m_ELoopListView_MonsersLoopHorizontalScrollRect;
			}
		}

		public TMPro.TextMeshProUGUI ETxtMonsterDesTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtMonsterDesTextMeshProUGUI == null )
				{
					this.m_ETxtMonsterDesTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGRoot/LeftSeasonInfoPanel/ETxtMonsterDes");
				}
				return this.m_ETxtMonsterDesTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView ETxtMonsterDesUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtMonsterDesUITextLocalizeMonoView == null )
				{
					this.m_ETxtMonsterDesUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EGRoot/LeftSeasonInfoPanel/ETxtMonsterDes");
				}
				return this.m_ETxtMonsterDesUITextLocalizeMonoView;
			}
		}

		public TMPro.TextMeshProUGUI ETxtCardsTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtCardsTextMeshProUGUI == null )
				{
					this.m_ETxtCardsTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGRoot/LeftSeasonInfoPanel/ETxtCards");
				}
				return this.m_ETxtCardsTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView ETxtCardsUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtCardsUITextLocalizeMonoView == null )
				{
					this.m_ETxtCardsUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EGRoot/LeftSeasonInfoPanel/ETxtCards");
				}
				return this.m_ETxtCardsUITextLocalizeMonoView;
			}
		}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopListView_CardsLoopHorizontalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopListView_CardsLoopHorizontalScrollRect == null )
				{
					this.m_ELoopListView_CardsLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "EGRoot/LeftSeasonInfoPanel/ELoopListView_Cards");
				}
				return this.m_ELoopListView_CardsLoopHorizontalScrollRect;
			}
		}

		public TMPro.TextMeshProUGUI ETxtCardDesTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtCardDesTextMeshProUGUI == null )
				{
					this.m_ETxtCardDesTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGRoot/LeftSeasonInfoPanel/ETxtCardDes");
				}
				return this.m_ETxtCardDesTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView ETxtCardDesUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtCardDesUITextLocalizeMonoView == null )
				{
					this.m_ETxtCardDesUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EGRoot/LeftSeasonInfoPanel/ETxtCardDes");
				}
				return this.m_ETxtCardDesUITextLocalizeMonoView;
			}
		}

		public TMPro.TextMeshProUGUI ETxtFrameTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtFrameTextMeshProUGUI == null )
				{
					this.m_ETxtFrameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGRoot/LeftSeasonInfoPanel/ETxtFrame");
				}
				return this.m_ETxtFrameTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView ETxtFrameUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtFrameUITextLocalizeMonoView == null )
				{
					this.m_ETxtFrameUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EGRoot/LeftSeasonInfoPanel/ETxtFrame");
				}
				return this.m_ETxtFrameUITextLocalizeMonoView;
			}
		}

		public TMPro.TextMeshProUGUI ETxtFrameDesTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtFrameDesTextMeshProUGUI == null )
				{
					this.m_ETxtFrameDesTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGRoot/LeftSeasonInfoPanel/ETxtFrameDes");
				}
				return this.m_ETxtFrameDesTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView ETxtFrameDesUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtFrameDesUITextLocalizeMonoView == null )
				{
					this.m_ETxtFrameDesUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EGRoot/LeftSeasonInfoPanel/ETxtFrameDes");
				}
				return this.m_ETxtFrameDesUITextLocalizeMonoView;
			}
		}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopListView_FrameLoopHorizontalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopListView_FrameLoopHorizontalScrollRect == null )
				{
					this.m_ELoopListView_FrameLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "EGRoot/LeftSeasonInfoPanel/ELoopListView_Frame");
				}
				return this.m_ELoopListView_FrameLoopHorizontalScrollRect;
			}
		}

		public EPage_Rank EPage_Rank
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_epage_rank == null )
				{
					Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject, "EGRoot/EPage_Rank");
					this.m_epage_rank = this.AddChild<EPage_Rank, Transform>(subTrans);
				}
				return this.m_epage_rank;
			}
		}

		public EPage_Powerup EPage_Powerup
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_epage_powerup == null )
				{
					Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject, "EGRoot/EPage_Powerup");
					this.m_epage_powerup = this.AddChild<EPage_Powerup, Transform>(subTrans);
				}
				return this.m_epage_powerup;
			}
		}

		public UnityEngine.UI.Button EBtnTabLeaderboardButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EBtnTabLeaderboardButton == null )
				{
					this.m_EBtnTabLeaderboardButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGRoot/RigthTopTabGroup/EBtnTabLeaderboard");
				}
				return this.m_EBtnTabLeaderboardButton;
			}
		}

		public UnityEngine.UI.Image EBtnTabLeaderboardImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EBtnTabLeaderboardImage == null )
				{
					this.m_EBtnTabLeaderboardImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRoot/RigthTopTabGroup/EBtnTabLeaderboard");
				}
				return this.m_EBtnTabLeaderboardImage;
			}
		}

		public TMPro.TextMeshProUGUI ETxtLeaderBoardTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtLeaderBoardTextMeshProUGUI == null )
				{
					this.m_ETxtLeaderBoardTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGRoot/RigthTopTabGroup/EBtnTabLeaderboard/ETxtLeaderBoard");
				}
				return this.m_ETxtLeaderBoardTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView ETxtLeaderBoardUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtLeaderBoardUITextLocalizeMonoView == null )
				{
					this.m_ETxtLeaderBoardUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EGRoot/RigthTopTabGroup/EBtnTabLeaderboard/ETxtLeaderBoard");
				}
				return this.m_ETxtLeaderBoardUITextLocalizeMonoView;
			}
		}

		public UnityEngine.UI.Button EBtnTabLeaderboardUnselectedButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EBtnTabLeaderboardUnselectedButton == null )
				{
					this.m_EBtnTabLeaderboardUnselectedButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGRoot/RigthTopTabGroup/EBtnTabLeaderboardUnselected");
				}
				return this.m_EBtnTabLeaderboardUnselectedButton;
			}
		}

		public UnityEngine.UI.Image EBtnTabLeaderboardUnselectedImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EBtnTabLeaderboardUnselectedImage == null )
				{
					this.m_EBtnTabLeaderboardUnselectedImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRoot/RigthTopTabGroup/EBtnTabLeaderboardUnselected");
				}
				return this.m_EBtnTabLeaderboardUnselectedImage;
			}
		}

		public TMPro.TextMeshProUGUI ETxtLeaderBoardUnselectedTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtLeaderBoardUnselectedTextMeshProUGUI == null )
				{
					this.m_ETxtLeaderBoardUnselectedTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGRoot/RigthTopTabGroup/EBtnTabLeaderboardUnselected/ETxtLeaderBoardUnselected");
				}
				return this.m_ETxtLeaderBoardUnselectedTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView ETxtLeaderBoardUnselectedUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtLeaderBoardUnselectedUITextLocalizeMonoView == null )
				{
					this.m_ETxtLeaderBoardUnselectedUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EGRoot/RigthTopTabGroup/EBtnTabLeaderboardUnselected/ETxtLeaderBoardUnselected");
				}
				return this.m_ETxtLeaderBoardUnselectedUITextLocalizeMonoView;
			}
		}

		public UnityEngine.UI.Button EBtnTabPowerupsButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EBtnTabPowerupsButton == null )
				{
					this.m_EBtnTabPowerupsButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGRoot/RigthTopTabGroup/EBtnTabPowerups");
				}
				return this.m_EBtnTabPowerupsButton;
			}
		}

		public UnityEngine.UI.Image EBtnTabPowerupsImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EBtnTabPowerupsImage == null )
				{
					this.m_EBtnTabPowerupsImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRoot/RigthTopTabGroup/EBtnTabPowerups");
				}
				return this.m_EBtnTabPowerupsImage;
			}
		}

		public TMPro.TextMeshProUGUI ETxtPowerupsTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtPowerupsTextMeshProUGUI == null )
				{
					this.m_ETxtPowerupsTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGRoot/RigthTopTabGroup/EBtnTabPowerups/ETxtPowerups");
				}
				return this.m_ETxtPowerupsTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView ETxtPowerupsUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtPowerupsUITextLocalizeMonoView == null )
				{
					this.m_ETxtPowerupsUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EGRoot/RigthTopTabGroup/EBtnTabPowerups/ETxtPowerups");
				}
				return this.m_ETxtPowerupsUITextLocalizeMonoView;
			}
		}

		public UnityEngine.UI.Image ERedDotIconImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ERedDotIconImage == null )
				{
					this.m_ERedDotIconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRoot/RigthTopTabGroup/EBtnTabPowerups/ERedDotIcon");
				}
				return this.m_ERedDotIconImage;
			}
		}

		public UnityEngine.UI.Button EBtnTabPowerups_UnselectedButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EBtnTabPowerups_UnselectedButton == null )
				{
					this.m_EBtnTabPowerups_UnselectedButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGRoot/RigthTopTabGroup/EBtnTabPowerups_Unselected");
				}
				return this.m_EBtnTabPowerups_UnselectedButton;
			}
		}

		public UnityEngine.UI.Image EBtnTabPowerups_UnselectedImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EBtnTabPowerups_UnselectedImage == null )
				{
					this.m_EBtnTabPowerups_UnselectedImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRoot/RigthTopTabGroup/EBtnTabPowerups_Unselected");
				}
				return this.m_EBtnTabPowerups_UnselectedImage;
			}
		}

		public TMPro.TextMeshProUGUI ETxtPowerupsUnselectedTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtPowerupsUnselectedTextMeshProUGUI == null )
				{
					this.m_ETxtPowerupsUnselectedTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGRoot/RigthTopTabGroup/EBtnTabPowerups_Unselected/ETxtPowerupsUnselected");
				}
				return this.m_ETxtPowerupsUnselectedTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView ETxtPowerupsUnselectedUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtPowerupsUnselectedUITextLocalizeMonoView == null )
				{
					this.m_ETxtPowerupsUnselectedUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EGRoot/RigthTopTabGroup/EBtnTabPowerups_Unselected/ETxtPowerupsUnselected");
				}
				return this.m_ETxtPowerupsUnselectedUITextLocalizeMonoView;
			}
		}

		public UnityEngine.UI.Image ERedDotIconUnselectedImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ERedDotIconUnselectedImage == null )
				{
					this.m_ERedDotIconUnselectedImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRoot/RigthTopTabGroup/EBtnTabPowerups_Unselected/ERedDotIconUnselected");
				}
				return this.m_ERedDotIconUnselectedImage;
			}
		}

		public void DestroyWidget()
		{
			this.m_E_BG_ClickButton = null;
			this.m_E_BG_ClickImage = null;
			this.m_EG_bgARRectTransform = null;
			this.m_EG_bgARTranslucentImage = null;
			this.m_EG_bgRectTransform = null;
			this.m_EG_bgImage = null;
			this.m_EGRootRectTransform = null;
			this.m_EG_CoinListRectTransform = null;
			this.m_EButton_ArcadeCoinButton = null;
			this.m_EButton_ArcadeCoinImage = null;
			this.m_ELabel_ArcadeCoinNumTextMeshProUGUI = null;
			this.m_EButton_PhysicalStrengthButton = null;
			this.m_EButton_PhysicalStrengthImage = null;
			this.m_ELabel_PhysicalStrengthNumTextMeshProUGUI = null;
			this.m_E_QuitRankButton = null;
			this.m_E_QuitRankImage = null;
			this.m_ETxtTitleTextMeshProUGUI = null;
			this.m_ETxtTimeTextMeshProUGUI = null;
			this.m_ETxtMonstersTextMeshProUGUI = null;
			this.m_ETxtMonstersUITextLocalizeMonoView = null;
			this.m_ELoopListView_MonsersLoopHorizontalScrollRect = null;
			this.m_ETxtMonsterDesTextMeshProUGUI = null;
			this.m_ETxtMonsterDesUITextLocalizeMonoView = null;
			this.m_ETxtCardsTextMeshProUGUI = null;
			this.m_ETxtCardsUITextLocalizeMonoView = null;
			this.m_ELoopListView_CardsLoopHorizontalScrollRect = null;
			this.m_ETxtCardDesTextMeshProUGUI = null;
			this.m_ETxtCardDesUITextLocalizeMonoView = null;
			this.m_ETxtFrameTextMeshProUGUI = null;
			this.m_ETxtFrameUITextLocalizeMonoView = null;
			this.m_ETxtFrameDesTextMeshProUGUI = null;
			this.m_ETxtFrameDesUITextLocalizeMonoView = null;
			this.m_ELoopListView_FrameLoopHorizontalScrollRect = null;
			this.m_epage_rank?.Dispose();
			this.m_epage_rank = null;
			this.m_epage_powerup?.Dispose();
			this.m_epage_powerup = null;
			this.m_EBtnTabLeaderboardButton = null;
			this.m_EBtnTabLeaderboardImage = null;
			this.m_ETxtLeaderBoardTextMeshProUGUI = null;
			this.m_ETxtLeaderBoardUITextLocalizeMonoView = null;
			this.m_EBtnTabLeaderboardUnselectedButton = null;
			this.m_EBtnTabLeaderboardUnselectedImage = null;
			this.m_ETxtLeaderBoardUnselectedTextMeshProUGUI = null;
			this.m_ETxtLeaderBoardUnselectedUITextLocalizeMonoView = null;
			this.m_EBtnTabPowerupsButton = null;
			this.m_EBtnTabPowerupsImage = null;
			this.m_ETxtPowerupsTextMeshProUGUI = null;
			this.m_ETxtPowerupsUITextLocalizeMonoView = null;
			this.m_ERedDotIconImage = null;
			this.m_EBtnTabPowerups_UnselectedButton = null;
			this.m_EBtnTabPowerups_UnselectedImage = null;
			this.m_ETxtPowerupsUnselectedTextMeshProUGUI = null;
			this.m_ETxtPowerupsUnselectedUITextLocalizeMonoView = null;
			this.m_ERedDotIconUnselectedImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_BG_ClickButton = null;
		private UnityEngine.UI.Image m_E_BG_ClickImage = null;
		private UnityEngine.RectTransform m_EG_bgARRectTransform = null;
		private BlurBackground.TranslucentImage m_EG_bgARTranslucentImage = null;
		private UnityEngine.RectTransform m_EG_bgRectTransform = null;
		private UnityEngine.UI.Image m_EG_bgImage = null;
		private UnityEngine.RectTransform m_EGRootRectTransform = null;
		private UnityEngine.RectTransform m_EG_CoinListRectTransform = null;
		private UnityEngine.UI.Button m_EButton_ArcadeCoinButton = null;
		private UnityEngine.UI.Image m_EButton_ArcadeCoinImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_ArcadeCoinNumTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_EButton_PhysicalStrengthButton = null;
		private UnityEngine.UI.Image m_EButton_PhysicalStrengthImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_PhysicalStrengthNumTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_E_QuitRankButton = null;
		private UnityEngine.UI.Image m_E_QuitRankImage = null;
		private TMPro.TextMeshProUGUI m_ETxtTitleTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ETxtTimeTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ETxtMonstersTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ETxtMonstersUITextLocalizeMonoView = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopListView_MonsersLoopHorizontalScrollRect = null;
		private TMPro.TextMeshProUGUI m_ETxtMonsterDesTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ETxtMonsterDesUITextLocalizeMonoView = null;
		private TMPro.TextMeshProUGUI m_ETxtCardsTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ETxtCardsUITextLocalizeMonoView = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopListView_CardsLoopHorizontalScrollRect = null;
		private TMPro.TextMeshProUGUI m_ETxtCardDesTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ETxtCardDesUITextLocalizeMonoView = null;
		private TMPro.TextMeshProUGUI m_ETxtFrameTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ETxtFrameUITextLocalizeMonoView = null;
		private TMPro.TextMeshProUGUI m_ETxtFrameDesTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ETxtFrameDesUITextLocalizeMonoView = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopListView_FrameLoopHorizontalScrollRect = null;
		private EPage_Rank m_epage_rank = null;
		private EPage_Powerup m_epage_powerup = null;
		private UnityEngine.UI.Button m_EBtnTabLeaderboardButton = null;
		private UnityEngine.UI.Image m_EBtnTabLeaderboardImage = null;
		private TMPro.TextMeshProUGUI m_ETxtLeaderBoardTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ETxtLeaderBoardUITextLocalizeMonoView = null;
		private UnityEngine.UI.Button m_EBtnTabLeaderboardUnselectedButton = null;
		private UnityEngine.UI.Image m_EBtnTabLeaderboardUnselectedImage = null;
		private TMPro.TextMeshProUGUI m_ETxtLeaderBoardUnselectedTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ETxtLeaderBoardUnselectedUITextLocalizeMonoView = null;
		private UnityEngine.UI.Button m_EBtnTabPowerupsButton = null;
		private UnityEngine.UI.Image m_EBtnTabPowerupsImage = null;
		private TMPro.TextMeshProUGUI m_ETxtPowerupsTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ETxtPowerupsUITextLocalizeMonoView = null;
		private UnityEngine.UI.Image m_ERedDotIconImage = null;
		private UnityEngine.UI.Button m_EBtnTabPowerups_UnselectedButton = null;
		private UnityEngine.UI.Image m_EBtnTabPowerups_UnselectedImage = null;
		private TMPro.TextMeshProUGUI m_ETxtPowerupsUnselectedTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ETxtPowerupsUnselectedUITextLocalizeMonoView = null;
		private UnityEngine.UI.Image m_ERedDotIconUnselectedImage = null;
		public Transform uiTransform = null;
	}
}
