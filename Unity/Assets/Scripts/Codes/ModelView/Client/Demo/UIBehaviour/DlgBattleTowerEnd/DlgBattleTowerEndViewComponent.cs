
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

		public UnityEngine.UI.Button E_btn_01Button
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_btn_01Button == null )
				{
					this.m_E_btn_01Button = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Root/E_ReturnRoom_Next/E_btn_01");
				}
				return this.m_E_btn_01Button;
			}
		}

		public UnityEngine.UI.Image E_btn_01Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_btn_01Image == null )
				{
					this.m_E_btn_01Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Root/E_ReturnRoom_Next/E_btn_01");
				}
				return this.m_E_btn_01Image;
			}
		}

		public UnityEngine.UI.Button E_btn_02Button
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_btn_02Button == null )
				{
					this.m_E_btn_02Button = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Root/E_ReturnRoom_Next/E_btn_02");
				}
				return this.m_E_btn_02Button;
			}
		}

		public UnityEngine.UI.Image E_btn_02Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_btn_02Image == null )
				{
					this.m_E_btn_02Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Root/E_ReturnRoom_Next/E_btn_02");
				}
				return this.m_E_btn_02Image;
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

		public UnityEngine.UI.Button EButton_GameEndButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_GameEndButton == null )
				{
					this.m_EButton_GameEndButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Root/EButton_GameEnd");
				}
				return this.m_EButton_GameEndButton;
			}
		}

		public UnityEngine.UI.Image EButton_GameEndImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_GameEndImage == null )
				{
					this.m_EButton_GameEndImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Root/EButton_GameEnd");
				}
				return this.m_EButton_GameEndImage;
			}
		}

		public UnityEngine.UI.Text ELabel_GameEndText
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_GameEndText == null )
				{
					this.m_ELabel_GameEndText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "E_Root/EButton_GameEnd/ELabel_GameEnd");
				}
				return this.m_ELabel_GameEndText;
			}
		}

		public UITextLocalizeMonoView ELabel_GameEndUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_GameEndUITextLocalizeMonoView == null )
				{
					this.m_ELabel_GameEndUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "E_Root/EButton_GameEnd/ELabel_GameEnd");
				}
				return this.m_ELabel_GameEndUITextLocalizeMonoView;
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

		public UnityEngine.UI.Button Effect_GameEndButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_Effect_GameEndButton == null )
				{
					this.m_Effect_GameEndButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Effect_PVP/Effect_GameEnd");
				}
				return this.m_Effect_GameEndButton;
			}
		}

		public UnityEngine.UI.Image Effect_GameEndImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_Effect_GameEndImage == null )
				{
					this.m_Effect_GameEndImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Effect_PVP/Effect_GameEnd");
				}
				return this.m_Effect_GameEndImage;
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

		public UnityEngine.UI.Button Effect_GameEnd2Button
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_Effect_GameEnd2Button == null )
				{
					this.m_Effect_GameEnd2Button = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Effect_Normal/Effect_GameEnd2");
				}
				return this.m_Effect_GameEnd2Button;
			}
		}

		public UnityEngine.UI.Image Effect_GameEnd2Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_Effect_GameEnd2Image == null )
				{
					this.m_Effect_GameEnd2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Effect_Normal/Effect_GameEnd2");
				}
				return this.m_Effect_GameEnd2Image;
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

		public UnityEngine.UI.Button Effect_GameEnd_ChallengeEndsButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_Effect_GameEnd_ChallengeEndsButton == null )
				{
					this.m_Effect_GameEnd_ChallengeEndsButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Effect_EndlessChallenge/Effect_GameEnd_ChallengeEnds");
				}
				return this.m_Effect_GameEnd_ChallengeEndsButton;
			}
		}

		public UnityEngine.UI.Image Effect_GameEnd_ChallengeEndsImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_Effect_GameEnd_ChallengeEndsImage == null )
				{
					this.m_Effect_GameEnd_ChallengeEndsImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Effect_EndlessChallenge/Effect_GameEnd_ChallengeEnds");
				}
				return this.m_Effect_GameEnd_ChallengeEndsImage;
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

		public UnityEngine.UI.Button Effect_GameEnd_ChallengeModeEndsButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_Effect_GameEnd_ChallengeModeEndsButton == null )
				{
					this.m_Effect_GameEnd_ChallengeModeEndsButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds");
				}
				return this.m_Effect_GameEnd_ChallengeModeEndsButton;
			}
		}

		public UnityEngine.UI.Image Effect_GameEnd_ChallengeModeEndsImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_Effect_GameEnd_ChallengeModeEndsImage == null )
				{
					this.m_Effect_GameEnd_ChallengeModeEndsImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds");
				}
				return this.m_Effect_GameEnd_ChallengeModeEndsImage;
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

		public UnityEngine.UI.Image E_Item_TowerRewardImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Item_TowerRewardImage == null )
				{
					this.m_E_Item_TowerRewardImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/E_victory/E_Reward/E_Item_TowerReward");
				}
				return this.m_E_Item_TowerRewardImage;
			}
		}

		public UnityEngine.UI.Image E_NoneImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_NoneImage == null )
				{
					this.m_E_NoneImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/E_victory/E_Reward/E_Item_TowerReward/E_None");
				}
				return this.m_E_NoneImage;
			}
		}

		public UnityEngine.UI.Image EImage_TowerBuyShowImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EImage_TowerBuyShowImage == null )
				{
					this.m_EImage_TowerBuyShowImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/E_victory/E_Reward/E_Item_TowerReward/EImage_TowerBuyShow");
				}
				return this.m_EImage_TowerBuyShowImage;
			}
		}

		public UnityEngine.UI.Image E_BoxImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BoxImage == null )
				{
					this.m_E_BoxImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/E_victory/E_Reward/E_Item_TowerReward/EImage_TowerBuyShow/E_Box");
				}
				return this.m_E_BoxImage;
			}
		}

		public UnityEngine.UI.Image EImage_LowImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EImage_LowImage == null )
				{
					this.m_EImage_LowImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/E_victory/E_Reward/E_Item_TowerReward/EImage_TowerBuyShow/E_Box/EImage_Low");
				}
				return this.m_EImage_LowImage;
			}
		}

		public UnityEngine.UI.Image EImage_MiddleImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EImage_MiddleImage == null )
				{
					this.m_EImage_MiddleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/E_victory/E_Reward/E_Item_TowerReward/EImage_TowerBuyShow/E_Box/EImage_Middle");
				}
				return this.m_EImage_MiddleImage;
			}
		}

		public UnityEngine.UI.Image EImage_HighImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EImage_HighImage == null )
				{
					this.m_EImage_HighImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/E_victory/E_Reward/E_Item_TowerReward/EImage_TowerBuyShow/E_Box/EImage_High");
				}
				return this.m_EImage_HighImage;
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
					this.m_EButton_IconButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/E_victory/E_Reward/E_Item_TowerReward/EImage_TowerBuyShow/EButton_Icon");
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
					this.m_EButton_IconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/E_victory/E_Reward/E_Item_TowerReward/EImage_TowerBuyShow/EButton_Icon");
				}
				return this.m_EButton_IconImage;
			}
		}

		public UnityEngine.RectTransform EG_IconStarRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_IconStarRectTransform == null )
				{
					this.m_EG_IconStarRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/E_victory/E_Reward/E_Item_TowerReward/EImage_TowerBuyShow/EG_IconStar");
				}
				return this.m_EG_IconStarRectTransform;
			}
		}

		public UnityEngine.UI.Image E_IconStar1Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_IconStar1Image == null )
				{
					this.m_E_IconStar1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/E_victory/E_Reward/E_Item_TowerReward/EImage_TowerBuyShow/EG_IconStar/E_IconStar1");
				}
				return this.m_E_IconStar1Image;
			}
		}

		public UnityEngine.UI.Image E_IconStar2Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_IconStar2Image == null )
				{
					this.m_E_IconStar2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/E_victory/E_Reward/E_Item_TowerReward/EImage_TowerBuyShow/EG_IconStar/E_IconStar2");
				}
				return this.m_E_IconStar2Image;
			}
		}

		public UnityEngine.UI.Image E_IconStar3Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_IconStar3Image == null )
				{
					this.m_E_IconStar3Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/E_victory/E_Reward/E_Item_TowerReward/EImage_TowerBuyShow/EG_IconStar/E_IconStar3");
				}
				return this.m_E_IconStar3Image;
			}
		}

		public TMPro.TextMeshProUGUI EButton_nameTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_nameTextMeshProUGUI == null )
				{
					this.m_EButton_nameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/E_victory/E_Reward/E_Item_TowerReward/EImage_TowerBuyShow/EButton_name");
				}
				return this.m_EButton_nameTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Button EButton_SelectButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_SelectButton == null )
				{
					this.m_EButton_SelectButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/E_victory/E_Reward/E_Item_TowerReward/EImage_TowerBuyShow/EButton_Select");
				}
				return this.m_EButton_SelectButton;
			}
		}

		public UnityEngine.UI.Image EButton_SelectImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_SelectImage == null )
				{
					this.m_EButton_SelectImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/E_victory/E_Reward/E_Item_TowerReward/EImage_TowerBuyShow/EButton_Select");
				}
				return this.m_EButton_SelectImage;
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
					this.m_EImage_Label1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/E_victory/E_Reward/E_Item_TowerReward/EImage_TowerBuyShow/EImage_Label1");
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
					this.m_ELabel_Label1TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/E_victory/E_Reward/E_Item_TowerReward/EImage_TowerBuyShow/EImage_Label1/ELabel_Label1");
				}
				return this.m_ELabel_Label1TextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Image EImage_Label2Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EImage_Label2Image == null )
				{
					this.m_EImage_Label2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/E_victory/E_Reward/E_Item_TowerReward/EImage_TowerBuyShow/EImage_Label2");
				}
				return this.m_EImage_Label2Image;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_Label2TextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_Label2TextMeshProUGUI == null )
				{
					this.m_ELabel_Label2TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/E_victory/E_Reward/E_Item_TowerReward/EImage_TowerBuyShow/EImage_Label2/ELabel_Label2");
				}
				return this.m_ELabel_Label2TextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Image EImage_Label3Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EImage_Label3Image == null )
				{
					this.m_EImage_Label3Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/E_victory/E_Reward/E_Item_TowerReward/EImage_TowerBuyShow/EImage_Label3");
				}
				return this.m_EImage_Label3Image;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_Label3TextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_Label3TextMeshProUGUI == null )
				{
					this.m_ELabel_Label3TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/E_victory/E_Reward/E_Item_TowerReward/EImage_TowerBuyShow/EImage_Label3/ELabel_Label3");
				}
				return this.m_ELabel_Label3TextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Image EImage_BuyBGImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EImage_BuyBGImage == null )
				{
					this.m_EImage_BuyBGImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/E_victory/E_Reward/E_Item_TowerReward/EImage_TowerBuyShow/EImage_BuyBG");
				}
				return this.m_EImage_BuyBGImage;
			}
		}

		public UnityEngine.UI.Text ELabel_BuyText
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_BuyText == null )
				{
					this.m_ELabel_BuyText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/E_victory/E_Reward/E_Item_TowerReward/EImage_TowerBuyShow/EImage_BuyBG/ELabel_Buy");
				}
				return this.m_ELabel_BuyText;
			}
		}

		public UITextLocalizeMonoView ELabel_BuyUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_BuyUITextLocalizeMonoView == null )
				{
					this.m_ELabel_BuyUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/E_victory/E_Reward/E_Item_TowerReward/EImage_TowerBuyShow/EImage_BuyBG/ELabel_Buy");
				}
				return this.m_ELabel_BuyUITextLocalizeMonoView;
			}
		}

		public UnityEngine.UI.Button EButton_BuyButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_BuyButton == null )
				{
					this.m_EButton_BuyButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/E_victory/E_Reward/E_Item_TowerReward/EImage_TowerBuyShow/EImage_BuyBG/EButton_Buy");
				}
				return this.m_EButton_BuyButton;
			}
		}

		public UnityEngine.UI.Image EButton_BuyImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_BuyImage == null )
				{
					this.m_EButton_BuyImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/E_victory/E_Reward/E_Item_TowerReward/EImage_TowerBuyShow/EImage_BuyBG/EButton_Buy");
				}
				return this.m_EButton_BuyImage;
			}
		}

		public UnityEngine.UI.Image ELabel_UnableBuy_iconImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_UnableBuy_iconImage == null )
				{
					this.m_ELabel_UnableBuy_iconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/E_victory/E_Reward/E_Item_TowerReward/EImage_TowerBuyShow/EImage_BuyBG/ELabel_UnableBuy_icon");
				}
				return this.m_ELabel_UnableBuy_iconImage;
			}
		}

		public UnityEngine.UI.Text ELabel_ContentText
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_ContentText == null )
				{
					this.m_ELabel_ContentText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/E_victory/E_Reward/E_Item_TowerReward/EImage_TowerBuyShow/ELabel_Content");
				}
				return this.m_ELabel_ContentText;
			}
		}

		public UITextLocalizeMonoView ELabel_ContentUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_ContentUITextLocalizeMonoView == null )
				{
					this.m_ELabel_ContentUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/E_victory/E_Reward/E_Item_TowerReward/EImage_TowerBuyShow/ELabel_Content");
				}
				return this.m_ELabel_ContentUITextLocalizeMonoView;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_Content12TextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_Content12TextMeshProUGUI == null )
				{
					this.m_ELabel_Content12TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/E_victory/E_Reward/E_Item_TowerReward/EImage_TowerBuyShow/ELabel_Content12");
				}
				return this.m_ELabel_Content12TextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Image EImage_PurchasedImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EImage_PurchasedImage == null )
				{
					this.m_EImage_PurchasedImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/E_victory/E_Reward/E_Item_TowerReward/EImage_TowerBuyShow/EImage_Purchased");
				}
				return this.m_EImage_PurchasedImage;
			}
		}

		public UnityEngine.UI.Image E_NewcardImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_NewcardImage == null )
				{
					this.m_E_NewcardImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/E_Newcard");
				}
				return this.m_E_NewcardImage;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_NewCardTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_NewCardTextMeshProUGUI == null )
				{
					this.m_ELabel_NewCardTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/E_Newcard/ELabel_NewCard");
				}
				return this.m_ELabel_NewCardTextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI E_numTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_numTextMeshProUGUI == null )
				{
					this.m_E_numTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/E_GoldCoins/E_num");
				}
				return this.m_E_numTextMeshProUGUI;
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
					this.m_ELabel_LvTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Effect_EndChallengeMode/Effect_GameEnd_ChallengeModeEnds/ELabel_Lv");
				}
				return this.m_ELabel_LvTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Button Effect_GameEnd_ModelButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_Effect_GameEnd_ModelButton == null )
				{
					this.m_Effect_GameEnd_ModelButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Effect_PVE/Effect_GameEnd_Model");
				}
				return this.m_Effect_GameEnd_ModelButton;
			}
		}

		public UnityEngine.UI.Image Effect_GameEnd_ModelImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_Effect_GameEnd_ModelImage == null )
				{
					this.m_Effect_GameEnd_ModelImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Effect_PVE/Effect_GameEnd_Model");
				}
				return this.m_Effect_GameEnd_ModelImage;
			}
		}

		public TMPro.TextMeshProUGUI E_ChanllengeLevel_TextTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ChanllengeLevel_TextTextMeshProUGUI == null )
				{
					this.m_E_ChanllengeLevel_TextTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Effect_PVE/Effect_GameEnd_Model/EButton_ChallengeMode_lose/info_box/E_ChanllengeLevel_Text");
				}
				return this.m_E_ChanllengeLevel_TextTextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI E_ChanllengeLevel_Text_2TextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ChanllengeLevel_Text_2TextMeshProUGUI == null )
				{
					this.m_E_ChanllengeLevel_Text_2TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Effect_PVE/Effect_GameEnd_Model/EButton_ChallengeMode_victory/info_box/E_ChanllengeLevel_Text_2");
				}
				return this.m_E_ChanllengeLevel_Text_2TextMeshProUGUI;
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
			this.m_E_RootImage = null;
			this.m_E_btn_01Button = null;
			this.m_E_btn_01Image = null;
			this.m_E_btn_02Button = null;
			this.m_E_btn_02Image = null;
			this.m_E_ReturnRoomButton = null;
			this.m_E_ReturnRoomImage = null;
			this.m_E_Return_TextTextMeshProUGUI = null;
			this.m_EButton_GameEndButton = null;
			this.m_EButton_GameEndImage = null;
			this.m_ELabel_GameEndText = null;
			this.m_ELabel_GameEndUITextLocalizeMonoView = null;
			this.m_E_Effect_PVPImage = null;
			this.m_Effect_GameEndButton = null;
			this.m_Effect_GameEndImage = null;
			this.m_E_Effect_NormalImage = null;
			this.m_Effect_GameEnd2Button = null;
			this.m_Effect_GameEnd2Image = null;
			this.m_E_Effect_EndlessChallengeImage = null;
			this.m_Effect_GameEnd_ChallengeEndsButton = null;
			this.m_Effect_GameEnd_ChallengeEndsImage = null;
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
			this.m_Effect_GameEnd_ChallengeModeEndsButton = null;
			this.m_Effect_GameEnd_ChallengeModeEndsImage = null;
			this.m_E_overImage = null;
			this.m_E_victoryImage = null;
			this.m_E_Item_TowerRewardImage = null;
			this.m_E_NoneImage = null;
			this.m_EImage_TowerBuyShowImage = null;
			this.m_E_BoxImage = null;
			this.m_EImage_LowImage = null;
			this.m_EImage_MiddleImage = null;
			this.m_EImage_HighImage = null;
			this.m_EButton_IconButton = null;
			this.m_EButton_IconImage = null;
			this.m_EG_IconStarRectTransform = null;
			this.m_E_IconStar1Image = null;
			this.m_E_IconStar2Image = null;
			this.m_E_IconStar3Image = null;
			this.m_EButton_nameTextMeshProUGUI = null;
			this.m_EButton_SelectButton = null;
			this.m_EButton_SelectImage = null;
			this.m_EImage_Label1Image = null;
			this.m_ELabel_Label1TextMeshProUGUI = null;
			this.m_EImage_Label2Image = null;
			this.m_ELabel_Label2TextMeshProUGUI = null;
			this.m_EImage_Label3Image = null;
			this.m_ELabel_Label3TextMeshProUGUI = null;
			this.m_EImage_BuyBGImage = null;
			this.m_ELabel_BuyText = null;
			this.m_ELabel_BuyUITextLocalizeMonoView = null;
			this.m_EButton_BuyButton = null;
			this.m_EButton_BuyImage = null;
			this.m_ELabel_UnableBuy_iconImage = null;
			this.m_ELabel_ContentText = null;
			this.m_ELabel_ContentUITextLocalizeMonoView = null;
			this.m_ELabel_Content12TextMeshProUGUI = null;
			this.m_EImage_PurchasedImage = null;
			this.m_E_NewcardImage = null;
			this.m_ELabel_NewCardTextMeshProUGUI = null;
			this.m_E_numTextMeshProUGUI = null;
			this.m_ELabel_LvTextMeshProUGUI = null;
			this.m_Effect_GameEnd_ModelButton = null;
			this.m_Effect_GameEnd_ModelImage = null;
			this.m_E_ChanllengeLevel_TextTextMeshProUGUI = null;
			this.m_E_ChanllengeLevel_Text_2TextMeshProUGUI = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_BG_ClickButton = null;
		private UnityEngine.UI.Image m_E_BG_ClickImage = null;
		private UnityEngine.RectTransform m_EG_bgARRectTransform = null;
		private BlurBackground.TranslucentImage m_EG_bgARTranslucentImage = null;
		private UnityEngine.RectTransform m_EG_bgRectTransform = null;
		private UnityEngine.UI.Image m_EG_bgImage = null;
		private UnityEngine.UI.Image m_E_RootImage = null;
		private UnityEngine.UI.Button m_E_btn_01Button = null;
		private UnityEngine.UI.Image m_E_btn_01Image = null;
		private UnityEngine.UI.Button m_E_btn_02Button = null;
		private UnityEngine.UI.Image m_E_btn_02Image = null;
		private UnityEngine.UI.Button m_E_ReturnRoomButton = null;
		private UnityEngine.UI.Image m_E_ReturnRoomImage = null;
		private TMPro.TextMeshProUGUI m_E_Return_TextTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_EButton_GameEndButton = null;
		private UnityEngine.UI.Image m_EButton_GameEndImage = null;
		private UnityEngine.UI.Text m_ELabel_GameEndText = null;
		private UITextLocalizeMonoView m_ELabel_GameEndUITextLocalizeMonoView = null;
		private UnityEngine.UI.Image m_E_Effect_PVPImage = null;
		private UnityEngine.UI.Button m_Effect_GameEndButton = null;
		private UnityEngine.UI.Image m_Effect_GameEndImage = null;
		private UnityEngine.UI.Image m_E_Effect_NormalImage = null;
		private UnityEngine.UI.Button m_Effect_GameEnd2Button = null;
		private UnityEngine.UI.Image m_Effect_GameEnd2Image = null;
		private UnityEngine.UI.Image m_E_Effect_EndlessChallengeImage = null;
		private UnityEngine.UI.Button m_Effect_GameEnd_ChallengeEndsButton = null;
		private UnityEngine.UI.Image m_Effect_GameEnd_ChallengeEndsImage = null;
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
		private UnityEngine.UI.Button m_Effect_GameEnd_ChallengeModeEndsButton = null;
		private UnityEngine.UI.Image m_Effect_GameEnd_ChallengeModeEndsImage = null;
		private UnityEngine.UI.Image m_E_overImage = null;
		private UnityEngine.UI.Image m_E_victoryImage = null;
		private UnityEngine.UI.Image m_E_Item_TowerRewardImage = null;
		private UnityEngine.UI.Image m_E_NoneImage = null;
		private UnityEngine.UI.Image m_EImage_TowerBuyShowImage = null;
		private UnityEngine.UI.Image m_E_BoxImage = null;
		private UnityEngine.UI.Image m_EImage_LowImage = null;
		private UnityEngine.UI.Image m_EImage_MiddleImage = null;
		private UnityEngine.UI.Image m_EImage_HighImage = null;
		private UnityEngine.UI.Button m_EButton_IconButton = null;
		private UnityEngine.UI.Image m_EButton_IconImage = null;
		private UnityEngine.RectTransform m_EG_IconStarRectTransform = null;
		private UnityEngine.UI.Image m_E_IconStar1Image = null;
		private UnityEngine.UI.Image m_E_IconStar2Image = null;
		private UnityEngine.UI.Image m_E_IconStar3Image = null;
		private TMPro.TextMeshProUGUI m_EButton_nameTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_EButton_SelectButton = null;
		private UnityEngine.UI.Image m_EButton_SelectImage = null;
		private UnityEngine.UI.Image m_EImage_Label1Image = null;
		private TMPro.TextMeshProUGUI m_ELabel_Label1TextMeshProUGUI = null;
		private UnityEngine.UI.Image m_EImage_Label2Image = null;
		private TMPro.TextMeshProUGUI m_ELabel_Label2TextMeshProUGUI = null;
		private UnityEngine.UI.Image m_EImage_Label3Image = null;
		private TMPro.TextMeshProUGUI m_ELabel_Label3TextMeshProUGUI = null;
		private UnityEngine.UI.Image m_EImage_BuyBGImage = null;
		private UnityEngine.UI.Text m_ELabel_BuyText = null;
		private UITextLocalizeMonoView m_ELabel_BuyUITextLocalizeMonoView = null;
		private UnityEngine.UI.Button m_EButton_BuyButton = null;
		private UnityEngine.UI.Image m_EButton_BuyImage = null;
		private UnityEngine.UI.Image m_ELabel_UnableBuy_iconImage = null;
		private UnityEngine.UI.Text m_ELabel_ContentText = null;
		private UITextLocalizeMonoView m_ELabel_ContentUITextLocalizeMonoView = null;
		private TMPro.TextMeshProUGUI m_ELabel_Content12TextMeshProUGUI = null;
		private UnityEngine.UI.Image m_EImage_PurchasedImage = null;
		private UnityEngine.UI.Image m_E_NewcardImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_NewCardTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_E_numTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ELabel_LvTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_Effect_GameEnd_ModelButton = null;
		private UnityEngine.UI.Image m_Effect_GameEnd_ModelImage = null;
		private TMPro.TextMeshProUGUI m_E_ChanllengeLevel_TextTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_E_ChanllengeLevel_Text_2TextMeshProUGUI = null;
		public Transform uiTransform = null;
	}
}
