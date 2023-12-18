
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

		public TMPro.TextMeshProUGUI E_ChanllengeText_2TextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ChanllengeText_2TextMeshProUGUI == null )
				{
					this.m_E_ChanllengeText_2TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Effect_EndlessChallenge/Effect_GameEnd_ChallengeEnds/EButton_GameEnd_ChallengeEnds/E_ChanllengeText_2");
				}
				return this.m_E_ChanllengeText_2TextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView E_ChanllengeText_2UITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ChanllengeText_2UITextLocalizeMonoView == null )
				{
					this.m_E_ChanllengeText_2UITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "E_Effect_EndlessChallenge/Effect_GameEnd_ChallengeEnds/EButton_GameEnd_ChallengeEnds/E_ChanllengeText_2");
				}
				return this.m_E_ChanllengeText_2UITextLocalizeMonoView;
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

		public UITextLocalizeMonoView E_ChanllengeLevel_TextUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ChanllengeLevel_TextUITextLocalizeMonoView == null )
				{
					this.m_E_ChanllengeLevel_TextUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "E_Effect_PVE/Effect_GameEnd_Model/EButton_ChallengeMode_lose/info_box/E_ChanllengeLevel_Text");
				}
				return this.m_E_ChanllengeLevel_TextUITextLocalizeMonoView;
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

		public UITextLocalizeMonoView E_ChanllengeLevel_Text_2UITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ChanllengeLevel_Text_2UITextLocalizeMonoView == null )
				{
					this.m_E_ChanllengeLevel_Text_2UITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "E_Effect_PVE/Effect_GameEnd_Model/EButton_ChallengeMode_victory/info_box/E_ChanllengeLevel_Text_2");
				}
				return this.m_E_ChanllengeLevel_Text_2UITextLocalizeMonoView;
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
			this.m_E_ChanllengeText_2TextMeshProUGUI = null;
			this.m_E_ChanllengeText_2UITextLocalizeMonoView = null;
			this.m_Effect_GameEnd_ModelButton = null;
			this.m_Effect_GameEnd_ModelImage = null;
			this.m_E_ChanllengeLevel_TextTextMeshProUGUI = null;
			this.m_E_ChanllengeLevel_TextUITextLocalizeMonoView = null;
			this.m_E_ChanllengeLevel_Text_2TextMeshProUGUI = null;
			this.m_E_ChanllengeLevel_Text_2UITextLocalizeMonoView = null;
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
		private TMPro.TextMeshProUGUI m_E_ChanllengeText_2TextMeshProUGUI = null;
		private UITextLocalizeMonoView m_E_ChanllengeText_2UITextLocalizeMonoView = null;
		private UnityEngine.UI.Button m_Effect_GameEnd_ModelButton = null;
		private UnityEngine.UI.Image m_Effect_GameEnd_ModelImage = null;
		private TMPro.TextMeshProUGUI m_E_ChanllengeLevel_TextTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_E_ChanllengeLevel_TextUITextLocalizeMonoView = null;
		private TMPro.TextMeshProUGUI m_E_ChanllengeLevel_Text_2TextMeshProUGUI = null;
		private UITextLocalizeMonoView m_E_ChanllengeLevel_Text_2UITextLocalizeMonoView = null;
		public Transform uiTransform = null;
	}
}
