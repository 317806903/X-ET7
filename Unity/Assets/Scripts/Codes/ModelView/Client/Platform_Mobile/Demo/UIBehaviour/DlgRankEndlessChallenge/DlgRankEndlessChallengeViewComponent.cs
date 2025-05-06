
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgRankEndlessChallenge))]
	[EnableMethod]
	public class DlgRankEndlessChallengeViewComponent : Entity, IAwake, IDestroy
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
					this.m_EG_bgARImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_BG_Click/EG_bgAR");
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
					Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject, "EGRoot/IndividualRank/ES_AvatarShow");
					this.m_es_avatarshow = this.AddChild<ES_AvatarShow, Transform>(subTrans);
				}
				return this.m_es_avatarshow;
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
					this.m_E_PlayerIcoImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRoot/IndividualRank/AvatarHome/Avatar/E_PlayerIco");
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
					this.m_E_PlayerNameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGRoot/IndividualRank/AvatarHome/E_PlayerName");
				}
				return this.m_E_PlayerNameTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Image EImage_ShortRankedBGImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EImage_ShortRankedBGImage == null )
				{
					this.m_EImage_ShortRankedBGImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRoot/IndividualRank/Ranked/EImage_ShortRankedBG");
				}
				return this.m_EImage_ShortRankedBGImage;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_ShortRankNumTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_ShortRankNumTextMeshProUGUI == null )
				{
					this.m_ELabel_ShortRankNumTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGRoot/IndividualRank/Ranked/EImage_ShortRankedBG/ELabel_ShortRankNum");
				}
				return this.m_ELabel_ShortRankNumTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Image EImage_LongRankedBGImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EImage_LongRankedBGImage == null )
				{
					this.m_EImage_LongRankedBGImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRoot/IndividualRank/Ranked/EImage_LongRankedBG");
				}
				return this.m_EImage_LongRankedBGImage;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_LongRankNumTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_LongRankNumTextMeshProUGUI == null )
				{
					this.m_ELabel_LongRankNumTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGRoot/IndividualRank/Ranked/EImage_LongRankedBG/ELabel_LongRankNum");
				}
				return this.m_ELabel_LongRankNumTextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_ChanllengeTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_ChanllengeTextMeshProUGUI == null )
				{
					this.m_ELabel_ChanllengeTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGRoot/IndividualRank/ELabel_Chanllenge");
				}
				return this.m_ELabel_ChanllengeTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.LoopVerticalScrollRect ELoopScrollList_RankLoopVerticalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopScrollList_RankLoopVerticalScrollRect == null )
				{
					this.m_ELoopScrollList_RankLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject, "EGRoot/Leaderboard/ELoopScrollList_Rank");
				}
				return this.m_ELoopScrollList_RankLoopVerticalScrollRect;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_EmptyLeaderbordTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_EmptyLeaderbordTextMeshProUGUI == null )
				{
					this.m_ELabel_EmptyLeaderbordTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGRoot/Leaderboard/ELabel_EmptyLeaderbord");
				}
				return this.m_ELabel_EmptyLeaderbordTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView ELabel_EmptyLeaderbordUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_EmptyLeaderbordUITextLocalizeMonoView == null )
				{
					this.m_ELabel_EmptyLeaderbordUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EGRoot/Leaderboard/ELabel_EmptyLeaderbord");
				}
				return this.m_ELabel_EmptyLeaderbordUITextLocalizeMonoView;
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
			this.m_E_BG_ClickButton = null;
			this.m_E_BG_ClickImage = null;
			this.m_EG_bgARRectTransform = null;
			this.m_EG_bgARImage = null;
			this.m_EG_bgRectTransform = null;
			this.m_EG_bgImage = null;
			this.m_EGRootRectTransform = null;
			this.m_E_QuitRankButton = null;
			this.m_E_QuitRankImage = null;
			this.m_es_avatarshow?.Dispose();
			this.m_es_avatarshow = null;
			this.m_E_PlayerIcoImage = null;
			this.m_E_PlayerNameTextMeshProUGUI = null;
			this.m_EImage_ShortRankedBGImage = null;
			this.m_ELabel_ShortRankNumTextMeshProUGUI = null;
			this.m_EImage_LongRankedBGImage = null;
			this.m_ELabel_LongRankNumTextMeshProUGUI = null;
			this.m_ELabel_ChanllengeTextMeshProUGUI = null;
			this.m_ELoopScrollList_RankLoopVerticalScrollRect = null;
			this.m_ELabel_EmptyLeaderbordTextMeshProUGUI = null;
			this.m_ELabel_EmptyLeaderbordUITextLocalizeMonoView = null;
			this.m_EG_OpenAnimationRectTransform = null;
			this.m_EG_CloseAnimationRectTransform = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_BG_ClickButton = null;
		private UnityEngine.UI.Image m_E_BG_ClickImage = null;
		private UnityEngine.RectTransform m_EG_bgARRectTransform = null;
		private UnityEngine.UI.Image m_EG_bgARImage = null;
		private UnityEngine.RectTransform m_EG_bgRectTransform = null;
		private UnityEngine.UI.Image m_EG_bgImage = null;
		private UnityEngine.RectTransform m_EGRootRectTransform = null;
		private UnityEngine.UI.Button m_E_QuitRankButton = null;
		private UnityEngine.UI.Image m_E_QuitRankImage = null;
		private ES_AvatarShow m_es_avatarshow = null;
		private UnityEngine.UI.Image m_E_PlayerIcoImage = null;
		private TMPro.TextMeshProUGUI m_E_PlayerNameTextMeshProUGUI = null;
		private UnityEngine.UI.Image m_EImage_ShortRankedBGImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_ShortRankNumTextMeshProUGUI = null;
		private UnityEngine.UI.Image m_EImage_LongRankedBGImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_LongRankNumTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ELabel_ChanllengeTextMeshProUGUI = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_ELoopScrollList_RankLoopVerticalScrollRect = null;
		private TMPro.TextMeshProUGUI m_ELabel_EmptyLeaderbordTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ELabel_EmptyLeaderbordUITextLocalizeMonoView = null;
		private UnityEngine.RectTransform m_EG_OpenAnimationRectTransform = null;
		private UnityEngine.RectTransform m_EG_CloseAnimationRectTransform = null;
		public Transform uiTransform = null;
	}
}
