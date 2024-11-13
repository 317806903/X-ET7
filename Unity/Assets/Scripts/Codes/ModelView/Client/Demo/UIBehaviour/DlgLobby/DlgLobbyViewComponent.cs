
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgLobby))]
	[EnableMethod]
	public class DlgLobbyViewComponent : Entity, IAwake, IDestroy
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

		public UnityEngine.UI.Image EGBackGroundImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EGBackGroundImage == null )
				{
					this.m_EGBackGroundImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround");
				}
				return this.m_EGBackGroundImage;
			}
		}

		public UnityEngine.UI.Button E_EnterMapButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_EnterMapButton == null )
				{
					this.m_E_EnterMapButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/E_EnterMap");
				}
				return this.m_E_EnterMapButton;
			}
		}

		public UnityEngine.UI.Image E_EnterMapImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_EnterMapImage == null )
				{
					this.m_E_EnterMapImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/E_EnterMap");
				}
				return this.m_E_EnterMapImage;
			}
		}

		public UnityEngine.UI.Button E_ReturnLoginButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ReturnLoginButton == null )
				{
					this.m_E_ReturnLoginButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/E_ReturnLogin");
				}
				return this.m_E_ReturnLoginButton;
			}
		}

		public UnityEngine.UI.Image E_ReturnLoginImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ReturnLoginImage == null )
				{
					this.m_E_ReturnLoginImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/E_ReturnLogin");
				}
				return this.m_E_ReturnLoginImage;
			}
		}

		public UnityEngine.UI.Button EButton_ChooseBattleCfgButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_ChooseBattleCfgButton == null )
				{
					this.m_EButton_ChooseBattleCfgButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/EButton_ChooseBattleCfg");
				}
				return this.m_EButton_ChooseBattleCfgButton;
			}
		}

		public UnityEngine.UI.Image EButton_ChooseBattleCfgImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_ChooseBattleCfgImage == null )
				{
					this.m_EButton_ChooseBattleCfgImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/EButton_ChooseBattleCfg");
				}
				return this.m_EButton_ChooseBattleCfgImage;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_BattleCfgIdTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_BattleCfgIdTextMeshProUGUI == null )
				{
					this.m_ELabel_BattleCfgIdTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EGBackGround/ELabel_BattleCfgId");
				}
				return this.m_ELabel_BattleCfgIdTextMeshProUGUI;
			}
		}

		public UnityEngine.RectTransform EG_ChgBattleDeckRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_ChgBattleDeckRectTransform == null )
				{
					this.m_EG_ChgBattleDeckRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EGBackGround/EG_ChgBattleDeck");
				}
				return this.m_EG_ChgBattleDeckRectTransform;
			}
		}

		public UnityEngine.UI.Button EButton_ChgBattleDeckButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_ChgBattleDeckButton == null )
				{
					this.m_EButton_ChgBattleDeckButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EGBackGround/EG_ChgBattleDeck/EButton_ChgBattleDeck");
				}
				return this.m_EButton_ChgBattleDeckButton;
			}
		}

		public UnityEngine.UI.Image EButton_ChgBattleDeckImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_ChgBattleDeckImage == null )
				{
					this.m_EButton_ChgBattleDeckImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EGBackGround/EG_ChgBattleDeck/EButton_ChgBattleDeck");
				}
				return this.m_EButton_ChgBattleDeckImage;
			}
		}

		public void DestroyWidget()
		{
			this.m_EGBackGroundRectTransform = null;
			this.m_EGBackGroundImage = null;
			this.m_E_EnterMapButton = null;
			this.m_E_EnterMapImage = null;
			this.m_E_ReturnLoginButton = null;
			this.m_E_ReturnLoginImage = null;
			this.m_EButton_ChooseBattleCfgButton = null;
			this.m_EButton_ChooseBattleCfgImage = null;
			this.m_ELabel_BattleCfgIdTextMeshProUGUI = null;
			this.m_EG_ChgBattleDeckRectTransform = null;
			this.m_EButton_ChgBattleDeckButton = null;
			this.m_EButton_ChgBattleDeckImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EGBackGroundRectTransform = null;
		private UnityEngine.UI.Image m_EGBackGroundImage = null;
		private UnityEngine.UI.Button m_E_EnterMapButton = null;
		private UnityEngine.UI.Image m_E_EnterMapImage = null;
		private UnityEngine.UI.Button m_E_ReturnLoginButton = null;
		private UnityEngine.UI.Image m_E_ReturnLoginImage = null;
		private UnityEngine.UI.Button m_EButton_ChooseBattleCfgButton = null;
		private UnityEngine.UI.Image m_EButton_ChooseBattleCfgImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_BattleCfgIdTextMeshProUGUI = null;
		private UnityEngine.RectTransform m_EG_ChgBattleDeckRectTransform = null;
		private UnityEngine.UI.Button m_EButton_ChgBattleDeckButton = null;
		private UnityEngine.UI.Image m_EButton_ChgBattleDeckImage = null;
		public Transform uiTransform = null;
	}
}
