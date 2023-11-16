
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgBattleTowerHUD))]
	[EnableMethod]
	public class DlgBattleTowerHUDViewComponent : Entity, IAwake, IDestroy
	{
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

		public TMPro.TextMeshProUGUI E_TowerNameTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_TowerNameTextMeshProUGUI == null )
				{
					this.m_E_TowerNameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_OperatorMenu/Image/E_TowerName");
				}
				return this.m_E_TowerNameTextMeshProUGUI;
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
					this.m_E_IconStar1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_OperatorMenu/Image/E_IconStar/E_IconStar1");
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
					this.m_E_IconStar2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_OperatorMenu/Image/E_IconStar/E_IconStar2");
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
					this.m_E_IconStar3Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_OperatorMenu/Image/E_IconStar/E_IconStar3");
				}
				return this.m_E_IconStar3Image;
			}
		}

		public UnityEngine.UI.Button E_SaleButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_SaleButton == null )
				{
					this.m_E_SaleButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG_OperatorMenu/OperatorRoot/E_Sale");
				}
				return this.m_E_SaleButton;
			}
		}

		public UnityEngine.UI.Image E_SaleImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_SaleImage == null )
				{
					this.m_E_SaleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_OperatorMenu/OperatorRoot/E_Sale");
				}
				return this.m_E_SaleImage;
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
					this.m_E_SaleMoney_textTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_OperatorMenu/OperatorRoot/E_Sale/money/E_SaleMoney_text");
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
					this.m_E_ReclaimButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG_OperatorMenu/OperatorRoot/E_Reclaim");
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
					this.m_E_ReclaimImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_OperatorMenu/OperatorRoot/E_Reclaim");
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
					this.m_E_UpgradeButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG_OperatorMenu/OperatorRoot/E_Upgrade");
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
					this.m_E_UpgradeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_OperatorMenu/OperatorRoot/E_Upgrade");
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
					this.m_E_Upgrade_number_iconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_OperatorMenu/OperatorRoot/E_Upgrade/number/E_Upgrade_number_icon");
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
					this.m_E_Upgrade_number_TextTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_OperatorMenu/OperatorRoot/E_Upgrade/number/E_Upgrade_number_Text");
				}
				return this.m_E_Upgrade_number_TextTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Button E_NotUpgradeableButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_NotUpgradeableButton == null )
				{
					this.m_E_NotUpgradeableButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG_OperatorMenu/OperatorRoot/E_NotUpgradeable");
				}
				return this.m_E_NotUpgradeableButton;
			}
		}

		public UnityEngine.UI.Image E_NotUpgradeableImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_NotUpgradeableImage == null )
				{
					this.m_E_NotUpgradeableImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_OperatorMenu/OperatorRoot/E_NotUpgradeable");
				}
				return this.m_E_NotUpgradeableImage;
			}
		}

		public UnityEngine.RectTransform EG_MyTowerDescRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_MyTowerDescRectTransform == null )
				{
					this.m_EG_MyTowerDescRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_MyTowerDesc");
				}
				return this.m_EG_MyTowerDescRectTransform;
			}
		}

		public UnityEngine.UI.Image EG_MyTowerDescImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_MyTowerDescImage == null )
				{
					this.m_EG_MyTowerDescImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_MyTowerDesc");
				}
				return this.m_EG_MyTowerDescImage;
			}
		}

		public UnityEngine.UI.Image E_IconImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_IconImage == null )
				{
					this.m_E_IconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_MyTowerDesc/E_Icon");
				}
				return this.m_E_IconImage;
			}
		}

		public TMPro.TextMeshProUGUI E_TextDescTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_TextDescTextMeshProUGUI == null )
				{
					this.m_E_TextDescTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_MyTowerDesc/Scroll View/Viewport/Content/E_TextDesc");
				}
				return this.m_E_TextDescTextMeshProUGUI;
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
					this.m_EImage_Label1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_MyTowerDesc/EImage_Label1");
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
					this.m_ELabel_Label1TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_MyTowerDesc/EImage_Label1/ELabel_Label1");
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
					this.m_EImage_Label2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_MyTowerDesc/EImage_Label2");
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
					this.m_ELabel_Label2TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_MyTowerDesc/EImage_Label2/ELabel_Label2");
				}
				return this.m_ELabel_Label2TextMeshProUGUI;
			}
		}

		public UnityEngine.RectTransform EG_OtherTowerDescRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_OtherTowerDescRectTransform == null )
				{
					this.m_EG_OtherTowerDescRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_OtherTowerDesc");
				}
				return this.m_EG_OtherTowerDescRectTransform;
			}
		}

		public UnityEngine.UI.Image EG_OtherTowerDescImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_OtherTowerDescImage == null )
				{
					this.m_EG_OtherTowerDescImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_OtherTowerDesc");
				}
				return this.m_EG_OtherTowerDescImage;
			}
		}

		public UnityEngine.UI.Image E_OtherIconImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_OtherIconImage == null )
				{
					this.m_E_OtherIconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_OtherTowerDesc/E_OtherIcon");
				}
				return this.m_E_OtherIconImage;
			}
		}

		public TMPro.TextMeshProUGUI E_OtherTowerNameTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_OtherTowerNameTextMeshProUGUI == null )
				{
					this.m_E_OtherTowerNameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_OtherTowerDesc/Image/E_OtherTowerName");
				}
				return this.m_E_OtherTowerNameTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Image E_OtherIconStar1Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_OtherIconStar1Image == null )
				{
					this.m_E_OtherIconStar1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_OtherTowerDesc/Image/E_OtherIconStar/E_OtherIconStar1");
				}
				return this.m_E_OtherIconStar1Image;
			}
		}

		public UnityEngine.UI.Image E_OtherIconStar2Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_OtherIconStar2Image == null )
				{
					this.m_E_OtherIconStar2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_OtherTowerDesc/Image/E_OtherIconStar/E_OtherIconStar2");
				}
				return this.m_E_OtherIconStar2Image;
			}
		}

		public UnityEngine.UI.Image E_OtherIconStar3Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_OtherIconStar3Image == null )
				{
					this.m_E_OtherIconStar3Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_OtherTowerDesc/Image/E_OtherIconStar/E_OtherIconStar3");
				}
				return this.m_E_OtherIconStar3Image;
			}
		}

		public TMPro.TextMeshProUGUI E_TextOtherDescTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_TextOtherDescTextMeshProUGUI == null )
				{
					this.m_E_TextOtherDescTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_OtherTowerDesc/Scroll View/Viewport/Content/E_TextOtherDesc");
				}
				return this.m_E_TextOtherDescTextMeshProUGUI;
			}
		}

		public void DestroyWidget()
		{
			this.m_E_Sprite_BGImage = null;
			this.m_EG_OperatorMenuRectTransform = null;
			this.m_EG_OperatorMenuImage = null;
			this.m_E_TowerNameTextMeshProUGUI = null;
			this.m_E_IconStar1Image = null;
			this.m_E_IconStar2Image = null;
			this.m_E_IconStar3Image = null;
			this.m_E_SaleButton = null;
			this.m_E_SaleImage = null;
			this.m_E_SaleMoney_textTextMeshProUGUI = null;
			this.m_E_ReclaimButton = null;
			this.m_E_ReclaimImage = null;
			this.m_E_UpgradeButton = null;
			this.m_E_UpgradeImage = null;
			this.m_E_Upgrade_number_iconImage = null;
			this.m_E_Upgrade_number_TextTextMeshProUGUI = null;
			this.m_E_NotUpgradeableButton = null;
			this.m_E_NotUpgradeableImage = null;
			this.m_EG_MyTowerDescRectTransform = null;
			this.m_EG_MyTowerDescImage = null;
			this.m_E_IconImage = null;
			this.m_E_TextDescTextMeshProUGUI = null;
			this.m_EImage_Label1Image = null;
			this.m_ELabel_Label1TextMeshProUGUI = null;
			this.m_EImage_Label2Image = null;
			this.m_ELabel_Label2TextMeshProUGUI = null;
			this.m_EG_OtherTowerDescRectTransform = null;
			this.m_EG_OtherTowerDescImage = null;
			this.m_E_OtherIconImage = null;
			this.m_E_OtherTowerNameTextMeshProUGUI = null;
			this.m_E_OtherIconStar1Image = null;
			this.m_E_OtherIconStar2Image = null;
			this.m_E_OtherIconStar3Image = null;
			this.m_E_TextOtherDescTextMeshProUGUI = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_Sprite_BGImage = null;
		private UnityEngine.RectTransform m_EG_OperatorMenuRectTransform = null;
		private UnityEngine.UI.Image m_EG_OperatorMenuImage = null;
		private TMPro.TextMeshProUGUI m_E_TowerNameTextMeshProUGUI = null;
		private UnityEngine.UI.Image m_E_IconStar1Image = null;
		private UnityEngine.UI.Image m_E_IconStar2Image = null;
		private UnityEngine.UI.Image m_E_IconStar3Image = null;
		private UnityEngine.UI.Button m_E_SaleButton = null;
		private UnityEngine.UI.Image m_E_SaleImage = null;
		private TMPro.TextMeshProUGUI m_E_SaleMoney_textTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_E_ReclaimButton = null;
		private UnityEngine.UI.Image m_E_ReclaimImage = null;
		private UnityEngine.UI.Button m_E_UpgradeButton = null;
		private UnityEngine.UI.Image m_E_UpgradeImage = null;
		private UnityEngine.UI.Image m_E_Upgrade_number_iconImage = null;
		private TMPro.TextMeshProUGUI m_E_Upgrade_number_TextTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_E_NotUpgradeableButton = null;
		private UnityEngine.UI.Image m_E_NotUpgradeableImage = null;
		private UnityEngine.RectTransform m_EG_MyTowerDescRectTransform = null;
		private UnityEngine.UI.Image m_EG_MyTowerDescImage = null;
		private UnityEngine.UI.Image m_E_IconImage = null;
		private TMPro.TextMeshProUGUI m_E_TextDescTextMeshProUGUI = null;
		private UnityEngine.UI.Image m_EImage_Label1Image = null;
		private TMPro.TextMeshProUGUI m_ELabel_Label1TextMeshProUGUI = null;
		private UnityEngine.UI.Image m_EImage_Label2Image = null;
		private TMPro.TextMeshProUGUI m_ELabel_Label2TextMeshProUGUI = null;
		private UnityEngine.RectTransform m_EG_OtherTowerDescRectTransform = null;
		private UnityEngine.UI.Image m_EG_OtherTowerDescImage = null;
		private UnityEngine.UI.Image m_E_OtherIconImage = null;
		private TMPro.TextMeshProUGUI m_E_OtherTowerNameTextMeshProUGUI = null;
		private UnityEngine.UI.Image m_E_OtherIconStar1Image = null;
		private UnityEngine.UI.Image m_E_OtherIconStar2Image = null;
		private UnityEngine.UI.Image m_E_OtherIconStar3Image = null;
		private TMPro.TextMeshProUGUI m_E_TextOtherDescTextMeshProUGUI = null;
		public Transform uiTransform = null;
	}
}
