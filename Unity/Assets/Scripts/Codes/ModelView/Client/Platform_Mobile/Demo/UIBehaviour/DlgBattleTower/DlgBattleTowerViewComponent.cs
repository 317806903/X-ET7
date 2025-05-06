
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgBattleTower))]
	[EnableMethod]
	public class DlgBattleTowerViewComponent : Entity, IAwake, IDestroy
	{
		public UnityEngine.RectTransform EGRootContentRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EGRootContentRectTransform == null )
				{
					this.m_EGRootContentRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGRootContent");
				}
				return this.m_EGRootContentRectTransform;
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
					this.m_EGRootRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGRootContent/EGRoot");
				}
				return this.m_EGRootRectTransform;
			}
		}

		public UnityEngine.UI.Image E_PutHomeAndMonsterPointImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_PutHomeAndMonsterPointImage == null )
				{
					this.m_E_PutHomeAndMonsterPointImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_PutHomeAndMonsterPoint");
				}
				return this.m_E_PutHomeAndMonsterPointImage;
			}
		}

		public UnityEngine.UI.Button EButton_PutHomeButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_PutHomeButton == null )
				{
					this.m_EButton_PutHomeButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_PutHomeAndMonsterPoint/EButton_PutHome");
				}
				return this.m_EButton_PutHomeButton;
			}
		}

		public UnityEngine.UI.Image EButton_PutHomeImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_PutHomeImage == null )
				{
					this.m_EButton_PutHomeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_PutHomeAndMonsterPoint/EButton_PutHome");
				}
				return this.m_EButton_PutHomeImage;
			}
		}

		public UnityEngine.UI.Image EButton_PutHome_iconImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_PutHome_iconImage == null )
				{
					this.m_EButton_PutHome_iconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_PutHomeAndMonsterPoint/EButton_PutHome/EButton_PutHome_icon");
				}
				return this.m_EButton_PutHome_iconImage;
			}
		}

		public UnityEngine.UI.Image EButton_PutHome_icon_TmpImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_PutHome_icon_TmpImage == null )
				{
					this.m_EButton_PutHome_icon_TmpImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_PutHomeAndMonsterPoint/EButton_PutHome/EButton_PutHome_icon_Tmp");
				}
				return this.m_EButton_PutHome_icon_TmpImage;
			}
		}

		public UnityEngine.UI.Button EButton_PutMonsterPointButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_PutMonsterPointButton == null )
				{
					this.m_EButton_PutMonsterPointButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_PutHomeAndMonsterPoint/EButton_PutMonsterPoint");
				}
				return this.m_EButton_PutMonsterPointButton;
			}
		}

		public UnityEngine.UI.Image EButton_PutMonsterPointImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_PutMonsterPointImage == null )
				{
					this.m_EButton_PutMonsterPointImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_PutHomeAndMonsterPoint/EButton_PutMonsterPoint");
				}
				return this.m_EButton_PutMonsterPointImage;
			}
		}

		public UnityEngine.UI.Image ELabel_PutMonsterPoint_iconImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_PutMonsterPoint_iconImage == null )
				{
					this.m_ELabel_PutMonsterPoint_iconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_PutHomeAndMonsterPoint/EButton_PutMonsterPoint/ELabel_PutMonsterPoint_icon");
				}
				return this.m_ELabel_PutMonsterPoint_iconImage;
			}
		}

		public UnityEngine.UI.Image ELabel_PutMonsterPoint_icon_TmpImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_PutMonsterPoint_icon_TmpImage == null )
				{
					this.m_ELabel_PutMonsterPoint_icon_TmpImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_PutHomeAndMonsterPoint/EButton_PutMonsterPoint/ELabel_PutMonsterPoint_icon_Tmp");
				}
				return this.m_ELabel_PutMonsterPoint_icon_TmpImage;
			}
		}

		public UnityEngine.UI.Button EButton_ResetHomeButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_ResetHomeButton == null )
				{
					this.m_EButton_ResetHomeButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_PutHomeAndMonsterPoint/EButton_ResetHome");
				}
				return this.m_EButton_ResetHomeButton;
			}
		}

		public UnityEngine.UI.Image EButton_ResetHomeImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_ResetHomeImage == null )
				{
					this.m_EButton_ResetHomeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_PutHomeAndMonsterPoint/EButton_ResetHome");
				}
				return this.m_EButton_ResetHomeImage;
			}
		}

		public UnityEngine.UI.Image ELabel_ResetHome_iconImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_ResetHome_iconImage == null )
				{
					this.m_ELabel_ResetHome_iconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_PutHomeAndMonsterPoint/EButton_ResetHome/ELabel_ResetHome_icon");
				}
				return this.m_ELabel_ResetHome_iconImage;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_CancelTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_CancelTextMeshProUGUI == null )
				{
					this.m_ELabel_CancelTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_PutHomeAndMonsterPoint/EButton_ResetHome/ELabel_Cancel");
				}
				return this.m_ELabel_CancelTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView ELabel_CancelUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_CancelUITextLocalizeMonoView == null )
				{
					this.m_ELabel_CancelUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_PutHomeAndMonsterPoint/EButton_ResetHome/ELabel_Cancel");
				}
				return this.m_ELabel_CancelUITextLocalizeMonoView;
			}
		}

		public UnityEngine.UI.Button E_CancelButtonButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_CancelButtonButton == null )
				{
					this.m_E_CancelButtonButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_PutHomeAndMonsterPoint/E_CancelButton");
				}
				return this.m_E_CancelButtonButton;
			}
		}

		public UnityEngine.UI.Image E_CancelButtonImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_CancelButtonImage == null )
				{
					this.m_E_CancelButtonImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_PutHomeAndMonsterPoint/E_CancelButton");
				}
				return this.m_E_CancelButtonImage;
			}
		}

		public UnityEngine.UI.Image E_BattleImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BattleImage == null )
				{
					this.m_E_BattleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_Battle");
				}
				return this.m_E_BattleImage;
			}
		}

		public UnityEngine.UI.Image E_CoinsAndTowersImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_CoinsAndTowersImage == null )
				{
					this.m_E_CoinsAndTowersImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_Battle/E_CoinsAndTowers");
				}
				return this.m_E_CoinsAndTowersImage;
			}
		}

		public UnityEngine.UI.Image ELabel_shadow02Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_shadow02Image == null )
				{
					this.m_ELabel_shadow02Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_Battle/E_CoinsAndTowers/ELabel_shadow02");
				}
				return this.m_ELabel_shadow02Image;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_TotalGoldTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_TotalGoldTextMeshProUGUI == null )
				{
					this.m_ELabel_TotalGoldTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_Battle/E_CoinsAndTowers/ELabel_TotalGold");
				}
				return this.m_ELabel_TotalGoldTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView ELabel_TotalGoldUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_TotalGoldUITextLocalizeMonoView == null )
				{
					this.m_ELabel_TotalGoldUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_Battle/E_CoinsAndTowers/ELabel_TotalGold");
				}
				return this.m_ELabel_TotalGoldUITextLocalizeMonoView;
			}
		}

		public UnityEngine.UI.Image ELabel_goldImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_goldImage == null )
				{
					this.m_ELabel_goldImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_Battle/E_CoinsAndTowers/ELabel_gold");
				}
				return this.m_ELabel_goldImage;
			}
		}

		public UnityEngine.UI.Image ELabel_shadowImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_shadowImage == null )
				{
					this.m_ELabel_shadowImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_Battle/E_CoinsAndTowers/ELabel_shadow");
				}
				return this.m_ELabel_shadowImage;
			}
		}

		public UnityEngine.UI.Image ELabel_towerImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_towerImage == null )
				{
					this.m_ELabel_towerImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_Battle/E_CoinsAndTowers/ELabel_tower");
				}
				return this.m_ELabel_towerImage;
			}
		}

		public TMPro.TextMeshProUGUI E_LeftCallPlayerTowerCountTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_LeftCallPlayerTowerCountTextMeshProUGUI == null )
				{
					this.m_E_LeftCallPlayerTowerCountTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_Battle/E_CoinsAndTowers/E_LeftCallPlayerTowerCount");
				}
				return this.m_E_LeftCallPlayerTowerCountTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Image ELabel_LeftMonsterWave_boxImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_LeftMonsterWave_boxImage == null )
				{
					this.m_ELabel_LeftMonsterWave_boxImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_Battle/ELabel_LeftMonsterWave_box");
				}
				return this.m_ELabel_LeftMonsterWave_boxImage;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_LeftMonsterWaveTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_LeftMonsterWaveTextMeshProUGUI == null )
				{
					this.m_ELabel_LeftMonsterWaveTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_Battle/ELabel_LeftMonsterWave_box/ELabel_LeftMonsterWave");
				}
				return this.m_ELabel_LeftMonsterWaveTextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_LeftTimeTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_LeftTimeTextMeshProUGUI == null )
				{
					this.m_ELabel_LeftTimeTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_Battle/ELabel_LeftTime");
				}
				return this.m_ELabel_LeftTimeTextMeshProUGUI;
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
					this.m_EG_ReadyRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_Battle/EG_Ready");
				}
				return this.m_EG_ReadyRectTransform;
			}
		}

		public UnityEngine.UI.Button EButton_ReadyWhenRestTimeButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_ReadyWhenRestTimeButton == null )
				{
					this.m_EButton_ReadyWhenRestTimeButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_Battle/EG_Ready/EButton_ReadyWhenRestTime");
				}
				return this.m_EButton_ReadyWhenRestTimeButton;
			}
		}

		public UnityEngine.UI.Image EButton_ReadyWhenRestTimeImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_ReadyWhenRestTimeImage == null )
				{
					this.m_EButton_ReadyWhenRestTimeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_Battle/EG_Ready/EButton_ReadyWhenRestTime");
				}
				return this.m_EButton_ReadyWhenRestTimeImage;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_ReadyWhenRestTimeTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_ReadyWhenRestTimeTextMeshProUGUI == null )
				{
					this.m_ELabel_ReadyWhenRestTimeTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_Battle/EG_Ready/EButton_ReadyWhenRestTime/ELabel_ReadyWhenRestTime");
				}
				return this.m_ELabel_ReadyWhenRestTimeTextMeshProUGUI;
			}
		}

		public UITextLocalizeMonoView ELabel_ReadyWhenRestTimeUITextLocalizeMonoView
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_ReadyWhenRestTimeUITextLocalizeMonoView == null )
				{
					this.m_ELabel_ReadyWhenRestTimeUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_Battle/EG_Ready/EButton_ReadyWhenRestTime/ELabel_ReadyWhenRestTime");
				}
				return this.m_ELabel_ReadyWhenRestTimeUITextLocalizeMonoView;
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
					Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_Battle/E_list_avatar/ES_AvatarShow");
					this.m_es_avatarshow = this.AddChild<ES_AvatarShow, Transform>(subTrans);
				}
				return this.m_es_avatarshow;
			}
		}

		public UnityEngine.RectTransform EG_BuyNodeRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_BuyNodeRectTransform == null )
				{
					this.m_EG_BuyNodeRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_Battle/EG_BuyNode");
				}
				return this.m_EG_BuyNodeRectTransform;
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
					this.m_EButton_BuyButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_Battle/EG_BuyNode/EButton_Buy");
				}
				return this.m_EButton_BuyButton;
			}
		}

		public UnityEngine.UI.Image EButton_BuyShow_iconImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_BuyShow_iconImage == null )
				{
					this.m_EButton_BuyShow_iconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_Battle/EG_BuyNode/EButton_Buy/EButton_BuyShow_icon");
				}
				return this.m_EButton_BuyShow_iconImage;
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
					this.m_ELabel_BuyText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_Battle/EG_BuyNode/EButton_Buy/ELabel_Buy");
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
					this.m_ELabel_BuyUITextLocalizeMonoView = UIFindHelper.FindDeepChild<UITextLocalizeMonoView>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_Battle/EG_BuyNode/EButton_Buy/ELabel_Buy");
				}
				return this.m_ELabel_BuyUITextLocalizeMonoView;
			}
		}

		public UnityEngine.RectTransform EG_TowerBuyShowRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_TowerBuyShowRectTransform == null )
				{
					this.m_EG_TowerBuyShowRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_Battle/EG_BuyNode/EG_TowerBuyShow");
				}
				return this.m_EG_TowerBuyShowRectTransform;
			}
		}

		public UnityEngine.UI.Image E_TowerBuyShow_boxImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_TowerBuyShow_boxImage == null )
				{
					this.m_E_TowerBuyShow_boxImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_Battle/EG_BuyNode/EG_TowerBuyShow/E_TowerBuyShow_box");
				}
				return this.m_E_TowerBuyShow_boxImage;
			}
		}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollList_BuyLoopHorizontalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopScrollList_BuyLoopHorizontalScrollRect == null )
				{
					this.m_ELoopScrollList_BuyLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_Battle/EG_BuyNode/EG_TowerBuyShow/ELoopScrollList_Buy");
				}
				return this.m_ELoopScrollList_BuyLoopHorizontalScrollRect;
			}
		}

		public UnityEngine.UI.Button EButton_RefreshButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_RefreshButton == null )
				{
					this.m_EButton_RefreshButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_Battle/EG_BuyNode/EG_TowerBuyShow/EButton_Refresh");
				}
				return this.m_EButton_RefreshButton;
			}
		}

		public UnityEngine.UI.Image EButton_RefreshImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_RefreshImage == null )
				{
					this.m_EButton_RefreshImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_Battle/EG_BuyNode/EG_TowerBuyShow/EButton_Refresh");
				}
				return this.m_EButton_RefreshImage;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_RefreshTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_RefreshTextMeshProUGUI == null )
				{
					this.m_ELabel_RefreshTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_Battle/EG_BuyNode/EG_TowerBuyShow/EButton_Refresh/ELabel_Refresh");
				}
				return this.m_ELabel_RefreshTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Image EButton_Refresh_iconImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_Refresh_iconImage == null )
				{
					this.m_EButton_Refresh_iconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_Battle/EG_BuyNode/EG_TowerBuyShow/EButton_Refresh/EButton_Refresh_icon");
				}
				return this.m_EButton_Refresh_iconImage;
			}
		}

		public UnityEngine.UI.Button EButton_BuyCloseButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_BuyCloseButton == null )
				{
					this.m_EButton_BuyCloseButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_Battle/EG_BuyNode/EG_TowerBuyShow/EButton_BuyClose");
				}
				return this.m_EButton_BuyCloseButton;
			}
		}

		public UnityEngine.UI.Image EButton_BuyCloseImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_BuyCloseImage == null )
				{
					this.m_EButton_BuyCloseImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_Battle/EG_BuyNode/EG_TowerBuyShow/EButton_BuyClose");
				}
				return this.m_EButton_BuyCloseImage;
			}
		}

		public UnityEngine.UI.Image EButton_BuyClose_iconImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_BuyClose_iconImage == null )
				{
					this.m_EButton_BuyClose_iconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_Battle/EG_BuyNode/EG_TowerBuyShow/EButton_BuyClose/EButton_BuyClose_icon");
				}
				return this.m_EButton_BuyClose_iconImage;
			}
		}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollList_TowerLoopHorizontalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopScrollList_TowerLoopHorizontalScrollRect == null )
				{
					this.m_ELoopScrollList_TowerLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_Battle/ELoopScrollList_Tower");
				}
				return this.m_ELoopScrollList_TowerLoopHorizontalScrollRect;
			}
		}

		public UnityEngine.UI.Image E_TipNodeImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_TipNodeImage == null )
				{
					this.m_E_TipNodeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRootContent/EGRoot/TipNode/E_TipNode");
				}
				return this.m_E_TipNodeImage;
			}
		}

		public TMPro.TextMeshProUGUI E_TipTextTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_TipTextTextMeshProUGUI == null )
				{
					this.m_E_TipTextTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGRootContent/EGRoot/TipNode/E_TipNode/E_TipText");
				}
				return this.m_E_TipTextTextMeshProUGUI;
			}
		}

		public UnityEngine.RectTransform EGDebugRootRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EGDebugRootRectTransform == null )
				{
					this.m_EGDebugRootRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGRootContent/EGRoot/EGDebugRoot");
				}
				return this.m_EGDebugRootRectTransform;
			}
		}

		public UnityEngine.UI.Button E_ForceGameEndWhenDebugButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ForceGameEndWhenDebugButton == null )
				{
					this.m_E_ForceGameEndWhenDebugButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGRootContent/EGRoot/EGDebugRoot/E_ForceGameEndWhenDebug");
				}
				return this.m_E_ForceGameEndWhenDebugButton;
			}
		}

		public UnityEngine.UI.Image E_ForceGameEndWhenDebugImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ForceGameEndWhenDebugImage == null )
				{
					this.m_E_ForceGameEndWhenDebugImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRootContent/EGRoot/EGDebugRoot/E_ForceGameEndWhenDebug");
				}
				return this.m_E_ForceGameEndWhenDebugImage;
			}
		}

		public UnityEngine.UI.Button E_ForceAddGameGoldWhenDebugButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ForceAddGameGoldWhenDebugButton == null )
				{
					this.m_E_ForceAddGameGoldWhenDebugButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGRootContent/EGRoot/EGDebugRoot/E_ForceAddGameGoldWhenDebug");
				}
				return this.m_E_ForceAddGameGoldWhenDebugButton;
			}
		}

		public UnityEngine.UI.Image E_ForceAddGameGoldWhenDebugImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ForceAddGameGoldWhenDebugImage == null )
				{
					this.m_E_ForceAddGameGoldWhenDebugImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRootContent/EGRoot/EGDebugRoot/E_ForceAddGameGoldWhenDebug");
				}
				return this.m_E_ForceAddGameGoldWhenDebugImage;
			}
		}

		public UnityEngine.UI.Button E_ForceAddHomeHpWhenDebugButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ForceAddHomeHpWhenDebugButton == null )
				{
					this.m_E_ForceAddHomeHpWhenDebugButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGRootContent/EGRoot/EGDebugRoot/E_ForceAddHomeHpWhenDebug");
				}
				return this.m_E_ForceAddHomeHpWhenDebugButton;
			}
		}

		public UnityEngine.UI.Image E_ForceAddHomeHpWhenDebugImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ForceAddHomeHpWhenDebugImage == null )
				{
					this.m_E_ForceAddHomeHpWhenDebugImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRootContent/EGRoot/EGDebugRoot/E_ForceAddHomeHpWhenDebug");
				}
				return this.m_E_ForceAddHomeHpWhenDebugImage;
			}
		}

		public UnityEngine.UI.Button E_ForceNextWaveWhenDebugButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ForceNextWaveWhenDebugButton == null )
				{
					this.m_E_ForceNextWaveWhenDebugButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGRootContent/EGRoot/EGDebugRoot/E_ForceNextWaveWhenDebug");
				}
				return this.m_E_ForceNextWaveWhenDebugButton;
			}
		}

		public UnityEngine.UI.Image E_ForceNextWaveWhenDebugImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ForceNextWaveWhenDebugImage == null )
				{
					this.m_E_ForceNextWaveWhenDebugImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRootContent/EGRoot/EGDebugRoot/E_ForceNextWaveWhenDebug");
				}
				return this.m_E_ForceNextWaveWhenDebugImage;
			}
		}

		public UnityEngine.UI.Button E_GameSettingButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_GameSettingButton == null )
				{
					this.m_E_GameSettingButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_GameSetting");
				}
				return this.m_E_GameSettingButton;
			}
		}

		public UnityEngine.UI.Image E_GameSettingImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_GameSettingImage == null )
				{
					this.m_E_GameSettingImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGRootContent/EGRoot/E_GameSetting");
				}
				return this.m_E_GameSettingImage;
			}
		}

		public UnityEngine.RectTransform EG_GameInstructionsRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_GameInstructionsRectTransform == null )
				{
					this.m_EG_GameInstructionsRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_GameInstructions");
				}
				return this.m_EG_GameInstructionsRectTransform;
			}
		}

		public TMPro.TextMeshProUGUI E_GameInstructionsTextTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_GameInstructionsTextTextMeshProUGUI == null )
				{
					this.m_E_GameInstructionsTextTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_GameInstructions/GameInstructionsNode/E_GameInstructionsText");
				}
				return this.m_E_GameInstructionsTextTextMeshProUGUI;
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
			this.m_EGRootContentRectTransform = null;
			this.m_EGRootRectTransform = null;
			this.m_E_PutHomeAndMonsterPointImage = null;
			this.m_EButton_PutHomeButton = null;
			this.m_EButton_PutHomeImage = null;
			this.m_EButton_PutHome_iconImage = null;
			this.m_EButton_PutHome_icon_TmpImage = null;
			this.m_EButton_PutMonsterPointButton = null;
			this.m_EButton_PutMonsterPointImage = null;
			this.m_ELabel_PutMonsterPoint_iconImage = null;
			this.m_ELabel_PutMonsterPoint_icon_TmpImage = null;
			this.m_EButton_ResetHomeButton = null;
			this.m_EButton_ResetHomeImage = null;
			this.m_ELabel_ResetHome_iconImage = null;
			this.m_ELabel_CancelTextMeshProUGUI = null;
			this.m_ELabel_CancelUITextLocalizeMonoView = null;
			this.m_E_CancelButtonButton = null;
			this.m_E_CancelButtonImage = null;
			this.m_E_BattleImage = null;
			this.m_E_CoinsAndTowersImage = null;
			this.m_ELabel_shadow02Image = null;
			this.m_ELabel_TotalGoldTextMeshProUGUI = null;
			this.m_ELabel_TotalGoldUITextLocalizeMonoView = null;
			this.m_ELabel_goldImage = null;
			this.m_ELabel_shadowImage = null;
			this.m_ELabel_towerImage = null;
			this.m_E_LeftCallPlayerTowerCountTextMeshProUGUI = null;
			this.m_ELabel_LeftMonsterWave_boxImage = null;
			this.m_ELabel_LeftMonsterWaveTextMeshProUGUI = null;
			this.m_ELabel_LeftTimeTextMeshProUGUI = null;
			this.m_EG_ReadyRectTransform = null;
			this.m_EButton_ReadyWhenRestTimeButton = null;
			this.m_EButton_ReadyWhenRestTimeImage = null;
			this.m_ELabel_ReadyWhenRestTimeTextMeshProUGUI = null;
			this.m_ELabel_ReadyWhenRestTimeUITextLocalizeMonoView = null;
			this.m_es_avatarshow?.Dispose();
			this.m_es_avatarshow = null;
			this.m_EG_BuyNodeRectTransform = null;
			this.m_EButton_BuyButton = null;
			this.m_EButton_BuyShow_iconImage = null;
			this.m_ELabel_BuyText = null;
			this.m_ELabel_BuyUITextLocalizeMonoView = null;
			this.m_EG_TowerBuyShowRectTransform = null;
			this.m_E_TowerBuyShow_boxImage = null;
			this.m_ELoopScrollList_BuyLoopHorizontalScrollRect = null;
			this.m_EButton_RefreshButton = null;
			this.m_EButton_RefreshImage = null;
			this.m_ELabel_RefreshTextMeshProUGUI = null;
			this.m_EButton_Refresh_iconImage = null;
			this.m_EButton_BuyCloseButton = null;
			this.m_EButton_BuyCloseImage = null;
			this.m_EButton_BuyClose_iconImage = null;
			this.m_ELoopScrollList_TowerLoopHorizontalScrollRect = null;
			this.m_E_TipNodeImage = null;
			this.m_E_TipTextTextMeshProUGUI = null;
			this.m_EGDebugRootRectTransform = null;
			this.m_E_ForceGameEndWhenDebugButton = null;
			this.m_E_ForceGameEndWhenDebugImage = null;
			this.m_E_ForceAddGameGoldWhenDebugButton = null;
			this.m_E_ForceAddGameGoldWhenDebugImage = null;
			this.m_E_ForceAddHomeHpWhenDebugButton = null;
			this.m_E_ForceAddHomeHpWhenDebugImage = null;
			this.m_E_ForceNextWaveWhenDebugButton = null;
			this.m_E_ForceNextWaveWhenDebugImage = null;
			this.m_E_GameSettingButton = null;
			this.m_E_GameSettingImage = null;
			this.m_EG_GameInstructionsRectTransform = null;
			this.m_E_GameInstructionsTextTextMeshProUGUI = null;
			this.m_EG_OpenAnimationRectTransform = null;
			this.m_EG_CloseAnimationRectTransform = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EGRootContentRectTransform = null;
		private UnityEngine.RectTransform m_EGRootRectTransform = null;
		private UnityEngine.UI.Image m_E_PutHomeAndMonsterPointImage = null;
		private UnityEngine.UI.Button m_EButton_PutHomeButton = null;
		private UnityEngine.UI.Image m_EButton_PutHomeImage = null;
		private UnityEngine.UI.Image m_EButton_PutHome_iconImage = null;
		private UnityEngine.UI.Image m_EButton_PutHome_icon_TmpImage = null;
		private UnityEngine.UI.Button m_EButton_PutMonsterPointButton = null;
		private UnityEngine.UI.Image m_EButton_PutMonsterPointImage = null;
		private UnityEngine.UI.Image m_ELabel_PutMonsterPoint_iconImage = null;
		private UnityEngine.UI.Image m_ELabel_PutMonsterPoint_icon_TmpImage = null;
		private UnityEngine.UI.Button m_EButton_ResetHomeButton = null;
		private UnityEngine.UI.Image m_EButton_ResetHomeImage = null;
		private UnityEngine.UI.Image m_ELabel_ResetHome_iconImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_CancelTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ELabel_CancelUITextLocalizeMonoView = null;
		private UnityEngine.UI.Button m_E_CancelButtonButton = null;
		private UnityEngine.UI.Image m_E_CancelButtonImage = null;
		private UnityEngine.UI.Image m_E_BattleImage = null;
		private UnityEngine.UI.Image m_E_CoinsAndTowersImage = null;
		private UnityEngine.UI.Image m_ELabel_shadow02Image = null;
		private TMPro.TextMeshProUGUI m_ELabel_TotalGoldTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ELabel_TotalGoldUITextLocalizeMonoView = null;
		private UnityEngine.UI.Image m_ELabel_goldImage = null;
		private UnityEngine.UI.Image m_ELabel_shadowImage = null;
		private UnityEngine.UI.Image m_ELabel_towerImage = null;
		private TMPro.TextMeshProUGUI m_E_LeftCallPlayerTowerCountTextMeshProUGUI = null;
		private UnityEngine.UI.Image m_ELabel_LeftMonsterWave_boxImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_LeftMonsterWaveTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ELabel_LeftTimeTextMeshProUGUI = null;
		private UnityEngine.RectTransform m_EG_ReadyRectTransform = null;
		private UnityEngine.UI.Button m_EButton_ReadyWhenRestTimeButton = null;
		private UnityEngine.UI.Image m_EButton_ReadyWhenRestTimeImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_ReadyWhenRestTimeTextMeshProUGUI = null;
		private UITextLocalizeMonoView m_ELabel_ReadyWhenRestTimeUITextLocalizeMonoView = null;
		private ES_AvatarShow m_es_avatarshow = null;
		private UnityEngine.RectTransform m_EG_BuyNodeRectTransform = null;
		private UnityEngine.UI.Button m_EButton_BuyButton = null;
		private UnityEngine.UI.Image m_EButton_BuyShow_iconImage = null;
		private UnityEngine.UI.Text m_ELabel_BuyText = null;
		private UITextLocalizeMonoView m_ELabel_BuyUITextLocalizeMonoView = null;
		private UnityEngine.RectTransform m_EG_TowerBuyShowRectTransform = null;
		private UnityEngine.UI.Image m_E_TowerBuyShow_boxImage = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_BuyLoopHorizontalScrollRect = null;
		private UnityEngine.UI.Button m_EButton_RefreshButton = null;
		private UnityEngine.UI.Image m_EButton_RefreshImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_RefreshTextMeshProUGUI = null;
		private UnityEngine.UI.Image m_EButton_Refresh_iconImage = null;
		private UnityEngine.UI.Button m_EButton_BuyCloseButton = null;
		private UnityEngine.UI.Image m_EButton_BuyCloseImage = null;
		private UnityEngine.UI.Image m_EButton_BuyClose_iconImage = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_TowerLoopHorizontalScrollRect = null;
		private UnityEngine.UI.Image m_E_TipNodeImage = null;
		private TMPro.TextMeshProUGUI m_E_TipTextTextMeshProUGUI = null;
		private UnityEngine.RectTransform m_EGDebugRootRectTransform = null;
		private UnityEngine.UI.Button m_E_ForceGameEndWhenDebugButton = null;
		private UnityEngine.UI.Image m_E_ForceGameEndWhenDebugImage = null;
		private UnityEngine.UI.Button m_E_ForceAddGameGoldWhenDebugButton = null;
		private UnityEngine.UI.Image m_E_ForceAddGameGoldWhenDebugImage = null;
		private UnityEngine.UI.Button m_E_ForceAddHomeHpWhenDebugButton = null;
		private UnityEngine.UI.Image m_E_ForceAddHomeHpWhenDebugImage = null;
		private UnityEngine.UI.Button m_E_ForceNextWaveWhenDebugButton = null;
		private UnityEngine.UI.Image m_E_ForceNextWaveWhenDebugImage = null;
		private UnityEngine.UI.Button m_E_GameSettingButton = null;
		private UnityEngine.UI.Image m_E_GameSettingImage = null;
		private UnityEngine.RectTransform m_EG_GameInstructionsRectTransform = null;
		private TMPro.TextMeshProUGUI m_E_GameInstructionsTextTextMeshProUGUI = null;
		private UnityEngine.RectTransform m_EG_OpenAnimationRectTransform = null;
		private UnityEngine.RectTransform m_EG_CloseAnimationRectTransform = null;
		public Transform uiTransform = null;
	}
}
