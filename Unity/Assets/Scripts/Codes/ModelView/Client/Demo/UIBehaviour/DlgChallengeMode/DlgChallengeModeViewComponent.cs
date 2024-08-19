
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgChallengeMode))]
	[EnableMethod]
	public class DlgChallengeModeViewComponent : Entity, IAwake, IDestroy
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

		public UnityEngine.UI.Button EBtnRegularButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EBtnRegularButton == null )
				{
					this.m_EBtnRegularButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/titleBK/TabRegularRoot/EBtnRegular");
				}
				return this.m_EBtnRegularButton;
			}
		}

		public UnityEngine.UI.Image EBtnRegularImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EBtnRegularImage == null )
				{
					this.m_EBtnRegularImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/titleBK/TabRegularRoot/EBtnRegular");
				}
				return this.m_EBtnRegularImage;
			}
		}

		public TMPro.TextMeshProUGUI ERegularTxtTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ERegularTxtTextMeshProUGUI == null )
				{
					this.m_ERegularTxtTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/titleBK/TabRegularRoot/EBtnRegular/ERegularTxt");
				}
				return this.m_ERegularTxtTextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI ETxtinfoTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtinfoTextMeshProUGUI == null )
				{
					this.m_ETxtinfoTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/titleBK/TabRegularRoot/EBtnRegular/ETxtinfo");
				}
				return this.m_ETxtinfoTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Button EBtnRegular_UnselectedButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EBtnRegular_UnselectedButton == null )
				{
					this.m_EBtnRegular_UnselectedButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/titleBK/TabRegularRoot/EBtnRegular_Unselected");
				}
				return this.m_EBtnRegular_UnselectedButton;
			}
		}

		public UnityEngine.UI.Image EBtnRegular_UnselectedImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EBtnRegular_UnselectedImage == null )
				{
					this.m_EBtnRegular_UnselectedImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/titleBK/TabRegularRoot/EBtnRegular_Unselected");
				}
				return this.m_EBtnRegular_UnselectedImage;
			}
		}

		public TMPro.TextMeshProUGUI ERegularTxtUnselectedTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ERegularTxtUnselectedTextMeshProUGUI == null )
				{
					this.m_ERegularTxtUnselectedTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/titleBK/TabRegularRoot/EBtnRegular_Unselected/ERegularTxtUnselected");
				}
				return this.m_ERegularTxtUnselectedTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Button EBtnSeasonButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EBtnSeasonButton == null )
				{
					this.m_EBtnSeasonButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/titleBK/TabSeasonRoot/EBtnSeason");
				}
				return this.m_EBtnSeasonButton;
			}
		}

		public UnityEngine.UI.Image EBtnSeasonImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EBtnSeasonImage == null )
				{
					this.m_EBtnSeasonImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/titleBK/TabSeasonRoot/EBtnSeason");
				}
				return this.m_EBtnSeasonImage;
			}
		}

		public TMPro.TextMeshProUGUI ETxtTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtTextMeshProUGUI == null )
				{
					this.m_ETxtTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/titleBK/TabSeasonRoot/EBtnSeason/ETxt");
				}
				return this.m_ETxtTextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI ETxtSeasonDesTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtSeasonDesTextMeshProUGUI == null )
				{
					this.m_ETxtSeasonDesTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/titleBK/TabSeasonRoot/EBtnSeason/ETxtDes/ETxtSeasonDes");
				}
				return this.m_ETxtSeasonDesTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Button EBtnSeason_UnselectedButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EBtnSeason_UnselectedButton == null )
				{
					this.m_EBtnSeason_UnselectedButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/titleBK/TabSeasonRoot/EBtnSeason_Unselected");
				}
				return this.m_EBtnSeason_UnselectedButton;
			}
		}

		public UnityEngine.UI.Image EBtnSeason_UnselectedImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EBtnSeason_UnselectedImage == null )
				{
					this.m_EBtnSeason_UnselectedImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/titleBK/TabSeasonRoot/EBtnSeason_Unselected");
				}
				return this.m_EBtnSeason_UnselectedImage;
			}
		}

		public TMPro.TextMeshProUGUI ETxtUnselectedTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ETxtUnselectedTextMeshProUGUI == null )
				{
					this.m_ETxtUnselectedTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/titleBK/TabSeasonRoot/EBtnSeason_Unselected/ETxtUnselected");
				}
				return this.m_ETxtUnselectedTextMeshProUGUI;
			}
		}

		public EPage_ChallengSeason EPage_ChallengSeason
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_epage_challengseason == null )
				{
					Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject, "E_HomePage/E_play/EPage_ChallengSeason");
					this.m_epage_challengseason = this.AddChild<EPage_ChallengSeason, Transform>(subTrans);
				}
				return this.m_epage_challengseason;
			}
		}

		public EPage_ChallengNormal EPage_ChallengNormal
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_epage_challengnormal == null )
				{
					Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject, "E_HomePage/E_play/EPage_ChallengNormal");
					this.m_epage_challengnormal = this.AddChild<EPage_ChallengNormal, Transform>(subTrans);
				}
				return this.m_epage_challengnormal;
			}
		}

		public UnityEngine.UI.Button E_QuitBattleButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_QuitBattleButton == null )
				{
					this.m_E_QuitBattleButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_QuitBattle");
				}
				return this.m_E_QuitBattleButton;
			}
		}

		public UnityEngine.UI.Image E_QuitBattleImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_QuitBattleImage == null )
				{
					this.m_E_QuitBattleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_QuitBattle");
				}
				return this.m_E_QuitBattleImage;
			}
		}

		public void DestroyWidget()
		{
			this.m_EG_bgARRectTransform = null;
			this.m_EG_bgARTranslucentImage = null;
			this.m_EG_bgRectTransform = null;
			this.m_EG_bgImage = null;
			this.m_EBtnRegularButton = null;
			this.m_EBtnRegularImage = null;
			this.m_ERegularTxtTextMeshProUGUI = null;
			this.m_ETxtinfoTextMeshProUGUI = null;
			this.m_EBtnRegular_UnselectedButton = null;
			this.m_EBtnRegular_UnselectedImage = null;
			this.m_ERegularTxtUnselectedTextMeshProUGUI = null;
			this.m_EBtnSeasonButton = null;
			this.m_EBtnSeasonImage = null;
			this.m_ETxtTextMeshProUGUI = null;
			this.m_ETxtSeasonDesTextMeshProUGUI = null;
			this.m_EBtnSeason_UnselectedButton = null;
			this.m_EBtnSeason_UnselectedImage = null;
			this.m_ETxtUnselectedTextMeshProUGUI = null;
			this.m_epage_challengseason?.Dispose();
			this.m_epage_challengseason = null;
			this.m_epage_challengnormal?.Dispose();
			this.m_epage_challengnormal = null;
			this.m_E_QuitBattleButton = null;
			this.m_E_QuitBattleImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EG_bgARRectTransform = null;
		private BlurBackground.TranslucentImage m_EG_bgARTranslucentImage = null;
		private UnityEngine.RectTransform m_EG_bgRectTransform = null;
		private UnityEngine.UI.Image m_EG_bgImage = null;
		private UnityEngine.UI.Button m_EBtnRegularButton = null;
		private UnityEngine.UI.Image m_EBtnRegularImage = null;
		private TMPro.TextMeshProUGUI m_ERegularTxtTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ETxtinfoTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_EBtnRegular_UnselectedButton = null;
		private UnityEngine.UI.Image m_EBtnRegular_UnselectedImage = null;
		private TMPro.TextMeshProUGUI m_ERegularTxtUnselectedTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_EBtnSeasonButton = null;
		private UnityEngine.UI.Image m_EBtnSeasonImage = null;
		private TMPro.TextMeshProUGUI m_ETxtTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ETxtSeasonDesTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_EBtnSeason_UnselectedButton = null;
		private UnityEngine.UI.Image m_EBtnSeason_UnselectedImage = null;
		private TMPro.TextMeshProUGUI m_ETxtUnselectedTextMeshProUGUI = null;
		private EPage_ChallengSeason m_epage_challengseason = null;
		private EPage_ChallengNormal m_epage_challengnormal = null;
		private UnityEngine.UI.Button m_E_QuitBattleButton = null;
		private UnityEngine.UI.Image m_E_QuitBattleImage = null;
		public Transform uiTransform = null;
	}
}
