
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgBattleTowerEnd))]
	[EnableMethod]
	public class DlgBattleTowerEndViewComponent : Entity, IAwake, IDestroy
	{
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
					this.m_E_ChanllengeText_1TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Effect_EndlessChallenge/Effect_GameEnd_ChallengeEnds/EButton_GameEnd_ChallengeEnds/E_ChanllengeText_1");
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
					this.m_E_ChanllengeText_1UITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "E_Effect_EndlessChallenge/Effect_GameEnd_ChallengeEnds/EButton_GameEnd_ChallengeEnds/E_ChanllengeText_1");
				}
				return this.m_E_ChanllengeText_1UITextLocalizeMonoView;
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

		public void DestroyWidget()
		{
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
			this.m_E_Effect_EndlessChallengeImage = null;
			this.m_Effect_GameEnd_ChallengeEndsButton = null;
			this.m_Effect_GameEnd_ChallengeEndsImage = null;
			this.m_E_ChanllengeText_1TextMeshProUGUI = null;
			this.m_E_ChanllengeText_1UITextLocalizeMonoView = null;
			this.m_E_ChanllengeText_2TextMeshProUGUI = null;
			this.m_E_ChanllengeText_2UITextLocalizeMonoView = null;
			this.m_Effect_GameEnd_ModelButton = null;
			this.m_Effect_GameEnd_ModelImage = null;
			this.uiTransform = null;
		}

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
		private UnityEngine.UI.Image m_E_Effect_EndlessChallengeImage = null;
		private UnityEngine.UI.Button m_Effect_GameEnd_ChallengeEndsButton = null;
		private UnityEngine.UI.Image m_Effect_GameEnd_ChallengeEndsImage = null;
		private TMPro.TextMeshProUGUI m_E_ChanllengeText_1TextMeshProUGUI = null;
		private UITextLocalizeMonoView m_E_ChanllengeText_1UITextLocalizeMonoView = null;
		private TMPro.TextMeshProUGUI m_E_ChanllengeText_2TextMeshProUGUI = null;
		private UITextLocalizeMonoView m_E_ChanllengeText_2UITextLocalizeMonoView = null;
		private UnityEngine.UI.Button m_Effect_GameEnd_ModelButton = null;
		private UnityEngine.UI.Image m_Effect_GameEnd_ModelImage = null;
		public Transform uiTransform = null;
	}
}
