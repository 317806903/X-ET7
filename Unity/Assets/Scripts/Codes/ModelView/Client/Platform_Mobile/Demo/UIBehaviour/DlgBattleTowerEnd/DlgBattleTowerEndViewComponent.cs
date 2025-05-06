
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgBattleTowerEnd))]
	[EnableMethod]
	public class DlgBattleTowerEndViewComponent : Entity, IAwake, IDestroy
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

		public UnityEngine.UI.Image E_Effect_PVPImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Effect_PVPImage == null )
				{
					this.m_E_Effect_PVPImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Effect_PVP");
				}
				return this.m_E_Effect_PVPImage;
			}
		}

		public UnityEngine.UI.Image E_Effect_NormalImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Effect_NormalImage == null )
				{
					this.m_E_Effect_NormalImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Effect_Normal");
				}
				return this.m_E_Effect_NormalImage;
			}
		}

		public UnityEngine.UI.Image E_Effect_EndlessChallengeImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Effect_EndlessChallengeImage == null )
				{
					this.m_E_Effect_EndlessChallengeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Effect_EndlessChallenge");
				}
				return this.m_E_Effect_EndlessChallengeImage;
			}
		}

		public TMPro.TextMeshProUGUI E_ChanllengeText_1TextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ChanllengeText_1TextMeshProUGUI == null )
				{
					this.m_E_ChanllengeText_1TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Effect_EndlessChallenge/Effect_GameEnd_ChallengeEnds/EButton_GameEnd_ChallengeEnds12/E_ChanllengeText_1");
				}
				return this.m_E_ChanllengeText_1TextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView E_ChanllengeText_1UITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ChanllengeText_1UITextLocalizeMonoView == null )
				{
					this.m_E_ChanllengeText_1UITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "E_Effect_EndlessChallenge/Effect_GameEnd_ChallengeEnds/EButton_GameEnd_ChallengeEnds12/E_ChanllengeText_1");
				}
				return this.m_E_ChanllengeText_1UITextLocalizeMonoView;
			}
		}

		public TMPro.TextMeshProUGUI E_ChanllengeText_3TextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ChanllengeText_3TextMeshProUGUI == null )
				{
					this.m_E_ChanllengeText_3TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Effect_EndlessChallenge/Effect_GameEnd_ChallengeEnds/EButton_GameEnd_ChallengeEnds12/E_ChanllengeText_3");
				}
				return this.m_E_ChanllengeText_3TextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView E_ChanllengeText_3UITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ChanllengeText_3UITextLocalizeMonoView == null )
				{
					this.m_E_ChanllengeText_3UITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "E_Effect_EndlessChallenge/Effect_GameEnd_ChallengeEnds/EButton_GameEnd_ChallengeEnds12/E_ChanllengeText_3");
				}
				return this.m_E_ChanllengeText_3UITextLocalizeMonoView;
			}
		}

		public TMPro.TextMeshProUGUI E_ChanllengeText_4TextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ChanllengeText_4TextMeshProUGUI == null )
				{
					this.m_E_ChanllengeText_4TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Effect_EndlessChallenge/Effect_GameEnd_ChallengeEnds/EButton_GameEnd_ChallengeEnds/E_ChanllengeText_4");
				}
				return this.m_E_ChanllengeText_4TextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView E_ChanllengeText_4UITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ChanllengeText_4UITextLocalizeMonoView == null )
				{
					this.m_E_ChanllengeText_4UITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "E_Effect_EndlessChallenge/Effect_GameEnd_ChallengeEnds/EButton_GameEnd_ChallengeEnds/E_ChanllengeText_4");
				}
				return this.m_E_ChanllengeText_4UITextLocalizeMonoView;
			}
		}

		public UnityEngine.UI.Text ELabel_ChanllengeNumText
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_ChanllengeNumText == null )
				{
					this.m_ELabel_ChanllengeNumText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "E_Effect_EndlessChallenge/Effect_GameEnd_ChallengeEnds/EButton_GameEnd_ChallengeEnds/E_ChanllengeText/ELabel_ChanllengeNum");
				}
				return this.m_ELabel_ChanllengeNumText;
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
					this.m_ELabel_RankTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Effect_EndlessChallenge/Effect_GameEnd_ChallengeEnds/EButton_GameEnd_ChallengeEnds/info_box/ELabel_Rank");
				}
				return this.m_ELabel_RankTextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_KillNumTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_KillNumTextMeshProUGUI == null )
				{
					this.m_ELabel_KillNumTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Effect_EndlessChallenge/Effect_GameEnd_ChallengeEnds/EButton_GameEnd_ChallengeEnds/info_box (1)/ELabel_KillNum");
				}
				return this.m_ELabel_KillNumTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Image E_Effect_EndChallengeModeImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Effect_EndChallengeModeImage == null )
				{
					this.m_E_Effect_EndChallengeModeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode");
				}
				return this.m_E_Effect_EndChallengeModeImage;
			}
		}

		public UnityEngine.UI.Image E_overImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_overImage == null )
				{
					this.m_E_overImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/E_lose/E_over");
				}
				return this.m_E_overImage;
			}
		}

		public UnityEngine.UI.Image E_victoryImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_victoryImage == null )
				{
					this.m_E_victoryImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/E_victory/E_victory");
				}
				return this.m_E_victoryImage;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_LvTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_LvTextMeshProUGUI == null )
				{
					this.m_ELabel_LvTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/title/ELabel_Lv");
				}
				return this.m_ELabel_LvTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Image E_RootImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_RootImage == null )
				{
					this.m_E_RootImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Root");
				}
				return this.m_E_RootImage;
			}
		}

		public UnityEngine.RectTransform EG_ItemListRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_ItemListRectTransform == null )
				{
					this.m_EG_ItemListRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "E_Root/EG_ItemList");
				}
				return this.m_EG_ItemListRectTransform;
			}
		}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollList_ItemLoopHorizontalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopScrollList_ItemLoopHorizontalScrollRect == null )
				{
					this.m_ELoopScrollList_ItemLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "E_Root/EG_ItemList/ELoopScrollList_Item");
				}
				return this.m_ELoopScrollList_ItemLoopHorizontalScrollRect;
			}
		}

		public UnityEngine.UI.Button E_ReturnRoomButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ReturnRoomButton == null )
				{
					this.m_E_ReturnRoomButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Root/E_ReturnRoom");
				}
				return this.m_E_ReturnRoomButton;
			}
		}

		public UnityEngine.UI.Image E_ReturnRoomImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ReturnRoomImage == null )
				{
					this.m_E_ReturnRoomImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Root/E_ReturnRoom");
				}
				return this.m_E_ReturnRoomImage;
			}
		}

		public TMPro.TextMeshProUGUI E_Return_TextTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Return_TextTextMeshProUGUI == null )
				{
					this.m_E_Return_TextTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Root/E_ReturnRoom/E_Return_Text");
				}
				return this.m_E_Return_TextTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView E_Return_TextUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Return_TextUITextLocalizeMonoView == null )
				{
					this.m_E_Return_TextUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "E_Root/E_ReturnRoom/E_Return_Text");
				}
				return this.m_E_Return_TextUITextLocalizeMonoView;
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
			this.m_E_Effect_PVPImage = null;
			this.m_E_Effect_NormalImage = null;
			this.m_E_Effect_EndlessChallengeImage = null;
			this.m_E_ChanllengeText_1TextMeshProUGUI = null;
			this.m_E_ChanllengeText_1UITextLocalizeMonoView = null;
			this.m_E_ChanllengeText_3TextMeshProUGUI = null;
			this.m_E_ChanllengeText_3UITextLocalizeMonoView = null;
			this.m_E_ChanllengeText_4TextMeshProUGUI = null;
			this.m_E_ChanllengeText_4UITextLocalizeMonoView = null;
			this.m_ELabel_ChanllengeNumText = null;
			this.m_ELabel_RankTextMeshProUGUI = null;
			this.m_ELabel_KillNumTextMeshProUGUI = null;
			this.m_E_Effect_EndChallengeModeImage = null;
			this.m_E_overImage = null;
			this.m_E_victoryImage = null;
			this.m_ELabel_LvTextMeshProUGUI = null;
			this.m_E_RootImage = null;
			this.m_EG_ItemListRectTransform = null;
			this.m_ELoopScrollList_ItemLoopHorizontalScrollRect = null;
			this.m_E_ReturnRoomButton = null;
			this.m_E_ReturnRoomImage = null;
			this.m_E_Return_TextTextMeshProUGUI = null;
			this.m_E_Return_TextUITextLocalizeMonoView = null;
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
		private UnityEngine.UI.Image m_E_Effect_PVPImage = null;
		private UnityEngine.UI.Image m_E_Effect_NormalImage = null;
		private UnityEngine.UI.Image m_E_Effect_EndlessChallengeImage = null;
		private TMPro.TextMeshProUGUI m_E_ChanllengeText_1TextMeshProUGUI = null;
		private UITextLocalizeMonoView m_E_ChanllengeText_1UITextLocalizeMonoView = null;
		private TMPro.TextMeshProUGUI m_E_ChanllengeText_3TextMeshProUGUI = null;
		private UITextLocalizeMonoView m_E_ChanllengeText_3UITextLocalizeMonoView = null;
		private TMPro.TextMeshProUGUI m_E_ChanllengeText_4TextMeshProUGUI = null;
		private UITextLocalizeMonoView m_E_ChanllengeText_4UITextLocalizeMonoView = null;
		private UnityEngine.UI.Text m_ELabel_ChanllengeNumText = null;
		private TMPro.TextMeshProUGUI m_ELabel_RankTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ELabel_KillNumTextMeshProUGUI = null;
		private UnityEngine.UI.Image m_E_Effect_EndChallengeModeImage = null;
		private UnityEngine.UI.Image m_E_overImage = null;
		private UnityEngine.UI.Image m_E_victoryImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_LvTextMeshProUGUI = null;
		private UnityEngine.UI.Image m_E_RootImage = null;
		private UnityEngine.RectTransform m_EG_ItemListRectTransform = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_ItemLoopHorizontalScrollRect = null;
		private UnityEngine.UI.Button m_E_ReturnRoomButton = null;
		private UnityEngine.UI.Image m_E_ReturnRoomImage = null;
		private TMPro.TextMeshProUGUI m_E_Return_TextTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_E_Return_TextUITextLocalizeMonoView = null;
		private UnityEngine.RectTransform m_EG_OpenAnimationRectTransform = null;
		private UnityEngine.RectTransform m_EG_CloseAnimationRectTransform = null;
		public Transform uiTransform = null;
	}
}
