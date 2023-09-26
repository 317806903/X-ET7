
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgBattleTowerHUD))]
	[EnableMethod]
	public class DlgBattleTowerHUDViewComponent : Entity, IAwake, IDestroy
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

		public UnityEngine.UI.Image E_OperatorMenuImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_OperatorMenuImage == null )
				{
					this.m_E_OperatorMenuImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_OperatorMenu");
				}
				return this.m_E_OperatorMenuImage;
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
					this.m_E_SaleButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_OperatorMenu/E_Sale");
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
					this.m_E_SaleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_OperatorMenu/E_Sale");
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
					this.m_E_SaleMoney_textTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_OperatorMenu/E_Sale/money/E_SaleMoney_text");
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
					this.m_E_ReclaimButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_OperatorMenu/E_Reclaim");
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
					this.m_E_ReclaimImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_OperatorMenu/E_Reclaim");
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
					this.m_E_UpgradeButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_OperatorMenu/E_Upgrade");
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
					this.m_E_UpgradeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_OperatorMenu/E_Upgrade");
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
					this.m_E_Upgrade_number_iconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_OperatorMenu/E_Upgrade/number/E_Upgrade_number_icon");
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
					this.m_E_Upgrade_number_TextTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_OperatorMenu/E_Upgrade/number/E_Upgrade_number_Text");
				}
				return this.m_E_Upgrade_number_TextTextMeshProUGUI;
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
					this.m_E_TowerNameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_OperatorMenu/Image/E_TowerName");
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
					this.m_E_IconStar1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_OperatorMenu/Image/E_IconStar/E_IconStar1");
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
					this.m_E_IconStar2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_OperatorMenu/Image/E_IconStar/E_IconStar2");
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
					this.m_E_IconStar3Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_OperatorMenu/Image/E_IconStar/E_IconStar3");
				}
				return this.m_E_IconStar3Image;
			}
		}

		public void DestroyWidget()
		{
			this.m_E_Sprite_BGButton = null;
			this.m_E_Sprite_BGImage = null;
			this.m_E_OperatorMenuImage = null;
			this.m_E_SaleButton = null;
			this.m_E_SaleImage = null;
			this.m_E_SaleMoney_textTextMeshProUGUI = null;
			this.m_E_ReclaimButton = null;
			this.m_E_ReclaimImage = null;
			this.m_E_UpgradeButton = null;
			this.m_E_UpgradeImage = null;
			this.m_E_Upgrade_number_iconImage = null;
			this.m_E_Upgrade_number_TextTextMeshProUGUI = null;
			this.m_E_TowerNameTextMeshProUGUI = null;
			this.m_E_IconStar1Image = null;
			this.m_E_IconStar2Image = null;
			this.m_E_IconStar3Image = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_Sprite_BGButton = null;
		private UnityEngine.UI.Image m_E_Sprite_BGImage = null;
		private UnityEngine.UI.Image m_E_OperatorMenuImage = null;
		private UnityEngine.UI.Button m_E_SaleButton = null;
		private UnityEngine.UI.Image m_E_SaleImage = null;
		private TMPro.TextMeshProUGUI m_E_SaleMoney_textTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_E_ReclaimButton = null;
		private UnityEngine.UI.Image m_E_ReclaimImage = null;
		private UnityEngine.UI.Button m_E_UpgradeButton = null;
		private UnityEngine.UI.Image m_E_UpgradeImage = null;
		private UnityEngine.UI.Image m_E_Upgrade_number_iconImage = null;
		private TMPro.TextMeshProUGUI m_E_Upgrade_number_TextTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_E_TowerNameTextMeshProUGUI = null;
		private UnityEngine.UI.Image m_E_IconStar1Image = null;
		private UnityEngine.UI.Image m_E_IconStar2Image = null;
		private UnityEngine.UI.Image m_E_IconStar3Image = null;
		public Transform uiTransform = null;
	}
}
