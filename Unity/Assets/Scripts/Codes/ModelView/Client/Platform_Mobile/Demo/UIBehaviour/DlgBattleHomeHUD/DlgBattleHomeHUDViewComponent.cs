
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgBattleHomeHUD))]
	[EnableMethod]
	public class DlgBattleHomeHUDViewComponent : Entity, IAwake, IDestroy
	{
		public UnityEngine.UI.Button E_Sprite_BGButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Sprite_BGButton == null )
				{
					this.m_E_Sprite_BGButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Sprite_BG");
				}
				return this.m_E_Sprite_BGButton;
			}
		}

		public UnityEngine.UI.Image E_Sprite_BGImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Sprite_BGImage == null )
				{
					this.m_E_Sprite_BGImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Sprite_BG");
				}
				return this.m_E_Sprite_BGImage;
			}
		}

		public UnityEngine.RectTransform EG_OperatorMenuRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_OperatorMenuRectTransform == null )
				{
					this.m_EG_OperatorMenuRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_OperatorMenu");
				}
				return this.m_EG_OperatorMenuRectTransform;
			}
		}

		public UnityEngine.UI.Image EG_OperatorMenuImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_OperatorMenuImage == null )
				{
					this.m_EG_OperatorMenuImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_OperatorMenu");
				}
				return this.m_EG_OperatorMenuImage;
			}
		}

		public UnityEngine.RectTransform EG_OperatorRootRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_OperatorRootRectTransform == null )
				{
					this.m_EG_OperatorRootRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_OperatorMenu/EG_OperatorRoot");
				}
				return this.m_EG_OperatorRootRectTransform;
			}
		}

		public UIGuide.EventTriggerListener EG_OperatorRootEventTriggerListener
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_OperatorRootEventTriggerListener == null )
				{
					this.m_EG_OperatorRootEventTriggerListener = UIFindHelper.FindDeepChild<UIGuide.EventTriggerListener>(this.uiTransform.gameObject, "EG_OperatorMenu/EG_OperatorRoot");
				}
				return this.m_EG_OperatorRootEventTriggerListener;
			}
		}

		public UnityEngine.UI.Button EButton_SaleButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_SaleButton == null )
				{
					this.m_EButton_SaleButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG_OperatorMenu/EG_OperatorRoot/Sell/EButton_Sale");
				}
				return this.m_EButton_SaleButton;
			}
		}

		public UnityEngine.UI.Image EButton_SaleImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_SaleImage == null )
				{
					this.m_EButton_SaleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_OperatorMenu/EG_OperatorRoot/Sell/EButton_Sale");
				}
				return this.m_EButton_SaleImage;
			}
		}

		public UnityEngine.UI.Button EButton_ConfirmButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_ConfirmButton == null )
				{
					this.m_EButton_ConfirmButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG_OperatorMenu/EG_OperatorRoot/Sell/EButton_Confirm");
				}
				return this.m_EButton_ConfirmButton;
			}
		}

		public UnityEngine.UI.Image EButton_ConfirmImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_ConfirmImage == null )
				{
					this.m_EButton_ConfirmImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_OperatorMenu/EG_OperatorRoot/Sell/EButton_Confirm");
				}
				return this.m_EButton_ConfirmImage;
			}
		}

		public TMPro.TextMeshProUGUI E_SaleMoney_textTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_SaleMoney_textTextMeshProUGUI == null )
				{
					this.m_E_SaleMoney_textTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_OperatorMenu/EG_OperatorRoot/Sell/money/E_SaleMoney_text");
				}
				return this.m_E_SaleMoney_textTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Button E_ReclaimButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ReclaimButton == null )
				{
					this.m_E_ReclaimButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG_OperatorMenu/EG_OperatorRoot/E_Reclaim");
				}
				return this.m_E_ReclaimButton;
			}
		}

		public UnityEngine.UI.Image E_ReclaimImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ReclaimImage == null )
				{
					this.m_E_ReclaimImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_OperatorMenu/EG_OperatorRoot/E_Reclaim");
				}
				return this.m_E_ReclaimImage;
			}
		}

		public UnityEngine.UI.Button E_UpgradeButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_UpgradeButton == null )
				{
					this.m_E_UpgradeButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG_OperatorMenu/EG_OperatorRoot/E_Upgrade");
				}
				return this.m_E_UpgradeButton;
			}
		}

		public UnityEngine.UI.Image E_UpgradeImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_UpgradeImage == null )
				{
					this.m_E_UpgradeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_OperatorMenu/EG_OperatorRoot/E_Upgrade");
				}
				return this.m_E_UpgradeImage;
			}
		}

		public UnityEngine.UI.Image E_Upgrade_number_iconImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Upgrade_number_iconImage == null )
				{
					this.m_E_Upgrade_number_iconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_OperatorMenu/EG_OperatorRoot/E_Upgrade/number/E_Upgrade_number_icon");
				}
				return this.m_E_Upgrade_number_iconImage;
			}
		}

		public TMPro.TextMeshProUGUI E_Upgrade_number_TextTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Upgrade_number_TextTextMeshProUGUI == null )
				{
					this.m_E_Upgrade_number_TextTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_OperatorMenu/EG_OperatorRoot/E_Upgrade/number/E_Upgrade_number_Text");
				}
				return this.m_E_Upgrade_number_TextTextMeshProUGUI;
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
			this.m_E_Sprite_BGButton = null;
			this.m_E_Sprite_BGImage = null;
			this.m_EG_OperatorMenuRectTransform = null;
			this.m_EG_OperatorMenuImage = null;
			this.m_EG_OperatorRootRectTransform = null;
			this.m_EG_OperatorRootEventTriggerListener = null;
			this.m_EButton_SaleButton = null;
			this.m_EButton_SaleImage = null;
			this.m_EButton_ConfirmButton = null;
			this.m_EButton_ConfirmImage = null;
			this.m_E_SaleMoney_textTextMeshProUGUI = null;
			this.m_E_ReclaimButton = null;
			this.m_E_ReclaimImage = null;
			this.m_E_UpgradeButton = null;
			this.m_E_UpgradeImage = null;
			this.m_E_Upgrade_number_iconImage = null;
			this.m_E_Upgrade_number_TextTextMeshProUGUI = null;
			this.m_EG_OpenAnimationRectTransform = null;
			this.m_EG_CloseAnimationRectTransform = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_Sprite_BGButton = null;
		private UnityEngine.UI.Image m_E_Sprite_BGImage = null;
		private UnityEngine.RectTransform m_EG_OperatorMenuRectTransform = null;
		private UnityEngine.UI.Image m_EG_OperatorMenuImage = null;
		private UnityEngine.RectTransform m_EG_OperatorRootRectTransform = null;
		private UIGuide.EventTriggerListener m_EG_OperatorRootEventTriggerListener = null;
		private UnityEngine.UI.Button m_EButton_SaleButton = null;
		private UnityEngine.UI.Image m_EButton_SaleImage = null;
		private UnityEngine.UI.Button m_EButton_ConfirmButton = null;
		private UnityEngine.UI.Image m_EButton_ConfirmImage = null;
		private TMPro.TextMeshProUGUI m_E_SaleMoney_textTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_E_ReclaimButton = null;
		private UnityEngine.UI.Image m_E_ReclaimImage = null;
		private UnityEngine.UI.Button m_E_UpgradeButton = null;
		private UnityEngine.UI.Image m_E_UpgradeImage = null;
		private UnityEngine.UI.Image m_E_Upgrade_number_iconImage = null;
		private TMPro.TextMeshProUGUI m_E_Upgrade_number_TextTextMeshProUGUI = null;
		private UnityEngine.RectTransform m_EG_OpenAnimationRectTransform = null;
		private UnityEngine.RectTransform m_EG_CloseAnimationRectTransform = null;
		public Transform uiTransform = null;
	}
}
