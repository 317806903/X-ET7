
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
					this.m_E_EnergyButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/title/E_Energy");
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
					this.m_E_EnergyImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_play/E_GameMode/title/E_Energy");
				}
				return this.m_E_EnergyImage;
			}
		}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollList_ChallengeLoopHorizontalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopScrollList_ChallengeLoopHorizontalScrollRect == null )
				{
					this.m_ELoopScrollList_ChallengeLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "E_HomePage/E_play/info/list/ELoopScrollList_Challenge");
				}
				return this.m_ELoopScrollList_ChallengeLoopHorizontalScrollRect;
			}
		}

		public UnityEngine.UI.Button E_SelectButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_SelectButton == null )
				{
					this.m_E_SelectButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_play/info/E_Select");
				}
				return this.m_E_SelectButton;
			}
		}

		public UnityEngine.UI.Image E_SelectImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_SelectImage == null )
				{
					this.m_E_SelectImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_play/info/E_Select");
				}
				return this.m_E_SelectImage;
			}
		}

		public UnityEngine.UI.Button E_UnlockedButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_UnlockedButton == null )
				{
					this.m_E_UnlockedButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_play/info/E_Unlocked");
				}
				return this.m_E_UnlockedButton;
			}
		}

		public UnityEngine.UI.Image E_UnlockedImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_UnlockedImage == null )
				{
					this.m_E_UnlockedImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_play/info/E_Unlocked");
				}
				return this.m_E_UnlockedImage;
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
					this.m_ELoopScrollList_RewardLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "E_HomePage/E_play/info/Reward/list/ELoopScrollList_Reward");
				}
				return this.m_ELoopScrollList_RewardLoopHorizontalScrollRect;
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
					this.m_E_NoneImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_play/info/Reward/list/ELoopScrollList_Reward/Content/Item_GoldCoin/E_None");
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
					this.m_EImage_TowerBuyShowImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_play/info/Reward/list/ELoopScrollList_Reward/Content/Item_GoldCoin/EImage_TowerBuyShow");
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
					this.m_E_BoxImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_play/info/Reward/list/ELoopScrollList_Reward/Content/Item_GoldCoin/EImage_TowerBuyShow/E_Box");
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
					this.m_EImage_LowImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_play/info/Reward/list/ELoopScrollList_Reward/Content/Item_GoldCoin/EImage_TowerBuyShow/E_Box/EImage_Low");
				}
				return this.m_EImage_LowImage;
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
					this.m_EButton_IconButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_HomePage/E_play/info/Reward/list/ELoopScrollList_Reward/Content/Item_GoldCoin/EImage_TowerBuyShow/EButton_Icon");
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
					this.m_EButton_IconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_play/info/Reward/list/ELoopScrollList_Reward/Content/Item_GoldCoin/EImage_TowerBuyShow/EButton_Icon");
				}
				return this.m_EButton_IconImage;
			}
		}

		public TMPro.TextMeshProUGUI EButton_numberTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_numberTextMeshProUGUI == null )
				{
					this.m_EButton_numberTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_HomePage/E_play/info/Reward/list/ELoopScrollList_Reward/Content/Item_GoldCoin/EImage_TowerBuyShow/EButton_number");
				}
				return this.m_EButton_numberTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollList_propLoopHorizontalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopScrollList_propLoopHorizontalScrollRect == null )
				{
					this.m_ELoopScrollList_propLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "E_HomePage/E_play/info/monster/list/ELoopScrollList_prop");
				}
				return this.m_ELoopScrollList_propLoopHorizontalScrollRect;
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
					this.m_EImage_Label1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_HomePage/E_play/info/monster/tips/EImage_Label1");
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
					this.m_ELabel_Label1TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_HomePage/E_play/info/monster/tips/EImage_Label1/ELabel_Label1");
				}
				return this.m_ELabel_Label1TextMeshProUGUI;
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
			this.m_E_EnergyButton = null;
			this.m_E_EnergyImage = null;
			this.m_ELoopScrollList_ChallengeLoopHorizontalScrollRect = null;
			this.m_E_SelectButton = null;
			this.m_E_SelectImage = null;
			this.m_E_UnlockedButton = null;
			this.m_E_UnlockedImage = null;
			this.m_ELoopScrollList_RewardLoopHorizontalScrollRect = null;
			this.m_E_NoneImage = null;
			this.m_EImage_TowerBuyShowImage = null;
			this.m_E_BoxImage = null;
			this.m_EImage_LowImage = null;
			this.m_EButton_IconButton = null;
			this.m_EButton_IconImage = null;
			this.m_EButton_numberTextMeshProUGUI = null;
			this.m_ELoopScrollList_propLoopHorizontalScrollRect = null;
			this.m_EImage_Label1Image = null;
			this.m_ELabel_Label1TextMeshProUGUI = null;
			this.m_E_QuitBattleButton = null;
			this.m_E_QuitBattleImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EG_bgARRectTransform = null;
		private BlurBackground.TranslucentImage m_EG_bgARTranslucentImage = null;
		private UnityEngine.RectTransform m_EG_bgRectTransform = null;
		private UnityEngine.UI.Image m_EG_bgImage = null;
		private UnityEngine.UI.Button m_E_EnergyButton = null;
		private UnityEngine.UI.Image m_E_EnergyImage = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_ChallengeLoopHorizontalScrollRect = null;
		private UnityEngine.UI.Button m_E_SelectButton = null;
		private UnityEngine.UI.Image m_E_SelectImage = null;
		private UnityEngine.UI.Button m_E_UnlockedButton = null;
		private UnityEngine.UI.Image m_E_UnlockedImage = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_RewardLoopHorizontalScrollRect = null;
		private UnityEngine.UI.Image m_E_NoneImage = null;
		private UnityEngine.UI.Image m_EImage_TowerBuyShowImage = null;
		private UnityEngine.UI.Image m_E_BoxImage = null;
		private UnityEngine.UI.Image m_EImage_LowImage = null;
		private UnityEngine.UI.Button m_EButton_IconButton = null;
		private UnityEngine.UI.Image m_EButton_IconImage = null;
		private TMPro.TextMeshProUGUI m_EButton_numberTextMeshProUGUI = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_propLoopHorizontalScrollRect = null;
		private UnityEngine.UI.Image m_EImage_Label1Image = null;
		private TMPro.TextMeshProUGUI m_ELabel_Label1TextMeshProUGUI = null;
		private UnityEngine.UI.Button m_E_QuitBattleButton = null;
		private UnityEngine.UI.Image m_E_QuitBattleImage = null;
		public Transform uiTransform = null;
	}
}
